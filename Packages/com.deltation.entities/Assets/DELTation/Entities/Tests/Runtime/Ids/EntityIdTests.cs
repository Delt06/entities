using NUnit.Framework;
using UnityEngine;

namespace DELTation.Entities.Tests.Runtime.Ids
{
	internal class EntityIdTests
	{
		[Test]
		public void EntityId_HasEntity_ReturnsIt()
		{
			var gameObject = new GameObject();
			var id = gameObject.AddComponent<TestEntityId>();
			var entity = gameObject.AddComponent<CachedEntity>();

			var foundEntity = id.Entity;

			Assert.That(foundEntity, Is.EqualTo(entity));
		}

		[Test]
		public void EntityId_DoesNotHaveEntity_ThrowsInvalidOperationException()
		{
			var gameObject = new GameObject();
			var id = gameObject.AddComponent<TestEntityId>();

			Assert.That(() => id.Entity, Throws.InvalidOperationException);
		}
	}
}