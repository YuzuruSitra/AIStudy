using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private TrainingManager _trainingManager;

    private bool _isHit => _trainingManager.IsHit;
    [SerializeField]
    private float _speed = 5;

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
            _trainingManager.AddCount(transform.position.x);
        }
    }
}
