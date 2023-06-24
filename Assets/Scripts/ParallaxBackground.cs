using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Camera cam;
    public float parallaxEffect;
    private float length;
    private Vector2 startPos;
    
    private void Start() {
        cam = Camera.main;
        startPos = transform.position;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }
    private void Update() {
        Vector2 relativePos = cam.transform.position*parallaxEffect;
        Vector2 dist = (Vector2)cam.transform.position - relativePos;
        if (dist.x > startPos.x + length) {
            startPos.x += length;
        }
        else if (dist.x < startPos.x - length) {
            startPos.x -= length;
        }
        transform.position = startPos + relativePos;
    }
}