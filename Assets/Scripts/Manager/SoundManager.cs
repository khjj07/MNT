using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    Debug.LogError("SoundManager singelton Error");
                }
            }
            return instance;
        }
    }

    [Header("[[AudioSource]]")]
    [SerializeField]
    AudioSource bgm;

    public AudioSource sfx;

    public AudioSource sfx_Loop;


    public enum EBGM
    {
        _bgm_intro,
        _bgm_outro,
        _bgm_title,
        _bgm_ingame,

    }

    public enum ESFX
    {
        _sfx_arrowEnd,
        _sfx_arrowStart,
        _sfx_blood,
        _sfx_button,
        _sfx_electricWave,
        _sfx_jump,
        _sfx_jumpLanding,
        _sfx_stalactiteBroken,
        _sfx_trap,
        _sfx_text,
    }

    public enum ESFX_Loop
    {
        _sfx_loop_move,
        _sfx_loop_heartBeat,
    }

    [Header("[BGM]")]
    [SerializeField]
    AudioClip bgm_intro;

    [SerializeField]
    AudioClip bgm_outro;

    [SerializeField]
    AudioClip bgm_title;

    [SerializeField]
    AudioClip bgm_ingame;


    [Header("[SFX]")]

    [SerializeField]
    AudioClip sfx_arrowEnd;
    [SerializeField]
    AudioClip sfx_arrowStart;
    [SerializeField]
    AudioClip sfx_blood;
    [SerializeField]
    AudioClip sfx_button;
    [SerializeField]
    AudioClip sfx_electricWave;
    [SerializeField]
    AudioClip sfx_jump;
    [SerializeField]
    AudioClip sfx_jumpLanding;
    [SerializeField]
    AudioClip sfx_stalactiteBroken;
    [SerializeField]
    AudioClip sfx_trap;
    [SerializeField]
    AudioClip sfx_text;




    [Header("[SFX_Loop]")]

    [SerializeField]
    AudioClip sfx_Loop_move;
    [SerializeField]
    AudioClip sfx_Loop_heartBeat;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayBGM(EBGM value, bool isLoop = false)
    {
        SetBGM(value);
        bgm.loop = isLoop;
        bgm.Play();
    }

    public void StopBGM(EBGM value)
    {
        SetBGM(value);
        bgm.Stop();
    }


    public void PlayOneShotSFX(ESFX value)
    {
        AudioClip aclip = SetSFX(value);

        if (aclip)
            sfx.PlayOneShot(aclip);
    }

    public void StopSFXLoop(ESFX_Loop value)
    {
        SetSFX_Loop(value);
        sfx_Loop.Stop();
    }
    public void PlayLoopSFX(ESFX_Loop value)
    {
        AudioClip bclip = SetSFX_Loop(value);
        if (bclip && sfx.isPlaying == false)
            sfx.PlayOneShot(bclip);
    }


    void SetBGM(EBGM value)
    {
        switch (value)
        {
            case EBGM._bgm_ingame:
                bgm.clip = bgm_ingame;
                break;
            case EBGM._bgm_intro:
                bgm.clip = bgm_intro;
                break;
            case EBGM._bgm_outro:
                bgm.clip = bgm_outro;
                break;
            case EBGM._bgm_title:
                bgm.clip = bgm_title;
                break;
        }
    }

    AudioClip SetSFX(ESFX value)
    {
        switch (value)
        {
            case ESFX._sfx_arrowEnd:
                return sfx_arrowEnd;
            case ESFX._sfx_arrowStart:
                return sfx_arrowStart;
            case ESFX._sfx_blood:
                return sfx_blood;
            case ESFX._sfx_button:
                return sfx_button;
            case ESFX._sfx_electricWave:
                return sfx_electricWave;
            case ESFX._sfx_jump:
                return sfx_jump;
            case ESFX._sfx_jumpLanding:
                return sfx_jumpLanding;
            case ESFX._sfx_stalactiteBroken:
                return sfx_stalactiteBroken;
            case ESFX._sfx_text:
                return sfx_text;
            case ESFX._sfx_trap:
                return sfx_trap;

            default:
                return null;
        }
    }

    AudioClip SetSFX_Loop(ESFX_Loop value)
    {
        switch (value)
        {
            case ESFX_Loop._sfx_loop_heartBeat:
                return sfx_Loop_heartBeat;
            case ESFX_Loop._sfx_loop_move:
                return sfx_Loop_move;
            default:
                return null;
        }
    }
}
