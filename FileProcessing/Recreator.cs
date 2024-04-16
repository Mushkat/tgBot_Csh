using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileProcessing
{
    /// <summary>
    /// Class for json objects.
    /// </summary>
    public class Recreator
    {
        [JsonPropertyName("Name")]
        public string Name { get; }
        [JsonPropertyName("RankYear")]
        public int RankYear { get; }
        [JsonPropertyName("MainObjects")]
        public string MainObjects { get; }
        [JsonPropertyName("Workplace")]
        public string Workplace { get; }
        [JsonPropertyName("Photo")]
        public string Photo {  get; }
        [JsonPropertyName("global_id")]
        public long GlobalId { get; }

        public Recreator()
        {
            Name = string.Empty;
            RankYear = 0;
            MainObjects = string.Empty;
            Workplace = string.Empty;
            Photo = string.Empty;
            GlobalId = 0;
        }
        [JsonConstructor]
        public Recreator(string name, int rankYear, string mainObjects, string workplace, string photo, long globalId)
        {
            Name = name;
            RankYear = rankYear;
            MainObjects = mainObjects;
            Workplace = workplace;
            Photo = photo;
            GlobalId = globalId;
        }

        public string ToCsv()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\"{Name}\";\"{RankYear}\";\"{MainObjects}\";\"{Workplace}\";\"{Photo}\";\"{GlobalId}\";");
            return sb.ToString();
        }

        public string ToJson()
        {
            string res = JsonSerializer.Serialize(this, new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            res = res.Replace("GlobalId", "global_id");
            return res;
        }
    }
}