using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class PlayerProjectile : MonoBehaviour
    {
        [SerializeField] GameObject bossObj;
        [SerializeField] Boss boss;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 2f;

        private Vector2 direction;

        public void SetDirection(Vector2 dir)
        {
            direction = dir.normalized;
        }

        void Start()
        {
            bossObj = GameObject.FindGameObjectWithTag("boss");
            boss = bossObj.GetComponent<Boss>();
            Destroy(gameObject, lifeTime);
        }

        void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("boss"))
            {
                Debug.Log("Enemy hit!");
                boss.health -= .5f;
                Destroy(gameObject);
            }
        }
    }
}