using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    float newX;
    float oldX;
    int ceilNewX; //切り上げ後
    int ceilOldX; //切り上げ後

    public GameObject[] stage;

    int random; //乱数

    // Start is called before the first frame update
    void Start()
    {
        oldX = GameObject.Find("Floor").transform.position.x; //Floorのx座標取得
        ceilOldX = Mathf.CeilToInt(oldX);  //切り上げてint型に変換
        Create();
    }

    // Update is called once per frame
    void Update()
    {
        newX = GameObject.Find("Floor").transform.position.x;
        ceilNewX = Mathf.CeilToInt(newX);

        //10マス移動したのか
        if (ceilOldX - ceilNewX == 50)
        {
            Create();
        }        
    }

    //ステージ生成
    public void Create()
    {
        random = Random.Range(0, 5);
        Instantiate(stage[random], new Vector3(30, 0, 0), Quaternion.identity);

        ceilOldX = ceilNewX;      
    }
}
