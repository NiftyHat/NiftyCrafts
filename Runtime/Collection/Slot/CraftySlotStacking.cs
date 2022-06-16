using System;

namespace NiftyFramework.NiftyCrafts.Slot
{
    public class CraftySlotStacking<TCollectable> : ICraftySlot<TCollectable> where TCollectable : class, ICraftyCollectable, IComparable<TCollectable>
    {
        private double _amount;
        public double Amount => _amount;

        private TCollectable _collectable;
        public TCollectable Collectable => _collectable;
        public event Action<ICraftySlot<TCollectable>> OnSlotChanged;
        public event Action<TCollectable, double, double> OnAmountChanged;

        private double _max = double.MaxValue;

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

        public CraftySlotStacking(TCollectable collectable, double amount = 1)
        {
            _collectable = collectable;
            _amount = amount;
        }
        
        public CraftySlotStacking()
        {
            _collectable = default(TCollectable);
            _amount = 0;
        }

        public bool ContainsAmount(double count)
        {
            return count > _amount;
        }

        public bool HasSpace(double count)
        {
            return _amount + count < _max;
        }

        public double RemainingSpace()
        {
            return _max - _amount;
        }

        /// <summary>
        /// Sets the max amount that can be stored in the slot. If clamp is used the current amount will be reduced to max.
        /// </summary>
        /// <param name="max"></param>
        /// <param name="clamp"></param>
        public void SetMax(double max, bool clamp)
        {
            _max = max;
            if (clamp && _amount > _max)
            {
                double changeAmount = _max - _amount;
                _amount = _max;
                OnAmountChanged?.Invoke(_collectable, _max, changeAmount);
            }
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

        public double Add(double count)
        {
            if (_collectable == null)
            {
                throw new CraftyInventoryException("CraftyCollectableSlotStackable trying to add " + count + " when Collectable is null");
            }
            if (!HasSpace(count))
            {
                throw new CraftyInventoryException("CraftyCollectableSlotStackable trying to total of " + (_amount + count) + " of " + _collectable + " max amount is " + _max);
            }
            _amount += count;
            OnSlotChanged?.Invoke(this);
            OnAmountChanged?.Invoke(_collectable, _amount, count);
            return Amount;
        }
        
        public double Add(ref double count)
        {
            if (_collectable == null)
            {
                throw new CraftyInventoryException("CraftyCollectableSlotStackable trying to add " + count + " when Collectable is null");
            }
            if (count + _amount > _max)
            {
                count = _max - _amount;
            }
            _amount += count;
            OnSlotChanged?.Invoke(this);
            OnAmountChanged?.Invoke(_collectable, _amount, count);
            return Amount;
        }
    }
}