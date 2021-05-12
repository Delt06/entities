using NUnit.Framework;
using UnityEngine;

namespace DELTation.Entities.Tests.Runtime.Ids
{
    internal class EntityIdTests
    {
        [Test]
        public void GivenEntityIdWithEntity_WhenGettingEntity_ThenTheEntityIsReturned()
        {
            // Arrange
            var gameObject = new GameObject();
            var id = gameObject.AddComponent<TestEntityId>();
            var entity = gameObject.AddComponent<CachedEntity>();

            // Act
            var foundEntity = id.Entity;

            // Assert
            Assert.That(foundEntity, Is.EqualTo(entity));
        }

        [Test]
        public void GivenEntityIdWithoutEntity_WhenGettingEntity_ThenThrowsInvalidOperationException()
        {
            // Arrange
            var gameObject = new GameObject();
            var id = gameObject.AddComponent<TestEntityId>();

            // Act

            // Assert
            Assert.That(() => id.Entity, Throws.InvalidOperationException);
        }
    }
}