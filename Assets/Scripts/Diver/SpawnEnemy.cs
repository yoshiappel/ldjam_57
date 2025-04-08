using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class SpawnEnemy : MonoBehaviour
    {

        [SerializeField] GameObject enemy;
        [SerializeField] Transform spawner;
        // Start is called before the first frame update
        void Start()
        {
            Instantiate(enemy, spawner.position, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
    
        }
    }
}