using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class UIObjectControllerX : MonoBehaviour
{
    public List<AnimationTarget> targets = new List<AnimationTarget>();

    [System.Serializable]
    public struct AnimationTarget
    {
        public GameObject go;
        public enum EAnimationType
        { SlideAndFadeIn = 0 }


        //public bool isUpAnimation;
        public float moveDistance;
        public float duration;
        public Ease ease;
        public float delay;

        public void SlideAndFadeIn()
        {
            CanvasGroup canvas;

            if (go.GetComponent<CanvasGroup>())
                canvas = go.GetComponent<CanvasGroup>();
            else
                canvas = go.AddComponent<CanvasGroup>();

            go.transform
                .DOMoveX(moveDistance, duration)
                .SetEase(ease)
                .From(true)
                .SetRelative(true)
                .SetDelay(delay);

            canvas
                .DOFade(1f, duration)
                .From(0f)
                .SetDelay(delay);
        }

    }

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        targets.ForEach(x =>
        {
            x.SlideAndFadeIn();
        });
        SoundManager.Instance.StopBGM(SoundManager.EBGM._bgm_ingame);
        SoundManager.Instance.PlayBGM(SoundManager.EBGM._bgm_outro);
    }

}
