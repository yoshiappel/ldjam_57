using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class SpawnPlr : MonoBehaviour
    {
        [SerializeField] GameObject plr;
        // Start is called before the first frame update
        void Start()
        {
            Instantiate(plr, transform.position, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}