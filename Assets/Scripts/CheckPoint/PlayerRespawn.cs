using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public AudioClip checkPointSound;
    public Transform currentCheckPoint;
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawning()
    {
        if (currentCheckPoint != null)
        {
            transform.position = currentCheckPoint.position;
        }
        else
        {
            transform.position = new Vector2(0,0);
        }
        playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "CheckPoint")
        {
            currentCheckPoint = coll.transform;
            this.GetComponent<AudioSource>().PlayOneShot(checkPointSound);
            coll.GetComponent<Collider2D>().enabled = false;
            coll.GetComponent<Animator>().SetTrigger("checked");
        }
    }
}
