using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordPuzzle : MonoBehaviour
{
    public RectTransform rectTransform;
    [Range(1, 3)]public int type; // 1: carakan, 2: sandhangan, 3: pasangan?
    private Vector3 offset;
    private bool isDropped = false;
    [SerializeField]private Image img;
    [SerializeField]private PuzzleDrop puzzleDrop;
    public string java;
    
    private void Start() {
        // img = GetComponentInChildren<Image>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PuzzleDrop")) {
            // Debug.Log("Drop here: " + other.name);
            // this.transform.position = other.transform.position;
            // isDropped = true;
            // other.GetComponent<PuzzleDrop>().isFilled = true;
            puzzleDrop = other.GetComponent<PuzzleDrop>();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("PuzzleDrop")) {
            // puzzleDrop.isFilled = false;
            // puzzleDrop.Clear();
            puzzleDrop.key.Replace(java, "");
            puzzleDrop = null;
        }
    }
    public void MoveObject()
    {
        if (!isDropped) {
            rectTransform.position = Input.mousePosition + offset;
        }
    }
    public void OnEndDrag() {
        offset = rectTransform.position - Input.mousePosition;
        if (puzzleDrop != null) {
            if (!puzzleDrop.isFilled) {
                if (type == 1) {
                    puzzleDrop.FillKey(java);
                    isDropped = true;
                    this.gameObject.SetActive(false);
                }
            }
            else {
                // if (type != 1) {
                //     this.transform.position = puzzleDrop.transform.position;
                //     // puzzleText.text += puzzleDrop.key;
                //     // puzzleDrop.FillKey(java);
                //     // puzzleDrop.addition.Push(java);
                //     // isDropped = true;
                //     puzzleDrop.FillOtherKey(type, java);
                //     this.gameObject.SetActive(false);
                // }
                if ((type == 2 && !puzzleDrop.isSecondFilled) ||
                    (type == 3 && !puzzleDrop.isThirdFilled) ||
                    (type == 4 && !puzzleDrop.isForthFilled)) {
                    puzzleDrop.FillOtherKey(type, java);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
    public void ProcessImage(string java) {
        if (type == 1) {
            img.sprite = UIManager.Instance.carakan[UIManager.Instance.AksaraToInt(java)];
        }
        else if (type == 2) {
            img.sprite = UIManager.Instance.pasangan[UIManager.Instance.AksaraToInt(java)];
        }
        else if (type == 3 || type == 4) {
            img.sprite = UIManager.Instance.GetSandhanganSprite(java);
        }
    }
}
