using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Key")
        {
            coll.gameObject.transform.SetParent(transform);
            coll.gameObject.layer = LayerMask.NameToLayer("Invincible");
        }
    }
}
