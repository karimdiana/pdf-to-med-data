using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.DocumentLayoutAnalysis.ReadingOrderDetector;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using UglyToad.PdfPig.DocumentLayoutAnalysis;
using System.ComponentModel;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;
using PDFExtractor.Models;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Drawing.Drawing2D;
using PDFExtractor.TextToJson;
using PDFExtractor.GeneSight;
using PDFExtractor.Models.DataMining;
using CsvHelper;
using System.Globalization;

namespace PDFExtractor
{
    public partial class Extractor : Form
    {
        #region Class Variables
        /// <summary>
        /// Variable to access the connected database throughout the application's runtime.
        /// </summary>
        private readonly IMongoDatabase database;

        /// <summary>
        /// This contains a list of all patients housed in the database.
        /// </summary>
        List<Patient> allPatients = [];

        /// <summary>
        /// This contains a list of all medications related to a selected patient.
        /// </summary>
        List<Medication> allMeds = [];

        /// <summary>
        /// This is a list that determines the number of genes in each graph gallery image.
        /// </summary>
        private readonly int[] medicationsGraphCount = [
            6, // Antidepressants
            7, // Anxiolytics and Hypnotics
            5, // Antipsychotics
            7, // Mood Stabilizers
            5, // Stimulants
            4 // Non-stimulants
            ];

        /// <summary>
        /// These are lists for reactivity levels of genes to be used with the graphs.
        /// Its values are set when loading the page and used when graph creation begins.
        /// </summary>
        List<string>[] geneReactivity = new List<string>[16];
        List<string>[] CYP1A2Reactivity = new List<string>[2];

        /// <summary>
        /// This list uses an Image object to store bitmap data of each graph image generated.
        /// This allows the program to delete the image file created by Python and still be able to load the images.
        /// </summary>
        List<Image> gallery;

        /// <summary>
        /// These are indices for getting the current graph image, gene selected, and medication type selected.
        /// A Winforms Background Worker uses a different thread to run the graph creation, which is why these variables are needed.
        /// Due to how a Winforms Background Loader object works, it cannot edit an object "in a different thread" than the background worker, but it can still grab data from a class variable.
        /// </summary>
        int galleryIndex = -1, geneIndex = -1, medIndex = -1;

        #endregion

        #region Processing Page
        /// <summary>
        /// Constructor for the Extractor class. Will load the database and instantiates lists for the CYP1A2 gene reactivity levels.
        /// If no appsettings.json is found, the program will not run. If no database is connected, the program will not run properly.
        /// </summary>
        public Extractor()
        {
            InitializeComponent();
            statusLabel.Text = "";
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot root = builder.Build();

            // DB_URI must be added as an environment variable in the appsettings.json file located in the root directory.
            database = new MongoClient(root["DB_URI"]).GetDatabase("pdfextractor");
            if (database == null)
            {
                Console.WriteLine("DB connection failed.");
                statusLabel.Text = "Database connection failed.";
                return;
            }
            gallery = new List<Image>();

            for (int i = 0; i < 16; i++)
            {
                geneReactivity[i] = new List<string>();
            }

            CYP1A2Reactivity[0] = new List<string>();
            CYP1A2Reactivity[1] = new List<string>();
            databaseLoader.RunWorkerAsync();
        }

        /// <summary>
        /// Is run at launch to load the database and all patients into the patientGridView.
        /// </summary>
        private void DatabaseLoader_DoWork(object sender, EventArgs e)
        {
            statusLabel.Text = "Loading database";
            processButton.Enabled = false;
            tabControl.Enabled = false;

            IMongoCollection<Patient> collection = database.GetCollection<Patient>("patients");
            allPatients = collection.Find(Builders<Patient>.Filter.Empty).ToList();

            patientGridView.DataSource = allPatients;
            patientGridView.Columns["Id"].Visible = false;
            patientGridView.Columns["DateOfBirth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void DatabaseLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statusLabel.Text = string.Empty;
            processButton.Enabled = true;
            tabControl.Enabled = true;
        }

        private void ProcessButton(object sender, EventArgs e)
        {
            // If no path is specified, do not pass.
            if (folderInput.Text == "")
            {
                Console.WriteLine("No file path inputted.");
                statusLabel.Text = "You must add a file path!";
                return;
            }

            // If the path does not exist, do not pass.
            if (!Directory.Exists(folderInput.Text))
            {
                Console.WriteLine("File path not found.");
                statusLabel.Text = "Path not found!";
                return;
            }

            // Begin processing.
            processWorker.RunWorkerAsync();
        }

        /// <summary>
        /// This function takes a PDF document and extracts text from each page.
        /// </summary>
        /// <param name="document">The PDFPig document to be searched</param>
        /// <param name="name"></param>
        /// <returns>A double List of strings to be put through data extraction</returns>
        public List<List<string>> ByPageExtraction(PdfDocument document)
        {
            List<List<string>> result = new List<List<string>>();

            // For each page until max
            const int MAX_PAGE = 19;
            for (var i = 0; i < MAX_PAGE; i++)
            {
                // GetPage begins at index 1
                int pageNum = i + 1;
                Page page;

                // If the page count is less than 19, catches out of bounds index and exits loop immediately
                try
                {
                    page = document.GetPage(pageNum);
                }
                catch
                {
                    break;
                }

                // Get all words and put them into blocks
                IEnumerable<Word> words = page.GetWords(NearestNeighbourWordExtractor.Instance);
                IReadOnlyList<TextBlock> blocks = DocstrumBoundingBoxes.Instance.GetBlocks(words);
                IEnumerable<TextBlock> textBlock = UnsupervisedReadingOrderDetector.Instance.Get(blocks);

                // String to be outputted
                string outputText = "";

                // Get each word from each block and add to text
                foreach (TextBlock block in textBlock)
                {
                    foreach (TextLine line in block.TextLines)
                    {
                        foreach (Word word in line.Words)
                        {
                            outputText += word.ToString() + " ";
                        }
                        outputText += "\n";
                    }
                    outputText += "\n";
                }

                // Split output string into a list of string blocks to be iterated in the data extraction function
                List<string> pageLine = outputText.Split('\n').ToList();
                result.Add(pageLine);
            }
            return result;
        }

        /// <summary>
        /// This function is run when hitting the process button to process the PDF files in the inputBox path.
        /// </summary>
        private void ProcessWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get PDF files from the inputBox path and set progressBar length.
            string path = folderInput.Text;
            string[] files = Directory.GetFiles(path, "*.pdf");
            // If no PDF files are found, cancel the processing.
            if (files.Length == 0)
            {
                e.Cancel = true;
                return;
            }

            // Report progress for the progress bar
            processWorker.ReportProgress(-1, files.Length.ToString());

            // Create a new string for the logs collection
            string logOutput = files.Length + " PDF files found.\n\n";
            List<Patient> patients = allPatients.ToList();

            //For each PDF in the directory
            for (int fileNum = 0; fileNum < files.Length; fileNum++)
            {
                using PdfDocument document = PdfDocument.Open(files[fileNum]);
                // Get the file name without the extension
                string name = Path.GetFileName(files[fileNum]);
                name = name.Substring(0, name.Length - 4);

                processWorker.ReportProgress(fileNum, name);

                // Create the output JSON string
                logOutput += "Extracting text from " + name + ".\n";
                GenesightResult result = DataExtractor.ExtractResult(ByPageExtraction(document));
                if (result == null)
                {
                    logOutput += name + " failed extraction.\n\n";
                    continue;
                }

                logOutput += "Data extracted from " + name + ".\n";
                string outputJSON = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });

                // Output
                logOutput += "Writing JSON to file.\n";
                string outFile = files[fileNum].Split(".pdf")[0] + @"-out.json";
                File.WriteAllText(outFile, outputJSON);

                logOutput += "Beginning database uploading.\n";
                Patient patientResult = result.getPatient();

                // Check if the report already exists in the database
                bool reportExists = false;
                foreach (Patient patient in patients)
                {
                    foreach (int orderNum in patientResult.OrderNumbers)
                    {
                        if (patient.OrderNumbers.Contains(orderNum))
                        {
                            reportExists = true;
                            break;
                        }
                    }

                    if (reportExists)
                    {
                        break;
                    }
                }

                if (reportExists)
                {
                    logOutput += "Report already found.\n";
                    continue;
                }

                // Add patient information
                database.GetCollection<Patient>("patients").InsertOne(patientResult);
                allPatients.Add(patientResult);
                logOutput += patientResult.PatientName + " info added.\n";

                // Counter for the number of items added to the database (resets with each section)
                int itemsAdded = 0;

                // For each medication found in the page
                foreach (GenesightPage page in result.Pages)
                {
                    foreach (GenesightDrug drug in page.UseAsDirected)
                    {
                        Medication medication = new Medication(
                            patientResult.Id,
                            drug.Scientific + " (" + drug.Commercial + ")",
                            page.SmokingStatus,
                            "Use As Directed",
                            drug.ClinicalClassifications,
                            page.Title
                        );

                        // Add medication information
                        database.GetCollection<BsonDocument>("medications").InsertOne(medication.ToBsonDocument());
                        itemsAdded++;
                    }

                    logOutput += itemsAdded + " added to Use as Directed.\n";
                    itemsAdded = 0;

                    foreach (GenesightDrug drug in page.ModerateInteraction)
                    {
                        Medication medication = new Medication(
                            patientResult.Id,
                            drug.Scientific + " (" + drug.Commercial + ")",
                            page.SmokingStatus,
                            "Moderate Gene-drug Interaction",
                            drug.ClinicalClassifications,
                            page.Title
                        );

                        // Add medication information
                        database.GetCollection<BsonDocument>("medications").InsertOne(medication.ToBsonDocument());
                        itemsAdded++;
                    }

                    logOutput += itemsAdded + " added to Moderate Interaction.\n";
                    itemsAdded = 0;

                    foreach (GenesightDrug drug in page.SignificantInteraction)
                    {
                        Medication medication = new Medication(
                            patientResult.Id,
                            drug.Scientific + " (" + drug.Commercial + ")",
                            page.SmokingStatus,
                            "Significant Gene-drug Interaction",
                            drug.ClinicalClassifications,
                            page.Title
                        );

                        // Add medication information
                        database.GetCollection<BsonDocument>("medications").InsertOne(medication.ToBsonDocument());
                        itemsAdded++;
                    }

                    logOutput += itemsAdded + " added to Significant Interaction.\n";
                    itemsAdded = 0;
                }

                // For each gene found in the pharmacokinetics section
                foreach (GenesightGene gsGene in result.PharmacokineticGenes)
                {
                    RGene gene = new RGene(
                        patientResult.Id,
                        gsGene.gene,
                        gsGene.allele,
                        gsGene.effect,
                        gsGene.activity,
                        gsGene.description
                    );

                    // Add gene information
                    database.GetCollection<BsonDocument>("pharmacokinetic_genes").InsertOne(gene.ToBsonDocument());
                    itemsAdded++;
                }

                logOutput += itemsAdded + " pharmacokinetic genes added.\n";
                itemsAdded = 0;

                // For each gene found in the pharmacodynamics section
                foreach (GenesightGene gsGene in result.PharmacodynamicGenes)
                {
                    RGene gene = new RGene(
                        patientResult.Id,
                        gsGene.gene,
                        gsGene.allele,
                        gsGene.effect,
                        gsGene.activity,
                        gsGene.description
                    );

                    // Add gene information
                    database.GetCollection<BsonDocument>("pharmacodynamic_genes").InsertOne(gene.ToBsonDocument());
                    itemsAdded++;
                }

                logOutput += itemsAdded + " pharmacodynamic genes added.\n";
                itemsAdded = 0;

                // For each gene found in the additional genes section
                foreach (GenesightGene gsGene in result.AdditionalGenes)
                {
                    RGene gene = new RGene(
                        patientResult.Id,
                        gsGene.gene,
                        gsGene.allele,
                        gsGene.effect,
                        gsGene.activity,
                        gsGene.description
                    );

                    // Add gene information
                    database.GetCollection<BsonDocument>("additional_genes").InsertOne(gene.ToBsonDocument());
                    itemsAdded++;
                }

                logOutput += itemsAdded + " additional gene info added.\n";
                itemsAdded = 0;
            }

            // Once the processing is completed, the log text will be uploaded to the database.
            database.GetCollection<Log>("logs").InsertOne(new Log(DateTime.Today, logOutput));
        }

        /// <summary>
        /// This function is run to update the status label and progress bar during processing.
        /// </summary>
        private void ProcessWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // A progress percentage of -1 indicates the beginning of processing
            string state = (string)e.UserState;
            state ??= "PDF " + e.ProgressPercentage;
            if (e.ProgressPercentage == -1)
            {
                progressBar.Maximum = Int32.Parse(state);
                progressBar.Visible = true;
                return;
            }

            // Subsequent progress percentages indicate the current file being processed
            statusLabel.Text = state + " processing.";
            progressBar.Value = e.ProgressPercentage + 1;
        }

        private void ProcessWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                statusLabel.Text = "No files found!";
                return;
            }

            if (e.Error != null)
            {
                Console.WriteLine(e.Error);
                statusLabel.Text = "Error: " + e.Error; return;
            }

            // Once the processing is completed, the output text will be updated.
            statusLabel.Text = "Processing completed!";
        }

        private void DragNDropL_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderInput.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void DragOrClick_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data == null) return;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void DragOrClick_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null) return;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    folderInput.Text = files[0];
                }
            }
        }
        #endregion

        #region Data Viewing Page

        #region Loading Patient Table
        private void PatientSearchBox_TextChanged(object sender, EventArgs e)
        {
            FilterPatientTable();
        }

        /// <summary>
        /// This function is used to search for patients in the patientGridView.
        /// </summary>
        /// <param name="text">Any text found in the patient search box.</param>
        private void PatientSearch(string text)
        {
            if (text == string.Empty)
            {
                LoadPatientTable();
            }
            else
            {
                //If valid Integer, search for partial or full order number
                if (int.TryParse(patientSearchBox.Text, out int number))
                {
                    LoadPatientTable(number);
                }

                //Search for patient name or clinician
                else
                {
                    LoadPatientTable(patientSearchBox.Text);
                }
            }
        }

        private void LoadPatientTable()
        {
            // Refreshes data source, which helps avoid anomalies.
            patientGridView.DataSource = null;
            patientGridView.DataSource = allPatients;
            patientGridView.Columns["Id"].Visible = false;
        }

        private void LoadPatientTable(int orderNumber)
        {
            List<Patient> filterPatient = [];

            foreach (Patient patient in allPatients)
            {
                bool addPatient = false;
                foreach (int patientOrdNum in patient.OrderNumbers)
                {
                    if (patientOrdNum.ToString().Contains(orderNumber.ToString()))
                    {
                        addPatient = true;
                        break;
                    }
                }

                if (addPatient) filterPatient.Add(patient);
            }

            patientGridView.DataSource = filterPatient;
        }

        private void LoadPatientTable(string name)
        {
            List<Patient> filterPatient = [];

            foreach (Patient patient in allPatients)
            {
                string patientName = patient.PatientName.ToLower();
                string clinicianName = patient.Clinician.ToLower();
                name = name.ToLower();

                if (patientName.Contains(name) || clinicianName.Contains(name))
                {
                    filterPatient.Add(patient);
                }
            }

            patientGridView.DataSource = filterPatient;
        }

        private void FilterPatientTable()
        {
            if (patientGridView.DataSource == null) return;

            PatientSearch(patientSearchBox.Text);
        }

        private void patientGridView_DataSourceChanged(object sender, EventArgs e)
        {
            PatientCountValue.Text = patientGridView.RowCount.ToString();
            if (patientGridView.RowCount < 1)
            {
                return;
            }

            UpdateICD10();
        }

        private void UpdateICD10()
        {
            // Catches if no patient is selected or in case of errors
            if (patientGridView.CurrentRow == null)
            {
                icd10L.Text = "ICD10 Codes:" + Environment.NewLine + "None";
                return;
            }

            Patient patient = (Patient)patientGridView.CurrentRow.DataBoundItem;
            icd10L.Text = patient.ICD10s();
        }
        #endregion

        #region Loading Medication and Gene Tables
        private void Patient_LoadOthers(object sender, DataGridViewCellEventArgs e)
        {
            UpdateICD10();
            LoadMedGeneTables();
        }

        private void PatientSearch_KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoadMedGeneTables();
            }
        }

        private void LoadMedGeneTables()
        {
            if (patientGridView.CurrentRow == null) return;

            LoadMedTable();
            LoadGeneTable("pharmacodynamic_genes", pdynamicGridView);
            LoadGeneTable("pharmacokinetic_genes", pkineticGridView);
            LoadGeneTable("additional_genes", additionalGridView);
        }

        private void LoadMedTable()
        {
            // Pull all medications related to the patient
            Patient patient = (Patient)patientGridView.CurrentRow.DataBoundItem;
            IMongoCollection<Medication> medications = database.GetCollection<Medication>("medications");
            allMeds = medications.Find(Builders<Medication>.Filter.Eq(m => m.patientId, patient.Id)).ToList();

            // Perform filtering and display
            FilterMedTable(medicationSearch.Text);
            medicationsGridView.Columns["patientId"].Visible = false;
        }

        private void LoadGeneTable(string databaseTable, DataGridView dataGridView)
        {
            Patient patient = (Patient)patientGridView.CurrentRow.DataBoundItem;
            IMongoCollection<RGene> geneDocuments = database.GetCollection<RGene>(databaseTable);
            List<RGene> genes = geneDocuments.Find(Builders<RGene>.Filter.Eq(g => g.patientId, patient.Id)).ToList();

            dataGridView.DataSource = genes;
            dataGridView.Columns["patientId"].Visible = false;
        }

        #endregion

        #region Medication Table
        private void ShowMedication()
        {
            if (medicationsGridView.CurrentRow == null) return;

            string text = ((Medication)medicationsGridView.CurrentRow.DataBoundItem).ToString();
            MessageBox.Show(text, "Full Description");
        }

        private void Medication_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowMedication();
        }

        private void FilterMedTable(string filter)
        {
            // Ignore if the medication search box is empty
            if (filter == string.Empty)
            {
                medicationsGridView.DataSource = allMeds;
                return;
            }

            filter = filter.ToLower();
            string[] filterWords = filter.Split(' ');

            List<Medication> filterMeds = [];
            foreach (Medication med in allMeds)
            {
                /*
                 * For each medication, get the values to be filtered.
                 * If all filtered words are found in the values, add the medication to the list.
                 */
                string[] values = [med.Name.ToLower(), med.Status.ToLower(), med.Interaction.ToLower(), med.Type.ToLower()];

                int filterCount = 0;
                foreach (string word in filterWords)
                {
                    if (values[0].Contains(word))
                    {
                        filterCount++;
                    }
                    else if (values[1].Contains(word))
                    {
                        filterCount++;
                    }
                    else if (values[2].Contains(word))
                    {
                        filterCount++;
                    }
                    else if (values[3].Contains(word))
                    {
                        filterCount++;
                    }
                }

                if (filterCount == filterWords.Length)
                {
                    filterMeds.Add(med);
                }
            }

            medicationsGridView.DataSource = filterMeds;
        }

        private void MedicationSearch_TextChanged(object sender, EventArgs e)
        {
            if (medicationsGridView.DataSource == null) return;
            FilterMedTable(medicationSearch.Text);
        }

        private void MedicationSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ShowMedication();
            }
        }

        private void medicationsGridView_DataSourceChanged(object sender, EventArgs e)
        {
            MedicationCountValue.Text = medicationsGridView.RowCount.ToString();
        }

        private void MedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSmokerSelection();
            medIndex = MedListBox.SelectedIndex;
        }
        #endregion

        #region Gene Tables
        private void Pdynamic_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string text = ((RGene)pdynamicGridView.CurrentRow.DataBoundItem).ToString();
            MessageBox.Show(text, "Full Description");
        }

        private void Pkineticic_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string text = ((RGene)pkineticGridView.CurrentRow.DataBoundItem).ToString();
            MessageBox.Show(text, "Full Description");
        }

        private void Additional_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string text = ((RGene)additionalGridView.CurrentRow.DataBoundItem).ToString();
            MessageBox.Show(text, "Full Description");
        }
        #endregion
        #endregion

        #region Data Analytics Page
        #region Gene and Smoker Selection
        private void SmokerStatusClicked(object sender, EventArgs e)
        {
            UpdateGeneReactivity();
        }

        private void UpdateSmokerSelection()
        {
            // If the med type is not any of the 'A's or the gene is not CYP1A2, disable the smoker buttons, as the option has no effect
            if (MedListBox.SelectedIndex > 2 && GeneListBox.SelectedIndex != 2)
            {
                SmokerButton.Enabled = false;
                SmokerButton.Checked = false;
                NonSmokerButton.Enabled = false;
                NonSmokerButton.Checked = false;
            }
            else
            {
                NonSmokerButton.Checked = true;
                SmokerButton.Enabled = true;
                NonSmokerButton.Enabled = true;
            }

            // CYP1A2
            if (GeneListBox.SelectedIndex == 2)
            {
                geneReactivity[2] = NonSmokerButton.Checked ? CYP1A2Reactivity[0] : CYP1A2Reactivity[1];
            }
        }

        private void UpdateGeneReactivity()
        {
            if (GeneListBox.SelectedIndex == -1)
            {
                return;
            }

            // CYP1A2
            if (GeneListBox.SelectedIndex == 2)
            {
                geneReactivity[2] = NonSmokerButton.Checked ? CYP1A2Reactivity[0] : CYP1A2Reactivity[1];
            }

            RadioButton[] levels = [reactivityOne, reactivityTwo, reactivityThree, reactivityFour, reactivityFive];

            // Update all of the buttons when a new gene is selected
            for (int i = 0; i < levels.Length; i++)
            {
                // Disable the buttons that aren't needed
                if (i > geneReactivity[GeneListBox.SelectedIndex].Count - 1)
                {
                    levels[i].Checked = false;
                    levels[i].Text = string.Empty;
                    levels[i].Enabled = false;
                    continue;
                }

                if (i == 0)
                {
                    levels[i].Checked = true;
                }
                levels[i].Enabled = true;
                levels[i].Text = geneReactivity[GeneListBox.SelectedIndex][i];
            }

            geneIndex = GeneListBox.SelectedIndex;
        }

        private void GeneListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSmokerSelection();

            UpdateGeneReactivity();
        }
        #endregion

        #region Graphing
        private void GraphLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);

            // List of Medication interactions that will be graphed. Interactions will be added in this function
            List<MedicationInteraction> interactions = [];

            // Get all data from the database
            IMongoCollection<Patient> collection = database.GetCollection<Patient>("patients");
            allPatients = collection.Find(Builders<Patient>.Filter.Empty).ToList();
            IMongoCollection<Medication> medications = database.GetCollection<Medication>("medications");
            IMongoCollection<RGene> dgenes = database.GetCollection<RGene>("pharmacodynamic_genes");
            IMongoCollection<RGene> kgenes = database.GetCollection<RGene>("pharmacokinetic_genes");
            IMongoCollection<RGene> agenes = database.GetCollection<RGene>("additional_genes");

            List<Patient> filteredPatients = [];
            foreach (var patient in allPatients)
            {
                // Connect the radio buttons and get the reactivity level
                string reactivity = string.Empty;
                RadioButton[] levels = [reactivityOne, reactivityTwo, reactivityThree, reactivityFour, reactivityFive];
                foreach (RadioButton level in levels)
                {
                    if (level.Checked)
                    {
                        reactivity = level.Text;
                    }
                }
                /*
                 * Add genes from each type to a list that match the selection.
                 * This gene is checked against each patient.
                 * Given no gene is added, the wrong category was searched.
                 * The first (and only) gene in the list is the connected gene.
                 */
                List<RGene> genes = dgenes.Find(Builders<RGene>.Filter.Eq(g => g.Gene, GeneListBox.Items[geneIndex].ToString()) & Builders<RGene>.Filter.Eq(g => g.patientId, patient.Id)).ToList();
                if (genes.Count == 0)
                {
                    genes = kgenes.Find(Builders<RGene>.Filter.Eq(g => g.Gene, GeneListBox.Items[geneIndex].ToString()) & Builders<RGene>.Filter.Eq(g => g.patientId, patient.Id)).ToList();
                }
                if (genes.Count == 0)
                {
                    genes = agenes.Find(Builders<RGene>.Filter.Eq(g => g.Gene, GeneListBox.Items[geneIndex].ToString()) & Builders<RGene>.Filter.Eq(g => g.patientId, patient.Id)).ToList();
                }

                if (genes.Count == 0)
                {
                    return;
                }

                RGene connectedGene = genes[0];

                // Check if the gene is CYP1A2 and if the selected medication is a smoker or not
                if (geneIndex == 2)
                {
                    if (NonSmokerButton.Checked)
                    {
                        if (connectedGene.Effect[0].Split(": ")[1] == reactivity)
                        {
                            filteredPatients.Add(patient);
                            continue;
                        }
                    }
                    else
                    {
                        if (connectedGene.Effect[1].Split(": ")[1] == reactivity)
                        {
                            filteredPatients.Add(patient);
                            continue;
                        }
                    }
                }

                // If the gene is not COMT, check effect, else check allele
                if (geneIndex != 14)
                {
                    if (connectedGene.Effect[0] == reactivity)
                    {
                        filteredPatients.Add(patient);
                        continue;
                    }
                }

                if (connectedGene.Allele == reactivity)
                {
                    filteredPatients.Add(patient);
                }

            }

            // Create MedicationInteraction objects with each name in the selected category
            string[] medicationNames = MedicationInteraction.medicationsList[medIndex];
            foreach (string med in medicationNames)
            {
                MedicationInteraction interaction = new MedicationInteraction
                {
                    Medication = med
                };

                interactions.Add(interaction);
            }

            // Add each patients medication reaction to the list based on their gene reactivity
            foreach (var patient in filteredPatients)
            {
                // Check smoker status based on the radio button
                List<Medication> connectedMeds;
                if (medIndex < 3)
                {
                    string status = "Smokers";
                    if (NonSmokerButton.Checked) status = "Non-Smokers";
                    connectedMeds = medications.Find(Builders<Medication>.Filter.Eq(m => m.Status, status) & Builders<Medication>.Filter.Eq(m => m.Type, MedListBox.Items[medIndex].ToString()) & Builders<Medication>.Filter.Eq(m => m.patientId, patient.Id)).ToList();
                }
                else
                {
                    connectedMeds = medications.Find((Builders<Medication>.Filter.Eq(m => m.Status, "Smokers and non-smokers") | Builders<Medication>.Filter.Eq(m => m.Status, "smokers and non-smokers")) & Builders<Medication>.Filter.Eq(m => m.Type, MedListBox.Items[medIndex].ToString()) & Builders<Medication>.Filter.Eq(m => m.patientId, patient.Id)).ToList();
                }

                // Check the level for each medication
                foreach (Medication med in connectedMeds)
                {
                    for (int i = 0; i < medicationNames.Length; i++)
                    {
                        if (medicationNames[i] == med.Name)
                        {
                            if (med.Interaction.Contains("Use"))
                            {
                                interactions[i].UAD++;
                            }
                            else if (med.Interaction.Contains("Mod"))
                            {
                                interactions[i].Mod++;
                            }
                            else if (med.Interaction.Contains("Sig"))
                            {
                                interactions[i].Sig++;
                            }
                            break;
                        }
                    }
                }
            }

            int count = 0, pageIndex = 0;
            List<List<MedicationInteraction>> pageInteractions = [new List<MedicationInteraction>()];

            /*
             * Add the interactinos to each graph image.
             * The number of interactions in each image/page is determined by the medicationsGraphCount value. 
             */
            for (int i = 0; i < interactions.Count; i++)
            {
                // New page
                if (count == medicationsGraphCount[medIndex])
                {
                    count = 0;
                    pageIndex++;
                    pageInteractions.Add(new List<MedicationInteraction>());
                }

                pageInteractions[pageIndex].Add(interactions[i]);
                count++;
            }

            // Refresh gallery after collecting data (if any)
            gallery.Clear();
            galleryIndex = -1;

            foreach (List<MedicationInteraction> page in pageInteractions)
            {
                // Turn the data into a CSV file for Python to read
                using (var writer = new StreamWriter("Python/data.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(page);
                }

                // Create a command prompt script to run the Python code located in its folder
                string pypath = path + @"\Python";
                ProcessStartInfo ProcessInfo;
                Process process;
                string cd = "cd " + pypath;
                string acivatePythonVEnv = pypath + @"\Scripts\activate";
                string runConversion = pypath + @"\convert_to_chart.py";

                ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + cd + " && " + acivatePythonVEnv + " && python " + runConversion);
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = false;

                // Automatically closes the command prompt window after waiting for processing to complete.
                process = Process.Start(ProcessInfo);
                process.WaitForExit();
                process.Close();

                // Load the generated image into the gallery and then delete the file
                var filename = pypath + @"\data.png";
                Image img;
                using (var stream = File.OpenRead(filename))
                {
                    img = new Bitmap(stream);
                }
                gallery.Add(img);
                File.Delete(filename);
                File.Delete(pypath + @"\data.csv");
            }
        }

        private void GraphLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Display gallery index info, else 0s if no graphs are generated
            if (gallery.Count > 0)
            {
                GraphBox.Image = gallery[0];
                GalleryLabel.Text = "Graph Images: 1 / " + gallery.Count;
                galleryIndex = 0;
            }
            else
            {
                GalleryLabel.Text = "Graph Images: 0 / 0";
            }

            RightGalleryButton.Enabled = true;
            LeftGalleryButton.Enabled = true;
            GenerateGraphButton.Enabled = true;
        }

        private void GraphButtonClick(object sender, EventArgs e)
        {
            // Guard clauses to check for valid selections
            bool invalid = false;
            if (MedListBox.SelectedIndex == -1) invalid = true;
            if (GeneListBox.SelectedIndex == -1 || GeneListBox.SelectedIndex > 14) invalid = true;
            if (!(NonSmokerButton.Checked || SmokerButton.Checked) && (MedListBox.SelectedIndex < 3 || GeneListBox.SelectedIndex == 2)) invalid = true;
            if (!(reactivityOne.Checked || reactivityTwo.Checked || reactivityThree.Checked || reactivityFour.Checked || reactivityFive.Checked)) invalid = true;

            if (invalid)
            {
                MessageBox.Show("Please select a valid medication, gene, smoking status, and reactivity level.", "Invalid Selection");
                return;
            }

            // Lock selections until graphs are generated
            RightGalleryButton.Enabled = false;
            LeftGalleryButton.Enabled = false;
            GenerateGraphButton.Enabled = false;
            GalleryLabel.Text = "Loading...";
            GraphLoader.RunWorkerAsync();
        }

        private void RightGalleryButton_Click(object sender, EventArgs e)
        {
            if (galleryIndex + 1 < gallery.Count)
            {
                GraphBox.Image = gallery[++galleryIndex];
                GalleryLabel.Text = "Graph Images: " + (galleryIndex + 1) + " / " + gallery.Count;
            }
        }

        private void LeftGalleryButton_Click(object sender, EventArgs e)
        {
            if (galleryIndex - 1 >= 0)
            {
                GraphBox.Image = gallery[--galleryIndex];
                GalleryLabel.Text = "Graph Images: " + (galleryIndex + 1) + " / " + gallery.Count;
            }
        }
        #endregion
        #endregion

        #region Other Functions
        /// <summary>
        /// This reloads data in the data viewing page when the tab is selected and will reload all gene reactivity data.
        /// </summary>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
                UpdatePatients();
                LoadPatientTable();
            }
            else if (tabControl.SelectedIndex == 2)
            {
                List<RGene> pharmacoD = database.GetCollection<RGene>("pharmacodynamic_genes").Find(Builders<RGene>.Filter.Empty).ToList();
                List<RGene> pharmacoK = database.GetCollection<RGene>("pharmacokinetic_genes").Find(Builders<RGene>.Filter.Empty).ToList();
                List<RGene> additional = database.GetCollection<RGene>("additional_genes").Find(Builders<RGene>.Filter.Empty).ToList();

                // Genes are indexed alphabetically to match the box in the page
                foreach (RGene gene in pharmacoD)
                {
                    int index = -1;
                    switch (gene.Gene)
                    {
                        case "ADRA2A":
                            index = 0;
                            break;
                        case "HLA-A*3101":
                            index = 8;
                            break;
                        case "HLA-B*1502":
                            index = 9;
                            break;
                        case "HTR2A":
                            index = 10;
                            break;
                        case "SLC6A4":
                            index = 11;
                            break;
                        default:
                            break;
                    }

                    if (index == -1) continue;

                    // Add data to the gene reactivity list that aligns with the index
                    foreach (string effect in gene.Effect)
                    {
                        if (geneReactivity[index].Contains(effect)) continue;
                        geneReactivity[index].Add(effect);
                    }
                }

                foreach (RGene gene in pharmacoK)
                {
                    int index = -1;
                    switch (gene.Gene)
                    {
                        case "CES1A1":
                            index = 1;
                            break;
                        case "CYP1A2":
                            index = 2;
                            break;
                        case "CYP2B6":
                            index = 3;
                            break;
                        case "CYP2C19":
                            index = 4;
                            break;
                        case "CYP2C9":
                            index = 5;
                            break;
                        case "CYP2D6":
                            index = 6;
                            break;
                        case "CYP3A4":
                            index = 7;
                            break;
                        case "UGT1A4":
                            index = 12;
                            break;
                        case "UGT2B15":
                            index = 13;
                            break;
                        default:
                            break;
                    }

                    if (index == -1) continue;

                    // Extract the reactivity level from the effect string specifically for CYP1A2
                    if (index == 2)
                    {
                        string nonSmoker = gene.Effect[0].Split(": ")[1];
                        string smoker = gene.Effect[1].Split(": ")[1];

                        if (CYP1A2Reactivity[0].Contains(nonSmoker)) continue;
                        CYP1A2Reactivity[0].Add(nonSmoker);

                        if (CYP1A2Reactivity[1].Contains(smoker)) continue;
                        CYP1A2Reactivity[1].Add(smoker);

                        continue;
                    }

                    foreach (string effect in gene.Effect)
                    {
                        if (geneReactivity[index].Contains(effect)) continue;
                        geneReactivity[index].Add(effect);
                    }
                }

                foreach (RGene gene in additional)
                {
                    switch (gene.Gene)
                    {
                        case "COMT":
                            if (geneReactivity[14].Contains(gene.Allele)) break;
                            geneReactivity[14].Add(gene.Allele);
                            break;
                        case "MTHFR":
                            foreach (string effect in gene.Activity)
                            {
                                if (geneReactivity[15].Contains(effect)) continue;
                                geneReactivity[15].Add(effect);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// This is a hidden function to clear the database. It can not be used if the purge button is not visible.
        /// </summary>
        private void CleanDB(object sender, EventArgs e)
        {
            database.GetCollection<BsonDocument>("patients").DeleteMany(new BsonDocument());
            database.GetCollection<BsonDocument>("medications").DeleteMany(new BsonDocument());
            database.GetCollection<BsonDocument>("pharmacokinetic_genes").DeleteMany(new BsonDocument());
            database.GetCollection<BsonDocument>("pharmacodynamic_genes").DeleteMany(new BsonDocument());
            database.GetCollection<BsonDocument>("additional_genes").DeleteMany(new BsonDocument());
        }

        private void UpdatePatients()
        {
            allPatients = database.GetCollection<Patient>("patients").Find(Builders<Patient>.Filter.Empty).ToList();
        }

        private void ProcessorPanel_Paint(object sender, PaintEventArgs e)
        {
            // Define the radius for the corners
            int cornerRadius = 20;

            // Get panel dimensions
            int width = processorPanel.Width;
            int height = processorPanel.Height;

            // Create a GraphicsPath for rounded corners
            GraphicsPath path = new();

            // Add rounded rectangle path
            path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90); // Top-left corner
            path.AddArc(width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90); // Top-right corner
            path.AddArc(width - cornerRadius, height - cornerRadius, cornerRadius, cornerRadius, 0, 90); // Bottom-right corner
            path.AddArc(0, height - cornerRadius, cornerRadius, cornerRadius, 90, 90); // Bottom-left corner
            path.CloseFigure();

            // Set the panel's region to the rounded rectangle
            processorPanel.Region = new Region(path);
        }
        #endregion
    }
}
