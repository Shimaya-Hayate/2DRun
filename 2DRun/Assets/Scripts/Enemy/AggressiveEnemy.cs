﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemy : MonoBehaviour
{
    int enemySpeed = 5; //移動速度
    int count = 0;
    public GameObject enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        
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
                enemySpeed *= -1;
            }
            this.transform.Translate(0, enemySpeed * -0.01f, 0);

            //攻撃する
            if (count == 30)
            {
                Instantiate(enemyBullet, this.transform.position, Quaternion.Euler(0, 0, 90));
            }
        }
    }
}
