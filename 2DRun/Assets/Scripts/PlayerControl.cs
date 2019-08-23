using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float maxSpeed = 10;      //最高速度
    public float maxJampPower = 10;  //最高ジャンプ力

    float speed;       //速度調節
    float jampPower;   //ジャンプ力調節

    float x; //矢印の右が+1、左が-1
    float y; //スペースキーon(+1)、off(-1)

    float currentSpeed = 0;  //現在の速度
    float currentDropSpeed = 0;  //現在の落下速度

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>(); //重力取得
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.x; //速度ベクトル（横）
        currentDropSpeed = GetComponent<Rigidbody>().velocity.y; //速度ベクトル（縦）

        speed = maxSpeed;
        jampPower = maxJampPower;
        y = 0;


        if (Input.GetKeyDown(KeyCode.Space))// スペースキーが押されたら
        {
            y = 1;

            jampPower = jampPower - currentDropSpeed; //落下速度に応じてジャンプ力調節
            if (jampPower <= 0)
            {
                jampPower = 0;
            }
        }



        x = Input.GetAxis("Horizontal"); // 右を正、左を負とする（-1 ～ +1）

        //最高速度を現在の速度が超えたら
        if ((speed - currentSpeed) <= 0) //正の方向
        {
            if (x == 1)
            {
                x = 0;
            }
        }

        if ((speed + currentSpeed) <= 0) //負の方向
        {
            if (x == -1)
            {
                x = 0;
            }
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(x * speed * 2, y * jampPower * 30, 0);
    }
}
