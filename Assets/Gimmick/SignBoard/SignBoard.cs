using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignBoard : MonoBehaviour
{
    public string message;
    public GameObject messagePrefab;

    private GameObject canvas;
    private GameObject messageUI;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        messagePrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!messageUI)
            {
                messagePrefab.SetActive(true);
                messageUI = Instantiate(messagePrefab) as GameObject;
                messageUI.transform.SetParent(canvas.transform, false);

                Text messageUIText = messageUI.transform.Find("MessageText").GetComponent<Text>();
                messageUIText.text = message;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (messageUI)
            {
                messagePrefab.SetActive(false);
                Destroy(messageUI);
            }
        }
    }
}
