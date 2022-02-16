using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Level//与LevelsManager组件一起使用。使用Inspector创建新关卡时，将创建此类的Instance
{
    public bool showLevel;
    [HideInInspector]
    public int previousNumberOfRows = -1;//以前的行数
    [HideInInspector]
    public int previousNumberOfCols = -1;//以前的列数
    public int numberOfColumns = 4;//列数
    public int numberOfRows = 4;//行数
    public int timeLimit = 60;//关卡的时间限制
    public int threeStarsTimePeriod = 30;//三个星星的时间长
    public int twoStarsTimePeriod = 15;//两个星星的时间长
    public List<Pair> pairs = new List<Pair>();//配对列表
    [System.Serializable]
    public class Pair
    {
        public bool showPair = true;
        public Sprite backgroundSprite;
        public Sprite normalSprite;
        public Sprite onClickSprite;
        public Element firstElement = new Element();
        public Element secondElement = new Element();
    }
    [System.Serializable]
    public class Element//网格中元素的索引。
    {
        public int index;
    }
}
