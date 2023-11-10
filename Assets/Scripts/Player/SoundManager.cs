using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip heartCollected;
    public AudioClip shootSound;
    public AudioClip meleeSound;
    public AudioClip noManaSound;
    public AudioClip hurtSound;

    private AudioSource playerSource;

    // Start is called before the first frame update
    void Start()
    {
        playerSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Collectibles")
        {
            playerSource.PlayOneShot(heartCollected);
            GameObject.Find("Player").GetComponent<Score>().AddScore(50);
        }
    }

    public void playShootSound()
    {
        playerSource.PlayOneShot(shootSound);
    }

    public void playMeleeSound()
    {
        playerSource.PlayOneShot(meleeSound);
    }

    public void playNoManaSound()
    {
        playerSource.PlayOneShot(noManaSound);
    }

    public void playHurtSound()
    {
        playerSource.PlayOneShot(hurtSound);
    }
}
