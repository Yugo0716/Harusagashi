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
        //SaveDataList������
        arrangeDataList = new SaveDataList();
        arrangeDataList.saveDatas = new SaveData[] { };
        //�V�[������ǂݍ���
        string stageName = PlayerPrefs.GetString("LastScene");
        //�V�[�������L�[�ɂ��ĕۑ��f�[�^��ǂݍ���
        string data = PlayerPrefs.GetString(stageName);
        if(data != "")
        {
            //JSON����SaveDataList�ɕϊ�����
            arrangeDataList = JsonUtility.FromJson<SaveDataList>(data);
            for(int i = 0; i < arrangeDataList.saveDatas.Length; i++)
            {
                //�z�񂩂���o��
                SaveData savedata = arrangeDataList.saveDatas[i];
                //�^�O�̃Q�[���I�u�W�F�N�g��T��
                string objTag = savedata.objTag;
                GameObject[] objects = GameObject.FindGameObjectsWithTag(objTag);
                for(int ii = 0; ii < objects.Length; ii++)
                {
                    //�z�񂩂�Q�[���I�u�W�F�N�g�����o��
                    GameObject obj = objects[ii];
                    //GameObject�̃^�O�𒲂ׂ�
                    //Exit�ɂ���
                    if(objTag == "Exit")
                    {
                        Exit exit = obj.GetComponent<Exit>();
                        if(exit.arrangeId == savedata.arrangeId)
                        {
                            exit.open = true;
                        }
                    }
                    //Damage�ɂ��āi���Enemy�j
                    else if(objTag == "Damage")
                    {

                    }
                    //Item�ɂ���
                    else if(objTag == "Item")
                    {
                        ItemData item = obj.GetComponent<ItemData>();
                        if(item.arrangeId == savedata.arrangeId)
                        {
                            Destroy(obj);
                        }
                    }
                    //ItemImage�ɂ���
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
            return;  //�L�^���Ȃ�
        }
        //�ǉ����邽�߂�1����SaveData�z������
        SaveData[] newSavedatas = new SaveData[arrangeDataList.saveDatas.Length + 1];
        //�f�[�^���R�s�[����
        for(int i = 0; i < arrangeDataList.saveDatas.Length; i++)
        {
            newSavedatas[i] = arrangeDataList.saveDatas[i];
        }
        //SaveData�쐬
        SaveData savedata = new SaveData();
        savedata.arrangeId = arrangeId;  //Id�L�^
        savedata.objTag = objTag;  //Tag�L�^
        //SaveData�ǉ�
        newSavedatas[arrangeDataList.saveDatas.Length] = savedata;
        arrangeDataList.saveDatas = newSavedatas;
    }

    public static void SaveArrangeData(string stageName)
    {
        if(arrangeDataList.saveDatas != null && stageName != "")
        {
            //SaveDataList��JSON�f�[�^�ɕϊ�
            string saveJson = JsonUtility.ToJson(arrangeDataList);
            //�V�[�������L�[�ɂ��ĕۑ�
            PlayerPrefs.SetString(stageName, saveJson);
        }
    }
}
