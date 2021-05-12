using JetBrains.Annotations;

namespace DELTation.Entities.Systems
{
    public interface ISystem
    {
        bool ShouldBeExecuted([NotNull] IEntity entity);
    }
}