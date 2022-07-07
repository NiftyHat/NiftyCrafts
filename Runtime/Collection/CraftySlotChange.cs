using System;

namespace NiftyFramework.NiftyCrafts
{
    public interface ICraftySlotChange<TCollectable>
    {
        TCollectable Collectable { get; }
        double Amount { get; }
    }
    
    [Serializable]
    public struct CraftySlotChange<TCollectable> : ICraftySlotChange<TCollectable> where TCollectable : CraftyCollectable
    {
        public TCollectable Collectable { get; }
        public double Amount { get; }

        public CraftySlotChange(TCollectable collectable, double amount)
        {
            Collectable = collectable;
            Amount = amount;
        }
    }
}