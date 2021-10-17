using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyF : MonoBehaviour
{//
    Rigidbody2D rbody;
    public float jump = 9.0f;
    public float speed = 6.0f;
    public LayerMask groundLayer;  //íÖínÇ≈Ç´ÇÈÉåÉCÉÑÅ[
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
        onGround = IsCollision();

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
        else if(!onGround && player != null)
        {
            if (dx >= 0 && dx <= xRange && dy >= -yRange && dy <= yRange)
            {
                if(rbody.velocity.x < 0.5f)
                {
                    rbody.AddForce(new Vector2(-1.5f, 0));
                }
            }
            else if (dx < 0 && dx >= -xRange && dy >= -yRange && dy <= yRange)
            {
                if (rbody.velocity.x > -0.5f)
                {
                    rbody.AddForce(new Vector2(1.5f, 0));
                }
            }
        }
    }

    bool IsCollision()
    {
        Vector3 leftSP = transform.position - Vector3.right * 0.8f - Vector3.up * 0.06f;
        Vector3 rightSP = transform.position + Vector3.right * 0.8f - Vector3.up * 0.06f;
        Vector3 EP = transform.position - Vector3.up * 0.06f;

        Debug.DrawLine(leftSP, EP);
        Debug.DrawLine(rightSP, EP);

        return Physics2D.Linecast(leftSP, EP, groundLayer) || Physics2D.Linecast(rightSP, EP, groundLayer);
    }
}
