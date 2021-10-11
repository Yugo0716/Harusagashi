using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyF : MonoBehaviour
{//
    Rigidbody2D rbody;
    public float jump = 9.0f;
    public float speed = 6.0f;
    public LayerMask groundLayer;  //’…’n‚Å‚«‚éƒŒƒCƒ„[
    public bool onGround = false;
    public float xRange = 0.0f;
    public float yRange = 0.0f;
    float dx;
    float dy;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.5f), groundLayer);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            dx = transform.position.x - player.transform.position.x;
            dy = transform.position.y - player.transform.position.y;
        }
        else
        {
            rbody.velocity = new Vector2(0, 0);
        }
        
       
        if (onGround && player != null)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, 0f);
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            if (dx >= 0 && dx <= xRange && dy >= -yRange && dy <= yRange)
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
                transform.localScale = new Vector2(-1, 1);

            }
            else if (dx < 0 && dx >= -xRange && dy >= -yRange && dy <= yRange)
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }
        }
    }
}
