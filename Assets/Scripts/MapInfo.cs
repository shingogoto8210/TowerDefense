using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapInfo : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemaps;　//Walk側のTilemapを指定する
    [SerializeField]
    private Grid grid;  //Base側のGridを指定する
    [SerializeField]
    private Transform defenseBaseTran;　　//DefenseBaseを生成する位置

    /// <summary>
    /// 出現するエネミー1体分の情報用クラス
    /// </summary>
    [System.Serializable]
    public class AppearEnemyInfo
    {
        [Header("x = 敵の番号。－1ならランダム")]
        public int enemyNo;

        [Header("敵の出現地点のランダム化。trueならランダム")]
        public bool isRandomPos;

        public PathData enemyPathData;  //移動用経路の情報
    }

    public AppearEnemyInfo[] appearEnemyInfos;  //複数の出現するエネミーの情報を管理するための配列


    /// <summary>
    /// マップの情報を取得
    /// </summary>
    /// <returns></returns>
    public (Tilemap, Grid) GetMapInfoandGrid()
    {
        return (this.tilemaps, this.grid);
    }

    /// <summary>
    /// 防衛拠点の情報を取得
    /// </summary>
    /// <returns></returns>
    public Transform GetDefenseBaseTran()
    {
        return defenseBaseTran;
    }
}
