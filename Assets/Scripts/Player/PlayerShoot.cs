using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private float attackCooldown = 5f;
    public int manaCost = 10;

    private Transform firePoint;

    [SerializeField]
    private GameObject[] fireballs;
    private float cooldownTimer = Mathf.Infinity;

    public Mana playerMana;

    private Animator anim;

    private void Awake()
    {
        anim =GetComponent<Animator>();
        playerMana =GetComponent<Mana>();
        firePoint = this.gameObject.transform.GetChild(0).GetComponent<Transform>();

        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            Transform fireballsHolder = parentTransform.Find("ProjectileHolder");

            if (fireballsHolder != null)
            {
                int childCount = fireballsHolder.childCount;
                fireballs = new GameObject[childCount];

                for (int i = 0; i < childCount; i++)
                {
                    fireballs[i] = fireballsHolder.GetChild(i).gameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMana.currentMana>=10)
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("shoot");
        cooldownTimer = 0;
        playerMana.SpentMana(manaCost);

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()]
            .GetComponent<Projectile>()
            .SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
