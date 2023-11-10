using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        boxCollider2D = transform.GetChild(1).GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*3f,ForceMode2D.Impulse);

            boxCollider2D.enabled = true;
            anim.SetTrigger("melee");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Health>().TakeDamage(1);
                GameObject.Find("Player").GetComponent<Score>().AddScore(50);
            }
        }
    }
}
