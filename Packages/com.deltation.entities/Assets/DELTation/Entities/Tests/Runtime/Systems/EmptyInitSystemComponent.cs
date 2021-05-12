using DELTation.Entities.Systems.Init;

namespace DELTation.Entities.Tests.Runtime.Systems
{
    public class EmptyInitSystemComponent : InitSystemComponentBase
    {
        protected override void OnInit(IEntity entity) { }
    }
}