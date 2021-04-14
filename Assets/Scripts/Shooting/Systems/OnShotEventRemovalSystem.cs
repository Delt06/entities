using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Shooting.Components;

namespace Shooting.Systems
{
	public class OnShotEventRemovalSystem : UpdateSystemComponentBase
	{
		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			entity.Tags.RemoveAll<OnShotEvent>();
		}
	}
}