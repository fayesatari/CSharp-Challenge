using System.Text.Json;
using System.Text.Json.Serialization;

namespace DescartesApi.Models
{
    public class DiffResult
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DiffResultType diffResultType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<DiffedType>? diffs { get; set; }
    }
}
