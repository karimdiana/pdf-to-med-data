using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace PDFExtractor
{
    partial class Extractor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            processButton = new Button();
            statusLabel = new Label();
            folderInput = new TextBox();
            folderLabel = new Label();
            folderBrowserDialog = new FolderBrowserDialog();
            progressBar = new ProgressBar();
            processWorker = new BackgroundWorker();
            purgeDatabaseButton = new Button();
            testingPurposesLabel = new Label();
            Header = new Panel();
            Line1 = new Label();
            parkviewSmallLogo = new PictureBox();
            tabControl = new TabControl();
            pdfProcessorPage = new TabPage();
            processorPanel = new Panel();
            dragNDropButton = new Button();
            dragNDropL = new Label();
            Line2 = new Label();
            dataViewerPage = new TabPage();
            MedicationCountValue = new Label();
            PatientCountValue = new Label();
            MedicationCountTitle = new Label();
            PatientCountTitle = new Label();
            instructionLabel = new Label();
            medicationSearch = new TextBox();
            patientTableL = new RoundedLabel();
            icd10L = new Label();
            additionalGridView = new DataGridView();
            additionalGenesL = new RoundedLabel();
            pkineticGridView = new DataGridView();
            pdynamicGridView = new DataGridView();
            kineticGenesL = new RoundedLabel();
            dynamicGenesL = new RoundedLabel();
            medicationsTableL = new RoundedLabel();
            medicationsGridView = new DataGridView();
            patientGridView = new DataGridView();
            patientSearchBox = new TextBox();
            Line3 = new Label();
            analyticsPage = new TabPage();
            SevereLabel = new RoundedLabel();
            ModerateLabel = new RoundedLabel();
            UADLabel = new RoundedLabel();
            GalleryLabel = new Label();
            GeneReactivityPanel = new Panel();
            reactivityFive = new RadioButton();
            reactivityFour = new RadioButton();
            reactivityThree = new RadioButton();
            reactivityOne = new RadioButton();
            reactivityTwo = new RadioButton();
            SmokerPanel = new Panel();
            NonSmokerButton = new RadioButton();
            SmokerButton = new RadioButton();
            SmokerStatusLabel = new RoundedLabel();
            GeneReactivityLabel = new RoundedLabel();
            RightGalleryButton = new Button();
            LeftGalleryButton = new Button();
            GeneListBox = new ListBox();
            MedListBox = new ListBox();
            GraphBox = new PictureBox();
            GenerateGraphButton = new Button();
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            databaseLoader = new BackgroundWorker();
            GraphLoader = new BackgroundWorker();
            Header.SuspendLayout();
            ((ISupportInitialize)parkviewSmallLogo).BeginInit();
            tabControl.SuspendLayout();
            pdfProcessorPage.SuspendLayout();
            processorPanel.SuspendLayout();
            dataViewerPage.SuspendLayout();
            ((ISupportInitialize)additionalGridView).BeginInit();
            ((ISupportInitialize)pkineticGridView).BeginInit();
            ((ISupportInitialize)pdynamicGridView).BeginInit();
            ((ISupportInitialize)medicationsGridView).BeginInit();
            ((ISupportInitialize)patientGridView).BeginInit();
            analyticsPage.SuspendLayout();
            GeneReactivityPanel.SuspendLayout();
            SmokerPanel.SuspendLayout();
            ((ISupportInitialize)GraphBox).BeginInit();
            SuspendLayout();
            // 
            // processButton
            // 
            processButton.BackColor = Color.White;
            processButton.FlatStyle = FlatStyle.Flat;
            processButton.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            processButton.ForeColor = Color.Black;
            processButton.Location = new Point(725, 151);
            processButton.Margin = new Padding(2, 3, 2, 3);
            processButton.Name = "processButton";
            processButton.Size = new Size(75, 28);
            processButton.TabIndex = 0;
            processButton.Text = "Process";
            processButton.UseVisualStyleBackColor = false;
            processButton.Click += ProcessButton;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusLabel.Location = new Point(204, 409);
            statusLabel.Margin = new Padding(2, 0, 2, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(89, 21);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "StatusLabel";
            // 
            // folderInput
            // 
            folderInput.Font = new Font("Segoe UI", 12F);
            folderInput.Location = new Point(203, 151);
            folderInput.Margin = new Padding(2);
            folderInput.Name = "folderInput";
            folderInput.PlaceholderText = "Input file path here or use box to find folder";
            folderInput.Size = new Size(404, 29);
            folderInput.TabIndex = 2;
            // 
            // folderLabel
            // 
            folderLabel.AutoSize = true;
            folderLabel.Font = new Font("Sitka Banner", 26F, FontStyle.Bold);
            folderLabel.ForeColor = Color.White;
            folderLabel.Location = new Point(263, 80);
            folderLabel.Margin = new Padding(0);
            folderLabel.Name = "folderLabel";
            folderLabel.Size = new Size(487, 50);
            folderLabel.TabIndex = 3;
            folderLabel.Text = "Select PDF File Folders to extract";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(204, 365);
            progressBar.Margin = new Padding(2);
            progressBar.Maximum = 10;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(403, 30);
            progressBar.Step = 1;
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 6;
            progressBar.Visible = false;
            // 
            // processWorker
            // 
            processWorker.WorkerReportsProgress = true;
            processWorker.WorkerSupportsCancellation = true;
            processWorker.DoWork += ProcessWorker_DoWork;
            processWorker.ProgressChanged += ProcessWorker_ProgressChanged;
            processWorker.RunWorkerCompleted += ProcessWorker_RunWorkerCompleted;
            // 
            // purgeDatabaseButton
            // 
            purgeDatabaseButton.BackColor = Color.Maroon;
            purgeDatabaseButton.FlatStyle = FlatStyle.Flat;
            purgeDatabaseButton.Font = new Font("Microsoft Sans Serif", 8.25F);
            purgeDatabaseButton.ForeColor = SystemColors.Control;
            purgeDatabaseButton.Location = new Point(1418, 37);
            purgeDatabaseButton.Margin = new Padding(2, 3, 2, 3);
            purgeDatabaseButton.Name = "purgeDatabaseButton";
            purgeDatabaseButton.Size = new Size(75, 28);
            purgeDatabaseButton.TabIndex = 10;
            purgeDatabaseButton.Text = "Purge DB";
            purgeDatabaseButton.UseVisualStyleBackColor = false;
            purgeDatabaseButton.Visible = false;
            purgeDatabaseButton.Click += CleanDB;
            // 
            // testingPurposesLabel
            // 
            testingPurposesLabel.AutoSize = true;
            testingPurposesLabel.Font = new Font("Segoe UI", 12F);
            testingPurposesLabel.Location = new Point(1366, 11);
            testingPurposesLabel.Margin = new Padding(2, 0, 2, 0);
            testingPurposesLabel.Name = "testingPurposesLabel";
            testingPurposesLabel.Size = new Size(160, 21);
            testingPurposesLabel.TabIndex = 11;
            testingPurposesLabel.Text = "Testing purposes only";
            testingPurposesLabel.Visible = false;
            // 
            // Header
            // 
            Header.AutoSize = true;
            Header.BackColor = Color.MediumSeaGreen;
            Header.Controls.Add(Line1);
            Header.Controls.Add(parkviewSmallLogo);
            Header.Dock = DockStyle.Top;
            Header.Location = new Point(0, 0);
            Header.Margin = new Padding(0);
            Header.Name = "Header";
            Header.Size = new Size(1539, 103);
            Header.TabIndex = 12;
            // 
            // Line1
            // 
            Line1.BackColor = SystemColors.ActiveCaptionText;
            Line1.Dock = DockStyle.Bottom;
            Line1.Location = new Point(0, 101);
            Line1.Margin = new Padding(2, 0, 2, 0);
            Line1.MaximumSize = new Size(8000, 2);
            Line1.Name = "Line1";
            Line1.Size = new Size(1539, 2);
            Line1.TabIndex = 13;
            Line1.Text = "Line1";
            // 
            // parkviewSmallLogo
            // 
            parkviewSmallLogo.Image = Properties.Resources.white_logo;
            parkviewSmallLogo.Location = new Point(486, 0);
            parkviewSmallLogo.Margin = new Padding(2);
            parkviewSmallLogo.Name = "parkviewSmallLogo";
            parkviewSmallLogo.Size = new Size(553, 101);
            parkviewSmallLogo.SizeMode = PictureBoxSizeMode.Zoom;
            parkviewSmallLogo.TabIndex = 5;
            parkviewSmallLogo.TabStop = false;
            // 
            // tabControl
            // 
            tabControl.AllowDrop = true;
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.Controls.Add(pdfProcessorPage);
            tabControl.Controls.Add(dataViewerPage);
            tabControl.Controls.Add(analyticsPage);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold);
            tabControl.Location = new Point(0, 103);
            tabControl.Margin = new Padding(0);
            tabControl.Name = "tabControl";
            tabControl.Padding = new Point(0, 0);
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1539, 741);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // pdfProcessorPage
            // 
            pdfProcessorPage.BackColor = Color.Ivory;
            pdfProcessorPage.Controls.Add(processorPanel);
            pdfProcessorPage.Controls.Add(Line2);
            pdfProcessorPage.Controls.Add(purgeDatabaseButton);
            pdfProcessorPage.Controls.Add(testingPurposesLabel);
            pdfProcessorPage.Location = new Point(4, 35);
            pdfProcessorPage.Margin = new Padding(2);
            pdfProcessorPage.Name = "pdfProcessorPage";
            pdfProcessorPage.Size = new Size(1531, 702);
            pdfProcessorPage.TabIndex = 0;
            pdfProcessorPage.Text = "PDF Processor";
            // 
            // processorPanel
            // 
            processorPanel.BackColor = Color.MediumSeaGreen;
            processorPanel.Controls.Add(dragNDropButton);
            processorPanel.Controls.Add(dragNDropL);
            processorPanel.Controls.Add(folderInput);
            processorPanel.Controls.Add(processButton);
            processorPanel.Controls.Add(statusLabel);
            processorPanel.Controls.Add(progressBar);
            processorPanel.Controls.Add(folderLabel);
            processorPanel.ForeColor = Color.White;
            processorPanel.Location = new Point(280, 92);
            processorPanel.Margin = new Padding(2);
            processorPanel.Name = "processorPanel";
            processorPanel.Size = new Size(994, 471);
            processorPanel.TabIndex = 15;
            processorPanel.Paint += ProcessorPanel_Paint;
            // 
            // dragNDropButton
            // 
            dragNDropButton.AllowDrop = true;
            dragNDropButton.BackColor = Color.MediumSeaGreen;
            dragNDropButton.FlatStyle = FlatStyle.Flat;
            dragNDropButton.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dragNDropButton.ForeColor = Color.White;
            dragNDropButton.Location = new Point(203, 237);
            dragNDropButton.Margin = new Padding(2, 3, 2, 3);
            dragNDropButton.Name = "dragNDropButton";
            dragNDropButton.Size = new Size(597, 101);
            dragNDropButton.TabIndex = 10;
            dragNDropButton.Text = "Choose Folder";
            dragNDropButton.UseVisualStyleBackColor = false;
            dragNDropButton.Click += DragNDropL_Click;
            dragNDropButton.DragDrop += DragOrClick_DragDrop;
            dragNDropButton.DragEnter += DragOrClick_DragEnter;
            // 
            // dragNDropL
            // 
            dragNDropL.AutoSize = true;
            dragNDropL.Font = new Font("Sitka Banner Semibold", 16F, FontStyle.Bold);
            dragNDropL.ForeColor = Color.White;
            dragNDropL.Location = new Point(203, 194);
            dragNDropL.Margin = new Padding(2, 0, 2, 0);
            dragNDropL.Name = "dragNDropL";
            dragNDropL.Size = new Size(300, 32);
            dragNDropL.TabIndex = 8;
            dragNDropL.Text = "Click or Drag and Drop into Box:";
            // 
            // Line2
            // 
            Line2.BackColor = SystemColors.ActiveCaptionText;
            Line2.Dock = DockStyle.Top;
            Line2.Location = new Point(0, 0);
            Line2.Margin = new Padding(0);
            Line2.Name = "Line2";
            Line2.Size = new Size(1531, 2);
            Line2.TabIndex = 14;
            Line2.Text = "Line2";
            // 
            // dataViewerPage
            // 
            dataViewerPage.AutoScrollMargin = new Size(0, 25);
            dataViewerPage.BackColor = Color.Ivory;
            dataViewerPage.Controls.Add(MedicationCountValue);
            dataViewerPage.Controls.Add(PatientCountValue);
            dataViewerPage.Controls.Add(MedicationCountTitle);
            dataViewerPage.Controls.Add(PatientCountTitle);
            dataViewerPage.Controls.Add(instructionLabel);
            dataViewerPage.Controls.Add(medicationSearch);
            dataViewerPage.Controls.Add(patientTableL);
            dataViewerPage.Controls.Add(icd10L);
            dataViewerPage.Controls.Add(additionalGridView);
            dataViewerPage.Controls.Add(additionalGenesL);
            dataViewerPage.Controls.Add(pkineticGridView);
            dataViewerPage.Controls.Add(pdynamicGridView);
            dataViewerPage.Controls.Add(kineticGenesL);
            dataViewerPage.Controls.Add(dynamicGenesL);
            dataViewerPage.Controls.Add(medicationsTableL);
            dataViewerPage.Controls.Add(medicationsGridView);
            dataViewerPage.Controls.Add(patientGridView);
            dataViewerPage.Controls.Add(patientSearchBox);
            dataViewerPage.Controls.Add(Line3);
            dataViewerPage.Font = new Font("Sitka Banner Semibold", 11F, FontStyle.Bold);
            dataViewerPage.ForeColor = SystemColors.ControlText;
            dataViewerPage.Location = new Point(4, 35);
            dataViewerPage.Margin = new Padding(0);
            dataViewerPage.Name = "dataViewerPage";
            dataViewerPage.Size = new Size(1531, 702);
            dataViewerPage.TabIndex = 1;
            dataViewerPage.Text = "Data Viewer";
            // 
            // MedicationCountValue
            // 
            MedicationCountValue.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MedicationCountValue.Location = new Point(959, 583);
            MedicationCountValue.Name = "MedicationCountValue";
            MedicationCountValue.Size = new Size(50, 15);
            MedicationCountValue.TabIndex = 34;
            MedicationCountValue.Text = "0";
            MedicationCountValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // PatientCountValue
            // 
            PatientCountValue.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PatientCountValue.Location = new Point(359, 583);
            PatientCountValue.Name = "PatientCountValue";
            PatientCountValue.Size = new Size(50, 15);
            PatientCountValue.TabIndex = 33;
            PatientCountValue.Text = "0";
            PatientCountValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // MedicationCountTitle
            // 
            MedicationCountTitle.AutoSize = true;
            MedicationCountTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MedicationCountTitle.Location = new Point(910, 583);
            MedicationCountTitle.Name = "MedicationCountTitle";
            MedicationCountTitle.Size = new Size(43, 15);
            MedicationCountTitle.TabIndex = 32;
            MedicationCountTitle.Text = "Count:";
            // 
            // PatientCountTitle
            // 
            PatientCountTitle.AutoSize = true;
            PatientCountTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PatientCountTitle.Location = new Point(310, 583);
            PatientCountTitle.Name = "PatientCountTitle";
            PatientCountTitle.Size = new Size(43, 15);
            PatientCountTitle.TabIndex = 31;
            PatientCountTitle.Text = "Count:";
            PatientCountTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // instructionLabel
            // 
            instructionLabel.AutoSize = true;
            instructionLabel.Font = new Font("Segoe UI", 9F);
            instructionLabel.Location = new Point(535, 583);
            instructionLabel.Margin = new Padding(2, 0, 2, 0);
            instructionLabel.Name = "instructionLabel";
            instructionLabel.Size = new Size(275, 30);
            instructionLabel.TabIndex = 30;
            instructionLabel.Text = "Click patient to load other tables.\r\nClick medications or genes to view full description.\r\n";
            instructionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // medicationSearch
            // 
            medicationSearch.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            medicationSearch.Location = new Point(629, 39);
            medicationSearch.Margin = new Padding(2);
            medicationSearch.Name = "medicationSearch";
            medicationSearch.PlaceholderText = "Medication Name";
            medicationSearch.Size = new Size(380, 27);
            medicationSearch.TabIndex = 29;
            medicationSearch.TextChanged += MedicationSearch_TextChanged;
            medicationSearch.KeyPress += MedicationSearch_KeyPress;
            // 
            // patientTableL
            // 
            patientTableL.AutoSize = true;
            patientTableL.BorderRadius = 15;
            patientTableL.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            patientTableL.ForeColor = Color.White;
            patientTableL.InsideBackColor = Color.FromArgb(83, 134, 228);
            patientTableL.Location = new Point(49, 41);
            patientTableL.Margin = new Padding(2, 0, 2, 0);
            patientTableL.Name = "patientTableL";
            patientTableL.Size = new Size(68, 23);
            patientTableL.TabIndex = 20;
            patientTableL.Text = " Patients ";
            // 
            // icd10L
            // 
            icd10L.AutoSize = true;
            icd10L.Font = new Font("Segoe UI", 9F);
            icd10L.Location = new Point(49, 583);
            icd10L.Margin = new Padding(2, 0, 2, 0);
            icd10L.Name = "icd10L";
            icd10L.Size = new Size(77, 30);
            icd10L.TabIndex = 28;
            icd10L.Text = "ICD10 Codes:\r\nNone";
            // 
            // additionalGridView
            // 
            additionalGridView.AllowUserToAddRows = false;
            additionalGridView.AllowUserToDeleteRows = false;
            additionalGridView.AllowUserToResizeColumns = false;
            additionalGridView.AllowUserToResizeRows = false;
            additionalGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            additionalGridView.BackgroundColor = Color.White;
            additionalGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Window;
            dataGridViewCellStyle7.Font = new Font("Sitka Banner Semibold", 11F, FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = Color.MediumSeaGreen;
            dataGridViewCellStyle7.SelectionForeColor = Color.White;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            additionalGridView.DefaultCellStyle = dataGridViewCellStyle7;
            additionalGridView.Location = new Point(1127, 500);
            additionalGridView.Margin = new Padding(2, 3, 2, 3);
            additionalGridView.MultiSelect = false;
            additionalGridView.Name = "additionalGridView";
            additionalGridView.ReadOnly = true;
            additionalGridView.RowHeadersVisible = false;
            additionalGridView.RowHeadersWidth = 51;
            additionalGridView.ScrollBars = ScrollBars.Vertical;
            additionalGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            additionalGridView.Size = new Size(372, 80);
            additionalGridView.TabIndex = 27;
            additionalGridView.CellClick += Additional_CellDoubleClick;
            // 
            // additionalGenesL
            // 
            additionalGenesL.AutoSize = true;
            additionalGenesL.BorderRadius = 15;
            additionalGenesL.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            additionalGenesL.ForeColor = Color.White;
            additionalGenesL.InsideBackColor = Color.MediumSeaGreen;
            additionalGenesL.Location = new Point(1127, 456);
            additionalGenesL.Margin = new Padding(2, 0, 2, 0);
            additionalGenesL.Name = "additionalGenesL";
            additionalGenesL.Size = new Size(125, 23);
            additionalGenesL.TabIndex = 26;
            additionalGenesL.Text = " Additional Genes ";
            // 
            // pkineticGridView
            // 
            pkineticGridView.AllowUserToAddRows = false;
            pkineticGridView.AllowUserToDeleteRows = false;
            pkineticGridView.AllowUserToResizeColumns = false;
            pkineticGridView.AllowUserToResizeRows = false;
            pkineticGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            pkineticGridView.BackgroundColor = Color.White;
            pkineticGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Sitka Banner Semibold", 11F, FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = Color.MediumSeaGreen;
            dataGridViewCellStyle8.SelectionForeColor = Color.White;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            pkineticGridView.DefaultCellStyle = dataGridViewCellStyle8;
            pkineticGridView.Location = new Point(1127, 290);
            pkineticGridView.Margin = new Padding(2, 3, 2, 3);
            pkineticGridView.MultiSelect = false;
            pkineticGridView.Name = "pkineticGridView";
            pkineticGridView.ReadOnly = true;
            pkineticGridView.RowHeadersVisible = false;
            pkineticGridView.RowHeadersWidth = 51;
            pkineticGridView.ScrollBars = ScrollBars.Vertical;
            pkineticGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            pkineticGridView.Size = new Size(372, 143);
            pkineticGridView.TabIndex = 25;
            pkineticGridView.CellClick += Pkineticic_CellClick;
            // 
            // pdynamicGridView
            // 
            pdynamicGridView.AllowUserToAddRows = false;
            pdynamicGridView.AllowUserToDeleteRows = false;
            pdynamicGridView.AllowUserToResizeColumns = false;
            pdynamicGridView.AllowUserToResizeRows = false;
            pdynamicGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            pdynamicGridView.BackgroundColor = Color.White;
            pdynamicGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = SystemColors.Window;
            dataGridViewCellStyle9.Font = new Font("Sitka Banner Semibold", 11F, FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = Color.MediumSeaGreen;
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            pdynamicGridView.DefaultCellStyle = dataGridViewCellStyle9;
            pdynamicGridView.Location = new Point(1127, 87);
            pdynamicGridView.Margin = new Padding(2, 3, 2, 3);
            pdynamicGridView.MultiSelect = false;
            pdynamicGridView.Name = "pdynamicGridView";
            pdynamicGridView.ReadOnly = true;
            pdynamicGridView.RowHeadersVisible = false;
            pdynamicGridView.RowHeadersWidth = 51;
            pdynamicGridView.ScrollBars = ScrollBars.Vertical;
            pdynamicGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            pdynamicGridView.Size = new Size(372, 146);
            pdynamicGridView.TabIndex = 24;
            pdynamicGridView.CellClick += Pdynamic_CellClick;
            // 
            // kineticGenesL
            // 
            kineticGenesL.AutoSize = true;
            kineticGenesL.BorderRadius = 15;
            kineticGenesL.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            kineticGenesL.ForeColor = Color.White;
            kineticGenesL.InsideBackColor = Color.MediumSeaGreen;
            kineticGenesL.Location = new Point(1127, 252);
            kineticGenesL.Margin = new Padding(2, 0, 2, 0);
            kineticGenesL.Name = "kineticGenesL";
            kineticGenesL.Size = new Size(165, 23);
            kineticGenesL.TabIndex = 23;
            kineticGenesL.Text = " Pharmacokinetic Genes ";
            // 
            // dynamicGenesL
            // 
            dynamicGenesL.AutoSize = true;
            dynamicGenesL.BorderRadius = 15;
            dynamicGenesL.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dynamicGenesL.ForeColor = Color.White;
            dynamicGenesL.InsideBackColor = Color.MediumSeaGreen;
            dynamicGenesL.Location = new Point(1127, 41);
            dynamicGenesL.Margin = new Padding(2, 0, 2, 0);
            dynamicGenesL.Name = "dynamicGenesL";
            dynamicGenesL.Size = new Size(176, 23);
            dynamicGenesL.TabIndex = 22;
            dynamicGenesL.Text = " Pharmacodynamic Genes ";
            // 
            // medicationsTableL
            // 
            medicationsTableL.AutoSize = true;
            medicationsTableL.BorderRadius = 15;
            medicationsTableL.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            medicationsTableL.ForeColor = Color.White;
            medicationsTableL.InsideBackColor = Color.FromArgb(226, 115, 150);
            medicationsTableL.Location = new Point(535, 42);
            medicationsTableL.Margin = new Padding(2, 0, 2, 0);
            medicationsTableL.Name = "medicationsTableL";
            medicationsTableL.Size = new Size(90, 23);
            medicationsTableL.TabIndex = 21;
            medicationsTableL.Text = " Medications";
            // 
            // medicationsGridView
            // 
            medicationsGridView.AllowUserToAddRows = false;
            medicationsGridView.AllowUserToDeleteRows = false;
            medicationsGridView.AllowUserToResizeColumns = false;
            medicationsGridView.AllowUserToResizeRows = false;
            medicationsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            medicationsGridView.BackgroundColor = Color.White;
            medicationsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = SystemColors.Window;
            dataGridViewCellStyle10.Font = new Font("Sitka Banner Semibold", 11F, FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = Color.FromArgb(226, 115, 150);
            dataGridViewCellStyle10.SelectionForeColor = Color.White;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
            medicationsGridView.DefaultCellStyle = dataGridViewCellStyle10;
            medicationsGridView.Location = new Point(535, 87);
            medicationsGridView.Margin = new Padding(2, 3, 2, 3);
            medicationsGridView.MultiSelect = false;
            medicationsGridView.Name = "medicationsGridView";
            medicationsGridView.ReadOnly = true;
            medicationsGridView.RowHeadersVisible = false;
            medicationsGridView.RowHeadersWidth = 51;
            medicationsGridView.ScrollBars = ScrollBars.Vertical;
            medicationsGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            medicationsGridView.Size = new Size(474, 493);
            medicationsGridView.TabIndex = 19;
            medicationsGridView.DataSourceChanged += medicationsGridView_DataSourceChanged;
            medicationsGridView.CellClick += Medication_CellClick;
            // 
            // patientGridView
            // 
            patientGridView.AllowUserToAddRows = false;
            patientGridView.AllowUserToDeleteRows = false;
            patientGridView.AllowUserToResizeColumns = false;
            patientGridView.AllowUserToResizeRows = false;
            patientGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            patientGridView.BackgroundColor = Color.White;
            patientGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = SystemColors.Window;
            dataGridViewCellStyle11.Font = new Font("Sitka Banner Semibold", 11F, FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = Color.MediumSeaGreen;
            dataGridViewCellStyle11.SelectionForeColor = Color.White;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            patientGridView.DefaultCellStyle = dataGridViewCellStyle11;
            patientGridView.Location = new Point(49, 87);
            patientGridView.Margin = new Padding(2, 3, 2, 3);
            patientGridView.MultiSelect = false;
            patientGridView.Name = "patientGridView";
            patientGridView.ReadOnly = true;
            patientGridView.RowHeadersVisible = false;
            patientGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle12.SelectionBackColor = Color.FromArgb(83, 134, 228);
            dataGridViewCellStyle12.SelectionForeColor = Color.White;
            patientGridView.RowsDefaultCellStyle = dataGridViewCellStyle12;
            patientGridView.ScrollBars = ScrollBars.Vertical;
            patientGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            patientGridView.Size = new Size(360, 493);
            patientGridView.TabIndex = 18;
            patientGridView.DataSourceChanged += patientGridView_DataSourceChanged;
            patientGridView.CellClick += Patient_LoadOthers;
            // 
            // patientSearchBox
            // 
            patientSearchBox.Font = new Font("Segoe UI", 10F);
            patientSearchBox.Location = new Point(123, 40);
            patientSearchBox.Margin = new Padding(2, 3, 2, 3);
            patientSearchBox.Name = "patientSearchBox";
            patientSearchBox.PlaceholderText = "Patient Name/Order Number/Clinician";
            patientSearchBox.Size = new Size(286, 25);
            patientSearchBox.TabIndex = 16;
            patientSearchBox.TextChanged += PatientSearchBox_TextChanged;
            patientSearchBox.KeyPress += PatientSearch_KeyPressEvent;
            // 
            // Line3
            // 
            Line3.BackColor = SystemColors.ActiveCaptionText;
            Line3.Dock = DockStyle.Top;
            Line3.Location = new Point(0, 0);
            Line3.Margin = new Padding(0);
            Line3.Name = "Line3";
            Line3.Size = new Size(1531, 2);
            Line3.TabIndex = 14;
            Line3.Text = "label3";
            // 
            // analyticsPage
            // 
            analyticsPage.BackColor = Color.Ivory;
            analyticsPage.Controls.Add(SevereLabel);
            analyticsPage.Controls.Add(ModerateLabel);
            analyticsPage.Controls.Add(UADLabel);
            analyticsPage.Controls.Add(GalleryLabel);
            analyticsPage.Controls.Add(GeneReactivityPanel);
            analyticsPage.Controls.Add(SmokerPanel);
            analyticsPage.Controls.Add(SmokerStatusLabel);
            analyticsPage.Controls.Add(GeneReactivityLabel);
            analyticsPage.Controls.Add(RightGalleryButton);
            analyticsPage.Controls.Add(LeftGalleryButton);
            analyticsPage.Controls.Add(GeneListBox);
            analyticsPage.Controls.Add(MedListBox);
            analyticsPage.Controls.Add(GraphBox);
            analyticsPage.Controls.Add(GenerateGraphButton);
            analyticsPage.Controls.Add(label1);
            analyticsPage.Location = new Point(4, 35);
            analyticsPage.Name = "analyticsPage";
            analyticsPage.Size = new Size(1531, 702);
            analyticsPage.TabIndex = 2;
            analyticsPage.Text = "Data Analytics";
            // 
            // SevereLabel
            // 
            SevereLabel.AutoSize = true;
            SevereLabel.BorderRadius = 15;
            SevereLabel.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SevereLabel.ForeColor = SystemColors.Window;
            SevereLabel.ImageAlign = ContentAlignment.MiddleLeft;
            SevereLabel.InsideBackColor = Color.Red;
            SevereLabel.Location = new Point(584, 57);
            SevereLabel.Margin = new Padding(2, 0, 2, 0);
            SevereLabel.Name = "SevereLabel";
            SevereLabel.Size = new Size(133, 23);
            SevereLabel.TabIndex = 37;
            SevereLabel.Text = " Severe Interaction ";
            // 
            // ModerateLabel
            // 
            ModerateLabel.AutoSize = true;
            ModerateLabel.BackColor = Color.Transparent;
            ModerateLabel.BorderRadius = 15;
            ModerateLabel.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ModerateLabel.ForeColor = Color.White;
            ModerateLabel.InsideBackColor = Color.Orange;
            ModerateLabel.Location = new Point(565, 101);
            ModerateLabel.Margin = new Padding(2, 0, 2, 0);
            ModerateLabel.Name = "ModerateLabel";
            ModerateLabel.Size = new Size(152, 23);
            ModerateLabel.TabIndex = 36;
            ModerateLabel.Text = " Moderate Interaction ";
            // 
            // UADLabel
            // 
            UADLabel.AutoSize = true;
            UADLabel.BorderRadius = 15;
            UADLabel.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            UADLabel.ForeColor = Color.White;
            UADLabel.InsideBackColor = Color.MediumSeaGreen;
            UADLabel.Location = new Point(602, 149);
            UADLabel.Margin = new Padding(2, 0, 2, 0);
            UADLabel.Name = "UADLabel";
            UADLabel.Size = new Size(115, 23);
            UADLabel.TabIndex = 35;
            UADLabel.Text = " Use as Directed ";
            // 
            // GalleryLabel
            // 
            GalleryLabel.FlatStyle = FlatStyle.Flat;
            GalleryLabel.Font = new Font("Sitka Banner", 12F, FontStyle.Bold);
            GalleryLabel.Location = new Point(482, 620);
            GalleryLabel.Name = "GalleryLabel";
            GalleryLabel.Size = new Size(156, 37);
            GalleryLabel.TabIndex = 34;
            GalleryLabel.Text = "Graph Images: 0 / 0";
            GalleryLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GeneReactivityPanel
            // 
            GeneReactivityPanel.Controls.Add(reactivityFive);
            GeneReactivityPanel.Controls.Add(reactivityFour);
            GeneReactivityPanel.Controls.Add(reactivityThree);
            GeneReactivityPanel.Controls.Add(reactivityOne);
            GeneReactivityPanel.Controls.Add(reactivityTwo);
            GeneReactivityPanel.Font = new Font("Sitka Banner", 12F, FontStyle.Bold);
            GeneReactivityPanel.Location = new Point(341, 297);
            GeneReactivityPanel.Name = "GeneReactivityPanel";
            GeneReactivityPanel.Size = new Size(297, 248);
            GeneReactivityPanel.TabIndex = 33;
            // 
            // reactivityFive
            // 
            reactivityFive.AutoSize = true;
            reactivityFive.Enabled = false;
            reactivityFive.Location = new Point(0, 221);
            reactivityFive.Name = "reactivityFive";
            reactivityFive.Size = new Size(14, 13);
            reactivityFive.TabIndex = 34;
            reactivityFive.TabStop = true;
            reactivityFive.UseVisualStyleBackColor = true;
            // 
            // reactivityFour
            // 
            reactivityFour.AutoSize = true;
            reactivityFour.Enabled = false;
            reactivityFour.Location = new Point(0, 168);
            reactivityFour.Name = "reactivityFour";
            reactivityFour.Size = new Size(14, 13);
            reactivityFour.TabIndex = 33;
            reactivityFour.TabStop = true;
            reactivityFour.UseVisualStyleBackColor = true;
            // 
            // reactivityThree
            // 
            reactivityThree.AutoSize = true;
            reactivityThree.Enabled = false;
            reactivityThree.Location = new Point(0, 112);
            reactivityThree.Name = "reactivityThree";
            reactivityThree.Size = new Size(14, 13);
            reactivityThree.TabIndex = 32;
            reactivityThree.TabStop = true;
            reactivityThree.UseVisualStyleBackColor = true;
            // 
            // reactivityOne
            // 
            reactivityOne.AutoSize = true;
            reactivityOne.Enabled = false;
            reactivityOne.Location = new Point(0, 0);
            reactivityOne.Name = "reactivityOne";
            reactivityOne.Size = new Size(14, 13);
            reactivityOne.TabIndex = 30;
            reactivityOne.TabStop = true;
            reactivityOne.UseVisualStyleBackColor = true;
            // 
            // reactivityTwo
            // 
            reactivityTwo.AutoSize = true;
            reactivityTwo.Enabled = false;
            reactivityTwo.Location = new Point(0, 54);
            reactivityTwo.Name = "reactivityTwo";
            reactivityTwo.Size = new Size(14, 13);
            reactivityTwo.TabIndex = 31;
            reactivityTwo.TabStop = true;
            reactivityTwo.UseVisualStyleBackColor = true;
            // 
            // SmokerPanel
            // 
            SmokerPanel.Controls.Add(NonSmokerButton);
            SmokerPanel.Controls.Add(SmokerButton);
            SmokerPanel.Location = new Point(341, 98);
            SmokerPanel.Name = "SmokerPanel";
            SmokerPanel.Size = new Size(120, 77);
            SmokerPanel.TabIndex = 32;
            // 
            // NonSmokerButton
            // 
            NonSmokerButton.Font = new Font("Sitka Banner", 12F, FontStyle.Bold);
            NonSmokerButton.Location = new Point(0, 0);
            NonSmokerButton.Name = "NonSmokerButton";
            NonSmokerButton.Size = new Size(122, 29);
            NonSmokerButton.TabIndex = 23;
            NonSmokerButton.TabStop = true;
            NonSmokerButton.Text = "Non-Smokers";
            NonSmokerButton.UseVisualStyleBackColor = true;
            NonSmokerButton.Click += SmokerStatusClicked;
            // 
            // SmokerButton
            // 
            SmokerButton.Font = new Font("Sitka Banner", 12F, FontStyle.Bold);
            SmokerButton.Location = new Point(0, 48);
            SmokerButton.Name = "SmokerButton";
            SmokerButton.Size = new Size(122, 29);
            SmokerButton.TabIndex = 24;
            SmokerButton.TabStop = true;
            SmokerButton.Text = "Smokers";
            SmokerButton.UseVisualStyleBackColor = true;
            SmokerButton.Click += SmokerStatusClicked;
            // 
            // SmokerStatusLabel
            // 
            SmokerStatusLabel.AutoSize = true;
            SmokerStatusLabel.BorderRadius = 15;
            SmokerStatusLabel.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SmokerStatusLabel.ForeColor = Color.White;
            SmokerStatusLabel.InsideBackColor = Color.MediumSeaGreen;
            SmokerStatusLabel.Location = new Point(341, 57);
            SmokerStatusLabel.Margin = new Padding(2, 0, 2, 0);
            SmokerStatusLabel.Name = "SmokerStatusLabel";
            SmokerStatusLabel.Size = new Size(111, 23);
            SmokerStatusLabel.TabIndex = 29;
            SmokerStatusLabel.Text = " Smoker Status ";
            // 
            // GeneReactivityLabel
            // 
            GeneReactivityLabel.AutoSize = true;
            GeneReactivityLabel.BorderRadius = 15;
            GeneReactivityLabel.Font = new Font("Sitka Banner Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            GeneReactivityLabel.ForeColor = Color.White;
            GeneReactivityLabel.InsideBackColor = Color.MediumSeaGreen;
            GeneReactivityLabel.Location = new Point(341, 256);
            GeneReactivityLabel.Margin = new Padding(2, 0, 2, 0);
            GeneReactivityLabel.Name = "GeneReactivityLabel";
            GeneReactivityLabel.Size = new Size(115, 23);
            GeneReactivityLabel.TabIndex = 28;
            GeneReactivityLabel.Text = " Gene Reactivity ";
            // 
            // RightGalleryButton
            // 
            RightGalleryButton.FlatStyle = FlatStyle.Flat;
            RightGalleryButton.Font = new Font("Sitka Banner", 16.1999989F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RightGalleryButton.Location = new Point(414, 620);
            RightGalleryButton.Name = "RightGalleryButton";
            RightGalleryButton.Size = new Size(47, 37);
            RightGalleryButton.TabIndex = 26;
            RightGalleryButton.Text = "→";
            RightGalleryButton.UseVisualStyleBackColor = true;
            RightGalleryButton.Click += RightGalleryButton_Click;
            // 
            // LeftGalleryButton
            // 
            LeftGalleryButton.FlatStyle = FlatStyle.Flat;
            LeftGalleryButton.Font = new Font("Sitka Banner", 16F, FontStyle.Bold);
            LeftGalleryButton.Location = new Point(341, 620);
            LeftGalleryButton.Name = "LeftGalleryButton";
            LeftGalleryButton.Size = new Size(47, 37);
            LeftGalleryButton.TabIndex = 25;
            LeftGalleryButton.Text = "←";
            LeftGalleryButton.UseVisualStyleBackColor = true;
            LeftGalleryButton.Click += LeftGalleryButton_Click;
            // 
            // GeneListBox
            // 
            GeneListBox.Font = new Font("Arial", 12F, FontStyle.Bold);
            GeneListBox.FormattingEnabled = true;
            GeneListBox.ItemHeight = 19;
            GeneListBox.Items.AddRange(new object[] { "ADRA2A", "CES1A1", "CYP1A2", "CYP2B6", "CYP2C19", "CYP2C9", "CYP2D6", "CYP3A4", "HLA-A*3101", "HLA-B*1502", "HTR2A", "SLC6A4", "UGT1A4", "UGT2B15", "COMT", "MTHFR" });
            GeneListBox.Location = new Point(87, 256);
            GeneListBox.Name = "GeneListBox";
            GeneListBox.Size = new Size(219, 308);
            GeneListBox.TabIndex = 19;
            GeneListBox.SelectedIndexChanged += GeneListBox_SelectedIndexChanged;
            // 
            // MedListBox
            // 
            MedListBox.Font = new Font("Arial", 12F, FontStyle.Bold);
            MedListBox.FormattingEnabled = true;
            MedListBox.ItemHeight = 19;
            MedListBox.Items.AddRange(new object[] { "Antidepressants", "Anxiolytics and Hypnotics", "Antipsychotics", "Mood Stabilizers", "Stimulants", "Non-Stimulants" });
            MedListBox.Location = new Point(87, 57);
            MedListBox.Name = "MedListBox";
            MedListBox.Size = new Size(219, 118);
            MedListBox.TabIndex = 18;
            MedListBox.SelectedIndexChanged += MedListBox_SelectedIndexChanged;
            // 
            // GraphBox
            // 
            GraphBox.BackColor = Color.MediumSeaGreen;
            GraphBox.InitialImage = null;
            GraphBox.Location = new Point(722, 57);
            GraphBox.Name = "GraphBox";
            GraphBox.Size = new Size(749, 600);
            GraphBox.SizeMode = PictureBoxSizeMode.CenterImage;
            GraphBox.TabIndex = 17;
            GraphBox.TabStop = false;
            // 
            // GenerateGraphButton
            // 
            GenerateGraphButton.FlatStyle = FlatStyle.Flat;
            GenerateGraphButton.Font = new Font("Sitka Banner", 12F, FontStyle.Bold);
            GenerateGraphButton.Location = new Point(87, 620);
            GenerateGraphButton.Name = "GenerateGraphButton";
            GenerateGraphButton.Size = new Size(203, 37);
            GenerateGraphButton.TabIndex = 16;
            GenerateGraphButton.Text = "Generate Graph";
            GenerateGraphButton.UseVisualStyleBackColor = true;
            GenerateGraphButton.Click += GraphButtonClick;
            // 
            // label1
            // 
            label1.BackColor = SystemColors.ActiveCaptionText;
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(1531, 2);
            label1.TabIndex = 15;
            label1.Text = "label3";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // databaseLoader
            // 
            databaseLoader.DoWork += DatabaseLoader_DoWork;
            databaseLoader.RunWorkerCompleted += DatabaseLoader_RunWorkerCompleted;
            // 
            // GraphLoader
            // 
            GraphLoader.DoWork += GraphLoader_DoWork;
            GraphLoader.RunWorkerCompleted += GraphLoader_RunWorkerCompleted;
            // 
            // Extractor
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.MediumSeaGreen;
            ClientSize = new Size(1539, 844);
            Controls.Add(tabControl);
            Controls.Add(Header);
            Margin = new Padding(2, 3, 2, 3);
            Name = "Extractor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PDF Report Processor";
            Header.ResumeLayout(false);
            ((ISupportInitialize)parkviewSmallLogo).EndInit();
            tabControl.ResumeLayout(false);
            pdfProcessorPage.ResumeLayout(false);
            pdfProcessorPage.PerformLayout();
            processorPanel.ResumeLayout(false);
            processorPanel.PerformLayout();
            dataViewerPage.ResumeLayout(false);
            dataViewerPage.PerformLayout();
            ((ISupportInitialize)additionalGridView).EndInit();
            ((ISupportInitialize)pkineticGridView).EndInit();
            ((ISupportInitialize)pdynamicGridView).EndInit();
            ((ISupportInitialize)medicationsGridView).EndInit();
            ((ISupportInitialize)patientGridView).EndInit();
            analyticsPage.ResumeLayout(false);
            analyticsPage.PerformLayout();
            GeneReactivityPanel.ResumeLayout(false);
            GeneReactivityPanel.PerformLayout();
            SmokerPanel.ResumeLayout(false);
            ((ISupportInitialize)GraphBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private void DrawBorderLabel(Label label, Color borderColor, int borderWidth)
        {
            label.Paint += (sender, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    Rectangle rect = new Rectangle(0, 0, label.Width - 1, label.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };
        }

        private Button processButton;
        private Label statusLabel;
        private TextBox folderInput;
        private Label folderLabel;
        private FolderBrowserDialog folderBrowserDialog;
        private ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker processWorker;
        private Button purgeDatabaseButton;
        private Label testingPurposesLabel;
        private Panel Header;
        private PictureBox parkviewSmallLogo;
        private Label Line1;
        private TabControl tabControl;
        private TabPage pdfProcessorPage;
        private TabPage dataViewerPage;
        private Label Line2;
        private Label Line3;
        private DataGridView patientGridView;
        private TextBox patientSearchBox;
        private DataGridView medicationsGridView;
        private DataGridView pdynamicGridView;
        private DataGridView pkineticGridView;
        private DataGridView additionalGridView;
        private Label icd10L;
        private RoundedLabel patientTableL;
        private RoundedLabel medicationsTableL;
        private RoundedLabel dynamicGenesL;
        private RoundedLabel kineticGenesL;
        private RoundedLabel additionalGenesL;
        private TextBox medicationSearch;
        private ContextMenuStrip contextMenuStrip1;
        private Label instructionLabel;
        private Panel processorPanel;
        private Label dragNDropL;
        private Button dragNDropButton;
        private BackgroundWorker databaseLoader;
        private Label PatientCountTitle;
        private Label MedicationCountTitle;
        private Label PatientCountValue;
        private Label MedicationCountValue;
        private TabPage analyticsPage;
        private Label label1;
        private Button GenerateGraphButton;
        private PictureBox GraphBox;
        private ListBox MedListBox;
        private ListBox GeneListBox;
        private RadioButton SmokerButton;
        private RadioButton NonSmokerButton;
        private Button LeftGalleryButton;
        private Button RightGalleryButton;
        private RoundedLabel SmokerStatusLabel;
        private RoundedLabel GeneReactivityLabel;
        private RadioButton reactivityTwo;
        private RadioButton reactivityOne;
        private Panel SmokerPanel;
        private Panel GeneReactivityPanel;
        private RadioButton reactivityFive;
        private RadioButton reactivityFour;
        private RadioButton reactivityThree;
        private Label GalleryLabel;
        private RoundedLabel SevereLabel;
        private RoundedLabel ModerateLabel;
        private RoundedLabel UADLabel;
        private BackgroundWorker GraphLoader;
        //private Button button_clear;
    }
}
