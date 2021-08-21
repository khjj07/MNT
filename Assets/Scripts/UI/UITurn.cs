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

        SetTurnIcon();
        TurnManager.TurnChangeEvent.AddListener(() => TurnChange());
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
                Debug.Log("Currnet : " + i);
                turnIcons[i].gameObject.SetActive(false);
                turnIcons.RemoveAt(i);
                i--;
            }
        }
    }


    void TurnChange()
    {
        remainTurn = TurnManager.RemainTurn;
        var text = remainTurn.ToString() + "/" + limitTurn;
        TextRemainTurn.text = text;
    }

    void TurnIconSort()
    {
        float directionTime = 0.4f;

        for (int i = 0; i < turnIcons.Count; i++)
        {
            if (turnIcons[i].IsPlayerDie())
            {
                turnIcons[i].DieDirection(directionTime);
                turnIcons.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < turnIcons.Count; i++)
        {
            var rect = turnIcons[i].GetComponent<RectTransform>();

            rect.DOAnchorPosX(i * rect.sizeDelta.x, directionTime)
                .SetEase(Ease.OutExpo)
                .SetAutoKill(false);
        }

    }


}