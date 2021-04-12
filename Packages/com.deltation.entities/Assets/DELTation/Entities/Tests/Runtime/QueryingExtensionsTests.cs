using System;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace DELTation.Entities.Tests.Runtime
{
	internal class QueryingExtensionsTests : CachedEntityTestsBase
	{
		[Test]
		public void GivenGameObjectWithNoEntity_WhenTryingToGetInEntity_ThenFalseReturned()
		{
			// Arrange
			var gameObject = new GameObject();

			// Act
			var hasInEntity = gameObject.TryGetInEntity<Rigidbody>(out _);

			// Assert
			hasInEntity.Should().BeFalse();
		}

		[Test]
		public void GivenEntityButNoComponent_WhenTryingToGetInEntity_ThenFalseReturned()
		{
			// Arrange

			// Act
			var hasInEntity = CachedEntity.gameObject.TryGetInEntity<Rigidbody>(out _);

			// Assert
			hasInEntity.Should().BeFalse();
		}

		[Test]
		public void GivenEntityWithComponent_WhenTryingToGetInEntity_ThenTrueAndComponentAreReturned()
		{
			// Arrange
			var component = CachedEntity.gameObject.AddComponent<Rigidbody>();

			// Act
			var hasInEntity = CachedEntity.gameObject.TryGetInEntity<Rigidbody>(out var foundComponent);

			// Assert
			hasInEntity.Should().BeTrue();
			foundComponent.Should().Be(component);
		}

		[Test]
		public void GivenNullGameObject_WhenTryingToGetInEntity_ThenThrowsArgumentNullException()
		{
			// Arrange

			// Act
			GameObject gameObject = null;

			// Assert
			gameObject.Invoking(go => go.TryGetInEntity<Rigidbody>(out _))
				.Should()
				.Throw<ArgumentNullException>();

		}
	}
}