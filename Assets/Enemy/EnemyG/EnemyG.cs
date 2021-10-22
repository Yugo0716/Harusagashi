using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG : MonoBehaviour
{
    public float reactionDistance = 20.0f;
    Rigidbody2D rbody;

    public bool isActive = false;

    Vector3 defPos;

    public float shootSpeedLong = 12.0f;  //弾の速度
    public float shootSpeedNormal = 12.0f;
    public float shootSpeedShort = 12.0f;
    public float shootDelay = 5.0f;  //発射間隔
    public float reactionDistanceLong = 10.0f;
    public float reactionDistanceNormal = 10.0f;
    public float reactionDistanceShort = 10.0f;
    public float yDirectionLong = 0.0f;
    public float yDirectionNormal = 0.0f;
    public float yDirectionShort = 0.0f;
    Vector3 v;
    public float dx;

    public GameObject bulletPrefab;

    Animator animator;

    //HP関連
    public int arrangeId = 0;
    public float hp = 3;
    public string deadAnime;
    public GameObject dropItem;

    // Start is called before the first frame update
    void Start()
    {
        defPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (dropItem != null)
        {
            dropItem.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(defPos.x, transform.position.y, transform.position.z);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        dx = player.transform.position.x - transform.position.x;
        if (player != null)
        {
            if (isActive)
            {
                //プレイヤーの方向を見る
                if (dx >= 0) transform.localScale = new Vector2(1, 1f);
                else if (dx < 0) transform.localScale = new Vector2(-1, 1f);

                float dy = transform.position.y - player.transform.position.y;
                if (Mathf.Abs(dy) < 8.0f && Mathf.Abs(dx) < reactionDistanceLong)
                {
                    animator.Play("EnemyGAttack");
                    //Attack();
                }
                else
                {
                    animator.Play("EnemyGNormal");
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
            animator.Play("EnemyGNormal");
        }
    }
    public void Attack()
    {

        if (Mathf.Abs(dx) < reactionDistanceLong)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //弾のベクトル
            float direction = transform.localScale.x;
            //弾を作る
            GameObject bulletObj;

            if (direction >= 0)
            {

                bulletObj = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 2f), Quaternion.identity);
            }
            else
            {

                bulletObj = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 2f), Quaternion.Euler(0, 0, 180));
            }
            Rigidbody2D rbody = bulletObj.GetComponent<Rigidbody2D>();

            if (Mathf.Abs(dx) < reactionDistanceLong)
            {

                if (Mathf.Abs(dx) >= reactionDistanceNormal)
                {

                    v = new Vector3(direction, yDirectionLong);
                    rbody.AddForce(v.normalized * shootSpeedLong, ForceMode2D.Impulse);
                }
                else if (Mathf.Abs(dx) >= reactionDistanceShort)
                {

                    v = new Vector3(direction, yDirectionNormal);
                    rbody.AddForce(v.normalized * shootSpeedNormal, ForceMode2D.Impulse);
                }
                else

                    v = new Vector3(direction, yDirectionShort);
                rbody.AddForce(v.normalized * shootSpeedShort, ForceMode2D.Impulse);
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            StartCoroutine ("DamageAnim");
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
