using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    public InputField inputField;
    public GameObject exit;
    public GameObject fieldObject;

    // Start is called before the first frame update
    void Start()
    {
        fieldObject.SetActive(false);
        Exit exitCnt = exit.GetComponent<Exit>();
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
            if (playerCnt.onGround == true && playerCnt.canFly == false)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    fieldObject.SetActive(true);
                    playerCnt.canControll = false;
                }
            }
        }
    }

    public void SetText()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerCnt = player.GetComponent<PlayerController>();

        if (inputField.text == "1234")
        {
            Exit exitCnt = exit.GetComponent<Exit>();
            exitCnt.Open();
        }
        inputField.text = "";
        fieldObject.SetActive(false);
        playerCnt.canControll = true;
    }
}
