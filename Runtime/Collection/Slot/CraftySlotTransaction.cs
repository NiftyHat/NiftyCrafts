using System;

namespace NiftyFramework.NiftyCrafts.Slot
{
    /// <summary>
    /// Transaction slots are intended to be created with a fixed amount of stuff in and disposed when empty. They are intended
    /// for temporary storage of amounts of items that eventually get destroyed if unclaimed.
    /// </summary>
    /// <typeparam name="TCollectable"></typeparam>
    public class CraftySlotTransaction<TCollectable> : ICraftySlot<TCollectable> where TCollectable : class, ICraftyCollectable, IComparable<TCollectable>
    {
        private double _amount;
        public double Amount => _amount;
        private TCollectable _collectable;
        public TCollectable Collectable => _collectable;
        public event Action<ICraftySlot<TCollectable>> OnSlotChanged;
        public event Action<TCollectable, double, double> OnAmountChanged;
        
        public bool IsEmpty()
        {
            return _collectable == null || _amount == 0;
        }

        public bool Contains(ICraftyCollectable collectable)
        {
            return Contains(collectable as TCollectable);
        }

        public bool Contains(TCollectable collectable)
        {
            if (_collectable != null && _collectable.Equals(collectable))
            {
                return true;
            }
            return false;
        }

        public void GetProperty<TCraftyProperty>() where TCraftyProperty : ICraftyCollectableProperty
        {
            throw new NotImplementedException();
        }

        public CraftySlotTransaction(TCollectable collectable, double amount = 1)
        {
            _collectable = collectable;
            _amount = amount;
        }
        
        public CraftySlotTransaction(TCollectable collectable)
        {
            _collectable = collectable;
            _amount = 1;
        }

        public bool ContainsAmount(double count)
        {
            return count > _amount;
        }
        
        /// <summary>
        /// Removes the given number of Collectables from the slot. Throws an exception if you remove more than avalible
        /// </summary>
        /// <param name="count">Number of Collectables to remove</param>
        /// <returns>New total amount</returns>
        /// <exception cref="NullReferenceException">Thrown when the Collectable in the slot is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when trying to remove more Collectables than in the stack</exception>
        public double Remove(double count)
        {
            if (_collectable == null)
            {
                throw new NullReferenceException("CraftyCollectableSlotStackable trying to remove " + count + " when Collectable is null");
            }
            if (!ContainsAmount(count))
            {
                throw new ArgumentOutOfRangeException("CraftyCollectableSlotStackable trying to remove " + count + " of " + _collectable + " when slot only contains " + _amount);
            }
            _amount -= count;
            if (_amount <= 0)
            {
                _collectable = default(TCollectable);
            }
            if (OnSlotChanged != null)
            {
                OnSlotChanged(this);
            }
            if (OnAmountChanged != null)
            {
                OnAmountChanged(_collectable, _amount, count);
            }
            return _amount;
        }
        
        public double Remove(ref double count)
        {
            if (_collectable == null)
            {
                throw new CraftyInventoryException("CraftyCollectableSlotStackable trying to remove " + count + " when Collectable is null");
            }
            if (count > _amount)
            {
                count = _amount - count;
            }
            _amount -= count;
            if (_amount <= 0)
            {
                _collectable = default(TCollectable);
            }

            OnSlotChanged?.Invoke(this);
            OnAmountChanged?.Invoke(_collectable, _amount, count);
            return _amount;
        }
    }
}