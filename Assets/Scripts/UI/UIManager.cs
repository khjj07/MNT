using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public TurnManager TurnManager;
    public UICutScene UICutScene;
    public UICharacter UICharacter;
    public Image UIDefeat;

    public List<Sprite> Sprites;

    private void Start()
    {
        TurnManager = TurnManager.instance;
        UICharacter = UICharacter.instance;
        UICutScene = UICutScene.instance;

        TurnManager.VictoryEvent.AddListener(Victory);
        TurnManager.DefeatEvent.AddListener(Defeat);


        int stageInfor =
            PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString());

        Debug.Log(stageInfor);

        stageInfor = 0;

        if (stageInfor == 0)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), 1);

            Debug.Log(UICutScene);

            if (UICutScene.isDirection)
                UICutScene.StartCut();
        }
    }

    void Victory()
    {
        Debug.Log("Victory Event");
        UICharacter.isGetStartChat = false;
        UICharacter.NextEndChat();
    }

    void Defeat()
    {
        Debug.Log("Defeat Event");
        UIDefeat.GetComponent<UIActiveController>().OnUI();
    }
}