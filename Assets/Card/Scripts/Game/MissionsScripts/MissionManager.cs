using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public List<string> CardList;
    public int SliderThreeValue;
    //创建静态变量instance
    static MissionManager instance;

    public static MissionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MissionManager>();
            }
            if (instance == null)  //场景里如果没有Manager实例时
            {
                //生成仅一次实例
                GameObject obj = new GameObject("MissionManager");
                obj.AddComponent<MissionManager>();
                instance = obj.GetComponent<MissionManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public void setCardList(List<string> teacherList)
    {
        
        CardList = teacherList;
        
    }
}