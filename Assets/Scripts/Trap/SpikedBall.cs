using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name == "Player"))
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
}
