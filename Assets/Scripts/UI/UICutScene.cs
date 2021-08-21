using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UICutScene : MonoBehaviour
{
    public bool isFirst = true;
    public List<Image> ImageCuts;
    private int currentCutNum = 0;

    private void Awake()
    {
        ImageCuts = GetComponentsInChildren<Image>(true).ToList();
        ImageCuts.RemoveAt(0);
        ImageCuts.ForEach(x => x.gameObject.SetActive(false));

        if (isFirst)
            NextScene();
    }

    public void NextScene()
    {
        // ¸¶Áö¸· ÄÆ¾ÀÀ»  º¸°í ³­ ÈÄ
        if (currentCutNum >= ImageCuts.Count)
        {
            this.GetComponent<CanvasGroup>()
                .DOFade(0f, 0.5f)
                .onComplete += 
                () => this.gameObject.SetActive(false);

            if (isFirst)
            {
                UICharacter.instance.NextStartChat();
            }
            else
            {

            }

            return;
        }

        ImageCuts[currentCutNum].gameObject.SetActive(true);
        ImageCuts[currentCutNum].DOFade(1f, 0.5f).From(0f, true);
        currentCutNum++;
    }

}
