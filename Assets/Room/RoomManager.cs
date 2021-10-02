using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
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
                float y = doorObj.transform.position.y;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(x, y);
                break;
            }
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
        SceneManager.LoadScene(scenename);
    }
}
