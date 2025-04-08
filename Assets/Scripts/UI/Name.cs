using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YA
{
    public class Name : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;

        [SerializeField] string tempPlayerName;

        public bool isLevi = false;

        [SerializeField] GameObject normalText;
        [SerializeField] GameObject leviText;

        bool hasBeenDead;


        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void ReadInput()
        {
            tempPlayerName = inputField.text;
            if (tempPlayerName == "Levi" || tempPlayerName == "levi")
            {
                isLevi = true;
            }
            Debug.Log("player name is: " + tempPlayerName);
            SceneManager.LoadScene(1);
        }

        private void Update()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            string sceneName = currentScene.name;

            if (sceneName == "GameOver")
            {
                hasBeenDead = true;
                if (isLevi)
                {
                    leviText.SetActive(true);
                    normalText.SetActive(false);
                }
                else
                {
                    leviText.SetActive(false);
                    normalText.SetActive(true);
                }
            
            }
            if (sceneName == "TitleScreen" && hasBeenDead)
            {
                Destroy(this.gameObject);
            }
        }
    }
}