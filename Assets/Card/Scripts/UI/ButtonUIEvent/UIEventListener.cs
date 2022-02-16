using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEventListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void UIEventProxy(GameObject gb);
    public event UIEventProxy OnMouseEnter;
    public event UIEventProxy OnMouseExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnMouseEnter != null)
            OnMouseEnter(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnMouseExit != null)
            OnMouseExit(this.gameObject);
    }

}
