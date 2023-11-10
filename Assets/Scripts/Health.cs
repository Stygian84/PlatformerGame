using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int startingHealth;
    public int currentHealth { get; private set; }
    private Animator anim;

    [Header("iFrames")]
    [SerializeField]
    private float iFramesDuration;

    [SerializeField]
    private int numberOffFlashes;
    private SpriteRenderer spriteRend;

    private bool invulnerable;
    public IntVariable health;
    private LayerMask originalLayer;

    private void Awake()
    {
        if (gameObject.name == "Player")
        {
            currentHealth = PlayerPrefs.GetInt("PlayerHealth", startingHealth);
            health.SetValue(currentHealth);
        }
        else
        {
            currentHealth = startingHealth;
        }
        originalLayer = this.gameObject.layer;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int _damage)
    {
        if (invulnerable)
            return;
        if (gameObject.name == "Player")
        {
            health.ApplyChange(-_damage);
            PlayerPrefs.SetInt("PlayerHealth", health.Value);

            if (health.Value < 0)
            {
                health.Value = 0;
                SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
            }
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            anim.SetTrigger("die");
        }
    }

    void Update()
    {
        if (invulnerable)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Invincible");
        }
        else
        {
            this.gameObject.layer = originalLayer;
        }
    }

    public void AddHealth(int _value)
    {
        if (gameObject.name == "Player")
        {
            PlayerPrefs.SetInt("PlayerHealth", health.Value);
            health.ApplyChange(_value);
        }
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);
        for (int i = 0; i < numberOffFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 10, false);
        Physics2D.IgnoreLayerCollision(9, 11, false);
        invulnerable = false;
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        this.gameObject.SetActive(true);
        AddHealth(startingHealth);
        health.SetValue(startingHealth);

        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(Invulnerability());
    }

    public int GetCurrentHealth()
    {
        return health.Value;
    }
}
