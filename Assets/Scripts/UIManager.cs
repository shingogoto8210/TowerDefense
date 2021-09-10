using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtCurrency;
    [SerializeField]
    private Transform canvasTran;
    //[SerializeField]
    //private LogoEffect openingLogoEffect;
    [SerializeField]
    private ReturnSelectCharaPopUp returnSelectCharaPopUpPrefab;
    

    /// <summary>
    /// �J�����V�[�̕\���X�V
    /// </summary>
    public void UpdateDisplayCurrency()
    {
        txtCurrency.text = GameData.instance.currency.ToString();
    }

    //�I�[�v�j���O���\�b�h Coroutine

    public void CreateReturnCharaPopUp(CharaController charaController, GameManager gameManager)
    {
        ReturnSelectCharaPopUp returnSelectCharaPopUp = Instantiate(returnSelectCharaPopUpPrefab, canvasTran, false);
        returnSelectCharaPopUp.SetUpReturnSelectCharaPopUp(charaController, gameManager);
    }

}
