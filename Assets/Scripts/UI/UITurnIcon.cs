using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;


public class UITurnIcon : MonoBehaviour
{
    private CameraManager CameraManager;
    public UIManager UIManager;
    public UITurn UITurn;
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

        Debug.Log(UIManager);

        this.player = player;
        ImagePortrait.sprite = UIManager.Sprites
            .FindAll(x => x.name.Split('_')[0].Equals("Turn"))
            .Find(x => x.name.Split('_')[1].Equals(player.type.ToString()));

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