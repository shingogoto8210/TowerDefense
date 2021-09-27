using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Coffee.UIExtensions;

public class LogoEffect : InfoManager
{
    [SerializeField]
    private Image imgStart;
    [SerializeField]
    private Image imgClear;
    [SerializeField]
    private ShinyEffectForUGUI shinyEffectOpening;
    [SerializeField]
    private ShinyEffectForUGUI shinyEffectClear;

    public IEnumerator PlayOpening()
    {
        SetUpImgStart();
        canvasGroup.alpha = 0.0f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1.0f, 0.5f));
        sequence.Append(imgStart.DOFade(1.0f, 0.5f).OnComplete(() => shinyEffectOpening.Play(1.0f)));
        sequence.AppendInterval(1.0f);
        sequence.Append(canvasGroup.DOFade(0.0f, 0.5f));//.OnComplete(() => Destroy(gameObject));
        yield return new WaitForSeconds(3.0f);
    }

    public IEnumerator PlayClear()
    {
        SetUpImgClear();
        canvasGroup.alpha = 0.0f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1.0f, 0.5f));
        sequence.Append(imgClear.DOFade(1.0f, 0.5f).OnComplete(() => shinyEffectClear.Play(1.0f)));
        sequence.AppendInterval(1.0f);
        sequence.Append(canvasGroup.DOFade(0.0f, 0.5f)).OnComplete(() => Destroy(gameObject));
        yield return new WaitForSeconds(3.0f);
    }

    private void SetUpImgStart()
    {
        imgStart.enabled = true;
        imgClear.enabled = false;
    }

    private void SetUpImgClear()
    {
        imgStart.enabled = false;
        imgClear.enabled = true;
    }

}
