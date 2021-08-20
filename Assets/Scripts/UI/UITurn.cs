using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class UITurn : MonoBehaviour
{
    private TurnManager TurnManager;

    private int remainTurn;
    private int limitTurn;

    public TextMeshProUGUI TextRemainTurn;

    public List<Sprite> sprites;
    public List<UITurnIcon> turnIcons = new List<UITurnIcon>();

    // Use this for initialization
    void Start()
    {
        TurnManager = TurnManager.instance;

        remainTurn = 0;
        limitTurn = TurnManager.TurnLimit;


        this.UpdateAsObservable()
            .Where(_ => TurnManager.RemainTurn != remainTurn)
            .Subscribe(_ => TurnChange())
            .AddTo(gameObject);
    }

    [ContextMenu("GetImage")]
    void GetImage()
    {
        Debug.Log("Get Image");
        sprites = Resources.LoadAll<Sprite>("/Sprite/UI").ToList();
    }


    void TurnChange()
    {
        remainTurn = TurnManager.RemainTurn;
        var text = remainTurn.ToString() + "/" + limitTurn;
        TextRemainTurn.text = text;




    }


}