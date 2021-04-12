using System;
using UnityEngine;

namespace DELTation.Entities.Systems
{
	[RequireComponent(typeof(IEntity))]
	public sealed class SystemUpdater : MonoBehaviour
	{
		private void Update()
		{
			var deltaTime = Time.deltaTime;

			foreach (var system in _systems)
			{
				if (system.ShouldBeExecuted(_entity))
					system.Execute(_entity, deltaTime);
			}
		}

		private void Awake()
		{
			_systems = GetComponentsInChildren<IUpdateSystem>(true);

			_entity = GetComponent<IEntity>();
			if (_entity == null)
				throw new InvalidOperationException("No entity was found.");
		}

		private IEntity _entity;
		private IUpdateSystem[] _systems;
	}
}