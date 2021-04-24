using UnityEngine;

namespace Enums
{
    [System.Serializable]
    public class Item
    {
        public enum ItemType { None, Weapon, Consumable }

        public string name;
        public int ID;
        public Sprite icon;

        public ItemType itemType;

        public void SetItemAction()
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    Debug.Log("Item selected: --- " + itemType);
                    break;
                case ItemType.Consumable:
                    Debug.Log("Consumable selected");
                    break;
            }
        }
    }
}