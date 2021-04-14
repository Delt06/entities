using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Movement.Components;
using UnityEngine;

namespace Movement.Systems
{
	public class PositionApplyingSystem : UpdateSystemComponentBase
	{
		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			entity.Get<Transform>().position = entity.Get<NewPosition>().Value;
		}
	}
}