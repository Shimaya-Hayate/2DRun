using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMove : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            gameManager.GameOver();//ゲームオーバー
        }

        if (other.gameObject.tag != "Enemy")//Enemy以外なら
        {
            Destroy(this.gameObject);//自身を削除
        }
    }
}
