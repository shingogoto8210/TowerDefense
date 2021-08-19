using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] charaPrefabs;
    public int charaNum;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemaps;
    public Vector3Int gridPos;
    [SerializeField] private int maxCharaCount;
    private int charaCount;
    public GameObject selectPanel;
    public bool isSelect;
 
    void Update()
    {
        //selectPanelがfalseのときgridPosを取得できる
        if (Input.GetMouseButtonDown(0) && charaCount <= maxCharaCount && selectPanel.activeSelf == false) 
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //gridPosのColliderTypeがNoneのときselectPanelを表示
            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None && selectPanel.activeSelf == false)
            {
                selectPanel.SetActive(true);
                //isSelect = true;
            }
        }
    }

    /// <summary>
    /// キャラ生成
    /// </summary>
    /// <param name="gridPos"></param>
    public void CreateChara(/*Vector3Int gridPos*/)
    {
        GameObject chara = Instantiate(charaPrefabs[charaNum], gridPos, Quaternion.identity);
        charaCount++;
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        selectPanel.SetActive(false);
        //isSelect = false;
    }
}
