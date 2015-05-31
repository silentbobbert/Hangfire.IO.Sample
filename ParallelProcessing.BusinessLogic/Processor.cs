using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelProcessing.BusinessLogic
{
    public class Processor
    {
        private readonly int _numberOfItems;
        private readonly ConcurrentDictionary<int, string> _processedItems;

        public Processor(int numberOfItems)
        {
            _processedItems = new ConcurrentDictionary<int, string>();
            _numberOfItems = numberOfItems;
        }

        public void DoWorkInParallel()
        {
            _processedItems.Clear();
            IProcessItems processor = new ParallelItemProcessor();

            ConcurrentDictionary<int, string> items;

            BuildItems(out items);

            Parallel.ForEach(items, (itm) => processor.QueueItem(itm, CallBackAction));
            processor.ProcessItems();
        }
        public void DoWork()
        {
            _processedItems.Clear();

            ConcurrentDictionary<int, string> items;
            BuildItems(out items);

            IProcessItems processor = new SerialItemProcessor();
            foreach (var item in items)
            {
                processor.QueueItem(item, CallBackAction);
            }
            processor.ProcessItems();
        }
        private void BuildItems(out ConcurrentDictionary<int, string> items)
        {
            var numbers = Enumerable.Range(1, _numberOfItems);

            items = new ConcurrentDictionary<int, string>(numbers.Select(n => new KeyValuePair<int, string>(n, string.Format("Cool data: {0}", n))));
        }
        private void CallBackAction(string message, KeyValuePair<int, string> item)
        {
            _processedItems.TryAdd(item.Key, item.Value);
            Debug.WriteLine(message);
        }
    }
}
