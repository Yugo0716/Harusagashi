using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BGMタイプ
public enum BGMType
{
    None
}

//SEタイプ
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
        //BGM再生
        if(soundManager == null)
        {
            soundManager = this;

            DontDestroyOnLoad(gameObject);  //シーン変化で破棄しない
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

    //BGM設定
    public void PlayBgm(BGMType type)
    {

    }

    //BGM停止
    public void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }

    //SE再生
    public void SEPlay(SEType type)
    {

    }
}
