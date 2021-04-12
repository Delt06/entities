using System;
using System.Collections;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace DELTation.Entities.Tests.Runtime
{
	[TestFixture]
	internal class CachedEntityTests : CachedEntityTestsBase
	{
		[Test]
		public void GivenCachedEntity_WhenGettingLocalComponent_ThenTheComponentIsReturned()
		{
			// Arrange
			var rigidbody = CachedEntity.GameObject.AddComponent<Rigidbody>();
			
			// Act
			var returnedRigidbody = CachedEntity.Get<Rigidbody>();

			// Assert
			returnedRigidbody.Should().Be(rigidbody);
		}

		[Test]
		public void GivenCachedEntity_WhenGettingNotExistingComponent_ThenThrowsException()
		{
			// Arrange

			// Act

			// Assert
			CachedEntity.Invoking(e => e.Get<Rigidbody>())
				.Should()
				.Throw<Exception>();
		}

		[Test]
		public void GivenCachedEntity_WhenGettingChildComponent_ThenTheComponentIsReturned()
		{
			// Arrange
			var child = new GameObject();
			child.transform.parent = CachedEntity.GameObject.transform;
			var rigidbody = child.AddComponent<Rigidbody>();

			// Act
			var returnedRigidbody = CachedEntity.Get<Rigidbody>();

			// Assert
			returnedRigidbody.Should().Be(rigidbody);
		}

		[Test]
		public void GivenInactiveSearchEnabled_WhenInactiveChildComponent_ThenTheComponentIsReturned()
		{
			// Arrange
			var child = new GameObject();
			child.transform.parent = CachedEntity.GameObject.transform;
			var rigidbody = child.AddComponent<Rigidbody>();
			child.SetActive(false);
			CachedEntity.SearchInInactiveChildren = true;

			// Act
			var returnedRigidbody = CachedEntity.Get<Rigidbody>();

			// Assert
			returnedRigidbody.Should().Be(rigidbody);
		}

		[Test]
		public void CachedEntityWithInactiveSearchDisabled_GetInactiveChildComponent_ThrowsException()
		{
			var child = new GameObject();
			child.transform.parent = CachedEntity.GameObject.transform;
			child.AddComponent<Rigidbody>();
			child.SetActive(false);
			CachedEntity.SearchInInactiveChildren = false;

			CachedEntity.Invoking(e => e.Get<Rigidbody>())
				.Should()
				.Throw<Exception>();
		}

		[UnityTest]
		public IEnumerator GivenDestroyedComponentsRemovalEnabled_WhenGettingDestroyedComponent_ThenThrowsException()
		{
			// Arrange
			CachedEntity.RemoveDestroyedComponents = true;
			var rigidbody = CachedEntity.GameObject.AddComponent<Rigidbody>();
			CachedEntity.Get<Rigidbody>();

			// Act
			Object.Destroy(rigidbody);
			yield return null;

			// Assert
			CachedEntity.Invoking(e => e.Get<Rigidbody>())
				.Should()
				.Throw<Exception>();
		}

		[UnityTest]
		public IEnumerator GivenDestroyedComponentsRemovalDisabled_WhenGettingDestroyedComponent_ThenNullIsReturned()
		{
			// Arrange
			CachedEntity.RemoveDestroyedComponents = false;
			var rigidbody = CachedEntity.GameObject.AddComponent<Rigidbody>();
			CachedEntity.Get<Rigidbody>();
			
			// Act
			Object.Destroy(rigidbody);
			yield return null;
			var returnedRigidbody = CachedEntity.Get<Rigidbody>();

			// Assert
			Assert.That(returnedRigidbody == null);
		}

		[Test]
		public void GivenCachedEntity_WhenGettingManyLocalComponents_ThenAllOfThemReturned()
		{
			// Arrange
			var colliders = Enumerable.Range(0, 5)
				.Select(i => CachedEntity.GameObject.AddComponent<BoxCollider>())
				.ToArray();

			// Act
			var returnedColliders = CachedEntity.GetMany<BoxCollider>();

			// Assert
			returnedColliders.Should().BeEquivalentTo(colliders);
		}

		[Test]
		public void GivenCachedEntity_WhenGettingManyLocalAndNonLocalComponents_ThenAllOfThemReturned()
		{
			// Arrange
			var colliders1 = Enumerable.Range(0, 5)
				.Select(i => CachedEntity.GameObject.AddComponent<BoxCollider>());
			var child = new GameObject();
			child.transform.parent = CachedEntity.transform;
			var colliders2 = Enumerable.Range(0, 5)
				.Select(i => child.AddComponent<BoxCollider>());
			var allColliders = colliders1.Concat(colliders2).ToArray();

			// Act
			var returnedColliders = CachedEntity.GetMany<BoxCollider>();

			// Assert
			returnedColliders.Should().BeEquivalentTo(allColliders);
		}

		[Test]
		public void GivenInactiveSearchDisabled_WhenGettingManyInactiveLocalAndNonLocalComponents_ThenReturnNone()
		{
			// Arrange
			CachedEntity.SearchInInactiveChildren = false;
			var child = new GameObject();
			child.transform.parent = CachedEntity.transform;
			Enumerable.Range(0, 5)
				.Select(i => child.AddComponent<BoxCollider>())
				.ToArray();
			child.SetActive(false);

			// Act
			var returnedColliders = CachedEntity.GetMany<BoxCollider>();

			// Assert
			returnedColliders.Should().BeEmpty();
		}

		[UnityTest]
		public IEnumerator GivenDestroyedComponentsRemovalDisabled_WhenDestroyingOneComponentAndGettingAll_ThenAllIncludingTheDeletedAreReturned()
		{
			// Arrange
			CachedEntity.RemoveDestroyedComponents = false;
			var colliders = Enumerable.Range(0, 5)
				.Select(i => CachedEntity.GameObject.AddComponent<BoxCollider>())
				.ToArray();
			CachedEntity.GetMany<BoxCollider>();

			// Act
			Object.Destroy(colliders[1]);
			yield return null;
			var returnedColliders = CachedEntity.GetMany<BoxCollider>();

			// Assert
			returnedColliders.Should().BeEquivalentTo(colliders);
		}

		[UnityTest]
		public IEnumerator GivenDestroyedComponentsRemoval_WhenDestroyingOneComponentAndGettingAll_ThenAllExceptTheDeletedAreReturned()
		{
			// Arrange
			CachedEntity.RemoveDestroyedComponents = true;
			var colliders = Enumerable.Range(0, 5)
				.Select(i => CachedEntity.GameObject.AddComponent<BoxCollider>())
				.ToArray();
			CachedEntity.GetMany<BoxCollider>();

			// Act
			Object.Destroy(colliders[0]);
			yield return null;
			var returnedColliders = CachedEntity.GetMany<BoxCollider>();

			// Assert
			returnedColliders.Should().BeEquivalentTo(colliders.Skip(1));
		}


		[Test]
		public void GivenEntityWithComponent_WhenTryingToGet_ThenTrueAndCorrectComponentAreReturned()
		{
			// Arrange
			var rigidbody = CachedEntity.GameObject.AddComponent<Rigidbody>();

			// Act
			var found = CachedEntity.TryGet<Rigidbody>(out var returnedRigidbody);

			// Assert
			found.Should().BeTrue();
			returnedRigidbody.Should().Be(rigidbody);
		}

		[Test]
		public void GivenEntityWithoutComponents_WhenTryingToGet_ThenFalseReturned()
		{
			// Arrange

			// Act
			var found = CachedEntity.TryGet<Rigidbody>(out _);

			// Assert
			found.Should().BeFalse();
		}
	}
}