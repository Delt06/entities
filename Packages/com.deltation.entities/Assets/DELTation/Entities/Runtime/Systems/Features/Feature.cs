using System;
using System.Collections.Generic;
using DELTation.Entities.Systems.Execute;
using DELTation.Entities.Systems.Init;
using JetBrains.Annotations;

namespace DELTation.Entities.Systems.Features
{
	public abstract class Feature : IInitSystem, IUpdateSystem, IFixedUpdateSystem, ILateUpdateSystem
	{
		public virtual bool ShouldBeExecuted(IEntity entity) => true;

		protected void Add([NotNull] IInitSystem system)
		{
			if (system == null) throw new ArgumentNullException(nameof(system));
			_initSystems.Add(system);
		}

		protected void Add([NotNull] IUpdateSystem system)
		{
			if (system == null) throw new ArgumentNullException(nameof(system));
			_updateSystems.Add(system);
		}

		protected void Add([NotNull] IFixedUpdateSystem system)
		{
			if (system == null) throw new ArgumentNullException(nameof(system));
			_fixedUpdateSystems.Add(system);
		}

		protected void Add([NotNull] ILateUpdateSystem system)
		{
			if (system == null) throw new ArgumentNullException(nameof(system));
			_lateUpdateSystems.Add(system);
		}

		public void Init(IEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			_initSystems.ExecuteAllThatShould(entity);
		}

		void IUpdateSystem.Execute(IEntity entity, float deltaTime)
		{
			ExecuteUpdate(entity, deltaTime);
		}

		public void ExecuteUpdate(IEntity entity, float deltaTime)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			_updateSystems.ExecuteAllThatShould(entity, deltaTime);
		}

		void ILateUpdateSystem.Execute(IEntity entity, float deltaTime)
		{
			ExecuteLateUpdate(entity, deltaTime);
		}

		public void ExecuteLateUpdate(IEntity entity, float deltaTime)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			_lateUpdateSystems.ExecuteAllThatShould(entity, deltaTime);
		}

		void IFixedUpdateSystem.Execute(IEntity entity, float deltaTime)
		{
			ExecuteFixedUpdate(entity, deltaTime);
		}

		void IExecuteSystem.Execute(IEntity entity, float deltaTime)
		{
			throw new InvalidOperationException("The exact execute system type is ambiguous.");
		}

		public void ExecuteFixedUpdate(IEntity entity, float deltaTime)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			_fixedUpdateSystems.ExecuteAllThatShould(entity, deltaTime);
		}

		private readonly List<IInitSystem> _initSystems = new List<IInitSystem>();
		private readonly List<IUpdateSystem> _updateSystems = new List<IUpdateSystem>();
		private readonly List<IFixedUpdateSystem> _fixedUpdateSystems = new List<IFixedUpdateSystem>();
		private readonly List<ILateUpdateSystem> _lateUpdateSystems = new List<ILateUpdateSystem>();
	}
}