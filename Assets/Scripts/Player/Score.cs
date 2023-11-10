using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int currentScore { get; private set; }

    public IntVariable score;

    private void Awake()
    {
        currentScore = PlayerPrefs.GetInt("Score", 0); // Default to 0 if not found
    }

    private void Update() { }

    public void AddScore(int _value)
    {
        currentScore = Mathf.Clamp(currentScore + _value, 0, int.MaxValue);
        score.SetValue(currentScore);
        PlayerPrefs.SetInt("Score", currentScore);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        this.gameObject.SetActive(true);
        PlayerPrefs.SetInt("Score", currentScore);
    }

    public int GetCurrentScore()
    {
        return score.Value;
    }
}
