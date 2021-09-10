using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReturnSelectCharaPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnSubmitReturnChara;
    [SerializeField]
    private Button btnClosePopUp;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private CharaController charaController;
    private GameManager gameManager;

    /// <summary>
    /// �ݒ�
    /// </summary>
    /// <param name="charaController"></param>
    /// <param name="gameManager"></param>
    public void SetUpReturnSelectCharaPopUp(CharaController charaController,GameManager gameManager)
    {
        this.charaController = charaController;
        this.gameManager = gameManager;
        btnSubmitReturnChara.interactable = false;
        btnClosePopUp.interactable = false;
        btnSubmitReturnChara.onClick.AddListener(OnClickSubmitReturnChara);
        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);
        btnSubmitReturnChara.interactable = true;
        btnClosePopUp.interactable = true;
    }

    /// <summary>
    /// �z�u��������I����
    /// </summary>
    private void OnClickSubmitReturnChara()
    {
        CloseReturnCharaPopUp(true);
    }

    /// <summary>
    /// �z�u�������Ȃ��I����
    /// </summary>
    private void OnClickClosePopUp()
    {
        CloseReturnCharaPopUp(false);
    }

    private void CloseReturnCharaPopUp(bool isReturnChara)
    {
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                gameManager.JudgeReturnChara(isReturnChara, charaController);
                DestroyReturnSelectCharaPopUp();
            });
    }

    /// <summary>
    /// �|�b�v�A�b�v��j��
    /// </summary>
    private void DestroyReturnSelectCharaPopUp()
    {
        Destroy(gameObject);
    }

}
