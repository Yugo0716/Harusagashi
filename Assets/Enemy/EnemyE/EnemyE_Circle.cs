using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyE_Circle : MonoBehaviour
{//
    public float speed = 0f;
    public float radius = 0f;
    public float isou = 0f;
    public bool reverseClock;
    public bool rotationRock = true;
    Vector2 defPos;
    float x;
    float y;



    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverseClock)
        {
            x = radius * Mathf.Sin((Time.time * speed + isou * Mathf.PI/180f));
            y = radius * Mathf.Cos((Time.time * speed + isou * Mathf.PI / 180f));
            if (!rotationRock) transform.localRotation = Quaternion.Euler(0, 0, -(Time.time * speed * 180 / Mathf.PI + isou));
        }
        else
        {
            x = radius * Mathf.Sin(-(Time.time * speed + isou * Mathf.PI / 180f));
            y = radius * Mathf.Cos(-(Time.time * speed + isou * Mathf.PI / 180f));

            if(!rotationRock) transform.localRotation = Quaternion.Euler(0, 0, (Time.time * speed * 180/Mathf.PI + isou));
        }

        transform.position = new Vector2(defPos.x + x, defPos.y + y);
    }
}
