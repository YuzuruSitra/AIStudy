import json
import tkinter as tk
from tkinter import filedialog
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

import numpy as np
from tensorflow import keras

 
# 帰納的学習の処理

def dead_predict(receve_seed):
    # モデルの構築
    model = keras.Sequential([
        keras.layers.Dense(64, activation='relu', input_shape=(1,)),
        keras.layers.Dense(32, activation='relu'),
        keras.layers.Dense(1)
    ])

    # モデルのコンパイル
    model.compile(optimizer='adam', loss='mean_squared_error')

    # 学習データのJSONファイルを読み込む
    with open('UOIL_Unity/Assets/data.json', 'r') as file:
        data = json.load(file)
    dataList = data["dataList"]

    # 学習データを作成
    X_train = np.array([item["SEED"] for item in dataList])
    y_train = np.array([item["RESULT"] for item in dataList])

    # 学習
    model.fit(X_train, y_train, epochs=1000, batch_size=1, verbose=0)

    # 予測したいシード値をコンソールから入力
    seed_value_to_predict = float(receve_seed)

    # 予測
    predicted_x = model.predict(np.array([seed_value_to_predict]))[0][0]
    return predicted_x


# jsonの監視、GUIの描画処理

# 前回のファイルの内容を保持する変数
previous_content = None

# 変更があった場合に行いたい処理を定義する関数
def on_file_change(event):
        
    global previous_content
    try:
        with open(event.src_path, 'r') as file:
            
            json_data = json.load(file)
            current_content = json.dumps(json_data, indent=4)
            if current_content != previous_content:
                # JSONファイルの内容が変わった場合のみ表示する
                json_text_box.delete(1.0, tk.END)  # テキストボックスの内容をクリア
                json_text_box.insert(tk.END, current_content)  # 新しい内容を挿入
                
                predict_pos.set('予測される死亡地点 : 計算中')
                # JSONファイルのSEED読み込み
                previous_content = current_content
                # AIによる算出
                result = dead_predict(json_data['SendAI']['SEED'])
                result = round(result,2)
                out_text = '予測される死亡地点 : {:.2f}'.format(result)  # 小数点第二位まで表示
                print(out_text)
                # テキストの変更
                predict_pos.set(out_text)
    except Exception as e:
        json_text_var.set("ファイルが変更されましたが、JSONファイルではありません.")

# ファイル選択ダイアログを開く関数
def select_file():
    filepath = filedialog.askopenfilename(filetypes=[("JSON files", "*.json")])
    if filepath:
        file_path_var.set(filepath)

# GUIアプリのウィンドウを作成
root = tk.Tk()
root.title("初めての帰納的学習")

# ファイル選択ボタンを作成
select_button = tk.Button(root, text="ファイルを選択", command=select_file)
select_button.grid(row=0, column=0, padx=10, pady=10)

# ファイルのパスを表示するラベル
file_path_var = tk.StringVar()
file_path_label = tk.Label(root, textvariable=file_path_var, wraplength=300)
file_path_label.grid(row=0, column=1, padx=50, pady=10)

# JSONファイルの内容を表示するテキストボックス
json_text_var = tk.StringVar()
json_text_var.set("ここにJSONファイルの内容が表示されます。")
json_text_box = tk.Text(root, height=10, width=40, wrap=tk.WORD)
json_text_box.grid(row=1, column=0, columnspan=2, padx=10, pady=10)
json_text_box.insert(tk.END, json_text_var.get())  # 初期表示

# 予測される死亡地点
predict_pos = tk.StringVar()
predict_pos.set("予測される死亡地点 :     ")
predict_pos_label = tk.Label(root, textvariable=predict_pos, wraplength=300)
predict_pos_label.grid(row=2, column=0, columnspan=2, padx=50, pady=10)

# 監視対象のJSONファイルへのパス
file_to_watch = ""

# 変更監視用の変数
is_watching = False
observer = None  # グローバル変数として定義

# 変更監視用の関数を作成
def start_file_watching():
    global file_to_watch
    global is_watching
    global observer  # グローバル変数として参照

    # 監視を開始する場合
    if not is_watching:
        file_to_watch = file_path_var.get()

        # Watchdogのハンドラを作成
        event_handler = FileSystemEventHandler()
        event_handler.on_modified = on_file_change

        # Observerのインスタンスを作成し、監視を開始
        observer = Observer()
        observer.schedule(event_handler, path=".", recursive=False)
        observer.start()

        # ボタンのテキストを変更
        start_button.config(text="監視中...")
        is_watching = True

    # 監視を停止する場合
    else:
        observer.stop()
        observer.join()

        # ボタンのテキストを変更
        start_button.config(text="監視を開始")
        is_watching = False

# 監視開始ボタンを作成
start_button = tk.Button(root, text="監視を開始", command=start_file_watching)
start_button.grid(row=3, column=0, columnspan=2, padx=10, pady=10)

# Tkinterのメインループを開始
root.mainloop()
    
