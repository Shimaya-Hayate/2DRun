using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //触れたものを削除
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Wall")
        {
            Destroy(other.gameObject);
        }
    }
}
