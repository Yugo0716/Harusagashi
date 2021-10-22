using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChild : MonoBehaviour
{
    float speed;
    float isou;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        EnemyE_Circle enemyE = parent.GetComponent<EnemyE_Circle>();
        speed = enemyE.speed;
        isou = enemyE.isou;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyE_Circle enemyE = parent.GetComponent<EnemyE_Circle>();
        if (!enemyE.reverseClock)
        {
            if (!enemyE.rotationRock) transform.localRotation = Quaternion.Euler(0, 0, (Time.time * speed * 180 / Mathf.PI + isou));
        }
        else
        {
            if (!enemyE.rotationRock) transform.localRotation = Quaternion.Euler(0, 0, -(Time.time * speed * 180 / Mathf.PI + isou));
        }
    }
}
