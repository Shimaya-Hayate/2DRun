﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySphereMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(-5000f, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
