using JetBrains.Annotations;

namespace DELTation.Entities.Systems.Execute
{
    public interface IFixedUpdateSystem : IExecuteSystem
    {
        new void Execute([NotNull] IEntity entity, float deltaTime);
    }
}