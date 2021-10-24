using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsKeeper : MonoBehaviour
{//
    public static string number;
    public GameObject[] needs = new GameObject[9];  //アイテム画像

    public static int[] itemNumber = new int[9];  //アイテムに付けた番号、needs[i]に対応
    public static int[] figure = new int[9];

    // Start is called before the first frame update
    void Start()
    {
        //itemNumberが1かそれ以外か判別、1なら消す、1以外ならそのまま表示させる
        for(int i = 0; i < itemNumber.Length; i++)
        {
            string iChara = i.ToString();
            itemNumber[i] = PlayerPrefs.GetInt("Item" + iChara);  //"Item0~8"がitemNumber[0~8]の値、1が入っているか読み込み
        }
        
        if(needs != null)
        {
            for (int k = 0; k < needs.Length; k++)
            {
                if (itemNumber[k] != 1)
                {
                    if(needs[k]!= null) needs[k].GetComponent<Image>().enabled = false;
                }

            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NeedsControll(int i)  //以下はアイテム0~8に触れると実行される
    {
        number = i.ToString();
        if(needs != null) needs[i].GetComponent<Image>().enabled = true;
        figure[i] = 1;  //figure[i]の値が1の時、シーン変更時にセーブされる
        //PlayerPrefs.SetInt("Item" + number, 1);
        
    }

    public void Initiarize()
    {
        for(int i = 0; i < figure.Length; i++)
        {
            figure[i] = 0;
            itemNumber[i] = 0;
        }
    }
}
