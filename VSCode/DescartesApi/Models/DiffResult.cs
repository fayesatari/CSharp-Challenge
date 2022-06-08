using System.Text.Json;
using System.Text.Json.Serialization;

namespace DescartesApi.Models
{
    public class DiffResult
    {
        /// <summary>
        /// Type of diffrentitation between left and right
        /// </summary>
        /// <value></value>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DiffResultType diffResultType { get; set; }

        /// <summary>
        /// List of diffrentitation between left and right
        /// </summary>
        /// <value></value>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<DiffedType>? diffs { get; set; }
    }
}
