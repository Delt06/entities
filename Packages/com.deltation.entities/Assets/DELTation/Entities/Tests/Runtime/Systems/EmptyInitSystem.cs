using DELTation.Entities.Systems.Init;

namespace DELTation.Entities.Tests.Runtime.Systems
{
	public class EmptyInitSystem : InitSystemBase
	{
		protected override void OnInit(IEntity entity) { }
	}
}