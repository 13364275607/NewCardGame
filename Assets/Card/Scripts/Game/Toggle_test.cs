using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle_test : MonoBehaviour
{
    private Toggle[] toggles;
    List<string> wordList = new List<string>();

    void Start()
    {
        toggles = transform.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            Toggle toggle = toggles[i];
            toggle.onValueChanged.AddListener((bool value) => OnToggleClick(toggle, value));
        }
    }
    void Update()
    {

    }
    void OnToggleClick(Toggle toggle, bool isSwitch)
    {
        if (isSwitch)
        {
            Text text = toggle.gameObject.GetComponentInChildren<Text>();
            wordList.Add(text.text.ToString());
            for (int i = 0; i < wordList.Count; i++)
            {
                Debug.Log(wordList[i]);
            }
        }
        else
        {
            Text text = toggle.gameObject.GetComponentInChildren<Text>();
            wordList.Remove(text.text.ToString());
            for (int i = 0; i < wordList.Count; i++)
            {
                Debug.Log(wordList[i]);
            }
        }
    }
}
