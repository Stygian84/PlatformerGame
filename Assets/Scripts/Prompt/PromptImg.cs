using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PromptImg : MonoBehaviour
{
    public GameObject key;
    public bool inside = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            inside = true;
            if (coll.gameObject.transform.Find("Key") == null)
            {
                //No key
                transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        inside = false;
        if (key.activeSelf)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
