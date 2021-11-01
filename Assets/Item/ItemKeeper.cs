using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemKeeper : MonoBehaviour
{//
    public static int hasKeys = 0;
    public static int hasNeeds = 0;

    // Start is called before the first frame update
    void Start()
    {
        hasKeys = PlayerPrefs.GetInt("Keys");
        hasNeeds = PlayerPrefs.GetInt("Needs");
    }

    // Update is called once per frame
    void Update()
    {
        if(hasNeeds == 7)
        {
            //クリア時の処理
            Debug.Log("ccc");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Needs:" + hasNeeds);
            Debug.Log("Key:" + hasKeys);
        }
    }

    //アイテム数保存
    public static void SaveItem()
    {
        PlayerPrefs.SetInt("Keys", hasKeys);
        PlayerPrefs.SetInt("Needs", hasNeeds);
    }
}
