using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGShoot : MonoBehaviour
{//
    public float shootSpeedLong = 12.0f;  //íeÇÃë¨ìx
    public float shootSpeedNormal = 12.0f;
    public float shootSpeedShort = 12.0f;
    public float shootDelay = 5.0f;  //î≠éÀä‘äu
    public float reactionDistanceLong = 10.0f;
    public float reactionDistanceNormal = 10.0f;
    public float reactionDistanceShort = 10.0f;
    public float yDirectionLong = 0.0f;
    public float yDirectionNormal = 0.0f;
    public float yDirectionShort = 0.0f;
    Vector3 v;
    public float dx;

    public GameObject enemyG;
    public GameObject bulletPrefab;

    bool inAttack = false;

    Animator aniator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemyBDG enemyg = enemyG.GetComponent<EnemyBDG>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (enemyg.isActive)
            {
                dx = transform.position.x - player.transform.position.x;
                float dy = transform.position.y - player.transform.position.y;
                if (Mathf.Abs(dy) < 12.0f)
                {
                    aniator.Play("EnemyGAttack");
                }


            }
        }
    }


    public void Attack()
    {

        if (inAttack == false && Mathf.Abs(dx) < reactionDistanceLong)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            inAttack = true;
            //íeÇÃÉxÉNÉgÉã
            float direction = enemyG.transform.localScale.x;
            //íeÇçÏÇÈ
            GameObject bulletObj;

            if (direction >= 0)
            {

                bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            }
            else
            {

                bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 180));
            }
            Rigidbody2D rbody = bulletObj.GetComponent<Rigidbody2D>();

            if (Mathf.Abs(dx) < reactionDistanceLong)
            {

                if(Mathf.Abs(dx) >= reactionDistanceNormal)
                {

                    v = new Vector3(direction, yDirectionLong);
                    rbody.AddForce(v.normalized * shootSpeedLong, ForceMode2D.Impulse);
                }
                else if(Mathf.Abs(dx) >= reactionDistanceShort)
                {

                    v = new Vector3(direction, yDirectionNormal);
                    rbody.AddForce(v.normalized * shootSpeedNormal, ForceMode2D.Impulse);
                }
                else

                    v = new Vector3(direction, yDirectionShort);
                    rbody.AddForce(v.normalized * shootSpeedShort, ForceMode2D.Impulse);
                }
                //íxâÑé¿çs
                Invoke("StopAttack", shootDelay);
                inAttack = true;
            }

        }

    
    public void StopAttack()
    {
        inAttack = false;
    }
}
