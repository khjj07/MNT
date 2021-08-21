using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

public class UIMenuController : MonoBehaviour
{
    public UIActiveController uIMenuAni;

    public TextMeshProUGUI TextCurrentState;
    public TextMeshProUGUI TextStageInformation;

    public Button ButtonResume;

    private void Awake()
    {
        TextStageInformation.text = SceneManager.GetActiveScene().name;


        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .Subscribe(_ =>
            {
                if (uIMenuAni.gameObject.activeSelf)
                {
                    uIMenuAni.OffUI();
                    Debug.Log("Off UI");
                }
                else
                {
                    uIMenuAni.OnUI();
                    Debug.Log("On UI");
                }
            })
            .AddTo(gameObject);
    }

}
