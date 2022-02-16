using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LevelsManager : MonoBehaviour
{
    public float initialDelay = 3;//��ʼǰ������ӳ١�
    public float dalayBetweenCells = 0.1f;//ÿһ�����ӳ�
    public Vector2 cellSize = new Vector2(150, 150);//�����п��Ĵ�С
    public Vector2 spacing = new Vector2(5, 5);//���Ƽ�ļ��
    public bool randomShuffleOnBuild; //�Ƿ������񹹽�ʱ�������ϴ�ơ�
    public bool matchGrid = true;//�Ƿ�ƥ������
    public bool singleLevel;//�Ƿ�������Ϊһ�������������������ؿ�������ֱ�ӽ���һ���ؿ�����һ�ؿ�������Ϸ����
    public Sprite defaultNormalSprite;
    public Sprite defaultOnClickSprite;
    public Sprite defaultBackgroundSprite;
    public readonly static int rowsLimit = 12;//��������
    public readonly static int colsLimit = 12;//��������
    public List<Level> levels = new List<Level>();
}
