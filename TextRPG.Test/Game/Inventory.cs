using NSubstitute;
using NUnit.Framework;

namespace TextRPG.Game.Test
{
    [TestFixture(typeof(Inventory))]
    public class InventoryTest<InventoryType> where InventoryType : IInventory, new()
    {
        IInventory EmptyInventory, OneItemInventory, TwoUniqueItemsInventory, TwoNonUniqueItemsInventory;
        Item TestItem, TestItem2;

        [SetUp]
        public void Setup()
        {
            EmptyInventory = new InventoryType();
            OneItemInventory = new InventoryType();
            TwoUniqueItemsInventory = new InventoryType();
            TwoNonUniqueItemsInventory = new InventoryType();

            TestItem = Substitute.For<Item>();
            TestItem.Name = "Test Item";
            TestItem.PluralName = "Test Items";
            TestItem.CanUse(Arg.Any<IUsageContext>()).ReturnsForAnyArgs(false);

            TestItem2 = Substitute.For<Item>();
            TestItem2.Name = "Test2 Item";
            TestItem2.PluralName = "Test2 Items";
            TestItem2.CanUse(Arg.Any<IUsageContext>()).ReturnsForAnyArgs(false);

            OneItemInventory.Add(TestItem);
            TwoUniqueItemsInventory.Add(TestItem);
            TwoUniqueItemsInventory.Add(TestItem2);
            TwoNonUniqueItemsInventory.Add(TestItem);
            TwoNonUniqueItemsInventory.Add(TestItem);
        }

        [Test]
        public void ItemCanBeCheckedForItsPresenceInInventory()
        {
            Assert.False(EmptyInventory.Has(TestItem));
            Assert.False(EmptyInventory.Has(TestItem2));

            Assert.That(OneItemInventory.Has(TestItem));
            Assert.False(OneItemInventory.Has(TestItem2));

            Assert.That(TwoUniqueItemsInventory.Has(TestItem));
            Assert.That(TwoUniqueItemsInventory.Has(TestItem2));

            Assert.That(TwoNonUniqueItemsInventory.Has(TestItem));
            Assert.False(TwoNonUniqueItemsInventory.Has(TestItem2));
        }

        [Test]
        public void ItemsCanBeAddedToInventory()
        {
            EmptyInventory.Add(TestItem);
            Assert.That(EmptyInventory.Has(TestItem));
        }

        [Test]
        public void ItemsCanBeRemovedFromInventory()
        {
            OneItemInventory.Remove(TestItem);
            Assert.False(OneItemInventory.Has(TestItem));
        }

        [Test]
        public void ItemsCanBeRemovedFromInventoryWithPredicate()
        {
            TwoUniqueItemsInventory.RemoveAll(item => item.Name.StartsWith("Test"));
            Assert.Zero(TwoUniqueItemsInventory.Size);

            TwoNonUniqueItemsInventory.Remove(item => item.Name.StartsWith("Test"));
            Assert.AreEqual(1, TwoNonUniqueItemsInventory.Size);
        }

        [Test]
        public void InventoryHasTotalItemCount()
        {
            Assert.AreEqual(0, EmptyInventory.Size);
            Assert.AreEqual(1, OneItemInventory.Size);
            Assert.AreEqual(2, TwoUniqueItemsInventory.Size);
            Assert.AreEqual(2, TwoNonUniqueItemsInventory.Size);
        }

        [Test]
        public void ItemsAreCountable()
        {
            Assert.AreEqual(0, EmptyInventory.Count(TestItem));
            Assert.AreEqual(0, EmptyInventory.Count(TestItem2));

            Assert.AreEqual(1, OneItemInventory.Count(TestItem));
            Assert.AreEqual(0, OneItemInventory.Count(TestItem2));

            Assert.AreEqual(1, TwoUniqueItemsInventory.Count(TestItem));
            Assert.AreEqual(1, TwoUniqueItemsInventory.Count(TestItem2));

            Assert.AreEqual(2, TwoNonUniqueItemsInventory.Count(TestItem));
            Assert.AreEqual(0, TwoNonUniqueItemsInventory.Count(TestItem2));
        }

        [Test]
        public void ItemsAreCountableByPredicate()
        {
            Assert.AreEqual(2, TwoNonUniqueItemsInventory.Count(item => item.Name.StartsWith("Test")));
            Assert.AreEqual(1, TwoUniqueItemsInventory.Count(item => item.Name.StartsWith("Test2")));
            Assert.AreEqual(0, TwoNonUniqueItemsInventory.Count(item => item.CanUse(null)));
        }


    }
}