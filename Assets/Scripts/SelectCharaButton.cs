using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharaButton : MonoBehaviour
{
    [SerializeField] private int charaNum;
    [SerializeField] private CharaGenerator charaGenerator;

    public void OnClick()
    {
        charaGenerator.charaNum = charaNum;
        if (charaGenerator.selectPanel.activeSelf == true)
        {
            charaGenerator.CreateChara(charaGenerator.gridPos);
        }
    }


}
