﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    float newX;
    float oldX;
    int ceilNewX; //切り上げ後
    int ceilOldX; //切り上げ後

    private bool oneMove = false; //1マス分移動したかの判別

    // Start is called before the first frame update
    void Start()
    {
        oldX = (GameObject.Find("Floor").transform.position.x); //Floorのx座標取得
        ceilOldX = Mathf.CeilToInt(oldX);  //切り上げてint型に変換
    }

    // Update is called once per frame
    void Update()
    {
        newX = GameObject.Find("Floor").transform.position.x;
        ceilNewX = Mathf.CeilToInt(newX);

        //1マス移動したのか
        if (ceilOldX - ceilNewX == 1)
        {
            oneMove = true;
        }

        //ブロック生成
        if (oneMove)
        {
            GameObject cube = (GameObject)Resources.Load("Cube");
            Instantiate(cube, new Vector3(10f, 1f, 0), Quaternion.identity);
            ceilOldX = ceilNewX;
            oneMove = false;
        }
        
    }
}
