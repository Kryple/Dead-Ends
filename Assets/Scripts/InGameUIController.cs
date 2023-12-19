using System;
using System.Collections;
using Core.Observer_Pattern;
using UnityEngine;

public class InGameUIController : MonoBehaviour, IObserver
{
    [SerializeField] private GameObject _gameOverScreen;
    public void OnNotify(IEvent @event)
    {
        if (@event == IEvent.OnPlayerDie)
        {
            _gameOverScreen.SetActive(true);
            // Time.timeScale = 0;
        }
    }
}