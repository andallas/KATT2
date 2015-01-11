using UnityEngine;

namespace Helper
{
    public enum ItemType { upgrade, weapons };

    [System.Serializable]
    public struct Item
    {
        public string title;
        public string description;
        public int price;
        public int level;
        public ItemType type;
        public bool equipped;

        Item(string title, string description, int price, int level, ItemType type, bool equipped)
        {
            this.title = title;
            this.description = description;
            this.price = price;
            this.level = level;
            this.type = type;
            this.equipped = equipped;
        }

        public override string ToString()
        {
            return  "Title: " + this.title +
                    "\nDescription: " + this.description +
                    "\nPrice: " + this.price +
                    "\nLevel: " + this.level +
                    "\nType: " + this.type +
                    "\nEquipped: " + this.equipped;
        }
    };
}