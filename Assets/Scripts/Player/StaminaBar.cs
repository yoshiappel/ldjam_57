using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YA
{
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] MovePlayer movePlayer;
        [SerializeField] private Image staminaBar;

        private float currentFill;

        private void Update()
        {
            float targetFill = movePlayer.Stamina / 10f;
            currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * 10f);
            staminaBar.fillAmount = currentFill;
        }
    }
}