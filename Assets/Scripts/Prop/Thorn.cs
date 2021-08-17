using System.Collections;
using UnityEngine;

public class Thorn : Prop
{

    public override void CollisionEnterUnit(Collision2D collision)
    {
        Debug.Log("Call DIE Method Character");
        GetComponent<Animator>().SetTrigger("Trigger");
    }

    public override void CollisionEnterProp(Collision2D collision)
    {
        Debug.Log("Call Destroy Prop");
    }


    /// <summary>
    /// Animation Cilp 에서 호출되는 함수
    /// </summary>
    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}