using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Shooting.Components;
using UnityEngine;

namespace Shooting.Systems
{
	public class ShootingCooldownCommandRemovalSystem : UpdateSystemComponentBase
	{
		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			var cooldown = entity.Get<ShootingCooldown>();

			var remainingTime = cooldown.RemainingTime;
			if (remainingTime <= 0f) return;

			remainingTime -= deltaTime;
			remainingTime = Mathf.Max(0f, remainingTime);
			cooldown.RemainingTime = remainingTime;
			entity.Tags.RemoveAll<ShootingCommand>();
		}
	}
}