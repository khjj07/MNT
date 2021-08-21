using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageCheck : MonoBehaviour
{
    public Button Stage2;
    public Button Stage3;
    public Button Stage4;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("1") == 2) Stage2.interactable = true; else Stage2.interactable = false;
        if (PlayerPrefs.GetInt("2") == 2) Stage3.interactable = true; else Stage3.interactable = false;
        if (PlayerPrefs.GetInt("3") == 2) Stage4.interactable = true; else Stage4.interactable = false;
    }
}
