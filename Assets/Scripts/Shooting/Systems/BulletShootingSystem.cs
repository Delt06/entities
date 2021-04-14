using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Shooting.Components;
using UnityEngine;

namespace Shooting.Systems
{
	public class BulletShootingSystem : UpdateSystemComponentBase
	{
		[SerializeField] private EntityBase _bulletPrefab = default;

		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			if (!entity.Tags.Contains<ShootingCommand>()) return;

			var position = entity.Get<ShootFrom>().Position;
			Instantiate(_bulletPrefab, position, Quaternion.identity);
			entity.Tags.RemoveAll<ShootingCommand>();
			entity.Tags.Add<OnShotEvent>();
		}
	}
}