using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CharaGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] charaPrefabs;
    public int charaNum;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemaps;
    public Vector3Int gridPos;
    [SerializeField] private int maxCharaCount;
    private int charaCount;
    [SerializeField] private GameObject selectPanel;
    public bool isSelect;
    

    private void Start()
    {
        selectPanel.SetActive(false);
        isSelect = false;
    }
    void Update()
    {
        //isSelect‚ªtrue‚Ì‚Æ‚«‚ÍgridPos‚ðŽæ“¾‚Å‚«‚È‚¢
        if (Input.GetMouseButtonDown(0) && charaCount <= maxCharaCount && isSelect ==false) 
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None && isSelect == false)
            {
                selectPanel.SetActive(true);
                isSelect = true;
            }
        }
    }

    public void CreateChara(Vector3Int gridPos)
    {
        GameObject chara = Instantiate(charaPrefabs[charaNum], gridPos, Quaternion.identity);
        charaCount++;
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        selectPanel.SetActive(false);
        isSelect = false;
    }
}
