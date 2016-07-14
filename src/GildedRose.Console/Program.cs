using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    internal class Program
    {
        private IList<Item> Items;

        internal static void Main(string[] args)
        {
            UpdateAndPrintItems();

            System.Console.ReadKey();
        }

        internal static void UpdateAndPrintItems()
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program
            {
                Items = new List<Item>
                {
                    new Item(ItemType.Perishable) {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item(ItemType.Aging) {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item(ItemType.Perishable) {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item(ItemType.Legendary) {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item(ItemType.Desirable)
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item(ItemType.Conjured) {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };

            app.UpdateQuality();

            PrintItems(app);
        }

        private static void PrintItems(Program app)
        {
            foreach (var item in app.Items)
            {
                System.Console.WriteLine("{0}|{1}|{2}", item.Name, item.Quality, item.SellIn);
            }
        }

        public void UpdateQuality()
        {
            UpdateQuality(Items.ToArray());
        }

        public static void UpdateQuality(Item[] items)
        {
            foreach (var item in items)
            {
                switch (item.ItemType)
                {
                    case ItemType.Perishable:
                        UpdatePerishableItem(item);
                        break;
                    case ItemType.Desirable:
                        UpdateDesirableEventItem(item);
                        break;
                    case ItemType.Legendary:
                        UpdateLegendaryItem(item);
                        break;
                    case ItemType.Aging:
                        UpdateAgeingItem(item);
                        break;
                    case ItemType.Conjured:
                        UpdateConjuredItem(item);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void UpdateConjuredItem(Item item)
        {
            DecreaseQuality(item);
            DecreaseQuality(item);
            DecreaseSellIn(item);
        }

        private static void UpdateAgeingItem(Item item)
        {
            IncreaseQuality(item);
            DecreaseSellIn(item);

            if (HasPassedSellByDate(item))
            {
                IncreaseQuality(item);
            }
        }

        private static void UpdateDesirableEventItem(Item item)
        {
            // Tickets are more valuable when an event is closer
            if (item.SellIn <= 10)
            {
                IncreaseQuality(item);
            }

            // They increase in value much more the closer we are to the event
            if (item.SellIn <= 5)
            {
                IncreaseQuality(item);
            }

            IncreaseQuality(item);
            DecreaseSellIn(item);

            if (HasPassedSellByDate(item))
            {
                item.Quality = 0;
            }
        }

        private static void UpdateLegendaryItem(Item item)
        {
        }

        private static void UpdatePerishableItem(Item item)
        {
            DecreaseQuality(item);
            DecreaseSellIn(item);

            if (HasPassedSellByDate(item))
            {
                DecreaseQuality(item);
            }
        }

        private static bool HasPassedSellByDate(Item item)
        {
            return item.SellIn < 0;
        }

        private static void IncreaseQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
        }

        private static void DecreaseQuality(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
        }

        private static void DecreaseSellIn(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }
    }
}