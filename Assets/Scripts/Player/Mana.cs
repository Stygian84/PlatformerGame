using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [Header("Mana")]
    public int startingMana;
    public int currentMana { get; private set; }
    public int manaIncreasePerSecond = 5; // Adjust as needed
    public float manaIncreaseInterval = 1f; // Adjust as needed

    private float timer = 0f;

    private Animator anim;
    public IntVariable mana;

    private void Awake()
    {
        currentMana = PlayerPrefs.GetInt("PlayerMana", startingMana); // Default to 50 if not found

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Increment health at a specified interval
        timer += Time.deltaTime;
        if (timer >= manaIncreaseInterval)
        {
            //mana.ApplyChange(manaIncreasePerSecond);
            currentMana += manaIncreasePerSecond;
            currentMana = Mathf.Clamp(currentMana, 0, startingMana);
            mana.SetValue(currentMana);
            timer = 0f;
        }
    }

    public void SpentMana(int _value)
    {
        currentMana = Mathf.Clamp(currentMana - _value, 0, startingMana);
        mana.SetValue(currentMana);
        if (currentMana < 10)
        {
            anim.SetTrigger("outofmana");
        }
        PlayerPrefs.SetInt("PlayerMana", currentMana);
    }

    public void AddMana(int _value)
    {
        currentMana = Mathf.Clamp(currentMana + _value, 0, startingMana);
        mana.SetValue(currentMana);
        PlayerPrefs.SetInt("PlayerMana", currentMana);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        this.gameObject.SetActive(true);
        AddMana(startingMana);
        mana.SetValue(startingMana);
        anim.ResetTrigger("outofmana");
        anim.Play("idle");
        PlayerPrefs.SetInt("PlayerMana", currentMana);
    }

    public int GetCurrentMana()
    {
        return mana.Value;
    }
}
