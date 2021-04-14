using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Movement.Components;
using UnityEngine;

namespace Movement.Systems
{
	public class MovementSystem : UpdateSystemComponentBase
	{
		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			var shipInput = entity.Get<MovementInput>();
			var direction = shipInput.Value;
			if (direction.sqrMagnitude >= 1f)
				direction.Normalize();
			var motion = direction * entity.Get<Speed>().Value * deltaTime;
			var motion3D = new Vector3(motion.x, 0f, motion.y);
			var currentPosition = entity.Get<Transform>().position;
			var newPosition = currentPosition + motion3D;
			entity.Get<NewPosition>().Value = newPosition;
		}
	}
}