using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsKeeper : MonoBehaviour
{//
    public static string number;
    public GameObject[] needs = new GameObject[9];  //�A�C�e���摜

    public static int[] itemNumber = new int[9];  //�A�C�e���ɕt�����ԍ��Aneeds[i]�ɑΉ�
    public static int[] figure = new int[9];

    // Start is called before the first frame update
    void Start()
    {
        //itemNumber��1������ȊO�����ʁA1�Ȃ�����A1�ȊO�Ȃ炻�̂܂ܕ\��������
        for(int i = 0; i < itemNumber.Length; i++)
        {
            string iChara = i.ToString();
            itemNumber[i] = PlayerPrefs.GetInt("Item" + iChara);  //"Item0~8"��itemNumber[0~8]�̒l�A1�������Ă��邩�ǂݍ���
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

    public void NeedsControll(int i)  //�ȉ��̓A�C�e��0~8�ɐG���Ǝ��s�����
    {
        number = i.ToString();
        if(needs != null) needs[i].GetComponent<Image>().enabled = true;
        figure[i] = 1;  //figure[i]�̒l��1�̎��A�V�[���ύX���ɃZ�[�u�����
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
