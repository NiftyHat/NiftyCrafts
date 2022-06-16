using System;

namespace NiftyFramework.NiftyCrafts
{
    public interface ICraftyCollectable : IComparable<ICraftyCollectable>
    {
        string Id { get; }
    }
}
