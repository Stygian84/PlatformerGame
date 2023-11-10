using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField]
    private Transform leftEdge;

    [SerializeField]
    private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField]
    private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField]
    private float speed = 5;
    private Vector3 initScale;
    private bool movingLeft = true;

    [Header("Idle Behaviour")]
    [SerializeField]
    private float idleDuration = 1;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField]
    private Animator anim;

    private void Awake()
    {
        leftEdge = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        rightEdge = this.gameObject.transform.GetChild(1).GetComponent<Transform>();
        enemy = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        anim = this.gameObject.transform.GetChild(2).GetComponent<Animator>();
    }

    private void Start()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                changeDirection();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                changeDirection();
            }
        }
    }

    private void changeDirection()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        enemy.localScale = new Vector3(
            Mathf.Abs(initScale.x) * _direction,
            initScale.y,
            initScale.z
        );
        enemy.position = new Vector3(
            enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y,
            enemy.position.z
        );
    }
}
