using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{//
    public float speed = 3.0f;
    public string direction = "left";
    public float range = 0.0f;
    Vector3 defPos;

    // Start is called before the first frame update
    void Start()
    {
        if (direction == "right") transform.localScale = new Vector2(-2, 2);
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(range > 0.0f)
        {
            if(transform.position.x < defPos.x - (range / 2))
            {
                direction = "right";
                transform.localScale = new Vector2(-2, 2);
            }
            if(transform.position.x > defPos.x + (range / 2))
            {
                direction = "left";
                transform.localScale = new Vector2(2, 2);
            }
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        if (direction == "right") rbody.velocity = new Vector2(speed, rbody.velocity.y);
        else rbody.velocity = new Vector2(-speed, rbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (direction == "right")
            {
                direction = "left";
                transform.localScale = new Vector2(2, 2);
            }
            else
            {
                direction = "right";
                transform.localScale = new Vector2(-2, 2);
            }
        }
    }

}
