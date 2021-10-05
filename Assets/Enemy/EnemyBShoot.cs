using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBShoot : MonoBehaviour
{
    public float shootSpeed = 12.0f;  //íeÇÃë¨ìx
    public float shootDelay = 5.0f;  //î≠éÀä‘äu
    public float reactionDistance = 10.0f;

    public GameObject enemyB;
    public GameObject bulletPrefab;

    bool inAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBDG enemyb = enemyB.GetComponent<EnemyBDG>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (enemyb.isActive)
            {
                float dist = Vector2.Distance(enemyb.transform.position, player.transform.position);
                if (dist < reactionDistance)
                    Attack();
            }
        }
    }

    public void Attack()
    {
        if (inAttack == false)
        {
            inAttack = true;
            //íeÇÃÉxÉNÉgÉã
            float direction = enemyB.transform.localScale.x;
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
            Vector3 v = new Vector3(direction, 0) * shootSpeed;
            //íeÇ…óÕÇâ¡Ç¶ÇÈ
            Rigidbody2D rbody = bulletObj.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);
            //íxâÑé¿çs
            Invoke("StopAttack", shootDelay);
        }

    }
    public void StopAttack()
    {
        inAttack = false;
    }
}
