using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        transform.DOMove(transform.position+direction * speed * duration, duration).OnComplete(()=>{
            Destroy();
        });
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Unit") || collision.CompareTag("Player")) && collision.gameObject != shooter)
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

