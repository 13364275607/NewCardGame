using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectSql : MonoBehaviour
{
    [SerializeField]
    private InputField UserNasmeInputField;
    [SerializeField]
    private InputField PassWordInputField;
    [SerializeField]
    private Button LoginButton;
    [SerializeField]
    private Button RegisterButton;
    // private string[] str ;
    private string[] tringBuilder;
    [SerializeField]
    private Text ShowText;

    private void Start()
    {
      
        LoginButton.onClick.AddListener(LoginButtonEvent);
        RegisterButton.onClick.AddListener(RegisterButtonEvent);
    }

    private void RegisterButtonEvent()
    {
        string strString= UserNasmeInputField.text + ":" + PassWordInputField.text;
         tringBuilder = strString.Split(':');
        if (!string.IsNullOrEmpty(tringBuilder[0]) && !string.IsNullOrEmpty(tringBuilder[1]))
        {

            StartCoroutine(Register(tringBuilder));
        }
    }

    private void LoginButtonEvent()
    {
        string strString = UserNasmeInputField.text + ":" + PassWordInputField.text;
        tringBuilder = strString.Split(':');
        if (!string.IsNullOrEmpty(tringBuilder[0]) && !string.IsNullOrEmpty(tringBuilder[1]))
        {
            if (UserNasmeInputField.text == "teacher" && PassWordInputField.text == "123")
            {
                StartCoroutine(Login(tringBuilder));
                SceneManager.LoadScene("TeacherSetting");
            }
            else
            {
                StartCoroutine(Login(tringBuilder));
            }
        }
       
       
    }

    private void PassWordInputFieldEvent(string arg0)
    {
        if (!string.IsNullOrEmpty(arg0))
        {
            //str[1] += PassWordInputField.text;
        }
    }

    private void UserNasmeInputFieldEvent(string arg0)
    {
        if (!string.IsNullOrEmpty(arg0))
        {
            //str[0] += UserNasmeInputField.text;
        }
    }

    /// <summary>
    /// 注册账号。
    /// </summary>
    /// <param name="strRegister"></param>
    /// <returns></returns>
    private IEnumerator Register(string[] strRegister)
    {
        string connStr = "Database=game01;datasource=127.0.0.1;port=3306;user=root;pwd=root;";
        MySqlConnection conn = new MySqlConnection(connStr);
        conn.Open();
        //先要查询一下目前数据库是否有重复的数据。
        MySqlCommand myCommand = new MySqlCommand("select*from user", conn);
        MySqlDataReader reader = myCommand.ExecuteReader();
        List<string> user = new List<string>();
        while (reader.Read())
        {
            string username = reader.GetString("username");
            string password = reader.GetString("password");
            user.Add(username);
        }
        //**避免账号重复。**
        foreach (var item in user)
        {
            if (user.Contains(strRegister[0]))
            {
                //SendMsg(new MsgBase((ushort)UIEvent.UI.GetLoginPanel));
                // SendMsg(new MsgString((ushort)UIEvent.Login.ShowFailMessage, "账号已存在！"));
                Debug.LogError("账号已存在！");
                break;
            }
            else
            {
                reader.Close();//**先将查询的功能关闭。**
                MySqlCommand cmd = new MySqlCommand("insert into user set username ='" + strRegister[0] + "'" + ",password='" + strRegister[1] + "'", conn);
                cmd.Parameters.AddWithValue("un", strRegister[0]);
                cmd.Parameters.AddWithValue("pwd", strRegister[1]);
                cmd.ExecuteNonQuery();

                ShowText.text = "注册成功";
                Debug.Log("注册成功！");
                break;
            }
        }
        yield return 0;
    }


    /// <summary>
    /// 登录。
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private IEnumerator Login(string[] str)
    {
        Dictionary<string, string> myDic = new Dictionary<string, string>();
        myDic.Clear();
        Debug.Log("str[0] " + str[0] + " str[1] " + str[1]);
        string connStr = "Database=game01;datasource=127.0.0.1;port=3306;user=root;pwd=root;";
        MySqlConnection conn = new MySqlConnection(connStr);
        conn.Open();
        #region 查询
        MySqlCommand cmd = new MySqlCommand("select * from user", conn);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string username = reader.GetString("username");
            string password = reader.GetString("password");
            myDic.Add(username, password);
        }
        if (myDic.ContainsKey(str[0]))
        {
            Debug.Log("账号存在！");
            string vale;
            if (myDic.TryGetValue(str[0], out vale))
            {
                if (vale == str[1])
                {
                    ShowText.text = "登录成功";
                    Debug.Log("登录成功！");
                    SceneManager.LoadScene("Main");
                }
                else
                {
                }
            }
        }
        else
        {
            Debug.Log("账号不存在！");
           // SendMsg(new MsgBase((ushort)UIEvent.UI.GetLogPanel));
           // SendMsg((new MsgString((ushort)UIEvent.Login.ShowFailMessage, "账号不存在，请重新输入！")));
        }
        #endregion
        reader.Close();//关闭读取
        conn.Close();//关闭与数据库的连接
        yield return 0;
    }

   
}
