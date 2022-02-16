using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeacherLogin : MonoBehaviour
{
    [SerializeField]
    private InputField UserNasmeInputField;
    [SerializeField]
    private InputField PassWordInputField;

    void OnClick()
    {
        if (UserNasmeInputField.text == "teacher" && PassWordInputField.text == "123")
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            return;
        }
    }
}
