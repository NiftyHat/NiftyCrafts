using System;

namespace NiftyFramework.NiftyCrafts.Slot
{
    public class CraftySlot<TCollectable> : ICraftySlot<TCollectable> where TCollectable: ICraftyCollectable
    {
        private TCollectable _collectable;
        public TCollectable Collectable => _collectable;
        
        public event Action<ICraftySlot<TCollectable>> OnSlotChanged;

        public CraftySlot(TCollectable collectable)
        {
            _collectable = collectable;
        }

        /// <summary>
        /// Is there an Collectable current inside this slot.
        /// </summary>
        /// <returns>true if there is an Collectable</returns>
        public bool IsEmpty()
        {
            return Collectable == null;
        }

        /// <summary>
        /// Does the Collectable inside the slot match the id of the Collectable being passed.
        /// </summary>
        /// <param name="collectable">Collectable you want to check</param>
        /// <returns>true if the slot has an Collectable matching the id of the Collectable being supplied</returns>
        public bool Contains(TCollectable collectable)
        {
            if (_collectable == null)
            {
                return false;
            }
            return _collectable.Id == collectable.Id;
        }

        public bool AcceptsChange(ICraftySlotChange<TCollectable> change)
        {
            if (_collectable != null)
            {
                if (change.Amount == -1)
                {
                    return true;
                }
            }
            return false;
        }
        
        public void SetCollectable(TCollectable collectable)
        {
            if (!IsEmpty())
            {
                throw new CraftyCollectionException(
                    "CraftySlot.SetCollectable: Can only be called on empty slots. Current slot already contains " + collectable.Id);
            }
            _collectable = collectable;
            OnSlotChanged?.Invoke(this);
        }

        public void GetProperty<TCraftyProperty>() where TCraftyProperty : ICraftyCollectableProperty
        {
            throw new System.NotImplementedException();
        }
    }
}