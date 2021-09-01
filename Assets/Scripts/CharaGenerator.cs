using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{
    //[SerializeField] private GameObject charaPrefab;
    [SerializeField]
    private CharaController charaControllerPrefab;
    [SerializeField] 
    private Grid grid;
    [SerializeField]
    private Tilemap tilemaps;
    private Vector3Int gridPos;
    [SerializeField]
    private int maxCharaCount;
    private int charaCount;
    //public GameObject selectPanel;
    //public bool isSelect;
    [SerializeField]
    private PlacementCharaSelectPopUp placementCharaSelectPopUpPrefab;
    [SerializeField]
    private Transform canvasTran;
    private PlacementCharaSelectPopUp placementCharaSelectPopUp;
    private GameManager gameManager;
    [SerializeField]
    private List<CharaData> charaDatasList = new List<CharaData>();
 
    void Update()
    {
        //selectPanel��false�̂Ƃ�gridPos���擾�ł���
        if (Input.GetMouseButtonDown(0) && charaCount <= maxCharaCount && !placementCharaSelectPopUp.gameObject.activeSelf && gameManager.currentGameState == GameManager.GameState.Play) 
        {
            //�}�E�X�N���b�N�̈ʒu���擾���ă��[���h���W�ɕϊ����C���������Ƀ^�C���̃Z�����W�ɕϊ�
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //gridPos��ColliderType��None�̂Ƃ�selectPanel��\��
            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                //selectPanel.SetActive(true);
                //isSelect = true;
                ActivatePlacementCharaSelectPopUp();
            }
        }
    }

    /// <summary>
    /// �L��������
    /// </summary>
    /// <param name="gridPos"></param>
    //public void CreateChara()
    //{
        //GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);
        //charaCount++;
        //chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        //selectPanel.SetActive(false);
        //isSelect = false;
    //}

    /// <summary>
    /// �ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator SetUpCharaGenerator(GameManager gameManager)
    {
        this.gameManager = gameManager;
        CreateHaveCharaDatasList();
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    }

    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this, charaDatasList);
        placementCharaSelectPopUp.gameObject.SetActive(false);
        yield return null;
    }
    private void ActivatePlacementCharaSelectPopUp()
    {
        gameManager.SetGameState(GameManager.GameState.Stop);
        gameManager.PauseEnemies();
        placementCharaSelectPopUp.gameObject.SetActive(true);
        placementCharaSelectPopUp.ShowPopUp();
    }

    public void InactivatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp.gameObject.SetActive(false);
        gameManager.SetGameState(GameManager.GameState.Play);
        gameManager.ResumeEnemies();
    }

    private void CreateHaveCharaDatasList()
    {
        for(int i = 0; i < DataBaseManager.instance.charaDataSO.charaDataList.Count; i++)
        {
            charaDatasList.Add(DataBaseManager.instance.charaDataSO.charaDataList[i]);
        }
    }

    /// <summary>
    /// �I�������L�����𐶐����Ĕz�u
    /// </summary>
    /// <param name="charaData"></param>
    public void CreateChooseChara(CharaData charaData)
    {
        CharaController chara = Instantiate(charaControllerPrefab, gridPos, Quaternion.identity);
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        charaCount++;
        chara.SetUpChara(charaData,gameManager);
        Debug.Log(charaData.charaName);
    }
}
