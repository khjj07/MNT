using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Unit
{
    public bool focus = false;
    private float rayDistance = 300f;
    private RaycastHit2D hit;
    private bool CoroutineRunning=false;
    public float attackdelay = 2f;
    public void FocusOn()
    {
        focus = true;
    }
    public void FocusOff()
    {
        focus = false;
    }
    public override void Move(int direction)
    {
        if (focus)
        {
            base.Move(direction);
        }
    }
    public override void Jump()
    {
        if (focus)
        {
            base.Jump();
        }
    }
    public override void Attack()
    {
        if (focus)
        {
            base.Attack();
        }
    }
    void Start()
    {
        /* this.UpdateAsObservable()
             .Where(_ => type == UnitType.GoblinArcher && !focus && GoblinArcherRayCheck())
             .Subscribe(_ => StartCoroutine(GoblinArcherAttack()))
             .AddTo(gameObject);
        */
    }
   
    public bool GoblinArcherRayCheck()
    {
        Vector3 direction;
        if (transform.rotation == Quaternion.Euler(0f, 0f, 0f))
            direction = Vector3.right;
        else
            direction = Vector3.left;
        hit = Physics2D.Raycast(transform.position+new Vector3(2f,1.5f,0), direction * rayDistance);
        Debug.DrawRay(transform.position + new Vector3(2f, 1.5f, 0), direction * rayDistance, new Color(0, 1, 0));
        return hit.collider != null && hit.collider.CompareTag("Player") && !CoroutineRunning;
    }
    public IEnumerator GoblinArcherAttack()
    {
        CoroutineRunning = true;
        while (!focus)
        {
            Debug.Log("attack");
            yield return new WaitForSeconds(attackdelay);
        }
        yield return null;
        CoroutineRunning = false;
    }
}
