using DELTation.Entities.Systems.Execute;

namespace DELTation.Entities.Tests.Runtime.Systems
{
    public class EmptyExecuteSystemComponent : UpdateSystemComponentBase
    {
        public int ExecutedTimes { get; private set; }

        protected override void OnExecute(IEntity entity, float deltaTime)
        {
            ExecutedTimes++;
        }
    }
}