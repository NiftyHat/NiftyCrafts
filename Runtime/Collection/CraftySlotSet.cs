using System;
using System.Collections.Generic;
using System.Linq;

namespace NiftyFramework.NiftyCrafts
{
    /// <summary>
    /// A group of Collectable slots.
    /// </summary>
    public class CraftySlotSet<TCollectable> : ICraftySlotSet<TCollectable>
    {
        protected List<ICraftySlot<TCollectable>> _slotList;
        protected int _maxSlots;

        public CraftySlotSet(int slotCount)
        {
            _slotList = new List<ICraftySlot<TCollectable>>(slotCount);
        }

        public TSlotType GetFirstSlot<TSlotType>(TCollectable collectable) where TSlotType : ICraftySlot<TCollectable>
        {
             return (TSlotType)_slotList.Select(slot => slot.Contains(collectable));
        }

        public IEnumerable<TSlotType> GetSlots<TSlotType>(TCollectable collectable) where TSlotType : ICraftySlot<TCollectable>
        {
            return _slotList.Select(slot => slot.Contains(collectable)).Cast<TSlotType>().ToArray();
        }

        public IEnumerable<TSlotType> GetSlots<TSlotType>(Func<ICraftySlot<TCollectable>, bool> predicate) where TSlotType : ICraftySlot<TCollectable>
        {
            return _slotList.Select(predicate).Cast<TSlotType>().ToArray();
        }
        
        public TSlotType GetFirstSlot<TSlotType>(Func<ICraftySlot<TCollectable>, bool> predicate) where TSlotType : ICraftySlot<TCollectable>
        {
            return (TSlotType)_slotList.First(predicate);
        }

        public void Add(ICraftySlot<TCollectable> slot)
        {
            if (_slotList.Contains(slot))
            {
                return;
            }
            _slotList.Add(slot);
        }

        public void Remove(ICraftySlot<TCollectable> slot)
        {
            _slotList.Remove(slot);
        }

        public event Action<ICraftySlotSet<TCollectable>> OnChange;
    }

    public class CraftyCollectionException : Exception
    {
        public CraftyCollectionException(string message) : base(message)
        {
            
        }
    }
}

