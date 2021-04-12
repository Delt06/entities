using JetBrains.Annotations;

namespace DELTation.Entities.Systems
{
	public abstract class UpdateSystem<T> : UpdateSystemBase where T : class
	{
		public sealed override bool ShouldBeExecuted(IEntity entity) =>
			base.ShouldBeExecuted(entity) && entity.TryGet(out T _);

		public sealed override void Execute(IEntity entity, float deltaTime)
		{
			base.Execute(entity, deltaTime);
			Execute(entity.Get<T>(), entity, deltaTime);
		}

		protected abstract void Execute(T component, [NotNull] IEntity entity, float deltaTime);
	}

	public abstract class UpdateSystem<T1, T2> : UpdateSystemBase where T1 : class where T2 : class
	{
		public sealed override bool ShouldBeExecuted(IEntity entity) => base.ShouldBeExecuted(entity) &&
		                                                                entity.TryGet(out T1 _) &&
		                                                                entity.TryGet(out T2 _);

		public sealed override void Execute(IEntity entity, float deltaTime)
		{
			base.Execute(entity, deltaTime);
			Execute(entity.Get<T1>(), entity.Get<T2>(), entity, deltaTime);
		}

		protected abstract void Execute(T1 component1, T2 component2, [NotNull] IEntity entity, float deltaTime);
	}

	public abstract class UpdateSystem<T1, T2, T3> : UpdateSystemBase where T1 : class where T2 : class where T3 : class
	{
		public sealed override bool ShouldBeExecuted(IEntity entity) => base.ShouldBeExecuted(entity) &&
		                                                                entity.TryGet(out T1 _) &&
		                                                                entity.TryGet(out T2 _) &&
		                                                                entity.TryGet(out T3 _);

		public sealed override void Execute(IEntity entity, float deltaTime)
		{
			base.Execute(entity, deltaTime);
			Execute(entity.Get<T1>(), entity.Get<T2>(), entity.Get<T3>(), entity, deltaTime);
		}

		protected abstract void Execute(T1 component1, T2 component2, T3 component3, [NotNull] IEntity entity,
			float deltaTime);
	}

	public abstract class UpdateSystem<T1, T2, T3, T4> : UpdateSystemBase
		where T1 : class where T2 : class where T3 : class where T4 : class
	{
		public sealed override bool ShouldBeExecuted(IEntity entity) => base.ShouldBeExecuted(entity) &&
		                                                                entity.TryGet(out T1 _) &&
		                                                                entity.TryGet(out T2 _) &&
		                                                                entity.TryGet(out T3 _) &&
		                                                                entity.TryGet(out T4 _);

		public sealed override void Execute(IEntity entity, float deltaTime)
		{
			base.Execute(entity, deltaTime);
			Execute(entity.Get<T1>(), entity.Get<T2>(), entity.Get<T3>(), entity.Get<T4>(), entity, deltaTime);
		}

		protected abstract void Execute(T1 component1, T2 component2, T3 component3, T4 component4,
			[NotNull] IEntity entity, float deltaTime);
	}

	public abstract class UpdateSystem<T1, T2, T3, T4, T5> : UpdateSystemBase where T1 : class
		where T2 : class
		where T3 : class
		where T4 : class
		where T5 : class
	{
		public sealed override bool ShouldBeExecuted(IEntity entity) => base.ShouldBeExecuted(entity) &&
		                                                                entity.TryGet(out T1 _) &&
		                                                                entity.TryGet(out T2 _) &&
		                                                                entity.TryGet(out T3 _) &&
		                                                                entity.TryGet(out T4 _) &&
		                                                                entity.TryGet(out T5 _);

		public sealed override void Execute(IEntity entity, float deltaTime)
		{
			base.Execute(entity, deltaTime);
			Execute(entity.Get<T1>(), entity.Get<T2>(), entity.Get<T3>(), entity.Get<T4>(), entity.Get<T5>(), entity,
				deltaTime
			);
		}

		protected abstract void Execute(T1 component1, T2 component2, T3 component3, T4 component4, T5 component5,
			[NotNull] IEntity entity, float deltaTime);
	}
}