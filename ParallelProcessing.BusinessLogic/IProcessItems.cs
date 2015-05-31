using System;
using System.Collections.Generic;

namespace ParallelProcessing.BusinessLogic
{
    public interface IProcessItems
    {
        void QueueItem(KeyValuePair<int, string> data, Action<string, KeyValuePair<int, string>> callBackAction);

        void ProcessItems();
    }
}
