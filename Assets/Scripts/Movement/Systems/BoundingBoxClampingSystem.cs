using DELTation.Entities;
using DELTation.Entities.Systems.Execute;
using Movement.Components;
using UnityEngine;

namespace Movement.Systems
{
	public class BoundingBoxClampingSystem : UpdateSystemComponentBase
	{
		protected override void OnExecute(IEntity entity, float deltaTime)
		{
			var newPosition = entity.Get<NewPosition>();
			var boundingBox = entity.Get<BoundingBox>();
			var rect = new Rect
			{
				size = boundingBox.Size,
				center = boundingBox.Center,
			};
			newPosition.Value.x = Mathf.Clamp(newPosition.Value.x, rect.xMin, rect.xMax);
			newPosition.Value.z = Mathf.Clamp(newPosition.Value.z, rect.yMin, rect.yMax);
		}
	}
}