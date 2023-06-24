using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizSettings : MonoBehaviour
{

    [Range(1, 3)]public int quizType; // 1: Puzzle, 2: Translate, 3: ?
    public LevelManager levelManager;
    public Transform puzzleDropContainer;
    public GameObject puzzleDropObject;
    public Transform puzzleKeyContainer;
    public GameObject puzzleKeyObject;
    public GameObject translateInput;
    private Text instructionText;
    private Text questionText;
    private Button retryPuzzle;
    private Button answerButton;

    private void Start() {
        levelManager = LevelManager.Instance;
        puzzleDropContainer = GameObject.Find("Word Puzzle Sheet").transform;
        puzzleKeyContainer = GameObject.Find("Word Puzzle Keys").transform;
        instructionText = transform.GetChild(1).GetComponent<Text>();
        questionText = transform.GetChild(2).GetComponent<Text>();
        retryPuzzle = GameObject.Find("Retry Puzzle").GetComponent<Button>();
        retryPuzzle.onClick.AddListener(delegate { ResetPuzzle(); });
        answerButton = GameObject.Find("Answer Button").GetComponent<Button>();
        answerButton.onClick.AddListener(delegate { Answer(); });

        switch (quizType) {
            case 1:
                GenerateKeys();
                translateInput.SetActive(false);
                instructionText.text = "Susun aksara hingga membentuk kata di bawah";
            break;
            case 2:
                translateInput.SetActive(true);
                puzzleDropContainer.gameObject.SetActive(false);
                puzzleKeyContainer.gameObject.SetActive(false);
                instructionText.text = "Apa terjemahan dari aksara di bawah ini?";
            break;
            default:
                GenerateKeys();
                translateInput.SetActive(false);
            break;
        }
    }
    private void GenerateKeys() {
        if (levelManager.itemsDrop.Count > 0) {
            foreach (Item i in levelManager.itemsDrop) {
                GameObject drop = Instantiate(puzzleDropObject, puzzleDropContainer);
                GameObject key = Instantiate(puzzleKeyObject, puzzleKeyContainer);
                // key.GetComponent<WordPuzzle>().puzzleText.text = i.java;
                key.GetComponent<WordPuzzle>().type = i.type;
                key.GetComponent<WordPuzzle>().java = i.java;
                key.GetComponent<WordPuzzle>().ProcessImage(i.java);
            }
        }
        else {
            Invoke(nameof(GenerateKeys), 1f);
        }
    }
    private void ResetPuzzle() {
        if (puzzleDropContainer.childCount > 0) {
            foreach (Transform t in puzzleDropContainer) {
                Destroy(t.gameObject);
            }
            foreach (Transform t in puzzleKeyContainer) {
                Destroy(t.gameObject);
            }
            GenerateKeys();
        }
    }
    private void Answer() {
        LevelManager.Instance.timeRemaining -= 30; // if wrong then do reset puzzle 
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetPuzzle();
        }
    }
}
