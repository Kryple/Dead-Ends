using System.Collections;
using System.Collections.Generic;
using Core.Observer_Pattern;
using UnityEngine;

public class AudioManager : MonoBehaviour, IObserver
{
    [SerializeField] private GameObject _musicManager;

    [SerializeField] private GameObject _sFXManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
