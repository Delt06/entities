namespace DELTation.Entities.Systems.Execute
{
    public abstract class ExecuteSystemBase : ISystem
    {
        public virtual bool ShouldBeExecuted(IEntity entity) => true;

        public abstract void Execute(IEntity entity, float deltaTime);
    }
}