using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using UniRx.Triggers;

public class UICharacter : MonoBehaviour
{
    private TurnManager TurnManager;
    private UIManager UIManager;

    public Image Portrait;

    public List<UITurnIcon> turnIcons = new List<UITurnIcon>();

    // Use this for initialization
    void Start()
    {
        TurnManager = TurnManager.instance;
        TurnManager.TurnChangeEvent.AddListener(() => PortraitChange());

        UIManager = UIManager.instance;
    }

    public void PortraitChange()
    {
        Player player = TurnManager.current_player;
        Portrait.sprite = UIManager.Sprites
            .Find(x => x.name.Split('_')[1].Equals(player.type.ToString()));
    }

    public void PortraitChange(Player player)
    {
        Portrait.sprite = UIManager.Sprites
            .Find(x => x.name.Split('_')[1].Equals(player.type.ToString()));
    }

}