using System.Collections.Generic;

namespace NiftyFramework.NiftyCrafts
{
    public class CraftyStackableProperty : ICraftyStackableProperty<ulong>
    {
        protected ulong _max = ulong.MaxValue;
        public ulong amount { get; protected set; }

        public void Combine(ICraftyStackableProperty<ulong> addedStack)
        {
            ulong newTotalAmount = amount + addedStack.amount;
            if (newTotalAmount > _max)
            {
                throw new CraftyCollectionException("CraftyCollectionException new stack size " + newTotalAmount + " exceeds maximum stack of " + _max);
            }
            amount = newTotalAmount;
        }
    }
}