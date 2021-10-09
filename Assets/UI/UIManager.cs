using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int hp = 0;  //�v���C���[��HP
    [SerializeField]
    public GameObject hpImage;  //HP�摜
    public Sprite life5Image;
    public Sprite life4Image;
    public Sprite life3Image;
    public Sprite life2Image;
    public Sprite life1Image;
    //public Sprite life0Image;
    public GameObject mainImage;
    public GameObject continueButton;
    public GameObject endButton;
    public GameObject panel;
    public Sprite gameOverSpr;

    public string retrySceneName = "";

    // Start is called before the first frame update
    void Start()
    {
        UpdateHP();
        InactiveImage();  //Invoke�ɂ���ƃQ�[���X�^�[�g�̎���
        panel.SetActive(false);       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHP();
    }

    //HP�X�V
    void UpdateHP()
    {
        if(PlayerController.gameState != "gameend")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                if(PlayerController.hp != hp)
                {
                    hp = PlayerController.hp;
                    if (hp <= 0)
                    {
                        Destroy(hpImage);
                        panel.SetActive(true);
                        mainImage.SetActive(true);
                        mainImage.GetComponent<Image>().sprite = gameOverSpr;
                        PlayerController.gameState = "gameend";
                    }
                    else if (hp == 1) hpImage.GetComponent<Image>().sprite = life1Image;
                    else if (hp == 2) hpImage.GetComponent<Image>().sprite = life2Image;
                    else if (hp == 3) hpImage.GetComponent<Image>().sprite = life3Image;
                    else if (hp == 4) hpImage.GetComponent<Image>().sprite = life4Image;
                    else hpImage.GetComponent<Image>().sprite = life5Image;
                }
            }
        }
    }

    //���g���C
    public void Retry()
    {
        PlayerController.hp = 3;
        SceneManager.LoadScene(retrySceneName);
    }

    //�摜��\��
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
