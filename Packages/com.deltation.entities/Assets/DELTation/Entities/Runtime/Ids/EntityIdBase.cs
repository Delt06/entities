using System;
using JetBrains.Annotations;
using UnityEngine;

namespace DELTation.Entities.Ids
{
	[RequireComponent(typeof(IEntity))]
	public abstract class EntityIdBase : MonoBehaviour, IEntityId
	{
		public IEntity Entity
		{
			get
			{
				if (_entity != null) return _entity;

				_entity = GetComponent<IEntity>();
				return _entity ?? throw new InvalidOperationException($"{this} does not have an entity attached.");
				;
			}
		}

		[CanBeNull] private IEntity _entity;
	}
}