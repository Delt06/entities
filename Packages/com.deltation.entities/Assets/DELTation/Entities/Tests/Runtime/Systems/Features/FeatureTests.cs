using System.Collections.Generic;
using System.Linq;
using DELTation.Entities.Systems;
using DELTation.Entities.Systems.Execute;
using DELTation.Entities.Systems.Features;
using DELTation.Entities.Systems.Init;
using NSubstitute;
using NUnit.Framework;

namespace DELTation.Entities.Tests.Runtime.Systems.Features
{
	public class FeatureTests
	{
		[Test]
		public void GivenEmptyFeature_WhenCallingInit_ThenDoesNothing()
		{
			// Arrange
			var feature = new ConfigurableFeature();
			var entity = Substitute.For<IEntity>();

			// Act
			feature.Init(entity);

			// Assert
		}

		[Test, TestCase(1), TestCase(2), TestCase(5)]
		public void GivenFeatureWithInitSystems_WhenCallingInit_ThenAllAreExecuted(int count)
		{
			// Arrange
			var systems = CreateSystems<IInitSystem>(count);
			var feature = new ConfigurableFeature(systems);
			var entity = Substitute.For<IEntity>();

			// Act
			feature.Init(entity);

			// Assert
			foreach (var system in systems)
			{
				system.Received(1).Init(entity);
			}
		}

		[Test, TestCase(1), TestCase(2), TestCase(5)]
		public void GivenFeatureWithUpdateSystems_WhenCallingExecuteUpdate_ThenAllAreExecuted(int count)
		{
			// Arrange
			var systems = CreateSystems<IUpdateSystem>(count);
			var feature = new ConfigurableFeature(systems);
			var entity = Substitute.For<IEntity>();

			// Act
			const float deltaTime = 0f;
			feature.ExecuteUpdate(entity, deltaTime);

			// Assert
			foreach (var system in systems)
			{
				system.Received(1).Execute(entity, Arg.Any<float>());
			}
		}

		[Test, TestCase(1), TestCase(2), TestCase(5)]
		public void GivenFeatureWithFixedUpdateSystems_WhenCallingExecuteFixedUpdate_ThenAllAreExecuted(int count)
		{
			// Arrange
			var systems = CreateSystems<IFixedUpdateSystem>(count);
			var feature = new ConfigurableFeature(systems);
			var entity = Substitute.For<IEntity>();

			// Act
			const float deltaTime = 0f;
			feature.ExecuteFixedUpdate(entity, deltaTime);

			// Assert
			foreach (var system in systems)
			{
				system.Received(1).Execute(entity, Arg.Any<float>());
			}
		}

		[Test, TestCase(1), TestCase(2), TestCase(5)]
		public void GivenFeatureWithLateUpdateSystems_WhenCallingExecuteLateUpdate_ThenAllAreExecuted(int count)
		{
			// Arrange
			var systems = CreateSystems<ILateUpdateSystem>(count);
			var feature = new ConfigurableFeature(systems);
			var entity = Substitute.For<IEntity>();

			// Act
			const float deltaTime = 0f;
			feature.ExecuteLateUpdate(entity, deltaTime);

			// Assert
			foreach (var system in systems)
			{
				system.Received(1).Execute(entity, Arg.Any<float>());
			}
		}

		[Test, TestCase(1), TestCase(2), TestCase(5)]
		public void GivenFeatureWithMixedSystems_WhenCallingInitAndExecuteUpdates_ThenAllAreExecuted(int count)
		{
			// Arrange
			var initSystems = CreateSystems<IInitSystem>(count);
			var updateSystems = CreateSystems<IUpdateSystem>(count);
			var fixedUpdateSystems = CreateSystems<IFixedUpdateSystem>(count);
			var lateUpdateSystems = CreateSystems<ILateUpdateSystem>(count);
			var allSystem = initSystems.Cast<ISystem>()
				.Concat(updateSystems)
				.Concat(fixedUpdateSystems)
				.Concat(lateUpdateSystems)
				.ToArray();
			var feature = new ConfigurableFeature(allSystem);
			var entity = Substitute.For<IEntity>();

			// Act
			const float deltaTime = 0f;
			feature.Init(entity);
			feature.ExecuteUpdate(entity, deltaTime);
			feature.ExecuteFixedUpdate(entity, deltaTime);
			feature.ExecuteLateUpdate(entity, deltaTime);

			// Assert
			foreach (var system in initSystems)
			{
				system.Received(1).Init(entity);
			}

			foreach (var system in updateSystems)
			{
				system.Received(1).Execute(entity, Arg.Any<float>());
			}

			foreach (var system in fixedUpdateSystems)
			{
				system.Received(1).Execute(entity, Arg.Any<float>());
			}

			foreach (var system in lateUpdateSystems)
			{
				system.Received(1).Execute(entity, Arg.Any<float>());
			}
		}

		private static IReadOnlyList<TSystem> CreateSystems<TSystem>(int count) where TSystem : class, ISystem =>
			Enumerable.Range(0, count)
				.Select(i =>
					{
						var system = Substitute.For<TSystem>();
						system.ShouldBeExecuted(Arg.Any<IEntity>()).Returns(true);
						return system;
					}
				).ToArray();
	}
}