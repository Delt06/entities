using UnityEngine;

namespace DELTation.Entities
{
	public interface IEntity
	{
		GameObject GameObject { get; }
		bool TryGet<T>(out T component) where T : class;
		T Get<T>() where T : class;
		T[] GetMany<T>() where T : class;
	}
}