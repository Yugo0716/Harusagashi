using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyKeeper : MonoBehaviour
{
    public GameObject keyImage;
    int hasKeys;

    // Start is called before the first frame update
    void Start()
    {
        hasKeys = PlayerPrefs.GetInt("Keys");
        if (hasKeys > 0)
        {
            keyImage.GetComponent<Image>().enabled = true;
        }
        else
        {
            keyImage.GetComponent<Image>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeyControll()
    {
        keyImage.GetComponent<Image>().enabled = true;
        //hasKeys = 1;
    }

    public void KeyImageErase()
    {
        keyImage.GetComponent<Image>().enabled = false;
    }
}
