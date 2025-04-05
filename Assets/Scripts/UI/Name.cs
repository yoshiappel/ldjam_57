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
    }
}