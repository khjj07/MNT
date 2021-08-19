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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
