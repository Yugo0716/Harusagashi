using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyE_Circle : MonoBehaviour
{
    public float speed = 0f;
    public float radius = 0f;
<<<<<<< HEAD
    Vector3 defPos;
=======
    public bool reverseClock;
    Vector2 defPos;
    float x;
    float y;

>>>>>>> yugo-branch


    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        float x = radius * Mathf.Sin(Time.time * speed);
        float y = radius * Mathf.Cos(Time.time * speed);
        transform.position = new Vector3(defPos.x + x, defPos.y + y);
=======
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
>>>>>>> yugo-branch
    }
}
