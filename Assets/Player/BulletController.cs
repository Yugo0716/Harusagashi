using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float deleteTime = 2;  //çÌèúéûä‘

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Mirror90")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Mirror90")
        {
            float z = transform.localRotation.z;
            z += 90;
            transform.localRotation = Quaternion.Euler(0, 0, z);
        }

        if (collision.gameObject.tag != "Mirror-90")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Mirror-90")
        {
            float z = transform.localRotation.z;
            z -= -90;
            transform.localRotation = Quaternion.Euler(0, 0, z);
        }
    }
}
