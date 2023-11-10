using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public CanvasGroup c;

    IEnumerator Fade()
    {
        for (float alpha = 1f; alpha >= -0.05f; alpha -= 0.05f)
        {
            c.alpha = alpha;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
        // once done, go to next scene
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
    }

    public void restartGame()
    {
        StartCoroutine(Fade());
    }
}
