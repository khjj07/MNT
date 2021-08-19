using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public float duration = 2f;
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public IEnumerator CollisionOn()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return 0;
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
        StartCoroutine(CollisionOn());
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            Debug.Log("hit");
            collision.GetComponent<Player>().Hit();
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Tile"))
        {
            Debug.Log("Tile Ãæµ¹");
            Destroy(gameObject);
        }
    }

}

