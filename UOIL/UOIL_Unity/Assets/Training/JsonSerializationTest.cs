using System.IO;
using UnityEngine;
using System.Collections.Generic;

// JSON形式のデータ読み書きテスト
public class JsonSerializationTest : MonoBehaviour
{
    // 位置データ
    [System.Serializable]
    private struct PositionData
    {
        public int SEED;
        public float RESULT;
    }

    // ラッパークラス
    [System.Serializable]
    private class PositionDataWrapper
    {
        public List<PositionData> dataList = new List<PositionData>();
    }

    PositionDataWrapper dataWrapper = new PositionDataWrapper();

    // ファイルパス
    private string _dataPath;
    private void Awake()
    {
        // ファイルのパスを計算
        _dataPath = Path.Combine(Application.dataPath, "data.json");
    }

    // JSON形式にシリアライズしてセーブ
    public void OnSave(int seed, float result)
    {
        // 新しいPositionDataを作成してリストに追加
        PositionData data = new PositionData
        {
            SEED = seed,
            RESULT = result
        };
        dataWrapper.dataList.Add(data);

        // 100個のデータがたまったらJSON形式にシリアライズして保存する
        if (dataWrapper.dataList.Count >= 100)
        {
            Debug.Log("save");
            // JSON形式にシリアライズ
            var json = JsonUtility.ToJson(dataWrapper, true);

            // JSONデータをファイルに保存
            File.WriteAllText(_dataPath, json);

            // dataListをクリアして新しいデータをためるために初期化する
            dataWrapper.dataList.Clear();
        }
    }

    // JSON形式をロードしてデシリアライズ
    private void OnLoad()
    {
        // 念のためファイルの存在チェック
        if (!File.Exists(_dataPath)) return;

        // JSONデータとしてデータを読み込む
        var json = File.ReadAllText(_dataPath);

        // JSON形式からオブジェクトにデシリアライズ
        dataWrapper = JsonUtility.FromJson<PositionDataWrapper>(json);

        // 最後に保存されたPositionDataのデータを取得してTransformにセット
        if (dataWrapper.dataList.Count > 0)
        {
            PositionData lastData = dataWrapper.dataList[dataWrapper.dataList.Count - 1];
            Vector3 newPosition = transform.position;
            newPosition.x = lastData.SEED;
            transform.position = newPosition;
        }
    }
}
