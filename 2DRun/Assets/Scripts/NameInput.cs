using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//RenameCanvasのボタンに保存機能を追加
public class NameInput : MonoBehaviour
{
    public InputField playerName;
    NameManager nameManager;
    bool rename = false;

    void Start ()
    {
        nameManager = GameObject.Find("NameManager").GetComponent<NameManager>();
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

            rename = false;
        }
    }
}
