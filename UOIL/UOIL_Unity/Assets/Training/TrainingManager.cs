using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TrainingManager : MonoBehaviour
{
    public bool IsHit = false;
    private int _tryCount = 0;
    private float _resultPos;
    private int _spawnSeed = 1;
    private const float SPAWN_INITIAL = 1f;
    private const float SPAWN_PADDING = 1.33f;

    public event Action<int> OnCountChanged;

    // プレイヤーと敵
    [SerializeField]
    private GameObject _playerObj;
    [SerializeField]
    private GameObject _enemyObj;

    // UI
    [SerializeField]
    private TextMeshProUGUI _resultText;
    [SerializeField]
    private TextMeshProUGUI _countText;
    [SerializeField]
    private TextMeshProUGUI _seedText;

    // json書き込み
    [SerializeField]
    private JsonSerializationTest _jsonSerializationTest;

    void Start()
    {
        OnCountChanged += Restart;
    }

    public void AddCount(float currentPos)
    {
        IsHit = true;
        _resultPos = (float)Math.Round(currentPos, 3);

        UpdateUI();
        OnCountChanged?.Invoke(_tryCount);
        _tryCount ++;
        if(_tryCount > 100) _tryCount = 100;
    }

    // リセット処理
    void Restart(int currentCount)
    {
        _jsonSerializationTest.OnSave(_spawnSeed,_resultPos);
        // プレイヤーの位置リセット
        _playerObj.transform.position = new Vector3(0,_playerObj.transform.position.y,_playerObj.transform.position.z);
        // 敵の位置リセット
        _spawnSeed = UnityEngine.Random.Range(1, 6);
        float spawnPoint = SPAWN_INITIAL + (_spawnSeed-1) * SPAWN_PADDING;
        float addPos = UnityEngine.Random.Range(0.01f, SPAWN_PADDING);
        spawnPoint += addPos;
        _enemyObj.transform.position = new Vector3(spawnPoint,_enemyObj.transform.position.y,_enemyObj.transform.position.z);
        if(currentCount > 99) return;
        IsHit = false;
    }

    void UpdateUI()
    {
        _resultText.text = "Result : " + _resultPos.ToString("0.00");
        _countText.text = "Try : " + _tryCount.ToString("000");
        _seedText.text = "SEED : " + _spawnSeed;
    }
}
