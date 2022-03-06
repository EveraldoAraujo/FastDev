using System.Text.Json.Serialization;

namespace FastDev.Infra.Data.Model;
public class DataModel<TId> : IDataModel<TId>
{
    [JsonIgnore]
    public bool Deleted { get; private set; }

    public TId? Id { get; set; }

    public void SetDeleted()
    {
        this.Deleted = true;
    }
}
