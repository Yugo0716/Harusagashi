using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{//
    public string sceneName = "";  //�ړ���̃V�[��
    public int doorNumber = 0;  //�h�A�ԍ�
    public bool open = true;
    public bool touch = false;

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
            if (touch == false)
            {
                if (playerCnt.onGround == true && playerCnt.canFly == false && open)
                {
                    float axisV = Input.GetAxisRaw("Vertical");
                    if (axisV > 0)
                    {
                        SoundManager.soundManager.SEPlay(SEType.Warp);
                        RoomManager.ChangeScene(sceneName, doorNumber);
                    }
                }
            }         
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(touch == true)
            {
                SoundManager.soundManager.SEPlay(SEType.Warp);
                RoomManager.ChangeScene(sceneName, doorNumber);
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
