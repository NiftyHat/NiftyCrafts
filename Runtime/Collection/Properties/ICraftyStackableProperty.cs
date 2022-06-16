using System;

namespace NiftyFramework.NiftyCrafts
{
    public interface ICraftyStackableProperty<TStackNumber> : ICraftySlotProperty where TStackNumber : IComparable<ulong>
    {
        TStackNumber amount { get; }
        void Combine(ICraftyStackableProperty<TStackNumber> stackable);
    }
}