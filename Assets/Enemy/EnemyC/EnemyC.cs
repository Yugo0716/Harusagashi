using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : MonoBehaviour
{//
    public float bulletSpeed = 2.0f;
    public float limitSpeed = 3.0f;
    public float reactionDistance = 8.0f;
    bool isActive = false;

    Rigidbody2D rbody;
    GameObject player;

    //HPŠÖ˜A
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
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            if (isActive)
            {
                Vector3 playerPosDX = new Vector3(player.transform.position.x, player.transform.position.y + 2.0f, player.transform.position.z);
                Vector3 v = playerPosDX - transform.position;
                rbody.AddForce(v.normalized * bulletSpeed);

                float speedXTemp = Mathf.Clamp(rbody.velocity.x, -limitSpeed, limitSpeed);
                float speedYTemp = Mathf.Clamp(rbody.velocity.y, -limitSpeed, limitSpeed);
                rbody.velocity = new Vector3(speedXTemp, speedYTemp);
            }
            else
            {
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
            rbody.velocity = Vector2.zero;
        }
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
