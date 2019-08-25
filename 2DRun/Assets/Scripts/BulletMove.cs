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
        Destroy(this.gameObject);

        if (other.gameObject.tag == "Target") //タグが破壊可(Target)なら
        {
            Destroy(other.gameObject);
        }
    }
}
