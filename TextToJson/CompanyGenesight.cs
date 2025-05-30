// John Bradin
// PFW Fall 2024 - CS46000 Capstone Project Team 4
// Parkview Genomic Testing PDF to Data Warehouse

// CompanyGenesight Class
// Subclass of Company class; provides algorithms for all GeneSight reports

using PDFExtractor.GeneSight;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PDFExtractor.TextToJson
{
    internal class CompanyGenesight : Company
    {
        private static readonly string[] titles = { "Antidepressants", "Anxiolytics and Hypnotics", "Antipsychotics", "Mood Stabilizers", "Stimulants" };
        private static readonly string[] statuses = { "smokers and non-smokers", "Non-Smokers", "Smokers" };
        private List<int> orderNumbers = new List<int>();
        private List<GenesightPage> pages = new List<GenesightPage>();
        private List<GenesightGene> pharmacodynamicGenes = new List<GenesightGene>();
        private List<GenesightGene> pharmacokineticGenes = new List<GenesightGene>();
        private List<GenesightGene> additionalGenes = new List<GenesightGene>();
        private List<string> icd10Codes = new List<string>();
        private string genotype = "";
        private string genotypeActivity = "";
        private string genotypeAllele = "";

        public CompanyGenesight(List<List<string>> inData) : base(inData)
        {
            data = inData;
        }

        // Extracts generic report data
        public override void findData()
        {
            // Sets up error codes dictionary for error detection and logging
            //   based on each individual field
            setupErrorCode("name");
            setupErrorCode("dob");
            setupErrorCode("physicianName");
            setupErrorCode("orderNumbers");
            setupErrorCode("reportDates");

            string prevLine = "";
            string tempString = "";
            string dobString = "";
            string reportDateString = "";
            string orderNumberString = "";
            int searchMode = 0;
            bool startup = true;
            DateTime tempDate;

            // Debug variables; remove later
            // Tracks the page and line numbers
            int DEBUG_pageNum = 1;
            int DEBUG_lineNum = 1;

            foreach (List<string> inPage in data)
            {
                startup = true;
                DEBUG_lineNum = 1;
                foreach (string inLine in inPage)
                {
                    tempString = inLine.Trim();
                    if (startup)
                    {
                        prevLine = tempString;
                        startup = false;
                    }

                    if (search(ref tempString, ["Date of Birth:", "DOB:"]))
                    {
                        findErrors(prevLine, ref name, "name");
                        findErrors(tempString, ref dobString, "dob");
                    }
                    else if (search(ref tempString, ["Clinician:"]))
                    {
                        findErrors(tempString, ref physician, "physicianName");
                    }
                    else if (searchMode == 0)
                    {
                        if (search(ref tempString, ["Order Number:"]))
                        {
                            findErrors(tempString, ref orderNumberString, "orderNumbers");
                            if (errorCodes["orderNumbers"] == 1 && orderNumberString == "")
                            {
                                searchMode = 1;
                            }
                            else if (tempString != "")
                            {
                                orderNumberString = tempString;
                                if (!orderNumbers.Contains(int.Parse(orderNumberString)))
                                {
                                    orderNumbers.Add(int.Parse(orderNumberString));
                                }
                            }
                        }

                        else if (search(ref tempString, ["Report Date:"]))
                        {
                            findErrors(tempString, ref reportDateString, "reportDates");
                            if (errorCodes["reportDates"] == 1 && reportDateString == "")
                            {
                                searchMode = 1;
                            }
                            else if (tempString != "")
                            {
                                reportDateString = tempString;
                                tempDate = DateTime.Parse(reportDateString);
                                reportDateString = tempDate.ToShortDateString();

                                if (!reportDates.Contains(reportDateString))
                                {
                                    reportDates.Add(reportDateString);
                                }
                            }
                        }
                    }
                    else
                    {
                        // If tempString is not empty AND matches the following in order
                        //   6 to 8 of any digit character
                        if (orderNumberString == "" && Regex.IsMatch(tempString, "^\\d{6,8}$"))
                        {
                            errorCodes["orderNumbers"] = 0;
                            orderNumberString = tempString;
                            if (orderNumberString != "" && !orderNumbers.Contains(int.Parse(orderNumberString)))
                            {
                                orderNumbers.Add(int.Parse(orderNumberString));
                            }
                        }

                        // If tempString if not empty AND matches the following in order (detects MM/DD/YYYY format)
                        //   1 or 2 of any digit character
                        //   1 forward slash '/'
                        //   1 or 2 of any digit character
                        //   1 forward slash '/'
                        //   4 of any digit character
                        else if (reportDateString == "" && Regex.IsMatch(tempString, "^\\d{1,2}\\/\\d{1,2}\\/\\d{4}$"))
                        {
                            errorCodes["reportDates"] = 0;
                            reportDateString = tempString;
                            if (reportDateString != "")
                            {
                                tempDate = DateTime.Parse(reportDateString);
                                reportDateString = tempDate.ToShortDateString();
                                if (!reportDates.Contains(reportDateString))
                                {
                                    reportDates.Add(reportDateString);
                                }
                            }
                            searchMode = 0;
                        }
                    }
                    prevLine = tempString;
                    DEBUG_lineNum++;
                }
                DEBUG_pageNum++;
            }

            printErrors("name");
            printErrors("dob");
            printErrors("physicianName");
            //printErrors("orderNumbers");
            //printErrors("reportDates");

            tempDate = DateTime.Parse(dobString);
            dob = tempDate.ToShortDateString();
            findDrugs();
            findGeneSequences();
            findICD10Codes();
            findGenotype();
        }

        // Extracts all Gene Sequences
        public override void findGeneSequences()
        {
            int pageNum = 1;
            List<GenesightGene> geneList;

            // Container lists that will hold all found values until they are passed
            //   into the permanent class lists at the end of each page
            List<string> genes;
            List<string> alleles;
            List<List<string>> effect;
            List<List<string>> activity;
            List<string> description;

            // Temporary variables that will change often to hold data for modification before
            //   being passed into the container lists
            string tempString;
            string tempDescString;
            List<string> tempEffect;
            List<string> tempActivity;
            List<string> tempDesc;

            // Flags that start/stop consecutive lines being passed into variables
            bool effectSwitch;
            bool descSwitch;

            // Local function to test if any previously found genes in the current page
            //   are in the current working line (tempString)
            // Used to find enzyme activity strings
            bool stringHasFoundGene()
            {
                foreach (string gene in genes)
                {
                    if (tempString.StartsWith(gene) && tempString.Length > gene.Length)
                    {
                        return true;
                    }
                }
                return false;
            }

            foreach (List<string> inPage in data)
            {
                // Only scans pages 9, 10, 11, and 18
                if (pageNum < 9 || (pageNum > 11 && pageNum < 18))
                {
                    pageNum++;
                    continue;
                }
                else if (pageNum > 18)
                {
                    break;
                }

                // Reset container values at the start of each page
                geneList = new List<GenesightGene>();
                genes = new List<string>();
                alleles = new List<string>();
                effect = new List<List<string>>();
                activity = new List<List<string>>();
                description = new List<string>();

                // Reset temporary variables and flags
                tempEffect = new List<string>();
                tempActivity = new List<string>();
                tempDesc = new List<string>();
                effectSwitch = false;
                descSwitch = false;

                foreach (string inLine in inPage)
                {
                    tempString = inLine.Trim();

                    // If blank line is encountered, pass temporary values to container values
                    //   before reseting the temporary variables and flags
                    if (string.IsNullOrEmpty(tempString))
                    {
                        if (effectSwitch)
                        {
                            effect.Add(tempEffect);
                            tempEffect = new List<string>();
                        }
                        if (tempActivity.Count > 0)
                        {
                            activity.Add(tempActivity);
                            tempActivity = new List<string>();
                        }
                        if (descSwitch)
                        {
                            tempDescString = "";
                            foreach (string descLine in tempDesc)
                            {
                                tempDescString += " " + descLine;
                            }
                            tempDescString = tempDescString.Trim();
                            tempDesc = new List<string>();
                            description.Add(tempDescString);
                        }
                        effectSwitch = false;
                        descSwitch = false;
                        continue;
                    }

                    if (pageNum < 18)
                    {
                        // If current line is a gene
                        // Found if all of the following conditions are met
                        //   1. Both effectSwitch and descSwitch are off (false)
                        //   2. Current line is between 3 and 15 characters long
                        //   3. Current line matches the following Regex in order:
                        //     1 of any letter from A to Z (uppercase only)
                        //     0 to unlimited of the following four statements in order
                        //       0 or 1 dash '-'
                        //       0 or 1 asterisk '*'
                        //       0 to unlimited of any letter from A to Z (uppercase only)
                        //       0 to unlimted of any digit character
                        //     1 to unlimited of any of the following statements
                        //       Any letter from A to Z (uppercase only)
                        //       Any digit character
                        if (!effectSwitch && !descSwitch && tempString.Length >= 3 && tempString.Length <= 15 && Regex.IsMatch(tempString, "^[A-Z](-?\\*?[A-Z]*[0-9]*)*([A-Z]|[0-9])+$"))
                        {
                            genes.Add(tempString);
                        }

                        // If current line is an allele
                        // Found if all of the following conditions are met
                        // 1. Both effectSwitch and descSwitch are off (false)
                        // 2. Any of the following three conditions are true
                        //   a. Current line equals "Not Present"
                        //   b. Current line equals "AA/" (PDFPig incorrectly parsing "A/A"; will fail the Regex in condition c)
                        //   c. Current line matches the following Regex in order:
                        //     1 to unlimited of any of the following statments
                        //       Dash '-'
                        //       Asterisk '*'
                        //       Any letter from A to Z (uppercase only)
                        //       Any digit character
                        //       Any whitespace character
                        //     1 forward dash '/'
                        //     0 or 1 asterisk '*'
                        //     1 to unlimited of any of the following
                        //       Any letter from A to Z (uppercase only)
                        //       Any digit character
                        else if (!effectSwitch && !descSwitch && (Regex.IsMatch(tempString, "^(-|\\*|[A-Z]|[0-9]|>|\\s)+\\/\\*?([A-Z]|[0-9])+$") || tempString == "Not Present" || tempString == "AA/"))
                        {
                            // Fixes PDFPig incorrectly interpreting "A/A" as "AA/"
                            if (tempString == "AA/")
                            {
                                tempString = "A/A";
                            }
                            alleles.Add(tempString);
                        }

                        // If current line shows an enzyme activity (only on page 10 in pages 9, 10, and 11)
                        // Uses the method stringHasFoundGene to detect if the current line
                        //   starts with a gene that was already found by this point in the page
                        else if (stringHasFoundGene())
                        {
                            tempActivity.Add(tempString);
                        }

                        // If current line is an effect string
                        // Found if any of the following two conditions are true
                        //   1. effectSwitch is on (true; this means the last line was also an effect string)
                        //   2. Current line matches the following Regex in order:
                        //     0 or 1 of any of the following strings (includes 1 space at the end)
                        //       Non-smoker: 
                        //       Smoker: 
                        //     Any of the following two options
                        //       Option 1:
                        //         1 of any of the following strings
                        //           Normal
                        //           Higher
                        //           Intermediate
                        //           Reduced
                        //           Moderately Reduced
                        //         1 space
                        //         1 of any of the following strings
                        //           Sensitivity
                        //           Risk
                        //           Response
                        //       Option 2:
                        //         1 of any of the following strings
                        //           Extensive (Normal)
                        //           Ultrarapid
                        //           Intermediate
                        //           Poor
                        //         Metabolizer
                        // This method sets effectSwitch to true, which stays on until the next blank line is found
                        else if (effectSwitch || Regex.IsMatch(tempString, "^(Non-smoker: |Smoker: )?((Normal|Higher|Intermediate|Reduced|Moderately Reduced) (Sensitivity|Risk|Response))|(Extensive \\(Normal\\)|Ultrarapid|Intermediate|Poor) Metabolizer$"))
                        {
                            tempEffect.Add(tempString);
                            effectSwitch = true;
                        }

                        // If current line is a gene description
                        // Found if any of the following four conditions are true
                        //   descSwitch is on (true; this means the last line was also part of the description)
                        //   Current string starts with any of the following
                        //     This patient
                        //     This individual
                        //     This genotype
                        else if (descSwitch || tempString.StartsWith("This patient") || tempString.StartsWith("This individual") || tempString.StartsWith("This genotype"))
                        {
                            tempDesc.Add(tempString);
                            descSwitch = true;
                        }
                    }
                    else
                    {
                        // Adds MTHFR gene from page 18
                        if (genes.Count == 0)
                        {
                            genes.Add("MTHFR");
                        }

                        // Sets allele for MTHFR gene
                        if (alleles.Count == 0 && (tempString == "C/C" || tempString == "C/T" || tempString == "T/T"))
                        {
                            alleles.Add(tempString);
                        }

                        // Sets enzyme activity for MTHFR gene
                        if (activity.Count == 0 && (tempString == "Normal Activity" || tempString == "Intermediate Activity" || tempString == "Reduced Activity"))
                        {
                            tempActivity.Add(tempString);
                        }

                        // Sets description for MTHFR gene
                        if (descSwitch || tempString.StartsWith("This patient") || tempString.StartsWith("This individual") || tempString.StartsWith("This genotype"))
                        {
                            tempDesc.Add(tempString);
                            descSwitch = true;
                        }
                    }
                }

                // Populates class list variables using all data found from the current page
                for (int i = 0; i < genes.Count; i++)
                {
                    switch (pageNum)
                    {
                        case 9:
                            pharmacodynamicGenes.Add(new GenesightGene(genes[i], alleles[i], effect[i], null, description[i]));
                            break;
                        case 10:
                            pharmacokineticGenes.Add(new GenesightGene(genes[i], alleles[i], effect[i], activity[i], description[i]));
                            break;
                        case 11:
                            additionalGenes.Add(new GenesightGene(genes[i], alleles[i], null, null, description[i]));
                            break;
                        case 18:
                            additionalGenes.Add(new GenesightGene(genes[i], alleles[i], null, activity[i], description[i]));
                            break;
                    }
                }
                pageNum++;
            }
        }

        // Extracts all found Drugs
        public void findDrugs()
        {
            // Universal container variables
            // Holds data to create new objects; changes infrequently
            string pageTitle = "";
            string smokerStatus = "";
            GenesightPage tempPage;
            List<string[]> classifications;
            List<GenesightDrug> drugsUAD;
            List<GenesightDrug> drugsMod;
            List<GenesightDrug> drugsSig;
            List<GenesightDrug> drugsNP;
            List<GenesightDrug>[] listArr;

            // Temporary data variables
            // Holds temporary data; frequently changes
            string scientific = "";
            string commerical = "";
            string possibleDrugSci = "";
            string possibleDrugCom = "";
            string[] tempStrArray;

            // Universal control variables
            // Changes program flow based on input found
            int pageNum;
            int drugSwitch;
            bool firstPass;
            bool titleSet;
            bool statusSet;
            bool writeFlag;
            bool classificationSwaped = false;

            // Page 8 container variables (non-stimulants)
            List<GenesightDrug> drugsUAD2;
            List<GenesightDrug> drugsMod2;
            List<GenesightDrug> drugsSig2;
            List<GenesightDrug> drugsNP2;
            List<GenesightDrug>[] listArr2;

            // Page 8 control variables
            int stimulantsFound;
            int dexedrineIndex;
            bool endOfSection;
            bool stimWriteToggle;
            bool UADStimulantsAdded;

            // Debug variables; remove later
            // Used to count the line number in a page
            int DEBUG_lineNum;

            // Local function to add classification numbers to drugs
            void addClassifications()
            {
                if (pageNum == 8)
                {
                    stimWriteToggle = !stimWriteToggle;
                    if (!endOfSection)
                    {
                        endOfSection = true;
                        return;
                    }
                }

                if (classifications.Count > 0 && listArr[drugSwitch].Count + listArr2[drugSwitch].Count > 0)
                {
                    // Variables for all pages
                    int index;
                    int startCount = listArr[drugSwitch].Count + listArr2[drugSwitch].Count;
                    int NPEndCount = 0;
                    bool tenFound = false;
                    GenesightDrug tempDrug;

                    // Variables exclusively used for page 8
                    int firstUAD = 0;
                    bool stimulantsToggle = false;

                    if (pageNum != 8)
                        index = startCount - classifications.Count;
                    else
                        index = 0;

                    foreach (string[] inArr in classifications)
                    {
                        tempDrug = null;
                        tenFound = false;

                        foreach (string inStr in inArr)
                        {
                            if (pageNum == 8 && index == stimulantsFound && !stimulantsToggle)
                            {
                                stimulantsToggle = true;
                                index = 0;
                            }

                            if (inStr == "10")
                            {
                                tenFound = true;
                                if (pageNum != 8 && classificationSwaped || pageNum == 8 && index == 0 && !stimulantsToggle)
                                {
                                    if (pageNum != 8 || !stimulantsToggle)
                                    {
                                        tempDrug = listArr[drugSwitch][index];
                                        listArr[drugSwitch].RemoveAt(index);
                                    }
                                    else
                                    {
                                        tempDrug = listArr2[drugSwitch][index];
                                        listArr2[drugSwitch].RemoveAt(index);
                                    }
                                }
                                else
                                {
                                    if (drugSwitch != 0)
                                        classificationSwaped = true;
                                    if (!stimulantsToggle)
                                    {
                                        // Replace with listArr[drugSwitch](index)?
                                        tempDrug = listArr[drugSwitch][listArr[drugSwitch].Count - 1];
                                        listArr[drugSwitch].RemoveAt(listArr[drugSwitch].Count - 1);
                                    }
                                    else if (stimulantsToggle && index == 0 && drugSwitch == 0 && firstUAD == 0)
                                    {
                                        tempDrug = listArr2[drugSwitch][index];
                                        listArr2[drugSwitch].RemoveAt(index);
                                        firstUAD = 1;
                                    }
                                    else
                                    {
                                        tempDrug = listArr2[drugSwitch][listArr2[drugSwitch].Count - 1];
                                        listArr2[drugSwitch].RemoveAt(listArr2[drugSwitch].Count - 1);
                                    }
                                }
                                NPEndCount++;
                                break;
                            }
                        }

                        if (tenFound)
                        {
                            if (!stimulantsToggle || stimulantsToggle && index == 0 && drugSwitch == 0 && firstUAD == 1)
                            {
                                listArr[3].Add(tempDrug);
                                stimulantsFound--;
                            }
                            else
                                listArr2[3].Add(tempDrug);
                        }

                        foreach (string inStr in inArr)
                        {
                            if (inStr == "*")
                            {
                                continue;
                            }
                            if (tenFound)
                            {
                                if (!stimulantsToggle || stimulantsToggle && index == 0 && drugSwitch == 0 && firstUAD == 1)
                                    listArr[3][listArr[3].Count - 1].AddClassification(int.Parse(inStr));
                                else
                                    listArr2[3][listArr2[3].Count - 1].AddClassification(int.Parse(inStr));
                                index--;
                            }
                            else
                            {
                                if (!stimulantsToggle)
                                    listArr[drugSwitch][index].AddClassification(int.Parse(inStr));
                                else
                                    listArr2[drugSwitch][index].AddClassification(int.Parse(inStr));
                            }
                        }
                        if (firstUAD == 1)
                            firstUAD = 2;
                        index++;
                    }
                    if (startCount == NPEndCount)
                    {
                        classificationSwaped = false;
                    }
                }
                classifications = new List<string[]>();
                endOfSection = false;
                stimulantsFound = 0;
            }

            pageNum = 1;

            foreach (List<string> inPage in data)
            {
                // Set/Reset variables
                pageTitle = "";
                smokerStatus = "";
                scientific = "";
                commerical = "";
                tempPage = null;
                drugsUAD = new List<GenesightDrug>();
                drugsMod = new List<GenesightDrug>();
                drugsSig = new List<GenesightDrug>();
                drugsNP = new List<GenesightDrug>();
                listArr = [drugsUAD, drugsMod, drugsSig, drugsNP];
                possibleDrugSci = "";
                possibleDrugCom = "";
                tempStrArray = null;
                classifications = new List<string[]>();
                drugSwitch = -1;
                firstPass = true;
                titleSet = false;
                statusSet = false;
                writeFlag = false;
                classificationSwaped = false;

                drugsUAD2 = new List<GenesightDrug>();
                drugsMod2 = new List<GenesightDrug>();
                drugsSig2 = new List<GenesightDrug>();
                drugsNP2 = new List<GenesightDrug>();
                listArr2 = [drugsUAD2, drugsMod2, drugsSig2, drugsNP2];
                stimulantsFound = 0;
                dexedrineIndex = -1;
                endOfSection = false;
                stimWriteToggle = false;
                UADStimulantsAdded = false;

                DEBUG_lineNum = 0;

                // Extracts data up to page 8
                if (pageNum > 8)
                {
                    pageNum++;
                    continue;
                }

                if (pageNum == 8)
                {
                    pageTitle = "Stimulants";
                    smokerStatus = "Smokers and non-smokers";
                    titleSet = true;
                    statusSet = true;
                }

                foreach (string inLine in inPage)
                {
                    DEBUG_lineNum++;
                    if (!titleSet)
                    {
                        foreach (string title in titles)
                        {
                            if (inLine.Contains(title))
                            {
                                pageTitle = title;
                                titleSet = true;
                                break;
                            }
                        }
                    }
                    if (!statusSet)
                    {
                        foreach (string status in statuses)
                        {
                            if (inLine.Contains(status))
                            {
                                smokerStatus = status;
                                statusSet = true;
                                break;
                            }
                        }
                    }

                    if (inLine.Trim() == "Use as")
                    {
                        if (!firstPass)
                        {
                            addClassifications();
                            if (!UADStimulantsAdded)
                            {
                                UADStimulantsAdded = true;
                                for (int i = stimulantsFound; i > 0; i--)
                                {
                                    classifications.Add(["*"]);
                                }
                            }
                        }
                        firstPass = false;
                        drugSwitch = 0;
                        continue;
                    }
                    else if (inLine.Trim() == "Moderate")
                    {
                        addClassifications();
                        drugSwitch = 1;
                        continue;
                    }
                    else if (inLine.Trim() == "Significant")
                    {
                        addClassifications();
                        drugSwitch = 2;
                        continue;
                    }

                    if (drugSwitch != -1)
                    {
                        writeFlag = false;

                        // If inLine does not contain a right parentheses ')' AND matches the following in order from start to the first left parentheses '(' if found
                        // Otherwise checks whole string
                        //   5 to 30 of any character
                        if (!inLine.Contains(')') && Regex.IsMatch(inLine.Trim().Split('(')[0], "^.{5,30}$"))
                        {
                            tempStrArray = inLine.Trim().Split('(');
                            possibleDrugSci = tempStrArray[0].Trim();
                            if (tempStrArray.Length > 1)
                            {
                                possibleDrugCom = tempStrArray[1].Replace(" ,", ",").Trim();
                            }
                        }

                        // If inLine matches the following in order
                        //   4 to 25 non-digit characters
                        //   1 left parentheses '('
                        //   4 to 15 non-digit characters
                        //   1 right parentheses ')'
                        //   0 to unlimited whitespace characters
                        //   0 to unlimited digit characters
                        //   0 to unlimited of the following 3 statements in order
                        //     1 to unlimited digit characters
                        //     1 comma ','
                        //     1 to unlimited digit characters
                        //   0 to unlimited whitespace characters
                        // Length is less than 40 characters
                        if (Regex.IsMatch(inLine.Trim(), "^\\D{4,25}\\(\\D{4,15}\\)\\s*\\d*(\\d+,\\d+)*\\s*$") && inLine.Length < 40)
                        {
                            tempStrArray = inLine.Split("(");
                            scientific = tempStrArray[0].Trim();
                            commerical = tempStrArray[1].Split(")")[0].Trim();
                            writeFlag = true;

                            // For the drug dextroamphetamine, the classification number ("10") is listed in the same line
                            //   instead of being in a seperate line later in the input
                            // The variable dexedrineIndex allows the algorithm to recognize when all stimulant drugs have been found
                            //   and all further drugs are to be marked as non-stimulants because of this anomaly
                            if (inLine.Trim() == "dextroamphetamine (Dexedrine )10")
                            {
                                if (stimulantsFound == 0)
                                    classifications.Add(["10"]);
                                else
                                    dexedrineIndex = stimulantsFound;
                            }
                        }

                        // If inLine matches the following in order
                        //   0 or 1 left parentheses '('
                        //   4 to 15 non-digit characters
                        //   1 right parentheses ')'
                        //   0 to unlimited whitespace characters
                        //   0 to unlimited digit characters
                        //   0 to unlimited of the following 3 statements in order
                        //     1 to unlimited digit characters
                        //     1 comma ','
                        //     1 to unlimited digit characters
                        //   0 to unlimited whitespace characters
                        // Length is less than 40 characters
                        else if (Regex.IsMatch(inLine.Trim(), "^\\(?\\D{4,15}\\)\\s*\\d*(\\d+,\\d+)*\\s*$"))
                        {
                            scientific = possibleDrugSci;
                            if (possibleDrugCom != "")
                            {
                                commerical = possibleDrugCom + " " + inLine.Split(")")[0].Trim();
                            }
                            else
                            {
                                commerical = inLine.Split("(")[1].Split(")")[0].Trim();
                            }
                            possibleDrugSci = "";
                            possibleDrugCom = "";
                            writeFlag = true;
                        }

                        if (writeFlag)
                        {
                            if (!stimWriteToggle)
                            {
                                stimulantsFound++;
                                switch (drugSwitch)
                                {
                                    case 0:
                                        drugsUAD.Add(new GenesightDrug(scientific, commerical));
                                        break;
                                    case 1:
                                        drugsMod.Add(new GenesightDrug(scientific, commerical));
                                        break;
                                    case 2:
                                        drugsSig.Add(new GenesightDrug(scientific, commerical));
                                        break;
                                }
                            }
                            else
                            {
                                switch (drugSwitch)
                                {
                                    case 0:
                                        drugsUAD2.Add(new GenesightDrug(scientific, commerical));
                                        break;
                                    case 1:
                                        drugsMod2.Add(new GenesightDrug(scientific, commerical));
                                        break;
                                    case 2:
                                        drugsSig2.Add(new GenesightDrug(scientific, commerical));
                                        break;
                                }
                            }
                        }
                    }

                    // If inLine matches the following
                    //   0 to unlimited of the following in order
                    //     1 of any digit character
                    //     0 or 1 comma ','
                    //   1 of any digit character
                    // AND if inLine does NOT match the following
                    //   4 to unlimited of the following in order
                    //     1 of any digit character
                    if (Regex.IsMatch(inLine.Trim(), "^([0-9],?)*[0-9]$") && !Regex.IsMatch(inLine.Trim(), "^[0-9]{4,}$"))
                    {
                        classifications.Add(inLine.Trim().Split(','));
                        if (dexedrineIndex == classifications.Count)
                        {
                            classifications.Add(["10"]);
                            dexedrineIndex = -1;
                        }
                    }
                }

                addClassifications();
                tempPage = new GenesightPage(pageTitle, smokerStatus);
                foreach (GenesightDrug drug in drugsUAD)
                {
                    tempPage.AddUAD(drug);
                }
                foreach (GenesightDrug drug in drugsMod)
                {
                    tempPage.AddMod(drug);
                }
                foreach (GenesightDrug drug in drugsSig)
                {
                    tempPage.AddSig(drug);
                }
                foreach (GenesightDrug drug in drugsNP)
                {
                    tempPage.AddNoProven(drug);
                }
                pages.Add(tempPage);

                if (pageNum == 8)
                {
                    tempPage = new GenesightPage("Non-Stimulants", smokerStatus);
                    foreach (GenesightDrug drug in drugsUAD2)
                    {
                        tempPage.AddUAD(drug);
                    }
                    foreach (GenesightDrug drug in drugsMod2)
                    {
                        tempPage.AddMod(drug);
                    }
                    foreach (GenesightDrug drug in drugsSig2)
                    {
                        tempPage.AddSig(drug);
                    }
                    foreach (GenesightDrug drug in drugsNP2)
                    {
                        tempPage.AddNoProven(drug);
                    }
                    pages.Add(tempPage);
                }
                pageNum++;
            }
        }

        // Extracts all ICD-10 Codes
        public void findICD10Codes()
        {
            int pageNum = 1;
            string tempString;

            foreach (List<string> inPage in data)
            {
                // Only scans pages 16 and 17
                if (pageNum < 16)
                {
                    pageNum++;
                    continue;
                }
                else if (pageNum > 17)
                {
                    break;
                }

                foreach (string inLine in inPage)
                {
                    tempString = inLine.Trim();

                    if (Regex.IsMatch(tempString, "^[A-Z][0-9]{2}(.[0-9]{1,2})?$") && !icd10Codes.Contains(tempString))
                    {
                        icd10Codes.Add(tempString);
                    }
                }
                pageNum++;
            }
        }

        // Finds patient's (MTHFR?) genotype from page 18
        public void findGenotype()
        {
            int pageNum = 1;
            string[] activities = { "Normal Activity", "Reduced Activity", "Intermediate Activity" };
            string[] alleles = { "C/C", "T/T", "C/T" };

            foreach (List<string> inPage in data)
            {
                // Only scans page 18
                if (pageNum != 18)
                {
                    pageNum++;
                    continue;
                }
                else
                {
                    genotype = "MTHFR";
                }

                foreach (string inLine in inPage)
                {
                    if (inLine.Contains("Activity"))
                    {
                        for (int i = 0; i < activities.Length; i++)
                        {
                            if (inLine.Contains(activities[i]))
                            {
                                genotypeActivity = activities[i];
                                genotypeAllele = alleles[i];
                                break;
                            }
                        }
                    }
                }
            }
        }

        public override string getJSONReport()
        {
            GenesightResult result = new GenesightResult(name, dob, physician, orderNumbers, reportDates, genotype, genotypeActivity, genotypeAllele);

            foreach (GenesightPage page in pages)
            {
                result.AddPage(page);
            }
            foreach (GenesightGene gene in pharmacodynamicGenes)
            {
                result.AddGene(gene, 0);
            }
            foreach (GenesightGene gene in pharmacokineticGenes)
            {
                result.AddGene(gene, 1);
            }
            foreach (GenesightGene gene in additionalGenes)
            {
                result.AddGene(gene, 2);
            }
            foreach (string code in icd10Codes)
            {
                result.AddICD10Code(code);
            }
            return JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
        }

        public GenesightResult getResult()
        {
            GenesightResult result = new GenesightResult(name, dob, physician, orderNumbers, reportDates, genotype, genotypeActivity, genotypeAllele);

            foreach (GenesightPage page in pages)
            {
                result.AddPage(page);
            }
            foreach (GenesightGene gene in pharmacodynamicGenes)
            {
                result.AddGene(gene, 0);
            }
            foreach (GenesightGene gene in pharmacokineticGenes)
            {
                result.AddGene(gene, 1);
            }
            foreach (GenesightGene gene in additionalGenes)
            {
                result.AddGene(gene, 2);
            }
            foreach (string code in icd10Codes)
            {
                result.AddICD10Code(code);
            }
            return result;
        }
    }
}