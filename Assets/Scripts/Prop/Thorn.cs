using System.Collections;
using UnityEngine;

public class Thorn : Prop
{
    public float delayTime;
    public override void CollisionEnterUnit(Collision2D collision)
    {
        Debug.Log("Hit Thorn");
        animator.SetTrigger("Trigger");
        collision.gameObject.GetComponent<Player>().Die();
    }

    public override void CollisionEnterProp(Collision2D collision)
    {

    }

    public override void CollisionEnterArrow(Collision2D collision)
    {
        
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
        GameObject.Destroy(this.gameObject);
    }
}