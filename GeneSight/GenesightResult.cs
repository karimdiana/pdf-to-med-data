// GenesightResult class provided by Jack

using PDFExtractor.Models;
using System;

namespace PDFExtractor.GeneSight
{
    public class GenesightResult
    {
        public string PatientName { get; set; } = "";
        public string DateOfBirth { get; set; } = "";
        public string Clinician { get; set; } = "";
        public List<int> OrderNumbers { get; set; } = new List<int>();
        public List<string> OrderDates { get; set; } = new List<string>();
        public List<GenesightPage> Pages { get; set; } = new List<GenesightPage>();
        public List<GenesightGene> PharmacodynamicGenes { get; set; } = new List<GenesightGene>();
        public List<GenesightGene> PharmacokineticGenes { get; set; } = new List<GenesightGene>();
        public List<GenesightGene> AdditionalGenes { get; set; } = new List<GenesightGene>();
        public List<string> ICD10Codes { get; set; } = new List<string>();
        public string Genotype { get; set; } = "";
        public string GenotypeActivity { get; set; } = "";
        public string GenotypeAllele { get; set; } = "";

        public GenesightResult() { }

        public GenesightResult(
            string PatientName,
            string DateOfBirth,
            string Clinician,
            List<int> OrderNumbers,
            List<string> OrderDates,
            string Genotype,
            string Activity,
            string Allele)
        {
            this.PatientName = PatientName;
            this.DateOfBirth = DateOfBirth;
            this.Clinician = Clinician;
            this.OrderNumbers = OrderNumbers;
            this.OrderDates = OrderDates;
            this.Genotype = Genotype;
            GenotypeActivity = Activity;
            GenotypeAllele = Allele;
        }

        public void AddPage(GenesightPage page)
        {
            Pages.Add(page);
        }
        public void AddGene(GenesightGene gene, int geneType)
        {
            switch (geneType)
            {
                case 0:
                    PharmacodynamicGenes.Add(gene);
                    break;
                case 1:
                    PharmacokineticGenes.Add(gene);
                    break;
                case 2:
                    AdditionalGenes.Add(gene);
                    break;
            }
        }

        public void AddICD10Code(string code)
        {
            ICD10Codes.Add(code);
        }

        public Patient getPatient()
        {
            return new Patient(PatientName, DateOfBirth, Clinician, OrderNumbers, OrderDates, ICD10Codes);
        }
    }
}
