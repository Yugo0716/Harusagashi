using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
    Rigidbody2D rbody;
    Vector2 defPos;
    public float downSpeed;
    public float reAppearCount;
    //public float fallCount;
    bool fall = false;
    bool afterFall = false;

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
        rbody = GetComponent<Rigidbody2D>();
        reAppearCount = 0;
        //fallCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (fall)
        {
            Fall();
        }
        if (afterFall)
        {
             reAppearCount += Time.deltaTime;

            if (reAppearCount >= 4.0f)
            {
                fall = false;
                afterFall = false;
                transform.position = defPos;
                reAppearCount = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {      
            collision.transform.SetParent(transform);
        }
        if(collision.gameObject.tag == "Dead")
        {
            AfterFall();
            reAppearCount += Time.deltaTime;

            if (reAppearCount >= 3.0f)
            {
                fall = false;
                transform.position = defPos;
                reAppearCount = 0;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

    public void DownStart()
    {
        fall = true;
        //fallCount += Time.deltaTime;
       // if (fallCount >= 3.0f)
        {

            //rbody.velocity = new Vector2(0, -downSpeed);
            //transform.position -= new Vector3(0, downSpeed * Time.deltaTime, 0);
        }
    }

    void Fall()
    {
        transform.position -= new Vector3(0, downSpeed * Time.deltaTime, 0);
    }

    void AfterFall()
    {
        afterFall = true;
    }
}
