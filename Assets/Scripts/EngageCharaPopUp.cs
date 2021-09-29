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
    /// ポップアップの設定
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

        //スクリプタブル・オブジェクトに登録されているキャラ分（引数で受け取った情報）を利用して
        for (int i = 0; i < haveCharaDataList.Count; i++)
        {
            //ボタンのゲームオブジェクトを生成
            SelectCharaDetail selectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);
            //ボタンのゲームオブジェクトの設定（CharaDataを設定する）
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
    /// 各ボタンのアクティブ状態の切り替え
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
    /// 契約コストが支払えるかどうかを書くSelectCharaDetailで確認してボタン押下機能を切り替え
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
