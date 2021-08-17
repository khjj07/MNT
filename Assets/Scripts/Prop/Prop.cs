using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

abstract public class Prop : MonoBehaviour
{
    public float speed = 10f;
    public float jumpforce = 2f;
    public bool jumpable = false;

    public virtual void Move(int direction)
    {
        Direction dir = (Direction)direction;

        if (dir == Direction.Left)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (dir == Direction.Right)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    public virtual void Jump()
    {
        if (jumpable)
        {
            transform.DOMoveY(transform.position.y + jumpforce, 0.3f);
            jumpable = false;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 normalVector = collision.contacts[0].normal;
        Debug.Log(normalVector);
        if (normalVector.y > 0.5)
        {
            jumpable = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            CollisionEnterUnit(collision);
        }

        if (collision.gameObject.CompareTag("Prop"))
        {
            CollisionEnterProp(collision);
        }
            
    }

    public abstract void CollisionEnterUnit(Collision2D collision);
    public abstract void CollisionEnterProp(Collision2D collision);

}
