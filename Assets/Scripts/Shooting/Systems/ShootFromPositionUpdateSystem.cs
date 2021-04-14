using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Shooting.Components;
using UnityEngine;

namespace Shooting.Systems
{
	public class ShootFromPositionUpdateSystem : UpdateSystemComponentBase
	{
		[SerializeField] private Transform _shootFrom = default;

		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			var shootFrom = entity.Get<ShootFrom>();
			shootFrom.Position = _shootFrom.position;
			shootFrom.Direction = _shootFrom.forward;
		}
	}
}