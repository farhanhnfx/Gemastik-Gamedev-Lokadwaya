using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCam : MonoBehaviour
{
    public Transform player;
    public BoxCollider2D mapBounds;
    public Vector3 offset = new Vector3(1, 2, -10);
    Vector3 currrentVelocity;
    public float xMin, xMax, yMin, yMax;
    public float camX, camY;
    public float camOrthoSize;
    public float camRatio;
    public Vector3 smoothPos;
    public float smoothSpeed = 0.25f;

    private void Start() {
        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;
        camOrthoSize = GetComponent<Camera>().orthographicSize;
        camRatio = (xMax + camOrthoSize) / 4.0f; // asline 2.0f
    }
    private void LateUpdate() {
        camX = Mathf.Clamp(player.transform.position.x + offset.x, xMin + camRatio, xMax - camRatio);
        camY = Mathf.Clamp(player.transform.position.y + offset.y, yMin + camOrthoSize, yMax - camOrthoSize);
        smoothPos = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(camX, camY, -10), smoothSpeed);
        // smoothPos = Vector3.Lerp(new Vector3(offset.x, offset.y, -10), new Vector3(camX, camY, -10), smoothSpeed);
        // Debug.Log(smoothPos);
        // transform.position = new Vector3(smoothPos.x, smoothPos.y, -10);
        // transform.position = new Vector3(player.position.x + camX, player.position.y + camY, -10);
        // transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref currrentVelocity, smoothSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, smoothPos, ref currrentVelocity, smoothSpeed);
    }
}
