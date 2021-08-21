using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class UICharacter : Singleton<UICharacter>
{
    private TurnManager TurnManager;
    private UIManager UIManager;
    private ChatManager ChatManager;

    public UIObjectControllerX UIEndCut;

    public Image Portrait;
    public Image ImageTooltipMove;
    public Image ImageTooltipJump;
    public Image ImageTooltipAttack;

    public Image ImageVictory;

    public Image ImageChatBox;
    //public TextMeshProUGUI TextName;
    public Text TextChat;
    public bool isGetStartChat = true;

    // Use this for initialization
    void Start()
    {
        TurnManager = TurnManager.instance;
        TurnManager.TurnChangeEvent.AddListener(() => PortraitChange());

        UIManager = UIManager.instance;

        ChatManager = ChatManager.instance;
    }

    public void PortraitChange()
    {
        Player player = TurnManager.current_player;
        Debug.Log(player);
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


    public void Chat(string name, string chat)
    {
        //TextName.text = name;
        TextChat.text = "";
        TextChat.DOText(chat, 1f);
    }

    public void ChatBoxButtonDown()
    {
        if (isGetStartChat)
            NextStartChat();
        else
            NextEndChat();
    }

    public void NextStartChat()
    {
        var chat = ChatManager.GetStartChat();

        if (chat == null)
        {
            ImageChatBox.gameObject.SetActive(false);
            //PortraitChange();
            CameraManager.instance.ChatPlayer(null);
            TurnManager.GameStart();
        }
        else
        {
            Chat(chat.player.name, chat.chat);
            PortraitChange(chat.player);
            CameraManager.instance.ChatPlayer(chat.player);
        }
    }

    public void NextEndChat()
    {
        var chat = ChatManager.GetEndChat();

        if (chat == null)
        {
            ImageChatBox.gameObject.SetActive(false);
            //PortraitChange();
            CameraManager.instance.ChatPlayer(null);

            if (UIEndCut)
            {
                UIEndCut.gameObject.SetActive(true);
                Invoke(nameof(Back), 8f);
            }
            else
            {
                ImageVictory.GetComponent<UIActiveController>().OnUI();
            }


        }
        else
        {
            ImageChatBox.gameObject.SetActive(true);
            Chat(chat.player.name, chat.chat);
            PortraitChange(chat.player);
            CameraManager.instance.ChatPlayer(chat.player);
        }
    }

    void Back()
    {
        SceneManager.LoadScene(0);
    }
}