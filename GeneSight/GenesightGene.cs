// John Bradin
// PFW Fall 2024 - CS46000 Capstone Project Team 4
// Parkview Genomic Testing PDF to Data Warehouse

// GenesightGene class
// Holds genomic data from GeneSight reports

namespace PDFExtractor.GeneSight
{
    public class GenesightGene
    {
        public string gene { get; set; }
        public string allele { get; set; }
        public List<string> effect { get; set; }
        public List<string> activity { get; set; }
        public string description { get; set; }

        public GenesightGene(string gene, string allele, List<string> effect, List<string> activity, string description)
        {
            this.gene = gene;
            this.allele = allele;
            this.effect = effect;
            this.activity = activity;
            this.description = description;
        }
    }
}
