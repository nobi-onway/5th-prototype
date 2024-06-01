using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rb;
    private const float MIN_FORCE = 12.0f;
    private const float MAX_FORCE = 16.0f;
    private const float TORQUE_RANGE = 10.0f;
    [SerializeField]
    private ParticleSystem _explosionParticle;

    private GameManager _gameManager;

    [SerializeField]
    private int _score;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Toss();
    }

    private void OnMouseDown()
    {
        if (_gameManager.State != GameManager.GameState.Running) return;
        Destroy(gameObject);
        _gameManager.UpdateScore(_score);
        Instantiate(_explosionParticle, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if(transform.gameObject.CompareTag("Good"))
        {
            _gameManager.GameOver();
        }
    }

    private void Toss()
    {
        _rb.AddForce(Vector3.up * Random.Range(MIN_FORCE, MAX_FORCE), ForceMode.Impulse);
        _rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    private float RandomTorque()
    {
        return Random.Range(-TORQUE_RANGE, TORQUE_RANGE);
    }
}
