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

    /// <summary>
    /// SelectCharaDetailの設定
    /// </summary>
    /// <param name="placementCharaSelectPopUp"></param>
    /// <param name="charaData"></param>
    public void SetUpSelectCharaDetail(PlacementCharaSelectPopUp placementCharaSelectPopUp, CharaData charaData)
    {
        this.placementCharaSelectPopUp = placementCharaSelectPopUp;
        this.charaData = charaData;

        //TODO ボタンを押せない状態に切り替える

        imgChara.sprite = this.charaData.charaSprite;

        //TODOカレンシーの値が更新される度にコストが支払えるか確認する

        //ボタンにメソッドを登録
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);

        //TODO　コストに応じてボタンを押せるかどうかを切り替える
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

    public void ChangeActiveButton(bool isButtonActive)
    {
            btnSelectCharaDetail.interactable = isButtonActive;
    }
}
