using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{//
    public Rigidbody2D rbody;
    //操作できるかどうか
    public bool canControll = true;
    //移動
    public float axisH = 0.0f;
    public float speed = 9.0f;
    //ジャンプ
    public float jump = 9.0f;  //ジャンプ力
    public LayerMask groundLayer;  //着地できるレイヤー
    bool goJump = false;
    public bool onGround = false;
    bool inAir = false;
    //飛行
    public bool canSelectFly = true;
    public bool canFly = false;
    public float flySpeed = 3.0f;
    public float axisV = 0.0f;
    public float angleZ = -90.0f;
    //ダメージ関係
    public static int hp = 5;  //プレイヤーのHP
    public static string gameState;
    bool inDamage = false;
    //アニメーション
    public Animator animator;
    public string stopAnime = "PlayerStop";
    public string walkAnime = "PlayerWalk";
    public string jumpAnime = "PlayerJump";
    public string flyAnime = "PlayerFly";
    public string damageAnime = "PlayerDamage";
    public string deadAnime = "PlayerDead";
    string nowAnime = "";
    string oldAnime = "";


    // Start is called before the first frame update
    void Start()
    {
        //制作用
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("PlayerHP", 5);

        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
        //アニメーション
        nowAnime = stopAnime;
        oldAnime = stopAnime;
        //gameStateをプレイ中にする
        gameState = "playing";
        //HP読み込み
        hp = PlayerPrefs.GetInt("PlayerHP");
    }

    // Update is called once per frame
    void Update()
    {
        string a = PlayerPrefs.GetString("LastScene");
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(a);
        }
        //ゲーム中以外は何もしない
        if (gameState != "playing")
        {
            return;
        }

        if (canControll)
        {
            #region//飛行との切り替え
            if (canSelectFly)
            {
                if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (canFly == true) canFly = false;
                    else canFly = true;
                }
            }
            #endregion

            if (canFly == false)
            {
                #region//左右移動とジャンプ
                //左右移動
                axisH = Input.GetAxisRaw("Horizontal");
                if (axisH > 0.0f)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                else if (axisH < 0.0f)
                {
                    transform.localScale = new Vector2(-1, 1);
                }

                //ジャンプさせる
                if (onGround && Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
                if (!onGround)
                {
                    inAir = true;
                }
                else
                {
                    inAir = false;
                }
                #endregion
            }
            else
            {
                #region//飛行
                axisH = Input.GetAxisRaw("Horizontal");
                axisV = Input.GetAxisRaw("Vertical");
                if (axisH > 0.0f)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                else if (axisH < 0.0f)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                //キー入力から移動角度を求める
                Vector2 fromPt = transform.position;
                Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
                angleZ = GetAngle(fromPt, toPt);
                #endregion
            }
        }
    }

    private void FixedUpdate()
    {
        //ゲーム中以外は何もしない
        if(gameState != "playing")
        {
            return;
        }

        #region//ダメージ処理
        if (inDamage)
        {
            //ダメージ中点滅させる
            float val = Mathf.Sin(Time.time * 50);
            if (val > 0) gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else gameObject.GetComponent<SpriteRenderer>().enabled = false;

            //return;
        }
        #endregion

        
            if (canFly == false)
            {
                rbody.gravityScale = 6.0f;
                //地上判定
                onGround = IsCollision();
                //Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.04f), groundLayer);
                if (canControll)
                {
                    #region//移動とジャンプ
                    //左右移動
                    if (onGround)
                    {
                        //rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
                        if (axisH > 0)
                        {
                            rbody.AddForce(transform.right * 50.0f);
                            if (rbody.velocity.x >= speed)
                            {
                                rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
                            }
                        }
                        else if (axisH < 0)
                        {
                            rbody.AddForce(-transform.right * 50.0f);
                            if (rbody.velocity.x <= -speed)
                            {
                                rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
                            }
                        }
                        else
                        {
                            rbody.velocity = new Vector2(0, 0);
                        }
                        if (axisH == 0)
                        {
                            nowAnime = stopAnime;
                        }
                        else
                        {
                            nowAnime = walkAnime;
                        }
                    }

                    //ジャンプ
                    if (onGround && goJump)
                    {
                        rbody.velocity = new Vector2(rbody.velocity.x, 0f);
                        Vector2 jumpPw = new Vector2(0, jump);

                        rbody.AddForce(jumpPw, ForceMode2D.Impulse);
                        goJump = false;

                        nowAnime = jumpAnime;
                    }

                    //空中移動
                    if (inAir)
                    {
                        if (axisH >= 0)
                        {
                            if (rbody.velocity.x <= speed)
                            {
                                Vector2 airPw = new Vector2(axisH * 40, 0);
                                rbody.AddForce(airPw, ForceMode2D.Force);
                            }
                            else if (rbody.velocity.x > speed && axisH != 0)
                            {
                                rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
                            }
                        }
                        else if (axisH < 0)
                        {
                            if (rbody.velocity.x >= -speed)
                            {
                                Vector2 airPw = new Vector2(axisH * 40, 0);
                                rbody.AddForce(airPw, ForceMode2D.Force);
                            }
                            else if (rbody.velocity.x < -speed && axisH != 0)
                            {
                                rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
                            }
                        }
                        nowAnime = jumpAnime;

                    }
                    #endregion
                }
            }
            else
            {
                if (canControll)
                {
                #region//飛行
                //重力不可
                rbody.gravityScale = 0.0f;
                //速度更新
                rbody.velocity = new Vector2(axisH, axisV) * flySpeed;

                nowAnime = flyAnime;
                #endregion
                }
            }
        

        if(rbody.velocity.y <= -20)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, -20);
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    public void Jump()
    {
        goJump = true;
    }

    bool IsCollision()
    {
        Vector3 leftSP = transform.position - Vector3.right * 0.4f - Vector3.up * 0.06f;
        Vector3 rightSP = transform.position + Vector3.right * 0.4f - Vector3.up * 0.06f;
        Vector3 EP = transform.position - Vector3.up * 0.06f;

        Debug.DrawLine(leftSP, EP);
        Debug.DrawLine(rightSP, EP);

        return Physics2D.Linecast(leftSP, EP, groundLayer) || Physics2D.Linecast(rightSP, EP, groundLayer);
    }
    //角度の取得
    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;
        if(axisH != 0 || axisV != 0)
        {
            //移動中なら角度を更新する
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            float rad = Mathf.Atan2(dy, dx);
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            //停止中なら以前の角度を維持
            angle = angleZ;
        }
        return angle;
    }

    //敵や即死、アイテムとの接触
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
             GetDamage(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Dead")
        {
            hp = 0;
            GameOver();
        }
    }

    //ダメージ
    void GetDamage(GameObject enemy)
    {
        if(gameState == "playing")
        {
            hp--;
            //HP更新
            PlayerPrefs.SetInt("PlayerHP", hp);
            if(hp > 0)
            {
                rbody.velocity = new Vector2(0, 0);
                //敵と反対方向にヒットバック
                Vector3 v = (transform.position - enemy.transform.position).normalized;
                rbody.AddForce(new Vector2(v.x * 4, v.y * 4), ForceMode2D.Impulse);
                inDamage = true;
                //敵をすり抜けるレイヤーにする
                gameObject.layer = LayerMask.NameToLayer("Player_Damage");
                //少しの間操作不能にする
                StartCoroutine("CanControllCoroutine");
                //元に戻す
                Invoke("DamageEnd", 1.5f);
            }
            else
            {
                GameOver();
            }
        }
    }

    //少しの間操作不能にするコルーチン
    IEnumerator CanControllCoroutine()
    {
        canControll = false;
        nowAnime = damageAnime;
        yield return new WaitForSeconds(0.7f);
        canControll = true;
        nowAnime = oldAnime;
    }

    void DamageEnd()
    {
        inDamage = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    void GameOver()
    {
        gameState = "gameover";
        GetComponent<CapsuleCollider2D>().enabled = false;
        rbody.velocity = new Vector2(0, 0);
        rbody.gravityScale = 1;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        animator.Play(deadAnime);
        Destroy(gameObject, 1.0f);
    }
}
