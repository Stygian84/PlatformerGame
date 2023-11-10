using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crates : MonoBehaviour
{
    public AudioClip crateDestroyed;
    public AudioClip crateHurt;
    private AudioSource audioSource;

    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void playCrateDestroyed()
    {
        audioSource.PlayOneShot(crateDestroyed);
    }

    public void playCrateHurt()
    {
        audioSource.PlayOneShot(crateHurt);
    }

}
