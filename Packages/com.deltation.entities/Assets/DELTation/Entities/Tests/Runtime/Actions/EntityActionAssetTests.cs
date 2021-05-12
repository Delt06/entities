using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DELTation.Entities.Tests.Runtime.Actions
{
    [TestFixture]
    internal class EntityActionAssetTests : CachedEntityTestsBase
    {
        private CounterEntityActionAsset _action;

        public override void SetUp()
        {
            base.SetUp();
            _action = ScriptableObject.CreateInstance<CounterEntityActionAsset>();
        }

        public override void TearDown()
        {
            base.TearDown();

            if (_action)
                Object.Destroy(_action);
        }

        [Test]
        public void GivenAction_WhenInvokingOnNullEntity_ThenThrowsArgumentNullException()
        {
            // Arrange

            // Act

            // Assert
            Assert.That(() => _action.Invoke(null), Throws.ArgumentNullException);
        }

        [Test]
        public void GivenAction_WhenInvokingOnEntity_ThenInvokedOneTime()
        {
            // Arrange

            // Act
            _action.Invoke(CachedEntity);

            // Assert
            Assert.That(_action.Value, Is.EqualTo(1));
        }
    }
}