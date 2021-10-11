using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpFrom : MonoBehaviour
{//
    public GameObject warpTo;
    public bool open = true;

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
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            if (playerCnt.onGround == true && open)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.transform.position = warpTo.transform.position;
                }
            }
        }
    }
}
