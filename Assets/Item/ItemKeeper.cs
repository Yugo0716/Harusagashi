using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    public static int hasKeys = 0;
    public static int hasNeeds = 0;
    public static int hasNeedsA = 0;
    public static int hasNeedsB = 0;
    public static int hasNeedsC = 0;
    public static int hasNeedsD = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ÉAÉCÉeÉÄêîï€ë∂
    public static void SaveItem()
    {
        PlayerPrefs.SetInt("Keys", hasKeys);
        PlayerPrefs.SetInt("Needs", hasNeeds);
        PlayerPrefs.SetInt("NeedsA", hasNeedsA);
        PlayerPrefs.SetInt("NeedsB", hasNeedsB);
        PlayerPrefs.SetInt("NeedsC", hasNeedsC);
        PlayerPrefs.SetInt("NeedsD", hasNeedsD);
    }
}
