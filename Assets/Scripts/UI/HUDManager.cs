using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject hpBar;
    public GameObject manaBar;

    public IntVariable gameScore;
    public IntVariable health;
    public IntVariable mana;

    public void Start()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text =
            "Score : " + PlayerPrefs.GetInt("Score", 0).ToString();
        hpBar.GetComponent<TextMeshProUGUI>().text =
            "HP : " + PlayerPrefs.GetInt("PlayerHealth", 10).ToString();
        manaBar.GetComponent<TextMeshProUGUI>().text =
            "Mana : " + PlayerPrefs.GetInt("PlayerMana", 50).ToString();
    }

    public void Update()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score : " + gameScore.Value.ToString();
        hpBar.GetComponent<TextMeshProUGUI>().text = "HP : " + health.Value.ToString();
        manaBar.GetComponent<TextMeshProUGUI>().text = "Mana : " + mana.Value.ToString();
    }

    public void SetScore()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score : " + gameScore.Value.ToString();
    }

    public void SetHealth()
    {
        hpBar.GetComponent<TextMeshProUGUI>().text = "HP : " + health.Value.ToString();
    }

    public void SetMana()
    {
        hpBar.GetComponent<TextMeshProUGUI>().text = "Mana : " + health.Value.ToString();
    }

    public void GameOver() { }
}
