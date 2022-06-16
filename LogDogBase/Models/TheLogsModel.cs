using Newtonsoft.Json;

namespace LogDogBase.Models
{
    internal class TheLogsModel
    {
        [JsonProperty("WhereToStoreLogs")]
        public string? WhereToStoreLogs { get; set; }

        [JsonProperty("WhereToStoreLogsPathType")]
        public string? WhereToStoreLogsPathType { get; set; }

        [JsonProperty("WhereToCreateTxtFiles")]
        public string? WhereToCreateTxtFiles { get; set; }

        [JsonProperty("WhereToCreateTxtFilesPathType")]
        public string? WhereToCreateTxtFilesPathType { get; set; }

        [JsonProperty("LoggingItems")]
        public Dictionary<string, Dictionary<string, string>>? LoggingItems { get; set; }

        [JsonProperty("WhenToDeleteTempFile")]
        public string WhenToDeleteTempFile { get; set; }

        [JsonProperty("WhenToCreateTempFile")]
        public string WhenToCreateTempFile { get; set; }
    } // BaseModel
} // namespace