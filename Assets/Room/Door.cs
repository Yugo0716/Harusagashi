using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{//
    public GameObject exit;
    public Sprite openImage;
    public Sprite closeImage;
    Animator animator;
    public string openAnime = "WarpZoneOpen";
    public string closeAnime = "WarpZoneClose";

    public int arrangeId = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Exit exitCnt = exit.GetComponent<Exit>();
        if(exitCnt.open == false)
        {
            GetComponent<SpriteRenderer>().sprite = closeImage;
            animator.Play(closeAnime);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = openImage;
            animator.Play(openAnime);
        }
    }
}
