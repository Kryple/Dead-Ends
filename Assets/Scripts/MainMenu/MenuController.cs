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
        

        private void Start()
        {
            _highScoreText.GetComponent<TextMeshProUGUI>().SetText($"High Score = {PlayerPrefs.GetInt("HighScore")}");

            _settingsScreen.SetActive(false);
        }

        public void OpenSettingsSreen()
        {
            _settingsScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}