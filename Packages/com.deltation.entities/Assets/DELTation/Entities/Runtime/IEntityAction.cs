using JetBrains.Annotations;

namespace DELTation.Entities
{
	public interface IEntityAction
	{
		void Invoke([NotNull] IEntity entity);
	}
}