using System;
using NiftyFramework.NiftyCrafts.Blueprint;

namespace NiftyFramework.NiftyCrafts.Slot
{
    /// <summary>
    /// Output slots can't change the Collectable inside them.
    /// </summary>
    public class CraftySlotFixed<TCollectable> : ICraftySlotOutput<TCollectable>
    {
        private double _amount;
        public double Amount => _amount;

        // TODO - This should be a modifiable value.
        private double _outputAmount = 1;
        public double OutputAmount
        {
            get => _outputAmount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("outputAmount", value, "must be greater than 0");
                }
                _outputAmount = value;
            }
        }
        
        // TODO - should be a modifiable value.
        private double _max = double.MaxValue;

        internal TCollectable _collectable;
        public TCollectable Collectable => _collectable;
        public event Action<ICraftySlot<TCollectable>> OnSlotChanged;
        public event Action<TCollectable, double, double> OnAmountChanged;

        public bool IsEmpty()
        {
            return _collectable == null || _amount == 0;
        }


        public bool CanProduce()
        {
            return HasSpace(OutputAmount);
        }

        public void Produce()
        {
            if (CanProduce())
            {
                Add(OutputAmount);
            }
        }

        public bool Contains(ICraftyCollectable collectable)
        {
            if (_collectable != null && _collectable.Equals(collectable) )
            {
                return true;
            }
            return false;
        }

        public bool AcceptsChange(ICraftySlotChange<TCollectable> change)
        {
            if (!Contains(change.Collectable))
            {
                return false;
            }
            if (change.Amount >= 0) 
            {
                return HasSpace(change.Amount);
            }
            return ContainsAmount(-change.Amount);
        }

        public void GetProperty<TCraftyProperty>() where TCraftyProperty : ICraftyCollectableProperty
        {
            throw new NotImplementedException();
        }

        public CraftySlotFixed(TCollectable collectable, double max = 1, double amount = 0)
        {
            _collectable = collectable;
            _max = max;
            _amount = amount;
        }

        public bool ContainsAmount(double count)
        {
            return count > _amount;
        }
        
        public bool Contains(TCollectable collectable)
        {
            return _collectable.Equals(collectable);
        }

        public bool HasSpace(double count)
        {
            return _amount + count < _max;
        }

        /// <summary>
        /// Removes the given number of Collectables from the slot. Throws an exception if you remove more than available
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
            if (OnSlotChanged != null)
            {
                OnSlotChanged(this);
            }
            if (OnAmountChanged != null)
            {
                OnAmountChanged(_collectable, _amount, count);
            }
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
            if (OnSlotChanged != null)
            {
                OnSlotChanged(this);
            }
            if (OnAmountChanged != null)
            {
                OnAmountChanged(_collectable, _amount, count);
            }
            return Amount;
        }
    }
}