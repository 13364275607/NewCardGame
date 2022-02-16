using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnMouseEvent : MonoBehaviour
{
    void Start()
    {
        Button button = this.GetComponent<Button>();
        UIEventListener ButtonListener = button.gameObject.AddComponent<UIEventListener>();

        ButtonListener.OnMouseEnter += delegate (GameObject gameobject)
        {
            button.transform.localScale *= 2;
        };

        ButtonListener.OnMouseExit += delegate (GameObject gameobject)
        {
            button.transform.localScale *= 0.5f;
        };
    }

}
