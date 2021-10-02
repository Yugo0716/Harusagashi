using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantFly : MonoBehaviour
{
    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerCnt = Player.GetComponent<PlayerController>();
        if (collision.gameObject.tag == "Player")
        {
            playerCnt.canSelectFly = false;
            playerCnt.canFly = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerCnt = Player.GetComponent<PlayerController>();
        if (collision.gameObject.tag == "Player")
        {
            playerCnt.canSelectFly = true;
        }
    }


}
