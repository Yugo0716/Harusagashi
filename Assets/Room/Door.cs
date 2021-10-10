using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{//
    public GameObject exit;
    public Sprite openImage;
    public Sprite closeImage;

    public int arrangeId = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Exit exitCnt = exit.GetComponent<Exit>();
        if(exitCnt.open == false)
        {
            GetComponent<SpriteRenderer>().sprite = closeImage;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = openImage;
        }
    }
}
