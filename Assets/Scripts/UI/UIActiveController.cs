using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class UIActiveController : MonoBehaviour
{
    private RectTransform rect;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0.8f, 0.8f);
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void OnUI()
    {
        float onTime = 0.2f;
        this.gameObject.SetActive(true);
        //ImageCredit.DOFade(1f, onTime);
        canvasGroup.DOFade(1f, onTime);
        rect.DOSizeDelta(new Vector2(1f, 1f), onTime, false);

    }

    public void OffUI()
    {
        float offTime = 0.2f;

        //ImageCredit.DOFade(0f, offTime);
        canvasGroup.DOFade(0f, offTime);
        rect.DOSizeDelta(new Vector2(0.8f, 0.8f), offTime, false);
        Invoke(nameof(Off), offTime);
    }

    private void Off()
    {
        this.gameObject.SetActive(false);
    }

}
