using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PDFExtractor.GeneSight;
using System.Diagnostics;

namespace PDFExtractor.Models
{
    [BsonIgnoreExtraElements]
    public class Medication
    {
        [BsonElement("patientId")]
        public ObjectId patientId { get; set; } = ObjectId.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("smokingStatus")]
        public string Status { get; set; } = string.Empty;

        [BsonElement("interaction")]
        public string Interaction { get; set; } = string.Empty;

        [BsonElement("clinicalClassifications")]
        public List<int> ClinicalClassifications { get; set; } = [];

        [BsonElement("type")]
        public string Type { get; set; } = string.Empty;

        public Medication(ObjectId patientId, string Name, string Status, string Interaction, List<int> ClinicalClassifications, string Type)
        {
            this.patientId = patientId;
            this.Name = Name;
            this.Status = Status;
            this.Interaction = Interaction;
            this.ClinicalClassifications = ClinicalClassifications;
            this.Type = Type;
        }

        public override string ToString()
        {
            string output = "Name (Commercial): " + Name;
            output += "\n\nType: " + Type;
            output += "\n\nSmoking Status: " + Status;
            output += "\n\nInteraction: " + Interaction;
            output += "\n\nClinical Classifications: ";

            if (ClinicalClassifications.Count == 0)
            {
                output += "\nNone";
            }
            else
            {
                foreach (var item in ClinicalClassifications)
                {
                    output += "\n" + item + ": " + GenesightDrug.ClassificationList[item];
                }
            }

            return output;
        }
    }
}
