using System;
using System.Collections.Generic;
using DELTation.Entities.Systems.Execute;
using DELTation.Entities.Systems.Init;
using JetBrains.Annotations;
using UnityEngine;

namespace DELTation.Entities.Systems
{
	internal static class SystemsExtensions
	{
		public static void ExecuteAllThatShould<T>([NotNull] this IReadOnlyList<T> systems, [NotNull] IEntity entity,
			float deltaTime) where T : IExecuteSystem
		{
			if (systems == null) throw new ArgumentNullException(nameof(systems));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			for (var i = 0; i < systems.Count; i++)
			{
				var system = systems[i];

				if (system == null)
				{
					Debug.LogWarning($"An execute system at {i} is null. Skipping.");
					continue;
				}

				if (system.ShouldBeExecuted(entity))
					system.Execute(entity, deltaTime);
			}
		}

		public static void ExecuteAllThatShould<T>([NotNull] this IReadOnlyList<T> systems, [NotNull] IEntity entity)
			where T : IInitSystem
		{
			if (systems == null) throw new ArgumentNullException(nameof(systems));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			for (var i = 0; i < systems.Count; i++)
			{
				var system = systems[i];

				if (system == null)
				{
					Debug.LogWarning($"An init system at {i} is null. Skipping.");
					continue;
				}

				if (system.ShouldBeExecuted(entity))
					system.Init(entity);
			}
		}
	}
}