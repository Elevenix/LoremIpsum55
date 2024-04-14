using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField] private float radiusGroundCheck = .2f;
    [SerializeField]
    private LayerMask groundLayer;
    private float horizontal;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpingPower;
    [SerializeField]
    private Animator visualPlayer;

    private bool isPlayerFacingRight = true;
    private Collider2D myCollider;

    private RandomSounds randomSounds;


    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        GameManager.Instance.AddPlayer(this.gameObject);

        randomSounds = GetComponent<RandomSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isPlayerFacingRight && horizontal > 0f)
        {
            Flip();
        } else if (isPlayerFacingRight && horizontal < 0f)
        {
            Flip();
        }

        if(rb.velocity.x == 0)
        {
            visualPlayer.SetBool("isMoving", false);
        } 
        else
        {
            visualPlayer.SetBool("isMoving", true);
        }

        if(rb.velocity.y <= -1f && !IsGrounded())
        {
            visualPlayer.SetBool("isFalling", true);
        } 
        else
        {
            visualPlayer.SetBool("isFalling", false);
        }
    }

    private bool IsGrounded()
    {
        Collider2D col = Physics2D.OverlapCircle(groundCheck.position, radiusGroundCheck, groundLayer);
        return col && col != myCollider;
    }

    private void Flip()
    {
        isPlayerFacingRight = !isPlayerFacingRight;
        Vector3 localScale = visualPlayer.transform.localScale;
        localScale.x *= -1f;
        visualPlayer.transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            randomSounds.PlaySound("Jump");
            visualPlayer.SetTrigger("Jump");
        }

        if (context.canceled && rb.velocity.y > 0.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, radiusGroundCheck);
    }
}
