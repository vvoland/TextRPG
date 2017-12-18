using System;
using NUnit.Framework;

namespace TextRPG.Game.Test
{
    [TestFixture]
    public class EntityTest
    {
        Entity SingularNameOnly;
        Entity BothNames;
        Entity PluralSetBeforeSingular;

        [OneTimeSetUp]
        public void Setup()
        {
            SingularNameOnly = new Entity();
            SingularNameOnly.Name = "TestEntity";

            BothNames = new Entity();
            BothNames.Name = "TestEntity";
            BothNames.PluralName = "TestEntities";

            PluralSetBeforeSingular = new Entity();
            PluralSetBeforeSingular.PluralName = "TestEntities";
            PluralSetBeforeSingular.Name = "TestEntity";
        }

        [Test]
        public void IfSingularNameNotSet_ThenPluralNameIsEqualToSingular()
        {
            Assert.AreEqual("TestEntity", SingularNameOnly.Name);
            Assert.AreEqual(SingularNameOnly.Name, SingularNameOnly.PluralName);
        }

        [Test]
        public void IfBothNamesSet_ThenTheyAreIndependent()
        {
            Assert.AreEqual("TestEntity", BothNames.Name);
            Assert.AreEqual("TestEntities", BothNames.PluralName);
        }

        [Test]
        public void IfPluralSetBeforeSingular_ThenItsOK()
        {
            Assert.AreEqual("TestEntity", PluralSetBeforeSingular.Name);
            Assert.AreEqual("TestEntities", PluralSetBeforeSingular.PluralName);
        }
        
    }
}