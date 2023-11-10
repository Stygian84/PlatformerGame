using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField]
    private LayerMask jumpableGround;

    [SerializeField]
    private LayerMask wallLayer;
    private float wallTime;

    private float dirX = 0f;

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float jumpForce = 14f;

    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling,
        onwall
    };

    [SerializeField]
    private AudioClip jumpSound;
    private AudioSource jumpSoundEffect;

    [Header("Multiple Jump")]
    [SerializeField]
    private int multipleJumps;
    private int jumpCounter;

    [Header("Wall Jump")]
    [SerializeField]
    private float wallJumpX;

    [SerializeField]
    private float wallJumpY;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        jumpSoundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (dirX > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (dirX < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        UpdateAnimationState();
        
        //Controllable Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
        if (onWall())
        {
            rb.gravityScale = 0;
            rb.AddForce(Vector2.down, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * transform.localScale.x * 15, ForceMode2D.Impulse);
            //rb.velocity = Vector2.zero;
        }
        else
        {
            rb.gravityScale = 3;
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
        if (IsGrounded())
        {
            jumpCounter = multipleJumps;
        }
    }

    private void Jump()
    {
        PlayJumpSound();
        if (!onWall() && jumpCounter <= 0)
            return;
        if (onWall())
        {
            WallJump();
        }
        else
        {
            if (jumpCounter > 0) //If we have extra jumps then jump and decrease the jump counter
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCounter--;
            }
        }
        
    }

    private void WallJump()
    {
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        // rb.AddForce(Vector2.up * wallJumpY, ForceMode2D.Impulse);
        // rb.AddForce(Vector2.right * -transform.localScale.x * wallJumpX, ForceMode2D.Impulse);
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        if (onWall())
        {
            state = MovementState.onwall;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            jumpableGround
        );
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            new Vector2(transform.localScale.x, 0),
            0.1f,
            wallLayer
        );
        return raycastHit.collider != null;
    }

    public void PlayJumpSound()
    {
        jumpSoundEffect.PlayOneShot(jumpSound);
    }
}
