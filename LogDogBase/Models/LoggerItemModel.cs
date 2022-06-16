using Newtonsoft.Json;

namespace LogDogBase.Models
{
    internal class LoggerItemModel
    {
        [JsonProperty("LogFilePath")]
        public string? LogFilePath { get; set; }
        [JsonProperty("LogFilePathType")]
        public string? LogFilePathType { get; set; }
        [JsonProperty("OutputLogFileName")]
        public string? OutputLogFileName { get; set; }
        [JsonProperty("OutputTempFileName")]
        public string? OutputTempFileName { get; set; }
    }
}