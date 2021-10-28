using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{//
    life,
    key,
    need,
}

public class ItemData : MonoBehaviour
{
    public ItemType type;
    public int count = 1;  //アイテム数
    public int needNumber;
    GameObject needskeeper;  //ヒエラルキーにあるやつ
    GameObject keykeeper;



    public int arrangeId = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        needskeeper = GameObject.Find("NeedsKeeper");
        keykeeper = GameObject.Find("KeyKeeper");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (type == ItemType.life)
            {
                if (PlayerController.hp < 5)
                {
                    SoundManager.soundManager.SEPlay(SEType.Heal);
                    PlayerController.hp++;
                    //HP更新
                    PlayerPrefs.SetInt("PlayerHP", PlayerController.hp);
                }
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(gameObject);
                
            }
            else if (type == ItemType.key)
            {
                SoundManager.soundManager.SEPlay(SEType.Heal);
                KeyKeeper keyKeeper = keykeeper.GetComponent<KeyKeeper>();

                ItemKeeper.hasKeys += 1;

                keyKeeper.KeyControll();

                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(gameObject);
            }
            else if (type == ItemType.need)
            {
                SoundManager.soundManager.SEPlay(SEType.Item);
                NeedsKeeper needsKeeper = needskeeper.GetComponent<NeedsKeeper>();

                ItemKeeper.hasNeeds += 1;
                //PlayerPrefs.SetInt("Needs", ItemKeeper.hasNeeds);

                needsKeeper.NeedsControll(needNumber);  //画面上側にとったアイテムを表示

                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(gameObject);
            }
            //配置Idの記録
            SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
        }
    }
}
