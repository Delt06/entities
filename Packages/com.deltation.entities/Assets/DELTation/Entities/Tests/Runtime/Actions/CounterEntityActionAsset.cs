using DELTation.Entities.Actions;

namespace DELTation.Entities.Tests.Runtime.Actions
{
    internal class CounterEntityActionAsset : EntityActionAssetBase
    {
        public int Value { get; private set; }

        protected override void InvokeFor(IEntity entity)
        {
            Value++;
        }
    }
}