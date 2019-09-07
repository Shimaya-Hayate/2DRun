using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMove : MonoBehaviour
{
    public int verticalSpeed = 5; //上下移動速度
    public int horizonSpeed = 0; //左右移動速度
    int count = 0;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        count = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            //上下移動する
            count++;
            if (count == 100)
            {
                count = 0;
                verticalSpeed *= -1;
                horizonSpeed *= -1;
            }
            this.transform.Translate(horizonSpeed * -0.01f, verticalSpeed * -0.01f, 0);
        }
    }

    //Player触れたら削除
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//Playerなら
        {
            gameManager.GameOver(); //ゲームオーバー
        }
    }
}
