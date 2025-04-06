using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace YA
{
    public class MovePlayer : MonoBehaviour
    {
        [SerializeField] CameraSwitch cameraSwitch;

        private float moveSpeed = 5f;
        private float jumpForce = 2.5f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        private float groundCheckRadius = 0.2f;

        [SerializeField] CamShake camShake;

        public float Stamina = 10f;

        private Rigidbody2D rb;
        public float moveInput;
        private bool isInWater;
        private bool isGrounded;
        public bool isMoving;
        public bool isDisquised;

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
                Stamina = 10f;
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

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("water"))
            {
                isInWater = true;
            }
            if (collision.CompareTag("scene1"))
                cameraSwitch.SwitchCam(1);
            if (collision.CompareTag("scene2"))
                cameraSwitch.SwitchCam(2);
            if (collision.CompareTag("scene3"))
                cameraSwitch.SwitchCam(3);
            if (collision.CompareTag("scene4"))
                cameraSwitch.SwitchCam(4);

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