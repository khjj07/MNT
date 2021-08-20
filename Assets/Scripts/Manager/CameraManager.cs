using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : Singleton<CameraManager>
{
    Tweener cameraMovingTween;
    Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        cameraMovingTween = transform.DOMove(initPos, 1).SetEase(Ease.OutExpo).SetAutoKill(false);
        TurnManager.instance.TurnChangeEvent.AddListener(
            () => NewInitPos(TurnManager.instance.current_player));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewInitPos(Player player)
    {
        initPos = player.transform.position;
        initPos.z = -1f;
        CameraMovingByTurn();
    }

    public void CameraMovingByTurn()
    {
        cameraMovingTween.ChangeEndValue(initPos, true).Restart();
    }

    public void CameraMovingByIcon(bool isMouseEnter)
    {
        if (isMouseEnter)
        {

        }
        else
        {

        }
        
    }

}
