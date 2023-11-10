using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name == "Player"))
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
    void Update()
    {
        transform.Rotate(0, 0, 360 * 2 * Time.deltaTime);
    }
}
