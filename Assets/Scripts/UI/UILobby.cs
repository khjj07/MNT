using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILobby : MonoBehaviour
{
    public bool isReset = false;

    void Start()
    {
        print("»£√‚");

        SoundManager.Instance.StopBGM(SoundManager.EBGM._bgm_outro);
        SoundManager.Instance.StopBGM(SoundManager.EBGM._bgm_ingame);
        SoundManager.Instance.PlayBGM(SoundManager.EBGM._bgm_title);
        if (isReset)
        {
            isReset = false;
            PlayerPrefs.SetInt("1", 0);
            PlayerPrefs.SetInt("2", 0);
            PlayerPrefs.SetInt("3", 0);
            PlayerPrefs.SetInt("4", 0);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
