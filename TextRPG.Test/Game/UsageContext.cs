using System;
using NSubstitute;
using NUnit.Framework;

namespace TextRPG.Game.Test
{
    [TestFixture(typeof(Item), false, false, true)]
    [TestFixture(typeof(ItemWeapon), true, false, true)]
    public class UsageContextGeneralTest<T> where T : Item
    {
        public class TestParams
        {
            public bool ExpectedCanUse;
            public bool CanUseItem, CanUseItemWeapon;
        };

        private Item Item;
        private TestUsageContext Context;
        private TestParams Params;

        public UsageContextGeneralTest(bool expectedCanUse, bool canUseItem, bool canUseItemWeapon)
        {
            Params = new TestParams() 
            {
                CanUseItem = canUseItem,
                ExpectedCanUse = expectedCanUse,
                CanUseItemWeapon = canUseItemWeapon
            };
        }

        class TestUsageContext : IUsageContext
        {
            private TestParams Params;

            public TestUsageContext(TestParams p)
            {
                Params = p;
            }

            public bool CanUse(Item item)
            {
                return Params.CanUseItem;
            }

            public bool CanUse(ItemWeapon weapon)
            {
                return Params.CanUseItemWeapon;
            }

            public bool CanUse(ItemHealthy healthy)
            {
                throw new NotImplementedException();
            }

            public void Use(Item item)
            {
                throw new System.NotImplementedException();
            }

            public void Use(ItemWeapon weapon)
            {
                throw new System.NotImplementedException();
            }

            public void Use(ItemHealthy healthy)
            {
                throw new NotImplementedException();
            }
        }

        [OneTimeSetUp]
        public void Setup()
        {
            Item = Activator.CreateInstance(typeof(T), new object[] { "Test", "Tests" }) as T;
            Context = new TestUsageContext(Params);
        }

        [Test]
        public void UsageContextUsesAppropriateImplementation()
        {
            Assert.AreEqual(Item.CanUse(Context), Params.ExpectedCanUse);
        }
    }
}