using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    int v = 15; //移動速度
    int stageMoveSpeed = 1;

    Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCollider.isTrigger == true)
        {
            stageMoveSpeed = 2;
        }
        else
        {
            stageMoveSpeed = 1;
        }
        this.transform.Translate(v * -0.01f * stageMoveSpeed, 0, 0);
    }
}
