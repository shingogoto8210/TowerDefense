using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{
    [SerializeField] private GameObject charaPrefab;
    public int charaNum;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemaps;
    public Vector3Int gridPos;
    [SerializeField] private int maxCharaCount;
    private int charaCount;
    //public GameObject selectPanel;
    //public bool isSelect;
    [SerializeField]
    private PlacementCharaSelectPopUp placementCharaSelectPopUpPrefab;
    [SerializeField]
    private Transform canvasTran;
    private PlacementCharaSelectPopUp placementCharaSelectPopUp;
    private GameManager gameManager;
 
    void Update()
    {
        //selectPanelÇ™falseÇÃÇ∆Ç´gridPosÇéÊìæÇ≈Ç´ÇÈ
        if (Input.GetMouseButtonDown(0) && charaCount <= maxCharaCount && !placementCharaSelectPopUp.gameObject.activeSelf) 
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //gridPosÇÃColliderTypeÇ™NoneÇÃÇ∆Ç´selectPanelÇï\é¶
            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                //selectPanel.SetActive(true);
                //isSelect = true;
                ActivatePlacementCharaSelectPopUp();
            }
        }
    }

    /// <summary>
    /// ÉLÉÉÉâê∂ê¨
    /// </summary>
    /// <param name="gridPos"></param>
    public void CreateChara(/*Vector3Int gridPos*/)
    {
        GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);
        charaCount++;
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        //selectPanel.SetActive(false);
        //isSelect = false;
    }

    /// <summary>
    /// ê›íË
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator SetUpCharaGenerator(GameManager gameManager)
    {
        this.gameManager = gameManager;
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    }

    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this);
        placementCharaSelectPopUp.gameObject.SetActive(false);
        yield return null;
    }
    private void ActivatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp.gameObject.SetActive(true);
        placementCharaSelectPopUp.ShowPopUp();
    }

    public void InactivatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp.gameObject.SetActive(false);
    }
}
