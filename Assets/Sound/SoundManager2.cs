using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
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

public class SoundManager2 : MonoBehaviour
{
    //BGM
    public AudioClip TitleBGM;
    public AudioClip FieldBGM;
    public AudioClip SnowMountain;
    public AudioClip TempleBGM;
    public AudioClip UnderGroundBGM;
    public AudioClip BossBGM;
    public AudioClip EndingBGM;

    public static SoundManager2 soundManager2;

    public static BGMType playingBGM = BGMType.None;

    public void Awake()
    {
        //BGMçƒê∂
        if (soundManager2 == null)
        {
            soundManager2 = this;

            DontDestroyOnLoad(gameObject);  //ÉVÅ[ÉìïœâªÇ≈îjä¸ÇµÇ»Ç¢
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

    //BGMê›íË
    public void PlayBgm(BGMType type)
    {
        if (type != playingBGM)
        {
            playingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if (type == BGMType.Title)
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

    //BGMí‚é~
    public void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }
}*/
