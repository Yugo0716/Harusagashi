using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{//
    public float hp = 3;
    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hp--;
            if(hp <= 0)
            {
                gameObject.layer = LayerMask.NameToLayer("Enemy_Dead");
                rbody.velocity = new Vector2(0, 0);
                Destroy(gameObject, 0.5f);
                
            }
        }
    }
}
