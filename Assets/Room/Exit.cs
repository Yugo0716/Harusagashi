using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{//
    public string sceneName = "";  //�ړ���̃V�[��
    public int doorNumber = 0;  //�h�A�ԍ�
    public bool open = true;

    public int arrangeId = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            if (playerCnt.onGround == true && open)
            {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        RoomManager.ChangeScene(sceneName, doorNumber);
                    }
            }
        }
    }

    public void Open()
    {
        open = true;
        //�z�uId�̋L�^ ���M�Ȃ�
        SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
    }

}
