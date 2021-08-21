﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;


public class UITurnIcon : MonoBehaviour
{
    private CameraManager CameraManager;
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
        CameraManager = CameraManager.instance;
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
            .Find(x =>
            {
                if (x.name.Split('_')[0].Equals("Turn"))
                {
                    if (x.name.Split('_')[1].Equals(player.type.ToString()))
                        return true;
                }
                return false;
            });

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Is Player Die?</returns>
    public bool IsPlayerDie()
    {
        Debug.Log("Check Player : " + player);
        if (player == null)
        {
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    public void DieDirection(float directionTime)
    {
        this.transform.DOLocalMoveY(100f, directionTime).SetRelative(true);
        this.ImagePortrait.DOFade(0f, directionTime).onComplete
            += () => gameObject.SetActive(false);
    }


    public void PointerEnter()
    {
        Debug.Log("Enter");
        //CameraManager.PurposePlayer(player);
    }

    public void PointerExit()
    {
        Debug.Log("Exit");
        //CameraManager.PurposePlayer(null);
    }
}