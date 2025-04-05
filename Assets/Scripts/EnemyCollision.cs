using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class EnemyCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Touched: " + collision.gameObject.tag);
            if (collision.gameObject.CompareTag("enemy"))
            {
                Debug.Log("Hit!");
            }
        }
    }
}