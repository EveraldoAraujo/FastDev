using System.Dynamic;
using System.Linq.Expressions;
using System.Text.Json;
using FastDev.Notifications;
using FastDev.Notifications.Interfaces;
using FastDev.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastDev.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController<T, TId, TDbContext> : ControllerBase where TId : struct where TDbContext: class
{

    public ControllerOptions<T> Options { get; set; }

    public BaseController(Func<ControllerOptions<T>, ControllerOptions<T>>? options = null)
    {
        this.Options = new ControllerOptions<T>();
        if (options is not null)
            this.Options = options.Invoke(new ControllerOptions<T>());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> GetAllAsync([FromServices] IServiceBase<T, TId, TDbContext> service, string? getFields)
    {
        if (!Options._useGetFields && getFields is not null) return NotFound();
        if (!Options._useGet) return NotFound();

        var properties = !string.IsNullOrWhiteSpace(getFields) ? getFields.Split(',') : null;

        var result = await service.GetAllAsync();

        if (result.Count() == 0)
            return NoContent();

        if (properties is null) return Ok(result);

        return Ok(result.Select((i) =>
        {
            ExpandoObject obj = new ExpandoObject();
            foreach (var property in properties)
            {
                var prop = typeof(T).GetProperty(property);
                var value = prop?.GetValue(i);
                obj.TryAdd(property, value);
            }

            return obj;
        }
        ));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<T?>> GetByIdAsync([FromServices] IServiceBase<T, TId, TDbContext> service, [FromRoute] TId id, string? getFields)
    {
        if (!Options._useGetByIdFields && getFields is not null) return NotFound();
        if (!Options._useGetById) return NotFound();

        var result = await service.GetByIdAsync(id!);
        var properties = !string.IsNullOrWhiteSpace(getFields) ? getFields.Split(',') : null;
        if (properties is null) return result;

        ExpandoObject obj = new ExpandoObject();
        foreach (var property in properties)
        {
            var prop = typeof(T).GetProperty(property);
            var value = prop?.GetValue(result);
            obj.TryAdd(property, value);
        }
        return Ok(obj);
    }

    [HttpPost]
    public async Task<ActionResult<T>> CreateAsync([FromServices] IServiceBase<T, TId, TDbContext> service, T entity, string? getFields)
    {
        if (!Options._usePostReturnFields && getFields is not null) return NotFound();
        if (!Options._usePost) return NotFound();

        if (this.Options.PostRulesFunctions is not null)
        {
            IEnumerable<INotification> notifications = this.Options.PostRulesFunctions!
                                                    .Select(f => f(HttpContext, entity).Result!)
                                                    .Where(n => n is not null);

            if (notifications.Count() > 0) return BadRequest(notifications);
        }

        if (!await service.CreateAsync(entity)) return BadRequest(service.Notifications.Count() > 0 ? service.Notifications : "null");

        var properties = !string.IsNullOrWhiteSpace(getFields) ? getFields.Split(',') : null;
        if (properties is null) return Created(typeof(T).Name, entity);

        ExpandoObject obj = new ExpandoObject();
        foreach (var property in properties)
        {
            var prop = typeof(T).GetProperty(property);
            var value = prop?.GetValue(entity);
            obj.TryAdd(property, value);
        }
        return Created(typeof(T).Name, obj);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<T>> UpdateAsync([FromServices] IServiceBase<T, TId, TDbContext> service, [FromRoute] TId id, T entity)
    {
        if (!Options._usePut) return NotFound();

        if (!await service.HasByIdAsync(id!)) return NotFound(new Notification(typeof(T).Name + " not found"));

        typeof(T).GetProperty("Id")!.SetValue(entity, id);

        if (this.Options.PutRulesFunctions is not null)
        {
            IEnumerable<INotification> notifications = this.Options.PutRulesFunctions!
                                                    .Select(f => f(HttpContext, entity).Result!)
                                                    .Where(n => n is not null);

            if (notifications.Count() > 0) return BadRequest(notifications);
        }

        if (await service.UpdateAsync(entity))
            return entity;

        return BadRequest(service.Notifications.Count() > 0 ? service.Notifications : null);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAsync([FromServices] IServiceBase<T, TId, TDbContext> service, [FromRoute] TId id)
    {
        if (!Options._useDelete) return NotFound();

        if (!await service.HasByIdAsync(id!)) return NotFound(new Notification(typeof(T).Name + " not found"));

        var entity = await service.GetByIdAsync(id!);

        if (this.Options.DeleteRulesFunctions is not null)
        {

            IEnumerable<INotification> notifications = this.Options.DeleteRulesFunctions!
                                                    .Select(f => f(HttpContext, entity!).Result!)
                                                    .Where(n => n is not null);

            if (notifications.Count() > 0) return BadRequest(notifications);
        }

        if (await service.DeleteAsync(id!))
            return Ok();

        return BadRequest(service.Notifications.Count() > 0 ? service.Notifications : null);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult> UpdatePartAsync([FromServices] IServiceBase<T, TId, TDbContext> service, [FromRoute] TId id, ExpandoObject update)
    {
        if (!Options._usePatch) return NotFound();

        if (!await service.HasByIdAsync(id!))
            return NotFound(new Notification($"{typeof(T)} not found"));

        T entity = (await service.GetByIdAsync(id!))!;

        try
        {
            foreach (var item in update)
            {
                if (typeof(T).GetProperty(item.Key) is null)
                    return BadRequest(new Notification($"{item.Key} not existis", item.Key));

                Type propType = typeof(T).GetProperty(item.Key)!.PropertyType;
                var desserializedValue = ((JsonElement)item.Value!).Deserialize(propType);

                typeof(T).GetProperty(item.Key)!.SetValue(entity, desserializedValue);
            }

            if (await service.UpdateAsync(entity))
                return Ok();
            else
                return BadRequest();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    // public class UpdatePart
    // {
    //     public UpdatePart(TId id, string property, JsonElement value)
    //     {
    //         Id = id;
    //         Property = property;
    //         Value = value;
    //     }

    //     public TId Id { get; set; }
    //     public string Property { get; set; }
    //     public JsonElement Value { get; set; }
    // }
}