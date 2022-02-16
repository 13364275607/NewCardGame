using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Level//��LevelsManager���һ��ʹ�á�ʹ��Inspector�����¹ؿ�ʱ�������������Instance
{
    public bool showLevel;
    [HideInInspector]
    public int previousNumberOfRows = -1;//��ǰ������
    [HideInInspector]
    public int previousNumberOfCols = -1;//��ǰ������
    public int numberOfColumns = 4;//����
    public int numberOfRows = 4;//����
    public int timeLimit = 60;//�ؿ���ʱ������
    public int threeStarsTimePeriod = 30;//�������ǵ�ʱ�䳤
    public int twoStarsTimePeriod = 15;//�������ǵ�ʱ�䳤
    public List<Pair> pairs = new List<Pair>();//����б�
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
    public class Element//������Ԫ�ص�������
    {
        public int index;
    }
}
