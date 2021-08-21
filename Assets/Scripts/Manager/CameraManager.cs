using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : Singleton<CameraManager>
{
    bool isCameraMovingStart = false;
    Tweener cameraMovingTween;
    Player curPlayer;
    Player purPlayer;
    Player chatPlayer;

    void Start()
    {
        cameraMovingTween = this.transform
            .DOMove(-Vector3.one, 0.6f)
            .SetEase(Ease.OutExpo)
            .SetAutoKill(false);

        TurnManager.instance.TurnChangeEvent.AddListener(
            () => CurrentPlayer(TurnManager.instance.current_player));

        isCameraMovingStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMoving();
    }

    /// <summary>
    /// Turn Manager의 Currnet Player.
    /// 기본 상태의 경우 카메라는 항상 Currnet Player를 추적한다.
    /// </summary>
    /// <param name="player">Turn Manager의 Current Player</param>
    public void CurrentPlayer(Player player)
    {
        curPlayer = player;
    }

    /// <summary>
    /// 플레이어가 UI Turn Icon 위에 마우스를 Enter 했을 때 호출
    /// 마우스를 Exit 했을 경우 null 값을 받도록 한다.
    /// </summary>
    /// <param name="player">Mouse Position의 Turn Icon의 Player</param>
    public void PurposePlayer(Player player)
    {
        purPlayer = player;
    }

    public void ChatPlayer(Player player)
    {
        chatPlayer = player;
    }

    /// <summary>
    /// 카메라 무빙.
    /// </summary>
    /// <param name="player"></param>
    void CameraMoving()
    {
        if (isCameraMovingStart == false)
            return;

        if (chatPlayer != null)
        {
            Vector3 targetVec = chatPlayer.transform.position;

            // z 축 조정
            targetVec.z = -2f;

            cameraMovingTween.ChangeEndValue(targetVec, true).Restart();

            return;
        }
        else
        {
            if (curPlayer == null)
                return;

            Vector3 targetVec;

            if (purPlayer == null)
                targetVec = curPlayer.transform.position;
            else
                targetVec = purPlayer.transform.position;

            // z 축 조정
            targetVec.z = -2f;

            cameraMovingTween.ChangeEndValue(targetVec, true).Restart();
        }
    }
}
