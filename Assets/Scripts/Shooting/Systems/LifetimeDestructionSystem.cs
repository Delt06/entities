using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Shooting.Components;

namespace Shooting.Systems
{
	public class LifetimeDestructionSystem : UpdateSystemComponentBase
	{
		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			var lifetime = entity.Get<Lifetime>();
			lifetime.TimeLeft -= deltaTime;
			if (lifetime.TimeLeft <= 0f)
				Destroy(entity.GameObject);
		}
	}
}