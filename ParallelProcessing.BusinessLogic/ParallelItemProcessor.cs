using System.Threading.Tasks;

namespace ParallelProcessing.BusinessLogic
{
    public class ParallelItemProcessor : ItemProcessor
    {
        public override void ProcessItems()
        {
            Parallel.ForEach(Queue, (itm) => itm.Callback(string.Format("Processing Complete: {0}", itm.Data), itm.Data));
        }
    }
}
