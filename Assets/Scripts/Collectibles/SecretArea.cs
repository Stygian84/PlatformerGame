using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretArea : MonoBehaviour
{
    private Animator anim;
    public GameObject rainbowText;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll != null && coll.transform.tag == "Player")
        {
            anim.SetBool("secret", true);
            coll.GetComponent<Score>().AddScore(1000);
            rainbowText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll != null && coll.transform.tag == "Player")
        {
            anim.SetBool("secret", false);
            rainbowText.SetActive(false);
        }
    }
}
