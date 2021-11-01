using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BGM�^�C�v
public enum BGMType
{
    None,
    Title,
    Field,
    SnowMountain,
    Temple,
    Underground,
    Boss,
    Ending
}

//SE�^�C�v
public enum SEType
{
    Jump,
    Warp,
    Reflect,
    Heal,
    Damage,
    Select,
    GameOver,
    Attack,
    Hit,
    Item,
    SwitchOn
}

public class SoundManager : MonoBehaviour
{
    //BGM
    public AudioClip TitleBGM;
    public AudioClip FieldBGM;
    public AudioClip SnowMountain;
    public AudioClip TempleBGM;
    public AudioClip UnderGroundBGM;
    public AudioClip BossBGM;
    public AudioClip EndingBGM;

    //SE
    public AudioClip selectSE;
    public AudioClip jumpSE;
    public AudioClip attackSE;
    public AudioClip damageSE;
    public AudioClip itemSE;
    public AudioClip hitSE;
    public AudioClip warpSE;
    public AudioClip switchOnSE;
    public AudioClip reflectSE;
    public AudioClip healSE;
    public AudioClip gameOverSE;

    public static SoundManager soundManager;

    public static BGMType playingBGM = BGMType.None;

    public void Awake()
    {
        //BGM�Đ�
        if(soundManager == null)
        {
            soundManager = this;

            DontDestroyOnLoad(gameObject);  //�V�[���ω��Ŕj�����Ȃ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //BGM�ݒ�
    public void PlayBgm(BGMType type)
    {
        if(type != playingBGM)
        {
            playingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if(type == BGMType.Title)
            {
                audio.clip = TitleBGM;
            }
            else if (type == BGMType.Field)
            {
                audio.clip = FieldBGM;
            }
            else if (type == BGMType.SnowMountain)
            {
                audio.clip = SnowMountain;
            }
            else if (type == BGMType.Temple)
            {
                audio.clip = TempleBGM;
            }
            else if (type == BGMType.Underground)
            {
                audio.clip = UnderGroundBGM;
            }
            else if (type == BGMType.Boss)
            {
                audio.clip = BossBGM;
            }
            else if (type == BGMType.Ending)
            {
                audio.clip = EndingBGM;
            }
            audio.Play();
        }
    }

    //BGM��~
    public void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }

    //SE�Đ�
    public void SEPlay(SEType type)
    {
        if(type == SEType.Jump)
        {
            GetComponent<AudioSource>().PlayOneShot(jumpSE);
        }
        else if(type == SEType.Warp)
        {
            GetComponent<AudioSource>().PlayOneShot(warpSE);
        }
        else if (type == SEType.Reflect)
        {
            GetComponent<AudioSource>().PlayOneShot(reflectSE);
        }
        else if (type == SEType.Heal)
        {
            GetComponent<AudioSource>().PlayOneShot(healSE);
        }
        else if (type == SEType.Damage)
        {
            GetComponent<AudioSource>().PlayOneShot(damageSE);
        }
        else if (type == SEType.Select)
        {
            GetComponent<AudioSource>().PlayOneShot(selectSE);
        }
        else if (type == SEType.GameOver)
        {
            GetComponent<AudioSource>().PlayOneShot(gameOverSE);
        }
        else if (type == SEType.Attack)
        {
            GetComponent<AudioSource>().PlayOneShot(attackSE);
        }
        else if (type == SEType.Hit)
        {
            GetComponent<AudioSource>().PlayOneShot(hitSE);
        }
        else if (type == SEType.Item)
        {
            GetComponent<AudioSource>().PlayOneShot(itemSE);
        }
        else if (type == SEType.SwitchOn)
        {
            GetComponent<AudioSource>().PlayOneShot(switchOnSE);
        }
    }
}
