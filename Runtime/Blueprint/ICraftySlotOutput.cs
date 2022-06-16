namespace NiftyFramework.NiftyCrafts
{
    public interface ICraftySlotOutput<TCollectable> : ICraftySlot<TCollectable>
    {
        void Produce();
        bool CanProduce();
    }
}