using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGate : MonoBehaviour
{
    private bool playerContact;
    private UIManager UIManager;
    public delegate void Interact(bool isPuzzle);
    public static Interact OnInteract;

    private void Start() {
        UIManager = UIManager.Instance;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || UIManager.doInteract.isPressed)
             && playerContact) {
            if (OnInteract != null) {
                OnInteract(isPuzzle:true);
                Debug.Log("Player opening the gate: quiz unlocked");
            }
        }
    }
    private void ShowInteractButton() {
        if (!UIManager.doInteract.isActiveAndEnabled)
            UIManager.doInteract.gameObject.SetActive(true);
    }
    private void HideInteractButton() {
        if (UIManager.doInteract.isActiveAndEnabled)
            UIManager.doInteract.isPressed = false;
            UIManager.doInteract.gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Debug.Log("plyr");
            playerContact = true;
            ShowInteractButton();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerContact = false;
            HideInteractButton();
        }
    }
}
