using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public float duration = 2f;
    public GameObject shooter;
    public float rotateDegree;
    public Vector3 direction;
    public void Destroy()
    {
        Destroy(gameObject);
    }
    
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
        Time.timeScale = 0.1f;
        this.UpdateAsObservable()
             .Where(_ => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space))
             .Subscribe(_ => { Time.timeScale = 1f; })
             .AddTo(gameObject);

        transform.DOMove(transform.position+direction * speed * duration, duration).OnComplete(()=>{
            Destroy();
        });
    }
    void OnDestroy()
    {
        Time.timeScale = 1f;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit") && collision.gameObject != shooter)
        {
            collision.GetComponent<Player>().Hit();
            Destroy(gameObject);
        }

        if(collision.CompareTag("Tile"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Prop"))
        {
            Destroy(gameObject);
        }
    }

}

