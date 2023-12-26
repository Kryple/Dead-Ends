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
        [SerializeField] private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            
            
            _highScoreText.GetComponent<TextMeshProUGUI>().SetText($"High Score = {PlayerPrefs.GetInt("HighScore")}");

            _audioSource.clip = _mainMenuBackground;
            _audioSource.Play();
        }
    }
}