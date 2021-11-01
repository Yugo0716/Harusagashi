using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{//
    public GameObject target;
    public Sprite imageOn;
    public Sprite imageOff;
    public bool on = false;
    public int arrangeId = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (on) GetComponent<SpriteRenderer>().sprite = imageOn;
        else GetComponent<SpriteRenderer>().sprite = imageOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            if (!on)
            {
                //on = true;
                On();
                SoundManager.soundManager.SEPlay(SEType.SwitchOn);
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
                if(collision.gameObject.tag == "Bullet")
                {
                    Destroy(collision.gameObject);
                }
            }
            //else
            {
                //on = true;
                //GetComponent<SpriteRenderer>().sprite = imageOn;
                //target.SetActive(true);
            }
        }
    }
    public void On()
    {
        on = true;
        GetComponent<SpriteRenderer>().sprite = imageOn;
        if(target != null) target.SetActive(false);

    }
}
