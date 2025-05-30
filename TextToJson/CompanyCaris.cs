// John Bradin
// PFW Fall 2024 - CS46000 Capstone Project Team 4
// Parkview Genomic Testing PDF to Data Warehouse

// CompanyCaris Class
// Subclass of Company class; provides algorithms for all Caris Life Sciences reports

// TODO
// Implement class

namespace PDFExtractor.TextToJson
{
    internal class CompanyCaris : Company
    {
        public CompanyCaris(List<List<string>> inData) : base(inData)
        {
            data = inData;
        }

        // Extracts and checks patient name
        //public override void findPatientName() { }

        // Extracts and checks patient date of birth
        //public override void findDOB() { }

        // Extracts and checks physician name
        //public override void findPhysician() { }

        // Extracts and checks report date
        //public override void findReportDate() { }

        // Extracts all found Gene Sequences
        //public override void findGeneSequences() { }
    }
}
