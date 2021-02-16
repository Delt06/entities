using UnityEngine;

namespace DELTation.Entities.Components
{
	public abstract class EntityBase : MonoBehaviour, IEntity
	{
		public GameObject GameObject => gameObject;

		public abstract T Get<T>() where T : class;
		public abstract T[] GetMany<T>() where T : class;
	}
}