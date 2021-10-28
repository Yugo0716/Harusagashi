using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    public float reactionDistance = 20.0f;
    Rigidbody2D rbody;
    public bool isActive = false;
    Vector3 defPos;
    public float shootSpeed = 12.0f;  //�e�̑��x
    public GameObject bulletPrefab;
    float dx;

    //HP�֘A
    public int arrangeId = 0;
    public float hp = 3;
    public string deadAnime;
    public GameObject dropItem;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        if (dropItem != null)
        {
            dropItem.SetActive(false);
        }

        defPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(defPos.x, transform.position.y, transform.position.z);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        dx = player.transform.position.x - transform.position.x;
        if (player != null)
        {
            float dist = Vector2.Distance(transform.position, player.transform.position);
            if (isActive)
            {
                //�v���C���[�̕���������
                float dy = player.transform.position.y - transform.position.y;

                if (dx >= 0) transform.localScale = new Vector2(1, 1f);
                else if (dx < 0) transform.localScale = new Vector2(-1, 1f);

                //float dist = Vector2.Distance(transform.position, player.transform.position);
                if (Mathf.Abs(dy) < 8.0f && Mathf.Abs(dx) < reactionDistance)
                {
                    //Attack();//�ق�Ƃ̓A�j��
                }
                else
                {

                }
                    
            }
            else
            {
                //�����`�F�b�N                
                if (dist < reactionDistance)
                {
                    isActive = true;
                }
            }
            if(dist >= reactionDistance)
            {
                isActive = false;
            }

            if (isActive && player != null)
            {
                float dy = player.transform.position.y - transform.position.y;

                if (Mathf.Abs(dy) < 8.0f && Mathf.Abs(dx) < reactionDistance)
                {
                    animator.Play("EnemyBNormal");
                }
            }
        }
        else if (isActive)
        {
            isActive = false;
        }
    }

    public void Attack()
    {
        if (isActive)
        {
            //�e�̃x�N�g��
            float direction = transform.localScale.x;
            //�e�����
            GameObject bulletObj;
            if (direction >= 0)
            {
                bulletObj = Instantiate(bulletPrefab, new Vector2(transform.position.x -0.6f, transform.position.y), Quaternion.identity);
            }
            else
            {
                bulletObj = Instantiate(bulletPrefab, new Vector2(transform.position.x - 0.6f, transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            Vector3 v = new Vector3(direction, 0) * shootSpeed;
            //�e�ɗ͂�������
            Rigidbody2D rbody = bulletObj.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            SoundManager.soundManager.SEPlay(SEType.Hit);
            StartCoroutine("DamageAnim");
            hp--;

            if (hp > 0)
            {

            }
            if (hp <= 0)
            {
                // animator.Play(deadAnime);
                gameObject.layer = LayerMask.NameToLayer("Enemy_Dead");
                rbody.velocity = new Vector2(0, 0);
                if (dropItem != null)
                {
                    dropItem.SetActive(true);
                }
                Destroy(gameObject, 0.1f);
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
            }
        }
    }

    IEnumerator DamageAnim()
    {
        Color col = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(1, 0.8f, 1, 1);
        yield return new WaitForSeconds(0.08f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
