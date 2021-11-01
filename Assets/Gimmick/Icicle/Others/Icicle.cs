using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{//
    [SerializeField]
    public bool isDelete = false;  //óéâ∫å„Ç…è¡Ç∑ÉtÉâÉO
    Rigidbody2D rbody;
    Animator animator;
    public string normalAnime = "IcicleNormal";
    public string breakAnime = "IcicleBreak";
    // Start is called before the first frame update
    void Start()
    {
       // generator = GameObject.Find("IcicleGenerator");
        //ï®óùãììÆí‚é~
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
        animator = GetComponent<Animator>();
        animator.Play(normalAnime);
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
            if(Mathf.Abs(dx) < 3.0f && dy < 20.0f && dy >= 0)
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
            if(collision.gameObject.tag == "Ground")
            {
                StartCoroutine("IcicleBreak");
            }
            
            //Destroy(gameObject);
        }
    }
    IEnumerator IcicleBreak()
    {
        animator.Play(breakAnime);

        yield return new WaitForSeconds(0.2f);

        GetComponent<PolygonCollider2D>().enabled = false;

        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
