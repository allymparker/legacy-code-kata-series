namespace GildedRose.Console
{
    public class Item
    {
        public Item(ItemType itemType)
        {
            ItemType = itemType;
        }

        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public ItemType ItemType { get; }
    }
}