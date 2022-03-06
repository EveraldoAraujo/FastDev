namespace FastDev.Infra.Data;
public interface IUoW
{
    Task CommitTransaction();
}