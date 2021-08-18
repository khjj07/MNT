using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

abstract public class Unit:MonoBehaviour
{
    public float speed=10f;
    public float jumpforce = 2f;
    public bool jumpable = false;
    public int type = 0;
    public GameObject arrow;

    public virtual void Move(int direction)
    {
        Direction dir = (Direction)direction;
        
        if (dir == Direction.Left)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if(dir == Direction.Right)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
    public virtual void Jump()
    {
        if(jumpable)
        {
            transform.DOMoveY(transform.position.y+jumpforce, 0.3f);
            jumpable = false;
        }
    }

    public virtual void Attack()
    {
        if((UnitType)type == UnitType.MeleeWeapon)
        {
            //무기휘두르기
        }
        else if((UnitType)type == UnitType.RangedWeapon)
        {
            GameObject instance=(GameObject)Instantiate(arrow,transform.position,transform.rotation);
            //투사체 생성
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 normalVector = collision.contacts[0].normal;
        Debug.Log(normalVector);
        if (normalVector.y>0.5)
        {
            jumpable = true;
        }
    }
}
