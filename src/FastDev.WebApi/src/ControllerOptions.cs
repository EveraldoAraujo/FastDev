using FastDev.Notifications;
using FastDev.Notifications.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FastDev.WebApi.Controllers;
public class ControllerOptions<T>
{
    internal ControllerOptions()
    {
    }

    internal Func<HttpContext, T, Task<INotification?>>[]? PostRulesFunctions = null;
    internal Func<HttpContext, T, Task<INotification?>>[]? PutRulesFunctions = null;
    internal Func<HttpContext, T, Task<INotification?>>[]? DeleteRulesFunctions = null;

    public ControllerOptions<T> CheckRulesOnPost(params Func<HttpContext, T, Task<INotification?>>[] rules)
    {
        this.PostRulesFunctions = rules;
        return this;
    }

    public ControllerOptions<T> CheckRulesOnPut(params Func<HttpContext, T, Task<INotification?>>[] rules)
    {
        this.PutRulesFunctions = rules;
        return this;
    }

    public ControllerOptions<T> CheckRulesOnDelete(params Func<HttpContext, T, Task<INotification?>>[] rules)
    {
        this.DeleteRulesFunctions = rules;
        return this;
    }

    public ControllerOptions<T> UsePostReturnFields()
    {
        this._usePostReturnFields = true;
        return this;
    }

    public ControllerOptions<T> UseGetFields()
    {
        this._useGetFields = true;
        return this;
    }

    public ControllerOptions<T> UseGetByIdFields()
    {
        this._useGetByIdFields = true;
        return this;
    }

    public ControllerOptions<T> OmmitGet()
    {
        this._useGet = false;
        return this;
    }

    public ControllerOptions<T> OmmitGetById()
    {
        this._useGetById = false;
        return this;
    }

    public ControllerOptions<T> OmmitPost()
    {
        this._usePost = false;
        return this;
    }

    public ControllerOptions<T> OmmitPut()
    {
        this._usePut = false;
        return this;
    }

    public ControllerOptions<T> OmmitDelete()
    {
        this._useDelete = false;
        return this;
    }
    public ControllerOptions<T> OmmitPatch()
    {
        this._usePatch = false;
        return this;
    }

    internal bool _useGetFields = false;
    internal bool _useGetByIdFields = false;
    internal bool _usePostReturnFields = false;
    internal bool _useGet = true;
    internal bool _useGetById = true;
    internal bool _usePost = true;
    internal bool _usePut = true;
    internal bool _useDelete = true;
    internal bool _usePatch = true;
}

