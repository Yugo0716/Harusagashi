using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDShoot : MonoBehaviour
{
    public GameObject enemyD;
    public float shootSpeed = 5.0f;
    public float reactionDistance = 10.0f;
    public float shootDelay = 5.0f;  //”­ŽËŠÔŠu
    public GameObject bulletPrefab;

    bool inAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBD enemyd = enemyD.GetComponent<EnemyBD>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject bulletObj = GameObject.Find(bulletPrefab.name);
        float dist = Vector2.Distance(enemyD.transform.position, player.transform.position);
        if (enemyd.isActive)
        {
            if (dist < reactionDistance)
            {
                if (bulletObj == null)
                {
                    Invoke("Attack", shootDelay);
                }
            }
        }
    }

    public void Attack()
    {
        if(inAttack == false)
        {
            inAttack = true;
            float direction = enemyD.transform.localScale.x;
            if (direction >= 0)
            {
                GameObject newBulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Vector3 v = new Vector3(direction, 0) * shootSpeed;
                Rigidbody2D rbody = newBulletObj.GetComponent<Rigidbody2D>();
                rbody.AddForce(v, ForceMode2D.Impulse);
                newBulletObj.name = bulletPrefab.name;
            }
            else
            {
                GameObject newBulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 180));
                Vector3 v = new Vector3(direction, 0) * shootSpeed;
                Rigidbody2D rbody = newBulletObj.GetComponent<Rigidbody2D>();
                rbody.AddForce(v, ForceMode2D.Impulse);
                newBulletObj.name = bulletPrefab.name;
            }
            
            Invoke("StopAttack", shootDelay);
        }
    }

    public void StopAttack()
    {
        inAttack = false;
    }
}
