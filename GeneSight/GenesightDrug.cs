// GenesightDrug class provided by Jack

namespace PDFExtractor.GeneSight
{
    public class GenesightDrug
    {
        public static readonly string[] ClassificationList =
            [
            "Index bump to align classification numbers with their descriptions",
            "Serum level may be too high, lower doses may be required.",
            "Serum level may be too low, higher doses may be required.",
            "Difficult to predict dose adjustments due to conflicting variations in metabolism.",
            "Genotype may impact drug mechanism of action and result in moderately reduced efficacy.",
            "CYP2D6 genotype indicates that this patient may experience increased frequency of side effects but also greater symptom improvement in those who find the treatment tolerable.",
            "Use of this drug may increase risk of side effects.",
            "Smoking status changes the results of this medication.",
            "FDA label identifies a potential gene-drug interaction for this medication.",
            "Per FDA label, this medication is contraindicated for this genotype.",
            "While this medication does not have clinically proven genetic markers that allow it to be categorized, it may be an effective choice based on other clinical factors."
            ];

        // Serializable fields
        public string Scientific { get; set; } = "";
        public string Commercial { get; set; } = "";
        public List<int> ClinicalClassifications { get; set; } = new List<int>();

        public GenesightDrug(string Scientific, string Commercial)
        {
            this.Scientific = Scientific;
            this.Commercial = Commercial;
        }

        public void AddClassification(int Classification)
        {
            ClinicalClassifications.Add(Classification);
        }
    }
}
