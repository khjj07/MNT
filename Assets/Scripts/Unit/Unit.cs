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
    public WeaponType weapontype = 0;
    public UnitType type;
    public GameObject arrow;
    public GameObject blood;
    private Animator animator;
    public Transform weapon;

    void Awake()
    {
        
        animator = transform.GetComponent<Animator>();
        this.UpdateAsObservable()
             .Where(_ => Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.D))
             .Subscribe(_ => Stop());
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
            if (type==UnitType.Goblin)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            if(transform.rotation== Quaternion.Euler(0f, 0f, 0f))
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            else
                transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if(dir == Direction.Right)
        {
            if (type == UnitType.Goblin)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            if (transform.rotation == Quaternion.Euler(0f, 0f, 0f))
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            else
                transform.Translate(Vector3.left * speed * Time.deltaTime);
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
        if(weapontype == WeaponType.RangedWeapon)
        {
            GameObject instance=(GameObject)Instantiate(arrow, weapon.position, weapon.rotation);
            instance.GetComponent<Arrow>().shooter = (GameObject)gameObject;
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
        TurnManager.instance.UnitDead(transform.GetComponent<GameState>());
        Destroy(gameObject);
        TurnManager.instance.UpdatePlayer();
    }

    public void OnTheGround(Collision2D collision)
    {
        Vector3 normalVector = collision.contacts[0].normal;
        if (normalVector.y>0.5)
        {
            jumpable = true;
        }
    }
   
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 normalVector = collision.contacts[0].normal;
        OnTheGround(collision);
        if (normalVector.y > 0.5)
        {
            animator.SetBool("jump", false);
        }
    }
}
