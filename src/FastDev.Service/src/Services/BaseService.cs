using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FastDev.Infra.Data;
using FastDev.Notifications.Interfaces;
using FastDev.Notifications;

namespace FastDev.Service;
public class BaseService<T, TRep, TId, TDbContext> : IBaseService<T, TId, TDbContext> where TRep : IRepositoryBase<T, TId> where TId : struct where TDbContext: class
{
    protected readonly TRep _repository;
    protected readonly IUoW<TDbContext> _uow;

    public IEnumerable<INotification> Notifications { get; set; }

    protected void AddNotification(INotification notification)
    {
        this.Notifications.ToList().Add(notification);
    }

    public BaseService(TRep repository, IUoW<TDbContext> uow)
    {
        Notifications = new List<INotification>();
        _repository = repository;
        _uow = uow;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<T?> GetByIdAsync(TId id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<bool> HasByIdAsync(TId id)
    {
        return await _repository.HasByIdAsync(id);
    }

    public async Task<bool> CreateAsync(T obj, bool CommitTransaction = true)
    {
        try
        {
            var result = await _repository.CreateAsync(obj);
            if (result is true && CommitTransaction is true)
                await _uow.CommitTransaction();

            return result;
        }
        catch (Exception e)
        {
            throw e;
            //TODO: Tratar id existente como notificação notificação do tipo application por essa ser uma decisão de aplicação
            return false;
        }
    }
    public async Task<bool> UpdateAsync(T obj, bool CommitTransaction = true)
    {
        try
        {
            var result = await _repository.UpdateAsync(obj);
            if (result is true && CommitTransaction is true)
                await _uow.CommitTransaction();
            return result;
        }
        catch (Exception)
        {
            return false;
        }

    }
    public async Task<bool> DeleteAsync(TId id, bool CommitTransaction = true)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity is null)
            {
                AddNotification(new Notification(typeof(T).Name + " not found"));
                return false;
            }

            var result = await _repository.DeleteAsync(id);
            if (result is true && CommitTransaction is true)
                await _uow.CommitTransaction();

            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }
}