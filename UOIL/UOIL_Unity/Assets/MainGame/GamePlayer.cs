using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    private bool _isHit => _gameManager.IsHit;
    [SerializeField]
    private float _speed = 2;

    // Update is called once per frame
    void Update()
    {
        if(_isHit) return;
        transform.position = transform.position + new Vector3(_speed, 0, 0) * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_isHit)
        {
            _gameManager.HitEnemy(transform.position.x);
        }
    }
}
