using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataList arrangeDataList;

    // Start is called before the first frame update
    void Start()
    {
        //SaveDataList初期化
        arrangeDataList = new SaveDataList();
        arrangeDataList.saveDatas = new SaveData[] { };
        //シーン名を読み込む
        string stageName = PlayerPrefs.GetString("LastScene");
        //シーン名をキーにして保存データを読み込む
        string data = PlayerPrefs.GetString(stageName);
        if(data != "")
        {
            //JSONからSaveDataListに変換する
            arrangeDataList = JsonUtility.FromJson<SaveDataList>(data);
            for(int i = 0; i < arrangeDataList.saveDatas.Length; i++)
            {
                //配列から取り出す
                SaveData savedata = arrangeDataList.saveDatas[i];
                //タグのゲームオブジェクトを探す
                string objTag = savedata.objTag;
                GameObject[] objects = GameObject.FindGameObjectsWithTag(objTag);
                for(int ii = 0; ii < objects.Length; ii++)
                {
                    //配列からゲームオブジェクトを取り出す
                    GameObject obj = objects[ii];
                    //GameObjectのタグを調べる
                    //Exitについて
                    if(objTag == "Exit")
                    {
                        Exit exit = obj.GetComponent<Exit>();
                        if(exit.arrangeId == savedata.arrangeId)
                        {
                            exit.open = true;
                        }
                    }
                    //Damageについて（主にEnemy）
                    else if(objTag == "Damage")
                    {

                    }
                    //Itemについて
                    else if(objTag == "Item")
                    {
                        ItemData item = obj.GetComponent<ItemData>();
                        if(item.arrangeId == savedata.arrangeId)
                        {
                            Destroy(obj);
                        }
                    }
                    //ItemImageについて
                    else if(objTag == "ItemImage")
                    {
                        ItemImage itemImage = obj.GetComponent<ItemImage>();
                        if(itemImage.arrangeId == savedata.arrangeId)
                        {
                            obj.GetComponent<Image>().enabled = true;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetArrangeId(int arrangeId, string objTag)
    {
        if(arrangeId == 0 || objTag == "")
        {
            return;  //記録しない
        }
        //追加するために1つ多いSaveData配列を作る
        SaveData[] newSavedatas = new SaveData[arrangeDataList.saveDatas.Length + 1];
        //データをコピーする
        for(int i = 0; i < arrangeDataList.saveDatas.Length; i++)
        {
            newSavedatas[i] = arrangeDataList.saveDatas[i];
        }
        //SaveData作成
        SaveData savedata = new SaveData();
        savedata.arrangeId = arrangeId;  //Id記録
        savedata.objTag = objTag;  //Tag記録
        //SaveData追加
        newSavedatas[arrangeDataList.saveDatas.Length] = savedata;
        arrangeDataList.saveDatas = newSavedatas;
    }

    public static void SaveArrangeData(string stageName)
    {
        if(arrangeDataList.saveDatas != null && stageName != "")
        {
            //SaveDataListをJSONデータに変換
            string saveJson = JsonUtility.ToJson(arrangeDataList);
            //シーン名をキーにして保存
            PlayerPrefs.SetString(stageName, saveJson);
        }
    }
}
