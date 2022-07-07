namespace NiftyFramework.NiftyCrafts.Blueprint
{
    public interface ICraftySlotOutput<TCollectable> : ICraftySlot<TCollectable>
    {
        void Produce();
        bool CanProduce();
    }
}