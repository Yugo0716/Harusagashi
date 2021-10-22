using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyF : MonoBehaviour
{//
    Rigidbody2D rbody;
    public float jump = 9.0f;
    public float speed = 6.0f;
    public LayerMask groundLayer;  //着地できるレイヤー
    public bool onGround = false;
    public float xRange = 0.0f;
    public float yRange = 0.0f;
    float dx;
    float dy;

    //HP関連
    public int arrangeId = 0;
    public float hp = 3;
    public string deadAnime;
    public GameObject dropItem;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
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
        
    }

    private void FixedUpdate()
    {
        onGround = IsCollision();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            dx = transform.position.x - player.transform.position.x;
            dy = transform.position.y - player.transform.position.y;
        }
        else
        {
            rbody.velocity = new Vector2(0, 0);
        }
        
       
        if (onGround && player != null)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, 0f);
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            if (dx >= 0 && dx <= xRange && dy >= -yRange && dy <= yRange)
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
                transform.localScale = new Vector2(-1, 1);

            }
            else if (dx < 0 && dx >= -xRange && dy >= -yRange && dy <= yRange)
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }
        }
        else if(!onGround && player != null)
        {
            if (dx >= 0 && dx <= xRange && dy >= -yRange && dy <= yRange)
            {
                if(rbody.velocity.x < 0.5f)
                {
                    rbody.AddForce(new Vector2(-1.5f, 0));
                }
            }
            else if (dx < 0 && dx >= -xRange && dy >= -yRange && dy <= yRange)
            {
                if (rbody.velocity.x > -0.5f)
                {
                    rbody.AddForce(new Vector2(1.5f, 0));
                }
            }
        }
    }

    bool IsCollision()
    {
        Vector3 leftSP = transform.position - Vector3.right * 0.8f - Vector3.up * 0.06f;
        Vector3 rightSP = transform.position + Vector3.right * 0.8f - Vector3.up * 0.06f;
        Vector3 EP = transform.position - Vector3.up * 0.06f;

        Debug.DrawLine(leftSP, EP);
        Debug.DrawLine(rightSP, EP);

        return Physics2D.Linecast(leftSP, EP, groundLayer) || Physics2D.Linecast(rightSP, EP, groundLayer);
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
