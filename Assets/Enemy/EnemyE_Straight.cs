using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyE_Straight : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float times = 0.0f;
    public float weight = 0.0f;

    public bool isCanMove = true;
    float perdx;
    float perdy;
    Vector3 defPos;
    bool isReverse = false;

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;  //初期位置
        //1フレームの移動時間取得,x移動値,y移動値
        float timestep = Time.fixedDeltaTime;
        perdx = moveX / (1.0f / timestep * times);
        perdy = moveY / (1.0f / timestep * times);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if (isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            if (isReverse)
            {
                if ((perdx >= 0.0f && x <= defPos.x) || (perdx < 0.0f && x >= defPos.x)) endX = true;
                if ((perdy >= 0.0f && y <= defPos.y) || (perdy < 0.0f && y >= defPos.y)) endY = true;
                transform.Translate(new Vector3(-perdx, -perdy, defPos.z));
            }
            else
            {
                if ((perdx >= 0.0f && x >= defPos.x + moveX) || (perdx < 0.0f && x <= defPos.x + moveX)) endX = true;
                if ((perdy >= 0.0f && y >= defPos.y + moveY) || (perdy < 0.0f && y <= defPos.y + moveY)) endY = true;
                Vector3 v = new Vector3(perdx, perdy, defPos.z);
                transform.Translate(v);
            }
            if (endX && endY)
            {
                if (isReverse) transform.position = defPos;
                isReverse = !isReverse;
                isCanMove = false;
                Invoke("Move", weight);
            }
        }
    }

    public void Move()
    {
        isCanMove = true;
    }

    public void Stop()
    {
        isCanMove = false;
    }
}
