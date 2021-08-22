using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.EBGM._bgm_ingame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
