using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    public float reactionDistance = 20.0f;
    Rigidbody2D rbody;
    public bool isActive = false;
    Vector3 defPos;
    public float shootSpeed = 12.0f;  //弾の速度
    public GameObject bulletPrefab;

    //HP関連
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
        if (player != null)
        {
            if (isActive)
            {
                //プレイヤーの方向を見る
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;

                if (dx >= 0) transform.localScale = new Vector2(1, 1f);
                else if (dx < 0) transform.localScale = new Vector2(-1, 1f);

                //float dist = Vector2.Distance(transform.position, player.transform.position);
                if (Mathf.Abs(dy) < 8.0f && Mathf.Abs(dx) < reactionDistance)
                {
                    Attack();//ほんとはアニメ
                }
                else
                {

                }
                    
            }
            else
            {
                //距離チェック
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
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

    public void Attack()
    {
            //弾のベクトル
            float direction = transform.localScale.x;
            //弾を作る
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
            //弾に力を加える
            Rigidbody2D rbody = bulletObj.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
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
