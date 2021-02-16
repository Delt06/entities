using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DELTation.Entities.Tests.Runtime
{
	[TestFixture]
	internal class CachedEntityTests : CachedEntityTestsBase
	{
		[Test]
		public void CachedEntity_GetLocalComponent_ReturnsTheComponent()
		{
			var rigidbody = CachedEntity.GameObject.AddComponent<Rigidbody>();

			var returnedRigidbody = CachedEntity.Get<Rigidbody>();
			
			Assert.That(returnedRigidbody, Is.EqualTo(rigidbody));
		}
		
		[Test]
		public void CachedEntity_GetNonExistingComponent_ThrowsException()
		{
			Assert.That(() => CachedEntity.Get<Rigidbody>(), Throws.Exception);
		}

		[Test]
		public void CachedEntity_GetChildComponent_ReturnsTheComponent()
		{
			var child = new GameObject();
			child.transform.parent = CachedEntity.GameObject.transform;
			var rigidbody = child.AddComponent<Rigidbody>();

			var returnedRigidbody = CachedEntity.Get<Rigidbody>();
			
			Assert.That(returnedRigidbody, Is.EqualTo(rigidbody));
		}
		
		[Test]
		public void CachedEntityWithInactiveSearchEnabled_GetInactiveChildComponent_ReturnsTheComponent()
		{
			var child = new GameObject();
			child.transform.parent = CachedEntity.GameObject.transform;
			var rigidbody = child.AddComponent<Rigidbody>();
			child.SetActive(false);
			CachedEntity.SearchInInactiveChildren = true;

			var returnedRigidbody = CachedEntity.Get<Rigidbody>();
			
			Assert.That(returnedRigidbody, Is.EqualTo(rigidbody));
		}
		
		[Test]
		public void CachedEntityWithInactiveSearchDisabled_GetInactiveChildComponent_ThrowsException()
		{
			var child = new GameObject();
			child.transform.parent = CachedEntity.GameObject.transform;
			child.AddComponent<Rigidbody>();
			child.SetActive(false);
			CachedEntity.SearchInInactiveChildren = false;

			Assert.That(() => CachedEntity.Get<Rigidbody>(), Throws.Exception);
		}

		[UnityTest]
		public IEnumerator CachedEntityWithDestroyedComponentsRemoval_GetDestroyedComponent_ThrowsException()
		{
			CachedEntity.RemoveDestroyedComponents = true;
			var rigidbody = CachedEntity.GameObject.AddComponent<Rigidbody>();
			CachedEntity.Get<Rigidbody>();

			Object.Destroy(rigidbody);
			yield return null;

			Assert.That(() => CachedEntity.Get<Rigidbody>(), Throws.Exception);
		}
		
		[UnityTest]
		public IEnumerator CachedEntityWithoutDestroyedComponentsRemoval_GetDestroyedComponent_ReturnsNullObject()
		{
			CachedEntity.RemoveDestroyedComponents = false;
			var rigidbody = CachedEntity.GameObject.AddComponent<Rigidbody>();
			CachedEntity.Get<Rigidbody>();

			Object.Destroy(rigidbody);
			yield return null;
			var returnedRigidbody = CachedEntity.Get<Rigidbody>();

			Assert.That(returnedRigidbody == null);
		}

		[Test]
		public void CachedEntity_GetManyLocalComponents_ReturnsThemAll()
		{
			var colliders = Enumerable.Range(0, 5)
				.Select(i => CachedEntity.GameObject.AddComponent<BoxCollider>())
				.ToArray();

			var returnedColliders = CachedEntity.GetMany<BoxCollider>();
			
			Assert.That(returnedColliders, Is.EquivalentTo(colliders));
		}

		[Test]
		public void CachedEntity_GetManyLocalAndNonLocalComponents_ReturnsThemAll()
		{
			var colliders1 = Enumerable.Range(0, 5)
				.Select(i => CachedEntity.GameObject.AddComponent<BoxCollider>());
			var child = new GameObject();
			child.transform.parent = CachedEntity.transform;
			var colliders2 = Enumerable.Range(0, 5)
				.Select(i => child.AddComponent<BoxCollider>());
			var allColliders = colliders1.Concat(colliders2).ToArray();

			var returnedColliders = CachedEntity.GetMany<BoxCollider>();
			
			Assert.That(returnedColliders, Is.EquivalentTo(allColliders));
		}
		
		[Test]
		public void CachedEntityWithInactiveSearchEnabled_GetManyInactiveLocalAndNonLocalComponents_ReturnsThemAll()
		{
			CachedEntity.SearchInInactiveChildren = true;
			var colliders1 = Enumerable.Range(0, 5)
				.Select(i => CachedEntity.GameObject.AddComponent<BoxCollider>());
			var child = new GameObject();
			child.transform.parent = CachedEntity.transform;
			var colliders2 = Enumerable.Range(0, 5)
				.Select(i => child.AddComponent<BoxCollider>());
			var allColliders = colliders1.Concat(colliders2).ToArray();
			child.SetActive(false);

			var returnedColliders = CachedEntity.GetMany<BoxCollider>();
			
			Assert.That(returnedColliders, Is.EquivalentTo(allColliders));
		}
		
		[Test]
		public void CachedEntityWithInactiveSearchDisabled_GetManyInactiveLocalAndNonLocalComponents_ReturnNone()
		{
			CachedEntity.SearchInInactiveChildren = false;
			var child = new GameObject();
			child.transform.parent = CachedEntity.transform;
			Enumerable.Range(0, 5)
				.Select(i => child.AddComponent<BoxCollider>())
				.ToArray();
			child.SetActive(false);

			var returnedColliders = CachedEntity.GetMany<BoxCollider>();
			
			Assert.That(returnedColliders, Is.EquivalentTo(Enumerable.Empty<BoxCollider>()));
		}
		
		[UnityTest]
		public IEnumerator CachedEntityWithoutDestroyedComponentsRemoval_DestroyOneComponentAndGetAll_ReturnsAllIncludingTheDeleted()
		{
			CachedEntity.RemoveDestroyedComponents = false;
			var colliders = Enumerable.Range(0, 5)
				.Select(i => CachedEntity.GameObject.AddComponent<BoxCollider>())
				.ToArray();
			CachedEntity.GetMany<BoxCollider>();

			Object.Destroy(colliders[1]);
			yield return null;
			var returnedColliders = CachedEntity.GetMany<BoxCollider>();

			Assert.That(returnedColliders, Is.EquivalentTo(colliders));
		}
		
		[UnityTest]
		public IEnumerator CachedEntityWithDestroyedComponentsRemoval_DestroyOneComponentAndGetAll_ReturnsAllExceptTheDeleted()
		{
			CachedEntity.RemoveDestroyedComponents = true;
			var colliders = Enumerable.Range(0, 5)
				.Select(i => CachedEntity.GameObject.AddComponent<BoxCollider>())
				.ToArray();
			CachedEntity.GetMany<BoxCollider>();

			Object.Destroy(colliders[0]);
			yield return null;
			var returnedColliders = CachedEntity.GetMany<BoxCollider>();

			Assert.That(returnedColliders, Is.EquivalentTo(colliders.Skip(1)));
		}

		[Test]
		public void CachedEntityWithComponent_TryGet_ReturnsTrueAndCorrectComponent()
		{
			var rigidbody = CachedEntity.GameObject.AddComponent<Rigidbody>();

			var found = CachedEntity.TryGet<Rigidbody>(out var returnedRigidbody);
			
			Assert.That(found);
			Assert.That(returnedRigidbody, Is.EqualTo(rigidbody));
		}
		
		[Test]
		public void CachedEntityWithoutComponent_TryGet_ReturnsFalse()
		{
			var found = CachedEntity.TryGet<Rigidbody>(out _);
			
			Assert.That(found, Is.False);
		}
	}
}