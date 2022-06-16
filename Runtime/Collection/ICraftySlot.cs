using System;

namespace NiftyFramework.NiftyCrafts
{
    public interface ICraftySlot<TCollectable>
    {
        TCollectable Collectable { get; }
        event Action<ICraftySlot<TCollectable>> OnSlotChanged;
        
        bool IsEmpty();
        bool Contains(TCollectable collectable);
        void GetProperty<TCraftyProperty>() where TCraftyProperty : ICraftyCollectableProperty;
    }
}