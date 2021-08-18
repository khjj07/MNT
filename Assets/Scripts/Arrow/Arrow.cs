using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public float duration = 5f;
    public void Destroy()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        Vector3 mPosition = Input.mousePosition;
        Vector3 oPosition = transform.position;
        mPosition.z = oPosition.z - Camera.main.transform.position.z;
        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
        Vector3 direction = Vector3.Normalize(target - oPosition);
        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
        transform.DOMove(transform.position+direction * speed * duration, duration).OnComplete(()=>{
            Destroy();
        });
    }
   
}

