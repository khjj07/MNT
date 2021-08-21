using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class BackGround : MonoBehaviour
{
    public Transform Cam;
    public float Draft;
    private readonly ReactiveProperty<Vector3> Campos = new ReactiveProperty<Vector3>();
    void Start()
    {
        Campos.Subscribe(x => {
            Vector3 pos = x;
            pos.z = transform.position.z;
            transform.DOMove((pos - transform.position) * Draft, 0.6f)
            .SetEase(Ease.OutExpo)
            .SetAutoKill(false);
        });
    }

    void Update()
    {
        Campos.Value = Cam.position;
    }
}
