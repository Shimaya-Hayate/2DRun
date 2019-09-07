using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    
    public string key = "NAME";
    public Text nameText;
    GameManager gameManager;

    void Start()
    {
        NameLoad();
    }

    public void NameLoad()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (PlayerPrefs.HasKey(key))//データが存在するなら
        {
            //名前をロード
            gameManager.playerName = PlayerPrefs.GetString(key);
        }
        else //存在しないなら
        {
            gameManager.Rename();
        }

        nameText.text = "Your name : " + gameManager.playerName;
    }
}
