using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class MovePlayer : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;

        [SerializeField] float Stamina = 10f;

        private Rigidbody2D rb;
        private float moveInput;
        private bool isInWater;
        private bool isGrounded;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Left / Right input
            moveInput = Input.GetAxisRaw("Horizontal");

            // Jump input for in water
            if (Input.GetKeyDown(KeyCode.Space) && isInWater && Stamina !> 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                Stamina -= 2f;
            } // for on ground
            else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if (isGrounded)
            {
                Stamina = 10f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("water")) 
            {
                isInWater = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("water"))
            {
                isInWater = false;
            }
        }

        private void FixedUpdate()
        {
            // Apply horizontal movement
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            // Ground check using overlap circle
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }
}