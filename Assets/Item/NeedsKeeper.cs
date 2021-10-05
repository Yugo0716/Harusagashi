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
        for(int k = 0; k < needs.Length; k++)
        {
            needs[k].GetComponent<Image>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NeedsControll(int i)
    {
        needs[i].GetComponent<Image>().enabled = true;
        
    }
}
