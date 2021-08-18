using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public virtual void Move()
    {
        Vector3 mPosition = Input.mousePosition; //마우스 좌표 저장
        Vector3 oPosition = transform.position; //게임 오브젝트 좌표 저장
        mPosition.z = oPosition.z - Camera.main.transform.position.z;
        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

    }
}
