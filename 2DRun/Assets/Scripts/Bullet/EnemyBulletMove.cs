using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0.5f, 0);
    }

    //当たり判定に触れた時
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //Playerなら
        {
            Destroy(other.gameObject);//Playerを削除
        }

        if (other.gameObject.tag != "Enemy")//Enemy以外なら
        {
            Destroy(this.gameObject);//自身を削除
        }
    }
}
