using MongoDB.Bson.Serialization.Attributes;

namespace PDFExtractor.Models
{
    [BsonIgnoreExtraElements]
    public class Log
    {
        [BsonElement("logDate")]
        public DateTime logDate { get; set; } = DateTime.MinValue;

        [BsonElement("logData")]
        public string logData { get; set; } = string.Empty;

        public Log(DateTime logDate, string logData)
        {
            this.logDate = logDate;
            this.logData = logData;
        }
    }
}
