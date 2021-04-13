using System;
using JetBrains.Annotations;

namespace DELTation.Entities.Systems.Execute
{
	public abstract class ExecuteSystemBase : SystemBase, IExecuteSystem
	{
		public void Execute(IEntity entity, float deltaTime)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			OnExecute(entity, deltaTime);
		}

		protected abstract void OnExecute([NotNull] IEntity entity, float deltaTime);
	}
}