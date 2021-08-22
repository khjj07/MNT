using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

public class MNTSceneManager : MonoBehaviour
{
    public void SceneMove(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
    public void StopBGM()
    {
        SoundManager.Instance.StopBGM(SoundManager.EBGM._bgm_title);
        
    }    

    public void PlayBGM()
    {
        SoundManager.Instance.PlayBGM(SoundManager.EBGM._bgm_ingame);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GotoNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
