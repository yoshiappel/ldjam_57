using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YA {
    public class Boss : MonoBehaviour
    {
        [SerializeField] GameObject beamPrefab;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject starAttack;

        [SerializeField] float beamSpeed = 10f;
        [SerializeField] float predictionTime = 0.5f;

        [SerializeField] Transform player;
        [SerializeField] Rigidbody2D playerRb;

        [SerializeField] float fireInterval = 3f;
        private float fireCooldown;

        [SerializeField] float chargeTime = 1f;
        [SerializeField] GameObject chargeEffect;

        public float health = 100f;

        [SerializeField] bool phaseTwoStarted = false;

        [SerializeField] float moveSpeed = 2f;
        [SerializeField] Transform[] movePoints;
        private int currentPointIndex = 0;

        bool fase3started;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("player").transform;
            playerRb = player.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (!phaseTwoStarted && health <= 75f)
            {
                phaseTwoStarted = true;
            }
            if (phaseTwoStarted && health <= 25 && !fase3started)
            {
                fase3started = true;
                moveSpeed = 3f;
                StartCoroutine(StartStarAttack());
            }
            if (health <= 0f)
            {
                Destroy(gameObject);
                health = 0f;
                GameWon();
            }
            if (phaseTwoStarted)
            {
                MoveBetweenPoints();
            }

            fireCooldown -= Time.deltaTime;

            if (fireCooldown <= 0f)
            {
                ShootPredictiveBeam();
                fireCooldown = fireInterval;
            }
        }

        private IEnumerator StartStarAttack()
        {
            starAttack.gameObject.SetActive(true);

            BoxCollider2D[] colliders = starAttack.GetComponentsInChildren<BoxCollider2D>();

            yield return new WaitForSeconds(2f);

            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = true;
            }

        }

        private void MoveBetweenPoints()
        {
            if (movePoints.Length == 0) return;

            Transform targetPoint = movePoints[currentPointIndex];
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                currentPointIndex = (currentPointIndex + 1) % movePoints.Length;
            }
        }

        void ShootPredictiveBeam()
        {
            StartCoroutine(ChargeAndFireBeam());
        }

        IEnumerator ChargeAndFireBeam()
        {
            GameObject charge = Instantiate(chargeEffect, firePoint.position, Quaternion.identity, firePoint);

            yield return new WaitForSeconds(chargeTime);

            Destroy(charge); 

            Vector2 predictedPos = (Vector2)player.position + playerRb.velocity * predictionTime;
            Vector2 direction = (predictedPos - (Vector2)firePoint.position).normalized;

            GameObject beam = Instantiate(beamPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = beam.GetComponent<Rigidbody2D>();
            rb.velocity = direction * beamSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            beam.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void GameWon()
        {
            SceneManager.LoadScene(3);
        }

    }
}
