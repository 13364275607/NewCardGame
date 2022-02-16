using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ConfigManagerByJson
{
    
    private Dictionary<string, string> _JsonConfig_2;

    public Dictionary<string, string> JsonConfig_2
    {
        get { return _JsonConfig_2; }
    }
    public ConfigManagerByJson(string jsonPath)   //构造函数
    {
        _JsonConfig_2 = new Dictionary<string, string>();
        InitAndAnalysisJson(jsonPath);
    }

    private void InitAndAnalysisJson(string jsonPath) //1.根据路径解析Json数据 2.加载到字典保存
    {
        string strReadContent = System.IO.File.ReadAllText(jsonPath);
        Info InfoObj = JsonUtility.FromJson<Info>(strReadContent);

        foreach (Node nodeinfo in InfoObj.ConfigInfo_2)
        {
            _JsonConfig_2.Add(nodeinfo.name, nodeinfo.path);
        }
    }
}

[Serializable]
class Info
{
    public List<Node> ConfigInfo_2 = null;
}

[Serializable]
class Node
{
    public string name = null;
    public string path = null;
}
