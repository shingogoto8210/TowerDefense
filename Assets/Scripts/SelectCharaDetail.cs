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
    /// SelectCharaDetail�̐ݒ�
    /// </summary>
    /// <param name="placementCharaSelectPopUp"></param>
    /// <param name="charaData"></param>
    public void SetUpSelectCharaDetail(PlacementCharaSelectPopUp placementCharaSelectPopUp, CharaData charaData)
    {
        this.placementCharaSelectPopUp = placementCharaSelectPopUp;
        this.charaData = charaData;
        imgChara.sprite = this.charaData.charaSprite;
        //�{�^���Ƀ��\�b�h��o�^
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);
    }

    private void OnClickSelectCharaDetail()
    {
        //TODO�@�A�j�����o
        //�^�b�v����SelectCharaDetail�̏����|�b�v�A�b�v�ɑ���
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
        //�{�^���Ƀ��\�b�h��o�^
        btnSelectCharaDetail.onClick.AddListener(OnClickEngageSelectCharaDetail);
    }

    private void OnClickEngageSelectCharaDetail()
    {
        //TODO�@�A�j�����o
        //�^�b�v����SelectCharaDetail�̏����|�b�v�A�b�v�ɑ���
        engageCharaPopUp.SetSelectCharaDetail(charaData);
    }
}
