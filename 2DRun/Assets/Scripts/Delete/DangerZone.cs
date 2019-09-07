using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
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
        
    }

    //触れたものを削除
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Ground")//Ground以外なら
        {
            if(other.gameObject.tag == "Player")//Playerなら
            {
                gameManager.GameOver(); //ゲームオーバー
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
