using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _highScoreText;
        [SerializeField] private AudioClip _mainMenuBackground;
        [SerializeField] private GameObject _settingsScreen;
        
        private string p_musicVolume = "Music Volume";
        private string p_sFXVolume = "SFX Volume";
        private string p_masterVolume = "Master Volume";

        private void Start()
        {
            _highScoreText.GetComponent<TextMeshProUGUI>().SetText($"High Score = {PlayerPrefs.GetInt("HighScore")}");
            
            Debug.Log("music: " + PlayerPrefs.GetFloat(p_musicVolume));
            Debug.Log("master: " + PlayerPrefs.GetFloat(p_masterVolume));
            Debug.Log("sfx: " + PlayerPrefs.GetFloat(p_sFXVolume));
        }

        public void OpenSettingsSreen()
        {
            _settingsScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }

        public void BackToMainMenu()
        {
            _settingsScreen.SetActive(false);
            
            this.gameObject.SetActive(true);
            
        }
    }
}