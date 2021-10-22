using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{//
   // public GameObject otherTarget;
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    public GameObject backGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            float x = player.transform.position.x;
            float y = player.transform.position.y + 3;
            float z = transform.position.z;

            Vector2 playerPos = new Vector3(x, y, -10);
            //êßå¿
            if (x < leftLimit) x = leftLimit;
            else if (x > rightLimit) x = rightLimit;
            if (y < bottomLimit) y = bottomLimit;
            else if (y > topLimit) y = topLimit;

            //if (otherTarget == null)
            {
                Vector3 v = new Vector3(x, y, z);
                transform.position = v;
            }
            //else
            {
                //Vector3 targetPos = new Vector3(otherTarget.transform.position.x, otherTarget.transform.position.y, -10);
               // Vector2 pos = Vector2.Lerp(playerPos, targetPos, 0.5f);
               // transform.position = pos;
            }
            if(backGround != null)
            {
                backGround.transform.position = transform.position;
            }
        }
    }
}
