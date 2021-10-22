using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{//
    public int arrangeId = 0;
    public float hp = 3;
    public string deadAnime;
    public GameObject dropItem;

    Animator animator;
    Rigidbody2D rbody;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        if(dropItem != null)
        {
            dropItem.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hp--;
            
            if (hp > 0)
            {
                
            }
            if(hp <= 0)
            {
                gameObject.layer = LayerMask.NameToLayer("Enemy_Dead");
                rbody.velocity = new Vector2(0, 0);
                if(dropItem != null)
                {
                    dropItem.SetActive(true);
                }
                Destroy(gameObject, 0.1f);
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
            }
        }
    }
}
