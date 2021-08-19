using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Unit
{
    public bool focus = false;
  
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
}
