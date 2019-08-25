using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreate : MonoBehaviour
{
    Vector3 playerPosition = new Vector3(0, 0, 0);
    int angle; //発射する方向設定
    bool key; //矢印キーを押したか

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //入力に応じて角度を変化させる
        if (Input.GetKeyDown("down"))
        {
            angle = 0;
            key = true;
        }

        if (Input.GetKeyDown("right"))
        {
            angle = 1;
            key = true;
        }

        if (Input.GetKeyDown("up"))
        {
            angle = 2;
            key = true;
        }

        if (Input.GetKeyDown("left"))
        {
            angle = 3;
            key = true;
        }

        if (key)
        {
            playerPosition = GameObject.Find("Player").transform.position; //Playerの座標取得
           
            Instantiate(bullet, playerPosition, Quaternion.Euler(0, 0, 90 * angle));

            key = false;
        }
    }
}
