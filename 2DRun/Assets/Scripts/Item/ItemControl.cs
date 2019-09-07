using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    //Playerの情報用
    GameObject player;
    Collider playerCollider;
    Rigidbody playerRigidbody;

    public BulletCreate bulletCreate;

    int random; //乱数

    // Start is called before the first frame update
    void Start()
    {
        //Playerの情報をそれぞれ代入
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
        playerRigidbody = player.GetComponent<Rigidbody>();

        //スクリプト[BulletCreate]を参照
        bulletCreate = GameObject.Find("BulletCreater").GetComponent<BulletCreate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //アイテム「InvisiblePlayer」------------------------------------------------------
    public void IP()
    {
        Invisible();
        Invoke("Visible", 2.0f);//2秒後に実行
    }

    //Playerが透けるようになる
    void Invisible()
    {
        playerCollider.isTrigger = true;
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;//座標固定
    }

    //Playerの当たり判定がもとに戻る
    void Visible()
    {
        playerCollider.isTrigger = false;
        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;//座標固定解除
    }

    //---------------------------------------------------------------------------------

    //アイテム「BulletPowerUp」--------------------------------------------------------
    public void BPU()
    {
        random = Random.Range(0, 2);

        PowerUp();
        Invoke("PowerDown", 3.0f);

    }

    //パワーアップ
    void PowerUp()
    {
        bulletCreate.powerUp = true; 
    }

    //もとに戻る
    void PowerDown()
    {
        bulletCreate.powerUp = false;
    }

    //--------------------------------------------------------------------------------
}
