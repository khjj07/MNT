using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

abstract public class Prop : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
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

        if (collision.gameObject.CompareTag("Arrow"))
        {
            CollisionEnterArrow(collision);
        }
            
    }

    public abstract void CollisionEnterUnit(Collision2D collision);
    public abstract void CollisionEnterProp(Collision2D collision);
    public abstract void CollisionEnterArrow(Collision2D collision);

}
