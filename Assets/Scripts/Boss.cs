using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public GameObject rangePrefab;
    public GameObject meleePrefab;
    public GameObject diePrefab;
    public int numberOfObjects = 8;
    private Animator anim;
    private float lifeTime;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemies", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("die"))
        {
            lifeTime += Time.deltaTime;

            if (lifeTime > 4)
            {
                DestroyAllChildren();

                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void DieAnim()
    {
        StartCoroutine(GenerateObjects());
    }

    private IEnumerator GenerateObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomX = Random.Range(-3, 2);
            float randomY = Random.Range(-3, 2);

            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            GameObject newObject = Instantiate(diePrefab, transform);
            newObject.transform.position += randomPosition;
            GameObject newObject2 = Instantiate(diePrefab, transform);
            newObject2.transform.position += randomPosition;

            // Yield and wait for a brief moment before generating the next object
            yield return new WaitForSeconds(0.2f);
        }
    }

    void DestroyAllChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    void SpawnEnemies()
    {
        GameObject newObject = Instantiate(rangePrefab, transform, true);
        newObject.transform.position += new Vector3(2.5f, 11, 0);
        newObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
