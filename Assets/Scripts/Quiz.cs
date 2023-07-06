using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    private bool isPuzzle;
    public Transform puzzleDropContainer;
    public GameObject puzzleDropObject;
    public Transform puzzleKeyContainer;
    public GameObject puzzleKeyObject;
    public GameObject translateInput;
    [SerializeField]private Text instructionText;
    [SerializeField]private Image questionImg;
    [SerializeField]private Text questionText;
    [SerializeField]private Button retryPuzzle;
    [SerializeField]private Button answerButton;
    // public string question;
    public string answerKey;

    private void Start() {
        // puzzleDropContainer = GameObject.Find("Word Puzzle Sheet").transform;
        // puzzleKeyContainer = GameObject.Find("Word Puzzle Keys").transform;
        // instructionText = transform.GetChild(1).GetComponent<Text>();
        // questionImg = transform.GetChild(2).GetComponent<Image>();
        // questionText = transform.GetChild(3).GetComponent<Text>();
        // retryPuzzle = GameObject.Find("Retry Puzzle").GetComponent<Button>();
        retryPuzzle.onClick.AddListener(delegate { ResetPuzzle(); });
        // answerButton = GameObject.Find("Answer Button").GetComponent<Button>();
        answerButton.onClick.AddListener(delegate { Answer(); });
    }

    public void GeneratePuzzle() {
        isPuzzle = true;
        QuizSetting.QuizData quizData = QuizDataHelper.Instance.GetQuizData("1b");
        translateInput.SetActive(false);
        instructionText.text = "Susun aksara hingga membentuk kata di bawah";
        answerKey = quizData.question;
        questionText.enabled = true;
        questionImg.enabled = false;
        questionText.text = answerKey.ToUpper();
        if (quizData.items.Count > 0) {
            foreach (QuizSetting.QuizItem item in quizData.items) {
                if (item.type == 1) {
                    GameObject drop = Instantiate(puzzleDropObject, puzzleDropContainer);
                }
                GameObject key = Instantiate(puzzleKeyObject, puzzleKeyContainer);
                key.GetComponent<WordPuzzle>().type = item.type;
                key.GetComponent<WordPuzzle>().java = item.java;
                key.GetComponent<WordPuzzle>().ProcessImage(item.java);
            }
        }
    }
    public void GenerateTranslate() {
        isPuzzle = false;
        QuizSetting.QuizData quizData = QuizDataHelper.Instance.GetQuizData("1b");
        translateInput.SetActive(true);
        puzzleDropContainer.gameObject.SetActive(false);
        puzzleKeyContainer.gameObject.SetActive(false);
        questionText.enabled = false;
        questionImg.enabled = true;
        instructionText.text = "Apa terjemahan dari aksara di bawah ini?";
        questionImg.sprite = Resources.Load<Sprite>(quizData.sideImgPath);
        questionImg.SetNativeSize();
        Debug.Log(quizData.sideImgPath);
    }
    private void ResetPuzzle() {
        if (isPuzzle) {
            if (puzzleDropContainer.childCount > 0) {
                foreach (Transform t in puzzleDropContainer) {
                    Destroy(t.gameObject);
                }
                foreach (Transform t in puzzleKeyContainer) {
                    Destroy(t.gameObject);
                }
                GeneratePuzzle();
            }
        }
    }
    private void Answer() {
        if (GetPlayerAnswer().Replace("h", "").Equals(answerKey.Replace(" ", ""))) {
            Debug.Log("Correct answer! Continued to the next level");
        }
        else {
            LevelManager.Instance.timeRemaining -= 30; // if wrong then do reset puzzle
            ResetPuzzle();
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetPuzzle();
        }
    }
    private string GetPlayerAnswer() {
        string res = "";
        foreach (Transform t in puzzleDropContainer) {
            res += t.GetComponent<PuzzleDrop>().GetFinalKey();
        }
        return res;
    }
}
