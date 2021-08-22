using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UICutScene : MonoBehaviour
{
    public bool isDirection = false;
    private bool isDirectionEnd = false;
    public bool isFirst = true;
    public List<Image> ImageCuts;
    private int currentCutNum = 0;

    private void Awake()
    {
        if(isDirection&&isFirst)
        {
            SoundManager.Instance.PlayBGM(SoundManager.EBGM._bgm_intro);
        }
    }

    public void StartCut()
    {
        this.gameObject.SetActive(true);
        ImageCuts = GetComponentsInChildren<Image>(true).ToList();
        ImageCuts.RemoveAt(0);
        ImageCuts.ForEach(x => x.gameObject.SetActive(false));
        NextCut();
    }

    public void NextCut()
    {
        if (isDirectionEnd)
            return;


        // ¸¶Áö¸· ÄÆ¾ÀÀ»  º¸°í ³­ ÈÄ
        if (currentCutNum >= ImageCuts.Count)
        {
            isDirectionEnd = true;

            this.GetComponent<CanvasGroup>()
                .DOFade(0f, 0.5f)
                .onComplete += 
                () => this.gameObject.SetActive(false);

            if (isFirst)
            {
                UICharacter.instance.NextStartChat();
                SoundManager.Instance.StopBGM(SoundManager.EBGM._bgm_intro);
                SoundManager.Instance.PlayBGM(SoundManager.EBGM._bgm_ingame);
            }
            else
            {
                SceneManager.LoadScene(0);
            }

            return;
        }

        int num = currentCutNum;

        ImageCuts[num].gameObject.SetActive(true);

        if (num > 0)
            ImageCuts[num].DOFade(1f, 0.5f).From(0f, true)
                .onComplete += () => ImageCuts[num - 1].gameObject.SetActive(false);
        else
            ImageCuts[num].DOFade(1f, 0.5f).From(0f, true);

        currentCutNum++;



    }

}
