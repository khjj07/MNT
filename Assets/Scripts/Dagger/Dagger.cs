using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public GameObject owner;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit") && collision.gameObject != owner)
        {
            collision.GetComponent<Player>().Hit();
        }

    }
}
