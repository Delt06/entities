using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Shooting.Components;
using UnityEngine;

namespace Shooting.Systems
{
	public class ShootingCooldownSetterSystem : UpdateSystemComponentBase
	{
		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			var cooldown = entity.Get<ShootingCooldown>();

			if (entity.Tags.Contains<OnShotEvent>())
				cooldown.RemainingTime = Mathf.Max(cooldown.RemainingTime, cooldown.Cooldown);
		}
	}
}