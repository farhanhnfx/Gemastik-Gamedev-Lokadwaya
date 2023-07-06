using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Range(1, 4)]public int type = 1; // 1: carakan, 2: pasangan, 3: sandhangan vokal, 4: sandhangan konsonan
    public string java;
    public string trans;
    public string desc;
    private bool playerContact;
    // private LevelManager levelManager;
    public delegate void PickedUp(Item item);
    public static event PickedUp OnPickedUp;
    public delegate void Interact();

    // Start is called before the first frame update
    void Start()
    {
        playerContact = false;
        // levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        if (this.transform.parent.name == "Item Location") {
            LevelManager.Instance.itemsDrop.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || UIManager.Instance.doInteract.isPressed)
             && playerContact) {
            // levelManager.itemsCollected.Add(this);
            // Destroy(this.gameObject);
            // this.gameObject.SetActive(false);
            if (OnPickedUp != null) {
                OnPickedUp(this);
            }
            // Ganti interact object udu destroy
        }
    }
    private void ShowInteractButton() {
        if (!UIManager.Instance.doInteract.isActiveAndEnabled)
            UIManager.Instance.doInteract.gameObject.SetActive(true);
    }
    private void HideInteractButton() {
        if (UIManager.Instance.doInteract.isActiveAndEnabled)
            UIManager.Instance.doInteract.isPressed = false;
            UIManager.Instance.doInteract.gameObject.SetActive(false);
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
