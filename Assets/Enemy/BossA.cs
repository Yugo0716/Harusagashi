using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BossStateA
{
    Start,
    Battle
}

public class BossA : MonoBehaviour
{
    Rigidbody2D rbody;
    BossStateA nowState;
    public float battleStartPosX;
    public float jump = 9.0f;
    public float speed = 6.0f;
    public LayerMask groundLayer;  //íÖínÇ≈Ç´ÇÈÉåÉCÉÑÅ[
    public bool onGround = false;
    public float xRange = 0.0f;
    public float yRange = 0.0f;
    float dx;
    float dy;

    public GameObject enemy1;
    public GameObject enemy2;




    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.localScale = new Vector2(-1, 1);
        nowState = BossStateA.Start;
        enemy1.SetActive(false);
        enemy2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (nowState)
        {
            case BossStateA.Start:

                break;

            case BossStateA.Battle:

                break;
        }
    }

    private void FixedUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if(player != null)
        {
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            switch (nowState)
            {
                case BossStateA.Start:
                        rbody.velocity = new Vector2(0, 0);
                        if (player.transform.position.x >= battleStartPosX)
                        {
                            StartCoroutine("Encount");
                        }
                   
                    break;

                case BossStateA.Battle:
                    onGround = IsCollision();
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
                        if (dx >= 0)
                        {
                            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
                            transform.localScale = new Vector2(-2, 2);

                        }
                        else
                        {
                            rbody.velocity = new Vector2(speed, rbody.velocity.y);
                            transform.localScale = new Vector2(2, 2);
                        }
                    }
                    else if (!onGround && player != null)
                    {
                        if (dx >= 0)
                        {
                            if (rbody.velocity.x < 0.5f)
                            {
                                rbody.AddForce(new Vector2(-1.5f, 0));
                            }
                        }
                        else
                        {
                            if (rbody.velocity.x > -0.5f)
                            {
                                rbody.AddForce(new Vector2(1.5f, 0));
                            }
                        }
                    }
                    break;
            }
        }
        
    }

    bool IsCollision()
    {
        Vector3 leftSP = transform.position - Vector3.right * 1.8f - Vector3.up * 0.06f;
        Vector3 rightSP = transform.position + Vector3.right * 1.8f - Vector3.up * 0.06f;
        Vector3 EP = transform.position - Vector3.up * 0.06f;

        Debug.DrawLine(leftSP, EP);
        Debug.DrawLine(rightSP, EP);

        return Physics2D.Linecast(leftSP, EP, groundLayer) || Physics2D.Linecast(rightSP, EP, groundLayer);
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

        yield return new WaitForSeconds(3.0f);

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(-2, 2), Time.deltaTime * 8);

        yield return new WaitForSeconds(3.0f);

        transform.localScale = new Vector2(-2, 2);
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        yield return null;
        playerCnt.canControll = true;
        enemy1.SetActive(true);
        enemy2.SetActive(true);
        nowState = BossStateA.Battle;
        StopCoroutine("Encount");
    }
}
