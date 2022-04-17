using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace SoulsLike
{

    public class ItemSpawn : MonoBehaviour
{
   
   public GameObject itemPrefab;
   public LayerMask targetLayers;
   public UnityEvent<ItemSpawn> onItemPickup;
    void Awake()
        
        {
            Instantiate(itemPrefab, transform);
            Destroy(transform.GetChild(0).gameObject);

            onItemPickup.AddListener(FindObjectOfType<InventoryManager>().OnItemPickUp);
        }

    private void OnTriggerEnter(Collider other) {
        
        if(0 != (targetLayers.value & 1 << other.gameObject.layer))
        {
            onItemPickup.Invoke(this);
            //Destroy(gameObject);
        }
    }
       
    }
}

