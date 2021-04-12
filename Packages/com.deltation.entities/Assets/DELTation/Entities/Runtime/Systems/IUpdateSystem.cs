using JetBrains.Annotations;

namespace DELTation.Entities.Systems
{ 
	public interface IUpdateSystem
	{
		bool ShouldBeExecuted([NotNull] IEntity entity);
		void Execute([NotNull] IEntity entity, float deltaTime);
	}
}