using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool IsHit = false;
    private float _resultPos;
    private int _spawnSeed = 1;
    private const float SPAWN_INITIAL = 1f;
    private const float SPAWN_PADDING = 1.33f;

    // プレイヤーと敵
    [SerializeField]
    private GameObject _playerObj;
    [SerializeField]
    private GameObject _enemyObj;

    // UI
    [SerializeField]
    private TextMeshProUGUI _resultText;
    [SerializeField]
    private TextMeshProUGUI _seedText;
    [SerializeField]
    private GameObject _resultPanel;

    private int _state = 0;

    void Start()
    {
        Launch();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(_state);
            switch(_state)
            {
                case 0:
                    _state = 1;
                    IsHit = false;
                    break;
                case 1:
                    break;
                case 2:
                    Launch();
                    break;
            }
        }
    }

    public void Launch()
    {
        _state = 0;
        _resultPanel.SetActive(false);
        // プレイヤーの位置リセット
        _playerObj.transform.position = new Vector3(0,_playerObj.transform.position.y,_playerObj.transform.position.z);
        // 敵の位置リセット
        _spawnSeed = UnityEngine.Random.Range(1, 6);
        float spawnPoint = SPAWN_INITIAL + (_spawnSeed-1) * SPAWN_PADDING;
        float addPos = UnityEngine.Random.Range(0.01f, SPAWN_PADDING);
        spawnPoint += addPos;
        _enemyObj.transform.position = new Vector3(spawnPoint,_enemyObj.transform.position.y,_enemyObj.transform.position.z);
        _seedText.text = "TrySeed : " + _spawnSeed;
    }

    public void HitEnemy(float currentPos)
    {
        _state = 2;
        IsHit = true;
        _resultPos = (float)Math.Round(currentPos, 3);
        _resultPanel.SetActive(true);
        _resultText.text = "Result : " + _resultPos.ToString("0.00");
    }

}
