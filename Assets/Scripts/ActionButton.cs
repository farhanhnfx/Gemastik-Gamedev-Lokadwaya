using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        // player.GetComponentInChildren<Animator>().SetBool(actionAnim, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        // player.GetComponentInChildren<Animator>().SetBool(actionAnim, false);
    }
}
