using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PDFExtractor.Models
{
    [BsonIgnoreExtraElements]
    public class RGene
    {
        [BsonElement("patientId")]
        public ObjectId patientId { get; set; } = ObjectId.Empty;

        [BsonElement("gene")]
        public string Gene { get; set; } = string.Empty;

        [BsonElement("allele")]
        public string Allele { get; set; } = string.Empty;

        [BsonElement("effect")]
        public List<string> Effect { get; set; } = [];

        [BsonElement("activity")]
        public List<string> Activity { get; set; } = [];

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        public RGene(ObjectId patientId, string Gene, string Allele, List<string> Effect, List<string> Activity, string Description)
        {
            this.patientId = patientId;
            this.Gene = Gene;
            this.Allele = Allele;
            this.Effect = Effect;
            this.Activity = Activity;
            this.Description = Description;
        }

        public override string ToString()
        {
            string output = "Gene: " + Gene;
            output += "\n\nAllele: " + Allele;
            output += "\n\nEffects:";
            if (Effect == null)
            {
                output += "\nNone";
            }
            else
            {
                foreach (var item in Effect)
                {
                    output += "\n" + item;
                }
            }
            output += "\n\nActivity:";

            if (Activity == null)
            {
                output += "\nNone";
            }
            else
            {
                foreach (var item in Activity)
                {
                    output += "\n" + item;
                }
            }

            output += "\n\nDescription\n" + Description;

            return output;
        }
    }
}
