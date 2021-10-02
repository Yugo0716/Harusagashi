using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public float shootSpeed = 12.0f;  //�e�̑��x
    public float shootDelay = 0.25f;  //���ˊԊu
    
    //public GameObject canonPrafab;
    public GameObject bulletPrefab;

    bool inAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerCnt = player.GetComponent<PlayerController>();
        if (playerCnt.canFly == false)
        {
            //�{�^�������čU��
            if ((Input.GetButtonDown("Fire3")))
            {
                Attack();
            }
        }
    }
    

    public void Attack()
    {
        if(inAttack == false)
        {
            inAttack = true;
            //�e�̃x�N�g��
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float direction =player.transform.localScale.x;
            //�e�����
            GameObject bulletObj;
            if (direction >= 0)
            {
                bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 180));
            }
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            Vector3 v = new Vector3(direction, 0) * shootSpeed;
            //�e�ɗ͂�������
            Rigidbody2D rbody = bulletObj.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);
            //�x�����s
            Invoke("StopAttack", shootDelay);
        }
    }

    public void StopAttack()
    {
        inAttack = false;
    }
}
