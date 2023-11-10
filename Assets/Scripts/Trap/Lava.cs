using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform CheckPoint = GameObject
            .Find("Player")
            .GetComponent<PlayerRespawn>()
            .currentCheckPoint;
        if (collision.name == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(4);
            if (CheckPoint != null)
            {
                GameObject.Find("Player").GetComponent<Transform>().position = CheckPoint.position;
            }
            else
            {
                GameObject.Find("Player").GetComponent<Transform>().position = new Vector2(0, 0);
            }
        }
    }
}
