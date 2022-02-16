using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LevelsManager : MonoBehaviour
{
    public float initialDelay = 3;//开始前的最初延迟。
    public float dalayBetweenCells = 0.1f;//每一个的延迟
    public Vector2 cellSize = new Vector2(150, 150);//网格中卡的大小
    public Vector2 spacing = new Vector2(5, 5);//卡牌间的间距
    public bool randomShuffleOnBuild; //是否在网格构建时进行随机洗牌。
    public bool matchGrid = true;//是否匹配网格
    public bool singleLevel;//是否将任务作为一个级别。这样做将跳过关卡场景，直接进入一个关卡（第一关卡）的游戏场景
    public Sprite defaultNormalSprite;
    public Sprite defaultOnClickSprite;
    public Sprite defaultBackgroundSprite;
    public readonly static int rowsLimit = 12;//行数限制
    public readonly static int colsLimit = 12;//列数限制
    public List<Level> levels = new List<Level>();
}
