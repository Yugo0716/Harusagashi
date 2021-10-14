using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BGM�^�C�v
public enum BGMType
{
    None
}

//SE�^�C�v
public enum SEType
{

}

public class SoundManager : MonoBehaviour
{
    //SE
    public AudioClip select;
    public AudioClip playerJump;
    public AudioClip playerShoot;
    public AudioClip playerDamage;
    public AudioClip getItem;
    public AudioClip BulletHit;
    public AudioClip warp;
    public AudioClip switchOn;
    public AudioClip reflect;

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

    }
}
