using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Events;
public class Player : Unit
{
    public bool focus = false;
    private float rayDistance = 300f;
    private RaycastHit2D hit;
    private bool CoroutineRunning=false;
    public float attackdelay = 2f;
    public UnityEvent AttackEvent;
    public Transform point;
    public GameObject Wifi;

    public IEnumerator DelayFocus()
    {
        yield return new WaitForSeconds(0.1f);
        focus = true;
        yield return null;
    }
    public void FocusOn()
    {
        if(type!=UnitType.Player && Wifi)
        {
            SoundManager.Instance.PlayOneShotSFX(SoundManager.ESFX._sfx_electricWave);
            Wifi.SetActive(true);
        }
        StartCoroutine(DelayFocus());
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void FocusOff()
    {
        focus = false;
        if (Wifi)
        {
            Wifi.SetActive(false);
        }
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void DeadChange()
    {
        if(focus)
        {
            TurnManager.instance.UpdatePlayer();
        }
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
    public override void PlayerAttack()
    {
        if (focus)
        {
            Debug.Log(this);
            base.PlayerAttack();
            TurnManager.instance.NextTurn();
        }
    }
    void Start()
    {
        this.UpdateAsObservable()
             .Where(_ => type == UnitType.GoblinArcher && !focus && GoblinArcherRayCheck())
             .Subscribe(_ => StartCoroutine(GoblinArcherAttack()))
             .AddTo(gameObject);
    }
    public bool GoblinArcherRayCheck()
    {
        Vector3 direction;
        if (transform.rotation == Quaternion.Euler(0f, 0f, 0f))
            direction = Vector3.right;
        else
            direction = Vector3.left;
        hit = Physics2D.Raycast(point.position, direction * rayDistance);
        Debug.DrawRay(point.position, direction * rayDistance, new Color(0, 1, 0));
        return hit.collider != null && hit.collider.CompareTag("Unit") && (hit.collider.GetComponent<Player>().type==UnitType.Player || hit.collider.GetComponent<Player>().type == UnitType.Friend) && !CoroutineRunning;
    }
    public IEnumerator GoblinArcherAttack()
    {

        CoroutineRunning = true;
        AttackEvent.Invoke();
        Debug.Log("attack");
        if (transform.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            Attack(Vector3.right, 180f,false);
        }
        else
        {
            Attack(Vector3.left, -180f, false);
        }

        yield return new WaitForSeconds(attackdelay);
        CoroutineRunning = false;
    }
}
