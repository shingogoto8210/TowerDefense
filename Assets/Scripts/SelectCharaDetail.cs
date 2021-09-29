using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectCharaDetail : MonoBehaviour
{
    [SerializeField]
    private Button btnSelectCharaDetail;
    [SerializeField]
    private Image imgChara;
    private PlacementCharaSelectPopUp placementCharaSelectPopUp;
    private CharaData charaData;
    private EngageCharaPopUp engageCharaPopUp;

    /// <summary>
    /// SelectCharaDetailの設定
    /// </summary>
    /// <param name="placementCharaSelectPopUp"></param>
    /// <param name="charaData"></param>
    public void SetUpSelectCharaDetail(PlacementCharaSelectPopUp placementCharaSelectPopUp, CharaData charaData)
    {
        this.placementCharaSelectPopUp = placementCharaSelectPopUp;
        this.charaData = charaData;
        imgChara.sprite = this.charaData.charaSprite;
        //ボタンにメソッドを登録
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);
    }

    private void OnClickSelectCharaDetail()
    {
        //TODO　アニメ演出
        //タップしたSelectCharaDetailの情報をポップアップに送る
        placementCharaSelectPopUp.SetSelectCharaDetail(charaData);
    }

    public bool JudgePermissionCost(int currency)
    {
        return charaData.cost <= currency ? true : false;
        //if (charaData.cost <= currency)
        //{
            //return true;
        //}
            //return false;
    }

    public bool JudgePermissionEngagePoint(int totalClearPoint)
    {
        return charaData.engagePoint <= totalClearPoint ? true : false;
    }

    public void ChangeActiveButton(bool isButtonActive)
    {
            btnSelectCharaDetail.interactable = isButtonActive;
    }

    public void SetUpSelectCharaDetail(EngageCharaPopUp EngageCharaPopUp, CharaData charaData)
    {
        this.engageCharaPopUp = EngageCharaPopUp;
        this.charaData = charaData;
        imgChara.sprite = this.charaData.charaSprite;
        //ボタンにメソッドを登録
        btnSelectCharaDetail.onClick.AddListener(OnClickEngageSelectCharaDetail);
    }

    private void OnClickEngageSelectCharaDetail()
    {
        //TODO　アニメ演出
        //タップしたSelectCharaDetailの情報をポップアップに送る
        engageCharaPopUp.SetSelectCharaDetail(charaData);
    }
}
