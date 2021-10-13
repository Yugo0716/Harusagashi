using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{//
    public float hp = 3;
    Rigidbody2D rbody;
    Animator animator;
    public string damageAnime = "EnemyDamage";
    public string normalAnime = "EnemyNormal";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hp--;
            Animator animator = GetComponent<Animator>();
            
            if (hp > 0)
            {
                StartCoroutine("DamageAnim");
            }
            if(hp <= 0)
            {
                gameObject.layer = LayerMask.NameToLayer("Enemy_Dead");
                rbody.velocity = new Vector2(0, 0);
                Destroy(gameObject, 0.5f);
                
            }
        }
    }
    IEnumerator DamageAnim()
    {
        animator.Play(damageAnime);
        yield return new WaitForSeconds(0.2f);
        animator.Play(normalAnime);
    }
}
