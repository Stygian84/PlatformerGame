using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public float direction;
    private float lifetime;
    private bool hit;
    private CircleCollider2D circleCollider;
    private Animator anim;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit)
            return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
        (collision.name == "Player")
        || (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        || (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        || (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        )
        {
            hit = true;
            anim.SetTrigger("explode");

            circleCollider.enabled = false;
            if (collision.name == "Player")
            {
                collision.GetComponent<Health>().TakeDamage(1);
            }
        }
        if (collision.tag == "PlayerMelee")
        {
            direction *= -1;
        }
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        circleCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(
            localScaleX,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
