using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Coffee.UIExtensions;

public class LogoEffect : InfoManager
{
    [SerializeField]
    private Image img;

    [SerializeField]
    private ShinyEffectForUGUI shinyEffect;


    public IEnumerator PlayOpening()
    {
        canvasGroup.alpha = 0.0f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1.0f, 0.5f));
        sequence.Append(img.DOFade(1.0f, 0.5f).OnComplete(() => shinyEffect.Play(1.0f)));
        sequence.AppendInterval(1.0f);
        sequence.Append(canvasGroup.DOFade(0.0f, 0.5f)).OnComplete(() => Destroy(gameObject));
        yield return new WaitForSeconds(3.0f);
    }

    public IEnumerator PlayClear()
    {
        canvasGroup.alpha = 0.0f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1.0f, 0.5f));
        sequence.Append(img.DOFade(1.0f, 0.5f).OnComplete(() => shinyEffect.Play(1.0f)));
        sequence.AppendInterval(1.0f);
        sequence.Append(canvasGroup.DOFade(0.0f, 0.5f)).OnComplete(() => Destroy(gameObject));
        yield return new WaitForSeconds(3.0f);
    }
    public IEnumerator PlayGameOver()
    {
        canvasGroup.alpha = 0.0f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1.0f, 0.5f));
        sequence.Append(img.DOFade(1.0f, 0.5f).OnComplete(() => shinyEffect.Play(1.0f)));
        sequence.AppendInterval(1.0f);
        sequence.Append(canvasGroup.DOFade(0.0f, 0.5f)).OnComplete(() => Destroy(gameObject));
        yield return new WaitForSeconds(3.0f);
    }
}
