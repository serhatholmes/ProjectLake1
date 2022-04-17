using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


namespace SoulsLike
{
    public class InventoryManager : MonoBehaviour
    {
        
        public List<InventorySlot> inventory = new List<InventorySlot>();
        public Transform inventoryPanel;

        private int mInventorySize;

        private void Awake() {
            mInventorySize = inventoryPanel.childCount;
            CreateInventory(mInventorySize);
        }

        public void OnItemPickUp(ItemSpawn spawner)

        {
            
            AddItemFrom(spawner);
        }
        private void CreateInventory(int size)
        {
            for(int i=0; i<size; i++)
            {
                inventory.Add(new InventorySlot(i));
                RegisterSlotHandler(i);
            }
        }

       private void RegisterSlotHandler(int slotIndex)
       {
           var slotBtn = inventoryPanel.GetChild(slotIndex).GetComponent<Button>();
           slotBtn.onClick.AddListener(() =>
           {
               UseItem(slotIndex);
           });
       }

       private void UseItem(int slotIndex){

           var inventorySlot = GetSlotByIndex(slotIndex);
           if(inventorySlot.itemPrefab == null) { return; }
           //Debug.Log("using item on index"+slotIndex);

           PlayerKontrol.Instance.UseItemFrom(inventorySlot);
       }

        public void AddItemFrom(ItemSpawn spawner) 
        {
            //Debug.Log(inventory.Count);
            var inventorySlot = GetFreeSlot();
            if (inventorySlot is null) 
            {
                //Debug.Log("inventory is full");
                return; 
            }
            var item = spawner.itemPrefab;
            inventorySlot.Place(item);
            inventoryPanel
            .GetChild(inventorySlot.index)
            .GetComponentInChildren<Text>().text = item.name;

            
            //Debug.Log("Added" + itemToPickup.name);
            Destroy(spawner.gameObject);


        }

        private InventorySlot GetFreeSlot()
        {
            Debug.Log("working");
            Debug.Log("Inventory size: " + inventory.Count);

            var obje = inventory.Find(slot => slot.itemName == "");
            return obje;
        }

        private InventorySlot GetSlotByIndex(int index)
        {
            return inventory.Find(slot => slot.index == index);
        }
        
    
}

}

