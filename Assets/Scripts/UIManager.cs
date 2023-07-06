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
    public Button inventoryButton;
    public Button closeInteract;
    public Animator itemRevealAnim;
    public Transform quizContainer;
    public List<Sprite> carakan = new List<Sprite>();
    public List<Sprite> pasangan = new List<Sprite>();
    public List<Sprite> sandhanganVokal = new List<Sprite>();
    public List<Sprite> sandhanganKonsonan = new List<Sprite>();
    public Vector2 pasanganDefaultPos;
    public Vector2 pasanganToRight;
    public Vector2 pasanganCenterRight;
    public Vector2 sandhanganDefaultPos;
    public Vector2 sandhanganToLeft;
    public Vector2 sandhanganToRight;
    public List<string> listOfTopPos = new List<string>{"ha", "sa", "pa"};
    public List<string> listOfLongerCarakan = new List<string>{"ha", "ka", "ta", "la", "ya", "nya", "ga", "ba"};

    private bool isItemFacingFront;

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
        inventoryButton.onClick.AddListener(delegate { ShowInventory(); });
        closeInteract.onClick.AddListener(delegate { CloseItemDetail(); });
        quizContainer.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { CloseQuiz(); });
        quizContainer.gameObject.SetActive(false);
        pasanganDefaultPos = new Vector2(0, -43);
        pasanganToRight = new Vector2(8, 0);
        pasanganCenterRight = new Vector2(70, 0);
        sandhanganDefaultPos = new Vector2(0, 2);
        sandhanganToLeft = new Vector2(-7.5f, 0);
        sandhanganToRight = -sandhanganToLeft;

        itemRevealAnim.GetComponent<Button>().onClick.AddListener(delegate { FlipCard(); });
    }
    public void setRevealed(Item item) {
        ProcessImage(item);
        // itemRevealAnim.transform.GetChild(0).GetComponent<Text>().text = item.java;
        itemRevealAnim.transform.GetChild(1).GetComponent<Text>().text = item.trans;
        isItemFacingFront = true;
    }
    public void ProcessImage(Item item) {
        if (item.type == 1) {
            itemRevealAnim.transform.GetChild(0).GetComponent<Image>().sprite = carakan[AksaraToInt(item.java)];
        }
        else if (item.type == 2) {
            itemRevealAnim.transform.GetChild(0).GetComponent<Image>().sprite = pasangan[AksaraToInt(item.java)];
        }
        else if (item.type == 3 || item.type == 4) {
            itemRevealAnim.transform.GetChild(0).GetComponent<Image>().sprite = GetSandhanganSprite(item.java);
        }
    }
    private void CloseItemDetail() {
        // itemRevealAnim.gameObject.SetActive(false);
        if (isItemFacingFront) {
            itemRevealAnim.Play("Item_close");
            closeInteract.gameObject.SetActive(false);
        }
        else {
            FlipCard();
            Invoke(nameof(CloseItemDetail), 1f);
        }
    }
    private void CloseQuiz() {
        quizContainer.gameObject.SetActive(false);
    }
    private void ShowInventory() {
        Inventory.Instance.gameObject.SetActive(true);
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
    public Sprite GetSandhanganSprite(string java) {
        switch(java) {
            case "i":
                return sandhanganVokal[0];
            case "u":
                return sandhanganVokal[1];
            case "e":
                return sandhanganVokal[2];
            case "ê":
                return sandhanganVokal[3];
            case "o":
                return sandhanganVokal[4];
            case "r":
                return sandhanganKonsonan[0];
            case "ng":
                return sandhanganKonsonan[1];
            case "h":
                return sandhanganKonsonan[2];
            case "/":
                return sandhanganKonsonan[3];
            case "ra":
                return sandhanganKonsonan[4];
            default:
                return null;
        }
    }
    public void FlipCard() {
        if (isItemFacingFront) {
            itemRevealAnim.Play("Item_to_back");
        }
        else {
            itemRevealAnim.Play("Item_to_front");
        }
        isItemFacingFront = !isItemFacingFront;
    }

}
