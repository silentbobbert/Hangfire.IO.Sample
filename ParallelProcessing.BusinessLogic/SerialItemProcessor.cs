namespace ParallelProcessing.BusinessLogic
{
    public class SerialItemProcessor : ItemProcessor
    {
        public override void ProcessItems()
        {
            foreach (var itemToProcess in Queue)
            {
                itemToProcess.Callback(string.Format("Processing Complete: {0}", itemToProcess.Data), itemToProcess.Data);
            }
        }
    }
}
