using NUnit.Framework;
using UnityEngine;

namespace DELTation.Entities.Tests.Runtime
{
	internal class QueryingExtensionsTests : CachedEntityTestsBase
	{
		[Test]
		public void TryGetInEntity_HasNoEntity_ReturnsFalse()
		{
			var gameObject = new GameObject();

			var hasInEntity = gameObject.TryGetInEntity<Rigidbody>(out _);

			Assert.That(hasInEntity, Is.False);
		}

		[Test]
		public void TryGetInEntity_HasEntityButNoComponent_ReturnsFalse()
		{
			var hasInEntity = CachedEntity.gameObject.TryGetInEntity<Rigidbody>(out _);

			Assert.That(hasInEntity, Is.False);
		}

		[Test]
		public void TryGetInEntity_HasEntityAndComponent_ReturnsTrueAndThatComponent()
		{
			var component = CachedEntity.gameObject.AddComponent<Rigidbody>();

			var hasInEntity = CachedEntity.gameObject.TryGetInEntity<Rigidbody>(out var foundComponent);

			Assert.That(hasInEntity);
			Assert.That(foundComponent, Is.EqualTo(component));
		}

		[Test]
		public void TryGetInEntity_GameObjectIsNull_ThrowsArgumentNullException()
		{
			GameObject gameObject = null;

			Assert.That(() => gameObject.TryGetInEntity<Rigidbody>(out _), Throws.ArgumentNullException);
		}
	}
}