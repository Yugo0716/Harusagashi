using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oneway : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            parent.layer = LayerMask.NameToLayer("Oneway");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("Kaijo", 0.02f);
        }
    }
    void Kaijo()
    {
        parent.layer = LayerMask.NameToLayer("Ground");
    }
}
