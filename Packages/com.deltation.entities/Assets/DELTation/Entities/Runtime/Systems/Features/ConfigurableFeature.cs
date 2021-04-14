using System;
using System.Collections;
using System.Collections.Generic;
using DELTation.Entities.Systems.Execute;
using DELTation.Entities.Systems.Init;
using JetBrains.Annotations;

namespace DELTation.Entities.Systems.Features
{
	public sealed class ConfigurableFeature : Feature
	{
		public ConfigurableFeature([NotNull] params ISystem[] systems)
		{
			if (systems == null) throw new ArgumentNullException(nameof(systems));

			foreach (var system in systems)
			{
				TryAddSystem(system);
			}
		}

		public ConfigurableFeature([NotNull] IEnumerable<ISystem> systems)
		{
			if (systems == null) throw new ArgumentNullException(nameof(systems));

			foreach (var system in systems)
			{
				TryAddSystem(system);
			}
		}

		private void TryAddSystem(ISystem system)
		{
			switch (system)
			{
				case null:
					throw new ArgumentNullException();
				case IInitSystem initSystem:
					Add(initSystem);
					break;
				case IUpdateSystem updateSystem:
					Add(updateSystem);
					break;
				case IFixedUpdateSystem fixedUpdateSystem:
					Add(fixedUpdateSystem);
					break;
				case ILateUpdateSystem lateUpdateSystem:
					Add(lateUpdateSystem);
					break;
			}
		}
	}
}