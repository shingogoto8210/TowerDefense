using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "StageDataSO", menuName = "Create StageDataSO")]
public class StageDataSO : ScriptableObject
{
    public List<StageData> stageDatasList = new List<StageData>(); 

    [Serializable]
    public class StageData
    {
        public string stageName;
        [Header("ステージ番号")]
        public int stageNo;
        public int generateIntervalTime;
        public int clearPoint;
        public int defenseBaseDurability;
        public Sprite stageSprite;
        [Header("マップ情報")]
        public MapInfo mapInfo;
        public int maxCharaPlacement;

    }
}
