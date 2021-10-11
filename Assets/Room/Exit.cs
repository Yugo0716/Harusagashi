using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{//
    public string sceneName = "";  //移動先のシーン
    public int doorNumber = 0;  //ドア番号
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
        //配置Idの記録 自信ない
        SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
    }

}
