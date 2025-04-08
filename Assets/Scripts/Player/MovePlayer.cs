using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YA
{
    public class MovePlayer : MonoBehaviour
    {
        [SerializeField] CameraSwitch cameraSwitch;

        private float moveSpeed = 5f;
        private float jumpForce = 2.5f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        private float groundCheckRadius = 0.3f;

        bool one = false;

        [SerializeField] CamShake camShake;

        public float Stamina = 10f;

        private Rigidbody2D rb;
        public float moveInput;
        private bool isInWater;
        public bool isGrounded;
        public bool isMoving;
        public bool isDisquised;

        private Coroutine disquisingCoroutine;
        private bool isTryingToDisguise = false;

        [SerializeField] GameObject projectilePrefab;
        [SerializeField] Transform firePoint;
        [SerializeField] float fireRate = 0.2f; 

        private float fireCooldown;

        public bool isBossFight = false;


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
                if (!one)
                {
                    one = true;
                    camShake.start = true;
                }
                Stamina = 10f;
            }
            else if (!isGrounded) 
            {
                one = false;
            }
            if (rb.velocity.x != 0)
            {
                isMoving = true;
                isDisquised = false;
            }
            else if (rb.velocity.x == 0) {
                isMoving = false;
            }

            AimAtMouse();

            if (Input.GetMouseButton(0) && fireCooldown <= 0f)
            {
                Fire();
                fireCooldown = fireRate;
            }

            if (fireCooldown > 0f)
            {
                fireCooldown -= Time.deltaTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("water")) 
            {
                isInWater = true;
            }
            if (collision.CompareTag("enemy") || collision.CompareTag("attack") || collision.CompareTag("boss"))
            {
                Hit();
            }
            if (collision.CompareTag("bossfight"))
            {
                SceneManager.LoadScene(2);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("water"))
            {
                isInWater = true;
            }
            // sadly couldnt use this bc of time
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

        private void AimAtMouse()
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void Fire()
        {
            if (isBossFight)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

                GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

                PlayerProjectile projectile = proj.GetComponent<PlayerProjectile>();
                projectile.SetDirection(direction);
            }
        }

        private void Hit()
        {
            Debug.Log("hit");
            SceneManager.LoadScene(4);
        }

    }
}