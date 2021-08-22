using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using UnityEngine.Events;
abstract public class Unit:MonoBehaviour
{
    public float speed=10f;
    public float jumpforce = 10f;
    public bool jumpable = false;
    public WeaponType weapontype = 0;
    public UnitType type;
    public bool Attackable = true;
    public GameObject arrow;
    public GameObject blood;
    private Animator animator;
    public Transform weapon;
    public UnityEvent HitEvent;
    public bool alive = true;
    public UnityEvent DieEvent;
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
        if (alive && jumpable)
        {
            SoundManager.Instance.PlayLoopSFX(SoundManager.ESFX_Loop._sfx_loop_move);
        }
        if (alive && dir == Direction.Left)
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
        else if(alive && dir == Direction.Right)
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
    public IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.1f);
        jumpable = false;
    }
    public virtual void Jump()
    {
        if(jumpable && alive)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 1.0f, 0) * jumpforce, ForceMode2D.Impulse);
            SoundManager.Instance.PlayOneShotSFX(SoundManager.ESFX._sfx_jump);
            animator.SetBool("jump", true);
            StartCoroutine(JumpDelay());
        }
    }
    public void CantAttack()
    {
        Attackable = false;
    }
    public void Attack(Vector3 direction,float rotateDegree,bool playershoot)
    {
        if (Attackable && weapontype == WeaponType.RangedWeapon)
        {
            SoundManager.Instance.PlayOneShotSFX(SoundManager.ESFX._sfx_arrowStart);
            GameObject instance = (GameObject)Instantiate(arrow, weapon.position, weapon.rotation);
            instance.GetComponent<Arrow>().shooter = (GameObject)gameObject;
            instance.GetComponent<Arrow>().direction = direction;
            instance.GetComponent<Arrow>().rotateDegree = rotateDegree;
            instance.GetComponent<Arrow>().playershoot = playershoot;
        }
        else if(Attackable)
        {
            animator.SetBool("attack", true);
            Debug.Log("Attack");
        }
    }
    public void StopAttack()
    {
        animator.SetBool("attack", false);
    }

    public virtual void PlayerAttack()
    {
        animator.SetBool("attack", true);
        if(weapontype == WeaponType.RangedWeapon)
        {
            Vector3 mPosition = Input.mousePosition;
            Vector3 oPosition = transform.position;
            mPosition.z = oPosition.z - Camera.main.transform.position.z;
            Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
            float dy = target.y - oPosition.y;
            float dx = target.x - oPosition.x;
            Attack(Vector3.Normalize(target - oPosition), Mathf.Atan2(dy, dx) * Mathf.Rad2Deg,true);
            //instance.GetComponent<Arrow>().target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //투사체 생성
        }
        
        animator.SetBool("attack", false);
    }
    public void Hit()
    {
        animator.SetBool("jump", false);
        animator.SetTrigger("die");
        //GetComponent<Rigidbody2D>().gravityScale = 0;
        //GetComponent<CapsuleCollider2D>().enabled = false;
        Instantiate(blood, transform.position+new Vector3(0,1f,0), Quaternion.identity);
        SoundManager.Instance.PlayOneShotSFX(SoundManager.ESFX._sfx_blood);
        CantAttack();
        alive = false;
    }
    public void Die()
    {
        if(type==UnitType.Player || type == UnitType.Friend)
        {
            TurnManager.instance.Defeat();
            Destroy(gameObject);
        }
        else
        {
            TurnManager.instance.UnitDead(transform.GetComponent<Player>());
            Destroy(gameObject);
            //DieEvent.Invoke();
            DeadChange();
        }
    }

    public virtual void DeadChange()
    {

    }

    public void OnTheGround(Collision2D collision)
    {
        Vector3 normalVector = collision.contacts[0].normal;
        if (normalVector.y>0.7 && jumpable == false)
        {
            jumpable = true;
            SoundManager.Instance.PlayOneShotSFX(SoundManager.ESFX._sfx_jumpLanding);
        }
    }
   
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 normalVector = collision.contacts[0].normal;
        
        if (collision.gameObject.tag=="Unit" && type==UnitType.Player)
        {
            UnitType type = collision.gameObject.GetComponent<Player>().type;
            if(type==UnitType.Goblin || type == UnitType.GoblinArcher || type == UnitType.IronGoblin)
            {
                Hit();
            }

        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!jumpable)
        {
            animator.SetBool("jump", false);
        }
        OnTheGround(collision); 
    }
}
