using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

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
            //êßå¿
            if (x < leftLimit) x = leftLimit;
            else if (x > rightLimit) x = rightLimit;
            if (y < bottomLimit) y = bottomLimit;
            else if (y > topLimit) y = topLimit;
            Vector3 v = new Vector3(x, y, z);
            transform.position = v;
        }
    }
}
