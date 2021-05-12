using JetBrains.Annotations;

namespace DELTation.Entities.Systems.Execute
{
    public interface IUpdateSystem : IExecuteSystem
    {
        new void Execute([NotNull] IEntity entity, float deltaTime);
    }
}