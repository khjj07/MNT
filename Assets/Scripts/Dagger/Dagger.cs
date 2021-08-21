using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public GameObject owner;
    public void TriggerOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit") && collision.gameObject != owner)
        {
            collision.GetComponent<Player>().Hit();
            GetComponent<BoxCollider2D>().enabled = false;
        }

    }
}
