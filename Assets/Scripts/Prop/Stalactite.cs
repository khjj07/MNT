using System.Collections;
using UnityEngine;

public class Stalactite : Prop
{
    protected Rigidbody2D rigid;

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody2D>();
    }

    public override void CollisionEnterProp(Collision2D collision)
    {

    }

    public override void CollisionEnterUnit(Collision2D collision)
    {

    }

    public override void CollisionEnterArrow(Collision2D collision)
    {
        rigid.gravityScale = 1;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            Debug.Log("Arrow 충돌");
            rigid.gravityScale = 1;
        }

        if (collision.CompareTag("Unit"))
        {
            Debug.Log("Unit 충돌");
            collision.gameObject.GetComponent<Unit>().Hit();
            rigid.gravityScale = 0;
            rigid.velocity = Vector3.zero;
            animator.SetTrigger("Destroy");
        }

        if (collision.CompareTag("Tile"))
        {
            Debug.Log("Tile 충돌");
            rigid.gravityScale = 0;
            rigid.velocity = Vector3.zero;
            animator.SetTrigger("Destroy");
        }
    }

    /// <summary>
    /// 다른 객체에서 이 객체를 부술때 사용할 메소드
    /// </summary>
    public void DestroyProp()
    {
        animator.SetTrigger("Destroy");
    }

    /// <summary>
    /// Animation Cilp 에서 Event를 통해 호출되는 함수
    /// </summary>
    private void Destroy()
    {
        SoundManager.Instance.PlayOneShotSFX(SoundManager.ESFX._sfx_stalactiteBroken);
        GameObject.Destroy(this.gameObject);
    }
}