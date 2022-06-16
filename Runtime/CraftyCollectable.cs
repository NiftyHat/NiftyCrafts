using System;
using System.Collections.Generic;

namespace NiftyFramework.NiftyCrafts
{
    public class CraftyCollectable : ICraftyCollectable, IComparable<CraftyCollectable>, IComparable
    {
        private readonly string _id;
        public string Id => _id;

        public CraftyCollectable(string id)
        {
            _id = id;
        }
        
        public int CompareTo(ICraftyCollectable other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(_id, other.Id, StringComparison.Ordinal);
        }

        public int CompareTo(CraftyCollectable other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(_id, other._id, StringComparison.Ordinal);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is CraftyCollectable other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(CraftyCollectable)}");
        }

        public static bool operator <(CraftyCollectable left, CraftyCollectable right)
        {
            return Comparer<CraftyCollectable>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(CraftyCollectable left, CraftyCollectable right)
        {
            return Comparer<CraftyCollectable>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(CraftyCollectable left, CraftyCollectable right)
        {
            return Comparer<CraftyCollectable>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(CraftyCollectable left, CraftyCollectable right)
        {
            return Comparer<CraftyCollectable>.Default.Compare(left, right) >= 0;
        }
    }
}
