using System.Text.Json.Serialization;

namespace FastDev.Infra.Data
{
    public interface IDataModel<TId>
    {
        [JsonIgnore]
        bool Deleted { get; }
        TId? Id { get; set; }

        void SetDeleted();
    }
}