using System;
using System.Collections;
using System.Collections.Generic;
using Core.Observer_Pattern;
using UnityEditor.SceneManagement;
using UnityEngine;

public class InGameUIController : MonoBehaviour, IObserver
{
    [SerializeField] private List<GameObject> _playerLivesList;
    private GameObject _player;
    private int _liveId = 2;
    
    [SerializeField] private GameObject _gameOverScreen;
    public void OnNotify(IEvent @event)
    {
        switch (@event)
        {
            case IEvent.OnPlayerDie:
                _gameOverScreen.SetActive(true);
                break;
            
            case IEvent.OnPlayerGetHurt:
                Debug.Log("Player hurt");
                _playerLivesList[_liveId--].SetActive(false);
                break;
        }
    }

    private void Start()
    {
        foreach (GameObject live in _playerLivesList)
        {
            live.SetActive(true);
        }
    }

}