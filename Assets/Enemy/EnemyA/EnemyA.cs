using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{//
    public float speed = 3.0f;
    public string direction = "left";
    public float range = 0.0f;
    Vector3 defPos;
    Rigidbody2D rbody;

    //HPŠÖ˜A
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

        if (direction == "right") transform.localScale = new Vector2(-1, 1);
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(range > 0.0f)
        {
            if(transform.position.x < defPos.x - (range / 2))
            {
                direction = "right";
                transform.localScale = new Vector2(-1, 1);
            }
            if(transform.position.x > defPos.x + (range / 2))
            {
                direction = "left";
                transform.localScale = new Vector2(1, 1);
            }
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        if (direction == "right") rbody.velocity = new Vector2(speed, rbody.velocity.y);
        else rbody.velocity = new Vector2(-speed, rbody.velocity.y);
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

        if (collision.gameObject.tag == "Ground")
        {
            if (direction == "right")
            {
                direction = "left";
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                direction = "right";
                transform.localScale = new Vector2(-1, 1);
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
