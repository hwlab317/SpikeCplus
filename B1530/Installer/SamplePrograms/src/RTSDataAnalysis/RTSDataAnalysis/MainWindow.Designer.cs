namespace RTSDataAnalysis
{
    partial class MainWindow
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.m_imgFolders = new System.Windows.Forms.ImageList(this.components);
            this.m_bgwAnalysis = new System.ComponentModel.BackgroundWorker();
            this.ssHelp = new System.Windows.Forms.StatusStrip();
            this.tsslHelp = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnGraphBase = new System.Windows.Forms.Panel();
            this.pnDistribution = new System.Windows.Forms.Panel();
            this.pbDistribution = new System.Windows.Forms.PictureBox();
            this.pnHistogram = new System.Windows.Forms.Panel();
            this.pbHistogram = new System.Windows.Forms.PictureBox();
            this.pnPSD = new System.Windows.Forms.Panel();
            this.pbPSD = new System.Windows.Forms.PictureBox();
            this.pnWaveForm = new System.Windows.Forms.Panel();
            this.pbWaveForm = new System.Windows.Forms.PictureBox();
            this.pnText = new System.Windows.Forms.Panel();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.btnAbort = new System.Windows.Forms.Button();
            this.cbReverseSign = new System.Windows.Forms.CheckBox();
            this.cbShowDigitized = new System.Windows.Forms.CheckBox();
            this.cbDebugLog = new System.Windows.Forms.CheckBox();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.tbThresholdLow = new System.Windows.Forms.TextBox();
            this.tbThresholdHigh = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbRatio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbToff = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFrequency = new System.Windows.Forms.TextBox();
            this.tbNumSample = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.spMain = new System.Windows.Forms.Splitter();
            this.pnFileSelection = new System.Windows.Forms.Panel();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.spFileSelection = new System.Windows.Forms.Splitter();
            this.tvFolders = new System.Windows.Forms.TreeView();
            this.tbFullPath = new System.Windows.Forms.TextBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.ssHelp.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.pnGraphBase.SuspendLayout();
            this.pnDistribution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDistribution)).BeginInit();
            this.pnHistogram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistogram)).BeginInit();
            this.pnPSD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPSD)).BeginInit();
            this.pnWaveForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveForm)).BeginInit();
            this.pnText.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.pnFileSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_imgFolders
            // 
            this.m_imgFolders.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgFolders.ImageStream")));
            this.m_imgFolders.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgFolders.Images.SetKeyName(0, "QUESTION.ICO");
            this.m_imgFolders.Images.SetKeyName(1, "QUESTION.ICO");
            this.m_imgFolders.Images.SetKeyName(2, "35FLOPPY.ICO");
            this.m_imgFolders.Images.SetKeyName(3, "DRIVE.ICO");
            this.m_imgFolders.Images.SetKeyName(4, "DRIVENET.ICO");
            this.m_imgFolders.Images.SetKeyName(5, "CDDRIVE.ICO");
            this.m_imgFolders.Images.SetKeyName(6, "DRIVE.ICO");
            this.m_imgFolders.Images.SetKeyName(7, "CLSDFOLD.ICO");
            this.m_imgFolders.Images.SetKeyName(8, "OPENFOLD.ICO");
            this.m_imgFolders.Images.SetKeyName(9, "MYCOMP.ICO");
            this.m_imgFolders.Images.SetKeyName(10, "document.ico");
            // 
            // m_bgwAnalysis
            // 
            this.m_bgwAnalysis.WorkerSupportsCancellation = true;
            this.m_bgwAnalysis.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_bgwAnalysis_DoWork);
            // 
            // ssHelp
            // 
            this.ssHelp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslHelp});
            this.ssHelp.Location = new System.Drawing.Point(0, 464);
            this.ssHelp.Name = "ssHelp";
            this.ssHelp.Size = new System.Drawing.Size(992, 22);
            this.ssHelp.TabIndex = 5;
            this.ssHelp.Text = "statusStrip1";
            // 
            // tsslHelp
            // 
            this.tsslHelp.Name = "tsslHelp";
            this.tsslHelp.Size = new System.Drawing.Size(0, 17);
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.pnGraphBase);
            this.pnMain.Controls.Add(this.pnText);
            this.pnMain.Controls.Add(this.spMain);
            this.pnMain.Controls.Add(this.pnFileSelection);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(992, 464);
            this.pnMain.TabIndex = 6;
            // 
            // pnGraphBase
            // 
            this.pnGraphBase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnGraphBase.Controls.Add(this.pnDistribution);
            this.pnGraphBase.Controls.Add(this.pnHistogram);
            this.pnGraphBase.Controls.Add(this.pnPSD);
            this.pnGraphBase.Controls.Add(this.pnWaveForm);
            this.pnGraphBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnGraphBase.Location = new System.Drawing.Point(363, 170);
            this.pnGraphBase.Name = "pnGraphBase";
            this.pnGraphBase.Size = new System.Drawing.Size(629, 294);
            this.pnGraphBase.TabIndex = 3;
            // 
            // pnDistribution
            // 
            this.pnDistribution.Controls.Add(this.pbDistribution);
            this.pnDistribution.Location = new System.Drawing.Point(170, 102);
            this.pnDistribution.Name = "pnDistribution";
            this.pnDistribution.Size = new System.Drawing.Size(138, 76);
            this.pnDistribution.TabIndex = 3;
            // 
            // pbDistribution
            // 
            this.pbDistribution.Location = new System.Drawing.Point(20, 14);
            this.pbDistribution.Name = "pbDistribution";
            this.pbDistribution.Size = new System.Drawing.Size(100, 50);
            this.pbDistribution.TabIndex = 3;
            this.pbDistribution.TabStop = false;
            this.pbDistribution.MouseLeave += new System.EventHandler(this.pbDistribution_MouseLeave);
            this.pbDistribution.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbDistribution_MouseDoubleClick);
            this.pbDistribution.MouseEnter += new System.EventHandler(this.pbDistribution_MouseEnter);
            // 
            // pnHistogram
            // 
            this.pnHistogram.Controls.Add(this.pbHistogram);
            this.pnHistogram.Location = new System.Drawing.Point(26, 102);
            this.pnHistogram.Name = "pnHistogram";
            this.pnHistogram.Size = new System.Drawing.Size(138, 76);
            this.pnHistogram.TabIndex = 2;
            // 
            // pbHistogram
            // 
            this.pbHistogram.Location = new System.Drawing.Point(22, 14);
            this.pbHistogram.Name = "pbHistogram";
            this.pbHistogram.Size = new System.Drawing.Size(100, 50);
            this.pbHistogram.TabIndex = 2;
            this.pbHistogram.TabStop = false;
            this.pbHistogram.MouseLeave += new System.EventHandler(this.pbHistogram_MouseLeave);
            this.pbHistogram.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbHistogram_MouseDoubleClick);
            this.pbHistogram.MouseEnter += new System.EventHandler(this.pbHistogram_MouseEnter);
            // 
            // pnPSD
            // 
            this.pnPSD.Controls.Add(this.pbPSD);
            this.pnPSD.Location = new System.Drawing.Point(170, 20);
            this.pnPSD.Name = "pnPSD";
            this.pnPSD.Size = new System.Drawing.Size(138, 76);
            this.pnPSD.TabIndex = 1;
            // 
            // pbPSD
            // 
            this.pbPSD.Location = new System.Drawing.Point(20, 14);
            this.pbPSD.Name = "pbPSD";
            this.pbPSD.Size = new System.Drawing.Size(100, 50);
            this.pbPSD.TabIndex = 1;
            this.pbPSD.TabStop = false;
            this.pbPSD.MouseLeave += new System.EventHandler(this.pbPSD_MouseLeave);
            this.pbPSD.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbPSD_MouseDoubleClick);
            this.pbPSD.MouseEnter += new System.EventHandler(this.pbPSD_MouseEnter);
            // 
            // pnWaveForm
            // 
            this.pnWaveForm.Controls.Add(this.pbWaveForm);
            this.pnWaveForm.Location = new System.Drawing.Point(26, 20);
            this.pnWaveForm.Name = "pnWaveForm";
            this.pnWaveForm.Size = new System.Drawing.Size(138, 76);
            this.pnWaveForm.TabIndex = 0;
            // 
            // pbWaveForm
            // 
            this.pbWaveForm.Location = new System.Drawing.Point(22, 14);
            this.pbWaveForm.Name = "pbWaveForm";
            this.pbWaveForm.Size = new System.Drawing.Size(100, 50);
            this.pbWaveForm.TabIndex = 0;
            this.pbWaveForm.TabStop = false;
            this.pbWaveForm.MouseLeave += new System.EventHandler(this.pbWaveForm_MouseLeave);
            this.pbWaveForm.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbWaveForm_MouseDoubleClick);
            this.pbWaveForm.MouseEnter += new System.EventHandler(this.pbWaveForm_MouseEnter);
            // 
            // pnText
            // 
            this.pnText.Controls.Add(this.gbOptions);
            this.pnText.Controls.Add(this.tbFileName);
            this.pnText.Controls.Add(this.gbResult);
            this.pnText.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnText.Location = new System.Drawing.Point(363, 0);
            this.pnText.Name = "pnText";
            this.pnText.Size = new System.Drawing.Size(629, 170);
            this.pnText.TabIndex = 2;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.btnAbort);
            this.gbOptions.Controls.Add(this.cbReverseSign);
            this.gbOptions.Controls.Add(this.cbShowDigitized);
            this.gbOptions.Controls.Add(this.cbDebugLog);
            this.gbOptions.Location = new System.Drawing.Point(13, 28);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(151, 137);
            this.gbOptions.TabIndex = 15;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // btnAbort
            // 
            this.btnAbort.Enabled = false;
            this.btnAbort.Location = new System.Drawing.Point(16, 21);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(121, 23);
            this.btnAbort.TabIndex = 14;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // cbReverseSign
            // 
            this.cbReverseSign.AutoSize = true;
            this.cbReverseSign.Location = new System.Drawing.Point(16, 54);
            this.cbReverseSign.Name = "cbReverseSign";
            this.cbReverseSign.Size = new System.Drawing.Size(90, 17);
            this.cbReverseSign.TabIndex = 15;
            this.cbReverseSign.Text = "Reverse Sign";
            this.cbReverseSign.UseVisualStyleBackColor = true;
            this.cbReverseSign.CheckedChanged += new System.EventHandler(this.cbReverseSign_CheckedChanged);
            // 
            // cbShowDigitized
            // 
            this.cbShowDigitized.AutoSize = true;
            this.cbShowDigitized.Checked = true;
            this.cbShowDigitized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowDigitized.Location = new System.Drawing.Point(16, 82);
            this.cbShowDigitized.Name = "cbShowDigitized";
            this.cbShowDigitized.Size = new System.Drawing.Size(122, 17);
            this.cbShowDigitized.TabIndex = 16;
            this.cbShowDigitized.Text = "Show Digitized Data";
            this.cbShowDigitized.UseVisualStyleBackColor = true;
            this.cbShowDigitized.CheckedChanged += new System.EventHandler(this.cbShowDigitized_CheckedChanged);
            // 
            // cbDebugLog
            // 
            this.cbDebugLog.AutoSize = true;
            this.cbDebugLog.Checked = true;
            this.cbDebugLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDebugLog.Location = new System.Drawing.Point(16, 111);
            this.cbDebugLog.Name = "cbDebugLog";
            this.cbDebugLog.Size = new System.Drawing.Size(79, 17);
            this.cbDebugLog.TabIndex = 17;
            this.cbDebugLog.Text = "Output Log";
            this.cbDebugLog.UseVisualStyleBackColor = true;
            this.cbDebugLog.CheckedChanged += new System.EventHandler(this.cbDebugLog_CheckedChanged);
            // 
            // tbFileName
            // 
            this.tbFileName.BackColor = System.Drawing.SystemColors.Control;
            this.tbFileName.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbFileName.Location = new System.Drawing.Point(0, 0);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(629, 20);
            this.tbFileName.TabIndex = 3;
            this.tbFileName.TabStop = false;
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.tbThresholdLow);
            this.gbResult.Controls.Add(this.tbThresholdHigh);
            this.gbResult.Controls.Add(this.label8);
            this.gbResult.Controls.Add(this.label7);
            this.gbResult.Controls.Add(this.label6);
            this.gbResult.Controls.Add(this.tbRatio);
            this.gbResult.Controls.Add(this.label5);
            this.gbResult.Controls.Add(this.tbToff);
            this.gbResult.Controls.Add(this.label4);
            this.gbResult.Controls.Add(this.tbTon);
            this.gbResult.Controls.Add(this.label3);
            this.gbResult.Controls.Add(this.tbFrequency);
            this.gbResult.Controls.Add(this.tbNumSample);
            this.gbResult.Controls.Add(this.label2);
            this.gbResult.Controls.Add(this.label1);
            this.gbResult.Location = new System.Drawing.Point(175, 28);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(442, 137);
            this.gbResult.TabIndex = 16;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // tbThresholdLow
            // 
            this.tbThresholdLow.BackColor = System.Drawing.SystemColors.Window;
            this.tbThresholdLow.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbThresholdLow.Location = new System.Drawing.Point(148, 108);
            this.tbThresholdLow.Name = "tbThresholdLow";
            this.tbThresholdLow.ReadOnly = true;
            this.tbThresholdLow.Size = new System.Drawing.Size(100, 21);
            this.tbThresholdLow.TabIndex = 21;
            this.tbThresholdLow.TabStop = false;
            this.tbThresholdLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbThresholdHigh
            // 
            this.tbThresholdHigh.BackColor = System.Drawing.SystemColors.Window;
            this.tbThresholdHigh.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbThresholdHigh.Location = new System.Drawing.Point(148, 82);
            this.tbThresholdHigh.Name = "tbThresholdHigh";
            this.tbThresholdHigh.ReadOnly = true;
            this.tbThresholdHigh.Size = new System.Drawing.Size(100, 21);
            this.tbThresholdHigh.TabIndex = 20;
            this.tbThresholdHigh.TabStop = false;
            this.tbThresholdHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(108, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Low";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(108, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "High";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 47);
            this.label6.TabIndex = 20;
            this.label6.Text = "Thresholds";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbRatio
            // 
            this.tbRatio.BackColor = System.Drawing.SystemColors.Window;
            this.tbRatio.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRatio.Location = new System.Drawing.Point(327, 82);
            this.tbRatio.Name = "tbRatio";
            this.tbRatio.ReadOnly = true;
            this.tbRatio.Size = new System.Drawing.Size(100, 21);
            this.tbRatio.TabIndex = 24;
            this.tbRatio.TabStop = false;
            this.tbRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(265, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Ton / Toff";
            // 
            // tbToff
            // 
            this.tbToff.BackColor = System.Drawing.SystemColors.Window;
            this.tbToff.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbToff.Location = new System.Drawing.Point(327, 54);
            this.tbToff.Name = "tbToff";
            this.tbToff.ReadOnly = true;
            this.tbToff.Size = new System.Drawing.Size(100, 21);
            this.tbToff.TabIndex = 23;
            this.tbToff.TabStop = false;
            this.tbToff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Toff";
            // 
            // tbTon
            // 
            this.tbTon.BackColor = System.Drawing.SystemColors.Window;
            this.tbTon.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTon.Location = new System.Drawing.Point(327, 26);
            this.tbTon.Name = "tbTon";
            this.tbTon.ReadOnly = true;
            this.tbTon.Size = new System.Drawing.Size(100, 21);
            this.tbTon.TabIndex = 22;
            this.tbTon.TabStop = false;
            this.tbTon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(295, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ton";
            // 
            // tbFrequency
            // 
            this.tbFrequency.BackColor = System.Drawing.SystemColors.Window;
            this.tbFrequency.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFrequency.Location = new System.Drawing.Point(148, 54);
            this.tbFrequency.Name = "tbFrequency";
            this.tbFrequency.ReadOnly = true;
            this.tbFrequency.Size = new System.Drawing.Size(100, 21);
            this.tbFrequency.TabIndex = 19;
            this.tbFrequency.TabStop = false;
            this.tbFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbNumSample
            // 
            this.tbNumSample.BackColor = System.Drawing.SystemColors.Window;
            this.tbNumSample.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNumSample.Location = new System.Drawing.Point(148, 26);
            this.tbNumSample.Name = "tbNumSample";
            this.tbNumSample.ReadOnly = true;
            this.tbNumSample.Size = new System.Drawing.Size(100, 21);
            this.tbNumSample.TabIndex = 18;
            this.tbNumSample.TabStop = false;
            this.tbNumSample.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Sampling Frequency (Hz)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Number of Samples";
            // 
            // spMain
            // 
            this.spMain.Location = new System.Drawing.Point(360, 0);
            this.spMain.Name = "spMain";
            this.spMain.Size = new System.Drawing.Size(3, 464);
            this.spMain.TabIndex = 4;
            this.spMain.TabStop = false;
            this.spMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spMain_SplitterMoved);
            // 
            // pnFileSelection
            // 
            this.pnFileSelection.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnFileSelection.BackColor = System.Drawing.SystemColors.Control;
            this.pnFileSelection.Controls.Add(this.btnUp);
            this.pnFileSelection.Controls.Add(this.lvFiles);
            this.pnFileSelection.Controls.Add(this.spFileSelection);
            this.pnFileSelection.Controls.Add(this.tvFolders);
            this.pnFileSelection.Controls.Add(this.tbFullPath);
            this.pnFileSelection.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnFileSelection.Location = new System.Drawing.Point(0, 0);
            this.pnFileSelection.Margin = new System.Windows.Forms.Padding(0);
            this.pnFileSelection.Name = "pnFileSelection";
            this.pnFileSelection.Size = new System.Drawing.Size(360, 464);
            this.pnFileSelection.TabIndex = 0;
            // 
            // lvFiles
            // 
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(174, 20);
            this.lvFiles.MultiSelect = false;
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(186, 444);
            this.lvFiles.SmallImageList = this.m_imgFolders;
            this.lvFiles.TabIndex = 2;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFiles_MouseDoubleClick);
            this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);
            this.lvFiles.MouseEnter += new System.EventHandler(this.lvFiles_MouseEnter);
            this.lvFiles.MouseLeave += new System.EventHandler(this.lvFiles_MouseLeave);
            // 
            // spFileSelection
            // 
            this.spFileSelection.Location = new System.Drawing.Point(171, 20);
            this.spFileSelection.Name = "spFileSelection";
            this.spFileSelection.Size = new System.Drawing.Size(3, 444);
            this.spFileSelection.TabIndex = 2;
            this.spFileSelection.TabStop = false;
            // 
            // tvFolders
            // 
            this.tvFolders.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvFolders.HideSelection = false;
            this.tvFolders.ImageIndex = 0;
            this.tvFolders.ImageList = this.m_imgFolders;
            this.tvFolders.Location = new System.Drawing.Point(0, 20);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageIndex = 0;
            this.tvFolders.Size = new System.Drawing.Size(171, 444);
            this.tvFolders.TabIndex = 1;
            this.tvFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFolders_AfterSelect);
            // 
            // tbFullPath
            // 
            this.tbFullPath.BackColor = System.Drawing.SystemColors.Control;
            this.tbFullPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbFullPath.Location = new System.Drawing.Point(0, 0);
            this.tbFullPath.Name = "tbFullPath";
            this.tbFullPath.ReadOnly = true;
            this.tbFullPath.Size = new System.Drawing.Size(360, 20);
            this.tbFullPath.TabIndex = 0;
            this.tbFullPath.TabStop = false;
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(327, 1);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(32, 18);
            this.btnUp.TabIndex = 15;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 486);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.ssHelp);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainWindow";
            this.Text = "RTS Data Analysis";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.ResizeEnd += new System.EventHandler(this.MainWindow_ResizeEnd);
            this.ssHelp.ResumeLayout(false);
            this.ssHelp.PerformLayout();
            this.pnMain.ResumeLayout(false);
            this.pnGraphBase.ResumeLayout(false);
            this.pnDistribution.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDistribution)).EndInit();
            this.pnHistogram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHistogram)).EndInit();
            this.pnPSD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPSD)).EndInit();
            this.pnWaveForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveForm)).EndInit();
            this.pnText.ResumeLayout(false);
            this.pnText.PerformLayout();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.pnFileSelection.ResumeLayout(false);
            this.pnFileSelection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList m_imgFolders;
        private System.ComponentModel.BackgroundWorker m_bgwAnalysis;
        private System.Windows.Forms.StatusStrip ssHelp;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnGraphBase;
        private System.Windows.Forms.Panel pnDistribution;
        private System.Windows.Forms.PictureBox pbDistribution;
        private System.Windows.Forms.Panel pnHistogram;
        private System.Windows.Forms.PictureBox pbHistogram;
        private System.Windows.Forms.Panel pnPSD;
        private System.Windows.Forms.PictureBox pbPSD;
        private System.Windows.Forms.Panel pnWaveForm;
        private System.Windows.Forms.PictureBox pbWaveForm;
        private System.Windows.Forms.Panel pnText;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.CheckBox cbReverseSign;
        private System.Windows.Forms.CheckBox cbShowDigitized;
        private System.Windows.Forms.CheckBox cbDebugLog;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.TextBox tbRatio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbToff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFrequency;
        private System.Windows.Forms.TextBox tbNumSample;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnFileSelection;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.Splitter spFileSelection;
        private System.Windows.Forms.TreeView tvFolders;
        private System.Windows.Forms.TextBox tbFullPath;
        private System.Windows.Forms.Splitter spMain;
        private System.Windows.Forms.ToolStripStatusLabel tsslHelp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbThresholdLow;
        private System.Windows.Forms.TextBox tbThresholdHigh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnUp;
    }
}

