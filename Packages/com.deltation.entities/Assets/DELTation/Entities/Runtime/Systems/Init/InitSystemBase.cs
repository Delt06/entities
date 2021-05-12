namespace DELTation.Entities.Systems.Init
{
    public abstract class InitSystemBase : IInitSystem
    {
        public bool ShouldBeExecuted(IEntity entity) => true;

        public abstract void Init(IEntity entity);
    }
}