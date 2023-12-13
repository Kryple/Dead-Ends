using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _darkBatPrefab;
    private float _spawnRadius = 10f;
    private GameObject _player;
    private float _coolDownTime = 1f;
        
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolDownTime);
            float angle = Random.Range(0, 2 * Mathf.PI);
            float x = Mathf.Cos(angle) * _spawnRadius;
            float y = Mathf.Sin(angle) * _spawnRadius;
            
            Vector3 spawnPosition = new Vector3(x, y, 0) + _player.transform.position;
            GameObject.Instantiate(_darkBatPrefab, spawnPosition, Quaternion.identity);
        }
    }
}