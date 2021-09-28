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
    [SerializeField]
    private LogoEffect openingLogoPrefab;
    public LogoEffect openingLogo;
    [SerializeField]
    private LogoEffect clearLogoPrefab;
    public LogoEffect clearLogo;
    [SerializeField]
    private LogoEffect gameoverLogoPrefab;
    public LogoEffect gameoverLogo;
    [SerializeField]
    private ReturnSelectCharaPopUp returnSelectCharaPopUpPrefab;
    

    /// <summary>
    /// カレンシーの表示更新
    /// </summary>
    public void UpdateDisplayCurrency()
    {
        txtCurrency.text = GameData.instance.currency.ToString();
    }

    //オープニングメソッド Coroutine

    public IEnumerator CreateOpeningLogo()
    {
        openingLogo = Instantiate(openingLogoPrefab, canvasTran, false);
        yield return null;
    }

    public IEnumerator CreateClearLogo()
    {
        clearLogo = Instantiate(clearLogoPrefab, canvasTran, false);
        yield return null;
    }

    public IEnumerator CreateGameOverLogo()
    {
        gameoverLogo = Instantiate(gameoverLogoPrefab, canvasTran, false);
        yield return null;
    }

    public void CreateReturnCharaPopUp(CharaController charaController, GameManager gameManager)
    {
        ReturnSelectCharaPopUp returnSelectCharaPopUp = Instantiate(returnSelectCharaPopUpPrefab, canvasTran, false);
        returnSelectCharaPopUp.SetUpReturnSelectCharaPopUp(charaController, gameManager);
    }

}
