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

        private void Start()
        {
            _highScoreText.GetComponent<TextMeshProUGUI>().SetText($"High Score = {PlayerPrefs.GetInt("HighScore")}");
        }
    }
}