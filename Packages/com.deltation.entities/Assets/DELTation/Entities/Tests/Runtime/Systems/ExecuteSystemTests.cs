using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace DELTation.Entities.Tests.Runtime.Systems
{
	public class ExecuteSystemTests
	{
		[Test]
		public void GivenUpdateSystem_WhenExecutingWithNullEntity_ThenThrowsArgumentNullException()
		{
			// Arrange
			var executeSystem = new GameObject().AddComponent<EmptyExecuteSystem>();

			// Act

			// Assert
			executeSystem.Invoking(s => s.Execute(null, 0f))
				.Should()
				.Throw<ArgumentNullException>();
		}

		[Test]
		public void GivenUpdateSystem_WhenExecuting_ThenExecutedOnce()
		{
			// Arrange
			var executeSystem = new GameObject().AddComponent<EmptyExecuteSystem>();
			var entity = Substitute.For<IEntity>();

			// Act
			executeSystem.Execute(entity, 0f);

			// Assert
			executeSystem.ExecutedTimes.Should().Be(1);
		}
	}
}