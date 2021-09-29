using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageButton : MonoBehaviour
{
    [SerializeField]
    private EngageCharaPopUp charaPopUpPrefab;
    [SerializeField]
    private Transform canvasTran;
    private EngageCharaPopUp charaPopUp;

    public void OnClick()
    {
        if (charaPopUp == null)
        {
            charaPopUp = Instantiate(charaPopUpPrefab, canvasTran, false);
            charaPopUp.SetUpEngageCharaSelectPopUp(this, DataBaseManager.instance.charaDataSO.charaDataList);
        }
        else
        {
            charaPopUp.gameObject.SetActive(true);
            charaPopUp.ShowPopUp();
        }
    }

    public void InactivePopUp()
    {
        charaPopUp.gameObject.SetActive(false);
    }
}

