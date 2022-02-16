using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TeacherSettingManager : MonoBehaviour
{
    public GameObject SelectForm;
    public GameObject SelectWords;
    public Text TiShi;

    private Toggle[] toggles;
    public List<string> wordList = new List<string>();
    public List<string> cardPath = new List<string>();

    public Dictionary<string, string> _DicUIFormPaths_2;
    public int i = 10;
    public bool j = true;
    void Start()
    {
        TiShi.text = "您还可以选择" + i + "个单词！";
        Button SaveBtn = GameObject.Find("SaveBtn").GetComponent<Button>();
        SaveBtn.onClick.AddListener(SaveGame);

        InitUIPathData();

        toggles = transform.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            Toggle toggle = toggles[i];
            toggle.onValueChanged.AddListener((bool value) => OnToggleClick(toggle, value));
        }
    }
    public void SaveGame()
    {
        ShowCard_RequestByOrder();
        SceneManager.LoadScene(2);
    }
    public void ShowCard_RequestByOrder() //根据需求json加载卡牌库json中的卡牌(顺序加载)
    {
        SelectForm.gameObject.SetActive(false);
        SelectWords.gameObject.SetActive(true);
        string Card_Path;
        if (cardPath.Count>0)
        {
            cardPath.Clear();
        }
        for (int i = 0; i < wordList.Count; i++)
        {
            _DicUIFormPaths_2.TryGetValue(wordList[i], out Card_Path); //这里的Card_Path是卡牌图片的路径
            cardPath.Add(Card_Path);   
        }
        MissionManager.Instance.setCardList(cardPath);
    }
    public void OnToggleClick(Toggle toggle, bool isSwitch)
    {
        if (isSwitch)
        {
            Text text = toggle.gameObject.GetComponentInChildren<Text>();
            if(i>0)
            {
                wordList.Add(text.text.ToString());
                i--;
                TiShi.text = "您还可以选择" + i + "个单词！";
                j = false;
            }
            else
            {
                TiShi.text = "您不能再选择单词了！";
                j = true;
                //wordList.Remove(text.text.ToString());
                toggle.isOn = false;

            }
        }
        else
        {
            if (i >= 0)
            {
                if(j)
                {
                    j = false;
                }
                else
                {
                    Text text = toggle.gameObject.GetComponentInChildren<Text>();
                    wordList.Remove(text.text.ToString());
                    i++;
                    TiShi.text = "您还可以选择" + i + "个单词！";
                    j = false;
                }
                
            }
        }
    }
    private void InitUIPathData()
    {   //得到Json路径
        string strJsonDeployPath_2 = Application.dataPath + "/Plugins/UIConfigInfo_2.json";
        //调用Json配置文件管理器，Json数据被转成字典存储
        ConfigManagerByJson configMgr_2 = new ConfigManagerByJson(strJsonDeployPath_2);
        if (configMgr_2 != null)
        {
            _DicUIFormPaths_2 = configMgr_2.JsonConfig_2;
        }
    }
}
