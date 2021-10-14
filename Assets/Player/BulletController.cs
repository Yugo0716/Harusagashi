using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{//
    public float deleteTime = 2;  //çÌèúéûä‘
    public float z;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime);
        z = transform.localRotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Mirror+90" && collision.gameObject.tag != "Mirror-90")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Mirror+90")
        {
            //z = transform.localRotation.z;
            z += 90;
            transform.localRotation = Quaternion.Euler(0, 0,  z);
        }

        else if (collision.gameObject.tag == "Mirror-90")
        {
            //z = transform.localRotation.z;
            z += -90;
            transform.localRotation = Quaternion.Euler(0, 0, -180 + z);
        }
    }
}
