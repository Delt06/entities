using JetBrains.Annotations;

namespace DELTation.Entities.Actions
{
    public interface IEntityAction
    {
        void Invoke([NotNull] IEntity entity);
    }
}