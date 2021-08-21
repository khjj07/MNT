using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIEndCut : MonoBehaviour
{
    public List<Image> ImageCuts;

    private void Awake()
    {

    }

    public void StartCut()
    {
        ImageCuts[1].GetComponent<RectTransform>()
        .DOAnchorPosX(-300f, 0.4f).From(true).SetDelay(0.5f);


        ImageCuts[1].GetComponent<RectTransform>()
        .DOAnchorPosX(-300f, 0.4f).From(true).SetDelay(0.5f);




    }
}
