using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLoader : MonoBehaviour
{
    public LevelsManager mission;
    //public LevelsManager mission2;
    void Start()
    {
        int index = 0;
        foreach (string cardpath in MissionManager.Instance.CardList)
        {
            Object preb = Resources.Load(cardpath, typeof(Sprite));
            Sprite sprite = null;
            sprite = Instantiate(preb) as Sprite;

            mission.levels[0].pairs[index].onClickSprite = sprite;
            index++;

        }

        mission.levels[0].timeLimit = 90;
    }
}