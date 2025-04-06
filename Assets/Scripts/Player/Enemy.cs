using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace YA
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] MovePlayer movePlayer;

        [SerializeField] Transform player;
        [SerializeField] float moveSpeed = 2f;
        [SerializeField] float followRange = 10f;
        [SerializeField] float stopDistance = 0.5f;

        [SerializeField] Transform pointA;
        [SerializeField] Transform pointB;
        private Transform currentTarget;

        [SerializeField] GameObject exclamationMark;

        private void Start()
        {
            currentTarget = pointA;
            exclamationMark.SetActive(false);
        }

        private void Update()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= followRange && distanceToPlayer > stopDistance && !movePlayer.isDisquised)
            {
                HandleExclamationMark(true);
                FollowPlayer();
            }
            else
            {
                HandleExclamationMark(false);
                StartPatrol();
            }
        }

        private void FollowPlayer()
        {
            MoveTowards(player.position);
            FlipSprite(player.position.x);
        }

        private void StartPatrol()
        {
            if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                if (currentTarget == pointA)
                {
                    currentTarget = pointB;
                }
                else
                {
                    currentTarget = pointA;
                }
            }

            MoveTowards(currentTarget.position);
            FlipSprite(currentTarget.position.x);
        }

        private void MoveTowards(Vector2 target)
        {
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }

        private void FlipSprite(float targetX)
        {
            Vector3 scale = transform.localScale;
            if (targetX < transform.position.x)
            {
                scale.x = -1;
            }
            else
            {
                scale.x = 1;
            }
            transform.localScale = scale;
        }

        private void HandleExclamationMark(bool state)
        {
            exclamationMark.SetActive(state);
        }
    }
}