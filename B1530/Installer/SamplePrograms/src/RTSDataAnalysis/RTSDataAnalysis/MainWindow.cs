//
//   (c) Copyright Agilent Technologies 2008
//              All rights reserved.
//
//  Customer shall have the personal, non-
// transferable rights to use, copy or modify
// this SAMPLE PROGRAM for Customer's internal
// operations. Customer shall use the SAMPLE
// PROGRAM solely and exclusively for its own
// purpose and shall not license, lease, market
// or distribute the SAMPLE PROGRAM or modification
// or any part thereof.
//
//  Agilent shall not be liable for the quality,
// performance or behavior of the SAMPLE PROGRAM.
// Agilent especially disclaims that the operation
// of the SAMPLE PROGRAM shall be uninterrupted or
// error free. This SAMPLE PROGRAM is provided
// AS IS.
//
//  AGILENT DISCLAIMS THE IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR
// PURPOSE.
//
//  Agilent shall not be liable for any infringement
// of any patent, trademark, copyright or other
// proprietary rights by the SAMPLE PROGRAM or
// its use. Agilent does not warrant that the SAMPLE
// PROGRAM is free from infringements or such
// rights of third parties. However, Agilent will not
// knowingly infringe or deliver a software that
// infringes the patent, trademark, copyright or
// other proprietary right of a third party.
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Globalization;
using System.Threading;
using Calculate;
using FileReader;
using Draw;

namespace RTSDataAnalysis
{
    public partial class MainWindow : Form
    {
        private FormWindowState _fws;
        private DrawGraph _dgWaveform;
        private DrawGraph _dgPSD;
        private DrawGraph _dgHistogram;
        private DrawGraph _dgDistribution;

        private const string sTitleWaveform = "Time Domain";
        private const string sLabelWaveformX = "Time [s]";
        private const string sLabelWaveformY = "Id [A]";

        private const string sTitlePSD = "Frequency Domain";
        private const string sLabelPSDX = "Freq [Hz]";
        private const string sLabelPSDY = "PSD [A\u00b2/Hz]";

        private const string sTitleHistogram = "Histogram";
        private const string sLabelHistogramX = "Id [A]";
        private const string sLabelHistogramY = "";

        private const string sTitleDistribution = "Distribution";
        private const string sLabelDistributionX = "Time [s]";
        private const string sLabelDistributionY = "";

        private DialogGraph _dlgWave;
        private DialogGraph _dlgPSD;
        private DialogGraph _dlgHistogram;
        private DialogGraph _dlgDistribution;

        private FileSystemWatcher _fileSystemWatcher;
        private TreeNode _tnNodeCurrent = null;

        private void fileSystemEventHandler(Object source, FileSystemEventArgs e)
        {
            //
            // Show sub-folders and folder files
            //
            showFolders(_tnNodeCurrent, _tnNodeCurrent.Nodes);
        }

        private void renamedEventHandler(Object source, RenamedEventArgs e)
        {
            //
            // Show sub-folders and folder files
            //
            showFolders(_tnNodeCurrent, _tnNodeCurrent.Nodes);
        }

        //
        // To overlap digitizing result on the waveform, set "true".
        // (This is not a constant to avoid warning message.)
        //
        private bool _bOverlapDigitizeResult = false;

        //
        // Constructor
        //
        public MainWindow()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // Show TreeView with drive list
            //
            ShowDriveList();

            //
            // Initialize window state
            //
            _fws = WindowState;

            //
            // Initialize graph objects
            //
            _dgWaveform = new DrawGraph(pnWaveForm, pbWaveForm);
            _dgWaveform.SetTitle(sTitleWaveform);
            _dgWaveform.SetAxisLabel(sLabelWaveformX, sLabelWaveformY);
            _dgWaveform.SetScaleType(ScaleType.LINEAR, ScaleType.LINEAR);

            _dgPSD = new DrawGraph(pnPSD, pbPSD);
            _dgPSD.SetTitle(sTitlePSD);
            _dgPSD.SetAxisLabel(sLabelPSDX, sLabelPSDY);
            _dgPSD.SetScaleType(ScaleType.LOG, ScaleType.LOG);

            _dgHistogram = new DrawGraph(pnHistogram, pbHistogram);
            _dgHistogram.SetTitle(sTitleHistogram);
            _dgHistogram.SetAxisLabel(sLabelHistogramX, sLabelHistogramY);
            _dgHistogram.SetScaleType(ScaleType.LINEAR, ScaleType.LINEAR);

            _dgDistribution = new DrawGraph(pnDistribution, pbDistribution);
            _dgDistribution.SetTitle(sTitleDistribution);
            _dgDistribution.SetAxisLabel(sLabelDistributionX, sLabelDistributionY);
            _dgDistribution.SetScaleType(ScaleType.LINEAR, ScaleType.LOG);

            //
            // Initialize file system watcher
            //
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.NotifyFilter = (NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName);
            _fileSystemWatcher.Filter = "";
            _fileSystemWatcher.SynchronizingObject = this;
            //_fileSystemWatcher.Changed += new FileSystemEventHandler(fileSystemEventHandler);
            _fileSystemWatcher.Created += new FileSystemEventHandler(fileSystemEventHandler);
            _fileSystemWatcher.Deleted += new FileSystemEventHandler(fileSystemEventHandler);
            _fileSystemWatcher.Renamed += new RenamedEventHandler(renamedEventHandler);
            _fileSystemWatcher.EnableRaisingEvents = false;
        }

        //
        // Show the TreeView with the drive list
        //
        private void ShowDriveList()
        {
            TreeNode tnNodeTreeNode;
            TreeNode tnRootNode;
            int iImageIndex;
            int iSelectIndex;

            //
            // Show wait cursor on the display
            //
            this.Cursor = Cursors.WaitCursor;

            //
            // Clear TreeView
            //
            tvFolders.Nodes.Clear();

            //
            // Add "My Computer".  (Image index is 9 in the image list)
            //
            tnNodeTreeNode = tnRootNode = new TreeNode("My Computer", 9, 9);
            tvFolders.Nodes.Add(tnNodeTreeNode);

            //
            // Set node collection
            //
            TreeNodeCollection nodeCollection = tnNodeTreeNode.Nodes;

            //
            // Get drive list
            //
            ManagementObjectCollection queryCollection = getDrives();
            foreach(ManagementObject mo in queryCollection)
            {
                iImageIndex = iSelectIndex = int.Parse(mo["DriveType"].ToString());

                //
                // Create and add new drive node
                //
                tnNodeTreeNode = new TreeNode(mo["Name"].ToString(), iImageIndex, iSelectIndex);
                nodeCollection.Add(tnNodeTreeNode);
            }

            //
            // Initialize files' listview
            //
            initListView();

            //
            // Restore cursor to the default
            //
            this.Cursor = Cursors.Default;
        }

        protected void initListView()
        {
            //
            // Initialize listview control
            //

            //
            // Clear the list
            //
            lvFiles.Clear();

            //
            // Create column headers for listview
            //
            lvFiles.Columns.Add("Name",150,System.Windows.Forms.HorizontalAlignment.Left);
            lvFiles.Columns.Add("Size",75, System.Windows.Forms.HorizontalAlignment.Right);
            lvFiles.Columns.Add("Last Updated", 140, System.Windows.Forms.HorizontalAlignment.Left);
        }

        protected ManagementObjectCollection getDrives()
        {
            //
            // Get drive collection
            //
            ManagementObjectSearcher mosQuery = new ManagementObjectSearcher("SELECT * From Win32_LogicalDisk ");
            ManagementObjectCollection mocQueryCollection = mosQuery.Get();

            return mocQueryCollection;
        }

        protected void showFolders(TreeNode tnNodeCurrent, TreeNodeCollection tnNodeCurrentCollection)
        {
            TreeNode tnNodeDir;
            int iClosedIndex = 7;    // Closed Folder (7)
            int iOpenedIndex = 8;    // Opened Folder (8)

            if (tnNodeCurrent.SelectedImageIndex != 0) 
            {
                //
                // Show treeview with folders
                //
                try
                {
                    //
                    // Check path
                    //
                    if (Directory.Exists(getFullPath(tnNodeCurrent.FullPath)) == false)
                    {
                        MessageBox.Show("Error: Cannot access.");
                    }
                    else
                    {
                        //
                        // Update file system watcher
                        //
                        _tnNodeCurrent = tnNodeCurrent;
                        _fileSystemWatcher.Path = tnNodeCurrent.FullPath.Substring(12);
                        _fileSystemWatcher.EnableRaisingEvents = true;

                        //
                        // Show files
                        //
                        showFiles(tnNodeCurrent, "");

                        string[] saFolders = Directory.GetDirectories(getFullPath(tnNodeCurrent.FullPath));
                        string sFullPath = "";
                        string sPathName = "";

                        //
                        // Walk through all folders
                        //
                        foreach (string sFolder in saFolders)
                        {
                            sFullPath = sFolder;
                            sPathName = getPathName(sFullPath);
                            
                            //
                            // Create node for folders
                            //
                            tnNodeDir = new TreeNode(sPathName.ToString(), iClosedIndex, iOpenedIndex);
                            tnNodeCurrentCollection.Add(tnNodeDir);
                        }
                    }
                }
                catch (IOException /* e */)
                {
                    MessageBox.Show("Error: I/O error");
                }
                catch (UnauthorizedAccessException /* e */)
                {
                    MessageBox.Show("Error: Unauthorized Access");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e);
                }
            }
        }

        protected string getPathName(string sPath)
        {
            //
            // Get folder name
            //
            string[] sSplit = sPath.Split('\\');
            int iMaxIndex = sSplit.Length;
            return sSplit[iMaxIndex - 1];
        }

        protected void showFiles(TreeNode tnNodeCurrent, string sSelectedFileName)
        {
            //
            // Show listview with files
            //
            string[] saData =  new string[4];
            
            //
            // Clear
            //
            initListView();

            if (tnNodeCurrent.SelectedImageIndex != 9) 
            {
                //
                // Check path
                //
                if (Directory.Exists((string) getFullPath(tnNodeCurrent.FullPath)) == false)
                {
                    MessageBox.Show("Folder(or path)" + tnNodeCurrent.ToString() + " was not found.");
                }
                else
                {
                    try
                    {
                        DateTime dtModifyDate;

                        //
                        // Walk through all folders
                        //
                        string[] saFolders = Directory.GetDirectories(getFullPath(tnNodeCurrent.FullPath));
                        foreach(string sFolderName in saFolders)
                        {
                            FileInfo objFileSize = new FileInfo(sFolderName);
                            dtModifyDate = objFileSize.LastWriteTime;

                            //
                            // Create listview
                            //
                            saData[0] = getPathName(sFolderName);
                            saData[1] = "";

                            //
                            // Check whether it is in local current daylight saving time or not
                            //
                            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(dtModifyDate) == false)
                            {
                                //
                                // Not in daylight saving time
                                //
                                saData[2] = formatDate(dtModifyDate);
                            }
                            else                            
                            {
                                //
                                // In daylight saving time
                                //
                                saData[2] = formatDate(dtModifyDate.AddHours(1));
                            }

                            //
                            // Create actual list item
                            //
                            ListViewItem lvItem = new ListViewItem(saData, 7);
                            lvFiles.Items.Add(lvItem);
                        }

                        string[] saFiles = Directory.GetFiles(getFullPath(tnNodeCurrent.FullPath));
                        Int64 lFileSize = 0;

                        //
                        // Walk through all files
                        //
                        foreach (string sFileName in saFiles)
                        {
                            FileInfo objFileSize = new FileInfo(sFileName);
                            lFileSize = objFileSize.Length;
                            dtModifyDate = objFileSize.LastWriteTime;

                            //
                            // Create listview
                            //
                            saData[0] = getPathName(sFileName);
                            saData[1] = formatSize(lFileSize);
                            
                            //
                            // Check whether it is in local current daylight saving time or not
                            //
                            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(dtModifyDate) == false)
                            {
                                //
                                // Not in daylight saving time
                                //
                                saData[2] = formatDate(dtModifyDate);
                            }
                            else                            
                            {
                                //
                                // In daylight saving time
                                //
                                saData[2] = formatDate(dtModifyDate.AddHours(1));
                            }

                            //
                            // Create actual list item
                            //
                            ListViewItem lvItem = new ListViewItem(saData, 10);
                            lvFiles.Items.Add(lvItem);

                            //
                            // Select specified filename
                            //
                            if (saData[0] == sSelectedFileName)
                            {
                                lvFiles.Items[lvFiles.Items.Count - 1].Selected = true;
                                lvFiles.Items[lvFiles.Items.Count - 1].Focused = true;
                                lvFiles.EnsureVisible(lvFiles.Items.Count - 1);
                            }
                        }

                        //
                        // Update path name
                        //
                        tbFullPath.Text = getFullPath(tnNodeCurrent.FullPath);
                    }
                    catch (IOException /* e */)
                    {
                        MessageBox.Show("Error: I/O error");
                    }
                    catch (UnauthorizedAccessException /* e */)
                    {
                        MessageBox.Show("Error: Unauthorized Access");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error: " + e);
                    }
                }
            }
        }

        protected string getFullPath(string stringPath)
        {
            //
            // Get full path
            //
            string sParse = "";

            //
            // Remove "My Computer" from path.
            //
            sParse = stringPath.Replace("My Computer\\", "");

            //
            // Add "\\" to the root path.
            //
            if (sParse.IndexOf("\\") < 0)
            {
                sParse += "\\";
            }

            return sParse;
        }
        
        protected string formatDate(DateTime dtDate)
        {
            //
            // Get timedate string in short format
            //
            string sDate = "";

            sDate = dtDate.ToShortDateString().ToString() + " " + dtDate.ToShortTimeString().ToString();

            return sDate;
        }

        protected string formatSize(Int64 lSize)
        {
            //
            // Format number in kilobytes(kB)
            //
            string sSize = "";
            NumberFormatInfo nfi = new NumberFormatInfo();

            Int64 lKBSize = 0;

            if (lSize < 1024) 
            {
                if (lSize == 0) 
                {
                    //
                    // Zero byte
                    //
                    sSize = "0";
                }
                else 
                {
                    //
                    // Less than 1k but not zero byte
                    //
                    sSize = "1";
                }
            }
            else 
            {
                //
                // Convert to kB
                //
                lKBSize = lSize / 1024;

                //
                // Format number with default format
                //
                sSize = lKBSize.ToString("n", nfi);

                //
                // Remove decimal
                //
                sSize = sSize.Replace(".00", "");
            }

            return sSize + " kB";
        }

        private void tvFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //
            // Show folders and files when a folder is selected
            //

            //
            // Show wait cursor on the display
            //
            this.Cursor = Cursors.WaitCursor;

            //
            // Get current selected drive or folder
            //
            TreeNode tnNodeCurrent = e.Node;

            if (tnNodeCurrent.SelectedImageIndex == 9) 
            {
                //
                // "My Computer" is selected.  Do nothing.
                //
            }
            else
            {
                //
                // Clear all sub-folders
                //
                tnNodeCurrent.Nodes.Clear();

                //
                // Show sub-folders and folder files
                //
                showFolders(tnNodeCurrent, tnNodeCurrent.Nodes);
            }

            //
            // Restore cursor to the default
            //
            this.Cursor = Cursors.Default;
        }

        private double[] daTime = new double[0];            // Sampling Time
        private double[] daCurrent = new double[0];         // Measured Current
        private double[] daDigitized = new double[0];       // Digitized Data
        private double[] daFFT = new double[0];             // FFT(PSD) Result
        private double[] daXSmooth = new double[0];         // Smoothing Result (X)
        private double[] daYSmooth = new double[0];         // Smoothing Result (Y)

        private double dThreshold = 0.0;
        private double dHighLimit = 0.0;
        private double dLowLimit = 0.0;
        private double dHysteresis = 0.0;
        private double dLowLevel = 0.0;
        private double dHighLevel = 0.0;
        private double dFreq;

        private double[] daDistributionHighX;
        private double[] daDistributionLowX;
        private double[] daBinLineHigh;
        private double[] daFreqLineHigh;
        private double[] daBinLineLow;
        private double[] daFreqLineLow;

        private double[] daPeakX = new double[2];
        private double[] daPeakY = new double[2];

        private const string sLogFolder = "RTSLog";
        private bool bReverseSign = false;           // Must be same with initial cbReverseSign.Checked.
        private bool bDebug = true;                  // Must be same with initial cbDebugLog.Checked.
        private bool bShowDigitized = true;          // Must be same with initial cbShowDigitized.Checked.

        private Histogram cHist;
        private Digitize cDig;
        private Tau cTau;

        delegate void setBusyStatusDelegate(bool bBusy);
        private void setBusyStatus(bool bBusy)
        {
            btnAbort.Enabled = bBusy;
            tvFolders.Enabled = !bBusy;
            lvFiles.Enabled = !bBusy;
            cbReverseSign.Enabled = !bBusy;
            cbDebugLog.Enabled = !bBusy;
            cbShowDigitized.Enabled = !bBusy;

            if (!bBusy)
            {
                lvFiles.Focus();
                btnAbort.Text = "Abort";
            }
        }

        delegate void prepareAnalysisDelegate();
        private void prepareAnalysis()
        {
            //
            // Close dialogs
            //
            if (_dlgWave != null)
            {
                _dlgWave.Dispose();
            }
            if (_dlgPSD != null)
            {
                _dlgPSD.Dispose();
            }
            if (_dlgHistogram != null)
            {
                _dlgHistogram.Dispose();
            }
            if (_dlgDistribution != null)
            {
                _dlgDistribution.Dispose();
            }

            //
            // Clear edit boxes
            //
            tbNumSample.Text = "";
            tbFrequency.Text = "";
            tbThresholdHigh.Text = ""; 
            tbThresholdLow.Text = "";
            tbTon.Text = "";
            tbToff.Text = "";
            tbRatio.Text = "";
            pnText.Update();

            //
            // Clear graphs
            //
            _dgWaveform.ClearGraph();
            _dgPSD.ClearGraph();
            _dgHistogram.ClearGraph();
            _dgDistribution.ClearGraph();

            //
            // Clear data arrays
            //
            Array.Resize<double>(ref daDigitized, 0);
        }

        delegate void updateNumSampleDelegate(string s);
        private void updateNumSample(string s)
        {
            tbNumSample.Text = s;
            tbNumSample.Update();
        }

        delegate void updateFrequencyDelegate(string s);
        private void updateFrequency(string s)
        {
            tbFrequency.Text = s;
            tbFrequency.Update();
        }

        private void readCSV()
        {
            //
            // Reset return value
            //
            iReturnValue = 0;

            //
            // Clear graphs
            //
            _dgWaveform.ClearData();
            _dgPSD.ClearData();
            _dgHistogram.ClearData();
            _dgDistribution.ClearData();

            //
            // Read CSV file
            //
            CSVReader cr = new CSVReader(tbFullPath.Text + "\\" + tbFileName.Text);
            if (cr.read(ref daTime, ref daCurrent) <= 0)
            {
                //
                // Set error status message
                //
                Invoke(new updateNumSampleDelegate(updateNumSample), new object[] {"CSV ERROR"});

                //
                // Restore cursor to the default
                //
                Invoke(new setBusyStatusDelegate(setBusyStatus), new object[] { false });

                //
                // Set return value
                //
                iReturnValue = -1;
                return;
            }
            Invoke(new updateNumSampleDelegate(updateNumSample), new object[] { string.Format("{0, 0:D}", daCurrent.Length) });

            if (daTime.Length < 2)
            {
                //
                // Set error status message
                //
                Invoke(new updateFrequencyDelegate(updateFrequency), new object[] { "UNKNOWN" });

                //
                // Restore cursor to the default
                //
                Invoke(new setBusyStatusDelegate(setBusyStatus), new object[] { false });

                //
                // Set return value
                //
                iReturnValue = -1;
                return;
            }

            //
            // Set sampling frequency
            //
            dFreq = 1.0 / (daTime[1] - daTime[0]);

            //
            // Check sampling frequency
            //
            double dIntervalMin = Double.MaxValue;
            double dIntervalMax = -Double.MaxValue;
            for ( int i = 0 ; i < daTime.Length - 1 ; i++ )
            {
                if (dIntervalMin > (daTime[i + 1] - daTime[i]))
                {
                    dIntervalMin = (daTime[i + 1] - daTime[i]);
                }
                if (dIntervalMax < (daTime[i + 1] - daTime[i]))
                {
                    dIntervalMax = (daTime[i + 1] - daTime[i]);
                }
            }
            if (((dIntervalMax - dIntervalMin) / dIntervalMax) > 0.0001)
            {
                //
                // Ambiguous frequency
                //
                Invoke(new updateFrequencyDelegate(updateFrequency), new object[] { "AMBIGUOUS" });
                iReturnValue = -1;
            }
            else
            {
                //
                // No error
                //
                Invoke(new updateFrequencyDelegate(updateFrequency), new object[] { string.Format("{0, 0:E3}", dFreq) });
                iReturnValue = 0;
            }

            return;
        }

        delegate void updateWaveformDelegate();
        private void updateWaveform()
        {
            //
            // Update waveform
            //
            _dgWaveform.ClearData();
            _dgWaveform.AddData(GraphType.LINE, daTime, daCurrent, Color.Blue, "", true, false, "");
            _dgWaveform.RedrawGraph();
        }

        delegate void updatePSDDelegate();
        private void updatePSD()
        {
            _dgPSD.ClearData();
            _dgPSD.AddData(GraphType.LINE, daXSmooth, daYSmooth, Color.Blue, "", true, false, "");
            _dgPSD.RedrawGraph();
        }
                
        private void analyzePSD()
        {
            //
            // Reset return value
            //
            iReturnValue = 0;

            //
            // FFT
            //
            FFT fft = new FFT(daCurrent);
            fft.fft();
            if (bDebug)
            {
                try
                {
                    Directory.CreateDirectory(tbFullPath.Text + "\\" + sLogFolder);
                    string sLogFileName = tbFullPath.Text + "\\" + sLogFolder + "\\" + tbFileName.Text;
                    sLogFileName = Path.ChangeExtension(sLogFileName, "fft");
                    using (StreamWriter sw = new StreamWriter(sLogFileName))
                    {
                        for (int i = 0; i < fft.daReal.Length; i++)
                        {
                            sw.WriteLine(fft.daReal[i] + "," + fft.daImage[i] + "i");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error occurs while writing Output Log.\n" + e.Message);
                }
            }

            //
            // FFT -> A ^ 2 / Hz
            //
            Array.Resize<double>(ref daFFT, fft.daReal.Length);
            for ( int i = 0 ; i < fft.daReal.Length ; i++ )
            {
                daFFT[i] = (System.Math.Pow(fft.daReal[i], 2) + System.Math.Pow(fft.daImage[i], 2)) / System.Math.Pow(fft.daReal.Length, 2);
                daFFT[i] /= (dFreq / fft.daReal.Length);
            }
            Array.Resize<double>(ref daFFT, daFFT.Length / 2);
            for ( int i = 0 ; i < daFFT.Length ; i++ )
            {
                daFFT[i] *= 2.0;
            }

            //
            // Prepare X/Y data for smoothing (2~End)
            //
            Array.Resize<double>(ref daXSmooth, daFFT.Length - 1);
            Array.Resize<double>(ref daYSmooth, daFFT.Length - 1);
            for ( int i = 0 ; i < daFFT.Length - 1 ; i++ )
            {
                daXSmooth[i] = (dFreq / 2 / daFFT.Length) * (i + 1);
                daYSmooth[i] = daFFT[i + 1];
            }

            //
            // Clean up FFT result
            //
            Array.Resize<double>(ref daFFT, 0);

            //
            // Moving Average Filter
            //
            Filters.MovingAverageFilter(ref daYSmooth, 49);

            //
            // Eliminate first 20 elements
            //
            if (daXSmooth.Length > 100)
            {
                Array.Reverse(daXSmooth);
                Array.Resize<double>(ref daXSmooth, daXSmooth.Length - 20);
                Array.Reverse(daXSmooth);
                Array.Reverse(daYSmooth);
                Array.Resize<double>(ref daYSmooth, daYSmooth.Length - 20);
                Array.Reverse(daYSmooth);
            }

            //
            // Eliminate last 10 elements
            //
            if (daXSmooth.Length > 100)
            {
                Array.Resize<double>(ref daXSmooth, daXSmooth.Length - 10);
                Array.Resize<double>(ref daYSmooth, daYSmooth.Length - 10);
            }

            //
            // Update PSD graph
            //
            Invoke(new updatePSDDelegate(updatePSD));

            if (bDebug)
            {
                try
                {
                    Directory.CreateDirectory(tbFullPath.Text + "\\" + sLogFolder);
                    string sLogFileName = tbFullPath.Text + "\\" + sLogFolder + "\\" + tbFileName.Text;
                    sLogFileName = Path.ChangeExtension(sLogFileName, "psd");
                    using (StreamWriter sw = new StreamWriter(sLogFileName))
                    {
                        for (int i = 0; i < daXSmooth.Length; i++)
                        {
                            sw.WriteLine(daXSmooth[i] + "," + daYSmooth[i]);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error occurs while writing Output Log.\n" + e.Message);
                }
            }
        }

        delegate void updateHistogramDelegate();
        private void updateHistogram()
        {
            _dgHistogram.ClearData();
            _dgHistogram.AddData(GraphType.HISTOGRAM, cHist.daX, cHist.daY, Color.Green, "", true, false, "");
            _dgHistogram.AddData(GraphType.LINE, cHist.daX, cHist.daYSmooth, Color.Blue, "", true, false, "");
            if (cHist.alPeak.Count >= 2)
            {
                //
                // Two (or more) peaks were found.
                //

                //
                // Plot top two peaks
                //
                for ( int i = 0 ; i < 2 ; i++ )
                {
                    daPeakX[i] = ((PeakData)(cHist.alPeak[i])).dValue;
                    daPeakY[i] = ((PeakData)(cHist.alPeak[i])).dHeight;
                }
                _dgHistogram.AddData(GraphType.PLOT, daPeakX, daPeakY, Color.Yellow, "O", false, false, "");

                //
                // Calculate paraemters for digitizing.
                //
                dLowLevel = System.Math.Min(((PeakData)(cHist.alPeak[0])).dValue, ((PeakData)(cHist.alPeak[1])).dValue);
                dHighLevel = System.Math.Max(((PeakData)(cHist.alPeak[0])).dValue, ((PeakData)(cHist.alPeak[1])).dValue);
                dThreshold = (dLowLevel + dHighLevel) / 2;
                dHighLimit = System.Math.Min(cHist.dMax, 2 * dHighLevel - dThreshold);
                dLowLimit = System.Math.Max(cHist.dMin, 2 * dLowLevel - dThreshold);
                //dHysteresis = cHist.dStd / 2;
                dHysteresis = (dHighLevel - dLowLevel) * 0.2;
            }
            _dgHistogram.RedrawGraph();

            //
            // Result
            //
            tbThresholdHigh.Text = string.Format("{0,0:E3}", dHighLevel);
            tbThresholdLow.Text = string.Format("{0,0:E3}", dLowLevel);
        }

        private void analyzeThreshold()
        {
            //
            // Reset return value
            //
            iReturnValue = 0;

            //
            // Bin and calculate threshold
            //
            cHist = new Histogram(daCurrent);
            cHist.Bin(200);
            cHist.FindPeak(2, 10);

            if (bDebug)
            {
                try
                {
                    Directory.CreateDirectory(tbFullPath.Text + "\\" + sLogFolder);
                    string sLogFileName = tbFullPath.Text + "\\" + sLogFolder + "\\" + tbFileName.Text;
                    sLogFileName = Path.ChangeExtension(sLogFileName, "his");
                    using (StreamWriter sw = new StreamWriter(sLogFileName))
                    {
                        for (int i = 0; i < cHist.daX.Length; i++)
                        {
                            sw.WriteLine(cHist.daX[i] + "," + cHist.daY[i]);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error occurs while writing Output Log.\n" + e.Message);
                }
            }

            //
            // Update histogram
            //
            Invoke(new updateHistogramDelegate(updateHistogram));

            if (cHist.alPeak.Count < 2)
            {
                //
                // If less than two peaks were found, stop analysis.
                //

                //
                // Restore cursor to the default
                //
                Invoke(new setBusyStatusDelegate(setBusyStatus), new object[] { false });

                //
                // Set return value
                //
                iReturnValue = -1;
                return;
            }
        }

        delegate void addDigitalWaveformDelegate();
        private void addDigitalWaveform()
        {
            //
            // NOTE: This function does NOT clear waveform graph.
            //
            double dYmin = _dgWaveform.GetMinY();
            double dYstep = _dgWaveform.GetStepY();
            Array.Resize<double>(ref daDigitized, daTime.Length);
            for ( int i = 0 ; i < daTime.Length ; i++ )
            {
                if (_bOverlapDigitizeResult)
                {
                    daDigitized[i] = dLowLevel + (dHighLevel - dLowLevel) * cDig.iaHL[i];
                }
                else
                {
                    daDigitized[i] = dYmin + dYstep * (0.1 + cDig.iaHL[i] * 0.5);
                }
            }
            if (bShowDigitized)
            {
                _dgWaveform.AddData(GraphType.LINE, daTime, daDigitized, Color.Red, "", false, false, "");
                _dgWaveform.RedrawGraph();
            }
        }

        private void digitizeWaveform()
        {
            //
            // Reset return value
            //
            iReturnValue = 0;

            //
            // Digitize
            //
            cDig = new Digitize(daCurrent, dThreshold, dHysteresis, dHighLevel, dLowLevel, dHighLimit, dLowLimit);
            cDig.digitize();
            if (bDebug)
            {
                try
                {
                    Directory.CreateDirectory(tbFullPath.Text + "\\" + sLogFolder);
                    string sLogFileName = tbFullPath.Text + "\\" + sLogFolder + "\\" + tbFileName.Text;
                    sLogFileName = Path.ChangeExtension(sLogFileName, "dig");
                    using (StreamWriter sw = new StreamWriter(sLogFileName))
                    {
                        foreach (int v in cDig.iaHL)
                        {
                            sw.WriteLine(v);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error occurs while writing Output Log.\n" + e.Message);
                }
            }

            //
            // Add digital waveform
            //
            Invoke(new addDigitalWaveformDelegate(addDigitalWaveform));
        }

        delegate void updateDistributionDelegate();
        private void updateDistribution()
        {
            if ((cTau.histH != null) && (cTau.histL != null))
            {
                _dgDistribution.ClearData();

                //
                // Calculate X-axis values
                //
                daDistributionLowX = new double[cTau.histL.daX.Length];
                for ( int i = 0 ; i < daDistributionLowX.Length ; i++ )
                {
                    daDistributionLowX[i] = cTau.histL.daX[i] / dFreq;
                }
                daDistributionHighX = new double[cTau.histH.daX.Length];
                for ( int i = 0 ; i < daDistributionHighX.Length ; i++ )
                {
                    daDistributionHighX[i] = cTau.histH.daX[i] / dFreq;
                }

                //
                // Plot binning result
                //
                if (daCurrent[0] > 0)
                {
                    _dgDistribution.AddData(GraphType.PLOT, daDistributionHighX, cTau.histH.daY, Color.Blue, "o", true, true, "Ton");
                    _dgDistribution.AddData(GraphType.PLOT, daDistributionLowX, cTau.histL.daY, Color.Green, "x", true, true, "Toff");
                }
                else
                {
                    _dgDistribution.AddData(GraphType.PLOT, daDistributionLowX, cTau.histL.daY, Color.Blue, "o", true, true, "Ton");
                    _dgDistribution.AddData(GraphType.PLOT, daDistributionHighX, cTau.histH.daY, Color.Green, "x", true, true, "Toff");
                }
                _dgDistribution.SetRange(0.0, System.Math.Max(daDistributionLowX[daDistributionLowX.Length - 1], daDistributionHighX[daDistributionHighX.Length - 1]));

                //
                // Draw fitting result (High)
                //
                if ((System.Math.Abs(cTau.daResultHigh[0]) > Double.Epsilon) &&
                    (System.Math.Abs(cTau.daResultHigh[1]) > Double.Epsilon))
                {
                    daBinLineHigh = new double[cTau.histH.daX.Length];
                    cTau.histH.daX.CopyTo(daBinLineHigh, 0);
                    daFreqLineHigh = new double[cTau.histH.daX.Length];
                    for ( int i = 0 ; i < daBinLineHigh.Length ; i++ )
                    {
                        daFreqLineHigh[i] = cTau.daResultHigh[0] * System.Math.Exp(cTau.daResultHigh[1] * daBinLineHigh[i]);
                    }
                    _dgDistribution.AddData(GraphType.LINE, daDistributionHighX, daFreqLineHigh, (daCurrent[0] > 0) ? Color.Blue : Color.Green, "", false, false, "");
                }

                //
                // Draw fitting result (Low)
                //
                if ((System.Math.Abs(cTau.daResultLow[0]) > Double.Epsilon) &&
                    (System.Math.Abs(cTau.daResultLow[1]) > Double.Epsilon))
                {
                    daBinLineLow = new double[cTau.histL.daX.Length];
                    cTau.histL.daX.CopyTo(daBinLineLow, 0);
                    daFreqLineLow = new double[cTau.histL.daX.Length];
                    for ( int i = 0 ; i < daBinLineLow.Length ; i++ )
                    {
                        daFreqLineLow[i] = cTau.daResultLow[0] * System.Math.Exp(cTau.daResultLow[1] * daBinLineLow[i]);
                    }
                    _dgDistribution.AddData(GraphType.LINE, daDistributionLowX, daFreqLineLow, (daCurrent[0] > 0) ? Color.Green : Color.Blue, "", false, false, "");
                }

                _dgDistribution.RedrawGraph();
            }

            //
            // Result
            //
            tbTon.Text = string.Format("{0,0:E3}", cTau.dTauOn);
            tbToff.Text = string.Format("{0,0:E3}", cTau.dTauOff);
            if (cTau.dTauOff != 0)
            {
                tbRatio.Text = string.Format("{0,0:0.######}", cTau.dTauOn / cTau.dTauOff);
            }
        }

        private void calculateTau()
        {
            //
            // Reset return value
            //
            iReturnValue = 0;

            //
            // Calculate Tau
            //
            cTau = new Tau();
            int iResultTau = cTau.Calculate(dFreq, cDig, daCurrent[0] > 0);

            //
            // Update calculation result
            //
            Invoke(new updateDistributionDelegate(updateDistribution));

            if (bDebug)
            {
                try
                {
                    Directory.CreateDirectory(tbFullPath.Text + "\\" + sLogFolder);
                    string sLogFileName = tbFullPath.Text + "\\" + sLogFolder + "\\" + tbFileName.Text;
                    sLogFileName = Path.ChangeExtension(sLogFileName, "tau");
                    using (StreamWriter sw = new StreamWriter(sLogFileName))
                    {
                        sw.WriteLine("Status = " + ((iResultTau == 0) ? "Success" : "Failure"));
                        sw.WriteLine("");

                        sw.WriteLine("High");
                        sw.WriteLine("Bin,Frequency");
                        if (cTau.histH != null)
                        {
                            for (int i = 0; i < cTau.histH.daX.Length; i++)
                            {
                                sw.WriteLine(cTau.histH.daX[i] + "," + cTau.histH.daY[i]);
                            }
                        }
                        sw.WriteLine("");

                        sw.WriteLine("Low");
                        sw.WriteLine("Bin,Frequency");
                        if (cTau.histL != null)
                        {
                            for (int i = 0; i < cTau.histL.daX.Length; i++)
                            {
                                sw.WriteLine(cTau.histL.daX[i] + "," + cTau.histL.daY[i]);
                            }
                        }
                        sw.WriteLine("");

                        sw.WriteLine("Fitting Result");
                        sw.WriteLine("High:  A = " + cTau.daResultHigh[1] + " B = " + cTau.daResultHigh[0]);
                        sw.WriteLine("Low:   A = " + cTau.daResultLow[1] + " B = " + cTau.daResultLow[0]);
                        sw.WriteLine("");

                        sw.WriteLine("Residual Square Mean");
                        sw.WriteLine("High:  " + string.Format("{0,0:000.000000}", cTau.dResidualHigh));
                        sw.WriteLine("Low:   " + string.Format("{0,0:000.000000}", cTau.dResidualLow));
                        sw.WriteLine("");

                        sw.WriteLine("Ton  = " + cTau.dTauOn);
                        sw.WriteLine("Toff = " + cTau.dTauOff);
                        if (cTau.dTauOff != 0)
                        {
                            sw.WriteLine("Ton / Toff  = " + (cTau.dTauOn / cTau.dTauOff));
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error occurs while writing Output Log.\n" + e.Message);
                }
            }
        }

        delegate void updateFilenameDelegate();
        private void updateFilename()
        {
            //
            // Show the last selected filename.
            //
            ListView.SelectedListViewItemCollection slv = lvFiles.SelectedItems;
            foreach(ListViewItem lvItem in slv)
            {
                tbFileName.Text = lvItem.SubItems[0].Text;
            }
        }

        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 0) return;

            int iImageIndex = -1;
            ListView.SelectedListViewItemCollection slv = lvFiles.SelectedItems;
            foreach(ListViewItem lvItem in slv)
            {
                iImageIndex = lvItem.ImageIndex;
            }

            if (iImageIndex == 7)
            {
                //
                // Do nothing for folders.
                //
            }
            else
            {
                //
                // Check current background job status.
                //
                if (m_bgwAnalysis.IsBusy)
                {
                    MessageBox.Show("Previous analysis is still running.");
                    return;
                }

                //
                // Perform analysis as a background job.
                //
                m_bgwAnalysis.RunWorkerAsync(true);
            }
        }

        private void expandFolder(TreeNode tn, string sSelected)
        {
            int iClosedIndex = 7;    // Closed Folder (7)
            int iOpenedIndex = 8;    // Opened Folder (8)

            if (!tn.IsExpanded)
            {
                //
                // Expand the node.
                //
                tn.Expand();

                //
                // Clear all sub-folders
                //
                tn.Nodes.Clear();

                try
                {
                    //
                    // Show sub-folders
                    //
                    string[] saFolders = Directory.GetDirectories(getFullPath(tn.FullPath));
                    string sFullPath = "";
                    string sPathName = "";

                    //
                    // Walk through all folders
                    //
                    foreach (string sFolder in saFolders)
                    {
                        sFullPath = sFolder;
                        sPathName = getPathName(sFullPath);
                            
                        //
                        // Create node for folders
                        //
                        TreeNode tnNodeDir = new TreeNode(sPathName.ToString(), iClosedIndex, iOpenedIndex);
                        tn.Nodes.Add(tnNodeDir);

                        if (sPathName == sSelected)
                        {
                            tvFolders.SelectedNode = tnNodeDir;
                            tnNodeDir.EnsureVisible();
                        }
                    }
                }
                catch (Exception /* ex */)
                {
                }
            }
        }

        private void lvFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 0) return;

            int iImageIndex = -1;
            string sSelected = "";

            ListView.SelectedListViewItemCollection slv = lvFiles.SelectedItems;
            foreach(ListViewItem lvItem in slv)
            {
                iImageIndex = lvItem.ImageIndex;
                sSelected = lvItem.SubItems[0].Text;
            }

            if (iImageIndex == 7)
            {
                //
                // Open the folder.
                //
                TreeNode tnNodeCurrent = tvFolders.SelectedNode;
                foreach(TreeNode tn in tnNodeCurrent.Nodes)
                {
                    if (tn.Text == sSelected)
                    {
                        tvFolders.SelectedNode = tn;
                        tnNodeCurrent = tn;
                    }
                }
            }
            else
            {
                //
                // Perform analysis as a background job.
                //
                m_bgwAnalysis.RunWorkerAsync(true);
            }
        }

        private void ResizeComponent()
        {
            int iLeft, iTop, iWidth, iHeight;

            //
            // Waveform area (Top-Left)
            //
            iLeft = 0;
            iTop = 0;
            iWidth = pnGraphBase.Size.Width / 2;
            iHeight = pnGraphBase.Size.Height / 2;
            pnWaveForm.SetBounds(iLeft, iTop, iWidth, iHeight);

            //
            // PSD area (Top-Right)
            //
            iLeft = iWidth;
            iWidth = pnGraphBase.Size.Width - iWidth;
            pnPSD.SetBounds(iLeft, iTop, iWidth, iHeight);

            //
            // Histogram area (Bottom-Left)
            //
            iLeft = 0;
            iTop = iHeight;
            iWidth = pnGraphBase.Size.Width / 2;
            iHeight = pnGraphBase.Size.Height - iHeight;
            pnHistogram.SetBounds(iLeft, iTop, iWidth, iHeight);

            //
            // Distribution area (Bottom-Right)
            //
            iLeft = iWidth;
            iWidth = pnGraphBase.Size.Width - iWidth;
            pnDistribution.SetBounds(iLeft, iTop, iWidth, iHeight);

            //
            // Resize child components
            //
            _dgWaveform.ResizeComponent();
            _dgPSD.ResizeComponent();
            _dgHistogram.ResizeComponent();
            _dgDistribution.ResizeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            ResizeComponent();

            //
            // 1st parameter is a filename.
            //
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                directAnalysis(args[1]);
            }
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            ResizeComponent();
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if ((WindowState != _fws) && (WindowState != FormWindowState.Minimized))
            {
                ResizeComponent();
            }
            _fws = WindowState;
        }

        private void spMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            ResizeComponent();
        }

        private void cbReverseSign_CheckedChanged(object sender, EventArgs e)
        {
            bReverseSign = cbReverseSign.Checked;

            //
            // If no data, return immediately.
            //
            if (daTime.Length < 2)
            {
                return;
            }

            //
            // Reverse polarity
            //
            for ( int i = 0 ; i < daCurrent.Length ; i++ )
            {
                daCurrent[i] *= -1.0;
            }

            //
            // Re-analyze as a background job.
            //
            m_bgwAnalysis.RunWorkerAsync(false);
        }

        private void cbDebugLog_CheckedChanged(object sender, EventArgs e)
        {
            bDebug = cbDebugLog.Checked;

            if (bDebug)
            {
                MessageBox.Show("Analysis log files are stored from next time.");
            }
            else
            {
                MessageBox.Show("Analysis log files are NOT stored from next time.");
            }
        }

        private void cbShowDigitized_CheckedChanged(object sender, EventArgs e)
        {
            bShowDigitized = cbShowDigitized.Checked;

            if (daDigitized.Length > 0)
            {
                //
                // Clear all graphs and redraw again.
                //
                _dgWaveform.ClearData();
                _dgWaveform.AddData(GraphType.LINE, daTime, daCurrent, Color.Blue, "", true, false, "");
                if (bShowDigitized && (daDigitized.Length > 0))
                {
                    _dgWaveform.AddData(GraphType.LINE, daTime, daDigitized, Color.Red, "", false, false, "");
                }
                _dgWaveform.RedrawGraph();
            }
        }

        private void pbWaveForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //
            // If no data, return immediately.
            //
            if (daTime.Length < 2)
            {
                return;
            }

            if ((_dlgWave == null) || (_dlgWave.IsDisposed))
            {
                _dlgWave = new DialogGraph();
                _dlgWave.Text = sTitleWaveform + " (" + tbFileName.Text + ")";
                _dlgWave.dg.CopyFrom(_dgWaveform);
            }
            _dlgWave.TopMost = true;
            _dlgWave.Show();
            _dlgWave.TopMost = false;
        }

        private void pbPSD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (daTime.Length < 2)
            {
                return;
            }

            if ((_dlgPSD == null) || (_dlgPSD.IsDisposed))
            {
                _dlgPSD = new DialogGraph();
                _dlgPSD.Text = sTitlePSD + " (" + tbFileName.Text + ")";
                _dlgPSD.dg.CopyFrom(_dgPSD);
            }
            _dlgPSD.TopMost = true;
            _dlgPSD.Show();
            _dlgPSD.TopMost = false;
        }

        private void pbHistogram_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cHist == null)
            {
                return;
            }

            if ((_dlgHistogram == null) || (_dlgHistogram.IsDisposed))
            {
                _dlgHistogram = new DialogGraph();
                _dlgHistogram.Text = sTitleHistogram + " (" + tbFileName.Text + ")";
                _dlgHistogram.dg.CopyFrom(_dgHistogram);
            }
            _dlgHistogram.TopMost = true;
            _dlgHistogram.Show();
            _dlgHistogram.TopMost = false;
        }

        private void pbDistribution_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cTau == null)
            {
                return;
            }

            if ((_dlgDistribution == null) || (_dlgDistribution.IsDisposed))
            {
                _dlgDistribution = new DialogGraph();
                _dlgDistribution.Text = sTitleDistribution + " (" + tbFileName.Text + ")";
                _dlgDistribution.dg.CopyFrom(_dgDistribution);
           }
            _dlgDistribution.TopMost = true;
            _dlgDistribution.Show();
            _dlgDistribution.TopMost = false;
        }

        private static int iReturnValue;
        private void m_bgwAnalysis_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread thread;
            BackgroundWorker bgw = (BackgroundWorker)sender;

            //
            // Perform analysis.
            //
            Invoke(new updateFilenameDelegate(updateFilename));

            //
            // Close child dialogs and clear analysis data
            //
            Invoke(new prepareAnalysisDelegate(prepareAnalysis));

            //
            // Show wait cursor on the display
            //
            Invoke(new setBusyStatusDelegate(setBusyStatus), new object[] { true });

            //
            // Show the file
            //
            try
            {
                if ((bool)e.Argument)
                {
                    //
                    // Read CSV file
                    //
                    thread = new Thread(new ThreadStart(readCSV));
                    thread.Start();
                    while (thread.Join(1000) == false)
                    {
                        if (bgw.CancellationPending)
                        {
                            thread.Abort();
                            thread.Join();
                            e.Cancel = true;
                            throw new CancelAnalysisException();
                        }
                    }
                    if (iReturnValue != 0)
                    {
                        throw new CancelAnalysisException();
                    }

                    if (bReverseSign)
                    {
                        //
                        // Reverse polarity
                        //
                        for ( int i = 0 ; i < daCurrent.Length ; i++ )
                        {
                            daCurrent[i] *= -1.0;
                        }
                    }
                }
                else
                {
                    Invoke(new updateNumSampleDelegate(updateNumSample), new object[] { string.Format("{0, 0:D}", daCurrent.Length) });
                    Invoke(new updateFrequencyDelegate(updateFrequency), new object[] { string.Format("{0, 0:E3}", dFreq) });
                }

                //
                // Update waveform
                //
                Invoke(new updateWaveformDelegate(updateWaveform));

                //
                // Perform FFT, convert to PSD, and show its result
                //
                thread = new Thread(new ThreadStart(analyzePSD));
                thread.Start();
                while (thread.Join(1000) == false)
                {
                    if (bgw.CancellationPending)
                    {
                        thread.Abort();
                        thread.Join();
                        e.Cancel = true;
                        throw new CancelAnalysisException();
                    }
                }
                if (iReturnValue != 0)
                {
                    throw new CancelAnalysisException();
                }

                //
                // Analyze threshold values
                //
                thread = new Thread(new ThreadStart(analyzeThreshold));
                thread.Start();
                while (thread.Join(1000) == false)
                {
                    if (bgw.CancellationPending)
                    {
                        thread.Abort();
                        thread.Join();
                        e.Cancel = true;
                        throw new CancelAnalysisException();
                    }
                }
                if (iReturnValue != 0)
                {
                    throw new CancelAnalysisException();
                }

                //
                // Digitize waveform and show its result
                //
                thread = new Thread(new ThreadStart(digitizeWaveform));
                thread.Start();
                while (thread.Join(1000) == false)
                {
                    if (bgw.CancellationPending)
                    {
                        thread.Abort();
                        thread.Join();
                        e.Cancel = true;
                        throw new CancelAnalysisException();
                    }
                }
                if (iReturnValue != 0)
                {
                    throw new CancelAnalysisException();
                }

                //
                // Calculate tau and show its result
                //
                thread = new Thread(new ThreadStart(calculateTau));
                thread.Start();
                while (thread.Join(1000) == false)
                {
                    if (bgw.CancellationPending)
                    {
                        thread.Abort();
                        thread.Join();
                        e.Cancel = true;
                        throw new CancelAnalysisException();
                    }
                }
                if (iReturnValue != 0)
                {
                    throw new CancelAnalysisException();
                }
            }
            catch (CancelAnalysisException /* ex */)
            {
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }

            //
            // Restore cursor to the default
            //
            Invoke(new setBusyStatusDelegate(setBusyStatus), new object[] { false });
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            m_bgwAnalysis.CancelAsync();
            btnAbort.Text = "Aborting...";
        }

        private void pbWaveForm_MouseEnter(object sender, EventArgs e)
        {
            tsslHelp.Text = "To enlarge the waveform, double left click it.";
        }

        private void pbWaveForm_MouseLeave(object sender, EventArgs e)
        {
            tsslHelp.Text = "";
        }

        private void pbPSD_MouseEnter(object sender, EventArgs e)
        {
            tsslHelp.Text = "To enlarge the PSD graph, double left click it.";
        }

        private void pbPSD_MouseLeave(object sender, EventArgs e)
        {
            tsslHelp.Text = "";
        }

        private void pbHistogram_MouseEnter(object sender, EventArgs e)
        {
            tsslHelp.Text = "To enlarge the histogram, double left click it.";
        }

        private void pbHistogram_MouseLeave(object sender, EventArgs e)
        {
            tsslHelp.Text = "";
        }

        private void pbDistribution_MouseEnter(object sender, EventArgs e)
        {
            tsslHelp.Text = "To enlarge the distribution graph, double left click it.";
        }

        private void pbDistribution_MouseLeave(object sender, EventArgs e)
        {
            tsslHelp.Text = "";
        }

        private void lvFiles_MouseEnter(object sender, EventArgs e)
        {
            tsslHelp.Text = "Select (or double left click) to analyze RTS result file.";
        }

        private void lvFiles_MouseLeave(object sender, EventArgs e)
        {
            tsslHelp.Text = "";
        }

        private void directAnalysis(string sFileName)
        {
            string[] sSplit = sFileName.Split(new Char[] { '\\' });
            if (sSplit.Length <= 0)
            {
                return;
            }

            //
            // Expand "My Computer".
            //
            TreeNode tnRootNode = tvFolders.Nodes[0];
            tnRootNode.Expand();

            //
            // Expand for dragged filename.
            //
            TreeNodeCollection tnc = tnRootNode.Nodes;
            TreeNode tn = null;

            for ( int i = 0 ; i < sSplit.Length - 1 ; i++ )
            {
                for ( int j = 0 ; j < tnc.Count ; j++ )
                {
                    if (tnc[j].Text == sSplit[i])
                    {
                        tn = tnc[j];
                        expandFolder(tn, sSplit[i]);
                        tnc = tnc[j].Nodes;
                        break;
                    }
                }
            }

            //
            // Select the filename and analyze it.
            //
            if (tn != null)
            {
                tvFolders.SelectedNode = tn;
                showFiles(tn, sSplit[sSplit.Length - 1]);
            }
        }

        private void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            //
            // Split into folder and file names.
            // (At least 1 string is needed)
            //
            string[] saDragFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            directAnalysis(saDragFiles[0]);
        }

        private void MainWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] saDragFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                //
                // Multiple selections are NOT allowed.
                //
                if (saDragFiles.Length != 1)
                {
                    return;
                }

                //
                // Folder is NOT allowed.
                //
                if (!System.IO.File.Exists(saDragFiles[0]))
                {
                    return;
                }

                //
                // Update a cursor for dropping.
                //
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (_tnNodeCurrent != null && _tnNodeCurrent.Parent != null && _tnNodeCurrent.Parent.Text != "My Computer")
            {
                _tnNodeCurrent = _tnNodeCurrent.Parent;

                //
                // Clear all sub-folders
                //
                _tnNodeCurrent.Nodes.Clear();

                //
                // Show sub-folders and folder files
                //
                showFolders(_tnNodeCurrent, _tnNodeCurrent.Nodes);
            }
        }
    }

    public class CancelAnalysisException : Exception
    {
        public CancelAnalysisException()
        {
        }
    }
}
