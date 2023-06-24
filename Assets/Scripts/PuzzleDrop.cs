using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDrop : MonoBehaviour
{
    public bool isFilled;
    public string key;
    public Stack<string> addition;
    private void Start() {
        isFilled = false;
    }
    public void FillKey(string key) {
        isFilled = true;
        this.key = key;
    }
    public void Clear() {
        isFilled = false;
        key = null;
    }
    public void UpdateKey() {
        key += addition.Peek();
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
