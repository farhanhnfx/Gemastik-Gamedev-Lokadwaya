using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleDrop : MonoBehaviour
{
    public bool isFilled;
    public bool isSecondFilled;
    public bool isThirdFilled;
    public bool isForthFilled;
    public string key;
    public string secondKey, thirdKey, forthKey;
    [SerializeField]private string finalKey;
    [SerializeField]private Image carakan, pasangan, sandhanganVokal, sandhanganKonsonan;
    private void Start() {
        isFilled = false;
        carakan.transform.localScale = new Vector2(0, 0);
        pasangan.transform.localScale = new Vector2(0, 0);
        sandhanganVokal.transform.localScale = new Vector2(0, 0);
        sandhanganKonsonan.transform.localScale = new Vector2(0, 0);
    }
    public void FillKey(string key) {
        isFilled = true;
        this.key = key;
        carakan.transform.localScale = new Vector2(1, 1);
        carakan.sprite = UIManager.Instance.carakan[UIManager.Instance.AksaraToInt(key)];
    }
    public void FillOtherKey(int type, string key) {
        if (type == 2) {
            isSecondFilled = true;
            this.secondKey = key;
            pasangan.transform.localScale = new Vector2(1, 1);
            pasangan.sprite = UIManager.Instance.pasangan[UIManager.Instance.AksaraToInt(key)];
            pasangan.transform.localPosition = UIManager.Instance.pasanganDefaultPos;
            if (UIManager.Instance.listOfTopPos.Contains(key)) {
                pasangan.transform.localPosition = UIManager.Instance.pasanganCenterRight;
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 75);
                this.GetComponent<BoxCollider2D>().size = new Vector2(120, 30);
            }
            if (UIManager.Instance.listOfLongerCarakan.Contains(this.key)) {
                pasangan.transform.localPosition += (Vector3)UIManager.Instance.pasanganToRight;
            }
        }
        else if (type == 3) {
            isThirdFilled = true;
            this.thirdKey = key;
            sandhanganVokal.transform.localScale = new Vector2(2, 2);
            sandhanganVokal.sprite = UIManager.Instance.GetSandhanganSprite(key);
            sandhanganVokal.transform.localPosition = UIManager.Instance.sandhanganDefaultPos;
            if (this.GetComponent<RectTransform>().sizeDelta.x == 150) {
                sandhanganVokal.transform.localPosition += new Vector3(37.5f, 0);
            }
        }
        else if (type == 4) {
            isForthFilled = true;
            this.forthKey = key;
            sandhanganKonsonan.transform.localScale = new Vector2(2, 2);
            sandhanganKonsonan.sprite = UIManager.Instance.GetSandhanganSprite(key);
            sandhanganKonsonan.transform.localPosition = UIManager.Instance.sandhanganDefaultPos;
            if (key.Equals("r") || key.Equals("ng")) {
                if (thirdKey.Equals("ê") || thirdKey.Equals("i")) {
                    sandhanganVokal.transform.localPosition += (Vector3)UIManager.Instance.sandhanganToLeft;
                    sandhanganKonsonan.transform.localPosition += (Vector3)UIManager.Instance.sandhanganToRight;
                }
            }
            if (this.GetComponent<RectTransform>().sizeDelta.x == 150) {
                sandhanganKonsonan.transform.localPosition += new Vector3(37.5f, 0);
            }
        }
    }
    public void Clear() {
        isFilled = false;
        key = null;
    }
    public string GetFinalKey() {
        string res = key;
        if (secondKey != "") {
            res = res.Replace(res[res.Length-1].ToString(), secondKey);
            if (thirdKey != "") {
                res = res.Replace(res[res.Length-1].ToString(), thirdKey);
            }
            if (forthKey != "") {
                res += forthKey;
            }
        }
        else {
            if (thirdKey != "") {
                res = res.Replace(res[res.Length-1].ToString(), thirdKey);
            }
            if (forthKey != "") {
                res += forthKey;
            }
        }
        return res;
    }
    private void Update() {
        finalKey = GetFinalKey();
    }
    // private void Drop(WordPuzzle word) {
    //     isFilled = true;
    //     word.transform.position = this.transform.position;
    // }
    // private void OnEnable() {
    //     WordPuzzle.OnPuzzleDragged += Drop;
    // }
    // private void OnDisable() {
    //     WordPuzzle.OnPuzzleDragged -= Drop;
    // }
}
