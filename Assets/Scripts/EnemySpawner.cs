using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _darkBatPrefab;
    private float _spawnRadius = 10f;
    [SerializeField] GameObject _player;
    private float _coolDownTime = 10f;
        
    private void Start()
    {
        // _player = GameObject.FindWithTag("Player");
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            
            float angle = Random.Range(0, 2 * Mathf.PI);
            float x = Mathf.Cos(angle) * _spawnRadius;
            float y = Mathf.Sin(angle) * _spawnRadius;
            
            Vector3 spawnPosition = new Vector3(x, y, 0) + _player.transform.position;
            GameObject tmp = GameObject.Instantiate(_darkBatPrefab, spawnPosition, Quaternion.identity);
            tmp.transform.parent = this.transform;
            
            yield return new WaitForSeconds(_coolDownTime);
        }
    }
}