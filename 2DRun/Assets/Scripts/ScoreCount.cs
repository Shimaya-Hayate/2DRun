using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public Text scoreText;

    float newX;
    float oldX;
    int ceilNewX; //切り上げ後
    int ceilOldX; //切り上げ後

    public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        oldX = GameObject.Find("Floor").transform.position.x; //Floorのx座標取得
        ceilOldX = Mathf.CeilToInt(oldX);  //切り上げてint型に変換
    }

    // Update is called once per frame
    void Update()
    {
        newX = GameObject.Find("Floor").transform.position.x;
        ceilNewX = Mathf.CeilToInt(newX);

        //10マス移動したのか
        if (ceilOldX - ceilNewX == 10)
        {
            ceilOldX = ceilNewX;
            score++;
        }

        scoreText.text = score.ToString("0");
    }
}
