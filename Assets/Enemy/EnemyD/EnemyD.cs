using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyD : MonoBehaviour
{
    public float reactionDistance = 20.0f;
    Rigidbody2D rbody;

    public bool isActive = false;

    Vector3 defPos;

    float dx;
    public float shootSpeed = 5.0f;
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
            float dist = Vector2.Distance(transform.position, player.transform.position);
            if (isActive)
            {
                //プレイヤーの方向を見る
                if (dx >= 0) transform.localScale = new Vector2(1, 1f);
                else if (dx < 0) transform.localScale = new Vector2(-1, 1f);
            }
            else
            {
                //距離チェック
                if (dist < reactionDistance)
                {
                    isActive = true;
                }
            }
            if(dist >= reactionDistance)
            {
                isActive = false;
            }
            

            GameObject bulletObj = GameObject.Find(bulletPrefab.name);

            if (isActive && player != null)
            {
                float dy = player.transform.position.y - transform.position.y;

                if (Mathf.Abs(dy) < 8.0f && Mathf.Abs(dx) < reactionDistance)
                {
                    animator.Play("EnemyDNormal");                 
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
        GameObject bulletObj = GameObject.Find(bulletPrefab.name);
        if (isActive)
        {
            if (bulletObj == null)
            {
                float direction = transform.localScale.x;
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
            }
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
