using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Visual Elements to activate")]
    public GameObject backgroundPurple;
    public GameObject iconLeft;
    public GameObject iconRight;

    //When the pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(backgroundPurple != null) backgroundPurple.SetActive(true);
        if(iconLeft != null) iconLeft.SetActive(true);
        if(iconRight != null) iconRight.SetActive(true);
    }

    // When the pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if(backgroundPurple != null) backgroundPurple.SetActive(false);
        if(iconLeft != null) iconLeft.SetActive(false);
        if(iconRight != null) iconRight.SetActive(false);
    }
}
