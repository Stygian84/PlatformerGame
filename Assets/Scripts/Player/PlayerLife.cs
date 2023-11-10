using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]
    private AudioClip deathSound;
    private AudioSource deathSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathSoundEffect = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        //rb.bodyType = RigidbodyType2D.Static;
        GetComponent<PlayerController>().enabled = false;
        anim.SetTrigger("death");
        Time.timeScale=0f;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void PlayDeathSound(){
        deathSoundEffect.PlayOneShot(deathSound);
    }
}
