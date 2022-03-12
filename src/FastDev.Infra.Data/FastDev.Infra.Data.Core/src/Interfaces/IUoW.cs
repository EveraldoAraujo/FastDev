namespace FastDev.Infra.Data;
public interface IUoW<TDbContext> where TDbContext: class
{
    Task CommitTransaction();
}