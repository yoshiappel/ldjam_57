using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YA
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] Boss boss;
        [SerializeField] GameObject bossObj;
        [SerializeField] private Image healthBar;
        [SerializeField] private TMP_Text procent;

        private float currentFill;
        private Color green = Color.green;
        private Color red = Color.red;

        private void Start()
        {
            boss.health = 100f;
        }

        private void Update()
        {
            if (bossObj == null)
            {
                bossObj = GameObject.FindGameObjectWithTag("boss");
                boss = bossObj.GetComponent<Boss>();
                boss.health = 100f;
            }
            float targetFill = boss.health / 100f;
            currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * 100f);
            healthBar.fillAmount = currentFill;
            procent.text = Mathf.RoundToInt(currentFill * 100f).ToString() + "%";
            healthBar.color = Color.Lerp(red, green, currentFill);
        }
    }
}