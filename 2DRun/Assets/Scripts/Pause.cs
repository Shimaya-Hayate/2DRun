using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    bool pause = false;
    public GameObject pauseCanvas;
    GameObject pauseCanvasClone;
    Button[] button;

    public void GameStop()
    {
        if (pause)
        {
            //時間を止める
            Time.timeScale = 0f;

            //Pause表示
            pauseCanvasClone = Instantiate(pauseCanvas);

            //ボタンを取得
            button = pauseCanvasClone.GetComponentsInChildren<Button>();

            //ボタンにイベント設定
            button[0].onClick.AddListener(GameStop);

            pause = false;
        }
        else
        {
            Time.timeScale = 1f;
            Destroy(pauseCanvasClone);
            pause = true;
        }
    }
}
