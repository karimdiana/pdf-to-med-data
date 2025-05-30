// John Bradin
// PFW Fall 2024 - CS46000 Capstone Project Team 4
// Parkview Genomic Testing PDF to Data Warehouse

// CompanyNatera Class
// Subclass of Company class; provides algorithms for all Altera/Natera reports


// TODO
// Update and refactor entire class to reflect changes made to Company class
// Program will not compile in its current state if uncommented

namespace PDFExtractor.TextToJson
{
    internal class CompanyNatera : Company
    {
        public CompanyNatera(List<List<string>> inData) : base(inData)
        {
            data = inData;
        }

        //// Extracts and checks patient name
        //public override void findPatientName()
        //{
        //    substring = "";

        //    foreach (string[] inPage in data)
        //    {
        //        foreach (string inLine in inPage)
        //        {
        //            if (inLine.Contains("Patient:"))
        //            {
        //                substring = inLine.Split("Patient:")[1].Trim();
        //                substring = substring.Split("Ordering Client:")[0];
        //                substring = substring.Split("Patient ID:")[0];
        //                if (string.IsNullOrEmpty(substring))
        //                {
        //                    err_patientName = 1;
        //                }
        //                else
        //                {
        //                    if (string.IsNullOrEmpty(name))
        //                    {
        //                        name = substring;
        //                        err_patientsFound.Add(substring);
        //                    }
        //                    else if (substring != name)
        //                    {
        //                        err_patientName = 2;
        //                        err_patientsFound.Add(substring);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (err_patientName == 1)
        //    {
        //        Console.WriteLine("Warning: Patient name was not found");
        //    }
        //    else if (err_patientName == 2)
        //    {
        //        Console.WriteLine("Warning: Multiple patient names were found");
        //        foreach (string foundPatient in err_patientsFound)
        //        {
        //            Console.WriteLine(foundPatient);
        //        }
        //    }
        //}

        //// Extracts and checks patient date of birth
        //public override void findDOB()
        //{
        //    substring = "";
        //    foreach (string[] inPage in data)
        //    {
        //        foreach (string inLine in inPage)
        //        {
        //            if (inLine.Contains("DOB:"))
        //            {
        //                substring = inLine.Split("DOB:")[1].Trim();
        //                substring = substring.Split("Specimen Site:")[0];
        //                substring = substring.Split("Ordering Physician:")[0];
        //                if (string.IsNullOrEmpty(substring))
        //                {
        //                    err_dob = 1;
        //                }
        //                else
        //                {
        //                    if (string.IsNullOrEmpty(dobString))
        //                    {
        //                        dobString = substring;
        //                        err_dobsFound.Add(substring);
        //                    }
        //                    else if (substring != dobString)
        //                    {
        //                        err_dob = 2;
        //                        err_dobsFound.Add(substring);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (err_dob == 1)
        //    {
        //        Console.WriteLine("Warning: Patient date of birth was not found");
        //    }
        //    else if (err_dob == 2)
        //    {
        //        Console.WriteLine("Warning: Multiple patient dates of birth were found");
        //        foreach (string foundDOB in err_dobsFound)
        //        {
        //            Console.WriteLine(foundDOB);
        //        }
        //    }
        //    dob = DateTime.Parse(dobString);
        //}

        //// Extracts and checks physician name
        //public override void findPhysician()
        //{
        //    substring = "";

        //    foreach (string[] inPage in data)
        //    {
        //        foreach (string inLine in inPage)
        //        {
        //            if (inLine.Contains("Ordering Physician:"))
        //            {
        //                substring = inLine.Split("Ordering Physician:")[1].Trim();
        //                substring = substring.Split("Received Date:")[0];
        //                if (string.IsNullOrEmpty(substring))
        //                {
        //                    err_physicianName = 1;
        //                }
        //                else
        //                {
        //                    if (string.IsNullOrEmpty(physician))
        //                    {
        //                        physician = substring;
        //                        err_physiciansFound.Add(substring);
        //                    }
        //                    else if (substring != physician)
        //                    {
        //                        err_physicianName = 2;
        //                        err_physiciansFound.Add(substring);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (err_physicianName != 0)
        //    {
        //        if (err_physicianName == 1)
        //        {
        //            Console.WriteLine("Warning: Physician name was not found");
        //        }
        //        else if (err_physicianName == 2)
        //        {
        //            Console.WriteLine("Warning: Multiple physician names were found");
        //            foreach (string foundPhysician in err_physiciansFound)
        //            {
        //                Console.WriteLine(foundPhysician);
        //            }
        //        }
        //        Console.WriteLine();
        //    }
        //}

        //// Extracts and checks report date
        //public override void findReportDate()
        //{
        //    substring = "";
        //    foreach (string[] inPage in data)
        //    {
        //        foreach (string inLine in inPage)
        //        {
        //            if (inLine.Contains("Report Date:"))
        //            {
        //                substring = inLine.Split("Report Date:")[1].Trim();
        //                if (string.IsNullOrEmpty(substring))
        //                {
        //                    err_reportDate = 1;
        //                }
        //                else
        //                {
        //                    if (string.IsNullOrEmpty(reportDateString))
        //                    {
        //                        reportDateString = substring;
        //                        err_reportDatesFound.Add(substring);
        //                    }
        //                    else if (substring != reportDateString)
        //                    {
        //                        err_reportDate = 2;
        //                        err_reportDatesFound.Add(substring);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (err_reportDate == 1)
        //    {
        //        Console.WriteLine("Warning: Report date was not found");
        //    }
        //    else if (err_reportDate == 2)
        //    {
        //        Console.WriteLine("Warning: Multiple report dates were found");
        //        foreach (string foundDate in err_reportDatesFound)
        //        {
        //            Console.WriteLine(foundDate);
        //        }
        //    }
        //    reportDate = DateTime.Parse(reportDateString);
        //}

        //// Extracts all found Gene Sequences
        //public override void findGeneSequences()
        //{
        //    substring = "";

        //    foreach (string[] inPage in data)
        //    {
        //        foreach (string inLine in inPage)
        //        {
        //            if (inLine.Contains("Alteration:"))
        //            {
        //                substring = inLine.Split("Alteration:")[1].Trim();
        //                if (!codes.Contains(substring))
        //                {
        //                    codes.Add(substring);
        //                }
        //            }
        //        }
        //    }
        //    codesArr = codes.ToArray();
        //    if (codesArr.Length == 0)
        //    {
        //        err_gene = 1;
        //        Console.WriteLine("Warning: No gene sequences were found");
        //    }
        //}
    }
}
