using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CharaGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] charaPrefabs;    
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemaps;
    public Vector3Int gridPos;
    [SerializeField] private int maxCharaCount;
    private int charaCount;

    [SerializeField] private GameObject selectPanel;
    [SerializeField] private GameMaster gameMaster;

    private void Start()
    {
        selectPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && charaCount <= maxCharaCount) 
        {
            Debug.Log(tilemaps.GetColliderType(gridPos));
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                selectPanel.SetActive(true);
            }
        }
    }

    public void CreateChara(Vector3Int gridPos)
    {
        GameObject chara = Instantiate(charaPrefabs[gameMaster.charaNum], gridPos, Quaternion.identity);
        charaCount++;
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        selectPanel.SetActive(false);

    }
}
