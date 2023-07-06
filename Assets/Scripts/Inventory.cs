using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField]private Transform currentLevelContainer;
    [SerializeField]private Transform allLevelContainer;
    [SerializeField]private GameObject itemPrefab;
    [SerializeField]private Image descImg;
    [SerializeField]private Text descTitle;
    [SerializeField]private Text descDetail;
    [SerializeField]private Button closeButton;
    [SerializeField]private List<Item> itemInThisLevel;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
    private void Start() {
        closeButton.onClick.AddListener(delegate { CloseInventory(); });
        CloseInventory();
        foreach (Transform t in GameObject.Find("Item Location").transform) {
            itemInThisLevel.Add(t.GetComponent<Item>());
        }
    }
    public void AddItem(Item item) {
        // check if item id is contained in level setting
        GameObject invItem;
        if (itemInThisLevel.Contains(item)) {
            invItem = Instantiate(itemPrefab, currentLevelContainer);
        }
        else {
            invItem = Instantiate(itemPrefab, allLevelContainer);
        }
        invItem.GetComponent<Item>().java = item.java;
        invItem.GetComponent<Item>().desc = item.desc;
        invItem.GetComponent<Item>().type = item.type;
        invItem.GetComponent<Item>().trans = item.trans;
        invItem.transform.GetChild(0).GetComponent<Image>().sprite = GetAksaraSprite(item);
        invItem.GetComponent<Button>().onClick.AddListener(delegate{ ShowItemDescription(item); });
    }
    private void ShowItemDescription(Item item) {
        descImg.sprite = GetAksaraSprite(item);
        descTitle.text = item.trans;
        descDetail.text = item.desc;
    }
    private Sprite GetAksaraSprite(Item item) {
        UIManager ui = UIManager.Instance;
        switch(item.type) {
            case 1: return ui.carakan[ui.AksaraToInt(item.java)];
            case 2: return ui.pasangan[ui.AksaraToInt(item.java)];
            case 3: return ui.GetSandhanganSprite(item.java);
            case 4: return ui.GetSandhanganSprite(item.java);
            default: return null;
        }
    }
    private void CloseInventory()
    {
        this.gameObject.SetActive(false);
    }
}
