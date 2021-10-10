using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : MonoBehaviour
{//
    public float bulletSpeed = 2.0f;
    public float limitSpeed = 3.0f;
    public float reactionDistance = 8.0f;
    bool isActive = false;

    Rigidbody2D rbody;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            if (isActive)
            {
                Vector3 playerPosDX = new Vector3(player.transform.position.x, player.transform.position.y + 2.0f, player.transform.position.z);
                Vector3 v = playerPosDX - transform.position;
                rbody.AddForce(v.normalized * bulletSpeed);

                float speedXTemp = Mathf.Clamp(rbody.velocity.x, -limitSpeed, limitSpeed);
                float speedYTemp = Mathf.Clamp(rbody.velocity.y, -limitSpeed, limitSpeed);
                rbody.velocity = new Vector3(speedXTemp, speedYTemp);
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
}
