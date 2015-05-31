using System;
using System.Collections.Generic;

namespace ParallelProcessing.BusinessLogic
{
    public struct ItemToProcess : IEquatable<ItemToProcess>
    {
        public readonly KeyValuePair<int, string> Data;
        public readonly Action<string, KeyValuePair<int, string>> Callback;

        public ItemToProcess(KeyValuePair<int, string> data, Action<string, KeyValuePair<int, string>> callback)
            : this()
        {
            Callback = callback;
            Data = data;
        }

        public bool Equals(ItemToProcess other)
        {
            return Data.Key.Equals(other.Data.Key);
        }

        public override bool Equals(object obj)
        {
            return Data.Key.Equals((int)obj);
        }

        public override int GetHashCode()
        {
            return Data.Key.GetHashCode();
        }
    }
}
