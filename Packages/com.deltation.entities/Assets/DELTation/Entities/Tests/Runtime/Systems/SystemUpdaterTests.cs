using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DELTation.Entities.Systems;
using DELTation.Entities.Systems.Execute;
using DELTation.Entities.Systems.Init;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static DELTation.Entities.Tests.Runtime.Systems.SystemTestsUtils;

namespace DELTation.Entities.Tests.Runtime.Systems
{
	public class SystemUpdaterTests
	{
		[Test]
		public void GivenUpdaterWithoutEntity_WhenAwaking_ThenBecomesDisabled()
		{
			// Arrange
			Debug.unityLogger.logEnabled = false;
			var systemUpdater = new GameObject().AddComponent<SystemUpdater>();

			// Act

			// Assert
			Debug.unityLogger.logEnabled = true;
			systemUpdater.enabled.Should().Be(false);
		}

		[UnityTest]
		public IEnumerator GivenEnabledInitSystem_WhenStarting_ThenSystemIsExecuted() =>
			CheckThatInitSystemIsExecuted(true);

		[UnityTest]
		public IEnumerator GivenDisabledInitSystem_WhenStarting_ThenSystemIsNotExecuted() =>
			CheckThatInitSystemIsExecuted(false);

		private static IEnumerator CheckThatInitSystemIsExecuted(bool enabled)
		{
			// Arrange
			var updater = CreateObjectWithUpdater();
			var initSystem = CreateSystem<IInitSystem>(enabled);
			updater.InitSystems.Add(initSystem);

			// Act
			yield return null;

			// Assert
			if (enabled)
				initSystem.Received(1).Init(updater.Entity);
			else
				initSystem.DidNotReceive().Init(Arg.Any<IEntity>());
		}

		[UnityTest]
		public IEnumerator GivenEnabledUpdateSystem_WhenUpdating_ThenSystemIsExecuted()
		{
			return CheckThatExecuteSystemIsExecuted(true, u => u.UpdateSystems, (s, e, dt) => s.Execute(e, dt));
		}

		[UnityTest]
		public IEnumerator GivenDisabledUpdateSystem_WhenUpdating_ThenSystemIsNotExecuted()
		{
			return CheckThatExecuteSystemIsExecuted(false, u => u.UpdateSystems, (s, e, dt) => s.Execute(e, dt));
		}

		[UnityTest]
		public IEnumerator GivenEnabledFixedUpdateSystem_WhenUpdating_ThenSystemIsExecuted()
		{
			return CheckThatExecuteSystemIsExecuted(true, u => u.FixedUpdateSystems, (s, e, dt) => s.Execute(e, dt),
				new WaitForFixedUpdate()
			);
		}

		[UnityTest]
		public IEnumerator GivenDisabledFixedUpdateSystem_WhenUpdating_ThenSystemIsNotExecuted()
		{
			return CheckThatExecuteSystemIsExecuted(false, u => u.FixedUpdateSystems, (s, e, dt) => s.Execute(e, dt),
				new WaitForFixedUpdate()
			);
		}

		[UnityTest]
		public IEnumerator GivenEnabledLateUpdateSystem_WhenUpdating_ThenSystemIsExecuted()
		{
			return CheckThatExecuteSystemIsExecuted(true, u => u.LateUpdateSystems, (s, e, dt) => s.Execute(e, dt));
		}

		[UnityTest]
		public IEnumerator GivenDisabledLateUpdateSystem_WhenUpdating_ThenSystemIsNotExecuted()
		{
			return CheckThatExecuteSystemIsExecuted(false, u => u.LateUpdateSystems, (s, e, dt) => s.Execute(e, dt));
		}

		private static IEnumerator CheckThatExecuteSystemIsExecuted<TExecuteSystem>(bool enabled,
			Func<SystemUpdater, List<TExecuteSystem>> getSystems, Action<TExecuteSystem, IEntity, float> executor,
			object awaiter = null)
			where TExecuteSystem : class, IExecuteSystem
		{
			// Arrange
			var updater = CreateObjectWithUpdater();
			var system = CreateSystem<TExecuteSystem>(enabled);
			getSystems(updater).Add(system);

			// Act
			yield return awaiter;

			// Assert
			if (enabled)
				executor(system.Received(1), updater.Entity, Arg.Any<float>());
			else
				executor(system.DidNotReceive(), Arg.Any<IEntity>(), Arg.Any<float>());
		}
	}
}