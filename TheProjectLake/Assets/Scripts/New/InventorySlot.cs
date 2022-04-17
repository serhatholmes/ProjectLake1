using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoulsLike
{
    [System.Serializable]
    public class InventorySlot : MonoBehaviour
    {
        public int index;
        public string itemName = "";
        public GameObject itemPrefab;
        public InventorySlot(int index){

            this.index = index;
        }

        public void Place(GameObject item)
        {
            itemName = item.transform.name;
            itemPrefab = item;
        }
    }
}

