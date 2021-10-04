using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyE_Circle : MonoBehaviour
{
    public float speed = 0f;
    public float radius = 0f;
    Vector3 defPos;


    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = radius * Mathf.Sin(Time.time * speed);
        float y = radius * Mathf.Cos(Time.time * speed);
        transform.position = new Vector3(defPos.x + x, defPos.y + y);
    }
}
