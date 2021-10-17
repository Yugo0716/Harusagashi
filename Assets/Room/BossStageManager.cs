using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageManager : MonoBehaviour
{
    public GameObject bossEnemy;
    public GameObject needItem;
    public GameObject exit;
    public GameObject enemy1, enemy2;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("PlayerHP", 5);
        Exit exitCnt = exit.GetComponent<Exit>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossEnemy == null)
        {
            if (enemy1 != null) Destroy(enemy1);
            if (enemy2 != null) Destroy(enemy2);
        }
        if(needItem == null)
        {
            Exit exitCnt = exit.GetComponent<Exit>();
            exitCnt.Open();
        }
    }
}
