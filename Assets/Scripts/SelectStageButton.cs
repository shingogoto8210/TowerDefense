using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStageButton : MonoBehaviour
{
    [SerializeField]
    private int stageNo;

    public void OnClickSelectStageNo ()
    {
        GameData.instance.stageNo = this.stageNo;
        SceneManager.LoadScene("Main");
    }
}
