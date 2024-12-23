using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Coin, Heart, Wood, Hammer }
    public CollectibleType collectibleType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (collectibleType)
            {
                case CollectibleType.Coin:
                    GameManager.Instance.CollectCoin(1000);
                    break;
                case CollectibleType.Heart:
                    GameManager.Instance.CollectHeart();
                    break;
                case CollectibleType.Wood:
                    GameManager.Instance.CollectWood();
                    break;
                case CollectibleType.Hammer:
                    GameManager.Instance.CollectHammer();
                    break;
            }
            Destroy(gameObject);
        }
    }
}