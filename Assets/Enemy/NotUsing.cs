using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotUsing : MonoBehaviour
{//
    public float speed = 5.0f;
    public float reactionDistance = 8.0f;
    float axisH;
    float axisV;
    float span = 0.1f;
    float delta = 0;
    Rigidbody2D rbody;

    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive)
            {
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y + 1.5f - transform.position.y;
                float rad = Mathf.Atan2(dy, dx);
                float angle = rad * Mathf.Rad2Deg;

                delta += Time.deltaTime;
                if (delta >= span)
                {
                    axisH = Mathf.Cos(rad) * speed;
                    axisV = Mathf.Sin(rad) * speed;
                    delta = 0;
                }
            }
            else
            {
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
                {
                    isActive = true;
                }
            }
        }
        else if (isActive)
        {
            isActive = false;
            rbody.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            rbody.velocity = new Vector2(axisH, axisV);
        }
    }
}
