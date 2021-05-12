using JetBrains.Annotations;

namespace DELTation.Entities.Systems.Init
{
    public interface IInitSystem : ISystem
    {
        void Init([NotNull] IEntity entity);
    }
}