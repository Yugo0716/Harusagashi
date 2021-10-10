using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{//
    [SerializeField]
    public bool isDelete = false;  //óéâ∫å„Ç…è¡Ç∑ÉtÉâÉO


    // Start is called before the first frame update
    void Start()
    {
       // generator = GameObject.Find("IcicleGenerator");
        //ï®óùãììÆí‚é~
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            //ãóó£åvë™
            float dx = transform.position.x - player.transform.position.x;
            float dy = transform.position.y - player.transform.position.y;
            if(Mathf.Abs(dx) < 3.0f && Mathf.Abs(dy) < 20.0f)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if(rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            Destroy(gameObject);
        }
    }
}
