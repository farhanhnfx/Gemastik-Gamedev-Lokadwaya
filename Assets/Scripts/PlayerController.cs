using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private CapsuleCollider2D coll;
    public float speed = 4;
    public float jumpForce = 2;
    // private bool isJump;
    public LayerMask groundLayer = 6;
    public Animator anim;
    private float moveInput;
    [SerializeField]private bool lookRight = true;
    [SerializeField]private bool isJumping;
    private string scene;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        coll = this.GetComponent<CapsuleCollider2D>();
        anim = this.GetComponentInChildren<Animator>();
        scene = SceneManager.GetActiveScene().name;
    }
    private void Run(bool toRight) {
        if (IsGrounded()) anim.SetBool("isRunning", true);
        if (!toRight) {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            // rb.AddForce(speed*Vector2.left, ForceMode2D.Impulse);
        }
        else {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            // rb.AddForce(speed*Vector2.right);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        // Debug.Log(moveInput);
        if (Input.GetKey(KeyCode.RightArrow) || UIManager.Instance.moveRight.isPressed) {
            Run(toRight: true);
            if (!lookRight) {
                FlipChar();
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || UIManager.Instance.moveLeft.isPressed) {
            Run(toRight: false);
            if (lookRight) {
                FlipChar();
            }
        }
        else if (!UIManager.Instance.moveLeft.isPressed || !UIManager.Instance.moveRight.isPressed) {
            anim.SetBool("isRunning", false);
        }

        if ((Input.GetKey(KeyCode.UpArrow) || UIManager.Instance.jumpButton.isPressed) && IsGrounded() && !IsTerrainAbove()) {
            rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
            // isJumping = true;
            anim.SetBool("isJump", true);
            StartCoroutine(ResetJump());
        }
        // if (isJumping) {
        //     rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
        //     anim.SetBool("isJump", true);
        //     isJumping = false;
        //     StartCoroutine(ResetJump());
        // }

        isJumping = anim.GetBool("isJump");
    }
    private void FlipChar() {
        if (!lookRight) {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        lookRight = !lookRight;
    }
    private void Update() {
        // Debug.DrawRay((Vector2)transform.position + Vector2.down, Vector2.down, Color.red);
        Vector2 u = new Vector2(transform.position.x, coll.bounds.min.y);
        Vector2 v = new Vector2(transform.position.x, coll.bounds.max.y);
        Debug.DrawLine(u, u + new Vector2(0, 0.1f), Color.red);
        Debug.DrawLine(v, v + new Vector2(0, 0.4f), Color.red);
        // if ((Input.GetKey(KeyCode.UpArrow) || UIManager.Instance.jumpButton.isPressed) && IsGrounded() && !IsTerrainAbove()) {
        //     isJumping = true;
        // }
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(scene);
        }
    }
    private IEnumerator ResetJump() {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("isJump", false);
    }
    private bool IsGrounded() {
        Vector2 vx = new Vector2(transform.position.x, coll.bounds.min.y);
        RaycastHit2D raycast = Physics2D.Raycast(vx, Vector2.down, 0.1f, groundLayer);
        if (raycast.collider != null) {
        }
        return raycast.collider != null;
    }
    // also check if solid terrain above player
    private bool IsTerrainAbove() {
        Vector2 vx = new Vector2(transform.position.x, coll.bounds.max.y);
        RaycastHit2D raycast = Physics2D.Raycast(vx, Vector2.up, 0.4f, groundLayer);
        return raycast.collider != null;
    }
    private void PickUpItem(Item item) {
        item.gameObject.SetActive(false);
        LevelManager.Instance.itemsCollected.Add(item);
        LevelManager.Instance.UpdateItemsCount();
        // UIManager.Instance.itemRevealAnim.transform.GetChild(0).GetComponent<Text>().text = item.java;
        // UIManager.Instance.itemRevealAnim.transform.GetChild(1).GetComponent<Text>().text = item.trans;
        UIManager.Instance.setRevealed(item);
        // UIManager.Instance.itemRevealAnim.enabled = true;
        UIManager.Instance.itemRevealAnim.Play("Item_reveal");
        UIManager.Instance.closeInteract.gameObject.SetActive(true);
        anim.Play("Player_interact");
        Debug.Log("Player picked up an item!");
        Inventory.Instance.AddItem(item);
    }
    private void OpenQuiz(bool isPuzzle)
    {
        UIManager.Instance.quizContainer.gameObject.SetActive(true);
        if (isPuzzle) {
            LevelManager.Instance.quiz.GeneratePuzzle();
        }
        else {
            LevelManager.Instance.quiz.GenerateTranslate();
        }
        anim.Play("Player_interact");
    }
    private void OnEnable() {
        Item.OnPickedUp += PickUpItem;
        LevelGate.OnInteract += OpenQuiz;
    }

    private void OnDisable() {
        Item.OnPickedUp -= PickUpItem;
        LevelGate.OnInteract -= OpenQuiz;
    }
}
