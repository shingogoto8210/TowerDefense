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

    /// <summary>
    /// �|�b�v�A�b�v�̐ݒ�
    /// </summary>
    /// <param name="charaGenerator"></param>
    public void SetUpPlacementCharaSelectPopUp(CharaGenerator charaGenerator)
    {
        this.charaGenerator = charaGenerator;
        canvasGroup.alpha = 0;
        ShowPopUp();
        SwithcActivateButtons(false);
        btnChooseChara.onClick.AddListener(OnClickSubmitChooseChara);
        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);
        SwithcActivateButtons(true);
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
}
