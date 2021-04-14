using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Movement.Components;
using UnityEngine;

namespace Movement.Systems
{
	public class KeyboardInputMovementSystem : UpdateSystemComponentBase
	{
		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			var input = entity.Get<MovementInput>();
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			input.Value = new Vector2(horizontal, vertical);
		}
	}
}