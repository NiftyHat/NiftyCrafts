using System;
using System.Collections.Generic;
using NiftyFramework.NiftyCrafts.Slot;

namespace NiftyFramework.NiftyCrafts
{
    
    public interface ICraftySlotSet<TCollectable>
    {
        TSlotType GetFirstSlot<TSlotType>(TCollectable collectable) where TSlotType : ICraftySlot<TCollectable>;
        IEnumerable<TSlotType> GetSlots<TSlotType>(TCollectable collectable) where TSlotType : ICraftySlot<TCollectable>;
        IEnumerable<TSlotType> GetSlots<TSlotType>(Func<ICraftySlot<TCollectable>, bool> predicate) where TSlotType : ICraftySlot<TCollectable>;

        void Add(ICraftySlot<TCollectable> slot);
        void Remove(ICraftySlot<TCollectable> slot);

        event Action<ICraftySlotSet<TCollectable>> OnChange;
    }
}