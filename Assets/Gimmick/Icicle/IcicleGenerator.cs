using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleGenerator : MonoBehaviour
{//
    [SerializeField] GameObject iciclePrefab;
    float span = 4.0f;
    public float delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject icicleObj = GameObject.Find(iciclePrefab.name);
        if(icicleObj == null)
        {
            delta += Time.deltaTime;
            if (delta > span)
            {
                delta = 0;
                GameObject newIcicleObj = Instantiate(iciclePrefab);
                newIcicleObj.name = iciclePrefab.name;
            }
        }
    }

   
}
