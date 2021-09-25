using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStageButton : MonoBehaviour
{
    [SerializeField]
    private int stageNo;
    [SerializeField]
    private Text btnName;

    public void OnClickSelectStageNo ()
    {
        GameData.instance.stageNo = this.stageNo;
        SceneStateManager.instance.PreparateNextScene(SceneType.Battle);
    }

    public void SetUpButton(StageDataSO.StageData stageData)
    {
        stageNo = stageData.stageNo;
        btnName.text = stageData.stageName;
    }
}
