using System;

namespace Hangfire.IO.Sample.BusinessLogic
{
    public interface IWorker
    {
        void DoWork(DateTime queuedAtDateTime, uint upperLimit);
    }
}