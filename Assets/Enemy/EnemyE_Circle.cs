using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyE_Circle : MonoBehaviour
{//
    public float speed = 0f;
    public float radius = 0f;
    public bool reverseClock;
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
            x = radius * Mathf.Sin(Time.time * speed);
            y = radius * Mathf.Cos(Time.time * speed);
        }
        else
        {
            x = radius * Mathf.Sin(Time.time * -speed);
            y = radius * Mathf.Cos(Time.time * -speed);
        }

        transform.position = new Vector2(defPos.x + x, defPos.y + y);
    }
}
