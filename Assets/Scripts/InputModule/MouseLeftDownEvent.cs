using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Events;

public class MouseLeftDownEvent : MonoBehaviour
{
    private Vector3 current_pos;

    public UnityEvent OnMouseInput;
    // Use this for initialization
    void Start()
    {
        this.UpdateAsObservable()
             .Where(_ => Input.GetMouseButtonDown(0))
             .Subscribe(_ => OnMouseInput.Invoke())
             .AddTo(gameObject);
    }
   
}