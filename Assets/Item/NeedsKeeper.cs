using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsKeeper : MonoBehaviour
{
    public GameObject[] needs = new GameObject[9];

    // Start is called before the first frame update
    void Start()
    {
        int[] itemNumber = new int[9];
        for(int i = 0; i < itemNumber.Length; i++)
        {
            string iChara = i.ToString();
            itemNumber[i] = PlayerPrefs.GetInt("Item" + iChara);
        }
        
        for (int k = 0; k < needs.Length; k++)
        {
            if(itemNumber[k] != 1)
            {
                needs[k].GetComponent<Image>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NeedsControll(int i)
    {
        string number = i.ToString();
        needs[i].GetComponent<Image>().enabled = true;
        PlayerPrefs.SetInt("Item" + number, 1);
        
    }
}
