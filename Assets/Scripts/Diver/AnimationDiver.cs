using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class AnimationDiver : MonoBehaviour
    {
        [SerializeField] Enemy enemy;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            animator.Play("divermove");
        }
    }
}