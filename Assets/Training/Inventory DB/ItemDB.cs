using System.Collections.Generic;
using UnityEngine;

namespace Inventories
{
    public class ItemDB : MonoBehaviour
    {
        public List<Item> itemDatabase = new List<Item>();

        // PlayerInv (Player.cs) to add or remove Item in Player
        public void AddItem(int itemID, Player playerInv)
        {
            // Check if itemID matches any existing item in DB
            foreach (var itemDB in itemDatabase)
            {
                if (itemID == itemDB.id)
                {
                    // Add item from DB List to Player inventory
                    // TODO improve code because it shows the message even if the item is already added to the array 
                    playerInv.inventory[1] = itemDB;
                    Debug.Log("Match found");
                    return;
                }
            }

            Debug.Log("Match not found");
        }

        public void RemoveItem(int itemID, Player playerInv)
        {
            foreach (var itemDB in itemDatabase)
            {
                if (itemID == itemDB.id)
                {
                    // INFO the way to remove an item from an array is set it to null, because the array is static
                    playerInv.inventory[1] = null;
                    Debug.Log("Item found and removed");
                    return;
                }
            }

            Debug.Log("Item not found");
        }
    }
}
