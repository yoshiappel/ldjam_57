using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YA
{
    public class SpawnBoss : MonoBehaviour
    {
        private GameObject plr;
        private MovePlayer movePlayer;

        [SerializeField] GameObject boss;
        public Transform bossSpawn;

        [SerializeField] GameObject point1;
        [SerializeField] GameObject point2;

        [SerializeField] Transform spawnPoint1;
        [SerializeField] Transform spawnPoint2;

        bool oneTimePlease;


        private void Start()
        {

        }

        private void Update()
        {
            if (plr == null)
            {
                plr = GameObject.FindGameObjectWithTag("player");
                movePlayer = plr.GetComponent<MovePlayer>();
                movePlayer.isBossFight = true;
            }
            if (movePlayer != null)
            {
                if (!oneTimePlease)
                {
                    oneTimePlease = true;
                    Debug.Log("spawnboss");
                    Instantiate(boss, bossSpawn.position, Quaternion.identity);
                }
            }
        }
    }
}