using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    //����ł��邩�ǂ���
    public bool canControll = true;
    //�ړ�
    public float axisH = 0.0f;
    public float speed = 7.0f;
    //�W�����v
    public float jump = 9.0f;  //�W�����v��
    public LayerMask groundLayer;  //���n�ł��郌�C���[
    bool goJump = false;
    public bool onGround = false;
    bool inAir = false;
    //��s
    public bool canSelectFly = true;
    public bool canFly = false;
    public float flySpeed = 3.0f;
    public float axisV = 0.0f;
    public float angleZ = -90.0f;
    //�_���[�W�֌W
    public static int hp = 5;  //�v���C���[��HP
    public static string gameState;
    bool inDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        rbody = GetComponent<Rigidbody2D>();
        //gameState���v���C���ɂ���
        gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[�����ȊO�͉������Ȃ�
        if(gameState != "playing")
        {
            return;
        }

        if (canControll)
        {
            #region//��s�Ƃ̐؂�ւ�
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
                #region//���E�ړ��ƃW�����v
                //���E�ړ�
                axisH = Input.GetAxisRaw("Horizontal");
                if (axisH > 0.0f)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                else if (axisH < 0.0f)
                {
                    transform.localScale = new Vector2(-1, 1);
                }

                //�W�����v������
                if (Input.GetButtonDown("Jump"))
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
                #region//��s
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
                //�L�[���͂���ړ��p�x�����߂�
                Vector2 fromPt = transform.position;
                Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
                angleZ = GetAngle(fromPt, toPt);
                #endregion
            }
        }
    }

    private void FixedUpdate()
    {
        //�Q�[�����ȊO�͉������Ȃ�
        if(gameState != "playing")
        {
            return;
        }

        #region//�_���[�W����
        if (inDamage)
        {
            //�_���[�W���_�ł�����
            float val = Mathf.Sin(Time.time * 50);
            if (val > 0) gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else gameObject.GetComponent<SpriteRenderer>().enabled = false;

            //return;
        }
        #endregion

        if (canControll)
        {
            if (canFly == false)
            {
                rbody.gravityScale = 6.0f;
                //�n�㔻��
                onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.05f), groundLayer);
                #region//�ړ��ƃW�����v
                //���E�ړ�
                if (onGround)
                {
                    rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
                }

                //�W�����v
                if (onGround && goJump)
                {
                    Vector2 jumpPw = new Vector2(0, jump);
                    rbody.AddForce(jumpPw, ForceMode2D.Impulse);
                    goJump = false;
                }

                //�󒆈ړ�
                if (inAir)
                {
                    if (axisH >= 0)
                    {
                        if (rbody.velocity.x <= speed)
                        {
                            Vector2 airPw = new Vector2(axisH * 50, 0);
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
                            Vector2 airPw = new Vector2(axisH * 50, 0);
                            rbody.AddForce(airPw, ForceMode2D.Force);
                        }
                        else if (rbody.velocity.x < -speed && axisH != 0)
                        {
                            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
                        }
                    }

                }
                #endregion
            }
            else
            {
                #region//��s
                //�d�͕s��
                rbody.gravityScale = 0.0f;
                //���x�X�V
                rbody.velocity = new Vector2(axisH, axisV) * flySpeed;
                #endregion
            }
        }
    }

    public void Jump()
    {
        goJump = true;
    }

    //�p�x�̎擾
    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;
        if(axisH != 0 || axisV != 0)
        {
            //�ړ����Ȃ�p�x���X�V����
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            float rad = Mathf.Atan2(dy, dx);
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            //��~���Ȃ�ȑO�̊p�x���ێ�
            angle = angleZ;
        }
        return angle;
    }

    //�G�Ƃ̐ڐG
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
             GetDamage(collision.gameObject);
        }
    }

    //�_���[�W
    void GetDamage(GameObject enemy)
    {
        if(gameState == "playing")
        {
            hp--;
            if(hp > 0)
            {
                rbody.velocity = new Vector2(0, 0);
                //�G�Ɣ��Ε����Ƀq�b�g�o�b�N
                Vector3 v = (transform.position - enemy.transform.position).normalized;
                rbody.AddForce(new Vector2(v.x * 2, v.y * 2), ForceMode2D.Impulse);
                inDamage = true;
                //�G�����蔲���郌�C���[�ɂ���
                gameObject.layer = LayerMask.NameToLayer("Player_Damage");
                //�����̊ԑ���s�\�ɂ���
                StartCoroutine("CanControllCoroutine");
                //���ɖ߂�
                Invoke("DamageEnd", 1.5f);
            }
            else
            {
                GameOver();
            }
        }
    }

    //�����̊ԑ���s�\�ɂ���R���[�`��
    IEnumerator CanControllCoroutine()
    {
        canControll = false;
        yield return new WaitForSeconds(0.7f);
        canControll = true;
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
        Destroy(gameObject, 1.0f);
    }
}
