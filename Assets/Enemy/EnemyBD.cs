using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBD : MonoBehaviour
{
    public float reactionDistance = 20.0f;
    Rigidbody2D rbody;

    public bool isActive = false;

    Vector3 defPos;

    // Start is called before the first frame update
    void Start()
    {
        defPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(defPos.x, transform.position.y, transform.position.z);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            if (isActive)
            {
                //プレイヤーの方向を見る
                float dx = player.transform.position.x - transform.position.x;

                if (dx >= 0) transform.localScale = new Vector2(2, 3);
                else if (dx < 0) transform.localScale = new Vector2(-2, 3);
            }
            else
            {
                //距離チェック
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if(dist < reactionDistance)
                {
                    isActive = true;
                }
            }
        }
        else if (isActive)
        {
            isActive = false;
        }
    }
}
