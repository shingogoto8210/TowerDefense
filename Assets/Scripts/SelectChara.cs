using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChara : MonoBehaviour
{
    [SerializeField] private int charaNum;
    public GameMaster gameMaster;
    [SerializeField] private CharaGenerator charaGenerator;

    public void OnClick()
    {
        gameMaster.charaNum = charaNum;
        charaGenerator.CreateChara(charaGenerator.gridPos);

    }


}
