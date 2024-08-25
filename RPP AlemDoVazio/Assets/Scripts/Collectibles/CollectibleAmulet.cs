using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAmulet : MonoBehaviour
{
    public int valueAmulet;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                AudioObserver.OnPlaySfxEvent("Collectibles");
                GameController.instance.UpdateAmulet(valueAmulet);
                Destroy(gameObject);
        }
    }
}
