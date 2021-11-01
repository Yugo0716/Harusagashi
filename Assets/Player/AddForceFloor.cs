using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 leftSP = transform.position - Vector3.right * 0.3f - Vector3.up * 0.1f;
        Vector3 rightSP = transform.position + Vector3.right * 0.3f - Vector3.up * 0.1f;
        Vector3 EP = transform.position - Vector3.up * 0.1f;

        Debug.DrawLine(leftSP, EP, Color.red);
        Debug.DrawLine(rightSP, EP, Color.red);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Fall")
        {
            Vector3 leftSP = transform.position - Vector3.right * 0.3f - Vector3.up * 0.1f;
            Vector3 rightSP = transform.position + Vector3.right * 0.3f - Vector3.up * 0.1f;
            Vector3 EP = transform.position - Vector3.up * 0.1f;

            Debug.DrawLine(leftSP, EP, Color.red);
            Debug.DrawLine(rightSP, EP, Color.red);

            if(Physics2D.Linecast(leftSP, EP, LayerMask.GetMask("Ground")) || Physics2D.Linecast(rightSP, EP, LayerMask.GetMask("Ground")))
            {
                collision.gameObject.GetComponent<FallBlock>().DownStart();
            }
            //if (Physics2D.Linecast(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * -0.1f, LayerMask.GetMask("Ground")))
            {
               // collision.gameObject.GetComponent<FallBlock>().DownStart();
            }
        }
    }
}
