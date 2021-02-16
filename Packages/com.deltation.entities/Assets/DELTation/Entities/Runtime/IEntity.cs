using UnityEngine;

namespace DELTation.Entities
{
	public interface IEntity
	{
		GameObject GameObject { get; }
		T Get<T>() where T : class;
		T[] GetMany<T>() where T : class;
	}
}