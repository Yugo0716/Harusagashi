using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{//
    public static int doorNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤー位置
        //出入り口を得る
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");
        for(int i = 0; i < enters.Length; i++)
        {
            GameObject doorObj = enters[i];
            Exit exit = doorObj.GetComponent<Exit>();
            if(doorNumber == exit.doorNumber)
            {
                float x = doorObj.transform.position.x;
                float y = doorObj.transform.position.y -1.5f;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(x, y);
                break;
            }
        }

        string scenename = PlayerPrefs.GetString("LastScene");

        if(scenename == "A-1" || scenename == "A-2" || scenename == "A-3" || scenename == "A-4" || scenename == "A-5")
        {
            SoundManager.soundManager.PlayBgm(BGMType.Field);
        }
        else if (scenename == "B-1" || scenename == "B-2")
        {
            SoundManager.soundManager.PlayBgm(BGMType.SnowMountain);
        }
        else if (scenename == "D-1" || scenename == "D-2" || scenename == "D-3" || scenename == "D-4" || scenename == "D-5" || scenename == "D-6")
        {
            SoundManager.soundManager.PlayBgm(BGMType.Temple);
        }
        else if (scenename == "E-1")
        {
            SoundManager.soundManager.PlayBgm(BGMType.Underground);
        }
        else if(scenename == "Title")
        {
            SoundManager.soundManager.PlayBgm(BGMType.Title);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //シーン移動
    public static void ChangeScene(string scenename, int doornum)
    {
        doorNumber = doornum;  //ドア番号をstatic変数に保存
        string nowScene = PlayerPrefs.GetString("LastScene");
        if(nowScene != "")
        {
            SaveDataManager.SaveArrangeData(nowScene);  //配置データを保存
        }
        PlayerPrefs.SetString("LastScene", scenename);  //シーン名を保存
        Debug.Log(scenename);
        PlayerPrefs.SetInt("LastDoor", doornum);  //ドア番号を保存
        ItemKeeper.SaveItem();  //アイテムを保存
        //以下for内はシーン変更時にアイテム画像が保存されるようにするためのもの
        for( int i = 0; i < NeedsKeeper.itemNumber.Length; i++)
        {
            if(NeedsKeeper.figure[i] == 1)
            {
                string iChara = i.ToString();
                PlayerPrefs.SetInt("Item" + iChara, 1);
            }

        }

        //SceneManager.LoadScene(scenename);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerCnt = player.GetComponent<PlayerController>();
        playerCnt.rbody.velocity = new Vector2(0, 0);
        playerCnt.canControll = false;
        FadeManager.Instance.LoadScene(scenename, 0.2f);
    }
}
