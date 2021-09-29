using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EngageCharaPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnClosePopUp;
    [SerializeField]
    private Button btnChooseChara;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Image imgPickupChara;
    [SerializeField]
    private Text txtPickupCharaName;
    [SerializeField]
    private Text txtPickupCharaAttackPower;
    [SerializeField]
    private Text txtPickupCharaAttackRangeType;
    [SerializeField]
    private Text txtPickupCharaCost;
    [SerializeField]
    private Text txtPickupCharaMaxAttackCount;
    [SerializeField]
    private Text txtPickupCharaEngagePoint;
    [SerializeField]
    private SelectCharaDetail selectCharaDetailPrefab;
    [SerializeField]
    private Transform selectCharaDetailTran;
    [SerializeField]
    private List<SelectCharaDetail> selectCharaDetailsList = new List<SelectCharaDetail>();
    private CharaData chooseCharaData;
    private EngageButton engageButton;

    /// <summary>
    /// �|�b�v�A�b�v�̐ݒ�
    /// </summary>
    /// <param name="charaGenerator"></param>
    public void SetUpEngageCharaSelectPopUp(EngageButton engageButton ,List<CharaData> haveCharaDataList)
    {
        this.engageButton = engageButton;
        canvasGroup.alpha = 0;
        ShowPopUp();
        SwithcActivateButtons(false);
        btnChooseChara.onClick.AddListener(OnClickSubmitChooseChara);
        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);
        SwithcActivateButtons(true);

        //�X�N���v�^�u���E�I�u�W�F�N�g�ɓo�^����Ă���L�������i�����Ŏ󂯎�������j�𗘗p����
        for (int i = 0; i < haveCharaDataList.Count; i++)
        {
            //�{�^���̃Q�[���I�u�W�F�N�g�𐶐�
            SelectCharaDetail selectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);
            //�{�^���̃Q�[���I�u�W�F�N�g�̐ݒ�iCharaData��ݒ肷��j
            selectCharaDetail.SetUpSelectCharaDetail(this, haveCharaDataList[i]);
            selectCharaDetailsList.Add(selectCharaDetail);
            //if (i == 0)
            //{
              //  SetSelectCharaDetail(haveCharaDataList[i]);
            //}
        }
        CheckAllCharaButtons();
    }

    /// <summary>
    /// �e�{�^���̃A�N�e�B�u��Ԃ̐؂�ւ�
    /// </summary>
    /// <param name="isSwithc"></param>
    public void SwithcActivateButtons(bool isSwithc)
    {
        btnChooseChara.interactable = isSwithc;
        btnClosePopUp.interactable = isSwithc;
    }

    public void ShowPopUp()
    {
        CheckAllCharaButtons();
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    private void OnClickSubmitChooseChara()
    {
        if (chooseCharaData.engagePoint > GameData.instance.totalClearPoint)
        {
            return;
        }
        EngageChara();
        //charaGenerator.CreateChooseChara(chooseCharaData);
        HidePopUp();
    }

    private void OnClickClosePopUp()
    {
        HidePopUp();
    }

    private void HidePopUp()
    {
        CheckAllCharaButtons();
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => engageButton.InactivePopUp());
    }

    public void SetSelectCharaDetail(CharaData charaData)
    {
        chooseCharaData = charaData;
        imgPickupChara.sprite = charaData.charaSprite;
        txtPickupCharaName.text = charaData.charaName;
        txtPickupCharaAttackPower.text = charaData.attackPower.ToString();
        txtPickupCharaAttackRangeType.text = charaData.attackRange.ToString();
        txtPickupCharaCost.text = charaData.cost.ToString();
        txtPickupCharaMaxAttackCount.text = charaData.maxAttackCount.ToString();
        txtPickupCharaEngagePoint.text = charaData.engagePoint.ToString();
    }


    /// <summary>
    /// �_��R�X�g���x�����邩�ǂ���������SelectCharaDetail�Ŋm�F���ă{�^�������@�\��؂�ւ�
    /// </summary>
    private void CheckAllCharaButtons()
    {
        if (selectCharaDetailsList.Count > 0)
        {
            for (int i = 0; i < selectCharaDetailsList.Count; i++)
            {
                if (GameData.instance.engageCharaNosList.Contains(DataBaseManager.instance.charaDataSO.charaDataList[i].charaNo))
                {
                    selectCharaDetailsList[i].ChangeActiveButton(false);
                    continue;
                }
                //selectCharaDetailsList[i].ChangeActiveButton(selectCharaDetailsList[i].JudgePermissionCost(GameData.instance.currency));
                selectCharaDetailsList[i].ChangeActiveButton(selectCharaDetailsList[i].JudgePermissionEngagePoint(GameData.instance.totalClearPoint));
            }
        }
    }

    private void EngageChara()
    {
        GameData.instance.totalClearPoint -= chooseCharaData.engagePoint;
        GameData.instance.engageCharaNosList.Add(chooseCharaData.charaNo);
    }
}
