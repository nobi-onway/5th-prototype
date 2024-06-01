using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Target[] _targets;
    private float _spawnInterval = 1.0f;
    private const float SPAWN_RANGE_X = 4.0f;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager.OnStateChange += (state) =>
        {
            if(state == GameManager.GameState.Running)
            {
                StartCoroutine(SpawnTarget());
            }
        };
    }

    private IEnumerator SpawnTarget()
    {
        while(_gameManager.State == GameManager.GameState.Running)
        {
            yield return new WaitForSeconds(_spawnInterval / _gameManager.difficulty);
            int randomIndex = Random.Range(0, _targets.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-SPAWN_RANGE_X, SPAWN_RANGE_X), transform.position.y, transform.position.z);
            Instantiate(_targets[randomIndex], spawnPos, Quaternion.identity);
        }
    }
}
