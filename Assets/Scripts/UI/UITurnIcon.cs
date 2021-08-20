using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UITurnIcon : MonoBehaviour
{
    private UIManager UIManager;
    private UITurn UITurn;
    [SerializeField]
    private Player player;

    public int id;
    public int turn;

    public Image ImagePortrait;
    public Image ImageTurn;

    // Use this for initialization
    void Start()
    {
        UITurn = GetComponentInParent<UITurn>();
        UIManager = UIManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayer(Player player)
    {
        if (player == null)
        {
            gameObject.SetActive(false);
            return;
        }

        this.player = player;
        ImagePortrait.sprite = UIManager.Sprites
            .Find(x => x.name.Split('_')[1].Equals(player.type.ToString()));

    }

    public void PointerEnter()
    {

    }

    public void PointerExit()
    {

    }
}