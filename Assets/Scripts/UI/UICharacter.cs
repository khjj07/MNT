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
    public Image ImageTooltipMove;
    public Image ImageTooltipJump;
    public Image ImageTooltipAttack;


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

        switch (player.type)
        {
            case UnitType.Player:
                ImageTooltipMove.gameObject.SetActive(true);
                ImageTooltipJump.gameObject.SetActive(true);
                ImageTooltipAttack.gameObject.SetActive(true);
                break;
            case UnitType.Goblin:
                ImageTooltipMove.gameObject.SetActive(true);
                ImageTooltipJump.gameObject.SetActive(false);
                ImageTooltipAttack.gameObject.SetActive(false);
                break;
            case UnitType.GoblinArcher:
                ImageTooltipMove.gameObject.SetActive(false);
                ImageTooltipJump.gameObject.SetActive(false);
                ImageTooltipAttack.gameObject.SetActive(true);
                break;
        }
    }

    public void PortraitChange(Player player)
    {
        Portrait.sprite = UIManager.Sprites
            .Find(x => x.name.Split('_')[1].Equals(player.type.ToString()));

    }

}