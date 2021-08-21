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
    /// Turn Manager�� Currnet Player.
    /// �⺻ ������ ��� ī�޶�� �׻� Currnet Player�� �����Ѵ�.
    /// </summary>
    /// <param name="player">Turn Manager�� Current Player</param>
    public void CurrentPlayer(Player player)
    {
        curPlayer = player;
    }

    /// <summary>
    /// �÷��̾ UI Turn Icon ���� ���콺�� Enter ���� �� ȣ��
    /// ���콺�� Exit ���� ��� null ���� �޵��� �Ѵ�.
    /// </summary>
    /// <param name="player">Mouse Position�� Turn Icon�� Player</param>
    public void PurposePlayer(Player player)
    {
        purPlayer = player;
    }

    public void ChatPlayer(Player player)
    {
        chatPlayer = player;
    }

    /// <summary>
    /// ī�޶� ����.
    /// </summary>
    /// <param name="player"></param>
    void CameraMoving()
    {
        if (isCameraMovingStart == false)
            return;

        if (chatPlayer != null)
        {
            Vector3 targetVec = chatPlayer.transform.position;

            // z �� ����
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

            // z �� ����
            targetVec.z = -2f;

            cameraMovingTween.ChangeEndValue(targetVec, true).Restart();
        }
    }
}
