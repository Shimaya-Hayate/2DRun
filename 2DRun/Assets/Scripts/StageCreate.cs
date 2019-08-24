using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    float newX;
    float oldX;
    int ceilNewX; //切り上げ後
    int ceilOldX; //切り上げ後

    private bool oneMove = false; //1マス分移動したかの判別

    int random; //乱数

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
            random = Random.Range(0, 20);
            GameObject cube = (GameObject)Resources.Load("Cube/Cube");
            Instantiate(cube, new Vector3(30, random, 0), Quaternion.identity);

            //cube = (GameObject)Resources.Load("Cube/ImmortalCube");
            //Instantiate(cube, new Vector3(30, 20, 0), Quaternion.identity); //天井生成

            ceilOldX = ceilNewX;
            oneMove = false;
        }
        
    }
}
