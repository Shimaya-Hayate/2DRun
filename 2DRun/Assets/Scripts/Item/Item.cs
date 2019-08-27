using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    ItemControl itemControl;

    // Start is called before the first frame update
    void Start()
    {
        itemControl = GameObject.Find("ItemController").GetComponent<ItemControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Playerが触れた時にアイテムの効果を発動
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(this.gameObject.name == "BulletPowerUp")
            {
                itemControl.BPU();
            }

            if (this.gameObject.name == "InvisiblePlayer")
            {
                itemControl.IP();
            }

            Destroy(this.gameObject);
        }
    }
}
