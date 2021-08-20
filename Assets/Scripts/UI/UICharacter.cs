using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using UniRx.Triggers;

public class UICharacter : MonoBehaviour
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

        this.UpdateAsObservable()
            .Where(_ => TurnManager.RemainTurn != remainTurn)
            .Subscribe(_ => PortraitChange())
            .AddTo(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PortraitChange()
    {
        
    }

}