using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGoblinRange : MonoBehaviour
{
    public List<Collider2D> colleague = new List<Collider2D>();
    public List<Collider2D> near_colleague = new List<Collider2D>();
    public Player Goblin;
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (near_colleague.Contains(other))
        {
            near_colleague.Remove(other);
            Debug.Log(near_colleague.Count);
            if (near_colleague.Count == 0)
            {
                Goblin.Attack(Vector3.zero,0.0f);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (colleague.Contains(other))
        {
            near_colleague.Add(other);
        }
    }
}
