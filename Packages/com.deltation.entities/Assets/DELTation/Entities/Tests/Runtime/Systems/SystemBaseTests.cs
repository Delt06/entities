using System;
using System.Collections;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DELTation.Entities.Tests.Runtime.Systems
{
	public class SystemBaseTests
	{
		[UnityTest]
		public IEnumerator GivenAlreadyEnabledInitSystem_WhenCallingShouldBeExecuted_ThenReturnsTrue()
		{
			// Arrange
			var gameObject = new GameObject();
			var system = gameObject.AddComponent<EmptyInitSystem>();
			var entity = Substitute.For<IEntity>();

			// Act
			yield return null;

			// Assert
			system.ShouldBeExecuted(entity).Should().BeTrue();
		}

		[UnityTest]
		public IEnumerator GivenNotEnabledInitSystem_WhenCallingShouldBeExecuted_ThenReturnsFalse()
		{
			// Arrange
			var gameObject = new GameObject();
			gameObject.SetActive(false);
			var system = gameObject.AddComponent<EmptyInitSystem>();
			var entity = Substitute.For<IEntity>();

			// Act
			yield return null;

			// Assert
			system.ShouldBeExecuted(entity).Should().BeFalse();
		}

		[Test]
		public void GivenInitSystem_WhenCallingShouldBeExecutedWithNullEntity_ThenThrowsArgumentNullException()
		{
			// Arrange
			var system = new GameObject().AddComponent<EmptyInitSystem>();

			// Act

			// Assert
			system.Invoking(s => s.ShouldBeExecuted(null))
				.Should()
				.Throw<ArgumentNullException>();
		}
	}
}