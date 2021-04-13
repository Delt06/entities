using JetBrains.Annotations;

namespace DELTation.Entities.Systems.Execute
{
	public interface IExecuteSystem : ISystem
	{
		void Execute([NotNull] IEntity entity, float deltaTime);
	}
}