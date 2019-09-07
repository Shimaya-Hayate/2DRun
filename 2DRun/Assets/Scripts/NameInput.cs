using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//RenameCanvasのボタンに保存機能を追加
public class NameInput : MonoBehaviour
{
    public InputField playerName;
    NameManager nameManager;
    GameManager gameManager;
    bool rename = false;

    void Start ()
    {
        nameManager = GameObject.Find("NameManager").GetComponent<NameManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void NameChange()
    {
        rename = true;
    }

    public void NameSave()
    {
        if (rename)
        {
            PlayerPrefs.SetString(nameManager.key, playerName.text); //名前を保存

            nameManager.NameLoad(); //名前をロード
            gameManager.Rename(); //名前変更画面を削除

            rename = false;
        }
    }
}
