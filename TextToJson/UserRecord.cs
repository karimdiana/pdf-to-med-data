// John Bradin
// PFW Fall 2024 - CS46000 Capstone Project Team 4
// Parkview Genomic Testing PDF to Data Warehouse

// Possible integration with Jack's GenesightResult class in a future update

// UserRecord class
// Used to convert all accumulated data into a JSON string
// - Patient Name
// - Patient Date of Birth (DOB)
// - Clinician / Physician
// - Report Date
// - Gene Code

// TODO
// After finishing CompanyGenesight class, look into combining this class with GenesightResult

namespace PDFExtractor.TextToJson
{
    internal class UserRecord
    {
        private string name { get; set; } = string.Empty;
        private string dob { get; set; } = string.Empty;
        private string physician { get; set; } = string.Empty;
        private string[] reportDates { get; set; } = [];
        private string[] codes { get; set; } = [];

        public UserRecord() { }

        public UserRecord(string name, string dob, string physician, string[] reportDates, string[] codes)
        {
            this.name = name;
            this.dob = dob;
            this.physician = physician;
            this.reportDates = reportDates;
            this.codes = codes;
        }
    }
}
