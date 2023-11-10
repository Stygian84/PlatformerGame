using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BouncePad : MonoBehaviour
{
    //public AudioClip bounceSound;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts[0].normal.y < -0.5f)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                this.GetComponent<AudioSource>().Play();
                anim.SetTrigger("bounce");
                col.gameObject
                    .GetComponent<Rigidbody2D>()
                    .AddForce(Vector2.up * 30, ForceMode2D.Impulse);
            }
        }
    }
}
