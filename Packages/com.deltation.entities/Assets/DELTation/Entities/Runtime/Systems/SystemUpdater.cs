using System.Collections.Generic;
using DELTation.Entities.Systems.Execute;
using DELTation.Entities.Systems.Init;
using UnityEngine;

namespace DELTation.Entities.Systems
{
	[RequireComponent(typeof(IEntity))]
	public sealed class SystemUpdater : MonoBehaviour
	{
		public IEntity Entity { get; private set; }

		public readonly List<IInitSystem> InitSystems = new List<IInitSystem>();
		public readonly List<IUpdateSystem> UpdateSystems = new List<IUpdateSystem>();
		public readonly List<IFixedUpdateSystem> FixedUpdateSystems = new List<IFixedUpdateSystem>();
		public readonly List<ILateUpdateSystem> LateUpdateSystems = new List<ILateUpdateSystem>();

		private void Update()
		{
			UpdateSystems.ExecuteAllThatShould(Entity, Time.deltaTime);
		}

		private void FixedUpdate()
		{
			FixedUpdateSystems.ExecuteAllThatShould(Entity, Time.fixedDeltaTime);
		}

		private void LateUpdate()
		{
			LateUpdateSystems.ExecuteAllThatShould(Entity, Time.fixedDeltaTime);
		}

		private void Start()
		{
			InitSystems.ExecuteAllThatShould(Entity);
		}

		private void Awake()
		{
			Entity = GetComponent<IEntity>();

			if (Entity == null)
			{
				Debug.LogError($"No entity was found at {gameObject}. Disabling the updater.", this);
				enabled = false;
				return;
			}

			GetComponentsInChildren(true, InitSystems);
			GetComponentsInChildren(true, UpdateSystems);
			GetComponentsInChildren(true, FixedUpdateSystems);
			GetComponentsInChildren(true, LateUpdateSystems);
		}
	}
}