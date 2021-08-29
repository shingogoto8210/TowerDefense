using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlacementCharaSelectPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnClosePopUp;
    [SerializeField]
    private Button btnChooseChara;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private CharaGenerator charaGenerator;
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
    private SelectCharaDetail selectCharaDetailPrefab;
    [SerializeField]
    private Transform selectCharaDetailTran;
    [SerializeField]
    private List<SelectCharaDetail> selectCharaDetailsList = new List<SelectCharaDetail>();
    private CharaData chooseCharaData;

    /// <summary>
    /// �|�b�v�A�b�v�̐ݒ�
    /// </summary>
    /// <param name="charaGenerator"></param>
    public void SetUpPlacementCharaSelectPopUp(CharaGenerator charaGenerator,List<CharaData> haveCharaDataList)
    {
        this.charaGenerator = charaGenerator;
        canvasGroup.alpha = 0;
        ShowPopUp();
        SwithcActivateButtons(false);
        btnChooseChara.onClick.AddListener(OnClickSubmitChooseChara);
        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);
        SwithcActivateButtons(true);

        //�X�N���v�^�u���E�I�u�W�F�N�g�ɓo�^����Ă���L�������i�����Ŏ󂯎�������j�𗘗p����
        for(int i = 0; i < haveCharaDataList.Count; i++)
        {
            //�{�^���̃Q�[���I�u�W�F�N�g�𐶐�
            SelectCharaDetail selectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);
            //�{�^���̃Q�[���I�u�W�F�N�g�̐ݒ�iCharaData��ݒ肷��j
            selectCharaDetail.SetUpSelectCharaDetail(this, haveCharaDataList[i]);
            selectCharaDetailsList.Add(selectCharaDetail);
            if(i == 0)
            {
                SetSelectCharaDetail(haveCharaDataList[i]);
            }
        }
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
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    private void OnClickSubmitChooseChara()
    {
        charaGenerator.CreateChooseChara(chooseCharaData);
        HidePopUp();
    }

    private void OnClickClosePopUp()
    {
        HidePopUp();
    }

    private void HidePopUp()
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => charaGenerator.InactivatePlacementCharaSelectPopUp());
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
    }
}
