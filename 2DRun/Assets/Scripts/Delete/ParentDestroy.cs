﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子がいなくなったら削除
public class ParentDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
