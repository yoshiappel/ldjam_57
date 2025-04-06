using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace YA
{
    public class MovePlayer : MonoBehaviour
    {
        private float moveSpeed = 5f;
        private float jumpForce = 2f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        private float groundCheckRadius = 0.2f;

        private float fallMultiplier = 2.5f;

        [SerializeField] CamShake camShake;

        public float Stamina = 10f;

        private Rigidbody2D rb;
        private float moveInput;
        private bool isInWater;
        private bool isGrounded;
        public bool isMoving;
        public bool isDisquised;

        private bool isAlreadyGrounded;

        private Coroutine disquisingCoroutine;
        private bool isTryingToDisguise = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && !isDisquised && !isMoving && isGrounded && disquisingCoroutine == null)
            {
                disquisingCoroutine = StartCoroutine(Disquising());
            }

            if (Input.GetKeyUp(KeyCode.F) && isTryingToDisguise)
            {
                StopCoroutine(disquisingCoroutine);
                disquisingCoroutine = null;
                isTryingToDisguise = false;
                Debug.Log("Stopped trying to disguise.");
            }

            if (isDisquised && isMoving)
            {
                isDisquised = false;
                Debug.Log("Disguise broken due to movement!");
            }

            moveInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && isInWater && Stamina !> 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                Stamina -= 2f;
            } 
            else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if (isGrounded)
            {
                if (!isAlreadyGrounded && Stamina < 10f)
                {
                    isAlreadyGrounded = true;
                    camShake.start = true;
                }
                Stamina = 10f;
            }
            if (!isGrounded)
            {
                isAlreadyGrounded = false;
            }
            if (rb.velocity.x != 0)
            {
                isMoving = true;
                isDisquised = false;
            }
            else if (rb.velocity.x == 0) {
                isMoving = false;
            }


        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("water")) 
            {
                isInWater = true;
            }
            if (collision.CompareTag("enemy") && !isDisquised)
            {
                Debug.Log("Hit");
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
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
        }

        private IEnumerator Disquising()
        {
            isTryingToDisguise = true;

            float disguiseTimer = 0f;
            float disguiseTime = 3f;

            Debug.Log("Started trying to disguise...");

            while (disguiseTimer < disguiseTime)
            {
                if (isMoving || !isGrounded || !Input.GetKey(KeyCode.F))
                {
                    Debug.Log("Disguise cancelled.");
                    isTryingToDisguise = false;
                    disquisingCoroutine = null;
                    yield break;
                }

                disguiseTimer += Time.deltaTime;
                yield return null;
            }

            isDisquised = true;
            Debug.Log("Disguised!");
            isTryingToDisguise = false;
            disquisingCoroutine = null;
        }

    }
}