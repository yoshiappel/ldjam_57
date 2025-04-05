using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace YA
{
    public class TitleScreen : MonoBehaviour
    {
        // subtitle animations
        [SerializeField] TMP_Text subTitle;

        private Vector2 startSubTitleScale = new Vector2(1, 1);
        private Vector2 minDesiredSubTitleScale = new Vector2(0.8f, 0.8f);
        private Vector2 maxDesiredSubTitleScale = new Vector2(1.2f, 1.2f);

        [SerializeField] GameObject SettingsSection;
        [SerializeField] GameObject HowToPlaySection;
        [SerializeField] GameObject NameSection;

        [SerializeField] GameObject SettingsBtn;
        [SerializeField] GameObject HowToPlayBtn;
        [SerializeField] GameObject StartGameBtn;

        [SerializeField] GameObject Main;

        private void Start()
        {
            AnimSubTitle();
        }

        public void OnStartGamePressed()
        {
            Debug.Log("OnStartPressed");
            Main.SetActive(false);
            NameSection.SetActive(true);
        }

        public void OnSettingPressed()
        {
            Debug.Log("OnSettingPressed");
            Main.SetActive(false);
            SettingsSection.SetActive(true);
        }

        public void OnHTPPressed()
        {
            Debug.Log("OnHTPPressed");
            Main.SetActive(false);
            HowToPlaySection.SetActive(true);
        }

        public void Quit()
        {
            Debug.Log("quitting game");
            Application.Quit();
        }

        public void ESC()
        {
            Main.SetActive(true);
            HowToPlaySection.SetActive(false);
            SettingsSection.SetActive(false);
            NameSection.SetActive(false);
        }

        private void AnimSubTitle()
        {
            subTitle.transform.LeanScale(maxDesiredSubTitleScale, 1.5f)
            .setOnComplete(() =>
             {
                 subTitle.transform.LeanScale(minDesiredSubTitleScale, 1.5f)
                 .setOnComplete(() =>
                 {
                     subTitle.transform.LeanScale(startSubTitleScale, 1.5f);
                     AnimSubTitle();
                 });
             });
        }
    }
}