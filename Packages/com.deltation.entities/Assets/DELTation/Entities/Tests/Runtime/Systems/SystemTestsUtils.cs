using DELTation.Entities.Systems;
using NSubstitute;
using UnityEngine;

namespace DELTation.Entities.Tests.Runtime.Systems
{
	public static class SystemTestsUtils
	{
		public static SystemUpdater CreateObjectWithUpdater()
		{
			var gameObject = new GameObject();
			gameObject.AddComponent<CachedEntity>();
			var updater = gameObject.AddComponent<SystemUpdater>();
			return updater;
		}

		public static T CreateSystem<T>(bool enabled = true) where T : class, ISystem
		{
			var system = Substitute.For<T>();
			system.ShouldBeExecuted(Arg.Any<IEntity>()).Returns(enabled);
			return system;
		}
	}
}