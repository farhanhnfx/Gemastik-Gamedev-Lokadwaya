using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public ActionButton doInteract;
    public ActionButton moveRight;
    public ActionButton moveLeft;
    public ActionButton jumpButton;
    public Button closeInteract;
    public Animator itemRevealAnim;
    public Transform quizContainer;
    public List<Sprite> carakan = new List<Sprite>();
    public List<Sprite> pasangan = new List<Sprite>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
    private void Start() {
        itemRevealAnim = GameObject.Find("Item Reveal").GetComponent<Animator>();
        // doInteract.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ShowItemDetail(); });
        closeInteract.onClick.AddListener(delegate { CloseItemDetail(); });
        quizContainer.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { CloseQuiz(); });
        quizContainer.gameObject.SetActive(false);
    }
    public void setRevealed(Item item) {
        ProcessImage(item);
        // itemRevealAnim.transform.GetChild(0).GetComponent<Text>().text = item.java;
        itemRevealAnim.transform.GetChild(1).GetComponent<Text>().text = item.trans;
    }
    public void ProcessImage(Item item) {
        if (item.type == 1) {
            itemRevealAnim.transform.GetChild(0).GetComponent<Image>().sprite = carakan[AksaraToInt(item.java)];
        }
        else if (item.type == 2) {
            itemRevealAnim.transform.GetChild(0).GetComponent<Image>().sprite = pasangan[AksaraToInt(item.java)];
        }
        else if (item.type == 3) {

        }
    }
    private void CloseItemDetail() {
        // itemRevealAnim.gameObject.SetActive(false);
        itemRevealAnim.Play("Item_close");
        closeInteract.gameObject.SetActive(false);
    }
    private void CloseQuiz() {
        quizContainer.gameObject.SetActive(false);
    }
    public int AksaraToInt(string java) {
        switch (java) {
            case "ha":
                return 0;
            case "na":
                return 1;
            case "ca":
                return 2;
            case "ra":
                return 3;
            case "ka":
                return 4;
            case "da":
                return 5;
            case "ta":
                return 6;
            case "sa":
                return 7;
            case "wa":
                return 8;
            case "la":
                return 9;
            case "pa":
                return 10;
            case "dha":
                return 11;
            case "ja":
                return 12;
            case "ya":
                return 13;
            case "nya":
                return 14;
            case "ma":
                return 15;
            case "ga":
                return 16;
            case "ba":
                return 17;
            case "tha":
                return 18;
            case "nga":
                return 19;
            default:
                return -1;
        }
    }
}
