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
			var executeSystem = new GameObject().AddComponent<EmptyExecuteSystemComponent>();

			// Act

			// Assert
			Assert.That(() => executeSystem.Execute(null, 0f), Throws.ArgumentNullException);
		}

		[Test]
		public void GivenUpdateSystem_WhenExecuting_ThenExecutedOnce()
		{
			// Arrange
			var executeSystem = new GameObject().AddComponent<EmptyExecuteSystemComponent>();
			var entity = executeSystem.gameObject.AddComponent<CachedEntity>();

			// Act
			executeSystem.Execute(entity, 0f);

			// Assert
			Assert.That(executeSystem.ExecutedTimes, Is.EqualTo(1));
		}
	}
}