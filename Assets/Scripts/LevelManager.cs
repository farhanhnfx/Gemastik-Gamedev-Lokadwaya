using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public GameObject player;
    public List<Item> itemsDrop = new List<Item>();
    public List<Item> itemsCollected = new List<Item>();
    public float timeRemaining;
    public bool isOver;
    public Text timeUI;
    public Text itemsCountText;
    private float minutes;
    private float seconds;
    private BoxCollider2D bgColl;
    public Quiz quiz;
    // public Quiz quiz;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bgColl = GameObject.Find("Background").GetComponent<BoxCollider2D>();
        isOver = false;
        Invoke(nameof(UpdateItemsCount), 0.125f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOver) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                UpdateTimeUI(timeRemaining);
            }
            else {
                timeRemaining = 0;
                isOver = true;
                Debug.Log("Game Over!");
            }
            if (PlayerFall()) {
                Debug.Log("Game Over because player falldown!");
                isOver = true;
            }
        }
        else {
            player.GetComponent<PlayerController>().speed = 0;
            // muncul retry
            // reload scene
        }
        // UpdateItemsCount();
    }

    private void UpdateTimeUI(float time)
    {
        time++;
        minutes = Mathf.FloorToInt(time/60);
        seconds = Mathf.FloorToInt(time%60);
        timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void UpdateItemsCount() {
        itemsCountText.text = $"{itemsCollected.Count}/{itemsDrop.Count}";
    }
    private bool PlayerFall() {
        if (player.transform.position.y < bgColl.bounds.min.y) {
            return true;
        }
        else {
            return false;
        }
    }
}
