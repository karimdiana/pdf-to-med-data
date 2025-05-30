// John Bradin
// PFW Fall 2024 - CS46000 Capstone Project Team 4
// Parkview Genomic Testing PDF to Data Warehouse

// Company Class
// Base class for all other Company subclasses

// TODO
// Finish implementing all subclasses of Company
//   - CompanyAltera
//   - CompanyCaris
//   - CompanyFoundation
// Adjust code as needed based on changes
//   - Move company specific variables to subclass, etc.

using System.ComponentModel.Design;
using System.Text.Json;
using System.Linq;

namespace PDFExtractor.TextToJson
{
    internal class Company
    {
        // Container variables; will be set once and then used to check for errors
        protected List<List<string>> data;
        protected string name = "";
        protected string dob = "";
        protected string physician = "";
        protected List<string> reportDates = new List<string>();
        protected List<string> codes = new List<string>();

        // Variables to indicate specific errors when extracting specific data
        // 0 = No errors found  
        // 1 = Missing data
        // 2 = Data mismatch
        protected static Dictionary<string, int> errorCodes = new Dictionary<string, int>();
        protected static Dictionary<string, List<string>> errorValuesFound = new Dictionary<string, List<string>>();

        // Constructor for base class, takes in a list of string lists
        public Company(List<List<string>> inData)
        {
            data = inData;
        }

        // Searches for and removes all substrings in array from main string (inString)
        // Returns inString (by reference)
        // Returns true if inString contained any substrings defined in prefixArray
        // Otherwise returns false
        protected static bool search(ref string inString, string[] prefixArray)
        {
            bool substringFound = false;
            foreach (string substring in prefixArray)
            {
                if (inString.Contains(substring))
                {
                    substringFound = true;
                    break;
                }
            }
            if (substringFound)
            {
                foreach (string prefix in prefixArray)
                {
                    inString = inString.Split(prefix)[inString.Split(prefix).Length - 1].Trim();
                }
            }
            return substringFound;
        }

        // Adds new dictionary key/value pairs based on input key name newKey
        // Sets or resets value of errorCodes to 0
        // Sets or resets value of errorValuesFound to a new string List
        protected static void setupErrorCode(string newKey)
        {
            if (!errorCodes.ContainsKey(newKey))
            {
                errorCodes.Add(newKey, 0);
                errorValuesFound.Add(newKey, new List<string>());
            }
            else
            {
                errorCodes[newKey] = 0;
                errorValuesFound[newKey] = new List<string>();
            }
        }

        // Searches for errors based on the parameters newValue and setValue
        // Adjusts the values of setValue, errorCodes, and errorValuesFound depending on the results
        // Returns setValue (by reference)
        protected static void findErrors(string newValue, ref string setValue, string key)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                if (string.IsNullOrEmpty(setValue))
                {
                    errorCodes[key] = 1;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(setValue))
                {
                    setValue = newValue;
                    errorValuesFound[key].Add(newValue);
                    errorCodes[key] = 0;
                }
                else if (newValue != setValue)
                {
                    if (key == "dob" || key == "reportDate")
                    {
                        if (DateTime.Parse(newValue) == DateTime.Parse(setValue))
                        {
                            return;
                        }
                    }
                    errorValuesFound[key].Add(newValue);
                    errorCodes[key] = 2;
                }
            }
        }

        // Analyzes error state from errorCodes and errorValuesFound using the parameter key
        // Outputs results to console
        protected static void printErrors(string key)
        {
            if (errorCodes[key] == 1)
            {
                Console.WriteLine("Warning: No values found for {0}", key);
            }
            else if (errorCodes[key] == 2)
            {
                Console.WriteLine("Warning: Multiple values were found for {0}", key);
                foreach (string value in errorValuesFound[key])
                {
                    Console.WriteLine("  " + value);
                }
            }
        }

        // Extracts and checks report data
        // findData() will be overridden in each company subclass as it contains the
        //   company specific algorithms that find and extract genomic data

        public virtual void findData() { }

        public virtual void findGeneSequences() { }

        // Creates a new UserRecord object from extracted data, and converts it into a JSON string
        public virtual string getJSONReport()
        {
            return JsonSerializer.Serialize(new UserRecord(name, dob, physician, reportDates.ToArray(), codes.ToArray()));
        }
    }
}