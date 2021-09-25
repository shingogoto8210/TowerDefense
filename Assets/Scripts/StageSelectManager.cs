using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField]
    private SelectStageButton btnPrefab;
    public List<Transform> btnsTranList = new List<Transform>();
    public List<SelectStageButton> selectStageButtonsList = new List<SelectStageButton>();
   
    void Start()
    {
        for(int i = 0; i < DataBaseManager.instance.stageDataSO.stageDatasList.Count; i++)
        {
            SelectStageButton btn = Instantiate(btnPrefab, btnsTranList[i], false);
            btn.SetUpButton(DataBaseManager.instance.stageDataSO.stageDatasList[i]);
            selectStageButtonsList.Add(btn);
        }
        CheckClear();
    }

    private void CheckClear()
    {
        for(int i = 0; i < DataBaseManager.instance.stageDataSO.stageDatasList.Count; i++)
        {
            if (GameData.instance.clearStageNosList.Contains(i))
            {
                selectStageButtonsList[i].gameObject.SetActive(true);
            }
            else
            {
                selectStageButtonsList[i].gameObject.SetActive(false);
            }

        }
    }
}
