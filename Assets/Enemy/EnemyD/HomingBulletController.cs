using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletController : MonoBehaviour
{//
    public float bulletSpeed = 2.0f;
    public float limitSpeed = 3.0f;
    public float deleteTime = 8;  //çÌèúéûä‘

    Rigidbody2D rbody;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime);
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            Vector3 playerPosDX = new Vector3(player.transform.position.x, player.transform.position.y + 1.8f, player.transform.position.z);
            Vector3 v = playerPosDX - transform.position;
            rbody.AddForce(v.normalized * bulletSpeed);

            float speedXTemp = Mathf.Clamp(rbody.velocity.x, -limitSpeed, limitSpeed);
            float speedYTemp = Mathf.Clamp(rbody.velocity.y, -limitSpeed, limitSpeed);
            rbody.velocity = new Vector3(speedXTemp, speedYTemp);

            float rad = Mathf.Atan2(rbody.velocity.y, rbody.velocity.x);
            float angle = rad * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
