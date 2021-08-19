using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

abstract public class Unit:MonoBehaviour
{
    public float speed=10f;
    public float jumpforce = 10f;
    public bool jumpable = false;
    public int type = 0;
    public GameObject arrow;
    public GameObject blood;
    private Animator animator;
    public Transform weapon;
    void Start()
    {
        animator = GetComponent<Animator>();
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            .Subscribe(_ => Stop())
            .AddTo(gameObject);
    }
    public void Stop()
    {
        animator.SetBool("move", false);
    }
    public virtual void Move(int direction)
    {
        Direction dir = (Direction)direction;
        animator.SetBool("move", true);

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
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 1.0f, 0) * jumpforce, ForceMode2D.Impulse);
            animator.SetBool("jump", true);
            jumpable = false;
        }
    }

    public virtual void Attack()
    {
        animator.SetBool("attack", true);
        if ((UnitType)type == UnitType.MeleeWeapon)
        {
            //무기휘두르기
        }
        else if((UnitType)type == UnitType.RangedWeapon)
        {
            GameObject instance=(GameObject)Instantiate(arrow, weapon.position, weapon.rotation);
            //투사체 생성
        }
        animator.SetBool("attack", false);
    }
    public void Hit()
    {
        animator.SetTrigger("die");
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Instantiate(blood, transform.position+new Vector3(0,1f,0), Quaternion.identity);
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    public void OnTheGround(Collision2D collision)
    {
        Vector3 normalVector = collision.contacts[0].normal;
        if (normalVector.y>0.5)
        {
            jumpable = true;
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        OnTheGround(collision);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 normalVector = collision.contacts[0].normal;
        if (normalVector.y > 0.5)
        {
            animator.SetBool("jump", false);
        }
    }
}
