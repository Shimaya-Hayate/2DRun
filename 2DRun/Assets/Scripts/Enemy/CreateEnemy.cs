using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    int enemySpeed = 5; //移動速度
    int count = 0;
    public GameObject enemySphere;

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
            if ((count % 50) == 0)
            {
                Instantiate(enemySphere, this.transform.position, Quaternion.identity);
            }
        }
    }
}
