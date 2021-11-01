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
        //�v���C���[�ʒu
        //�o������𓾂�
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

    //�V�[���ړ�
    public static void ChangeScene(string scenename, int doornum)
    {
        doorNumber = doornum;  //�h�A�ԍ���static�ϐ��ɕۑ�
        string nowScene = PlayerPrefs.GetString("LastScene");
        if(nowScene != "")
        {
            SaveDataManager.SaveArrangeData(nowScene);  //�z�u�f�[�^��ۑ�
        }
        PlayerPrefs.SetString("LastScene", scenename);  //�V�[������ۑ�
        Debug.Log(scenename);
        PlayerPrefs.SetInt("LastDoor", doornum);  //�h�A�ԍ���ۑ�
        ItemKeeper.SaveItem();  //�A�C�e����ۑ�
        //�ȉ�for���̓V�[���ύX���ɃA�C�e���摜���ۑ������悤�ɂ��邽�߂̂���
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
