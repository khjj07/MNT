using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class UITurn : MonoBehaviour
{
    private TurnManager TurnManager;
    private UIManager UIManager;

    private int remainTurn;
    private int limitTurn;

    public TextMeshProUGUI TextRemainTurn;

    public List<UITurnIcon> turnIcons = new List<UITurnIcon>();

    // Use this for initialization
    void Start()
    {
        TurnManager = TurnManager.instance;
        UIManager = UIManager.instance;

        remainTurn = 0;
        limitTurn = TurnManager.TurnLimit;

        TurnManager.TurnChangeEvent.AddListener(() => TurnChange());
        SetTurnIcon();
    }


    void SetTurnIcon()
    {
        Debug.Log(" Set ");
        turnIcons = GetComponentsInChildren<UITurnIcon>().ToList();

        for (int i = 0; i < turnIcons.Count; i++)
        {
            if (i < TurnManager.player_list.Count)
            {
                turnIcons[i].SetPlayer(TurnManager.player_list[i].GetComponent<Player>());
            }
            else
            {
                turnIcons[i].SetPlayer(null);
            }
        }
    }


    void TurnChange()
    {
        remainTurn = TurnManager.RemainTurn;
        var text = remainTurn.ToString() + "/" + limitTurn;
        TextRemainTurn.text = text;
    }

    void TurnIcon()
    {


    }


}