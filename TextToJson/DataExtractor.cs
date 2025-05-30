// John Bradin
// PFW Fall 2024 - CS46000 Capstone Project Team 4
// Parkview Genomic Testing PDF to Data Warehouse

// DataExtractor Class
// Wrapper for entire text-to-json section of program
// 1. Takes a 2D string array ([page][line]) of raw text data
// 2. Detects which company authored a genomic report
// 3. Creates a Company subclass based on results
// 4. Calls overridden methods of the subclass to extract data
// 5. Returns JSON string if data could be extracted; otherwise returns an empty string

// TODO
// Finish implementing all subclasses of Company
//   - CompanyGenesight
//   - CompanyAltera
//   - CompanyCaris
//   - CompanyFoundation
// Adjust code as needed based on changes

using PDFExtractor.GeneSight;

namespace PDFExtractor.TextToJson
{
    internal class DataExtractor
    {
        private static string[] companyNames = ["Natera", "GeneSight", "Caris Life Sciences", "FoundationOne"];
        public static CompanyGenesight? extractData(List<List<string>> data)
        {
            Company companyReport;

            // Data to extract in this application
            // 1. Report Date
            // 2. Patient Name
            // 3. Patient Date of Birth (DOB)
            // 4. Clinician / Physician
            // 5. Gene Code

            // Data to extract from Genesight reports(from class fields indicated in Jack's GenesightResult class)
            // 1. Patient Name
            // 2. Patient Date of Birth(DOB)
            // 3. Clinician
            // 4. Order Number
            // 5. Order Date
            // 6. List of Report Pages
            //     6-1. Title(drug type)
            //     6-2. Smoking Status
            //     6-3. List of Drugs(use as directed)
            //         6-3-1. Scientific Drug Name
            //         6-3-2. Commercial Drug Name
            //         6-3-3. Drug Clinical Classifications
            //     6-4. List of Drugs(moderate interaction)
            //         6-4-1. Scientific Drug Name
            //         6-4-2. Commercial Drug Name
            //         6-4-3. Drug Clinical Classifications
            //     6-5. List of Drugs(significant interaction)
            //         6-5-1. Scientific Drug Name
            //         6-5-2. Commercial Drug Name
            //         6-5-3. Drug Clinical Classifications

            List<string> companies = new List<string>();

            // Find company who created the report
            foreach (List<string> inPage in data)
            {
                foreach (string inLine in inPage)
                {
                    // If no companies are found
                    if (companies.Count == 0)
                    {
                        foreach (string company in companyNames)
                        {
                            if (inLine.Contains(company))
                            {
                                companies.Add(company);
                            }
                        }
                    }

                    // If multiple company names are found
                    else
                    {
                        foreach (string company in companyNames)
                        {
                            // Add to the list if not already in it
                            if (inLine.Contains(company) && !companies.Contains(company))
                            {
                                companies.Add(company);
                            }
                        }
                    }
                }
            }

            // Returns an empty string if no company was found
            if (companies.Count == 0)
            {
                Console.WriteLine("Error: No identifiable company was found");
                // TODO: Log report
                return null;
            }

            // Displays error messages and returns an empty string if any errors were detected
            if (companies.Count > 1)
            {
                Console.WriteLine("Error: Multiple company names were found");
                foreach (string company in companies)
                {
                    Console.WriteLine("- " + company);
                }
                // TODO: Log report
                return null;
            }

            // Sets reference to subclass based on which company name was found
            switch (companies[0])
            {
                case "Natera":
                    // Needs updating 
                    companyReport = new CompanyNatera(data);
                    Console.WriteLine("Altera/Natera report detected");
                    Console.WriteLine("Error: Reports from Altera/Natera cannot be processed at this time");
                    break;

                case "Caris Life Sciences":
                    companyReport = new CompanyCaris(data);
                    Console.WriteLine("Caris Life Sciences report detected");
                    Console.WriteLine("Error: Reports from Caris Life Sciences cannot be processed at this time");
                    break;
                case "FoundationOne":
                    companyReport = new CompanyFoundation(data);
                    Console.WriteLine("FoundationOne report detected");
                    Console.WriteLine("Error: Reports from FoundationOne cannot be processed at this time");
                    break;
                case "GeneSight":
                    companyReport = new CompanyGenesight(data);
                    Console.WriteLine("GeneSight report detected");
                    //Console.WriteLine("Error: Reports from GeneSight cannot be processed at this time");
                    break;
                default:
                    // TODO: Log report
                    return null;
            }

            // Extracts data
            companyReport.findData();
            return (CompanyGenesight)companyReport;
        }

        public static GenesightResult? ExtractResult(List<List<string>> data)
        {
            CompanyGenesight companyReport = extractData(data);

            if (companyReport == null)
            {
                return null;
            }

            return companyReport.getResult();
        }
    }
}