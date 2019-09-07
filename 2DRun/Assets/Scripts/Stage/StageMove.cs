using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    int v = 20; //移動速度

    void Update()
    {
        this.transform.Translate(v * -0.01f * Time.timeScale, 0, 0);
    }
}
