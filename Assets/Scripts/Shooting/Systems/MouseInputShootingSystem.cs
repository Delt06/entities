using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Shooting.Components;
using UnityEngine;

namespace Shooting.Systems
{
	public class MouseInputShootingSystem : UpdateSystemComponentBase
	{
		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			if (Input.GetMouseButtonDown(0))
				entity.Tags.Add<ShootingCommand>();
		}
	}
}