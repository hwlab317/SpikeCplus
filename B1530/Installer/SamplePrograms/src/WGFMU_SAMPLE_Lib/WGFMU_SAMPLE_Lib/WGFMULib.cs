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

// Rev. A.01.01.2009.01.07



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace WGFMU_SAMPLE_Lib
{
    public partial class WGFMULib
    {

        // Parameter Range of WGFMU

        #region Input Parameter Range

        public const double DELTA_TIME_MIN = 9.99E-9;
        public const double DELTA_TIME_MAX = 1.001E+4;
        public const double V_RESOLUTION = 9.99E-6;
        public const int SEQUENCE_ID_MIN = 0;
        public const int SEQUENCE_ID_MAX = 511;
        public const double SEQUENCE_LOOP_MAX = 1000000000000;
        public const int VECTOR_LENGTH_MAX = 2048;
        public const int PATTERN_NAME_MAX = 256;
        public const double HWDELAY_MAX = 49.9E-9;
        public const double HWDELAY_MIN = -49.9E-9;
        public const double VFORCE_MAX = 10;
        public const double VFORCE_MIN = -10;
        public const double SMU_VFORCE_MAX = 10;
        public const double SMU_VFORCE_MIN = -10;
        public const double SMU_ICOMP_MAX = 100.00001E-3;
        public const double SMU_ICOMP_MIN = -100.00001E-3;
        public const double DCSTRESS_DURAION_MAX = 1E+6;
        public const double DCSTRESS_DURAION_MIN = DELTA_TIME_MIN;
        public const double MEAS_DELAY_MAX = 1E+6;
        public const double MEAS_DELAY_MIN = -1E+6;
        public const double AVERAGING_TIME_MAX = 20.001E-3;
        public const double AVERAGING_TIME_MIN = 9.99E-9;
        public const int SWEEP_STEP_MAX = 101;
        public const int SWEEP_STEP_MIN = 0;
        public const int SAMPLING_POINTS_MAX = 4000000;
        public const int SAMPLING_POINTS_MIN = 1;
        public const double SAMPLING_INTERVAL_MIN = 9.99E-9;
        public const double SAMPLING_INTERVAL_MAX = 1.0;
        public const double TERMINATION_DELAY_MIN = 0.0;
        public const double TERMINATION_DELAY_MAX = DELTA_TIME_MAX;
        public const double SEQUENCE_DELAY_MIN = -1.0 * DELTA_TIME_MAX;
        public const double SEQUENCE_DELAY_MAX = DELTA_TIME_MAX;
        public const double EDGE_MIN = DELTA_TIME_MIN;
        public const double EDGE_MAX = DELTA_TIME_MAX;
        public const double HOLD_MIN = 0.0;
        public const double HOLD_MAX = DELTA_TIME_MAX;
        public const double PULSE_DELAY_MIN = 0.0;
        public const double PULSE_DELAY_MAX = DELTA_TIME_MAX;
        public const double PULSE_EDGE_MIN = DELTA_TIME_MIN;
        public const double PULSE_EDGE_MAX = DELTA_TIME_MAX;
        public const double PULSE_WIDTH_MIN = DELTA_TIME_MIN;
        public const double PULSE_WIDTH_MAX = DELTA_TIME_MAX * 2;
        public const double PULSE_PERIOD_MIN = DELTA_TIME_MIN * 2;
        public const double PULSE_PERIOD_MAX = DELTA_TIME_MAX * 5;
        public const double DUAL_SWEEP_DELAY_MIN = 0.0;
        public const double DUAL_SWEEP_DELAY_MAX = DELTA_TIME_MAX;
        public const double SWEEP_HOLD_MIN = 0.0;
        public const double SWEEP_HOLD_MAX = DELTA_TIME_MAX;
        public const double SWEEP_STEP_DELAY_MIN = 0.0;
        public const double SWEEP_STEP_DELAY_MAX = DELTA_TIME_MAX;
        public const double PULSE_DUTY_MIN = 0.0;
        public const double PULSE_DUTY_MAX = 100.0;
        public const double STRESS_MEAS_DELAY_MIN = 0.0;
        public const double STRESS_MEAS_DELAY_MAX = DELTA_TIME_MAX;
        public const double STRESS_RANGE_CHANGE_HOLD_MIN = 0.0;
        public const double STRESS_RANGE_CHANGE_HOLD_MAX = DELTA_TIME_MAX;
        public const double MEAS_INTERVAL_MIN = DELTA_TIME_MIN;
        public const double MEAS_INTERVAL_MAX = DELTA_TIME_MAX;
        public const int MEAS_ITERATION_NUM_MIN = 1;
        public const int MEAS_ITERATION_NUM_MAX = 10000;
        public const double SAMPLING_RATE_MIN = 1;
        public const double SAMPLING_RATE_MAX = 200.0001E+6;
        public const double SEQUENCE_GAP_TIME = 50.0E-9;

        #endregion

        GPIB gpib = new GPIB();

        private string Revision = "";
        string LogFileName = Application.StartupPath + @"\WgfmuLog.txt";

        private string VisaId = "";
        private int GpibAddress = 0;
        private int LogEnable = 0;
        private int OnLine = 0;

        private int ErrMsgSize = 0;
        private StringBuilder ErrMsg = new StringBuilder();
        public const int WGFMU_SAMPLE_LIB_ERROR = -42;
        private const int GENERAL_EXCEPTION_ERROR = -41;

        private int[] ChannelIdList;
        private string[] ModuleList = new string[10];

        private string SaveFileInitialDirectory = "";
        private Stopwatch ExecTimer = new Stopwatch();
        private double RecordedTime = 0.0;


        #region properties

        public string WgfmuRevision
        {
            get { return Revision; }
        }

        public string WgfmuVisaId
        {
            set { VisaId = value; }
            get { return VisaId; }
        }

        public int WgfmuGpibAddress
        {
            set { GpibAddress = value; }
            get { return GpibAddress; }

        }

        public int WgfmuDebuEnable
        {
            set { LogEnable = value; }
        }

        public int WgfmuOnline
        {
            set { OnLine = value; }
        }

        public int[] WgfmuChannelList
        { 
            get { return ChannelIdList;}
        }

        public string[] WgfmuModuleList
        {
            get { return ModuleList; }
        }


        #endregion

        #region GUI Actions

        #region CheckChannelConflict: Check the conflict of channel assignment for source 1 and 2
        // Method  name: CheckChannelConflict
        // Parameter 1: Input:  ComboBox Source1
        // Parameter 2: Input:  ComboBox Source2
        // If selected index of the source 2 is same with source 1,
        // Set the index of the source 2 different from source 1.

        public void CheckChannelConflict(ComboBox Source1, ref ComboBox Source2)
        {
            try
            {
                if (Source1.SelectedIndex != -1 && Source2.SelectedIndex != -1)
                {
                    if (Source2.SelectedIndex == Source1.SelectedIndex)
                    {
                        if (Source2.SelectedIndex < 9)
                        {
                            Source1.SelectedIndex = Source2.SelectedIndex + 1;
                        }
                        else
                        {
                            Source1.SelectedIndex = Source2.SelectedIndex - 1;
                        }
                        throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Specified channel must be different from another channel."));
                    }
                }
            }
            catch (WGFMULibException we)
            {
                throw we;
            }
            catch (Exception e)
            {
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in CheckChannelConflict:" + e.GetType().FullName));
            }
        }
        #endregion

        #region SetWgfmuMeasMode: Set measurement mode by the operationmode.
        // Method Name: SetWgfmuMeasRange
        // Parameter 1: Input:  ComboBox Operation Mode
        //                      ComboBox.Index = 0: Fast I/V
        //                      ComboBox.Index = 1: PGU
        //
        // Parameter 2: Output: ListBox Measurement Mode
        //              If oeration mode is "Fast I/V",
        //                      ComboBox.Index = 0: V Measurement
        //                      ComboBox.Index = 1: I Measurement
        //              If oeration mode is "PGU",
        //                      ComboBox.Index = 0: V Measurement
                 

        public void SetWgfmuMeasMode(
                                    ComboBox OperationMode,
                                    ref ComboBox MeasurementMode)
        {
            try
            {
                MeasurementMode.Items.Clear(); // Initilize the ComboBox

                switch (OperationMode.SelectedIndex)
                {
                    case 0: // Add items for voltage measurement
                        MeasurementMode.Items.Add("V Measurement");
                        MeasurementMode.Items.Add("I Measurement");
                        MeasurementMode.SelectedIndex = 0;         // Set default index
                        break;
                    case 1:
                        MeasurementMode.Items.Add("V Measurement");
                        MeasurementMode.SelectedIndex = 0;         // Set default index
                        break;
                }
            }
            catch (Exception e)
            {
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in SetWgfmuMeasMode: " + e.GetType().FullName));
            }

        }
        #endregion

        #region SetWgfmuMeasRange: Set measurement range by measurement mode.
        // Method Name: SetWgfmuMeasRange
        // Parameter 1: Input:  ComboBox MeasurementMode
        //                      ComboBox.Index = 1: IM
        //                      ComboBox.Index = 0: VM
        //
        // Parameter 2: Output: ListBox Measurement Range
        //              If measurement mode is IM,
        //                      ComboBox.Index = 0: +/- 1 uA
        //                      ComboBox.Index = 1: +/- 10 uA
        //                      ComboBox.Index = 2: +/- 100 uA      
        //                      ComboBox.Index = 3: +/- 1 mA     
        //                      ComboBox.Index = 4: +/- 10 mA   
        //              If measurement mode is VM,
        //                      ComboBox.Index = 0: +/- 5V
        //                      ComboBox.Index = 1: 0 V to +10 V
        //                      ComboBox.Index = 2: -10 V to 0 V                        
 
        public void SetWgfmuMeasRange(
                                    ComboBox MeasurementMode,
                                    ComboBox OperationMode,
                                    ref ComboBox MeasurementRange)
        {
            try
            {
                MeasurementRange.Items.Clear(); // Initilize the ComboBox

                switch (MeasurementMode.SelectedIndex)
                {
                    case 0:
                        switch (OperationMode.SelectedIndex)
                        {
                            case 0:
                                MeasurementRange.Items.Add("+/- 5 V");
                                MeasurementRange.Items.Add("+/-10 V");
                                MeasurementRange.SelectedIndex = 0;         // Set default index
                                break;
                            case 1:
                                MeasurementRange.Items.Add("+/- 5 V");
                                MeasurementRange.SelectedIndex = 0;         // Set default index
                                break;
                        }
                        break;
                    case 1:
                        MeasurementRange.Items.Add("+/- 1 uA");
                        MeasurementRange.Items.Add("+/- 10 uA");
                        MeasurementRange.Items.Add("+/- 100 uA");
                        MeasurementRange.Items.Add("+/- 1 mA");
                        MeasurementRange.Items.Add("+/- 10 mA");
                        MeasurementRange.SelectedIndex = 0;         // Set default index
                        break;
                }
            }
            catch( Exception e)
            {
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in SetWgfmuMeasRange: " + e.GetType().FullName));
            }

        }
        #endregion

        #region SetWgfmuForceRange: Set force range by operation mode.
        // Method Name: SetWgfmuForceRange
        // Parameter 1: Input:  ComboBox MeasurementMode
        //                      ComboBox.Index = 0: Fast IV
        //                      ComboBox.Index = 1: PGU
        //
        // Parameter 2: Output: ListBox Measurement Range
        //              If measurement mode is Fast IV,
        //                      ComboBox.Index = 0:Auto
        //                      ComboBox.Index = 1:+/-3 V
        //                      ComboBox.Index = 2:+/-5 V
        //                      ComboBox.Index = 3:-10 V to 0 V
        //                      ComboBox.Index = 4:0 V to +10 V
        //              If measurement mode is PGU, 
        //                      ComboBox.Index = 0:Auto
        //                      ComboBox.Index = 1:+/-3 V
        //                      ComboBox.Index = 2:+/-5 V
        //
        public void SetWgfmuForceRange(
                                    ComboBox OperationMode,
                                    ref ComboBox ForceRange)
        {

            try
            {
                ForceRange.Items.Clear(); // Initilize the list box

                switch (OperationMode.SelectedIndex)
                {
                    case 0: // Add items of voltage forcing for fast I/V mode
                        ForceRange.Items.Add("Auto");
                        ForceRange.Items.Add("+/- 3 V");
                        ForceRange.Items.Add("+/- 5 V");
                        ForceRange.Items.Add("-10 V to 0 V");
                        ForceRange.Items.Add("0 V to +10 V");
                        ForceRange.SelectedIndex = 0;         // Set default index
                        break;

                    case 1: // Add items of voltage forcing for PGU mode
                        ForceRange.Items.Add("Auto");
                        ForceRange.Items.Add("+/- 3 V");
                        ForceRange.Items.Add("+/- 5 V");
                        ForceRange.SelectedIndex = 0;         // Set default index
                        break;
                }          
            }
            catch (Exception e)
            {
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in SetWgfmuForceRange: " + e.GetType().FullName));          
            }

        }
        #endregion

        #region Check format of the input paramter

        #region  for integer
        public void CheckInt(string Value)
        {
            int i;
            try
            {
                i = int.Parse(Value);
            }
            catch (FormatException)
            {
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Input value must be an integer."));
            }
        }   
        #endregion

        #region  for double
        public void CheckDouble(string Value)
        {
            double r;
            try
            {
                r = double.Parse(Value);
            }
            catch (FormatException)
            {
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Input value must be an double."));
            }

        }
        #endregion


        #endregion

        #region GetSaveFileName()
        public void  GetSaveFileName(string FileType, out string FileName )
        {

            SaveFileDialog sfd = new SaveFileDialog();
            FileInfo fi;

            try
            {
                if (SaveFileInitialDirectory == "")
                {
                    SaveFileInitialDirectory = Application.StartupPath;
                }
                sfd.InitialDirectory = SaveFileInitialDirectory;

                if (FileType == "CONFIG")
                {
                    sfd.Filter = "Config File(*.cf)|*.cf|All Files(*.*)|*.*";
                }
                else if (FileType == "DATA")
                {
                    sfd.Filter = "Data File(*.csv)|*.csv|All Files(*.*)|*.*";
                }
                else if (FileType == "TEXT")
                {
                    sfd.Filter = "CSV File(*.csv)|*.csv|All Files(*.*)|*.*";
                }
                else
                {
                    sfd.Filter = "All Files(*.*)|*.*";               
                }
     
                sfd.FilterIndex = 1;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileName = sfd.FileName;
                    fi = new FileInfo(FileName);
                    SaveFileInitialDirectory = fi.Directory.ToString();
                    
                }
                else
                {
                    FileName = "";               
                }

            }
            catch (Exception e)
            {
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in GetSaveFileName: " + e.GetType().FullName));
            }
        }
        #endregion

        #region GetLoadFileName()
        public void GetLoadFileName(out string FileName)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            FileInfo fi;

            try
            {
                if (SaveFileInitialDirectory == "")
                {
                    SaveFileInitialDirectory = Application.StartupPath;

                }

                ofd.InitialDirectory = SaveFileInitialDirectory;

                ofd.Filter = "Config File(*.cf)|*.cf|DATA File(*.csv)|*.csv|All Files(*.*)|*.*";

                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileName = ofd.FileName;
                    fi = new FileInfo(FileName);
                    SaveFileInitialDirectory = fi.Directory.ToString();
                    
                }
                else
                {
                    FileName = "";
                }
            }
            catch (Exception e)
            {
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in GetSaveFileName: " + e.GetType().FullName));
            }
        }
        #endregion

        #region UpdateConfigInfo
        public void UpdateConfiginfo()
        {
            WGFMU_CONFIG wgcnfg = new WGFMU_CONFIG();
            WgfmuVisaId = wgcnfg.VISANAME;
            WgfmuGpibAddress = wgcnfg.GPIBADDRESS;
        
        }

        #endregion

        #region SetBiasSourceCh
        public void SetBiasSourceCh(ComboBox SourceType, ref ComboBox Ch, ref TextBox Compliance)
        {
            switch (SourceType.SelectedIndex)
            {
                case 0:
                    Ch.Items.Clear();
                    Ch.Items.Add("N/A");
                    Ch.SelectedIndex = 0;
                    break;
                case 1:
                    Ch.Items.Clear();
                    Ch.Items.Add("101");
                    Ch.Items.Add("102");
                    Ch.Items.Add("201");
                    Ch.Items.Add("202");
                    Ch.Items.Add("301");
                    Ch.Items.Add("302");
                    Ch.Items.Add("401");
                    Ch.Items.Add("402");
                    Ch.Items.Add("501");
                    Ch.Items.Add("502");
                    Ch.Items.Add("601");
                    Ch.Items.Add("602");
                    Ch.Items.Add("701");
                    Ch.Items.Add("702");
                    Ch.Items.Add("801");
                    Ch.Items.Add("802");
                    Ch.Items.Add("901");
                    Ch.Items.Add("902");
                    Ch.Items.Add("1001");
                    Ch.Items.Add("1002");
                    Ch.SelectedIndex = 0;

                    Compliance.Text = "100E-6";
                    Compliance.Enabled = false;

                    break;
                case 2:
                    Ch.Items.Clear();
                    Ch.Items.Add("1");
                    Ch.Items.Add("2");
                    Ch.Items.Add("3");
                    Ch.Items.Add("4");
                    Ch.Items.Add("5");
                    Ch.Items.Add("6");
                    Ch.Items.Add("7");
                    Ch.Items.Add("8");
                    Ch.Items.Add("9");
                    Ch.Items.Add("10");

                    Ch.SelectedIndex = 0;

                    Compliance.Text = "100E-6";
                    Compliance.Enabled = true;
                    break;
            }
        }
        #endregion

        #region CheckBiasSourceChConflict
        public void CheckBiasSourceChConflict(
            ComboBox Source1Type,
            ref ComboBox Source1Ch,
            ComboBox Source2Type,
            ComboBox Source2Ch,
            ComboBox WGFMUCh1,
            ComboBox WGFMUCh2
            )
        {

            int Conflict  = 0;
            int i;
            switch (Source1Type.SelectedIndex)
            { 
            
                case 0:
                    break;
                case 1:
                    switch (Source2Type.SelectedIndex)
                    {
                        case 0:
                            if (Source1Ch.SelectedIndex == WGFMUCh1.SelectedIndex)
                            {
                                Conflict = 1;
                            }
                            else if (Source1Ch.SelectedIndex == WGFMUCh2.SelectedIndex)
                            {
                                Conflict = 1;
                            }

                            if (Conflict == 1)
                            {
                                for (i = 0; i < 19; i++)
                                {
                                    if (i != WGFMUCh1.SelectedIndex &&
                                        i != WGFMUCh2.SelectedIndex)
                                    {
                                        Source1Ch.SelectedIndex = i;
                                        break;
                                    }
                                }
                                throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "Specified chunnel conflitcs with other channels"));
                            }
                            break;
                        case 1:
                            if (Source1Ch.SelectedIndex == Source2Ch.SelectedIndex)
                            {
                                Conflict = 1;
                            }
                            else if (Source1Ch.SelectedIndex == WGFMUCh1.SelectedIndex)
                            {
                                Conflict = 1;
                            }
                            else if (Source1Ch.SelectedIndex == WGFMUCh2.SelectedIndex)
                            {
                                Conflict = 1;
                            }

                            if (Conflict == 1)
                            {
                                for (i = 0; i < 19; i++)
                                {
                                    if (i != Source2Ch.SelectedIndex &&
                                        i != WGFMUCh1.SelectedIndex &&
                                        i != WGFMUCh2.SelectedIndex)
                                    {
                                        Source1Ch.SelectedIndex = i;
                                        break;
                                    }
                                }
                                throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "Specified chunnel conflitcs with other channels"));
                            }
                            break;
                        case 2:
                            if (Source1Ch.SelectedIndex == WGFMUCh1.SelectedIndex)
                            {
                                Conflict = 1;
                            }
                            else if (Source1Ch.SelectedIndex == WGFMUCh2.SelectedIndex)
                            {
                                Conflict = 1;
                            }

                            if (Conflict == 1)
                            {
                                for (i = 0; i < 19; i++)
                                {
                                    if (i != WGFMUCh1.SelectedIndex &&
                                        i != WGFMUCh2.SelectedIndex)
                                    {
                                        Source1Ch.SelectedIndex = i;
                                        break;
                                    }
                                }
                                throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "Specified chunnel conflitcs with other channels"));
                            }
                            break;                            
                     }
                     break;
                case 2:
                    switch (Source2Type.SelectedIndex)
                    {
                        case -1:
                            break;
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            if (Source1Ch.SelectedIndex == Source2Ch.SelectedIndex)
                            {
                                if (Source1Ch.SelectedIndex != 9)
                                {
                                    Source2Ch.SelectedIndex = Source1Ch.SelectedIndex + 1;
                                }
                                else
                                {
                                    Source2Ch.SelectedIndex = Source1Ch.SelectedIndex - 1;
                                }
                                throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "Specified chunnel conflitcs with other channels"));
                            }
                            break;
                    }
                    break;

            }
            
        
        }

        #endregion

        #region Valiation of input value

        public void ValidateInt(TextBox input, int Min, int Max, CancelEventArgs e, ErrorProvider ep)
        {
            int Value;
            try
            {
                Value = int.Parse(input.Text);
                ep.SetError(input, "");

                if (Value < Min)
                {
                    ep.SetError(input, "Input value must be larger than " + Min.ToString("E"));
                    e.Cancel = true;
                }
                else if (Value > Max)
                {
                    ep.SetError(input, "Input value must be smaller than " + Max.ToString("E"));
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                ep.SetError(input, "Not an integer value.");
                e.Cancel = true;
            }
        }

        public void ValidateDouble(TextBox input, double Min, double Max, CancelEventArgs e, ErrorProvider ep)
        {
            double Value;
            try
            {
                Value = double.Parse(input.Text);
                ep.SetError(input, "");

                if (Value < Min)
                {
                    ep.SetError(input, "Input value must be larger than " + Min.ToString("E"));
                    e.Cancel = true;
                }
                else if (Value > Max)
                {
                    ep.SetError(input, "Input value must be smaller than " + Max.ToString("E"));
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                ep.SetError(input, "Not an double value.");
                e.Cancel = true;
            }
        }
        #endregion

        #endregion

        #region Sub functions
        
        public string DoubleToTrimedString(double InputValue)
        {

            string[] SplitString;
            string Number;

            if (InputValue != 0.0)
            {
                Number = InputValue.ToString("0.000000000000000E+00");


                SplitString = Number.Split('E');

                Number = SplitString[0].Trim('0');

                Number = Number + "E" + SplitString[1];
            }
            else
            {
                Number = "0.0E+00";
            }
            return Number;

        }

        public void TimerStart()
        {
            RecordedTime = 0.0;
            ExecTimer.Reset();
            ExecTimer.Start();

            
        }

        public double TimerLap()
        {
            double Time0;
            double LapTime;

            ExecTimer.Stop();

            Time0 = (double) ExecTimer.ElapsedTicks / (double) Stopwatch.Frequency;

            LapTime = Time0 - RecordedTime;
            
            RecordedTime = Time0;

            ExecTimer.Start();
         

            return LapTime;
            
        }

        public void TimerStop()
        {
            ExecTimer.Stop();
            ExecTimer.Reset();
        
        }
        #endregion

        #region Save and restore setting

        #region SaveSettings

        public void SaveSettings(Control ApplicationForm, string ApplicationName, string FileName)
        {
            int i = 0;
            int j = 0;
            Control Cont = new Control();

            ComboBox cmb = new ComboBox();
            RadioButton rdb = new RadioButton();
            TextBox txtb = new TextBox();
            CheckBox chkb = new CheckBox();
            DataGridView dgv = new DataGridView();

            try
            {

                StreamWriter sw = new StreamWriter(FileName);
                try
                {
                    sw.WriteLine("<Application>");
                    sw.WriteLine(ApplicationName);
                    sw.WriteLine("</Application>");

                    Cont = ApplicationForm.GetNextControl(ApplicationForm, true);
                    
                    sw.WriteLine("<Setting>");

                    while ((Cont = ApplicationForm.GetNextControl(Cont, true)) != null)
                    {

                        if (Cont.GetType() == txtb.GetType())
                        {
                            sw.WriteLine(Cont.Name.ToString() + ":" + Cont.Text.Trim());

                        }
                        else if (Cont.GetType() == cmb.GetType())
                        {
                            cmb = (ComboBox)Cont;
                            if (cmb.SelectedItem != null)
                            {
                                sw.WriteLine(Cont.Name.ToString() + ":" + cmb.SelectedIndex.ToString() + ":" + cmb.SelectedItem.ToString());
                            }
                            else
                            {
                                sw.WriteLine(Cont.Name.ToString() + ":" + cmb.SelectedIndex.ToString() + ":");                            
                            }
                        }
                        else if (Cont.GetType() == rdb.GetType())
                        {
                            rdb = (RadioButton)Cont;
                            sw.WriteLine(Cont.Name.ToString() + ":" + rdb.Checked.ToString());
                        }
                        else if (Cont.GetType() == chkb.GetType())
                        {
                            chkb = (CheckBox)Cont;
                            sw.WriteLine(Cont.Name.ToString() + ":" + chkb.Checked.ToString());
                        }
                        else if (Cont.GetType() == dgv.GetType())
                        {
                            dgv = (DataGridView)Cont;
                            sw.WriteLine(Cont.Name.ToString() + ":" + dgv.RowCount.ToString());

                            if (ApplicationName == "NBTI")
                            {

                                for (i = 0; i < dgv.RowCount; i++)
                                {
                                    if (dgv[1, i].Value != null)
                                    {
                                        sw.WriteLine(dgv[1, i].Value.ToString());
                                    }
                                    else
                                    {
                                        sw.WriteLine("null");
                                    }
                                }
                            }
                            else
                            {
                                for (i = 0; i < dgv.RowCount; i++)
                                {
                                    if (dgv[0, i].Value != null)
                                    {
                                        for (j = 0; j < dgv.ColumnCount; j++)
                                        {
                                            sw.Write(dgv[j, i].Value.ToString());
                                            sw.Write(",");
                                        }
                                        sw.WriteLine("");
                                    }
                                    else
                                    {
                                        for (j = 0; j < dgv.ColumnCount; j++)
                                        {
                                            sw.Write("null");
                                            sw.Write(",");
                                        }
                                        sw.WriteLine("");
 
                                    }
                                }
                            
                            }
                        }
                    }

                    sw.WriteLine("</Setting>");
                    
                }
                catch (Exception e)
                {
                    sw.Close();
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in SaveSettings: " + e.GetType().FullName + ":" + e.Message));
                }
                sw.Close();
            }
            catch (Exception e)
            {

                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in SaveSettings: " + e.GetType().FullName + ":" + e.Message));

            }

        }

         #endregion

        #region RestoreSettings
        public void RestoreSettings(Control ApplicationForm, string ApplicationName, string FileName)
        {
            string OneLine = "";
            string[] SplitBuffer;
            string ControlName = "";
            Control FindedControl = new Control();

            ComboBox cmb = new ComboBox();
            RadioButton rdb = new RadioButton();
            TextBox txtb = new TextBox();
            CheckBox chkb = new CheckBox();
            DataGridView dgv = new DataGridView();

            int RowCount = 0;
            int i = 0;
            int j = 0;

            try 
            {
                StreamReader sr = new StreamReader(FileName);

                try
                {
                    while (((OneLine = sr.ReadLine()) != null) && (OneLine != "<Setting>"))
                    { 
                    
                    }

                    while (((OneLine = sr.ReadLine()) != null) && (OneLine != "</Setting>"))
                    {
                        SplitBuffer = OneLine.Split(':');
                        ControlName = SplitBuffer[0];
                        FindedControl = FindControl(ApplicationForm, ControlName);
                        if (FindedControl != null)
                        {
                            if (FindedControl.GetType() == txtb.GetType())
                            {
                                txtb = (TextBox)FindedControl;
                                txtb.Text = SplitBuffer[1];
                            }
                            else if (FindedControl.GetType() == cmb.GetType())
                            {
                                cmb = (ComboBox)FindedControl;
                                cmb.SelectedIndex = int.Parse(SplitBuffer[1].Trim());

                            }
                            else if (FindedControl.GetType() == rdb.GetType())
                            {
                                rdb = (RadioButton)FindedControl;
                                if (SplitBuffer[1].Trim() == "True")
                                {
                                    rdb.Checked = true;
                                }
                                else
                                {
                                    rdb.Checked = false;
                                }

                            }
                            else if (FindedControl.GetType() == chkb.GetType())
                            {
                                chkb = (CheckBox)FindedControl;
                                if (SplitBuffer[1].Trim() == "True")
                                {
                                    chkb.Checked = true;
                                }
                                else
                                {
                                    chkb.Checked = false;
                                }
                            }
                            else if (FindedControl.GetType() == dgv.GetType())
                            {
                                dgv = (DataGridView)FindedControl;
                                RowCount = int.Parse(SplitBuffer[1].Trim());

                                if (ApplicationName == "NBTI")
                                {
                                    for (i = 0; i < RowCount; i++)
                                    {
                                        OneLine = sr.ReadLine();
                                        if (OneLine.Trim() != "null")
                                        {
                                            dgv[1, i].Value = OneLine.Trim();
                                        }
                                        else
                                        {
                                            dgv[1, i].Value = null;
                                        }

                                    }
                                }
                                else
                                {
                                    dgv.RowCount = RowCount;
                                    for (i = 0; i < RowCount; i++)
                                    {
                                        OneLine = sr.ReadLine();
                                        SplitBuffer = OneLine.Split(',');

                                        for (j = 0; j < SplitBuffer.Length - 1; j++)
                                        {
                                            if (SplitBuffer[j].Trim() != "null")
                                            {
                                                dgv[j, i].Value = SplitBuffer[j].Trim();
                                            }
                                            else
                                            {
                                                dgv[j, i].Value = null;
                                            }
                                        }
                                        

                                    }
                                
                                }
                            }
                            else
                            {
                                sr.Close();
                                //throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Specified control is not found."));
                            }
                        }
                    }
                    sr.Close();
                }
                catch (Exception ex)
                {
                    sr.Close();
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in RestoreSettings: " + ex.GetType().FullName + ": " + ex.Message.ToString()));
                }

            }
            catch (Exception ex)
            {
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in RestoreSettings: " + ex.GetType().FullName + ": " + ex.Message.ToString()));
            }
        }

        private Control FindControl(Control ApplicationForm, string ControlName)
        {
            foreach (Control Cont in ApplicationForm.Controls)
            {
                if (Cont.HasChildren)
                {
                    Control cFindControl = FindControl(Cont, ControlName);

                    if (cFindControl != null)
                    {
                        return cFindControl;
                    }
                
                }
                if (Cont.Name == ControlName)
                {
                    return Cont;
                }
            }
            return null;
        }

        #region Check ApplicationType
        
        public void CheckApplicationType(string ApplicationName, string FileName)
        {
            string HeadderToFind = "<Application>";
            string OneLine = "";
            int FindApplicationSection = 0;
            
            try 
            {
                StreamReader sr = new StreamReader(FileName);
                while (((OneLine = sr.ReadLine()) != null) )
                {
                    if (OneLine.Trim() == HeadderToFind)
                    {
                        FindApplicationSection = 1;
                        break;
                    }
                }
                
                if (FindApplicationSection != 1)
                {
                    sr.Close();
                    throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "Application section was not found."));
                }
                else
                {
                    if ((OneLine = sr.ReadLine()) != null)
                    {
                        if (OneLine != ApplicationName)
                        {
                            sr.Close();
                            throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "Application mismatch. The specified file is not for this application."));                              
                        }
                    }
                    else
                    {
                        sr.Close();
                        throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Application section was not found."));                    
                    }
                }
                sr.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #endregion

        #region ClearSettings

        public void ClearSettings(Control ApplicationFrom)
        {

            try
            {
                ClearControlSettings(ApplicationFrom);
            }
            catch (Exception e)
            {

                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, "Error in SaveSettings: " + e.GetType().FullName));

            }

        }

        private Control ClearControlSettings(Control ApplicationForm)
        {
            int i;
            ComboBox cmb = new ComboBox();
            RadioButton rdb = new RadioButton();
            TextBox txtb = new TextBox();
            CheckBox chkb = new CheckBox();
            DataGridView dgv = new DataGridView();

            try
            {

                foreach (Control Cont in ApplicationForm.Controls)
                {
                    if (Cont.HasChildren)
                    {
                        Control cClearControlSettings = ClearControlSettings(Cont);

                        if (cClearControlSettings != null)
                        {
                            return cClearControlSettings;
                        }
                    }

                    if (Cont.GetType() == txtb.GetType())
                    {
                        Cont.Text = "0";

                    }
                    else if (Cont.GetType() == cmb.GetType())
                    {
                        cmb = (ComboBox)Cont;
                        cmb.SelectedIndex = -1;
                    }
                    else if (Cont.GetType() == rdb.GetType())
                    {
                        rdb = (RadioButton)Cont;
                        rdb.Checked = false;
                    }
                    else if (Cont.GetType() == chkb.GetType())
                    {
                        chkb = (CheckBox)Cont;
                        chkb.Checked = false;
                    }
                    else if (Cont.GetType() == dgv.GetType())
                    {
                        dgv = (DataGridView)Cont;
                        for (i = 0; i < dgv.RowCount; i++)
                        {
                            dgv[1, i].Value = null;
                        }
                    }

                }
                return null;
            }
            catch (Exception e)
            {
                throw e;

            }



        }

        #endregion

        #endregion

        #region Session Operation

        #region Init()
        public void Init()
        {
            #region Declaration of generic parameters

            int rc = 0;
            int SessionStatus = 0;
            StringBuilder LogFile = new StringBuilder();

            #endregion

            #region Declaration of method dedicated parameters
            string FunctionName = "Init()";

            int ChIdSize = 0;
            string Readout;
            string GpibDescription = this.VisaId + "::" + this.GpibAddress.ToString() + "::INSTR";

 
            #endregion

            try
            {
                #region Body of method

                if (OnLine == 1)
                {
                    // Dummy to ensure the clos of the GPIB session
                    rc = WGFMU.closeSession();

                    // Open WGFMU session
                    rc = WGFMU.openSession(GpibDescription);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }

                    SessionStatus = 1;


                    rc = WGFMU.initialize();
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }

                    rc = WGFMU.getChannelIdSize(ref ChIdSize);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }

                    ChannelIdList = new int[ChIdSize];

                    rc = WGFMU.getChannelIds(ChannelIdList, ref ChIdSize);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }

                    gpib.Open(GpibDescription);

                    gpib.sendCmd("UNT?");

                    gpib.queryStr(out Readout);

                    gpib.ErrorCheck();

                    gpib.Close();

                    ModuleList = Readout.Split(';');

                    rc = WGFMU.initialize();
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                    // Clear WGFMU patterns and sequence information
                }

                rc = WGFMU.clear();
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

     

                // Enable log 
                if (LogEnable == 1)
                {
                    rc = WGFMU.openLogFile(LogFileName);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }
                
                #endregion

            }
            catch (WGFMULibException we)
            {
                if (SessionStatus == 1)
                {
                    WGFMU.closeSession();
                }
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        }
        #endregion

        #region Close
        public void Close()
        {
            #region Declaration of generic parameters

            int rc = 0;

            #endregion

            #region Declaration of method dedicated parameters

            #endregion

            #region Body of method

            // Enable log 
            if (LogEnable == 1)
            {
                rc = WGFMU.closeLogFile();
            }

            rc = WGFMU.clear();
            rc = WGFMU.initialize();
            rc = WGFMU.closeSession();

            #endregion

        }
        #endregion

        #region SelfCalibration
        public void SelfCalibration(ref int Result, out string Message, ref int Size)
        {
            #region Declaration of generic parameters

            int rc = 0;
            int SessionStatus = 0;
            StringBuilder LogFile = new StringBuilder();

            #endregion

            #region Declaration of method dedicated parameters
            string FunctionName = "Selfcalibration()";

            StringBuilder Detail = new StringBuilder(257);

            #endregion

            try
            {
                #region Body of method

                rc = WGFMU.doSelfCalibration(ref Result, Detail, ref Size);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                Message = Detail.ToString();

                #endregion

            }
            catch (WGFMULibException we)
            {
                if (SessionStatus == 1)
                {
                    WGFMU.closeSession();
                }
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        }
        #endregion

        #region Selftest
        public void SelfTest(ref int Result, out string Message, ref int Size)
        {
            #region Declaration of generic parameters

            int rc = 0;
            int SessionStatus = 0;
            StringBuilder LogFile = new StringBuilder();

            #endregion

            #region Declaration of method dedicated parameters
            string FunctionName = "Selftest()";

            StringBuilder Detail = new StringBuilder(257);

            #endregion

            try
            {
                #region Body of method

                rc = WGFMU.doSelfTest(ref Result, Detail, ref Size);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                Message = Detail.ToString();

                #endregion

            }
            catch (WGFMULibException we)
            {
                if (SessionStatus == 1)
                {
                    WGFMU.closeSession();
                }
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        }
        #endregion

        #endregion

        #region Instrument Settings

        #region SetupChannel
        public void SetupChannel(   int ChNumber, 
                                    int OperationMode, 
                                    int ForceRange,
                                    int MeasurementMode, 
                                    int IMeasurementRange,
                                    int VMeasurementRange,
                                    int MeasurementEnable,
                                    double HwSkew)
        {
            #region Declaration of generic parameters

            int rc = 0;

            #endregion

            #region Declaration of method dedicated parameters

            StringBuilder GpibDescription = new StringBuilder();
            string FunctionName = "SetupChannel()";

            #endregion   
         
            try
            {
                #region Body of method

                // Set operation mode
                rc = WGFMU.setOperationMode(ChNumber, OperationMode);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                // Set force range
                rc = WGFMU.setForceVoltageRange(ChNumber, ForceRange);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                // Set measurement mode
                rc = WGFMU.setMeasureMode(ChNumber, MeasurementMode);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                // Set measurement range

                rc = WGFMU.setMeasureCurrentRange(ChNumber, IMeasurementRange);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                if (MeasurementMode == WGFMU.MEASURE_MODE_VOLTAGE)
                {
                    rc = WGFMU.setMeasureVoltageRange(ChNumber, VMeasurementRange);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }
            

                // Set HW skew
                rc = WGFMU.setForceDelay(ChNumber, HwSkew);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                rc = WGFMU.setMeasureDelay(ChNumber, HwSkew);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }


                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }
        #endregion

        #region ConnectChannel
        public void ConnectChannesl(int ChNum, int Status)
        {
            #region Declaration of generic parameters

            int rc = 0;

            #endregion

            #region Declaration of method dedicated parameters
            string FunctionName = "ConnectChannel()";
            #endregion

            try
            {
                #region input parameter check

                if (!(Status == 0 || Status == 1 || Status == 2))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Status must be 0 or 1"));
                }

                #endregion

                #region Body of method
                
                // Connect or disconnect specified channel
                
                if (Status == 1)
                {
                    //rc = WGFMU.setRSUSlectorPath(ChNum, 0);
                    rc = WGFMU.connect(ChNum);
                }
                else if (Status == 0)
                {
                    //rc = WGFMU.setRSUSlectorPath(ChNum, 1);
                    rc = WGFMU.disconnect(ChNum);
                }

                else if (Status == 2)
                {
                    //rc = WGFMU.setRSUSlectorPath(ChNum, 1);
                }
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }    
                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            } 
        
        }
        
        #endregion 

        #region UpdateCahannel
        public void UpdateChannel(int ChNum)
        {
            #region Declaration of generic parameters

            int rc = 0;

            #endregion

            #region Declaration of method dedicated parameters

            string FunctionName = "UpdateChannel()";

            #endregion

            try
            {
                #region input parameter check


                #endregion

                #region Body of method

                rc = WGFMU.updateChannel(ChNum);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }    

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion 

        
        #endregion

        #region Execution & data retrieving results

        #region ExecuteMeasurement
        public void ExecuteMeasurement()
        {
            #region Declaration of generic parameters

            int rc = 0;

            #endregion

            #region Declaration of method dedicated parameters
            string FunctionName = "ExecuteMeasurement()";
            #endregion

            try
            {
                #region input parameter check


                #endregion

                #region Body of method

                rc = WGFMU.execute();
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        }

        #endregion


        #region Retrieve results based on "Channel"


            #region Add Data to the Datagrid Vew
            private void AddDataToDatagridView(DATAVIEW dtv, int ChIndex, int Index, int Size, ref double[] Time, ref double[] MeasuredData)
            {
                int i;
                string FunctionName = "AddDataToDatagridView()";

                try
                {
                    for (i = Index; i < Index + Size; i++)
                    {
                        dtv.dataGridViewResult[ChIndex * 2, i].Value = DoubleToTrimedString(Time[i]);
                        dtv.dataGridViewResult[ChIndex * 2 + 1, i].Value = MeasuredData[i].ToString("E");
                    }

                    System.Windows.Forms.Application.DoEvents();
                    System.Windows.Forms.Application.DoEvents();
                }
                catch (WGFMULibException we)
                {
                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);
                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    this.Close();
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    this.Close();
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }
            }

            #endregion

            #region UpdateChannnelResultsFile()

            private void UpdateChannelResultsFile(string FileName, int Index, int Size, ref double[] Time, ref double[] MeasuredData)
            {
                int i;
                string OutputString;
                FileStream fs;
                StreamWriter sw;

                string FunctionName = "UpdateChannnelResultsFile()";

                try
                {
                    if (Index == 0)
                    {
                        fs = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    }
                    else
                    {
                        fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    }

                    sw = new StreamWriter(fs);

                    for (i = Index; i < Index + Size; i++)
                    {
                        OutputString = DoubleToTrimedString(Time[i]) + "," + MeasuredData[i].ToString("E");
                        sw.WriteLine(OutputString);
                    }

                    sw.Close();
                    fs.Close();
                }
                catch (WGFMULibException we)
                {
                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);
                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    this.Close();
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    this.Close();
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }
            }

            #endregion

            #region AddResultsToTestResultFile
            private void AddResultstoTestResultFile(String FileName,
                                            ref int[] ChannelList,
                                            ref int[] MeasuredDataSize,
                                            ref double[][] Time,
                                            ref double[][] MeasuredData)
            {
                int MaxIndex = 0;
                int i, j;
                string OutputString = "";
                FileStream fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                string FunctionName = "SaveResultsToFile()";

                try
                {
                    for (i = 0; i < MeasuredDataSize.Length; i++)
                    {
                        if (MeasuredDataSize[i] > MaxIndex)
                        {
                            MaxIndex = MeasuredDataSize[i];
                        }
                    }

                    sw.WriteLine("<Results>");

                    OutputString = "";
                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        OutputString = OutputString
                                    + ChannelList[i].ToString() + " Time (s),"
                                    + ChannelList[i].ToString() + " Value,";
                    }

                    sw.WriteLine(OutputString);

                    for (i = 0; i < MaxIndex; i++)
                    {
                        OutputString = "";
                        for (j = 0; j < ChannelList.Length; j++)
                        {
                            if (i < Time[j].Length)
                            {
                                OutputString = OutputString
                                            + DoubleToTrimedString(Time[j][i]) + ","
                                            + MeasuredData[j][i].ToString("E") + ",";
                            }
                            else
                            {
                                OutputString = OutputString + "0,0,";
                            }
                        }
                        sw.WriteLine(OutputString);
                    }

                    sw.WriteLine("</Results>");

                    sw.Close();
                    fs.Close();
                }
                catch (WGFMULibException we)
                {
                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);

                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    this.Close();
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    this.Close();
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }
            }
            #endregion

            #region Retrieve Results By Channel In Realtime

            #region GetTimeInfo()
            public void GetTimeInfo(ref double ElapsedTime, ref double TotalTime)
            {

                int rc = 0;
                int Status = 0;
                string FunctionName = "GetTimeInfo()";
                try
                {
                    rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }
                catch (WGFMULibException we)
                {
                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);

                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }

            }
            #endregion RetrieveResultsByChannelRealTime

            #region RetrieveResultsByChannelRealTime
            public void RetrieveResultsByChannelRealTime(ref int[] ChannelList, string FileName)
            {
                #region Declaration of generic parameters

                int rc = 0;
                string FunctionName = "RetrieveResultsByChannelRealTime";

                #endregion

                #region Declaration of method dedicated parameters
                WGFMU_PROGRESS wgpg = new WGFMU_PROGRESS();
                int i;
                int j;
                int[] TimeSize = new int[ChannelList.Length];
                double[][] TimeData = new double[ChannelList.Length][];
                double[][] MeasuredData = new double[ChannelList.Length][];
                int[] DataIndex = new int[ChannelList.Length];
                int[] ChStatus = new int[ChannelList.Length];
                int[] RemainDataSize = new int[ChannelList.Length];
                int[] PrevRemainDataSize = new int[ChannelList.Length];
                int[] MeasuredDataSize = new int[ChannelList.Length];
                string[] TempFileName = new string[ChannelList.Length];
                int[] SaveIndex = new int[ChannelList.Length];
                int Status;
                int DataStatus;
                double ElapsedTime;
                double TotalTime;
                int MeasSize;
                int TotalSize;
                int ReadoutCount;
                int RetInterval;
                string DirectoryInfo;
                string SaveFileName;
                string[] SplitBuffer;
                DATAVIEW[] dtv = new DATAVIEW[ChannelList.Length];
                int MaxIndex = 0;
                const int INDEX_MAX = 20000;

                System.IO.FileInfo FileInfo = new FileInfo(FileName);

                DirectoryInfo = FileInfo.DirectoryName;
                SaveFileName = FileInfo.Name;
                SplitBuffer = SaveFileName.Split('.');

                for (i = 0; i < ChannelList.Length; i++)
                {
                    DataIndex[i] = 0;
                    ChStatus[i] = 0;
                    RemainDataSize[i] = 0;
                    PrevRemainDataSize[i] = 0;
                    MeasuredDataSize[i] = 0;
                    TempFileName[i] = DirectoryInfo + @"\" + SplitBuffer[0] + "_Channel" + ChannelList[i].ToString() + ".csv";
                    SaveIndex[i] = 0;

                }

                ReadoutCount = 0;

                RetInterval = 500;          // wait 500 msec

                #endregion

                try
                {
                    #region input parameter check

                    #endregion

                    #region Body of method


                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        rc = WGFMU.getMeasureTimeSize(ChannelList[i], ref TimeSize[i]);
                        if (rc < WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                        TimeData[i] = new double[TimeSize[i]];
                        MeasuredData[i] = new double[TimeSize[i]];
                        if (MaxIndex < TimeSize[i])
                        {
                            MaxIndex = TimeSize[i];
                        }
                        dtv[i] = new DATAVIEW();
                        dtv[i].dataGridViewResult.ColumnCount = 2;
                    }

                    if (MaxIndex <= INDEX_MAX)
                    {
                        for (i = 0; i < ChannelList.Length; i++)
                        {
                            if (TimeSize[i] != 0)
                            {

                                dtv[i].dataGridViewResult.Columns[0].HeaderText = ChannelList[i].ToString() + " Time (s)";
                                dtv[i].dataGridViewResult.Columns[1].HeaderText = ChannelList[i].ToString() + " Value";

                                dtv[i].dataGridViewResult.RowCount = MaxIndex;

                                for (j = 0; j < TimeSize[i]; j++)
                                {
                                    dtv[i].dataGridViewResult.Rows[j].HeaderCell.Value = j.ToString();
                                }
                                dtv[i].Show();

                            }

                        }

                    }

                    Application.DoEvents();

                    DataStatus = 0;
                    Status = 0;
                    ElapsedTime = 0.0;
                    TotalTime = 0.0;
                    rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                    wgpg.ElapsedTime = ElapsedTime;
                    wgpg.ExpectedExcecutionTime = TotalTime;
                    wgpg.Show();
                    MeasSize = 0;
                    TotalSize = 0;
                    while (DataStatus != 1 || Status != WGFMU.STATUS_COMPLETED)
                    {
                        Thread.Sleep(RetInterval);
                        rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                        if (rc < WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                        wgpg.ElapsedTime = ElapsedTime;
                        System.Windows.Forms.Application.DoEvents();
                        System.Windows.Forms.Application.DoEvents();
                        if (wgpg.AbortStatus == 1)
                        {
                            wgpg.Close();
                            WGFMU.abort();
                            if (rc < WGFMU.NO_ERROR)
                            {
                                throw (new WGFMULibException(rc, ""));
                            }
                            break;
                        }

                        DataStatus = 0;

                        for (i = 0; i < ChannelList.Length; i++)
                        {
                            rc = WGFMU.getMeasureValueSize(ChannelList[i], ref MeasSize, ref TotalSize);
                            if (rc < WGFMU.NO_ERROR)
                            {
                                throw (new WGFMULibException(rc, ""));
                            }

                            if (TotalSize == 0)
                            {
                                ChStatus[i] = 1;
                            }
                            else
                            {
                                if (MeasuredDataSize[i] < TotalSize) // || RemainDataSize[i] == 0 || RemainDataSize[i] < PrevRemainDataSize[i])
                                {
                                    ReadoutCount = MeasSize - MeasuredDataSize[i];
                                    SaveIndex[i] = DataIndex[i];
                                    for (j = 0; j < ReadoutCount; j++)
                                    {
                                        rc = WGFMU.getMeasureValue(ChannelList[i], DataIndex[i], ref TimeData[i][DataIndex[i]], ref MeasuredData[i][DataIndex[i]]);
                                        if (rc < WGFMU.NO_ERROR)
                                        {
                                            throw (new WGFMULibException(rc, ""));
                                        } 
                                        TimeData[i][DataIndex[i]] = TimeData[i][DataIndex[i]];
                                        DataIndex[i] = DataIndex[i] + 1;
                                    }
                                    MeasuredDataSize[i] = DataIndex[i];
                                    if (MeasuredDataSize[i] == TotalSize)
                                    {
                                        ChStatus[i] = 1;
                                    }
                                    this.UpdateChannelResultsFile(TempFileName[i], SaveIndex[i], ReadoutCount, ref TimeData[i], ref MeasuredData[i]);
                                    if (MaxIndex <= INDEX_MAX)
                                    {
                                        this.AddDataToDatagridView(dtv[i], 0, SaveIndex[i], ReadoutCount, ref TimeData[i], ref MeasuredData[i]);
                                    }
                                }
                                rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                                if (rc < WGFMU.NO_ERROR)
                                {
                                    throw (new WGFMULibException(rc, ""));
                                }
                                wgpg.ElapsedTime = ElapsedTime;

                            }

                            DataStatus = 0;

                            for (j = 0; j < ChannelList.Length; j++)
                            {
                                DataStatus = DataStatus + ChStatus[j];
                            }

                            if (DataStatus == ChannelList.Length)
                            {
                                DataStatus = 1;
                            }
                            else
                            {
                                DataStatus = 0;
                            }
                        }
                    }

                    this.AddResultstoTestResultFile(FileName, ref ChannelList, ref MeasuredDataSize, ref TimeData, ref MeasuredData);

                    wgpg.Close();

                    #endregion
                }
                catch (WGFMULibException we)
                {
                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);

                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }

            }
            #endregion

            #endregion


            #region RetrieveResults
            public void RetrieveResults(ref int[] ChannelList)
            {
                #region Declaration of generic parameters

                int rc = 0;
                string FunctionName = "RetrieveResultsByChannelRealTime";

                #endregion

                #region Declaration of method dedicated parameters

                int i;

                int[] TimeSize = new int[ChannelList.Length];
                double[][] TimeData = new double[ChannelList.Length][];
                double[][] MeasuredData = new double[ChannelList.Length][];
                int[] DataIndex = new int[ChannelList.Length];
                int[] ChStatus = new int[ChannelList.Length];
                int[] RemainDataSize = new int[ChannelList.Length];
                int[] PrevRemainDataSize = new int[ChannelList.Length];
                int[] MeasuredDataSize = new int[ChannelList.Length];
                string[] TempFileName = new string[ChannelList.Length];
                int[] SaveIndex = new int[ChannelList.Length];


                int MaxIndex = 0;

                for (i = 0; i < ChannelList.Length; i++)
                {
                    DataIndex[i] = 0;
                    ChStatus[i] = 0;
                    RemainDataSize[i] = 0;
                    PrevRemainDataSize[i] = 0;
                    MeasuredDataSize[i] = 0;
                    SaveIndex[i] = 0;

                }


                #endregion

                try
                {
                    #region input parameter check

                    #endregion

                    #region Body of method


                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        rc = WGFMU.getMeasureTimeSize(ChannelList[i], ref TimeSize[i]);
                        if (rc < WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                        TimeData[i] = new double[TimeSize[i]];
                        MeasuredData[i] = new double[TimeSize[i]];
                        if (MaxIndex < TimeSize[i])
                        {
                            MaxIndex = TimeSize[i];
                        }
                    }
                    //Debug.WriteLine("Before Wait OPC:" + this.TimerLap());

                    rc = WGFMU.waitUntilCompleted();

                    //Debug.WriteLine("Wait OPC:" + this.TimerLap());

                    
                    
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                    for (i = 0; i < ChannelList.Length; i++)
                    {   //rc = WGFMU.getMeasureValueSize(ChannelList[i], ref MeasuredDataSize[i], ref TotalSize);
                        if (TimeSize[i] >0)
                        {
                            rc = WGFMU.getMeasureValues(ChannelList[i], 0, ref TimeSize[i], TimeData[i], MeasuredData[i]);
                        }

                        //Debug.WriteLine("Get Measure Valuses:" + this.TimerLap());

                        if (rc < WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }
 


                    #endregion
                }
                catch (WGFMULibException we)
                {
                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);

                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }

            }

            #endregion

        #endregion

        #region Retrieve based on "Event"

            #region SaveResultsToFileByEvent()

            private void SavetResultsToFileByEvent(String FileName,
                                                ref int[] ChannelList,
                                                ref string[][] PatternNames,
                                                ref string[][] EventNames,
                                                ref int[][] Cycles,
                                                ref double[][] Loops,
                                                ref int[][] Counts,
                                                ref int[][] Sizes,
                                                ref double[][][] MeasuredTime,
                                                ref double[][][] MeasuredValue)
            {
                FileStream fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                string FunctionName = "SaveEventResultsToFile";
                int i, j, k;
                int EventSize = 0;
                int DataSize = 0;

                string OutputString;
                try
                {
                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        if (EventSize < PatternNames[i].Length)
                        {
                            EventSize = PatternNames[i].Length;
                        }
                        for (j = 0; j < PatternNames[i].Length; j++)
                        {
                            if (DataSize < Sizes[i][j])
                            {
                                DataSize = Sizes[i][j];
                            }
                        }
                    }


                    sw.WriteLine("<Results>");
                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        for (j = 0; j < EventSize; j++)
                        {
                            if (PatternNames[i].Length != 0)
                            {
                                OutputString = EventNames[i][j] + Cycles[i][j].ToString() + Loops[i][j].ToString() + Counts[i][j].ToString() + " Time [s]";
                                sw.Write(OutputString);
                                sw.Write(",");

                                OutputString = EventNames[i][j] + Cycles[i][j].ToString() + Loops[i][j].ToString() + Counts[i][j].ToString() + " Value";
                                sw.Write(OutputString);
                                sw.Write(",");
                            }
                        }
                    }

                    sw.WriteLine("");
                    for (k = 0; k < DataSize; k++)
                    {
                        for (i = 0; i < ChannelList.Length; i++)
                        {
                            for (j = 0; j < EventSize; j++)
                            {
                                if (PatternNames[i].Length != 0)
                                {
                                    if (k < MeasuredTime[i][j].Length)
                                    {
                                        OutputString = this.DoubleToTrimedString(MeasuredTime[i][j][k]);
                                        sw.Write(OutputString);
                                        sw.Write(",");

                                        OutputString = this.DoubleToTrimedString(MeasuredValue[i][j][k]);
                                        sw.Write(OutputString);
                                        sw.Write(",");
                                    }
                                    else
                                    {
                                        OutputString = "";
                                        sw.Write(OutputString);
                                        sw.Write(",");

                                        OutputString = "";
                                        sw.Write(OutputString);
                                        sw.Write(",");                                   
                                    
                                    }
                                }
                            }
                        }
                        sw.WriteLine("");
                    }


                    sw.WriteLine("</Results>");

                    sw.Close();
                    fs.Close();
                }
                catch (WGFMULibException we)
                {
                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);

                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    this.Close();
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    this.Close();
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }
            }

            #endregion

            #region NBTISaveResultsToFileByEvent()

            private void NBTISavetResultsToFileByEvent(String FileName,
                                                ref int[] ChannelList,
                                                ref double [][] PatternTimes,
                                                ref string[][] PatternNames,
                                                ref string[][] EventNames,
                                                ref int[][] Cycles,
                                                ref double[][] Loops,
                                                ref int[][] Counts,
                                                ref int[][] Sizes,
                                                ref double[][][] MeasuredTime,
                                                ref double[][][] MeasuredValue)
            {
                FileStream fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                string FunctionName = "NBTISaveEventResultsToFile";
                int i, j, k;
                int EventSize = 0;
                int DataSize = 0;

                string OutputString;
                try
                {
                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        if (EventSize < PatternNames[i].Length)
                        {
                            EventSize = PatternNames[i].Length;
                        }
                        for (j = 0; j < PatternNames[i].Length; j++)
                        {
                            if (DataSize < Sizes[i][j])
                            {
                                DataSize = Sizes[i][j];
                            }
                        }
                    }


                    sw.WriteLine("<Results>");

                    if (EventSize != 0)
                    {
                        sw.Write(" ");
                        sw.Write(",");

                    }
                    for (i = 0; i < ChannelList.Length; i++)
                    {   
                        for (j = 0; j < EventSize; j++)
                        {
                            if (PatternNames[i].Length != 0)
                            {
                                OutputString = EventNames[i][j] + ":" + Cycles[i][j].ToString() + ":" + Loops[i][j].ToString() + ":" + Counts[i][j].ToString() + " Value";
                                sw.Write(OutputString);
                                sw.Write(",");
                            }
                        }
                    }
                    sw.WriteLine("");

                    if (EventSize != 0)
                    {
                        sw.Write("Time (s)");
                        sw.Write(",");

                    }
                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        for (j = 0; j < EventSize; j++)
                        {
                            
                            if (PatternNames[i].Length != 0)
                            {
                                OutputString = this.DoubleToTrimedString(MeasuredTime[i][j][0]);
                                sw.Write(OutputString);
                                sw.Write(",");
                            }
                        }
                    }
                    sw.WriteLine("");


                    for (k = 0; k < DataSize; k++)
                    {
                        for (i = 0; i < ChannelList.Length; i++)
                        {
                            if (PatternNames[i].Length != 0)
                            {
                                OutputString = this.DoubleToTrimedString(PatternTimes[i][k]);
                                sw.Write(OutputString);
                                sw.Write(",");
                                break;
                            }
                        }
                        for (i = 0; i < ChannelList.Length; i++)
                        {
                            for (j = 0; j < EventSize; j++)
                            {
                                if (PatternNames[i].Length != 0)
                                {
                                    if (k < MeasuredTime[i][j].Length)
                                    {
                                        OutputString = this.DoubleToTrimedString(MeasuredValue[i][j][k]);
                                        sw.Write(OutputString);
                                        sw.Write(",");
                                    }
                                    else
                                    {
                                        OutputString = "";
                                        sw.Write(OutputString);
                                        sw.Write(",");

                                        OutputString = "";
                                        sw.Write(OutputString);
                                        sw.Write(",");

                                    }
                                }
                            }
                        }
                        sw.WriteLine("");
                    }


                    sw.WriteLine("</Results>");

                    sw.Close();
                    fs.Close();
                }
                catch (WGFMULibException we)
                {
                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);

                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    this.Close();
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    this.Close();
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }
            }

            #endregion

            #region GetEventsInfo()

            public void GetEventsInfo(ref int[] ChList,
                    out string[][] PatternNames,
                    out string[][] EventNames,
                    out int[][] Cycles,
                    out double[][] Loops,
                    out int[][] Counts,
                    out int[][] Offsets,
                    out int[][] Sizes)
            {
                #region Declaration of generic parameters

                int rc = 0;

                string FunctionName = "GetEventsInfo";

                #endregion

                #region Declaration of method dedicated parameters
                int i, j;
                int Size = 0;

                PatternNames = new string[ChList.Length][];
                EventNames = new string[ChList.Length][];
                Cycles = new int[ChList.Length][];
                Loops = new double[ChList.Length][];
                Counts = new int[ChList.Length][];
                Offsets = new int[ChList.Length][];
                Sizes = new int[ChList.Length][];

                #endregion

                try
                {
                    for (i = 0; i < ChList.Length; i++)
                    {
                        rc = WGFMU.getMeasureEventSize(ChList[i], ref Size);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }

                        PatternNames[i] = new string[Size];
                        EventNames[i] = new string[Size];

                        for (j = 0; j < Size; j++)
                        {
                            PatternNames[i][j] = new string(' ', PATTERN_NAME_MAX);
                            EventNames[i][j] = new string(' ', PATTERN_NAME_MAX);
                        }

                        Cycles[i] = new int[Size];
                        Loops[i] = new double[Size];
                        Counts[i] = new int[Size];
                        Offsets[i] = new int[Size];
                        Sizes[i] = new int[Size];
                        if (Size > 0)
                        {
                            rc = WGFMU.getMeasureEvents(ChList[i], 0, ref Size,
                                PatternNames[i],
                                EventNames[i],
                                Cycles[i],
                                Loops[i],
                                Counts[i],
                                Offsets[i],
                                Sizes[i]);
                            if (rc != WGFMU.NO_ERROR)
                            {
                                throw (new WGFMULibException(rc, ""));
                            }
                        }
                    }
                }
                catch (WGFMULibException we)
                {
                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);

                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    this.Close();
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    this.Close();
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }

            }

            #endregion

            #region RetrieveResultsByEvent

            public void RetrieveResultsByEvent(ref int[] ChList, string FileName)
            {


                #region Declaration of generic parameters

                int rc = 0;

                string FunctionName = "RetrieveResultsByEvent";

                #endregion

                #region Declaration of method dedicated parameters
                WGFMU_PROGRESS wgpg = new WGFMU_PROGRESS();
                
                DATAVIEW[] dtv = new DATAVIEW[ChList.Length];

                int i, j;
                string Frequency;
                int MaxEventSize = 0;
                int[] MaxDataSize = new int[ChList.Length];
                int[] ColumnCount = new int[ChList.Length];
                string[][] PatternNames;
                string[][] EventNames;
                int[][] Cycles;
                double[][] Loops;
                int[][] Counts;
                int[][] Offsets;
                int[][] Sizes;

                string[][] ChannelResultsFileName = new string[ChList.Length][];

                double[][][] MeasuredValues = new double[ChList.Length][][];
                double[][][] MeasuredTimes = new double[ChList.Length][][];

                string SaveDirectoryName;
                string SaveFileName;
                string SaveFileExtention;

                #endregion

                try
                {
                    System.IO.FileInfo SaveFileInfo = new FileInfo(FileName);

                    SaveDirectoryName = SaveFileInfo.DirectoryName;
                    SaveFileName = SaveFileInfo.Name;
                    SaveFileExtention = SaveFileInfo.Extension.ToString();
                    SaveFileName = SaveFileName.Remove(SaveFileName.IndexOf(SaveFileExtention));

                    this.GetEventsInfo(ref ChList,
                                        out PatternNames,
                                        out EventNames,
                                        out Cycles,
                                        out Loops,
                                        out Counts,
                                        out Offsets,
                                        out Sizes);

                    #region Setup datagrid view

                    for (i = 0; i < ChList.Length; i++)
                    {
                        dtv[i] = new DATAVIEW();

                        MeasuredValues[i] = new double[PatternNames[i].Length][];
                        MeasuredTimes[i] = new double[PatternNames[i].Length][];

                        ChannelResultsFileName[i] = new string[PatternNames[i].Length];

                        for (j = 0; j < PatternNames[i].Length; j++)
                        {
                            if (MaxEventSize < PatternNames[i].Length)
                            {
                                MaxEventSize = PatternNames[i].Length;
                            }

                            if (MaxDataSize[i] < Sizes[i][j])
                            {
                                MaxDataSize[i] = Sizes[i][j];
                            }
                            MeasuredValues[i][j] = new double[Sizes[i][j]];
                            MeasuredTimes[i][j] = new double[Sizes[i][j]];
                        }


                        ColumnCount[i] = PatternNames[i].Length * 2;

                        dtv[i].dataGridViewResult.ColumnCount = ColumnCount[i];
                        dtv[i].dataGridViewResult.RowCount = MaxDataSize[i];
                        dtv[i].Text = "Channel " + ChList[i].ToString();

                        for (j = 0; j < PatternNames[i].Length; j++)
                        {
                            Frequency = "-" + Cycles[i][j].ToString() + "_" + Loops[i][j].ToString() + "_" + Counts[i][j].ToString();
                            ChannelResultsFileName[i][j] = SaveDirectoryName + @"\" + SaveFileName + "_" + EventNames[i][j].Trim() + Frequency + "_" + ChList[i].ToString() + SaveFileExtention;

                            dtv[i].dataGridViewResult.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                            Frequency = ":" + Cycles[i][j].ToString() + ":" + Loops[i][j].ToString() + ":" + Counts[i][j].ToString();
                            dtv[i].dataGridViewResult.Columns[ j * 2 ].HeaderText = EventNames[i][j].Trim() + Frequency.ToString() + " Time(s)";
                            dtv[i].dataGridViewResult.Columns[j * 2 + 1].HeaderText = EventNames[i][j].Trim() + Frequency.ToString() + " Value";
                        }
                        for (j = 0; j < MaxDataSize[i]; j++)
                        {
                            dtv[i].dataGridViewResult.Rows[j].HeaderCell.Value = j.ToString();
                        }

                        dtv[i].Show();
                     }

                    #endregion

                    #region Retrieving Data

                    int Index = 0;
                    int Offset = 0;
                    int Size = 0;
                    int MeasStatus = 0;

                    int Status = 0;
                    double ElapsedTime = 0.0;
                    double TotalTime = 0.0;
                    int k;

                    #region Get expected execution time
                    
                    rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                    wgpg.ElapsedTime = ElapsedTime;
                    wgpg.ExpectedExcecutionTime = TotalTime;
                    wgpg.Show();

                    #endregion

                    #region Retrieve result by event

                    for (j = 0; j < MaxEventSize; j++)
                    {
                        for (i = 0; i < ChList.Length; i++)
                        {
                            if (PatternNames[i].Length > 0)
                            {
                                MeasStatus = 0;
                                while (MeasStatus != WGFMU.MEASURE_EVENT_COMPLETED)
                                {
                                    if (wgpg.AbortStatus == 1)
                                    {
                                        wgpg.Close();
                                        WGFMU.abort();
                                        if (rc < WGFMU.NO_ERROR)
                                        {
                                            throw (new WGFMULibException(rc, ""));
                                        }
                                        break;
                                    }

                                    rc = WGFMU.isMeasureEventCompleted(ChList[i], PatternNames[i][j], EventNames[i][j], Cycles[i][j], Loops[i][j],
                                        Counts[i][j], ref MeasStatus, ref Index, ref Offset, ref Size);
                                    if (rc < WGFMU.NO_ERROR)
                                    {
                                        throw (new WGFMULibException(rc, ""));
                                    }
                                    Thread.Sleep(100);
                                    rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                                    if (rc < WGFMU.NO_ERROR)
                                    {
                                        throw (new WGFMULibException(rc, ""));
                                    }
                                    wgpg.ElapsedTime = ElapsedTime;
                                    wgpg.ExpectedExcecutionTime = TotalTime;
                                    Application.DoEvents();

                                }
                                rc = WGFMU.getMeasureValues(ChList[i], Offsets[i][j], ref Sizes[i][j], MeasuredTimes[i][j], MeasuredValues[i][j]);
                                if (rc < WGFMU.NO_ERROR)
                                {
                                    throw (new WGFMULibException(rc, ""));
                                }

                                for (k = 0; k < MeasuredValues[i][j].Length; k++)
                                {
                                    dtv[i].dataGridViewResult[j * 2, k].Value = this.DoubleToTrimedString(MeasuredTimes[i][j][k]);
                                }
                                Application.DoEvents();

                                for (k = 0; k < MeasuredValues[i][j].Length; k++)
                                {
                                    dtv[i].dataGridViewResult[j * 2 + 1, k].Value = MeasuredValues[i][j][k].ToString("0.000000E+00");
                                }
                                Application.DoEvents();

                                #region Update Channel Results File

                                this.UpdateChannelResultsFile(ChannelResultsFileName[i][j], 0, MeasuredValues[i][j].Length, ref MeasuredTimes[i][j], ref MeasuredValues[i][j]);

                                #endregion
                                
                            }
                        }
                    }
                    #endregion

                    #endregion
                    wgpg.Close();

                    this.SavetResultsToFileByEvent(FileName, ref ChList, ref PatternNames, ref EventNames, ref Cycles, ref Loops, ref Counts, ref Sizes, ref MeasuredTimes, ref MeasuredValues);



                }
                catch (WGFMULibException we)
                {

                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);
                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    this.Close();
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    this.Close();
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }

            }

            #endregion

            #region RetrieveResultsByEventRealTime

            public void RetrieveResultsByEventRealTtime(ref int[] ChList, string FileName)
            {
                #region Declaration of generic parameters

                int rc = 0;

                string FunctionName = "RetrieveResultsByEventRealTime";

                #endregion

                #region Declaration of method dedicated parameters
                WGFMU_PROGRESS wgpg = new WGFMU_PROGRESS();

                DATAVIEW[] dtv = new DATAVIEW[ChList.Length];

                int i, j, l;

                string SaveDirectoryName;
                string SaveFileName;
                string SaveFileExtention;

                string Frequency;
                int MaxEventSize = 0;
                int[] MaxDataSize = new int[ChList.Length];
                int[] ColumnCount = new int[ChList.Length];
                string[][] PatternNames;
                string[][] EventNames;
                int[][] Cycles;
                double[][] Loops;
                int[][] Counts;
                int[][] Offsets;
                int[][] Sizes;
                string[][] ChannelResultsFileName = new string [ChList.Length][];
                double[][][] MeasuredValues = new double[ChList.Length][][];
                double[][][] MeasuredTimes = new double[ChList.Length][][];


                #endregion

                try
                {
                    System.IO.FileInfo SaveFileInfo = new FileInfo(FileName);

                    SaveDirectoryName = SaveFileInfo.DirectoryName;
                    SaveFileName = SaveFileInfo.Name;
                    SaveFileExtention = SaveFileInfo.Extension.ToString();
                    SaveFileName = SaveFileName.Remove(SaveFileName.IndexOf(SaveFileExtention));

                    this.GetEventsInfo(ref ChList,
                                        out PatternNames,
                                        out EventNames,
                                        out Cycles,
                                        out Loops,
                                        out Counts,
                                        out Offsets,
                                        out Sizes);

                    #region Setup datagrid view

                    for (i = 0; i < ChList.Length; i++)
                    {
                        dtv[i] = new DATAVIEW();

                        MeasuredValues[i] = new double[PatternNames[i].Length][];
                        MeasuredTimes[i] = new double[PatternNames[i].Length][];
                        ChannelResultsFileName[i] = new string[PatternNames[i].Length];

                        for (j = 0; j < PatternNames[i].Length; j++)
                        {
                            if (MaxEventSize < PatternNames[i].Length)
                            {
                                MaxEventSize = PatternNames[i].Length;
                            }

                            if (MaxDataSize[i] < Sizes[i][j])
                            {
                                MaxDataSize[i] = Sizes[i][j];
                            }
                            MeasuredValues[i][j] = new double[Sizes[i][j]];
                            MeasuredTimes[i][j] = new double[Sizes[i][j]];
                        }


                        ColumnCount[i] = PatternNames[i].Length * 2;

                        dtv[i].dataGridViewResult.ColumnCount = ColumnCount[i];
                        dtv[i].dataGridViewResult.RowCount = MaxDataSize[i];
                        dtv[i].Text ="Channel " +  ChList[i].ToString();

                        for (j = 0; j < PatternNames[i].Length; j++)
                        {
                            //dtv[i].dataGridViewResult.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                            Frequency = "_" + Cycles[i][j].ToString() + "_" + Loops[i][j].ToString() + "_" + Counts[i][j].ToString();
                            ChannelResultsFileName[i][j] = SaveDirectoryName + @"\" + SaveFileName + "_" + EventNames[i][j].Trim() + Frequency + "_" + ChList[i].ToString() + SaveFileExtention ;
 
                            Frequency = ":" + Cycles[i][j].ToString() + ":" + Loops[i][j].ToString() + ":" + Counts[i][j].ToString();                            
                            dtv[i].dataGridViewResult.Columns[j * 2].HeaderText = EventNames[i][j].Trim() + Frequency + " Time(s)";
                            dtv[i].dataGridViewResult.Columns[j * 2 + 1].HeaderText = EventNames[i][j].Trim() + Frequency.ToString() + " Value";
                        }
                        for (j = 0; j < MaxDataSize[i]; j++)
                        {
                            dtv[i].dataGridViewResult.Rows[j].HeaderCell.Value = j.ToString();
                        }

                        dtv[i].Show();

                    }

                    #endregion

                    #region Retrieving Data


                    int Status = 0;
                    double ElapsedTime = 0.0;
                    double TotalTime = 0.0;
                    int k;

                    #region Get expected execution time

                    rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                    wgpg.ElapsedTime = ElapsedTime;
                    wgpg.ExpectedExcecutionTime = TotalTime;
                    wgpg.Show();

                    #endregion

                    int EventRetrievedStatus = 0;
                    int[] ChEventRetrievedStatus = new int[ChList.Length];

                    int[] RetrievedDataSize = new int[ChList.Length];
                    int[] MeasuredSize = new int[ChList.Length];
                    int[] TotalSize = new int[ChList.Length];

                    int[] RetrievedEventDataSize = new int[ChList.Length];
                    int[] MeasuredEventDataSize = new int[ChList.Length];
                    int[] TotalEventDataSize = new int[ChList.Length];

                    int RetrievingDataSize = 0;

                    rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                    wgpg.ElapsedTime = ElapsedTime;
                    wgpg.ExpectedExcecutionTime = TotalTime;
                    wgpg.Show();

                    #region Retrieve result by event

                    for (j = 0; j < MaxEventSize; j++)
                    {
                        Status = 0;

                        while (EventRetrievedStatus != 1)
                        {

                            #region Wait and update progress window

                            Thread.Sleep(100);
                            rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                            if (rc < WGFMU.NO_ERROR)
                            {
                                throw (new WGFMULibException(rc, ""));
                            }
                            wgpg.ElapsedTime = ElapsedTime;
                            wgpg.ExpectedExcecutionTime = TotalTime;
                            Application.DoEvents();

                            #endregion

                            for (i = 0; i < ChList.Length; i++)
                            {
                                if (PatternNames[i].Length > 0)
                                {
                                    TotalEventDataSize[i] = Sizes[i][j];
                                    RetrievedEventDataSize[i] = 0;

                                    WGFMU.getMeasureValueSize(ChList[i], ref MeasuredSize[i], ref TotalSize[i]);
                                    if (rc < WGFMU.NO_ERROR)
                                    {
                                        throw (new WGFMULibException(rc, ""));
                                    }

                                    MeasuredEventDataSize[i] = MeasuredSize[i] - Offsets[i][j];

                                    if (MeasuredEventDataSize[i] < 0)
                                    {
                                        MeasuredEventDataSize[i] = 0;
                                    }

                                    if (MeasuredEventDataSize[i] > TotalEventDataSize[i])
                                    {

                                        MeasuredEventDataSize[i] = TotalEventDataSize[i];
                                    }

                                    RetrievingDataSize = MeasuredEventDataSize[i] - RetrievedEventDataSize[i];
                                    if (RetrievingDataSize > 0)
                                    {
                                        for (k = 0; k < RetrievingDataSize; k++)
                                        {
                                            WGFMU.getMeasureValue(ChList[i], Offsets[i][j] + k + RetrievedEventDataSize[i],
                                                                    ref MeasuredTimes[i][j][k + RetrievedEventDataSize[i]],
                                                                    ref MeasuredValues[i][j][k + RetrievedEventDataSize[i]]);
                                        }

                                        #region Update results display

                                        for (k = 0; k < RetrievingDataSize; k++)
                                        {
                                            dtv[i].dataGridViewResult[j * 2, RetrievedEventDataSize[i] + k].Value = this.DoubleToTrimedString(MeasuredTimes[i][j][RetrievedEventDataSize[i] + k]);
                                            dtv[i].dataGridViewResult[j * 2 + 1, RetrievedEventDataSize[i] + k].Value = this.DoubleToTrimedString(MeasuredValues[i][j][RetrievedEventDataSize[i] + k]);
                                        
                                        }

                                        #endregion

                                        #region Update Channel Results File

                                        this.UpdateChannelResultsFile(ChannelResultsFileName[i][j], RetrievedEventDataSize[i], RetrievingDataSize, ref MeasuredTimes[i][j], ref MeasuredValues[i][j]);
                                        
                                        #endregion


                                        RetrievedEventDataSize[i] = MeasuredEventDataSize[i];
                                        RetrievingDataSize = 0;

                                        if (RetrievedEventDataSize[i] == TotalEventDataSize[i])
                                        {
                                            ChEventRetrievedStatus[i] = 1;
                                        }
                                        else if (PatternNames[i].Length == 0)
                                        {
                                            ChEventRetrievedStatus[i] = 1;
                                        }
                                        else
                                        {
                                            ChEventRetrievedStatus[i] = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    ChEventRetrievedStatus[i] = 1;
                                }
                            }
                            Status = 0;
                            for (l = 0; l < ChList.Length; l++)
                            {
                                Status = Status + ChEventRetrievedStatus[l];
                            }
                            if (Status == ChList.Length)
                            {
                                EventRetrievedStatus = 1;
                            }

                            if (wgpg.AbortStatus == 1)
                            {
                                wgpg.Close();
                                WGFMU.abort();
                                if (rc < WGFMU.NO_ERROR)
                                {
                                    throw (new WGFMULibException(rc, ""));
                                }
                                break;
                            }

                        }
                        for (l = 0; l < ChList.Length; l++)
                        {
                            RetrievedEventDataSize[l] = 0;
                            ChEventRetrievedStatus[l] = 0;
                        }
                        EventRetrievedStatus = 0;
                    }
                    #endregion

                    #endregion

                    wgpg.Close();

                    this.SavetResultsToFileByEvent(FileName, ref ChList, ref PatternNames, ref EventNames, ref Cycles, ref Loops, ref Counts, ref Sizes, ref MeasuredTimes, ref MeasuredValues);


                }
                catch (WGFMULibException we)
                {

                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);
                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    this.Close();
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    wgpg.Close();
                    this.Close();
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }

            }

            #endregion

            #region NBTIRetrieveResultsByEventRealTime

            public void NBTIRetrieveResultsByEventRealTtime(ref int[] ChList, string FileName)
            {
                #region Declaration of generic parameters

                int rc = 0;

                string FunctionName = "NBTIRetrieveResultsByEventRealTime";

                #endregion

                #region Declaration of method dedicated parameters
                WGFMU_PROGRESS wgpg = new WGFMU_PROGRESS();

                DATAVIEW[] dtv = new DATAVIEW[ChList.Length];

                int i, j, l;

                string SaveDirectoryName;
                string SaveFileName;
                string SaveFileExtention;
                int Size = 0;

                string Frequency;
                int MaxEventSize = 0;
                int[] MaxDataSize = new int[ChList.Length];
                int[] ColumnCount = new int[ChList.Length];
                string[][] PatternNames;
                string[][] EventNames;
                int[][] Cycles;
                double[][] Loops;
                int[][] Counts;
                int[][] Offsets;
                int[][] Sizes;
                string[][] ChannelResultsFileName = new string[ChList.Length][];
                double[][][] MeasuredValues = new double[ChList.Length][][];
                double[][][] MeasuredTimes = new double[ChList.Length][][];
                double[][] PatternTimes = new double[ChList.Length][];


                #endregion

                try
                {
                    System.IO.FileInfo SaveFileInfo = new FileInfo(FileName);

                    SaveDirectoryName = SaveFileInfo.DirectoryName;
                    SaveFileName = SaveFileInfo.Name;
                    SaveFileExtention = SaveFileInfo.Extension.ToString();
                    SaveFileName = SaveFileName.Remove(SaveFileName.IndexOf(SaveFileExtention));

                    this.GetEventsInfo(ref ChList,
                                        out PatternNames,
                                        out EventNames,
                                        out Cycles,
                                        out Loops,
                                        out Counts,
                                        out Offsets,
                                        out Sizes);

                    #region Setup datagrid view

                    for (i = 0; i < ChList.Length; i++)
                    {
                        dtv[i] = new DATAVIEW();

                        MeasuredValues[i] = new double[PatternNames[i].Length][];
                        MeasuredTimes[i] = new double[PatternNames[i].Length][];
                        ChannelResultsFileName[i] = new string[PatternNames[i].Length];

                        for (j = 0; j < PatternNames[i].Length; j++)
                        {
                            if (MaxEventSize < PatternNames[i].Length)
                            {
                                MaxEventSize = PatternNames[i].Length;
                            }

                            if (MaxDataSize[i] < Sizes[i][j])
                            {
                                MaxDataSize[i] = Sizes[i][j];
                            }
                            MeasuredValues[i][j] = new double[Sizes[i][j]];
                            MeasuredTimes[i][j] = new double[Sizes[i][j]];
                        }


                        PatternTimes[i] = new double[MaxDataSize[i]];

                        ColumnCount[i] = PatternNames[i].Length;

                        dtv[i].dataGridViewResult.ColumnCount = ColumnCount[i];
                        dtv[i].dataGridViewResult.RowCount = MaxDataSize[i] + 1;
                        dtv[i].Text = "Channel " + ChList[i].ToString();


                        for (j = 0; j < PatternNames[i].Length; j++)
                        {
                            dtv[i].dataGridViewResult.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;

                            Frequency = "_" + Cycles[i][j].ToString() + "_" + Loops[i][j].ToString() + "_" + Counts[i][j].ToString();
                            ChannelResultsFileName[i][j] = SaveDirectoryName + @"\" + SaveFileName + "_" + EventNames[i][j].Trim() + Frequency + "_" + ChList[i].ToString() + SaveFileExtention;

                            Frequency = ":" + Cycles[i][j].ToString() + ":" + Loops[i][j].ToString() + ":" + Counts[i][j].ToString();
                            dtv[i].dataGridViewResult.Columns[j].HeaderText = EventNames[i][j].Trim() + Frequency.ToString() + " Value";
                        }

                        dtv[i].dataGridViewResult.Rows[0].HeaderCell.Value = "Time (s)";

                        Size = MeasuredTimes[i][0].Length;
                        if (Size != 0)
                        {
                            rc = WGFMU.getPatternMeasureTimes(PatternNames[i][0], 0, ref Size, PatternTimes[i]);
                        }

                        for (j = 0; j < MaxDataSize[i]; j++)
                        {
                            dtv[i].dataGridViewResult.Rows[j + 1].HeaderCell.Value = this.DoubleToTrimedString(PatternTimes[i][j]);
                        }

                        dtv[i].Show();

                    }

                    #endregion

                    #region Retrieving Data


                    int Status = 0;
                    double ElapsedTime = 0.0;
                    double TotalTime = 0.0;
                    int k;

                    #region Get expected execution time

                    rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                    wgpg.ElapsedTime = ElapsedTime;
                    wgpg.ExpectedExcecutionTime = TotalTime;
                    wgpg.Show();

                    #endregion

                    int EventRetrievedStatus = 0;
                    int[] ChEventRetrievedStatus = new int[ChList.Length];

                    int[] RetrievedDataSize = new int[ChList.Length];
                    int[] MeasuredSize = new int[ChList.Length];
                    int[] TotalSize = new int[ChList.Length];

                    int[] RetrievedEventDataSize = new int[ChList.Length];
                    int[] MeasuredEventDataSize = new int[ChList.Length];
                    int[] TotalEventDataSize = new int[ChList.Length];

                    int RetrievingDataSize = 0;

                    rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                    wgpg.ElapsedTime = ElapsedTime;
                    wgpg.ExpectedExcecutionTime = TotalTime;
                    wgpg.Show();

                    #region Retrieve result by event

                    for (j = 0; j < MaxEventSize; j++)
                    {
                        Status = 0;

                        while (EventRetrievedStatus != 1)
                        {

                            #region Wait and update progress window

                            Thread.Sleep(100);
                            rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                            if (rc < WGFMU.NO_ERROR)
                            {
                                throw (new WGFMULibException(rc, ""));
                            }
                            wgpg.ElapsedTime = ElapsedTime;
                            wgpg.ExpectedExcecutionTime = TotalTime;
                            Application.DoEvents();

                            #endregion

                            for (i = 0; i < ChList.Length; i++)
                            {
                                if (PatternNames[i].Length > 0)
                                {
                                    TotalEventDataSize[i] = Sizes[i][j];
                                    RetrievedEventDataSize[i] = 0;

                                    WGFMU.getMeasureValueSize(ChList[i], ref MeasuredSize[i], ref TotalSize[i]);
                                    if (rc < WGFMU.NO_ERROR)
                                    {
                                        throw (new WGFMULibException(rc, ""));
                                    }

                                    MeasuredEventDataSize[i] = MeasuredSize[i] - Offsets[i][j];

                                    if (MeasuredEventDataSize[i] < 0)
                                    {
                                        MeasuredEventDataSize[i] = 0;
                                    }

                                    if (MeasuredEventDataSize[i] > TotalEventDataSize[i])
                                    {

                                        MeasuredEventDataSize[i] = TotalEventDataSize[i];
                                    }

                                    RetrievingDataSize = MeasuredEventDataSize[i] - RetrievedEventDataSize[i];

                                    if (RetrievingDataSize > 0)
                                    {
                                        for (k = 0; k < RetrievingDataSize; k++)
                                        {
                                            WGFMU.getMeasureValue(ChList[i], Offsets[i][j] + k + RetrievedEventDataSize[i],
                                                                    ref MeasuredTimes[i][j][k + RetrievedEventDataSize[i]],
                                                                    ref MeasuredValues[i][j][k + RetrievedEventDataSize[i]]);
                                        }

                                        #region Update results display

                                        for (k = 0; k < RetrievingDataSize; k++)
                                        {
                                            if (RetrievedDataSize[i] + k == 0)
                                            {
                                                dtv[i].dataGridViewResult[j, 0].Value = this.DoubleToTrimedString(MeasuredTimes[i][j][0] - PatternTimes[i][0]);
                                            }

                                            dtv[i].dataGridViewResult[j, RetrievedEventDataSize[i] + k +1].Value = this.DoubleToTrimedString(MeasuredValues[i][j][RetrievedEventDataSize[i] + k]);

                                        }

                                        #endregion

                                        #region Update Channel Results File

                                        this.UpdateChannelResultsFile(ChannelResultsFileName[i][j], RetrievedEventDataSize[i], RetrievingDataSize, ref MeasuredTimes[i][j], ref MeasuredValues[i][j]);

                                        #endregion


                                        RetrievedEventDataSize[i] = MeasuredEventDataSize[i];

                                        RetrievingDataSize = 0;

                                        if (RetrievedEventDataSize[i] == TotalEventDataSize[i])
                                        {
                                            ChEventRetrievedStatus[i] = 1;
                                        }
                                        else if (PatternNames[i].Length == 0)
                                        {
                                            ChEventRetrievedStatus[i] = 1;
                                        }
                                        else
                                        {
                                            ChEventRetrievedStatus[i] = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    ChEventRetrievedStatus[i] = 1;
                                }
                            }
                            Status = 0;
                            for (l = 0; l < ChList.Length; l++)
                            {
                                Status = Status + ChEventRetrievedStatus[l];
                            }
                            if (Status == ChList.Length)
                            {
                                EventRetrievedStatus = 1;
                            }

                            if (wgpg.AbortStatus == 1)
                            {
                                wgpg.Close();
                                WGFMU.abort();
                                if (rc < WGFMU.NO_ERROR)
                                {
                                    throw (new WGFMULibException(rc, ""));
                                }
                                break;
                            }

                        }
                        for (l = 0; l < ChList.Length; l++)
                        {
                            RetrievedEventDataSize[l] = 0;
                            ChEventRetrievedStatus[l] = 0;
                        }
                        EventRetrievedStatus = 0;
                    }
                    #endregion

                    #endregion

                    wgpg.Close();

                    this.NBTISavetResultsToFileByEvent(FileName, ref ChList, ref PatternTimes, ref PatternNames, ref EventNames, ref Cycles, ref Loops, ref Counts, ref Sizes, ref MeasuredTimes, ref MeasuredValues);


                }
                catch (WGFMULibException we)
                {

                    if (we.Message == "")
                    {
                        WGFMU.getErrorSize(ref ErrMsgSize);
                        ErrMsg = new StringBuilder(ErrMsgSize + 1);
                        WGFMU.getError(ErrMsg, ref ErrMsgSize);
                    }
                    else
                    {
                        ErrMsg = new StringBuilder();
                        ErrMsg.Append(":" + we.Message);
                    }
                    ErrMsg.Insert(0, FunctionName + ":");
                    this.Close();
                    throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
                }
                catch (Exception ex)
                {
                    wgpg.Close();
                    this.Close();
                    ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                    throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
                }

            }

            #endregion

        #endregion

        #region Retrieve based on "Pattern"

        #region SaveResultsToFileByEvent()

        private void SavetResultsToFileByPattern(String FileName,
                                            ref int[] ChannelList,
                                            ref string[][] PatternLabel,
                                            ref double[][][] MeasuredTime,
                                            ref double[][][] MeasuredValue)
        {
            FileStream fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            string FunctionName = "SaveResultsToFileByPattern";
            int i, j, k;
            int PatternSize = 0;
            int DataSize = 0;

            string OutputString;
            try
            {
                for (i = 0; i < ChannelList.Length; i++)
                {
                    if (PatternSize < PatternLabel[i].Length)
                    {
                        PatternSize = PatternLabel[i].Length;
                    }
                    for (j = 0; j < PatternLabel[i].Length; j++)
                    {
                        if (DataSize < MeasuredTime[i][j].Length)
                        {
                            DataSize = MeasuredTime[i][j].Length;
                        }
                    }
                }


                sw.WriteLine("<Results>");
                for (i = 0; i < ChannelList.Length; i++)
                {
                    for (j = 0; j < PatternSize; j++)
                    {
                        if (PatternLabel[i].Length != 0)
                        {
                            OutputString = PatternLabel[i][j] + " Time [s]";
                            sw.Write(OutputString);
                            sw.Write(",");

                            OutputString = PatternLabel[i][j] + " Value";
                            sw.Write(OutputString);
                            sw.Write(",");
                        }
                    }
                }

                sw.WriteLine("");
                for (k = 0; k < DataSize; k++)
                {
                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        for (j = 0; j < PatternSize; j++)
                        {
                            if (PatternLabel[i].Length != 0)
                            {
                                if (k < MeasuredTime[i][j].Length)
                                {
                                    OutputString = this.DoubleToTrimedString(MeasuredTime[i][j][k]);
                                    sw.Write(OutputString);
                                    sw.Write(",");

                                    OutputString = this.DoubleToTrimedString(MeasuredValue[i][j][k]);
                                    sw.Write(OutputString);
                                    sw.Write(",");
                                }
                                else
                                {
                                    OutputString = "";
                                    sw.Write(OutputString);
                                    sw.Write(",");

                                    OutputString = "";
                                    sw.Write(OutputString);
                                    sw.Write(",");

                                }
                            }
                        }
                    }
                    sw.WriteLine("");
                }


                sw.WriteLine("</Results>");

                sw.Close();
                fs.Close();
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region RetrieveResultsByPatternRealTime

        public void RetrieveResultsByPatternRealTime(ref int[] ChList, string FileName, double Hold)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "RetrieveResultsByPatternRealTime";

            #endregion

            #region Declaration of method dedicated parameters
            WGFMU_PROGRESS wgpg = new WGFMU_PROGRESS();

            DATAVIEW[] dtv = new DATAVIEW[ChList.Length];

            int i, j, k, l;

            string SaveDirectoryName;
            string SaveFileName;
            string SaveFileExtention;

            string Frequency;
            int MaxPatternSize = 0;
            int[] MaxDataSize = new int[ChList.Length];
            int[] ColumnCount = new int[ChList.Length];
            string[][] PatternNames;
            string[][] EventNames;
            int[][] Cycles;
            double[][] Loops;
            int[][] Counts;
            int[][] Offsets;
            int[][] Sizes;
            string[][] ChannelResultsFileName = new string[ChList.Length][];
            double[][][] MeasuredValues = new double[ChList.Length][][];
            double[][][] MeasuredTimes = new double[ChList.Length][][];
            int[] PatternCount = new int[ChList.Length];
            int[][] PatternEventCount = new int[ChList.Length][];
            int[][] PatternDataCount = new int[ChList.Length][];
            string[][] PatternLabel = new string[ChList.Length][];
            string[][] PatternFileName = new string[ChList.Length][];
            int[][] PatternOffsets = new int[ChList.Length][];
            int EventCount;

            #endregion

            try
            {
                System.IO.FileInfo SaveFileInfo = new FileInfo(FileName);

                SaveDirectoryName = SaveFileInfo.DirectoryName;
                SaveFileName = SaveFileInfo.Name;
                SaveFileExtention = SaveFileInfo.Extension.ToString();
                SaveFileName = SaveFileName.Remove(SaveFileName.IndexOf(SaveFileExtention));

                this.GetEventsInfo(ref ChList,
                                    out PatternNames,
                                    out EventNames,
                                    out Cycles,
                                    out Loops,
                                    out Counts,
                                    out Offsets,
                                    out Sizes);

                #region Setup datagrid view

                // Count number of the individual pattern devided by the cycle and loop.
                for (i = 0; i < ChList.Length; i++)
                {
                    PatternCount[i] = 0;

                    k = 0;
                    for (j = 0; j < PatternNames[i].Length; j++)
                    {
                        if (j == 0)
                        {
                            PatternCount[i] = 1;
                        }
                        if (!((PatternNames[i][k] == PatternNames[i][j]) && (Cycles[i][k] == Cycles[i][j]) && (Loops[i][k] == Loops[i][j])))
                        {
                            PatternCount[i] = PatternCount[i] + 1;
                            k = j;
                        }
                    }
                    PatternEventCount[i] = new int[PatternCount[i]];
                    PatternDataCount[i] = new int[PatternCount[i]];
                    PatternLabel[i] = new string[PatternCount[i]];
                    PatternFileName[i] = new string[PatternCount[i]];
                    PatternOffsets[i] = new int[PatternCount[i]];
                }

                // Count number of the measurement event in each pattern
                for (i = 0; i < ChList.Length; i++)
                {
                    k = 0;
                    l = 0;

                    if (PatternNames[i].Length > 0)
                    {
                        PatternEventCount[i][0] = 0;
                    }
                    EventCount = 0;

                    for (j = 0; j < PatternNames[i].Length; j++)
                    {
                        if ((PatternNames[i][k] == PatternNames[i][j]) && (Cycles[i][k] == Cycles[i][j]) && (Loops[i][k] == Loops[i][j]))
                        {
                            PatternEventCount[i][l] = PatternEventCount[i][l] + 1;
                            if (EventCount == 0)
                            {
                                PatternLabel[i][l] = PatternNames[i][k] + ":" + Cycles[i][k].ToString() + ":" + Loops[i][k].ToString();
                                PatternFileName[i][l] = PatternNames[i][k] + "_" + Cycles[i][k].ToString() + "_" + Loops[i][k].ToString();
                            }
                            EventCount = EventCount + 1;
                        }
                        else
                        {
                            k = j;
                            j = j - 1;
                            l = l + 1;
                            EventCount = 0;
                        }
                    }
                }

                // Count number of data in the each pattern
                for (i = 0; i < ChList.Length; i++)
                {
                    l = 0;
                    for (j = 0; j < PatternCount[i]; j++)
                    {
                        PatternDataCount[i][j] = 0;

                        for (k = 0; k < PatternEventCount[i][j]; k++)
                        {
                            PatternDataCount[i][j] = PatternDataCount[i][j] + Sizes[i][l];
                            l = l + 1;
                        }
                    }

                }

                // Create pattern offset
                for (i = 0; i < ChList.Length; i++)
                {
                    for (j = 0; j < PatternCount[i]; j++)
                    {
                        if (j == 0)
                        {
                            PatternOffsets[i][j] = 0;
                        }
                        else
                        {
                            PatternOffsets[i][j] = PatternOffsets[i][j-1] + PatternDataCount[i][j - 1];
                        }
                    }                   
                }

                // Count maximum data size of the data view window
    
                for (i = 0; i < ChList.Length; i++)
                {
                    if (PatternNames[i].Length > 0)
                    {
                        dtv[i] = new DATAVIEW();

                        MeasuredValues[i] = new double[PatternCount[i]][];
                        MeasuredTimes[i] = new double[PatternCount[i]][];
                        ChannelResultsFileName[i] = new string[PatternCount[i]];

                        if (MaxPatternSize < PatternCount[i])
                        {
                            MaxPatternSize = PatternCount[i];
                        }

                        for (j = 0; j < PatternCount[i]; j++)
                        {
                            if (MaxDataSize[i] < PatternDataCount[i][j])
                            {
                                MaxDataSize[i] = PatternDataCount[i][j];
                            }
                            MeasuredValues[i][j] = new double[PatternDataCount[i][j]];
                            MeasuredTimes[i][j] = new double[PatternDataCount[i][j]];
                        }


                        ColumnCount[i] = PatternCount[i] * 2;

                        dtv[i].dataGridViewResult.ColumnCount = ColumnCount[i];
                        dtv[i].dataGridViewResult.RowCount = MaxDataSize[i];
                        dtv[i].Text = "Channel " + ChList[i].ToString();

                        // Put the header of the column
                        for (j = 0; j < PatternCount[i]; j++)
                        {

                            Frequency = j.ToString();
                            ChannelResultsFileName[i][j] = SaveDirectoryName + @"\" + SaveFileName + "_" + PatternFileName[i][j] + "_" + ChList[i].ToString() + SaveFileExtention;

                            dtv[i].dataGridViewResult.Columns[j * 2].HeaderText = PatternLabel[i][j] + " Time(s)";
                            dtv[i].dataGridViewResult.Columns[j * 2 + 1].HeaderText = PatternLabel[i][j] + " Value";
                        }
                        for (j = 0; j < MaxDataSize[i]; j++)
                        {
                            dtv[i].dataGridViewResult.Rows[j].HeaderCell.Value = j.ToString();
                        }

                        dtv[i].Show();
                    }

                }

                #endregion

                #region Retrieving Data


                int Status = 0;
                double ElapsedTime = 0.0;
                double TotalTime = 0.0;

                #region Get expected execution time

                rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                if (rc < WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }
                wgpg.ElapsedTime = ElapsedTime;
                wgpg.ExpectedExcecutionTime = TotalTime;
                wgpg.Show();

                #endregion

                int PatternRetrievedStatus = 0;
                int[] ChPatternRetrievedStatus = new int[ChList.Length];

                int[] RetrievedDataSize = new int[ChList.Length];
                int[] MeasuredSize = new int[ChList.Length];
                int[] TotalSize = new int[ChList.Length];

                int[] RetrievedPatternDataSize = new int[ChList.Length];
                int[] MeasuredPatternDataSize = new int[ChList.Length];
                int[] TotalPatternDataSize = new int[ChList.Length];

                int RetrievingDataSize = 0;

                rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                if (rc < WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }
                wgpg.ElapsedTime = ElapsedTime;
                wgpg.ExpectedExcecutionTime = TotalTime;
                wgpg.Show();

                #region Retrieve result by pattern

                for (j = 0; j < MaxPatternSize; j++)
                {
                    Status = 0;

                    while (PatternRetrievedStatus != 1)
                    {

                        #region Wait and update progress window

                        Thread.Sleep(100);
                        rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                        if (rc < WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                        wgpg.ElapsedTime = ElapsedTime;
                        wgpg.ExpectedExcecutionTime = TotalTime;
                        Application.DoEvents();

                        #endregion

                        for (i = 0; i < ChList.Length; i++)
                        {
                            if (PatternNames[i].Length > 0)
                            {
                                TotalPatternDataSize[i] = PatternDataCount[i][j];
                                RetrievedPatternDataSize[i] = 0;

                                WGFMU.getMeasureValueSize(ChList[i], ref MeasuredSize[i], ref TotalSize[i]);
                                if (rc < WGFMU.NO_ERROR)
                                {
                                    throw (new WGFMULibException(rc, ""));
                                }

                                MeasuredPatternDataSize[i] = MeasuredSize[i] - PatternOffsets[i][j];

                                if (MeasuredPatternDataSize[i] < 0)
                                {
                                    MeasuredPatternDataSize[i] = 0;
                                }

                                if (MeasuredPatternDataSize[i] > TotalPatternDataSize[i])
                                {

                                    MeasuredPatternDataSize[i] = TotalPatternDataSize[i];
                                }

                                RetrievingDataSize = MeasuredPatternDataSize[i] - RetrievedPatternDataSize[i];
                                if (RetrievingDataSize > 0)
                                {
                                    for (k = 0; k < RetrievingDataSize; k++)
                                    {
                                        WGFMU.getMeasureValue(ChList[i], PatternOffsets[i][j] + k + RetrievedPatternDataSize[i],
                                                                ref MeasuredTimes[i][j][k + RetrievedPatternDataSize[i]],
                                                                ref MeasuredValues[i][j][k + RetrievedPatternDataSize[i]]);
                                        MeasuredTimes[i][j][k + RetrievedPatternDataSize[i]] = MeasuredTimes[i][j][k + RetrievedPatternDataSize[i]] - Hold;
                                    }

                                    #region Update results display

                                    for (k = 0; k < RetrievingDataSize; k++)
                                    {
                                        dtv[i].dataGridViewResult[j * 2, RetrievedPatternDataSize[i] + k].Value = this.DoubleToTrimedString(1E-9 * Math.Round((MeasuredTimes[i][j][RetrievedPatternDataSize[i] + k])/1E-9));
                                        dtv[i].dataGridViewResult[j * 2 + 1, RetrievedPatternDataSize[i] + k].Value = this.DoubleToTrimedString(MeasuredValues[i][j][RetrievedPatternDataSize[i] + k]);

                                    }

                                    #endregion

                                    #region Update Channel Results File

                                    this.UpdateChannelResultsFile(ChannelResultsFileName[i][j], RetrievedPatternDataSize[i], RetrievingDataSize, ref MeasuredTimes[i][j], ref MeasuredValues[i][j]);

                                    #endregion


                                    RetrievedPatternDataSize[i] = MeasuredPatternDataSize[i];
                                    RetrievingDataSize = 0;

                                    if (RetrievedPatternDataSize[i] == TotalPatternDataSize[i])
                                    {
                                        ChPatternRetrievedStatus[i] = 1;
                                    }
                                    else if (PatternNames[i].Length == 0)
                                    {
                                        ChPatternRetrievedStatus[i] = 1;
                                    }
                                    else
                                    {
                                        ChPatternRetrievedStatus[i] = 0;
                                    }
                                }
                            }
                            else
                            {
                                ChPatternRetrievedStatus[i] = 1;
                            }
                        }
                        Status = 0;
                        for (l = 0; l < ChList.Length; l++)
                        {
                            Status = Status + ChPatternRetrievedStatus[l];
                        }
                        if (Status == ChList.Length)
                        {
                            PatternRetrievedStatus = 1;
                        }

                        if (wgpg.AbortStatus == 1)
                        {
                            wgpg.Close();
                            WGFMU.abort();
                            if (rc < WGFMU.NO_ERROR)
                            {
                                throw (new WGFMULibException(rc, ""));
                            }
                            break;
                        }

                    }
                    for (l = 0; l < ChList.Length; l++)
                    {
                        RetrievedPatternDataSize[l] = 0;
                        ChPatternRetrievedStatus[l] = 0;
                    }
                    PatternRetrievedStatus = 0;
                }
                #endregion

                #endregion

                wgpg.Close();

                this.SavetResultsToFileByPattern(FileName, ref ChList, ref PatternLabel, ref MeasuredTimes, ref MeasuredValues);

            }
            catch (WGFMULibException we)
            {

                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);
                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                wgpg.Close();
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        }

        #endregion    

        #region NBTISaveResultsToFileByPattern()

        private void NBTISavetResultsToFileByPattern(String FileName,
                                            ref int[] ChannelList,
                                            ref double[][] PatternTimes,
                                            ref string[][] PatternLabel,
                                            ref double[][][] MeasuredTime,
                                            ref double[][][] MeasuredValue)
        {
            FileStream fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            string FunctionName = "NBTISaveResultsByPatternToFile";
            int i, j, k;
            int PatternSize = 0;
            int DataSize = 0;
            double ConvertedTime = 0.0;

            string OutputString;
            try
            {
                for (i = 0; i < ChannelList.Length; i++)
                {
                    if (PatternSize < PatternLabel[i].Length)
                    {
                        PatternSize = PatternLabel[i].Length;
                    }
                    for (j = 0; j < PatternLabel[i].Length; j++)
                    {
                        if (DataSize < MeasuredTime[i][j].Length)
                        {
                            DataSize = MeasuredTime[i][j].Length;
                        }
                    }
                }

                sw.WriteLine("<Results>");

                if (PatternSize != 0)
                {
                    sw.Write(" ");
                    sw.Write(",");

                }
                for (i = 0; i < ChannelList.Length; i++)
                {
                    for (j = 0; j < PatternSize; j++)
                    {
                        if (PatternLabel[i].Length != 0)
                        {
                            OutputString = PatternLabel[i][j] + " Value";
                            sw.Write(OutputString);
                            sw.Write(",");
                        }
                    }
                }
                sw.WriteLine("");

                if (PatternSize != 0)
                {
                    sw.Write("Time (s)");
                    sw.Write(",");

                }
                for (i = 0; i < ChannelList.Length; i++)
                {
                    for (j = 0; j < PatternSize; j++)
                    {

                        if (PatternLabel[i].Length != 0)
                        {
                            OutputString = this.DoubleToTrimedString(MeasuredTime[i][j][0]);
                            sw.Write(OutputString);
                            sw.Write(",");
                        }
                    }
                }
                sw.WriteLine("");


                for (k = 0; k < DataSize; k++)
                {
                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        if (PatternLabel[i].Length != 0)
                        {
                            ConvertedTime = MeasuredTime[i][0][k] - (MeasuredTime[i][0][0] - PatternTimes[i][0]);
                            OutputString = this.DoubleToTrimedString(ConvertedTime);
                            sw.Write(OutputString);
                            sw.Write(",");
                            break;
                        }
                    }
                    for (i = 0; i < ChannelList.Length; i++)
                    {
                        for (j = 0; j < PatternSize; j++)
                        {
                            if (PatternLabel[i].Length != 0)
                            {
                                if (k < MeasuredTime[i][j].Length)
                                {
                                    OutputString = this.DoubleToTrimedString(MeasuredValue[i][j][k]);
                                    sw.Write(OutputString);
                                    sw.Write(",");
                                }
                                else
                                {
                                    OutputString = "";
                                    sw.Write(OutputString);
                                    sw.Write(",");

                                    OutputString = "";
                                    sw.Write(OutputString);
                                    sw.Write(",");

                                }
                            }
                        }
                    }
                    sw.WriteLine("");
                }


                sw.WriteLine("</Results>");

                sw.Close();
                fs.Close();
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region NBTIRetrieveResultsByPatternRealTime

        public void NBTIRetrieveResultsByPatternRealTime(ref int[] ChList, string FileName, double BiasHold)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "NBTIRetrieveResultsByPatternRealTime";

            #endregion

            #region Declaration of method dedicated parameters
            WGFMU_PROGRESS wgpg = new WGFMU_PROGRESS();

            DATAVIEW[] dtv = new DATAVIEW[ChList.Length];

            int i, j, k, l;

            string SaveDirectoryName;
            string SaveFileName;
            string SaveFileExtention;

            int MaxPatternSize = 0;
            int[] MaxDataSize = new int[ChList.Length];
            int[] ColumnCount = new int[ChList.Length];
            string[][] PatternNames;
            string[][] EventNames;
            int[][] Cycles;
            double[][] Loops;
            int[][] Counts;
            int[][] Offsets;
            int[][] Sizes;
            string[][] ChannelResultsFileName = new string[ChList.Length][];
            double[][][] MeasuredValues = new double[ChList.Length][][];
            double[][][] MeasuredTimes = new double[ChList.Length][][];
            int[] PatternCount = new int[ChList.Length];
            int[][] PatternEventCount = new int[ChList.Length][];
            int[][] PatternDataCount = new int[ChList.Length][];
            string[][] PatternLabel = new string[ChList.Length][];
            string[][] PatternFileName = new string[ChList.Length][];
            int[][] PatternOffsets = new int[ChList.Length][];
            int EventCount;
            double[][] PatternTimes = new double[ChList.Length][];
            int PatternTimeSize = 0;

            #endregion

            try
            {
                System.IO.FileInfo SaveFileInfo = new FileInfo(FileName);

                SaveDirectoryName = SaveFileInfo.DirectoryName;
                SaveFileName = SaveFileInfo.Name;
                SaveFileExtention = SaveFileInfo.Extension.ToString();
                SaveFileName = SaveFileName.Remove(SaveFileName.IndexOf(SaveFileExtention));

                this.GetEventsInfo(ref ChList,
                                    out PatternNames,
                                    out EventNames,
                                    out Cycles,
                                    out Loops,
                                    out Counts,
                                    out Offsets,
                                    out Sizes);

                #region Setup datagrid view

                // Count number of the individual pattern devided by the cycle and loop.
                for (i = 0; i < ChList.Length; i++)
                {
                    PatternCount[i] = 0;

                    k = 0;
                    for (j = 0; j < PatternNames[i].Length; j++)
                    {
                        if (j == 0)
                        {
                            PatternCount[i] = 1;
                        }
                        if (!((PatternNames[i][k] == PatternNames[i][j]) && (Cycles[i][k] == Cycles[i][j]) && (Loops[i][k] == Loops[i][j])))
                        {
                            PatternCount[i] = PatternCount[i] + 1;
                            k = j;
                        }
                    }
                    PatternEventCount[i] = new int[PatternCount[i]];
                    PatternDataCount[i] = new int[PatternCount[i]];
                    PatternLabel[i] = new string[PatternCount[i]];
                    PatternFileName[i] = new string[PatternCount[i]];
                    PatternOffsets[i] = new int[PatternCount[i]];
                }

                // Count number of the measurement event in each pattern
                for (i = 0; i < ChList.Length; i++)
                {
                    k = 0;
                    l = 0;
                        
                    if (PatternNames[i].Length > 0)
                    {    
                        PatternEventCount[i][0] = 0;
                    }
                    EventCount = 0;

                    for (j = 0; j < PatternNames[i].Length; j++)
                    {
                        if ((PatternNames[i][k] == PatternNames[i][j]) && (Cycles[i][k] == Cycles[i][j]) && (Loops[i][k] == Loops[i][j]))
                        {
                            PatternEventCount[i][l] = PatternEventCount[i][l] + 1;
                            if (EventCount == 0)
                            {
                                PatternLabel[i][l] = PatternNames[i][k] + ":" + Cycles[i][k].ToString() + ":" + Loops[i][k].ToString();
                                PatternFileName[i][l] = PatternNames[i][k] + "_" + Cycles[i][k].ToString() + "_" + Loops[i][k].ToString();
                            }
                            EventCount = EventCount + 1;
                        }
                        else
                        {
                            k = j;
                            j = j - 1;
                            l = l + 1;
                            EventCount = 0;
                        }
                    }
                }

                // Count number of data in the each pattern
                for (i = 0; i < ChList.Length; i++)
                {
                    l = 0;
 
                    for (j = 0; j < PatternCount[i]; j++)
                    {
                        if (PatternNames[i].Length > 0)
                        {
                            PatternDataCount[i][j] = 0;
                        }

                        for (k = 0; k < PatternEventCount[i][j]; k++)
                        {
                            PatternDataCount[i][j] = PatternDataCount[i][j] + Sizes[i][l];
                            l = l + 1;
                        }
                    }

                }

                // Create pattern offset
                for (i = 0; i < ChList.Length; i++)
                {
                    for (j = 0; j < PatternCount[i]; j++)
                    {
                        if (j == 0)
                        {
                            PatternOffsets[i][j] = 0;
                        }
                        else
                        {
                            PatternOffsets[i][j] = PatternOffsets[i][j - 1] + PatternDataCount[i][j - 1];
                        }
                    }
                }

                // Get pattern time data
                for (i = 0; i < ChList.Length; i++)
                {
                    if (PatternNames[i].Length > 0)
                    {
                        rc = WGFMU.getPatternMeasureTimeSize(PatternNames[i][0], ref PatternTimeSize);
                        if (rc < WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                        PatternTimes[i] = new double[PatternTimeSize];

                        rc = WGFMU.getPatternMeasureTimes(PatternNames[i][0], 0, ref PatternTimeSize, PatternTimes[i]);
                        if (rc < WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }
                }

                // Count maximum data size of the data view window

                for (i = 0; i < ChList.Length; i++)
                {
                    if (PatternNames[i].Length > 0)
                    {
                        dtv[i] = new DATAVIEW();

                        MeasuredValues[i] = new double[PatternCount[i]][];
                        MeasuredTimes[i] = new double[PatternCount[i]][];
                        ChannelResultsFileName[i] = new string[PatternCount[i]];

                        if (MaxPatternSize < PatternCount[i])
                        {
                            MaxPatternSize = PatternCount[i];
                        }

                        for (j = 0; j < PatternCount[i]; j++)
                        {
                            if (MaxDataSize[i] < PatternDataCount[i][j])
                            {
                                MaxDataSize[i] = PatternDataCount[i][j];
                            }
                            MeasuredValues[i][j] = new double[PatternDataCount[i][j]];
                            MeasuredTimes[i][j] = new double[PatternDataCount[i][j]];
                        }


                        ColumnCount[i] = PatternCount[i];

                        dtv[i].dataGridViewResult.ColumnCount = ColumnCount[i];
                        dtv[i].dataGridViewResult.RowCount = MaxDataSize[i] + 1;
                        dtv[i].Text = "Channel " + ChList[i].ToString();

                        // Put the header of the column
                        for (j = 0; j < PatternCount[i]; j++)
                        {

                            ChannelResultsFileName[i][j] = SaveDirectoryName + @"\" + SaveFileName + "_" + PatternFileName[i][j] + "_" + ChList[i].ToString() + SaveFileExtention;

                            dtv[i].dataGridViewResult.Columns[j].HeaderText = PatternLabel[i][j] + " Value";
                        }

                        dtv[i].dataGridViewResult.Rows[0].HeaderCell.Value = "Time (s)";

                        for (k = 0; k < PatternTimes[i].Length; k++)
                        {
                            PatternTimes[i][k] = 1E-9 * Math.Round((PatternTimes[i][k] - BiasHold)/1E-9);
                            dtv[i].dataGridViewResult.Rows[k + 1].HeaderCell.Value = this.DoubleToTrimedString(PatternTimes[i][k]);
                        }

                        dtv[i].Show();
                    }
                }

                #endregion

                #region Retrieving Data


                int Status = 0;
                double ElapsedTime = 0.0;
                double TotalTime = 0.0;

                #region Get expected execution time

                rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                if (rc < WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }
                wgpg.ElapsedTime = ElapsedTime;
                wgpg.ExpectedExcecutionTime = TotalTime;
                wgpg.Show();

                #endregion

                int PatternRetrievedStatus = 0;
                int[] ChPatternRetrievedStatus = new int[ChList.Length];

                int[] RetrievedDataSize = new int[ChList.Length];
                int[] MeasuredSize = new int[ChList.Length];
                int[] TotalSize = new int[ChList.Length];

                int[] RetrievedPatternDataSize = new int[ChList.Length];
                int[] MeasuredPatternDataSize = new int[ChList.Length];
                int[] TotalPatternDataSize = new int[ChList.Length];

                int RetrievingDataSize = 0;

                rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                if (rc < WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }
                wgpg.ElapsedTime = ElapsedTime;
                wgpg.ExpectedExcecutionTime = TotalTime;
                wgpg.Show();

                #region Retrieve result by event

                for (j = 0; j < MaxPatternSize; j++)
                {
                    Status = 0;

                    while (PatternRetrievedStatus != 1)
                    {

                        #region Wait and update progress window
                        Application.DoEvents();
                        Thread.Sleep(100);
                        rc = WGFMU.getStatus(ref Status, ref ElapsedTime, ref TotalTime);
                        if (rc < WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                        wgpg.ElapsedTime = ElapsedTime;
                        wgpg.ExpectedExcecutionTime = TotalTime;
                        Application.DoEvents();

                        #endregion

                        for (i = 0; i < ChList.Length; i++)
                        {
                            if (PatternNames[i].Length > 0)
                            {
                                TotalPatternDataSize[i] = PatternDataCount[i][j];
                                RetrievedPatternDataSize[i] = 0;



                                WGFMU.getMeasureValueSize(ChList[i], ref MeasuredSize[i], ref TotalSize[i]);
                                if (rc < WGFMU.NO_ERROR)
                                {
                                    throw (new WGFMULibException(rc, ""));
                                }

                                MeasuredPatternDataSize[i] = MeasuredSize[i] - PatternOffsets[i][j];

                                if (MeasuredPatternDataSize[i] < 0)
                                {
                                    MeasuredPatternDataSize[i] = 0;
                                }

                                if (MeasuredPatternDataSize[i] > TotalPatternDataSize[i])
                                {

                                    MeasuredPatternDataSize[i] = TotalPatternDataSize[i];
                                }

                                RetrievingDataSize = MeasuredPatternDataSize[i] - RetrievedPatternDataSize[i];
                                if (RetrievingDataSize > 0)
                                {
                                    for (k = 0; k < RetrievingDataSize; k++)
                                    {
                                        WGFMU.getMeasureValue(ChList[i], PatternOffsets[i][j] + k + RetrievedPatternDataSize[i],
                                                                ref MeasuredTimes[i][j][k + RetrievedPatternDataSize[i]],
                                                                ref MeasuredValues[i][j][k + RetrievedPatternDataSize[i]]);
                                    }

                                    #region Update results display

                                    for (k = 0; k < RetrievingDataSize; k++)
                                    {
                                       //
                                        if (RetrievedDataSize[i] + k == 0)
                                        {
                                            dtv[i].dataGridViewResult[j, 0].Value = this.DoubleToTrimedString(MeasuredTimes[i][j][0] - PatternTimes[i][0]);
                                        }
                                        //
                                        /*
                                        if (j == 0)
                                        {
                                            ConvertedPaternTime = MeasuredTimes[i][j][RetrievedPatternDataSize[i] + k] - (MeasuredTimes[i][0][0] - PatternTimes[i][0]);
                                            ConvertedPaternTime = 1E-9 * Math.Round(ConvertedPaternTime / 1E-9);
                                            dtv[i].dataGridViewResult.Rows[RetrievedPatternDataSize[i] + k + 1].HeaderCell.Value 
                                                                                                    = this.DoubleToTrimedString(ConvertedPaternTime);
                                        }
                                        */
                                        
                                        
                                        dtv[i].dataGridViewResult[j, RetrievedPatternDataSize[i] + k + 1].Value = this.DoubleToTrimedString(MeasuredValues[i][j][RetrievedPatternDataSize[i] + k]);
                                        //Application.DoEvents();
                                    }

                                    #endregion

                                    #region Update Channel Results File

                                    this.UpdateChannelResultsFile(ChannelResultsFileName[i][j], RetrievedPatternDataSize[i], RetrievingDataSize, ref MeasuredTimes[i][j], ref MeasuredValues[i][j]);

                                    #endregion


                                    RetrievedPatternDataSize[i] = MeasuredPatternDataSize[i];
                                    RetrievingDataSize = 0;

                                    if (RetrievedPatternDataSize[i] == TotalPatternDataSize[i])
                                    {
                                        ChPatternRetrievedStatus[i] = 1;
                                    }
                                    else if (PatternNames[i].Length == 0)
                                    {
                                        ChPatternRetrievedStatus[i] = 1;
                                    }
                                    else
                                    {
                                        ChPatternRetrievedStatus[i] = 0;
                                    }
                                }
                            }
                            else
                            {
                                ChPatternRetrievedStatus[i] = 1;
                            }
                        }
                        Status = 0;
                        for (l = 0; l < ChList.Length; l++)
                        {
                            Status = Status + ChPatternRetrievedStatus[l];
                        }
                        if (Status == ChList.Length)
                        {
                            PatternRetrievedStatus = 1;
                        }

                        if (wgpg.AbortStatus == 1)
                        {
                            wgpg.Close();
                            WGFMU.abort();
                            if (rc < WGFMU.NO_ERROR)
                            {
                                throw (new WGFMULibException(rc, ""));
                            }
                            break;
                        }

                    }
                    for (l = 0; l < ChList.Length; l++)
                    {
                        RetrievedPatternDataSize[l] = 0;
                        ChPatternRetrievedStatus[l] = 0;
                    }
                    PatternRetrievedStatus = 0;
                }
                #endregion

                #endregion

                wgpg.Close();

                this.NBTISavetResultsToFileByPattern(FileName, ref ChList, ref PatternTimes, ref PatternLabel, ref MeasuredTimes, ref MeasuredValues);

            }
            catch (WGFMULibException we)
            {

                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);
                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                wgpg.Close();
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        }

        #endregion    

        #endregion

        #endregion

        #region Pattern Creation

        #region Clear

        public void Clear()
        {
            int rc;

            rc = WGFMU.clear();
        
        }

        #endregion


        #region Create Waveform

        #region CreateStepWaveform

        public void CreateStepWaveform( 
            string PatternName,
            double InitV,
            double InitHold,
            double InitToBaseTransient,
            double BaseV,
            double BaseHold,
            double BaseToSourceTransient,
            double SourceV,
            double SourceDuration,
            double SourceToTermTransient,
            double TermV,
            double TermHold )
        {

            #region Declaration of generic parameters

            int rc = 0;
            string FunctionName = "CreateStepWaveform";

            #endregion

            #region Declaration of method dedicated parameters
            int PatternCount;
            double RemainDuration;
            int i;

            #endregion

            try
            {
                #region Input Parameter Check

                if (InitToBaseTransient < DELTA_TIME_MIN && InitV != BaseV)
                { 
                    rc = -1;
                    throw (new WGFMULibException(rc," Transient between the different levels must not be 0 sec."));       
                }

                if (BaseToSourceTransient < DELTA_TIME_MIN && BaseV != SourceV)
                { 
                    rc = -1;
                    throw (new WGFMULibException(rc, " Transient between the different levels must not be 0 sec."));       
                }

                if (SourceToTermTransient < DELTA_TIME_MIN && SourceV != TermV)
                { 
                    rc = -1;
                    throw (new WGFMULibException(rc, " Transient between the different levels must not be 0 sec."));       
                }

                #endregion


                #region Body of method


                #region Create step waveform
                
                rc = WGFMU.createPattern(PatternName, InitV);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #region Add initial vectors

                if (!(InitHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, InitHold, InitV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(InitToBaseTransient < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, InitToBaseTransient, BaseV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(BaseHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, BaseHold, BaseV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #region Add step vectors

                if (!(BaseToSourceTransient < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, BaseToSourceTransient, SourceV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (SourceDuration <= DELTA_TIME_MAX)
                {
                    if (SourceDuration >= DELTA_TIME_MIN)
                    {
                        rc = WGFMU.addVector(PatternName, SourceDuration, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }
                }

                else
                {
                    PatternCount = (int)Math.Floor(SourceDuration / DELTA_TIME_MAX);
                    RemainDuration = SourceDuration - DELTA_TIME_MAX * PatternCount;

                    for (i = 0; i < PatternCount; i++)
                    {
                        rc = WGFMU.addVector(PatternName, DELTA_TIME_MAX, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }

                    }
                    if (RemainDuration >= DELTA_TIME_MIN)
                    {
                        rc = WGFMU.addVector(PatternName, RemainDuration, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }
                }

                #endregion

                #region Add terminating vector

                if (!(SourceToTermTransient < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, SourceToTermTransient, TermV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #endregion

                if (!(TermHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, TermHold, TermV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region CreateStairecaseSweepWaveform

        public void CreateStairecaseSweepWaveform(
            string PatternName,
            double InitV,
            double InitHold,
            double InitToStartTransient,
            double SweepHold,
            double StartV,
            double StopV,
            int StepNum,
            double StepRise,
            double StepDuration,
            double TerminationDelay,
            double SourceToTermTransient,
            double TermV,
            double TermHold )
        {
            #region Declaration of generic parameters

            int i = 0;
            double SourceV = 0;
            int rc = 0;
            string FunctionName = "CreateStairecaseSweepWaveform";

            #endregion

            #region Declaration of method dedicated parameters

            double StepV = 0.0;
            
            #endregion

            try
            {
                #region Input Parameter Check
                if (InitToStartTransient < DELTA_TIME_MIN && InitV != StartV)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " Transient between the different levels must not be 0 sec."));
                }


                if (SourceToTermTransient< DELTA_TIME_MIN && StopV != TermV)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " Transient between the different levels must not be 0 sec."));
                }

                #endregion


                #region Body of method

                #region Create waveform
    
                rc = WGFMU.createPattern(PatternName, InitV);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #region Add initial vectors

                if (!(InitHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, InitHold, InitV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(InitToStartTransient < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, InitToStartTransient, StartV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(SweepHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, SweepHold, StartV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #region Add satirecase sweep vector
                
                StepV = (StopV - StartV) / (StepNum - 1);

                for (i = 0; i < StepNum; i++)
                {
                    SourceV = StartV + StepV * i;

                    if (!(StepRise < DELTA_TIME_MIN))
                    {
                        rc = WGFMU.addVector(PatternName, StepRise, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }

                    if (!(StepDuration < DELTA_TIME_MIN))
                    {
                        rc = WGFMU.addVector(PatternName, StepDuration, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }
                }

                if (!(TerminationDelay < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, TerminationDelay, SourceV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #region Add terminating vecotor

                if (!(SourceToTermTransient < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, SourceToTermTransient, TermV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(TermHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, TermHold, TermV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region CreateRampSweepWaveform

        public void CreateRampSweepWaveform(
            string PatternName,
            double InitV,
            double InitHold,
            double InitToStartTransient,
            double SweepHold,
            double StartV,
            double StopV,
            int StepNum,
             double StepDuration,
            double TerminationDelay,
            double SourceToTermTransient,
            double TermV,
            double TermHold)
        {
            #region Declaration of generic parameters

            int i = 0;
            double SourceV = 0;
            int rc = 0;
            string FunctionName = "CreateRampSweepWaveform";

            #endregion

            #region Declaration of method dedicated parameters

            double StepV = 0.0;

            #endregion

            try
            {
                #region Input Parameter Check
                if (InitToStartTransient < DELTA_TIME_MIN && InitV != StartV)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " Transient between the different levels must not be 0 sec."));
                }

                if (SourceToTermTransient < DELTA_TIME_MIN && StopV != TermV)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " Transient between the different levels must not be 0 sec."));
                }

                #endregion

                #region Body of method

                #region Create waveform

                rc = WGFMU.createPattern(PatternName, InitV);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #region Add initial vectors

                if (!(InitHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, InitHold, InitV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(InitToStartTransient < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, InitToStartTransient, StartV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }


                #endregion

                #region Add ramp sweep vector

                if (!(SweepHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, SweepHold, StartV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                StepV = (StopV - StartV) / StepNum;


                for (i = 1; i <= StepNum; i++)
                {
                    SourceV = StartV + StepV * i;

                    if (!(StepDuration < DELTA_TIME_MIN))
                    {
                        rc = WGFMU.addVector(PatternName, StepDuration, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }
                }

                if (!(TerminationDelay < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, TerminationDelay, SourceV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #region Add terminating vecotor

                if (!(SourceToTermTransient < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, SourceToTermTransient, TermV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(TermHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, TermHold, TermV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region CreatePulseSweepWaveform

        public void CreatePulseSweepWaveform(
            string PatternName,
            double InitV,
            double InitHold,
            double InitToBaseTransient,
            double BaseV,
            double BaseHold,
            double StartV,
            double StopV,
            int StepNum,
            double Period,
            double Delay,
            double Width,
            double RiseTime,
            double FallTime,
            double TerminationDelay,
            double BaseToTermTransient,
            double TermV,
            double TermHold)
        {
            #region Declaration of generic parameters
            
            int rc = 0;

            #endregion

            #region Declaration of method dedicated parameters

            string FunctionName = "CreatePulseSweepWaveform";

            double StepV = 0.0;
            int i = 0;
            double SourceV = 0;
            double PulseDt1 = 0;
            double PulseDt2 = 0;
            double PulseDt3 = 0;
            double PulseDt4 = 0;
            double PulseDt5 = 0;

            #endregion

            try
            {
                #region Input Parameter Check
                if (InitToBaseTransient < DELTA_TIME_MIN && InitV != BaseV)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " Transient between the different levels must not be 0 sec."));
                }

                if (BaseToTermTransient < DELTA_TIME_MIN && BaseV != TermV)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " Transient between the different levels must not be 0 sec."));
                }

                #region Check pulse timing parameters

                if (RiseTime < DELTA_TIME_MIN)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Rise time must be larger than " + DELTA_TIME_MIN.ToString("E") + " s."));
                }

                if (FallTime < DELTA_TIME_MIN)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Fall time must be larger than " + DELTA_TIME_MIN.ToString("E") + " s."));
                }

                if (Width <  RiseTime)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "The pulse width must be larger than the rise time. "));
                }
                
                if ((Delay + Width + FallTime) > Period)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Delay + pulse width + fall time msut be smaller than the pulse period. "));
                }
                
                #endregion

                #endregion


                #region Body of method

                #region Create waveform

                rc = WGFMU.createPattern(PatternName, InitV);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #region Add initial vectors

                if (!(InitHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, InitHold, InitV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(InitToBaseTransient < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, InitToBaseTransient, BaseV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(BaseHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, BaseHold, BaseV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #region Add pulse sweep vector

                PulseDt1 = Delay;
                PulseDt2 = RiseTime;
                PulseDt3 = Width - RiseTime;
                PulseDt4 = FallTime;
                PulseDt5 = Period - (Delay + Width + FallTime);

                StepV = (StopV - StartV) / (StepNum - 1);
                
                for (i = 0; i < StepNum; i++)
                {
                    SourceV = StartV + StepV * i;

                    if (!(PulseDt1 < DELTA_TIME_MIN))
                    {
                        rc = WGFMU.addVector(PatternName, PulseDt1, BaseV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }

                    rc = WGFMU.addVector(PatternName, PulseDt2, SourceV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }

                    if (!(PulseDt3 < DELTA_TIME_MIN))
                    {
                        rc = WGFMU.addVector(PatternName, PulseDt3, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }

                    rc = WGFMU.addVector(PatternName, PulseDt4, BaseV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }


                    if (!(PulseDt5 < DELTA_TIME_MIN))
                    {
                        rc = WGFMU.addVector(PatternName, PulseDt5, BaseV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }

                }

                if (!(TerminationDelay < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, TerminationDelay, BaseV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #region Add terminating vecotor

                if (!(BaseToTermTransient < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, BaseToTermTransient, TermV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                if (!(TermHold < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, TermHold, TermV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region CreateSinglePulseWaveform

        public void CreateSinglePulseWaveform(
            string PatternName,
            double BaseV,
            double TopV,
            double Period,
            double Delay,
            double Width,
            double RiseTime,
            double FallTime)
        {
            #region Declaration of generic parameters
            int rc = 0;
            string FunctionName = "CreateSinglePulseWaveform";

            #endregion

            #region Declaration of method dedicated parameters

            double PulseDt1 = 0;
            double PulseDt2 = 0;
            double PulseDt3 = 0;
            double PulseDt4 = 0;
            double PulseDt5 = 0;

            #endregion

            try
            {
                #region Input Parameter Check

                #region Check pulse timing parameters

                if (RiseTime < DELTA_TIME_MIN)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Rise time must be larger than " + DELTA_TIME_MIN.ToString("E") + " s."));
                }

                if (FallTime < DELTA_TIME_MIN)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Fall time must be larger than " + DELTA_TIME_MIN.ToString("E") + " s."));
                }

                if (Width < RiseTime)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "The pulse width must be larger than the rise time. "));
                }

                if ((Delay + Width + FallTime) > Period)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Delay + pulse width + fall time msut be smaller than the pulse period. "));
                }

                #endregion

                #endregion

                #region Body of method

                #region Create waveform

                rc = WGFMU.createPattern(PatternName, BaseV);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #region Add pulsevector

                PulseDt1 = Delay;
                PulseDt2 = RiseTime;
                PulseDt3 = Width - RiseTime;
                PulseDt4 = FallTime;
                PulseDt5 = Period - (Delay + Width + FallTime);

                if (!(PulseDt1 < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, PulseDt1, BaseV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                rc = WGFMU.addVector(PatternName, PulseDt2, TopV);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                if (!(PulseDt3 < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, PulseDt3, TopV);
                   if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                rc = WGFMU.addVector(PatternName, PulseDt4, BaseV);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }
                
                if (!(PulseDt5 < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, PulseDt5, BaseV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                #endregion

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region CreateSingleFlashWaveform

        public void CreateSingleFlashWaveform(
            string PatternName,
            double BaseVBefore,
            double BaseVAfter,
            double TopV,
            double Period,
            double Delay,
            double Width,
            double RiseTime,
            double FallTime)
        {
            #region Declaration of generic parameters
            int rc = 0;
            string FunctionName = "CreateSinglePulseWaveform";

            #endregion

            #region Declaration of method dedicated parameters

            double PulseDt1 = 0;
            double PulseDt2 = 0;
            double PulseDt3 = 0;
            double PulseDt4 = 0;
            double PulseDt5 = 0;


            #endregion

            try
            {
                #region Input Parameter Check

                #region Check pulse timing parameters

                if (RiseTime < DELTA_TIME_MIN)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Rise time must be larger than " + DELTA_TIME_MIN.ToString("E") + " s."));
                }

                if (FallTime < DELTA_TIME_MIN)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Fall time must be larger than " + DELTA_TIME_MIN.ToString("E") + " s."));
                }

                if (Width < RiseTime)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "The pulse width must be larger than the rise time. "));
                }

                if ((Delay + Width + FallTime) > Period)
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Delay + pulse width + fall time msut be smaller than the pulse period. "));
                }

                #endregion

                #endregion

                #region Body of method

                #region Create waveform

                rc = WGFMU.createPattern(PatternName, BaseVBefore);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #region Add pulsevector

                PulseDt1 = Delay;
                PulseDt2 = RiseTime;
                PulseDt3 = Width - RiseTime;
                PulseDt4 = FallTime;
                PulseDt5 = Period - (Delay + Width + FallTime);


                if (!(PulseDt1 < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, PulseDt1, BaseVBefore);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                rc = WGFMU.addVector(PatternName, PulseDt2, TopV);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                if (!(PulseDt3 < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, PulseDt3, TopV);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                rc = WGFMU.addVector(PatternName, PulseDt4, BaseVAfter);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                if (!(PulseDt5 < DELTA_TIME_MIN))
                {
                    rc = WGFMU.addVector(PatternName, PulseDt5, BaseVAfter);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }


                #endregion

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region CreateDcWaveform

        public void CreateDcWaveForm    (
            string PatternName,
            double SourceV,
            double SourceDuration)
        {
            #region Declaration of generic parameters

            int rc = 0;
            string FunctionName = "CreateDcWaveform";

            #endregion

            #region Declaration of method dedicated parameters
            int PatternCount;
            double RemainDuration;
            int i;

            #endregion

            try
            {
                #region Input Parameter Check

                #region Check pulse timing parameters

                #endregion

                #endregion

                #region Body of method

                #region Create waveform

                rc = WGFMU.createPattern(PatternName, SourceV);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }
                if (SourceDuration <= DELTA_TIME_MAX)
                {
                    if (SourceDuration >= DELTA_TIME_MIN)
                    {
                        rc = WGFMU.addVector(PatternName, SourceDuration, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }
                }

                else
                {
                    PatternCount = (int)Math.Floor(SourceDuration / DELTA_TIME_MAX);
                    RemainDuration = SourceDuration - DELTA_TIME_MAX * PatternCount;

                    for (i = 0; i < PatternCount; i++)
                    {
                        rc = WGFMU.addVector(PatternName, DELTA_TIME_MAX, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }                    
                    
                    }
                    if (RemainDuration >= DELTA_TIME_MIN)
                    {
                        rc = WGFMU.addVector(PatternName, RemainDuration, SourceV);
                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }
                    }
                }

                #endregion

                #endregion

            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region CreateDcStressWaveformInfo
        /*
         * Since maximum dt is 10,000 seconds, DC stress vector can be written as a single pattern and
         * it can be created by the CrateStepWaveform.
         */
        #endregion

        #region CrateAcStressWavefformInfo
        /*
         * Since the maximum loop count is up to 1,000,000,000,000, the AC stress can be described as 
         * a repeatetion of the single pulse.
         */
        #endregion

        #region CreateALWGWaveform
        public void CreateALWGWaveform(string PatternName, ref double[] Time, ref double[] Level)
        {
            int Size;
            int rc = 0;
            string FunctionName = "CreateALWGWaveform";
            try
            {
                Size = Time.Length;
                rc = WGFMU.createPattern(PatternName, Level[0]);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                rc = WGFMU.setVectors(PatternName, Time, Level, Size);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #endregion

        #region Set Measurement Event Vector

        #region SetSamplineMeasEvent
        public void SetSamplingMeasEvent(
            string PatternName,
            string MeasEventName,
            int SamplingMode,
            double InitialInterval,
            int SamplingPoints,
            double AveragingTime,
            double MeasurementDelay,
            int RawData)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "SetSamplingMeasEvent";

            #endregion

            #region Declaration of method dedicated parameters
            int i = 0;
            double TimeOfMeas = 0;
            double LogBase = 10;
            double NumInDecade = 0;
            double CountResolution = 0;
            double PowerFactor = 0;
            int RawDataStat;
            double MinimumInterval = 0.0;

            #endregion

            try
            {
                #region Input Parameter Check
                if (!(SamplingMode == 0 || SamplingMode == 1 || SamplingMode == 2))
                {
                    rc = WGFMU_SAMPLE_LIB_ERROR;
                    throw (new WGFMULibException(rc, " Sampling mode must be 0: Linear, 1: Log10 or 2: Log25."));
                }

                // Other parameters are checked by the instrument library of 

                #endregion


                #region Body of method


                #region Add measurement event
                if (SamplingMode == 0)
                {

                    RawDataStat = RawData;
                    rc = WGFMU.setMeasureEvent( PatternName,
                                                MeasEventName,
                                                MeasurementDelay,
                                                SamplingPoints,
                                                InitialInterval,
                                                AveragingTime,
                                                RawDataStat);


                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }


                }
                else
                {
                    if (SamplingMode == 1)
                    {
                        NumInDecade = 10;
                    }
                    else if (SamplingMode == 2)
                    {
                        NumInDecade = 25;
                    }
                    CountResolution = 1.0 / NumInDecade;
                    
                    MinimumInterval = InitialInterval * (Math.Pow(LogBase, CountResolution) - 1);
                    
                    MinimumInterval = 10E-9 * (Math.Round(MinimumInterval / 10E-9));

                    if (MinimumInterval < AveragingTime)
                    { 
                        throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "Minimum interval, " + this.DoubleToTrimedString(MinimumInterval) + ", must be larger than the averaging time"));
                    
                    }
                    for (i = 0; i < SamplingPoints; i++)
                    {

                        PowerFactor = CountResolution * (i);
                        TimeOfMeas = MeasurementDelay + InitialInterval * (Math.Pow(LogBase, PowerFactor) -1 );

                        RawDataStat = RawData;
                        rc = WGFMU.setMeasureEvent(
                                PatternName,
                                MeasEventName,
                                TimeOfMeas,
                                1,
                                AveragingTime,
                                AveragingTime,
                                RawDataStat);


                        if (rc != WGFMU.NO_ERROR)
                        {
                            throw (new WGFMULibException(rc, ""));
                        }                  
                    }

                }

                #endregion



                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }
        #endregion

        #region SetSweepMeasEvent

        public void SetSweepMeasEvent(
            string PatternName,
            string MeasEventName,
            double Hold,
            int StepNum,
            double MeasurementDelay,
            double AveragingTime,
            double StepDelay,
            int RawData)
        {
            #region Declaration of generic parameters

            int rc = 0;
            string FunctionName = " CreateSweepMeasEvent";

            #endregion

            #region Declaration of method dedicated parameters
            
            double MeasTime;
            double MeasInterval;
            int MeasPoints;

            #endregion

            try
            {
                #region Input Parameter Check

                // Other parameters are checked by the instrument library of 

                #endregion


                #region Body of method

                #region Add measurement event

                MeasTime = Hold + MeasurementDelay;
                MeasInterval = MeasurementDelay + AveragingTime + StepDelay;
                MeasPoints = StepNum;

                rc = WGFMU.setMeasureEvent(
                    PatternName,
                    MeasEventName,
                    MeasTime,
                    MeasPoints,
                    MeasInterval,
                    AveragingTime,
                    RawData);
                
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #endregion



                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
            
        }

        #endregion

        #region SetMeasRangeEvent
        public void SetMeasRangeEvent(
            string PatternName,
            string RangeEventName,
            double TimetoChange,
            int RangeIndex
            )
        {

            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "SetMeasRangeEvent";

            #endregion

            #region Declaration of method dedicated parameters

            #endregion

            try
            {
                #region Input Parameter Check

                // Other parameters are checked by the instrument library of 

                #endregion


                #region Body of method

                #region Add range change event

                rc = WGFMU.setRangeEvent(PatternName, RangeEventName, TimetoChange, RangeIndex);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        
        }

        #endregion

        #endregion

        #region GetPatternDuration

        public void GetPatternDuration(string PatternName, out double PatternDuraiton)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "GetPatternDuration";

            #endregion

            #region Declaration of method dedicated parameters
            
            int Size = 0;
            double[] ForceTimes;
            double[] ForceValues;

            #endregion

            try
            {
                #region Input Parameter Check

                // Other parameters are checked by the instrument library of 

                #endregion


                #region Body of method

                #region Get pattern force values

                rc = WGFMU.getPatternForceValueSize(PatternName, ref Size);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }
                ForceTimes = new double[Size];
                ForceValues = new double[Size];

                rc = WGFMU.getPatternForceValues(PatternName, 0, ref Size, ForceTimes, ForceValues);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                PatternDuraiton = ForceTimes[ForceTimes.Length - 1];

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }
        #endregion

        #endregion

        #region Sequence handling

        #region AddSequence
        public void AddSequence(
            int ChannelId,
            string PatternName,
            double LoopCount)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "AddSequence";

            #endregion

            #region Declaration of method dedicated parameters

            #endregion

            try
            {
                #region Input Parameter Check

                // Other parameters are checked by the instrument library of 

                #endregion


                #region Body of method

                #region Add range change event

                rc = WGFMU.addSequence(ChannelId, PatternName, LoopCount);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        }
        #endregion
        #endregion

        #region Debugging

        #region ExportAscii

        public void ExportAscii(
            string FileName)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "ExportAscii";

            #endregion

            #region Declaration of method dedicated parameters

            #endregion

            try
            {
                #region Input Parameter Check

                // Other parameters are checked by the instrument library of 

                #endregion


                #region Body of method

                #region Add range change event

                WGFMU.exportAscii(FileName);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        }

        #endregion

        #region GetPatternData
        public void GetPatternData(int ChNum, out double[] ForceTime, out double[] ForceValue, out double [] MeasTime)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "GetPatternData";

            #endregion

            #region Declaration of method dedicated parameters

            int MeasPatternSize = 0;
            double ForcePatternSize = 0;
            int Size;

            #endregion

            try
            {
                #region Input Parameter Check

                // Other parameters are checked by the instrument library of 

                #endregion

                #region Body of method

                #region Add range change event

               rc =  WGFMU.getMeasureTimeSize( ChNum, ref MeasPatternSize);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }
                MeasTime = new double[MeasPatternSize];

                if (MeasPatternSize != 0)
                {
                    rc = WGFMU.getMeasureTimes(ChNum, 0, ref MeasPatternSize, MeasTime);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                }

                rc = WGFMU.getForceValueSize(ChNum, ref ForcePatternSize);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }


                Size = (int)ForcePatternSize;
                ForceTime= new double[Size];
                ForceValue = new double[Size];
                
                rc = WGFMU.getForceValues(ChNum, 0, ref Size, ForceTime, ForceValue);
                if (rc != WGFMU.NO_ERROR)
                {
                    throw (new WGFMULibException(rc, ""));
                }

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        
        }

        #endregion

        #region ExportPatternData
        private void SavePatternData( String FileName, 
                                        ref int[] ChannelList, 
                                        ref double[][] ForceTime, 
                                        ref double[][] ForceValue, 
                                        ref double[][] MeasTime)
        {
            int MaxIndex = 0;
            int i, j, k;
            int ArrayLength;
            int CountStart;
            string OutputString = "";
            StreamWriter sw = new StreamWriter(FileName);
            double[][] MeasValue = new double[MeasTime.Length][];
            string FunctionName = "ExportPatternData()";

            try
            {
                for (i = 0; i < MeasTime.Length; i++)
                {
                    if (MeasTime[i] != null)
                    {
                        ArrayLength = MeasTime[i].Length;
                    }
                    else
                    {
                        ArrayLength = 0;
                    }
                    MeasValue[i] = new double[ArrayLength];
                }


                for (k = 0; k < ChannelList.Length; k++)
                {
                    CountStart = 0;
                    if (MeasTime[k] != null)
                    {
                        for (j = 0; j < ForceTime[k].Length - 1; j++)
                        {
                            for (i = CountStart; i < MeasTime[k].Length; i++)
                            {
                                if (MeasTime[k][i] == ForceTime[k][j])
                                {
                                    MeasValue[k][i] = ForceValue[k][j];
                                }
                                else if (MeasTime[k][i] > ForceTime[k][j] && MeasTime[k][i] < ForceTime[k][j + 1])
                                {
                                    MeasValue[k][i] = ForceValue[k][j]
                                                      + (ForceValue[k][j + 1] - ForceValue[k][j]) / (ForceTime[k][j + 1] - ForceTime[k][j])
                                                      * (MeasTime[k][i] - ForceTime[k][j]);
                                }
                                else
                                {
                                    CountStart = i;
                                    break;
                                }
                            }

                        }
                    }
                }

                for (i = 0; i < ForceTime.Length; i++)
                {
                    if (ForceTime[i] != null)
                    {
                        if (MaxIndex < ForceTime[i].Length)
                        {
                            MaxIndex = ForceTime[i].Length;
                        }
                    }
                }


                for (i = 0; i < MeasTime.Length; i++)
                {
                    if (MeasTime[i] != null)
                    {
                        if (MaxIndex < MeasTime[i].Length)
                        {
                            MaxIndex = MeasTime[i].Length;
                        }
                    }
                }


                OutputString = "";
                for (i = 0; i < ChannelList.Length; i++)
                {
                    OutputString = OutputString
                                + ChannelList[i].ToString() + " Force Time (s),"
                                + ChannelList[i].ToString() + " Force Value,"
                                + ChannelList[i].ToString() + " Measure Time (s),"
                                + ChannelList[i].ToString() + " Value at Measre,";

                }

                sw.WriteLine(OutputString);

                for (i = 0; i < MaxIndex; i++)
                {
                    OutputString = "";
                    for (j = 0; j < ChannelList.Length; j++)
                    {
                        if (MeasTime[j] != null)
                        {
                            if (i < ForceTime[j].Length && i < MeasTime[j].Length)
                            {
                                OutputString = OutputString
                                            + DoubleToTrimedString(ForceTime[j][i]) + ","
                                            + ForceValue[j][i].ToString("E") + ","
                                            + DoubleToTrimedString(MeasTime[j][i]) + ","
                                            + MeasValue[j][i].ToString("E") + ",";

                            }
                            else if (i < ForceTime[j].Length && i >= MeasTime[j].Length)
                            {
                                OutputString = OutputString
                                            + DoubleToTrimedString(ForceTime[j][i]) + ","
                                            + ForceValue[j][i].ToString("E") + ",,,";
                            }
                            else if (i >= ForceTime[j].Length && i < MeasTime[j].Length)
                            {
                                OutputString = OutputString
                                            + ",,"
                                            +DoubleToTrimedString(MeasTime[j][i]) + ","
                                            + MeasValue[j][i].ToString("E") + ",";
                            }
                            else
                            {
                                OutputString = OutputString
                                               + ",,,,";
                            }
                        }

                    }
                    sw.WriteLine(OutputString);

                }

                sw.Close();
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }        

        }

        public void ExportWavefomrData(ref int[] ChannelList, string FileName)
        {
            #region Declaration of generic parameters

            string FunctionName = "ExportPatternData";

            #endregion

            #region Declaration of method dedicated parameters

            int i;
            double[][] MeasTime = new double[ChannelList.Length][];
            double[][] ForceValue = new double[ChannelList.Length][];
            double[][] ForceTime = new double[ChannelList.Length][];

            #endregion

            try
            {
                #region Input Parameter Check

                // Other parameters are checked by the instrument library of 

                #endregion

                #region Body of method
                for (i = 0; i < ChannelList.Length; i++)
                {
                    GetPatternData(ChannelList[i], out ForceTime[i], out ForceValue[i], out MeasTime[i]);
                }

                SavePatternData(FileName, ref ChannelList, ref ForceTime, ref ForceValue, ref MeasTime);

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }       
        
        }
        #endregion

        #region ValidatePattern
        public void ValidatePattern(ref int[] ChannelList)
        {
            string FunctionName = "ValidatePattern()";
            string FileType = "TEXT"; ;
            string FileName = "";

            DialogResult Result = new DialogResult() ;

            SAVEFILEMODE svfm = new SAVEFILEMODE();
            
            Result = svfm.ShowDialog();

            try
            {
                if (Result == DialogResult.OK)
                {
                    this.GetSaveFileName(FileType, out FileName);
                    if (FileName != "")
                    {
                        if (svfm.ExportMode == 0)
                        {
                            this.ExportAscii(FileName);
                        }
                        else if (svfm.ExportMode == 1)
                        {
                            this.ExportWavefomrData(ref ChannelList, FileName);
                        }
                    }
                }
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }           

        }
        #endregion

        #endregion

        #region Create vector of the measurement application

        #region CreateStairecaseSweepMeasurement

        #region CreateStairecaseSweepSource

        public void CreateStaircaseSweepSource(
            int MeasEnable,
            string PatternName,
            string MeasEventName,
            double OutputSequenceDelay,
            double InitV,
            double InitHold,
            double InitToStartTransient,
            double Hold,
            double StartV,
            double StopV,
            int StepNum,
            double StepRise,
            double MeasDelay,
            double AveragingTime,
            double StepDelay,
            double TerminationDelay,
            double TermV,
            double StopToTermTransient,
            double TermHold,
            int EnableDualSweep,
            double DualSweepDelay,
            int RawData,
            ref double MeasOrigin)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "CreateStaircaseSweepSource";

            #endregion

            #region Declaration of method dedicated parameters

            double SweepInitV;
            double SweepInitHold;
            double SweepInitToStartTransition;
            double SweepHold;
            double SweepStartV;
            double SweepStopV;
            int SweepStepNum;
            double SweepStepRise;
            double SweepStepDuration;
            double SweepStepDelay;
            double SweepTerminationDelay;
            double SweepStopToTermTransient;
            double SweepTermV;
            double SweepTermHold;
            double SweepMeasDelay;
            double SweepAveragingTime;
            double SweepMeasHold;
            string FwdSweepName = "";
            string FwdSweepMeasName = "";
            string RevSweepName = "";
            string RevSweepMeasName = "";

            #endregion

            try
            {
                #region Input Parameter Check

                if (!(Hold >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Hold must be larger than 0 sec."));
                }

                if (!(StepNum >= 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Step number of the sweep must be larger than 1."));
                }

                if (!(StepRise >= DELTA_TIME_MIN))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "StepRise must be equal or larger than " + DELTA_TIME_MIN.ToString("E")));
                }

                if (!(MeasDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "StepDelay must be equal or larger than 0 sec."));
                }

                if (!(EnableDualSweep == 0 || EnableDualSweep == 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "DualSweep must 0:Disable or 1:Enable."));
                }

                if (!(DualSweepDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " DualSweepDelay must be larger than 0 sec."));
                }

                // Other parameters are checked by the instrument library of 

                #endregion

                #region Body of method

                #region Create single and primary only sweep

                if (EnableDualSweep == 0)
                {
                    FwdSweepName = PatternName;
                    FwdSweepMeasName = MeasEventName;
                    SweepInitV = InitV;
                    SweepInitHold = InitHold;
                    SweepInitToStartTransition = InitToStartTransient;
                    SweepStartV = StartV;
                    SweepStopV = StopV;
                    SweepStepNum = StepNum;
                    SweepStepRise = StepRise;
                    SweepStepDuration = (MeasDelay + AveragingTime + StepDelay) - StepRise;
                    SweepStepDelay = StepDelay;
                    SweepTermV = TermV;
                    SweepStopToTermTransient = StopToTermTransient;

                    if (OutputSequenceDelay >= 0)
                    {
                        SweepInitHold = InitHold;
                        SweepHold = OutputSequenceDelay + Hold;
                        SweepMeasHold = InitHold + InitToStartTransient + OutputSequenceDelay + Hold;
                        SweepTerminationDelay = TerminationDelay + OutputSequenceDelay;
                        SweepTermHold = TermHold;
                    }
                    else
                    {
                        SweepInitHold = InitHold + (-1.0 * OutputSequenceDelay); ;
                        SweepHold = Hold;
                        SweepMeasHold = InitHold + InitToStartTransient - OutputSequenceDelay + Hold;
                        SweepTerminationDelay = TerminationDelay;
                        SweepTermHold = TermHold + -1.0 * OutputSequenceDelay;

                    }

                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;
                    
                    #region Create sweep pattern

                    CreateStairecaseSweepWaveform(
                        FwdSweepName,
                        SweepInitV,
                        SweepInitHold,
                        SweepInitToStartTransition,
                        SweepHold,
                        SweepStartV,
                        SweepStopV,
                        SweepStepNum,
                        SweepStepRise,
                        SweepStepDuration,
                        SweepTerminationDelay,
                        SweepStopToTermTransient,
                        SweepTermV,
                        SweepTermHold);

                    #endregion

                    #region Set measurement event
                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            FwdSweepName,
                            FwdSweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);

                        MeasOrigin = SweepMeasHold;
                    }
                    #endregion
                }

                #endregion

                #region Create dual and primary only sweep

                if (EnableDualSweep == 1)
                {
                    #region Fowrad sweep
                    FwdSweepName = "Fwd" + PatternName;
                    FwdSweepMeasName = "Fwd" + MeasEventName;
                    SweepInitV = InitV;
                    SweepInitHold = InitHold;
                    SweepInitToStartTransition = InitToStartTransient;
                    SweepStartV = StartV;
                    SweepStopV = StopV;
                    SweepStepNum = StepNum;
                    SweepStepRise = StepRise;
                    SweepStepDuration = (MeasDelay + AveragingTime + StepDelay) - StepRise;
                    SweepStepDelay = StepDelay;
                    SweepTermV = StopV;
                    SweepTermHold = 0;
                    SweepStopToTermTransient = 0;

                    if (OutputSequenceDelay >= 0)
                    {
                        SweepInitHold = InitHold;
                        SweepHold = OutputSequenceDelay + Hold;
                        SweepMeasHold = InitHold + InitToStartTransient + OutputSequenceDelay + Hold;
                        SweepTerminationDelay = 0.0;
                    }
                    else
                    {
                        SweepInitHold = InitHold + (-1.0 * OutputSequenceDelay); ;
                        SweepHold = Hold;
                        SweepMeasHold = InitHold + InitToStartTransient - OutputSequenceDelay + Hold;
                        SweepTerminationDelay = 0.0;

                    }

                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;

                    #region Create sweep pattern

                    CreateStairecaseSweepWaveform(
                        FwdSweepName,
                        SweepInitV,
                        SweepInitHold,
                        SweepInitToStartTransition,
                        SweepHold,
                        SweepStartV,
                        SweepStopV,
                        SweepStepNum,
                        SweepStepRise,
                        SweepStepDuration,
                        SweepTerminationDelay,
                        SweepStopToTermTransient,
                        SweepTermV,
                        SweepTermHold);

                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            FwdSweepName,
                            FwdSweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);

                        MeasOrigin = SweepMeasHold;
                    }

                    #endregion

                    #endregion

                    #region Reverse sweep

                    RevSweepName = "Rev" + PatternName;
                    RevSweepMeasName = "Rev" + MeasEventName;
                    SweepInitV = StopV;
                    SweepInitHold = 0;
                    SweepInitToStartTransition = 0;
                    SweepStartV = StopV;
                    SweepStopV = StartV;
                    SweepStepNum = StepNum;
                    SweepStepRise = StepRise;
                    SweepStepDuration = (MeasDelay + AveragingTime + StepDelay) - StepRise;
                    SweepStepDelay = StepDelay;
                    SweepTermV = TermV;
                    SweepTermHold = TermHold;
                    SweepStopToTermTransient = StopToTermTransient;

                    if (OutputSequenceDelay >= 0)
                    {
                        SweepInitHold = 0;
                        SweepHold = DualSweepDelay;
                        SweepTerminationDelay = TerminationDelay + OutputSequenceDelay;
                        SweepTermHold = TermHold;
                    }
                    else
                    {
                        SweepInitHold = 0.0;
                        SweepHold = DualSweepDelay;
                        SweepTerminationDelay = TerminationDelay;
                        SweepTermHold = TermHold - 1.0 * OutputSequenceDelay;

                    }

                    SweepMeasHold = DualSweepDelay;
                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;

                    #region Create sweep pattern

                    CreateStairecaseSweepWaveform(
                        RevSweepName,
                        SweepInitV,
                        SweepInitHold,
                        SweepInitToStartTransition,
                        SweepHold,
                        SweepStartV,
                        SweepStopV,
                        SweepStepNum,
                        SweepStepRise,
                        SweepStepDuration,
                        SweepTerminationDelay,
                        SweepStopToTermTransient,
                        SweepTermV,
                        SweepTermHold);

                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            RevSweepName,
                            RevSweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }

                    #endregion

                    #endregion

                    #region Merging forward and reverse sweep

                    rc = WGFMU.createMergedPattern(PatternName, FwdSweepName, RevSweepName, WGFMU.AXIS_TIME);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }                    
                    #endregion 

                }

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region CreateStairecaseConstantSource

        public void CreateStaircaseConstantSource(
            int MeasEnable,
            string PatternName,
            string MeasEventName,
            double OutputSequenceDelay,
            double InitV,
            double InitHold,
            double InitToBaseTransient,
            double BaseV,
            double BasetoSourceTransient,
            double Hold,
            double SourceV,
            int StepNum,
            double StepRise,
            double MeasDelay,
            double AveragingTime,
            double StepDelay,
            double TerminationDelay,
            double SourceToTermTransient,
            double TermV,
            double TermHold,
            int EnableDualSweep,
            double DualSweepDelay,
            int RawData)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "CreateStaircaseConstantSource";

            #endregion

            #region Declaration of method dedicated parameters

            int SweepStepNum;
            double SweepStepRise;
            double SweepStepDuration;
            double SweepStepDelay;

            double SweepTermHold;

            double ConstInitV;
            double ConstInitHold;
            double ConstInitToBaseTransient;
            double ConstBaseV;
            double ConstBaseHold;
            double ConstBaseToSourceTransient;
            double ConstSourceV;
            double ConstSourceDuration;
            double ConstTerminationDelay;
            double ConstSourceToTermTransient;
            double ConstTermV;
            double ConstTermHold;

            double SweepMeasDelay;
            double SweepAveragingTime;

            double SweepMeasHold;

            string SweepName = "";
            string SweepMeasName = "";


            #endregion

            try
            {
                #region Input Parameter Check

                if (!(Hold >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Hold must be larger than 0 sec."));
                }

                if (!(StepNum >= 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Step number of the sweep must be larger than 1."));
                }

                if (!(StepRise >= DELTA_TIME_MIN))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "StepRise must be equal or larger than " + DELTA_TIME_MIN.ToString("E")));
                }

                if (!(MeasDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "StepDelay must be equal or larger than 0 sec."));
                }

                if (!(EnableDualSweep == 0 || EnableDualSweep == 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "DualSweep must 0:Disable or 1:Enable."));
                }

                if (!(DualSweepDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " DualSweepDelay must be larger than 0 sec."));
                }

                // Other parameters are checked by the instrument library of 

                #endregion

                #region Body of method

                #region Create single and primary only sweep

                if (EnableDualSweep == 0)
                {
                    SweepName = PatternName;
                    SweepMeasName = MeasEventName;
     
                    SweepStepNum = StepNum;
                    SweepStepRise = StepRise;
                    SweepStepDelay = StepDelay;
                    SweepTermHold = TermHold;

                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;

                    ConstInitV = InitV;
                    ConstInitHold = InitHold;
                    ConstInitToBaseTransient = InitToBaseTransient;
                    ConstBaseV = BaseV;
                    ConstSourceV = SourceV;
                    
                    if (OutputSequenceDelay >= 0)
                    {
                        ConstBaseHold = OutputSequenceDelay;
                        SweepMeasHold = InitHold + InitToBaseTransient + BasetoSourceTransient + OutputSequenceDelay + Hold;
                        ConstTerminationDelay = TerminationDelay;
                        ConstTermHold = TermHold + OutputSequenceDelay;
                        ConstSourceDuration = Hold + (MeasDelay + AveragingTime + StepDelay) * StepNum + ConstTerminationDelay;
                    }
                    else
                    {
                        ConstBaseHold = 0.0;
                        SweepMeasHold = InitHold + InitToBaseTransient + BasetoSourceTransient - OutputSequenceDelay + Hold;
                        ConstTerminationDelay = TerminationDelay + -1.0 * OutputSequenceDelay;
                        ConstTermHold = TermHold;
                        ConstSourceDuration = -1.0 * OutputSequenceDelay+ Hold + (MeasDelay + AveragingTime + StepDelay) * StepNum + ConstTerminationDelay;
                    }

                    ConstBaseToSourceTransient = StepRise;
                    ConstSourceToTermTransient = SourceToTermTransient;
                    ConstTermV = TermV;
            

                    #region Create step pattern

                    CreateStepWaveform(
                        SweepName,
                        ConstInitV,
                        ConstInitHold,
                        ConstInitToBaseTransient,
                        ConstBaseV,
                        ConstBaseHold,
                        ConstBaseToSourceTransient,
                        ConstSourceV,
                        ConstSourceDuration,
                        ConstSourceToTermTransient,
                        ConstTermV,
                        ConstTermHold);
                        

                    #endregion

                    #region Set measurement event
                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }
                    #endregion
                }

                #endregion

                #region Create dual and primary only sweep

                if (EnableDualSweep == 1)
                {
                    #region Fowrad sweep
                    SweepName = "Fwd" + PatternName;
                    SweepMeasName = "Fwd" + MeasEventName;

                    SweepStepNum = StepNum;
                    SweepStepRise = StepRise;
                    SweepStepDuration = (MeasDelay + AveragingTime + StepDelay) - StepRise;
                    SweepStepDelay = StepDelay;

                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;

                    ConstInitV = InitV;
                    ConstInitHold = InitHold;
                    ConstInitToBaseTransient = InitToBaseTransient;
                    ConstBaseV = BaseV;
                    ConstSourceV = SourceV;

                    if (OutputSequenceDelay >= 0)
                    {
                        ConstBaseHold = OutputSequenceDelay;
                        SweepMeasHold = InitHold + InitToBaseTransient + BasetoSourceTransient + Hold + OutputSequenceDelay;
                        ConstTerminationDelay = 0.0;
                        ConstSourceDuration = Hold + (MeasDelay + AveragingTime + StepDelay) * StepNum + ConstTerminationDelay;
                    }
                    else
                    {
                        ConstBaseHold = 0.0;
                        SweepMeasHold = InitHold + InitToBaseTransient + BasetoSourceTransient + Hold - OutputSequenceDelay;
                        ConstTerminationDelay = 0.0;
                        ConstSourceDuration = -1.0 * OutputSequenceDelay + Hold + (MeasDelay + AveragingTime + StepDelay) * StepNum + ConstTerminationDelay;
                    }

                    ConstBaseToSourceTransient = StepRise;
                    ConstSourceToTermTransient = 0.0;
                    ConstTermV = SourceV;
                    ConstTermHold = 0;
 
                    #region Create sweep pattern

                    CreateStepWaveform(
                        SweepName,
                        ConstInitV,
                        ConstInitHold,
                        ConstInitToBaseTransient,
                        ConstBaseV,
                        ConstBaseHold,
                        ConstBaseToSourceTransient,
                        ConstSourceV,
                        ConstSourceDuration,
                        ConstSourceToTermTransient,
                        ConstTermV,
                        ConstTermHold);
                        
                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }

                    #endregion

                    #endregion

                    #region Reverse sweep

                    SweepName = "Rev" + PatternName;
                    SweepMeasName = "Rev" + MeasEventName;

                    SweepStepNum = StepNum;
                    SweepStepRise = StepRise;
                    SweepStepDelay = StepDelay;

                    SweepMeasHold = DualSweepDelay;
                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;

                    ConstInitV = SourceV;
                    ConstInitHold = 0;
                    ConstInitToBaseTransient = 0;
                    ConstBaseV = SourceV;
                    ConstSourceV = SourceV;

                    if (OutputSequenceDelay >= 0)
                    {
                        ConstBaseHold = 0.0;
                        ConstTerminationDelay = TerminationDelay;
                        ConstTermHold = TermHold + OutputSequenceDelay;
                    }
                    else
                    {
                        ConstBaseHold = 0.0;
                        ConstTerminationDelay = TerminationDelay + -1.0 * OutputSequenceDelay;
                        ConstTermHold = TermHold;
                    }
                    ConstBaseToSourceTransient = 0.0;
                    ConstSourceDuration = DualSweepDelay + (MeasDelay + AveragingTime + StepDelay) * StepNum + ConstTerminationDelay;
                    ConstSourceToTermTransient = SourceToTermTransient;
                    ConstTermV = TermV;

                    #region Create sweep pattern

                    CreateStepWaveform(
                        SweepName,
                        ConstInitV,
                        ConstInitHold,
                        ConstInitToBaseTransient,
                        ConstBaseV,
                        ConstBaseHold,
                        ConstBaseToSourceTransient,
                        ConstSourceV,
                        ConstSourceDuration,
                        ConstSourceToTermTransient,
                        ConstTermV,
                        ConstTermHold);

                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }


                    #endregion

                    #endregion

                    #region Merging forward and reverse sweep

                    rc = WGFMU.createMergedPattern(PatternName, "Fwd" + PatternName, "Rev" + PatternName, WGFMU.AXIS_TIME);

                    #endregion

                }

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #endregion

        #region CreateRampSweepMeasurement

        #region CreateRampSweepSource

        public void CreateRampSweepSource(
            int MeasEnable,
            string PatternName,
            string MeasEventName,
            double OutputSequenceDelay,
            double InitV,
            double InitHold,
            double InitToStartTransient,
            double Hold,
            double StartV,
            double StopV,
            int StepNum,
            double StepRise,
            double MeasDelay,
            double AveragingTime,
            double StepDelay,
            double TerminationDelay,
            double TermV,
            double StopToTermTransient,
            double TermHold,
            int EnableDualSweep,
            double DualSweepDelay,
            int RawData,
            ref double MeasOrigin)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "CreateRampSweepSource";

            #endregion

            #region Declaration of method dedicated parameters

            double SweepInitV;
            double SweepInitHold;
            double SweepInitToStartTransition;
            double SweepHold;
            double SweepStartV;
            double SweepStopV;
            int SweepStepNum;
            double SweepStepRise;
            double SweepStepDuration;
            double SweepStepDelay;
            double SweepTerminationDelay;
            double SweepStopToTermTransient;
            double SweepTermV;
            double SweepTermHold;
            double SweepMeasDelay;
            double SweepAveragingTime;
            double SweepMeasHold;
            string SweepName = "";
            string SweepMeasName = "";


            #endregion

            try
            {
                #region Input Parameter Check

                if (!(Hold >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Hold must be larger than 0 sec."));
                }

                if (!(StepNum >= 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Step number of the sweep must be larger than 1."));
                }

                if (!(StepRise >= DELTA_TIME_MIN))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "StepRise must be equal or larger than " + DELTA_TIME_MIN.ToString("E")));
                }

                if (!(MeasDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "StepDelay must be equal or larger than 0 sec."));
                }

                if (!(EnableDualSweep == 0 || EnableDualSweep == 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "DualSweep must 0:Disable or 1:Enable."));
                }

                if (!(DualSweepDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " DualSweepDelay must be larger than 0 sec."));
                }


                // Other parameters are checked by the instrument library of 

                #endregion

                #region Body of method

                #region Create single and primary only sweep

                if (EnableDualSweep == 0 )
                {
                    SweepName = PatternName;
                    SweepMeasName = MeasEventName;
                    SweepInitV = InitV;
                    SweepInitHold = InitHold;
                    SweepInitToStartTransition = InitToStartTransient;
                    SweepStartV = StartV;
                    SweepStopV = StopV;
                    SweepStepNum = StepNum;
                    SweepStepRise = StepRise;
                    SweepStepDuration = (MeasDelay + AveragingTime + StepDelay);
                    SweepStepDelay = StepDelay;
                    SweepTermV = TermV;
                    SweepStopToTermTransient = StopToTermTransient;

                    if (OutputSequenceDelay >= 0)
                    {
                        SweepInitHold = InitHold;
                        SweepHold = OutputSequenceDelay + Hold;
                        SweepMeasHold = InitHold + InitToStartTransient + OutputSequenceDelay + Hold;
                        SweepTerminationDelay = TerminationDelay + OutputSequenceDelay;
                        SweepTermHold = TermHold;
                    }
                    else
                    {
                        SweepInitHold = InitHold + (-1.0 * OutputSequenceDelay); ;
                        SweepHold = Hold;
                        SweepMeasHold = InitHold + InitToStartTransient - OutputSequenceDelay + Hold;
                        SweepTerminationDelay = TerminationDelay;
                        SweepTermHold = TermHold + -1.0 * OutputSequenceDelay;

                    }

                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;

                    #region Create sweep pattern
                    CreateRampSweepWaveform(
                        SweepName,
                        SweepInitV,
                        SweepInitHold,
                        SweepInitToStartTransition,
                        SweepHold,
                        SweepStartV,
                        SweepStopV,
                        SweepStepNum,
                        SweepStepDuration,
                        SweepTerminationDelay,
                        SweepStopToTermTransient,
                        SweepTermV,
                        SweepTermHold);

                    MeasOrigin = SweepMeasHold;

                    #endregion

                    #region Set measurement event
                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }
                    #endregion
                }

                #endregion

                #region Create dual and primary only sweep

                if (EnableDualSweep == 1 )
                {
                    #region Fowrad sweep
                    SweepName = "Fwd" + PatternName;
                    SweepMeasName = "Fwd" + MeasEventName;
                    SweepInitV = InitV;
                    SweepInitHold = InitHold;
                    SweepInitToStartTransition = InitToStartTransient;
                    SweepStartV = StartV;
                    SweepStopV = StopV;
                    SweepStepNum = StepNum;
                    SweepStepRise = StepRise;
                    SweepStepDuration = (MeasDelay + AveragingTime + StepDelay) ;
                    SweepStepDelay = StepDelay;
                    SweepTermV = StopV;
                    SweepTermHold = 0;
                    SweepStopToTermTransient = 0;

                    if (OutputSequenceDelay >= 0)
                    {
                        SweepInitHold = InitHold;
                        SweepHold = OutputSequenceDelay + Hold;
                        SweepMeasHold = InitHold + InitToStartTransient + OutputSequenceDelay + Hold;
                        SweepTerminationDelay = 0.0;
                    }
                    else
                    {
                        SweepInitHold = InitHold + (-1.0 * OutputSequenceDelay); ;
                        SweepHold = Hold;
                        SweepMeasHold = InitHold + InitToStartTransient - OutputSequenceDelay + Hold;
                        SweepTerminationDelay = 0.0;

                    }

                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;

                    #region Create sweep pattern

                    CreateRampSweepWaveform(
                        SweepName,
                        SweepInitV,
                        SweepInitHold,
                        SweepInitToStartTransition,
                        SweepHold,
                        SweepStartV,
                        SweepStopV,
                        SweepStepNum,
                        SweepStepDuration,
                        SweepTerminationDelay,
                        SweepStopToTermTransient,
                        SweepTermV,
                        SweepTermHold);

                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                        MeasOrigin = SweepMeasHold;
                    }

                    #endregion

                    #endregion

                    #region Reverse sweep

                    SweepName = "Rev" + PatternName;
                    SweepMeasName = "Rev" + MeasEventName;
                    SweepInitV = StopV;
                    SweepInitHold = 0;
                    SweepInitToStartTransition = 0;
                    SweepStartV = StopV;
                    SweepStopV = StartV;
                    SweepStepNum = StepNum;
                    SweepStepRise = StepRise;
                    SweepStepDuration = (MeasDelay + AveragingTime + StepDelay) ;
                    SweepStepDelay = StepDelay;
                    SweepTermV = TermV;
                    SweepTermHold = TermHold;
                    SweepStopToTermTransient = StopToTermTransient;

                    if (OutputSequenceDelay >= 0)
                    {
                        SweepInitHold = 0;
                        SweepHold = DualSweepDelay;
                        SweepTerminationDelay = TerminationDelay + OutputSequenceDelay;
                        SweepTermHold = TermHold;
                    }
                    else
                    {
                        SweepInitHold = 0.0;
                        SweepHold = DualSweepDelay;
                        SweepTerminationDelay = TerminationDelay;
                        SweepTermHold = TermHold - 1.0 * OutputSequenceDelay;

                    }

                    SweepMeasHold = DualSweepDelay;
                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;

                    #region Create sweep pattern

                    CreateRampSweepWaveform(
                        SweepName,
                        SweepInitV,
                        SweepInitHold,
                        SweepInitToStartTransition,
                        SweepHold,
                        SweepStartV,
                        SweepStopV,
                        SweepStepNum,
                        SweepStepDuration,
                        SweepTerminationDelay,
                        SweepStopToTermTransient,
                        SweepTermV,
                        SweepTermHold);

                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }

                    #endregion

                    #endregion

                    #region Merging forward and reverse sweep

                    rc = WGFMU.createMergedPattern(PatternName, "Fwd" + PatternName, "Rev" + PatternName, WGFMU.AXIS_TIME);
                    if (rc != WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(rc, ""));
                    }
                    #endregion

                }

                #endregion

                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }

        #endregion

        #region CreateRampConstSource

        // Same with stairecase sweep

        #endregion

        #endregion

        #region CreatePulseSweepMeasurement

        #region CreatePulseSweepSource

        public void CreatePulseSweepSource(
            int MeasEnable,
            string PatternName,
            string MeasEventName,
            double OutputSequenceDelay,
            double InitV,
            double InitHold,
            double InitToBaseTransient,
            double Hold,
            double PulseBase,
            double StartV,
            double StopV,
            int StepNum,
            double PulsePeriod,
            double PulseWidth,
            double PulseRiseTime,
            double PulseFallTime,
            double PulseDelay,
            double MeasDelay,
            double AveragingTime,
            double TerminationDelay,
            double TermV,
            double BaseToTermTransient,
            double TermHold,
            int DualSweep,
            double DualSweepDelay,
            ref double MeasOrigin)
        {
            #region Declaration of generic parameters

            int rc = 0;

            double SweepInitV;
            double SweepInitHold;
            double SweepInitToBaseTransient;
            double SweepHold;
            double SweepStartV;
            double SweepStopV;
            int SweepStepNum;
            double SweepStepDelay;
            double SweepTerminationDelay;
            double SweepBaseToTermTransient;
            double SweepTermV;
            double SweepTermHold;

            double SweepPulseBase;
            double SweepPulsePeriod;
            double SweepPulseDelay;
            double SweepPulseWidth;
            double SweepPulseRiseTime;
            double SweepPulseFallTime;

            double SweepMeasDelay;
            double SweepAveragingTime;

            double SweepMeasHold;

            string SweepName = "";
            string SweepMeasName = "";


            string FunctionName = "CreatePulseSweepSource";

            #endregion

            #region Declaration of method dedicated parameters
            int RawData = WGFMU.MEASURE_EVENT_DATA_AVERAGED;
            #endregion

            try
            {
                #region Input Parameter Check

                if (!(Hold >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Hold must be larger than 0 sec."));
                }

                if (!(StepNum >= 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Step number of the sweep must be larger than 1."));
                }


                if (!(MeasDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "StepDelay must be equal or larger than 0 sec."));
                }

                if (!(DualSweep == 0 || DualSweep == 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "DualSweep must 0:Disable or 1:Enable."));
                }

                if (!(DualSweepDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " DualSweepDelay must be larger than 0 sec."));
                }


                if (!(PulsePeriod > (PulseDelay + PulseWidth + PulseFallTime)))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " Pulse period must be larger than the PulseDelay + PulseWidth + PulseFallTime."));
                }

                if (!(MeasDelay < (PulsePeriod - PulseDelay)))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " MeasDelay must be smaller than the PulsePeriod - PulseDelay."));
                }

                // Other parameters are checked by the instrument library of 

                #endregion

                #region Body of method

                #region Create single and primary only sweep

                if (DualSweep == 0)
                {
                    SweepName = PatternName;
                    SweepMeasName = MeasEventName;
                    SweepInitV = InitV;
                    SweepInitHold = InitHold;
                    SweepInitToBaseTransient = InitToBaseTransient;
                    SweepPulseBase = PulseBase;
                    SweepStartV = StartV;
                    SweepStopV = StopV;
                    SweepStepNum = StepNum;
                    SweepPulsePeriod = PulsePeriod;
                    SweepPulseWidth = PulseWidth;
                    SweepPulseRiseTime = PulseRiseTime;
                    SweepPulseFallTime = PulseFallTime;
                    SweepPulseDelay = PulseDelay;
                    SweepBaseToTermTransient = BaseToTermTransient;
                    SweepTermV = TermV;

                    if (OutputSequenceDelay >= 0)
                    {
                        SweepInitHold = InitHold;
                        SweepHold = OutputSequenceDelay + Hold;
                        SweepMeasHold = InitHold + InitToBaseTransient + OutputSequenceDelay + Hold + PulseDelay;
                        SweepTerminationDelay = TerminationDelay + OutputSequenceDelay;
                        SweepTermHold = TermHold;
                    }
                    else
                    {
                        SweepInitHold = InitHold + (-1.0 * OutputSequenceDelay); ;
                        SweepHold = Hold;
                        SweepMeasHold = InitHold + InitToBaseTransient - OutputSequenceDelay + Hold + PulseDelay;
                        SweepTerminationDelay = TerminationDelay;
                        SweepTermHold = TermHold + -1.0 * OutputSequenceDelay;

                    }

                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;
                    SweepStepDelay = PulsePeriod - MeasDelay - AveragingTime;

                    #region Create sweep pattern

                    CreatePulseSweepWaveform(
                        PatternName,
                        SweepInitV,
                        SweepInitHold,
                        SweepInitToBaseTransient,
                        SweepPulseBase,
                        SweepHold,
                        SweepStartV,
                        SweepStopV,
                        SweepStepNum,
                        SweepPulsePeriod,
                        SweepPulseDelay,
                        SweepPulseWidth,
                        SweepPulseRiseTime,
                        SweepPulseFallTime,
                        SweepTerminationDelay,
                        SweepBaseToTermTransient,
                        SweepTermV,
                        SweepTermHold);

                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);

                        MeasOrigin= SweepMeasHold;
                    }
                    #endregion
                }

                #endregion

                #region Create dual and primary only sweep

                if (DualSweep == 1)
                {
                    #region Fowrad sweep
                    SweepName = "Fwd" + PatternName;
                    SweepMeasName = "Fwd" + MeasEventName;
                    SweepInitV = InitV;
                    SweepInitHold = InitHold;
                    SweepInitToBaseTransient = InitToBaseTransient;
                    SweepPulseBase = PulseBase;
                    SweepStartV = StartV;
                    SweepStopV = StopV;
                    SweepStepNum = StepNum;
                    SweepPulsePeriod = PulsePeriod;
                    SweepPulseWidth = PulseWidth;
                    SweepPulseRiseTime = PulseRiseTime;
                    SweepPulseFallTime = PulseFallTime;
                    SweepPulseDelay = PulseDelay;
                    SweepTerminationDelay = 0.0;
                    SweepTermV = PulseBase;
                    SweepTermHold = 0.0;
                    SweepBaseToTermTransient = 0.0;

                    if (OutputSequenceDelay >= 0)
                    {
                        SweepInitHold = InitHold;
                        SweepHold = OutputSequenceDelay + Hold;
                        SweepTerminationDelay = 0.0;
                    }
                    else
                    {
                        SweepInitHold = InitHold + (-1.0 * OutputSequenceDelay); ;
                        SweepHold = Hold;
                        SweepTerminationDelay = 0.0;

                    }

                    SweepMeasHold = InitHold + InitToBaseTransient + OutputSequenceDelay + Hold;


                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;
                    SweepStepDelay = PulsePeriod - MeasDelay - AveragingTime;

                    #region Create sweep pattern

                    CreatePulseSweepWaveform(
                        SweepName,
                        SweepInitV,
                        SweepInitHold,
                        SweepInitToBaseTransient,
                        SweepPulseBase,
                        SweepHold,
                        SweepStartV,
                        SweepStopV,
                        SweepStepNum,
                        SweepPulsePeriod,
                        SweepPulseDelay,
                        SweepPulseWidth,
                        SweepPulseRiseTime,
                        SweepPulseFallTime,
                        SweepTerminationDelay,
                        SweepBaseToTermTransient,
                        SweepTermV,
                        SweepTermHold);

                    #endregion

                    #region Set measurement event
                    if (MeasEnable == 1)
                    {
                    SetSweepMeasEvent(
                        SweepName,
                        SweepMeasName,
                        SweepMeasHold,
                        SweepStepNum,
                        SweepMeasDelay,
                        SweepAveragingTime,
                        SweepStepDelay,
                        RawData);
                    }

                    #endregion

                    #endregion

                    #region Reverse sweep

                    SweepName = "Rev" + PatternName;
                    SweepMeasName = "Rev" + MeasEventName;
                    SweepInitV = PulseBase;
                    SweepInitHold = 0.0;
                    SweepInitToBaseTransient = 0.0;
                    SweepPulseBase = PulseBase;
                    SweepStartV = StopV;
                    SweepStopV = StartV;
                    SweepStepNum = StepNum;
                    SweepPulsePeriod = PulsePeriod;
                    SweepPulseWidth = PulseWidth;
                    SweepPulseRiseTime = PulseRiseTime;
                    SweepPulseFallTime = PulseFallTime;
                    SweepPulseDelay = PulseDelay;
                    SweepHold = DualSweepDelay;
   
                    SweepTermV = TermV;
                    SweepTermHold = TermHold;
                    SweepBaseToTermTransient = BaseToTermTransient;

                    if (OutputSequenceDelay >= 0)
                    {
                        SweepInitHold = 0;
                        SweepHold = DualSweepDelay;
                        SweepTerminationDelay = TerminationDelay + OutputSequenceDelay;
                        SweepTermHold = TermHold;
                    }
                    else
                    {
                        SweepInitHold = 0.0;
                        SweepHold = DualSweepDelay;
                        SweepTerminationDelay = TerminationDelay;
                        SweepTermHold = TermHold - 1.0 * OutputSequenceDelay;

                    }

                    SweepMeasHold = DualSweepDelay + PulseDelay; ;
                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;

                    SweepStepDelay = PulsePeriod - MeasDelay - AveragingTime;

                    #region Create sweep pattern

                    CreatePulseSweepWaveform(
                        SweepName,
                        SweepInitV,
                        SweepInitHold,
                        SweepInitToBaseTransient,
                        SweepPulseBase,
                        SweepHold,
                        SweepStartV,
                        SweepStopV,
                        SweepStepNum,
                        SweepPulsePeriod,
                        SweepPulseDelay,
                        SweepPulseWidth,
                        SweepPulseRiseTime,
                        SweepPulseFallTime,
                        SweepTerminationDelay,
                        SweepBaseToTermTransient,
                        SweepTermV,
                        SweepTermHold);

                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }

                    #endregion


                    #endregion

                    #region Merging forward and reverse sweep

                    rc = WGFMU.createMergedPattern(PatternName, "Fwd" + PatternName, "Rev" + PatternName, WGFMU.AXIS_TIME);

                    #endregion

                }
                #endregion


            #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }
        #endregion

        #region CreatePulseConstantSource

        public void CreatePulseConstantSource(
            int MeasEnable,
            string PatternName,
            string MeasEventName,
            int EnablePulseBias,
            double OutputSequenceDelay,
            double InitV,
            double InitHold,
            double InitToBaseTransient,
            double Hold,
            double PulseBase,
            double SourceV,
            int StepNum,
            double PulsePeriod,
            double PulseWidth,
            double PulseRiseTime,
            double PulseFallTime,
            double PulseDelay,
            double MeasDelay,
            double AveragingTime,
            double TerminationDelay,
            double TermV,
            double BaseToTermTransient,
            double TermHold,
            int DualSweep,
            double DualSweepDelay)
        {
            #region Declaration of generic parameters

            int rc = 0;

            string FunctionName = "CreatePulseConstantSource";

            #endregion

            #region Declaration of method dedicated parameters

            int SweepStepNum;
            double SweepStepDelay;

            int RawData = WGFMU.MEASURE_EVENT_DATA_AVERAGED;
            double ConstPulseBaseV;
            double ConstPulsePeriod;
            double ConstPulseDelay;
            double ConstPulseWidth;
            double ConstPulseRiseTime;
            double ConstPulseFallTime;

            double SweepMeasDelay;
            double SweepAveragingTime;

            double SweepMeasHold;

            string SweepName = "";
            string SweepMeasName = "";

            double ConstInitV;
            double ConstInitHold;
            double ConstInitToBaseTransient;
            double ConstBaseV;
            double ConstBaseHold;
            double ConstBaseToSourceTransient;
            double ConstSourceV;
            double ConstSourceDuration;
            double ConstTerminationDelay;
            double ConstSourceToTermTransient;
            double ConstBaseToTermTransient;
            double ConstTermV;
            double ConstTermHold;


            #endregion

            try
            {
                #region Input Parameter Check

                if (!(Hold >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Hold must be larger than 0 sec."));
                }

                if (!(StepNum >= 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "Step number of the sweep must be larger than 1."));
                }


                if (!(MeasDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "StepDelay must be equal or larger than 0 sec."));
                }

                if (!(DualSweep == 0 || DualSweep == 1))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, "DualSweep must 0:Disable or 1:Enable."));
                }

                if (!(DualSweepDelay >= 0))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " DualSweepDelay must be larger than 0 sec."));
                }


                if (!(PulsePeriod > (PulseDelay + PulseWidth + PulseFallTime)))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " Pulse period must be larger than the PulseDelay + PulseWidth + PulseFallTime."));
                }

                if (!(MeasDelay < (PulsePeriod - PulseDelay)))
                {
                    rc = -1;
                    throw (new WGFMULibException(rc, " MeasDelay must be smaller than the PulsePeriod - PulseDelay."));
                }

                // Other parameters are checked by the instrument library of 

                #endregion

                #region Body of method

                #region Create single and primary only sweep

                if (DualSweep == 0)
                {
                    SweepName = PatternName;
                    SweepMeasName = MeasEventName;

                    SweepStepNum = StepNum;
                    ConstPulsePeriod = PulsePeriod;
                    ConstPulseWidth = PulseWidth;
                    ConstPulseRiseTime = PulseRiseTime;
                    ConstPulseFallTime = PulseFallTime;
                    ConstPulseDelay = PulseDelay;



                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;
                    SweepStepDelay = PulsePeriod - MeasDelay - AveragingTime;

                    ConstInitV = InitV;
                    ConstInitHold = InitHold;

                    ConstPulseBaseV = PulseBase;
                    ConstBaseV = SourceV;
                    ConstSourceV = SourceV;
                    ConstBaseHold = Hold;

                    ConstInitToBaseTransient = InitToBaseTransient;
                    ConstBaseToSourceTransient = 0.0;
                    ConstSourceToTermTransient = BaseToTermTransient;
                    ConstBaseToTermTransient = BaseToTermTransient;
                    ConstTermV = TermV;
                    ConstTermHold = TermHold;

                    if (OutputSequenceDelay >= 0)
                    {
                        ConstBaseHold = OutputSequenceDelay;
                        SweepMeasHold = InitHold + ConstBaseToSourceTransient + OutputSequenceDelay + Hold + PulseDelay;
                        ConstTerminationDelay = TerminationDelay;
                        ConstTermHold = TermHold + OutputSequenceDelay;
                    }
                    else
                    {
                        ConstBaseHold = 0.0;
                        SweepMeasHold = InitHold + ConstBaseToSourceTransient  - OutputSequenceDelay + Hold + PulseDelay;
                        ConstTerminationDelay = TerminationDelay + -1.0 * OutputSequenceDelay;
                        ConstTermHold = TermHold;
                      }

                    ConstSourceDuration = Hold + PulsePeriod * StepNum + ConstTerminationDelay;
                    
                    #region Create const bias pattern

                    if (EnablePulseBias == 1)
                    {
                        ConstBaseHold = Hold;
                        CreatePulseSweepWaveform(
                            SweepName,
                            ConstInitV,
                            ConstInitHold,
                            ConstInitToBaseTransient,
                            ConstPulseBaseV,
                            ConstBaseHold,
                            ConstSourceV,
                            ConstSourceV,
                            SweepStepNum,
                            ConstPulsePeriod,
                            ConstPulseDelay,
                            ConstPulseWidth,
                            ConstPulseRiseTime,
                            ConstPulseFallTime,
                            ConstTerminationDelay,
                            ConstBaseToTermTransient,
                            ConstTermV,
                            ConstTermHold);
                    }
                    else
                    {
     
                        CreateStepWaveform(
                            SweepName,
                            ConstInitV,
                            ConstInitHold,
                            ConstInitToBaseTransient,
                            ConstBaseV,
                            ConstBaseHold,
                            ConstBaseToSourceTransient,
                            ConstSourceV,
                            ConstSourceDuration,
                            ConstSourceToTermTransient,
                            ConstTermV,
                            ConstTermHold);

                    }

                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }


                    #endregion
                }

                #endregion

                #region Create dual and primary only sweep

                if (DualSweep == 1)
                {
                    #region Fowrad sweep

                    SweepName = "Fwd" + PatternName;
                    SweepMeasName = "Fwd" + MeasEventName;

                    SweepStepNum = StepNum;
                    ConstPulsePeriod = PulsePeriod;
                    ConstPulseWidth = PulseWidth;
                    ConstPulseRiseTime = PulseRiseTime;
                    ConstPulseFallTime = PulseFallTime;
                    ConstPulseDelay = PulseDelay;

                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;
                    SweepStepDelay = PulsePeriod - MeasDelay - AveragingTime;

                    ConstInitV = InitV;
                    ConstInitHold = InitHold;
                    ConstInitToBaseTransient = InitToBaseTransient;
                    ConstBaseV = 0.0;
                    ConstPulseBaseV = PulseBase;
                    ConstSourceV = SourceV;

                    ConstInitToBaseTransient = InitToBaseTransient;
                    ConstBaseToSourceTransient = ConstPulseRiseTime;
                    ConstSourceToTermTransient = 0.0;
                    ConstBaseToTermTransient = 0.0;

                    ConstTermHold = 0.0;

                    if (OutputSequenceDelay >= 0)
                    {
                        ConstBaseHold = OutputSequenceDelay;
                        SweepMeasHold = InitHold + ConstBaseToSourceTransient + Hold + OutputSequenceDelay + PulseDelay;
                        ConstTerminationDelay = 0.0;
                    }
                    else
                    {
                        ConstBaseHold = 0.0;
                        SweepMeasHold = InitHold + ConstBaseToSourceTransient + Hold - OutputSequenceDelay + PulseDelay;
                        ConstTerminationDelay = 0.0;
  
                    }

                    ConstSourceDuration = Hold + PulsePeriod * StepNum + ConstTerminationDelay;

                    #region Create const bias pattern

                    if (EnablePulseBias == 1)
                    {
                        ConstBaseHold = Hold;
                        ConstInitToBaseTransient = ConstPulseRiseTime;
                        ConstTermV = ConstPulseBaseV;
                        CreatePulseSweepWaveform(
                            SweepName,
                            ConstInitV,
                            ConstInitHold,
                            ConstInitToBaseTransient,
                            ConstPulseBaseV,
                            ConstBaseHold,
                            ConstSourceV,
                            ConstSourceV,
                            SweepStepNum,
                            ConstPulsePeriod,
                            ConstPulseDelay,
                            ConstPulseWidth,
                            ConstPulseRiseTime,
                            ConstPulseFallTime,
                            ConstTerminationDelay,
                            ConstBaseToTermTransient,
                            ConstTermV,
                            ConstTermHold);
                    }
                    else
                    {
                        ConstInitToBaseTransient = 0;
                        ConstTermV = SourceV;
                        CreateStepWaveform(
                            SweepName,
                            ConstInitV,
                            ConstInitHold,
                            ConstInitToBaseTransient,
                            ConstBaseV,
                            ConstBaseHold,
                            ConstBaseToSourceTransient,
                            ConstSourceV,
                            ConstSourceDuration,
                            ConstSourceToTermTransient,
                            ConstTermV,
                            ConstTermHold);

                    }


                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }

                    #endregion

                    #endregion

                    #region Reverse sweep

                    SweepName = "Rev" + PatternName;
                    SweepMeasName = "Rev" + MeasEventName;

                    ConstPulsePeriod = PulsePeriod;
                    ConstPulseWidth = PulseWidth;
                    ConstPulseRiseTime = PulseRiseTime;
                    ConstPulseFallTime = PulseFallTime;
                    ConstPulseDelay = PulseDelay;


                    SweepStepNum = StepNum;
                    SweepMeasDelay = MeasDelay;
                    SweepAveragingTime = AveragingTime;
                    SweepStepDelay = PulsePeriod - MeasDelay - AveragingTime;
                    SweepMeasHold = DualSweepDelay;


                    ConstInitHold = 0;
                    ConstInitToBaseTransient = InitToBaseTransient;
                    ConstBaseV = PulseBase;
                    ConstSourceV = SourceV;
                    ConstBaseHold = DualSweepDelay;

                    ConstInitToBaseTransient = 0;
                    ConstBaseToSourceTransient = 0;
                    ConstSourceToTermTransient = BaseToTermTransient;
                    ConstBaseToTermTransient = BaseToTermTransient;
                    ConstTermV = TermV;
                    ConstTermHold = TermHold;

                    if (OutputSequenceDelay >= 0)
                    {
                        ConstBaseHold = DualSweepDelay;
                        ConstTerminationDelay = TerminationDelay;
                        ConstTermHold = TermHold + OutputSequenceDelay;

                    }
                    else
                    {
                        ConstBaseHold = 0;
                        ConstTerminationDelay = TerminationDelay + -1.0 * OutputSequenceDelay;
                        ConstTermHold = TermHold;
                    }

                    ConstSourceDuration = PulsePeriod * StepNum + ConstTerminationDelay;

                    #region Create const bias pattern

                    if (EnablePulseBias == 1)
                    {
                        ConstInitV = ConstPulseBaseV;
                        ConstBaseV = ConstPulseBaseV;

                        CreatePulseSweepWaveform(
                            SweepName,
                            ConstInitV,
                            ConstInitHold,
                            ConstInitToBaseTransient,
                            ConstPulseBaseV,
                            ConstBaseHold,
                            ConstSourceV,
                            ConstSourceV,
                            SweepStepNum,
                            ConstPulsePeriod,
                            ConstPulseDelay,
                            ConstPulseWidth,
                            ConstPulseRiseTime,
                            ConstPulseFallTime,
                            ConstTerminationDelay,
                            ConstBaseToTermTransient,
                            ConstTermV,
                            ConstTermHold);
                    }
                    else
                    {
                        ConstInitV = SourceV;
                        ConstBaseV = SourceV;

                        CreateStepWaveform(
                            SweepName,
                            ConstInitV,
                            ConstInitHold,
                            ConstInitToBaseTransient,
                            ConstBaseV,
                            ConstBaseHold,
                            ConstBaseToSourceTransient,
                            ConstSourceV,
                            ConstSourceDuration,
                            ConstSourceToTermTransient,
                            ConstTermV,
                            ConstTermHold);

                    }

                    #endregion

                    #region Set measurement event

                    if (MeasEnable == 1)
                    {
                        SetSweepMeasEvent(
                            SweepName,
                            SweepMeasName,
                            SweepMeasHold,
                            SweepStepNum,
                            SweepMeasDelay,
                            SweepAveragingTime,
                            SweepStepDelay,
                            RawData);
                    }


                    #endregion


                    #endregion

                    #region Merging forward and reverse sweep

                    rc = WGFMU.createMergedPattern(PatternName, "Fwd" + PatternName, "Rev" + PatternName, WGFMU.AXIS_TIME);

                    #endregion

                }
                #endregion


                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }
        #endregion

        #endregion

        #region CreateSamplingMeasurement
        public void CreateSamplingMeasurement(
            int SourceType,
            int EnableMeasurement,
            int MeasMode,
            string PatternName,
            string MeasEventName,
            double InitV,
            double InitHold,
            double InitToBaseTransient,
            double BaseV,
            double BaseHold,
            double BaseToSourceTransient,
            double SourceV,
            double TermV,
            double SourcetoTremTransient,
            double TermHold,
            int SamplingMode,
            double MeasDelay,
            double InitialInterval,
            int SamplingPoints,
            double AveragingTime,
            double TerminationDelay,
            double OutputSequenceDelay,
            int EnableSecondary,
            int SecondMeasRange,
            int SecondSamplingMode,
            double SecondMeasOrigin,
            double SecondMeasDelay,
            double SecondInitialInterval,
            int SecondSamplingPoints,
            double SecondAveragingTime,
            int RawData,
            ref double MeasOrigin)
        {
            #region Declaration of generic parameters

            string FunctionName = "CreateSamplingMeasurement";

            #endregion

            #region Declaration of method dedicated parameters
            double TempBaseHold;
            double TempMeasDelay;
            double TempSecondMeasOrigin;
            double TempTerminationDelay;
            double TempTermHold;
            double TempMeasDuration;
            double TempSourceDuration;
            double LogBase = 10.0;
            double LogResolution = 0 ;

            #endregion

            try
            {
                #region Input Parameter Check

                // Other parameters are checked by the instrument library of 

                #endregion

                #region Body of method

                #region Set parameters

                if (SourceType == 0)
                {
                    #region for primaly source

                    if (OutputSequenceDelay >= 0)
                    {
                        TempBaseHold = BaseHold;
                        TempTerminationDelay = TerminationDelay + OutputSequenceDelay;
                        TempTermHold = TermHold;
                        
                        
                    }
                    else
                    {
                        TempBaseHold = BaseHold -1.0 * OutputSequenceDelay;
                        TempTerminationDelay = TerminationDelay;
                        TempTermHold = TermHold - 1.0 * OutputSequenceDelay;
                    }

                    TempMeasDelay = InitHold + InitToBaseTransient + TempBaseHold + MeasDelay;
                    TempSecondMeasOrigin = InitHold + InitToBaseTransient + TempBaseHold + SecondMeasOrigin;


                    if (SamplingMode == 0)
                    {
                        TempMeasDuration = MeasDelay + InitialInterval * (SamplingPoints - 1) + AveragingTime;
                    }
                    else 
                    {
                        LogBase = 10;
                        if (SamplingMode == 1)
                        {
                            LogResolution = 1.0 / 10.0;
                        }
                        else if (SamplingMode == 2)
                        {
                            LogResolution = 1.0 / 25.0;
                        }

                        TempMeasDuration = MeasDelay + InitialInterval 
                                                        * (Math.Pow(LogBase, LogResolution * (SamplingPoints - 1)) -1) 
                                                        + AveragingTime;

                    }

                    if (EnableSecondary == 1)
                    {
                        if (TempMeasDuration > SecondMeasOrigin)
                        {
                            throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "SecondMeasOrigin must be lager than the total measurement time of the primary measurement."));

                        }
                        if (SecondSamplingMode == 0)
                        {
                            TempMeasDuration =   SecondMeasOrigin 
                                                + SecondMeasDelay 
                                                + SecondInitialInterval * (SecondSamplingPoints - 1) 
                                                + SecondAveragingTime;
                        }
                        else
                        {
                            LogBase = 10;
                            if (SecondSamplingMode == 1)
                            {
                                LogResolution = 1.0 / 10.0;
                            }
                            else if (SecondSamplingMode == 2)
                            {
                                LogResolution = 1.0 / 25.0;
                            }
                            TempMeasDuration = MeasDelay + AveragingTime;

                            if (SecondSamplingPoints > 1)
                            {
                                TempMeasDuration =  SecondMeasOrigin
                                                    + SecondMeasDelay
                                                    + SecondInitialInterval * ( Math.Pow(LogBase, LogResolution * (SecondSamplingPoints - 1) ) -1) 
                                                    + SecondAveragingTime;
                            }
                        }
                    
                    }

                    TempSourceDuration = TempMeasDuration + TempTerminationDelay - BaseToSourceTransient;

                    #endregion
                }
                else
                {
                    if (OutputSequenceDelay >= 0)
                    {
                        TempBaseHold = BaseHold + OutputSequenceDelay;
                        TempTerminationDelay = TerminationDelay;
                        TempMeasDelay = InitHold + InitToBaseTransient + BaseHold + MeasDelay;
                        TempSecondMeasOrigin = InitHold + InitToBaseTransient + BaseHold + SecondMeasOrigin;
                        TempTermHold = TermHold + OutputSequenceDelay;
                    }
                    else
                    {
                        TempBaseHold = BaseHold;
                        TempTerminationDelay = TerminationDelay - 1.0 * OutputSequenceDelay;
                        TempMeasDelay = InitHold + InitToBaseTransient + BaseHold - 1.0 * OutputSequenceDelay + MeasDelay;
                        TempSecondMeasOrigin = InitHold + InitToBaseTransient + BaseHold - 1.0 * OutputSequenceDelay + SecondMeasOrigin;
                        TempTermHold = TermHold;
                    }

                    if (SamplingMode == 0)
                    {
                        TempMeasDuration = MeasDelay + InitialInterval * (SamplingPoints - 1) + AveragingTime;
                    }
                    else
                    {

                        LogBase = 10;
                        if (SamplingMode == 1)
                        {
                            LogResolution = 1.0 / 10.0;
                        }
                        else if (SamplingMode == 2)
                        {
                            LogResolution = 1.0 / 25.0;
                        }

                        TempMeasDuration = MeasDelay + InitialInterval 
                                                        * (Math.Pow(LogBase, LogResolution * (SamplingPoints - 1) )- 1) 
                                                        +AveragingTime;

                    }

                    if (EnableSecondary == 1)
                    {
                        if (TempMeasDuration > SecondMeasOrigin)
                        {
                            throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "SecondMeasOrigin must be lager than the total measurement time of the primary measurement."));

                        }
                        if (SecondSamplingMode == 0)
                        {
                            TempMeasDuration = SecondMeasOrigin
                                                + SecondMeasDelay
                                                + SecondInitialInterval * (SecondSamplingPoints - 1)
                                                + SecondAveragingTime;
                        }
                        else
                        {
                            LogBase = 10;
                            if (SecondSamplingMode == 1)
                            {
                                LogResolution = 1.0 / 10.0;
                            }
                            else if (SecondSamplingMode == 2)
                            {
                                LogResolution = 1.0 / 25.0;
                            }
                            TempMeasDuration = MeasDelay + AveragingTime;

                            TempMeasDuration = SecondMeasOrigin
                                                + SecondMeasDelay
                                                + SecondInitialInterval *( Math.Pow(LogBase, LogResolution * (SecondSamplingPoints - 1) ) - 1)
                                                + SecondAveragingTime;
                        }

                    }
               
                TempSourceDuration = TempMeasDuration + (-1.0 * OutputSequenceDelay)
                                             + TempTerminationDelay - BaseToSourceTransient;
                }

                #region Create waveform
                CreateStepWaveform(
                    PatternName,
                    InitV,
                    InitHold,
                    InitToBaseTransient,
                    BaseV,
                    TempBaseHold,
                    BaseToSourceTransient,
                    SourceV,
                    TempSourceDuration,
                    SourcetoTremTransient,
                    TermV,
                    TempTermHold);
                #endregion

                #region Set measurement event
                if (EnableMeasurement == 1)
                {

                    SetSamplingMeasEvent(
                        PatternName,
                        MeasEventName,
                        SamplingMode,
                        InitialInterval,
                        SamplingPoints,
                        AveragingTime,
                        TempMeasDelay,
                        RawData);

                    MeasOrigin = TempMeasDelay - MeasDelay;

                    if (EnableSecondary == 1)
                    {
                        TempMeasDelay = TempSecondMeasOrigin + SecondMeasDelay;
                        MeasEventName = "Scnd" + MeasEventName;
                        SetSamplingMeasEvent(
                            PatternName,
                            MeasEventName,
                            SecondSamplingMode,
                            SecondInitialInterval,
                            SecondSamplingPoints,
                            SecondAveragingTime,
                            TempMeasDelay,
                            RawData);

                        if (MeasMode == WGFMU.MEASURE_MODE_CURRENT)
                        {
                            SetMeasRangeEvent(
                                PatternName,
                                "SndRange",
                                TempSecondMeasOrigin,
                                SecondMeasRange);
                        }
                    }
                }


                #endregion                  
                    
                #endregion
                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }  
        }
        #endregion

        #region CreatePulseMesurement

        #endregion

        #region CreateOTFMeasurement

        public void CreateOTFMeasurement(
            int MeasEnable,
            string PatternName,
            string MeasEventName,
            int SourceType,
            double OutputSequenceDelay,
            double InitV,
            double InitHold,
            double InitToMeasTransient,
            double StressV,
            double DeltaV,
            double OtfMeasEdge,
            double MeasDelay,
            double AveragingTime,
            double StepDelay,
            double TermV,
            double MeasToTermTransient,
            double TermHold,
            ref double MeasOrigin)
        {

            int rc = 0;
            string FunctionName = "CreateOTFMeasurement";

            string OtfPatternNameStep0;
            string OtfPatternNameStep1;
            string OtfPatternNameStep2;

            string MergedPatternName;
            string OtfMeasNameStep0;
            string OtfMeasNameStep1;
            string OtfMeasNameStep2;

            double OtfInitV;
            double OtfInitHold;
            double OtfInitToBaseTransient;
            double OtfBaseV;
            double OtfBaseHold;
            double OtfBaseTosourceTransient;
            double OtfSourceV;
            double OtfSourceDuration;
  
            double OtfSourceToTermtransient;
            double OtfTermV;
            double OtfTermHold;

            double OtfMeasDelay;
            double OtfAveragingTime;
            double OtfSetpDelay;

            int SamplingMode;
            double SamplingInterval;
            int SamplingPoints;
            int SamplingRawData = WGFMU.MEASURE_EVENT_DATA_AVERAGED;

            try
            {
                #region Cresate gate pattern

                if (SourceType == 0)
                {
                    #region create 1st step

                    OtfPatternNameStep0 = PatternName + "Step0";
                    OtfInitV = InitV;
                    OtfInitHold = InitHold;
                    OtfInitToBaseTransient = InitToMeasTransient;

                    OtfBaseV = StressV;
                    if (OutputSequenceDelay >= 0)
                    {
                        OtfBaseHold = 0.0;
                        OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge + OutputSequenceDelay;

                    }
                    else
                    {
                        OtfBaseHold = -1.0 * OutputSequenceDelay;
                        OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge;

                    }

                    OtfBaseTosourceTransient = OtfMeasEdge;
                    OtfSourceV = StressV;
           
                    OtfTermV = StressV;
                    OtfSourceToTermtransient = 0.0;
                    OtfTermHold = 0.0;

                    OtfMeasDelay = OtfBaseHold + MeasDelay + InitHold + InitToMeasTransient;
                    OtfAveragingTime = AveragingTime;
                    OtfSetpDelay = StepDelay;


                    this.CreateStepWaveform(
                        OtfPatternNameStep0,
                        OtfInitV,
                        OtfInitHold,
                        OtfInitToBaseTransient,
                        OtfBaseV,
                        OtfBaseHold,
                        OtfBaseTosourceTransient,
                        OtfSourceV,
                        OtfSourceDuration,
                        OtfSourceToTermtransient,
                        OtfTermV,
                        OtfTermHold);

                    if (MeasEnable == 1)
                    {
                        OtfMeasNameStep0 = MeasEventName + "0";
                        SamplingMode = 0;
                        SamplingInterval = AveragingTime;
                        SamplingPoints = 1;

                        this.SetSamplingMeasEvent(
                            OtfPatternNameStep0,
                            OtfMeasNameStep0,
                            SamplingMode,
                            SamplingInterval,
                            SamplingPoints,
                            AveragingTime,
                            OtfMeasDelay,
                            SamplingRawData);
                    }

                    #endregion

                    #region create 2nd step

                    OtfPatternNameStep1 = PatternName + "Step1";
                    OtfInitV = StressV;
                    OtfInitHold = 0.0;
                    OtfInitToBaseTransient = 0.0;

                    OtfBaseV = StressV;
                    OtfBaseHold = 0.0;

                    OtfBaseTosourceTransient = OtfMeasEdge;
                    OtfSourceV = StressV + DeltaV;
                    OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge;

                    OtfTermV = StressV;
                    OtfSourceToTermtransient = OtfMeasEdge;
                    OtfTermHold = 0.0;

                    OtfMeasDelay = MeasDelay;
                    OtfAveragingTime = AveragingTime;
                    OtfSetpDelay = StepDelay;

                    this.CreateStepWaveform(
                        OtfPatternNameStep1,
                        OtfInitV,
                        OtfInitHold,
                        OtfInitToBaseTransient,
                        OtfBaseV,
                        OtfBaseHold,
                        OtfBaseTosourceTransient,
                        OtfSourceV,
                        OtfSourceDuration,
                        OtfSourceToTermtransient,
                        OtfTermV,
                        OtfTermHold);



                    if (MeasEnable == 1)
                    {
                        OtfMeasNameStep1 = MeasEventName + "1";
                        SamplingMode = 0;
                        SamplingInterval = AveragingTime;
                        SamplingPoints = 1;

                        this.SetSamplingMeasEvent(
                            OtfPatternNameStep1,
                            OtfMeasNameStep1,
                            SamplingMode,
                            SamplingInterval,
                            SamplingPoints,
                            AveragingTime,
                            OtfMeasDelay,
                            SamplingRawData);
                    }

                    #endregion

                    #region create 3rd step

                    OtfPatternNameStep2 = PatternName + "Step2";
                    OtfInitV = StressV;
                    OtfInitHold = 0.0;
                    OtfInitToBaseTransient = 0.0;

                    OtfBaseV = StressV;
                    OtfBaseTosourceTransient = OtfMeasEdge;
                    OtfSourceV = StressV - DeltaV;

                    if (OutputSequenceDelay >= 0)
                    {
                        OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge + OutputSequenceDelay;
                        OtfTermHold = TermHold;
                    }
                    else
                    {
                        OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge;
                        OtfTermHold = -1.0 * OutputSequenceDelay + TermHold; ;
                    }


                    OtfTermV = TermV;
                    OtfSourceToTermtransient = MeasToTermTransient;

                    OtfMeasDelay = OtfBaseHold + MeasDelay;
                    OtfAveragingTime = AveragingTime;
                    OtfSetpDelay = StepDelay;


                    this.CreateStepWaveform(
                        OtfPatternNameStep2,
                        OtfInitV,
                        OtfInitHold,
                        OtfInitToBaseTransient,
                        OtfBaseV,
                        OtfBaseHold,
                        OtfBaseTosourceTransient,
                        OtfSourceV,
                        OtfSourceDuration,
                        OtfSourceToTermtransient,
                        OtfTermV,
                        OtfTermHold);

                    if (MeasEnable == 1)
                    {
                        OtfMeasNameStep2 = MeasEventName + "2";
                        SamplingMode = 0;
                        SamplingInterval = AveragingTime;
                        SamplingPoints = 1;

                        this.SetSamplingMeasEvent(
                            OtfPatternNameStep2,
                            OtfMeasNameStep2,
                            SamplingMode,
                            SamplingInterval,
                            SamplingPoints,
                            AveragingTime,
                            OtfMeasDelay,
                           SamplingRawData);
                    }

                    #endregion

                    #region Merge pattern
                    MergedPatternName = PatternName + "Step1+Step2";
                    rc = WGFMU.createMergedPattern(MergedPatternName, OtfPatternNameStep0, OtfPatternNameStep1, WGFMU.AXIS_TIME);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, ""));
                    }

                    rc = WGFMU.createMergedPattern(PatternName, MergedPatternName, OtfPatternNameStep2, WGFMU.AXIS_TIME);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, ""));
                    }
                    #endregion

                }
                #endregion

                #region Create drain pattern

                if (SourceType == 1)
                {
                    #region create 1st step

                    OtfPatternNameStep0 = PatternName + "Step0";
                    OtfInitV = InitV;
                    OtfInitHold = InitHold;
                    OtfInitToBaseTransient = InitToMeasTransient;

                    OtfBaseV = StressV;

                    if (OutputSequenceDelay >= 0)
                    {
                        OtfBaseHold = 0.0;
                        OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge + OutputSequenceDelay;

                    }
                    else
                    {
                        OtfBaseHold = -1.0 * OutputSequenceDelay;
                        OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge;

                    }

                    OtfBaseTosourceTransient = OtfMeasEdge;
                    OtfSourceV = StressV + DeltaV;

                    OtfTermV = StressV + DeltaV;
                    OtfSourceToTermtransient = 0.0;
                    OtfTermHold = 0.0;

                    OtfMeasDelay = OtfBaseHold + MeasDelay + InitHold + InitToMeasTransient;
                    OtfAveragingTime = AveragingTime;
                    OtfSetpDelay = StepDelay;


                    this.CreateStepWaveform(
                        OtfPatternNameStep0,
                        OtfInitV,
                        OtfInitHold,
                        OtfInitToBaseTransient,
                        OtfBaseV,
                        OtfBaseHold,
                        OtfBaseTosourceTransient,
                        OtfSourceV,
                        OtfSourceDuration,
                        OtfSourceToTermtransient,
                        OtfTermV,
                        OtfTermHold);

                    if (MeasEnable == 1)
                    {
                        OtfMeasNameStep0 = MeasEventName + "0";
                        SamplingMode = 0;
                        SamplingInterval = AveragingTime;
                        SamplingPoints = 1;

                        this.SetSamplingMeasEvent(
                            OtfPatternNameStep0,
                            OtfMeasNameStep0,
                            SamplingMode,
                            SamplingInterval,
                            SamplingPoints,
                            AveragingTime,
                            OtfMeasDelay,
                            SamplingRawData);
                    }

                    #endregion

                    #region create 2nd step

                    OtfPatternNameStep1 = PatternName + "Step1";
                    OtfInitV = StressV + DeltaV;
                    OtfInitHold = 0.0;
                    OtfInitToBaseTransient = 0.0;

                    OtfBaseV = StressV + DeltaV;
                    OtfBaseHold = 0.0;

                    OtfBaseTosourceTransient = OtfMeasEdge;
                    OtfSourceV = StressV + DeltaV;
                    OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge;

                    OtfTermV = StressV + DeltaV;
                    OtfSourceToTermtransient = OtfMeasEdge;
                    OtfTermHold = 0.0;

                    OtfMeasDelay = MeasDelay ;
                    OtfAveragingTime = AveragingTime;
                    OtfSetpDelay = StepDelay;

                    this.CreateStepWaveform(
                        OtfPatternNameStep1,
                        OtfInitV,
                        OtfInitHold,
                        OtfInitToBaseTransient,
                        OtfBaseV,
                        OtfBaseHold,
                        OtfBaseTosourceTransient,
                        OtfSourceV,
                        OtfSourceDuration,
                        OtfSourceToTermtransient,
                        OtfTermV,
                        OtfTermHold);

                    if (MeasEnable == 1)
                    {
                        OtfMeasNameStep1 = MeasEventName + "1";
                        SamplingMode = 0;
                        SamplingInterval = AveragingTime;
                        SamplingPoints = 1;

                        this.SetSamplingMeasEvent(
                            OtfPatternNameStep1,
                            OtfMeasNameStep1,
                            SamplingMode,
                            SamplingInterval,
                            SamplingPoints,
                            AveragingTime,
                            OtfMeasDelay,
                            SamplingRawData);
                    }

                    #endregion

                    #region create 3rd step

                    OtfPatternNameStep2 = PatternName + "Step2";
                    OtfInitV = StressV + DeltaV;
                    OtfInitHold = 0.0;
                    OtfInitToBaseTransient = 0.0;

                    OtfBaseV = StressV + DeltaV;
                    OtfBaseTosourceTransient = OtfMeasEdge;
                    OtfSourceV = StressV + DeltaV;

                    if (OutputSequenceDelay >= 0)
                    {
                        OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge + OutputSequenceDelay;
                        OtfTermHold = TermHold;
                    }
                    else
                    {
                        OtfSourceDuration = MeasDelay + AveragingTime + StepDelay - OtfMeasEdge;
                        OtfTermHold = -1.0 * OutputSequenceDelay + TermHold;
                    }


                    OtfTermV = TermV ;
                    OtfSourceToTermtransient = MeasToTermTransient;

                    OtfMeasDelay = OtfBaseHold + MeasDelay;
                    OtfAveragingTime = AveragingTime;
                    OtfSetpDelay = StepDelay;


                    this.CreateStepWaveform(
                        OtfPatternNameStep2,
                        OtfInitV,
                        OtfInitHold,
                        OtfInitToBaseTransient,
                        OtfBaseV,
                        OtfBaseHold,
                        OtfBaseTosourceTransient,
                        OtfSourceV,
                        OtfSourceDuration,
                        OtfSourceToTermtransient,
                        OtfTermV,
                        OtfTermHold);

                    if (MeasEnable == 1)
                    {

                        OtfMeasNameStep2 = MeasEventName + "2";
                        SamplingMode = 0;
                        SamplingInterval = AveragingTime;
                        SamplingPoints = 1;

                        this.SetSamplingMeasEvent(
                            OtfPatternNameStep2,
                            OtfMeasNameStep2,
                            SamplingMode,
                            SamplingInterval,
                            SamplingPoints,
                            AveragingTime,
                            OtfMeasDelay,
                            SamplingRawData);
                    }
                    

                    #endregion

                    #region Merge pattern
                    MergedPatternName = PatternName + "Step0+Step1";
                    rc = WGFMU.createMergedPattern(MergedPatternName, OtfPatternNameStep0, OtfPatternNameStep1, WGFMU.AXIS_TIME);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, ""));
                    }

                    rc = WGFMU.createMergedPattern(PatternName, MergedPatternName, OtfPatternNameStep2, WGFMU.AXIS_TIME);
                    if (rc < WGFMU.NO_ERROR)
                    {
                        throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, ""));
                    }
                    #endregion

                    
                
                
                }


                #endregion
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                this.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                this.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }
        #endregion


        #endregion

        #region Bias source
        private void SMUErrorCheck()
        {
            int rc = 0;
            int ErrorCode;
            string ErrorMessage;
            string cmmd;
            string Readout = "";
            string[] SplittedString;
            string FunctionName = "SMUErrorCheck()";

            try 
            {
                cmmd = "ERRX?";
                rc = gpib.sendCmd(cmmd);
                if (rc < 0)
                { 
                    throw (new WGFMULibException(rc, "VISA ERROR"));
                }

                rc = gpib.queryStr(out Readout);
                if (rc < 0)
                {
                    throw (new WGFMULibException(rc, "VISA ERROR"));
                }

                SplittedString = Readout.Split(',');

                ErrorCode = int.Parse(SplittedString[0]);
                ErrorMessage = SplittedString[1];

                if (ErrorCode != 0)
                { 
                    throw (new WGFMULibException(ErrorCode, ErrorMessage));
                }

            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }

        }
        public void ForceBias(int BiasSourceType,
                              int BiasSourceCh,
                              double BiasSourceBiasV,
                              double BiasSourceCompliance)
        {
            string GpibDescription = this.VisaId + "::" + this.GpibAddress.ToString() +"::INSTR";
            string Cmmd;
            string FunctionName = "ForceBias()";
            try
            {
                if (BiasSourceType == 1)
                {
                    WGFMU.connect(BiasSourceCh);
                    WGFMU.setOperationMode(BiasSourceCh, WGFMU.OPERATION_MODE_DC);
                    WGFMU.dcforceVoltage(BiasSourceCh, BiasSourceBiasV);
                }
                else if (BiasSourceType == 2)
                {
                    gpib.Open(GpibDescription);
                    
                    Cmmd = "CN " + BiasSourceCh.ToString();
                    gpib.sendCmd(Cmmd);
                    
                    SMUErrorCheck();

                    Cmmd = "DV " + BiasSourceCh.ToString() + ",0," + BiasSourceBiasV.ToString() + "," + BiasSourceCompliance.ToString();
                    gpib.sendCmd(Cmmd);

                    SMUErrorCheck();

                }

                gpib.Close();
            
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);
                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                gpib.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                gpib.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        
        }

        public void StopBias(int BiasSourceType,
                              int BiasSourceCh)
        {
            string GpibDescription = this.VisaId + "::" + this.GpibAddress.ToString() + "::INSTR";
            string Cmmd;
            string FunctionName = "StopBias()";
            try
            {
                gpib.Open(GpibDescription);

                if (BiasSourceType == 1)
                {
                    WGFMU.disconnect(BiasSourceCh);
                    WGFMU.setOperationMode(BiasSourceCh, WGFMU.OPERATION_MODE_SMU);
                }
                else if (BiasSourceType == 2)
                {

                    Cmmd = "CL " + BiasSourceCh.ToString();
                    gpib.sendCmd(Cmmd);
                    SMUErrorCheck();

                    gpib.Close();

                }
            }
            catch (WGFMULibException we)
            {
                if (we.Message == "")
                {
                    WGFMU.getErrorSize(ref ErrMsgSize);
                    ErrMsg = new StringBuilder(ErrMsgSize + 1);
                    WGFMU.getError(ErrMsg, ref ErrMsgSize);

                }
                else
                {
                    ErrMsg = new StringBuilder();
                    ErrMsg.Append(":" + we.Message);
                }
                ErrMsg.Insert(0, FunctionName + ":");
                gpib.Close();
                throw (new WGFMULibException(we.StatusCode, ErrMsg.ToString()));
            }
            catch (Exception ex)
            {
                gpib.Close();
                ErrMsg.Append(FunctionName + " : " + ex.GetType().FullName.ToString() + " : " + ex.Message);
                throw (new WGFMULibException(GENERAL_EXCEPTION_ERROR, ErrMsg.ToString()));
            }
        }
        #endregion


    }
}
