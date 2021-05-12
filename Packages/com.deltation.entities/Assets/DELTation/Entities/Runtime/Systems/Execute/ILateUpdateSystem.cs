using JetBrains.Annotations;

namespace DELTation.Entities.Systems.Execute
{
    public interface ILateUpdateSystem : IExecuteSystem
    {
        new void Execute([NotNull] IEntity entity, float deltaTime);
    }
}