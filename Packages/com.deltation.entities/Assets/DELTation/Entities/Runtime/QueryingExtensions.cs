using System;
using JetBrains.Annotations;
using UnityEngine;

namespace DELTation.Entities
{
	public static class QueryingExtensions
	{
		public static bool TryGetInEntity<T>([NotNull] this GameObject target, out T component) where T : class
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			component = default;
			return target.TryGetComponent(out IEntity entity) && entity.TryGet(out component);
		}

		public static bool TryGetInEntity<T>([NotNull] this Component target, out T component) where T : class
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return target.gameObject.TryGetInEntity(out component);
		}
	}
}