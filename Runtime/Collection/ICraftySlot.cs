using System;

namespace NiftyFramework.NiftyCrafts
{
    public interface ICraftySlot<TCollectable>
    {
        TCollectable Collectable { get; }
        event Action<ICraftySlot<TCollectable>> OnSlotChanged;
        
        bool IsEmpty();
        bool Contains(TCollectable collectable);
        bool AcceptsChange(ICraftySlotChange<TCollectable> change);
        void GetProperty<TCraftyProperty>() where TCraftyProperty : ICraftyCollectableProperty;
    }
}