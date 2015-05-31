using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ParallelProcessing.BusinessLogic
{
    public abstract class ItemProcessor : IProcessItems
    {
        protected readonly ConcurrentQueue<ItemToProcess> Queue;

        protected ItemProcessor()
        {
            Queue = new ConcurrentQueue<ItemToProcess>();
        }

        public virtual void QueueItem(KeyValuePair<int, string> data, Action<string, KeyValuePair<int, string>> callBackAction)
        {
            Queue.Enqueue(new ItemToProcess(data, callBackAction));
        }

        public abstract void ProcessItems();
    }
}
