using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float _moveDirection = 1.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private bool enableDoubleJump = true;

    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField]
    private bool isGrounded;
    private bool canDoubleJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed * _moveDirection, rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if (enableDoubleJump)
                {
                    canDoubleJump = true;
                }
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void EnablePlayerPhysics(float gravityDirection, float jumpDirection, float moveDir)
    {
        Debug.Log("Physics Enabled");
        jumpForce = jumpDirection;
        if (rb != null)
        {
            rb.gravityScale = gravityDirection;
            _moveDirection = moveDir;
        }

        if (coll != null)
        {
            coll.enabled = true;
        }
    }

    public void DisablePlayerPhysics(float moveDir)
    {
        Debug.Log("Physics Disabled");
        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            _moveDirection = moveDir;
        }

        if (coll != null)
        {
            coll.enabled = false;
        }
    }
}
