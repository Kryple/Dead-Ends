using System;
using Core.Observer_Pattern;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class MenuController : Subject
    {
        [SerializeField] private GameObject _highScoreText;
        [SerializeField] private GameObject _settingsScreen;
        [SerializeField] private GameObject _aboutScreen;
        [SerializeField] private GameObject _quitScreen;
        
        private string p_musicVolume = "Music Volume";
        private string p_sFXVolume = "SFX Volume";
        private string p_masterVolume = "Master Volume";

        [SerializeField] private SettingsManager _settingsManager;

        private void Start()
        {
            _highScoreText.GetComponent<TextMeshProUGUI>().SetText($"High Score = {PlayerPrefs.GetInt("HighScore")}");
            
            _settingsManager.LoadVolume();
            
            
            
            _settingsScreen.SetActive(false);
            _quitScreen.SetActive(false);
            _aboutScreen.SetActive(false);
        }

        public void OpenSettingsSreen()
        {
            _settingsScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
        public void OpenAboutScreen()
        {   
            _aboutScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }

        public void OpenQuitScreen()
        {
            _quitScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }

        public void BackToMainMenu()
        {
            _settingsScreen.SetActive(false);
            _quitScreen.SetActive(false);
            _aboutScreen.SetActive(false);
            
            this.gameObject.SetActive(true);
            
        }


        public void QuitTheGame()
        {
            Application.Quit();
        }
        
    }
}