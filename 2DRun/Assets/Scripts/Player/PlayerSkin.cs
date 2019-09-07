using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    string key = "SKIN";
    public int newScore = 0; // スコア変数
    public int highScore;

    void Start()
    {
        //シーンを切り替えてもこのゲームオブジェクトを削除しないようにする
        DontDestroyOnLoad(gameObject);
    }

    //ハイスコアの表示
    public void HighScore()
    {
        highScore = PlayerPrefs.GetInt(key, 0);//ロード

        if (highScore <= newScore)//ハイスコアなら
        {

            highScore = newScore;//更新
            PlayerPrefs.SetInt(key, highScore);// スコアを保存
            PlayerPrefs.Save();

        }
        //scoreText.text = "HighScore : " + highScore;//表示
    }
}
