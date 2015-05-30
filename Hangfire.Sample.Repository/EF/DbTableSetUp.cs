using System.Data.Entity;

namespace Hangfire.Sample.Repository.EF
{
    public abstract class DbTableSetUp<T> where T : class, new()
    {
        public abstract void Setup(DbModelBuilder modelBuilder);
    }
}
