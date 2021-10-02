using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    life,
    key,
    need,
    needA,
    needB,
    needC,
    needD,
}

public class ItemData : MonoBehaviour
{
    public ItemType type;
    public int count = 1;  //ÉAÉCÉeÉÄêî

    public int arrangeId = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(type == ItemType.life)
            {
                if(PlayerController.hp < 5)
                {
                    PlayerController.hp++;
                }
            }
            else if(type == ItemType.key)
            {
                ItemKeeper.hasKeys += 1;
            }
            else if(type == ItemType.needA)
            {
                ItemKeeper.hasNeeds += 1;
                ItemKeeper.hasNeedsA += 1;
            }
            else if (type == ItemType.needB)
            {
                ItemKeeper.hasNeeds += 1;
                ItemKeeper.hasNeedsB += 1;
            }
            else if (type == ItemType.needC)
            {
                ItemKeeper.hasNeeds += 1;
                ItemKeeper.hasNeedsC += 1;
            }
            else if (type == ItemType.needD)
            {
                ItemKeeper.hasNeeds += 1;
                ItemKeeper.hasNeedsD += 1;
            }

            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}
