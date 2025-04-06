using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class AnimationPlr : MonoBehaviour
    {
        [SerializeField] MovePlayer movePlayer;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {

            // Check movement direction
            if (movePlayer.isMoving)
            {
                if (movePlayer.moveInput < 0)
                {
                    animator.Play("octoL");
                }
                else if (movePlayer.moveInput > 0)
                {
                    animator.Play("octoR");
                }
                else
                {
                    animator.Play("octoidle");
                }
            }
        }

    }
}