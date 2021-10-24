using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum BossStateB
{
    Start,
    Battle
}

public class BossB : MonoBehaviour
{
    private float angle;
    private float speed = 1.2f;
    public float battleStartPosX;
    float fadeTime = 0;

    public GameObject bulletPrefab;
    Rigidbody2D rbody;
    Animator animator;
    BossStateB nowState;
    SpriteRenderer spriteRenderer;
    public float shootSpeed = 5.0f;

    bool inAttack = false;

    //HPŠÖ˜A
    public int arrangeId = 0;
    public float hp = 10;
    public string deadAnime;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        nowState = BossStateB.Start;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        switch (nowState)
        {
            case BossStateB.Start:
                rbody.velocity = new Vector2(0, 0);
                if (player.transform.position.x >= battleStartPosX)
                {
                    StartCoroutine("Encount");
                }
                break;

            case BossStateB.Battle:
                angle += Time.deltaTime * speed;
                transform.position = new Vector3(Mathf.Sin(angle) * 10, Mathf.Sin(angle * 2) * 2.0f + 4);

                if (hp > 0)
                {             
                    if (player != null)
                    {
                        Vector3 playerPos = player.transform.position;
                        if (inAttack == false)
                        {
                            inAttack = true;
                            GetComponent<Animator>().Play("BossAttack");
                        }
                        else
                        {
                            inAttack = false;
                            GetComponent<Animator>().Play("BossIdle");
                        }
                    }
                    else
                    {
                        inAttack = false;
                        GetComponent<Animator>().Play("BossIdle");
                    }
                }
                break;
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
                Destroy(gameObject, 0.1f);
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
            }
        }
    }

    void Attack()
    {
        Transform tr = transform.Find("gate");  //”­ŽËŒû
        GameObject gate = tr.gameObject;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            float dx = player.transform.position.x - gate.transform.position.x;
            float dy = player.transform.position.y - gate.transform.position.y;
            float rad = Mathf.Atan2(dy, dx);
            float angle = rad * Mathf.Rad2Deg;
            GameObject bullet = Instantiate(bulletPrefab, gate.transform.position, Quaternion.Euler(0, 0, angle));
            float x = Mathf.Cos(rad);
            float y = Mathf.Sin(rad);
            Vector2 v = new Vector2(x, y) * shootSpeed;

            Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);
        }
    }

    IEnumerator Encount()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerCnt = player.GetComponent<PlayerController>();

        if (playerCnt.canFly == true)
        {
            playerCnt.canFly = false;
        }
        playerCnt.canControll = false;
        playerCnt.rbody.velocity = new Vector2(0, playerCnt.rbody.velocity.y);
        playerCnt.animator.Play(playerCnt.stopAnime);

        yield return new WaitForSeconds(2.0f);

        Color col = GetComponent<SpriteRenderer>().color;
        fadeTime += Time.deltaTime * 0.8f;
        col.a = fadeTime;
        GetComponent<SpriteRenderer>().color = col;
        if(fadeTime >= 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            fadeTime = 1;
        }
        
        yield return new WaitForSeconds(3.0f);

        gameObject.layer = LayerMask.NameToLayer("Enemy");
        yield return null;
        playerCnt.canControll = true;
        nowState = BossStateB.Battle;
        StopCoroutine("Encount");
    }
    IEnumerator DamageAnim()
    {
        Color col = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(1, 0.8f, 1, 1);
        yield return new WaitForSeconds(0.08f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
