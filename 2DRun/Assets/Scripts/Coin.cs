using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    ScoreCount scoreCount;

    // Start is called before the first frame update
    void Start()
    {
        scoreCount = GameObject.Find("ScoreCounter").GetComponent<ScoreCount>();
    }

    // Update is called once per frame
    void Update()
    {
        //回転する
        this.transform.Rotate(0, 0, 7);
    }

    //Playerが触れた時
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            scoreCount.score += 20;
            Destroy(this.gameObject);
        }
    }
}
