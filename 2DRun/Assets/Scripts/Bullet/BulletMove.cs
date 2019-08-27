using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, -1f, 0);
    }

    //当たり判定に触れた時
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")//Player以外なら
        {
                Destroy(this.gameObject);//自身を削除
        }

        if (other.gameObject.tag == "Target") //タグが破壊可(Target)なら
        {
            Destroy(other.gameObject);//Targetを削除
        }
    }
}
