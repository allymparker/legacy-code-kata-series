using System;
using System.Collections.Generic;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    public class ItemUpdaterTests
    {
        [Test]
        public void PerishableItemShouldLowerQualityAndSellInByOne()
        {
            var item = new Item(ItemType.Perishable) { SellIn = 3, Quality = 6 };

            UpdateItem(item);

            Assert.AreEqual(5, item.Quality);
            Assert.AreEqual(2, item.SellIn);
        }

        [Test]
        public void PerishableItemShouldLowerQualityTwiceAsFastWhenSellInIsNegative()
        {
            var item = new Item(ItemType.Perishable) { SellIn = -2, Quality = 6 };

            UpdateItem(item);

            Assert.AreEqual(4, item.Quality);
            Assert.AreEqual(-3, item.SellIn);
        }

        [Test]
        public void PerishableItemShouldLowerQualityTwiceAsFastWhenSellInIsZero()
        {
            var item = new Item(ItemType.Perishable) {SellIn = 0, Quality = 6 };

            UpdateItem(item);

            Assert.AreEqual(4, item.Quality);
            Assert.AreEqual(-1, item.SellIn);
        }

        [Test]
        public void DesirableItemShouldIncreaseQualityTwiceAsFastWhenSellInLessThanElevenDays()
        {
            var item = new Item(ItemType.Desirable) { SellIn = 10, Quality = 6 };

            UpdateItem(item);

            Assert.AreEqual(8, item.Quality);
            Assert.AreEqual(9, item.SellIn);
        }

        [Test]
        public void DesirableItemShouldIncreaseQualityThreeTimesAsFastWhenSellInLessThanSixDays()
        {
            var item = new Item(ItemType.Desirable) { SellIn = 5, Quality = 6 };

            UpdateItem(item);

            Assert.AreEqual(9, item.Quality);
            Assert.AreEqual(4, item.SellIn);
        }

        [Test]
        public void DesirableItemShouldHaveZeroQualityWhenSellInBelowZero()
        {
            var item = new Item(ItemType.Desirable) { SellIn = 0, Quality = 6 };

            UpdateItem(item);

            Assert.AreEqual(0, item.Quality);
            Assert.AreEqual(-1, item.SellIn);
        }

        [Test]
        public void AgingItemQualityIncreasesTwiceAsFastWhenSellInIsLessThanZero()
        {
            var item = new Item(ItemType.Aging) {SellIn = 0, Quality = 6 };

            UpdateItem(item);

            Assert.AreEqual(8, item.Quality);
            Assert.AreEqual(-1, item.SellIn);
        }

        [Test]
        public void PerishableItemQualityIsNeverNegative()
        {
            var item = new Item(ItemType.Perishable) { SellIn = 10, Quality = 0 };

            UpdateItem(item);

            Assert.AreEqual(0, item.Quality);
            Assert.AreEqual(9, item.SellIn);
        }

        [Test]
        public void LegendaryNeverDecreasesInQualityAndNeverHasToBeSold()
        {
            var item = new Item(ItemType.Legendary) {SellIn = 10, Quality = 80 };

            UpdateItem(item);

            Assert.AreEqual(80, item.Quality);
            Assert.AreEqual(10, item.SellIn);
        }

        [Test]
        public void AgingItemQualityCanNeverBeMoreThanFifty()
        {
            var item = new Item(ItemType.Aging) { SellIn = -1, Quality = 50 };

            UpdateItem(item);

            Assert.AreEqual(50, item.Quality);
            Assert.AreEqual(-2, item.SellIn);
        }

        [Test]
        public void ConjuredItemQualityDecreasesTwiceAsFast()
        {
            var item = new Item(ItemType.Conjured) { SellIn = 6, Quality = 10 };

            UpdateItem(item);

            Assert.AreEqual(8, item.Quality);
            Assert.AreEqual(5, item.SellIn);
        }

        [Test]
        public void AgingItemIncreasesQualityWithAge()
        {
            var item = new Item(ItemType.Aging) { SellIn = 1, Quality = 1};
            
            UpdateItem(item);

            Assert.AreEqual(2, item.Quality);
        }

        private void UpdateItem(Item item)
        {
            Program.UpdateQuality(new[] { item });
        }
    }
}