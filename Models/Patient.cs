using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace PDFExtractor.Models
{
    [BsonIgnoreExtraElements]
    public class Patient
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.Empty;

        [DisplayName("Patient Name")]
        [BsonElement("patientName")]
        public string PatientName { get; set; } = string.Empty;

        [DisplayName("Date of Birth")]
        [BsonElement("dateOfBirth")]
        public string DateOfBirth { get; set; } = string.Empty;

        [BsonElement("clinician")]
        public string Clinician { get; set; } = string.Empty;

        [BsonElement("orderNumbers")]
        public List<int> OrderNumbers { get; set; } = [];

        [BsonElement("orderDates")]
        public List<string> OrderDates { get; set; } = [];

        [BsonElement("icd10codes")]
        public List<string> icd10_codes { get; set; } = [];

        public Patient(string PatientName, string DateOfBirth, string Clinician, List<int> OrderNumbers, List<string> OrderDates, List<string> icd10_codes)
        {
            this.PatientName = PatientName;
            this.DateOfBirth = DateOfBirth;
            this.Clinician = Clinician;
            this.OrderNumbers = OrderNumbers;
            this.OrderDates = OrderDates;
            this.icd10_codes = icd10_codes;
        }

        public string ICD10s()
        {
            string output = string.Empty;

            output = "ICD10 Codes:\n";

            if (icd10_codes.Count == 0)
            {
                output += "None";
            }
            else
            {
                int count = 1;
                foreach (string code in icd10_codes)
                {
                    output += code.PadRight(10, ' ');
                    count++;
                    if (count > 7)
                    {
                        output += Environment.NewLine;
                        count = 1;
                    }
                }
            }

            return output;
        }
    }
}
