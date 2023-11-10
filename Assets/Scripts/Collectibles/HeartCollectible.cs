using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
    [SerializeField]
    private int healthValue;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            collision.GetComponent<Score>().AddScore(100);
            gameObject.SetActive(false);
        }
    }

}
