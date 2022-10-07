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

// DATASAMPLER_Form.cs Rev. A.01.01.2009.01.07

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WGFMU_SAMPLE_Lib;

namespace DATASAMPLER
{
    public partial class DATASAMPLER_Form : Form
    {
        WGFMULib wglib = new WGFMULib();
        private string ApplicationName = "DATASAMPLER";
        private string LatestConfigFileName = Application.StartupPath + @"\DATASAMPLER.cf";
        private string DefaultConfigFileName = Application.StartupPath + @"\DATASAMPLERDefault";

        public DATASAMPLER_Form()
        {
            InitializeComponent();
        }

        #region Source1 setup

        int Source1ChId;
        int Source1MeasEnable;
        int Source1OperationMode;
        int Source1MeasMode;
        int Source1ForceRange;
        int Source1IMeasRange;
        int Source1VMeasRange;
        double Source1HwSkew;
        int Source1RawData;

        #endregion

        #region Soruce2 setup

        int Source2ChId;
        int Source2MeasEnable;
        int Source2OperationMode;
        int Source2MeasMode;
        int Source2ForceRange;
        int Source2IMeasRange;
        int Source2VMeasRange;
        double Source2HwSkew;
        int Source2RawData;

        #endregion

        #region Timing

        double SamplingFreq;
        double MeasDelay;
        double Duration;
        double AveragingTime;

        #endregion

        #region bias

        double Source1SourceV;
        double Source2SourceV;
        double StepEdge;

        #endregion

        #region Bias source setup

        int BiasSource1Type;
        int BiasSource1Ch;
        double BiasSource1BiasV;
        double BiasSource1Compliance;

        int BiasSource2Type;
        int BiasSource2Ch;
        double BiasSource2BiasV;
        double BiasSource2Compliance;

        #endregion

        #region User defined functions

        #region validate input parameter




        #endregion

        #region SetParameters
        private void SetParameters()
        {
            string ParseControlName = "";
            try
            {
                #region Source1 setup
                ParseControlName = "Source1 Channel";
                Source1ChId = int.Parse(this.comboBoxSource1Ch.Text);

                ParseControlName = "Source1 Meas."; 
                if (checkBoxSource1Meas.Checked == true)
                {
                    Source1MeasEnable = 1;
                }
                else
                {
                    Source1MeasEnable = 0;
                }

                ParseControlName = "Source1 Operation Mode"; 
                switch (this.comboBoxSource1OperationMode.SelectedIndex)
                {
                    case 0:
                        Source1OperationMode = WGFMU.OPERATION_MODE_FASTIV; // Set Fast IV mode
                        break;
                    case 1:
                        Source1OperationMode = WGFMU.OPERATION_MODE_PG; // Set PG mode
                        break;

                }

                ParseControlName = "Source1 Meas. Mode"; 
                switch (this.comboBoxSource1MeasMode.SelectedIndex)
                {
                    case 0:
                        Source1MeasMode = WGFMU.MEASURE_MODE_VOLTAGE; // Set voltage measurement mode
                        break;
                    case 1:
                        Source1MeasMode = WGFMU.MEASURE_MODE_CURRENT; // Set current measurement mode
                        break;
                }

                ParseControlName = "Source1 Force Range"; 
                switch (this.comboBoxSource1ForceRange.SelectedIndex)
                {
                    case 0:
                        Source1ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_AUTO;
                        break;
                    case 1:
                        Source1ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_3V;
                        break;
                    case 2:
                        Source1ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_5V;
                        break;
                    case 3:
                        Source1ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_10V_NEGATIVE;
                        break;
                    case 4:
                        Source1ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_10V_POSITIVE;
                        break;
                }

                ParseControlName = "Source1 IMeas. Range"; 
                switch (this.comboBoxSource1IMeasRange.SelectedIndex)
                {
                    case 0:
                        Source1IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1UA;
                        break;
                    case 1:
                        Source1IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10UA;
                        break;
                    case 2:
                        Source1IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_100UA;
                        break;
                    case 3:
                        Source1IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1MA;
                        break;
                    case 4:
                        Source1IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10MA;
                        break;
                }

                ParseControlName = "Source1 VMeas. Range"; 
                switch (this.comboBoxSource1VMeasRange.SelectedIndex)
                {
                    case 0:
                        Source1VMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_5V;
                        break;
                    case 1:
                        Source1VMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_10V;
                        break;
                }

                ParseControlName = "Source HW Skew"; 
                Source1HwSkew = double.Parse(this.textBoxSource1HwSkew.Text);

                #endregion

                #region Source2 setup

                ParseControlName = "Source2 Channel"; 
                Source2ChId = int.Parse(this.comboBoxSource2Ch.Text);

                ParseControlName = "Source2 Meas."; 
                if (checkBoxSource2Meas.Checked == true)
                {
                    Source2MeasEnable = 1;
                }
                else
                {
                    Source2MeasEnable = 0;
                }

                ParseControlName = "Source2 Operation Mode"; 
                switch (this.comboBoxSource2OperationMode.SelectedIndex)
                {
                    case 0:
                        Source2OperationMode = WGFMU.OPERATION_MODE_FASTIV; // Set Fast IV mode
                        break;
                    case 1:
                        Source2OperationMode = WGFMU.OPERATION_MODE_PG; // Set PG mode
                        break;

                }

                ParseControlName = "Source2 Meas. Mode"; 
                switch (this.comboBoxSource2MeasMode.SelectedIndex)
                {
                    case 0:
                        Source2MeasMode = WGFMU.MEASURE_MODE_VOLTAGE; // Set voltage measurement mode
                        break;
                    case 1:
                        Source2MeasMode = WGFMU.MEASURE_MODE_CURRENT; // Set current measurement mode
                        break;
                }

                ParseControlName = "Source2 Force Range"; 
                switch (this.comboBoxSource2ForceRange.SelectedIndex)
                {
                    case 0:
                        Source2ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_AUTO;
                        break;
                    case 1:
                        Source2ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_3V;
                        break;
                    case 2:
                        Source2ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_5V;
                        break;
                    case 3:
                        Source2ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_10V_NEGATIVE;
                        break;
                    case 4:
                        Source2ForceRange = WGFMU.FORCE_VOLTAGE_RANGE_10V_POSITIVE;
                        break;
                }

                ParseControlName = "Source2 IMeas Range"; 
                switch (this.comboBoxSource2IMeasRange.SelectedIndex)
                {
                    case 0:
                        Source2IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1UA;
                        break;
                    case 1:
                        Source2IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10UA;
                        break;
                    case 2:
                        Source2IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_100UA;
                        break;
                    case 3:
                        Source2IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1MA;
                        break;
                    case 4:
                        Source2IMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10MA;
                        break;
                }

                ParseControlName = "Source2 VMeas. Range"; 
                switch (this.comboBoxSource2VMeasRange.SelectedIndex)
                {
                    case 0:
                        Source2VMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_5V;
                        break;
                    case 1:
                        Source2VMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_10V;
                        break;
                }

                ParseControlName = "Source2 HW Skew"; 
                Source2HwSkew = double.Parse(this.textBoxSource2HwSkew.Text);

                #endregion

                #region Timing

                ParseControlName = "Sampling Rate";
                SamplingFreq = double.Parse(this.textBoxSamplingRate.Text);

                ParseControlName = "Measurement Delay"; 
                MeasDelay = double.Parse(this.textBoxMeasurementDelay.Text);

                ParseControlName = "Duration"; 
                Duration = double.Parse(this.textBoxDuration.Text);

                ParseControlName = "Averaging Time"; 
                AveragingTime = double.Parse(this.textBoxAveragingTime.Text);

                #endregion

                #region Bias
                ParseControlName = "Source1 SourceV"; 
                Source1SourceV = double.Parse(this.textBoxSource1SourceV.Text);

                ParseControlName = "Source2 SourceV"; 
                Source2SourceV = double.Parse(this.textBoxSource2SourceV.Text);

                ParseControlName = "Step Edge"; 
                StepEdge = double.Parse(this.textBoxStepEdge.Text);

                #endregion

                #region BiasSource

                ParseControlName = "Bias Source1 Type"; 
                BiasSource1Type = this.comboBoxBiasSource1Type.SelectedIndex;
                if (BiasSource1Type != 0)
                {
                    ParseControlName = "Bias Source1 Channel"; 
                    BiasSource1Ch = int.Parse(this.comboBoxBiasSource1Ch.Text);
                }

                ParseControlName = "Bias Source1 Bias V"; 
                BiasSource1BiasV = double.Parse(this.textBoxBiasSource1BiasV.Text);

                ParseControlName = "Bias Source1 I Compliance"; 
                BiasSource1Compliance = double.Parse(this.textBoxBiasSource1Compliance.Text);

                ParseControlName = "Bias Source2 Type"; 
                BiasSource2Type = this.comboBoxBiasSource2Type.SelectedIndex;

                if (BiasSource2Type != 0)
                {
                    ParseControlName = "Bias Source2 Channel"; 
                    BiasSource2Ch = int.Parse(this.comboBoxBiasSource2Ch.Text);
                }

                ParseControlName = "Bias Source2 Bias V"; 
                BiasSource2BiasV = double.Parse(this.textBoxBiasSource2BiasV.Text);

                ParseControlName = "Bias Source2 I Compliance"; 
                BiasSource2Compliance = double.Parse(this.textBoxBiasSource2Compliance.Text);
                #endregion
            }
            catch (Exception)
            { 
                throw (new Exception("Input format error at " + ParseControlName));
            }
        }
        #endregion

        private void CreatePattern()
        {

            string Source1InitialPatternName;
            string Source1PatternName;
            string Source1FinalPatternName;
            string Source1MeasEventName;

            string Source2InitialPatternName;
            string Source2PatternName;
            string Source2FinalPatternName;
            string Source2MeasEventName;

            double InitV;
            double InitHold;
            double InitToBaseEdge;
            double BaseV;
            double BaseHold;
            double BaseToSourceEdge;
            double SourceV;
            double SourceDuration;
            double TermV;
            double SourceToTermEdge;
            double TermHold;

            int SamplingPoints;
            int SamplingMode;

            double SamplingMeasDelay;

            double LoopCount;

            double SamplingPeriod;

            SamplingPeriod = 1 / SamplingFreq;
            SamplingPeriod = 5.0E-9 * Math.Round(SamplingPeriod / 5.0E-9);

            if (SamplingFreq > 100E+6 * 1.0000001)
            {
                SamplingPeriod = 10E-9;
                AveragingTime = 10E-9;
                Source1RawData = WGFMU.MEASURE_EVENT_DATA_RAW;
                Source2RawData = WGFMU.MEASURE_EVENT_DATA_RAW;
            }
            else
            {
                Source1RawData = WGFMU.MEASURE_EVENT_DATA_AVERAGED;
                Source2RawData = WGFMU.MEASURE_EVENT_DATA_AVERAGED;
            }


            Source1InitialPatternName = "Src1Init";

            InitV = 0.0;
            InitHold = 0.0;
            InitToBaseEdge = 0.0;
            BaseV = 0.0;
            BaseHold = 0.0;
            BaseToSourceEdge = StepEdge;
            SourceV = Source1SourceV;
            SourceDuration = MeasDelay;
            SourceToTermEdge = 0.0;
            TermV = Source1SourceV;
            TermHold = 0.0;

            wglib.CreateStepWaveform(
                Source1InitialPatternName,
                InitV,
                InitHold,
                InitToBaseEdge,
                BaseV,
                BaseHold,
                BaseToSourceEdge,
                SourceV,
                SourceDuration,
                SourceToTermEdge,
                TermV,
                TermHold);

            Source2InitialPatternName = "Src2Init";

            InitV = 0.0;
            InitHold = 0.0;
            InitToBaseEdge = 0.0;
            BaseV = 0.0;
            BaseHold = 0.0;
            BaseToSourceEdge = StepEdge;
            SourceV = Source2SourceV;
            SourceDuration = MeasDelay;
            SourceToTermEdge = 0.0;
            TermV = Source2SourceV;
            TermHold = 0.0;

            wglib.CreateStepWaveform(
                Source2InitialPatternName,
                InitV,
                InitHold,
                InitToBaseEdge,
                BaseV,
                BaseHold,
                BaseToSourceEdge,
                SourceV,
                SourceDuration,
                SourceToTermEdge,
                TermV,
                TermHold);

        // Create final pattern

            Source1FinalPatternName = "Src1Final";

            InitV = Source1SourceV;
            InitHold = 0.0;
            InitToBaseEdge = 0.0;
            BaseV = Source1SourceV;
            BaseHold = 0.0;
            BaseToSourceEdge = 0.0;
            SourceV = Source1SourceV;
            SourceDuration = 0.0;
            SourceToTermEdge = StepEdge;
            TermV = 0.0;
            TermHold = 0.0;

            wglib.CreateStepWaveform(
                Source1FinalPatternName,
                InitV,
                InitHold,
                InitToBaseEdge,
                BaseV,
                BaseHold,
                BaseToSourceEdge,
                SourceV,
                SourceDuration,
                SourceToTermEdge,
                TermV,
                TermHold);

            Source2FinalPatternName = "Src2Final";

            InitV = Source2SourceV;
            InitHold = 0.0;
            InitToBaseEdge = 0.0;
            BaseV = Source2SourceV;
            BaseHold = 0.0;
            BaseToSourceEdge = 0.0;
            SourceV = Source2SourceV;
            SourceDuration = 0.0;
            SourceToTermEdge = StepEdge;
            TermV = 0.0;
            TermHold = 0.0;

            wglib.CreateStepWaveform(
                Source2FinalPatternName,
                InitV,
                InitHold,
                InitToBaseEdge,
                BaseV,
                BaseHold,
                BaseToSourceEdge,
                SourceV,
                SourceDuration,
                SourceToTermEdge,
                TermV,
                TermHold);

        // Craete measurement pattern

            Source1PatternName = "Src1";
            wglib.CreateDcWaveForm(
                Source1PatternName,
                Source1SourceV,
                SamplingPeriod);

            Source2PatternName = "Src2";
            wglib.CreateDcWaveForm(
                Source2PatternName,
                Source2SourceV,
                SamplingPeriod);

        // Set measurement event
            SamplingMode = 0;            // set linear sampling mode
            SamplingPoints = 1;          // set single measuremenet
            SamplingMeasDelay = 0.0;

            if (Source1MeasEnable == 1)
            {
                Source1MeasEventName = "Src1Meas";
                wglib.SetSamplingMeasEvent(
                    Source1PatternName,
                    Source1MeasEventName,
                    SamplingMode,
                    SamplingPeriod,
                    SamplingPoints,
                    AveragingTime,
                    SamplingMeasDelay,
                    Source1RawData);
            }

            if (Source2MeasEnable == 1)
            {
                Source2MeasEventName = "Src2Meas";
                wglib.SetSamplingMeasEvent(
                    Source2PatternName,
                    Source2MeasEventName,
                    SamplingMode,
                    SamplingPeriod,
                    SamplingPoints,
                    AveragingTime,
                    SamplingMeasDelay,
                    Source2RawData);
            }

        // Set sequence by loopcount


            LoopCount = Math.Round(Duration / SamplingPeriod);


            wglib.AddSequence(
                Source1ChId,
                Source1InitialPatternName,
                1);

            wglib.AddSequence(
                Source2ChId,
                Source2InitialPatternName,
                1);

            wglib.AddSequence(
                    Source1ChId,
                    Source1PatternName,
                    LoopCount);

            wglib.AddSequence(
                    Source2ChId,
                    Source2PatternName,
                    LoopCount);
                
            wglib.AddSequence(
                Source1ChId,
                Source1FinalPatternName,
                1);

            wglib.AddSequence(
                Source2ChId,
                Source2FinalPatternName,
                1);
        }
        #endregion


        private void DATARECORDER_Form_Load(object sender, EventArgs e)
        {
            int SamplingPoints;

            try
            {
                wglib.RestoreSettings(this, ApplicationName, LatestConfigFileName);


                SamplingFreq = double.Parse(this.textBoxSamplingRate.Text);
                Duration = double.Parse(this.textBoxDuration.Text);
                SamplingPoints = (int)Math.Round(Duration * SamplingFreq);
                this.labelSamplingPoints.Text = "Sampling Points: " + SamplingPoints.ToString("#,##0");
            }
            catch
            {
                try
                {
                    wglib.RestoreSettings(this, ApplicationName, DefaultConfigFileName);

                    SamplingFreq = double.Parse(this.textBoxSamplingRate.Text);
                    Duration = double.Parse(this.textBoxDuration.Text);
                    SamplingPoints = (int)Math.Round(Duration * SamplingFreq);
                    this.labelSamplingPoints.Text = "Sampling Points: " + SamplingPoints.ToString("#,##0");
                }
                catch
                { }
            }
        }


        private void comboBoxSource1Ch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.CheckChannelConflict(comboBoxSource1Ch, ref comboBoxSource2Ch);
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void comboBoxSource2Ch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.CheckChannelConflict(comboBoxSource2Ch, ref comboBoxSource1Ch);
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void comboBoxSource1OperationMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                wglib.SetWgfmuForceRange(comboBoxSource1OperationMode, ref comboBoxSource1ForceRange);

                wglib.SetWgfmuMeasMode(comboBoxSource1OperationMode, ref comboBoxSource1MeasMode);

            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }

        }

        private void comboBoxSource2OperationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.SetWgfmuForceRange(comboBoxSource2OperationMode, ref comboBoxSource2ForceRange);

                wglib.SetWgfmuMeasMode(comboBoxSource2OperationMode, ref comboBoxSource2MeasMode);
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void comboBoxSource1MeasMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxSource1MeasMode.SelectedIndex == 1)
                {
                    comboBoxSource1VMeasRange.Enabled = false;

                }
                else
                {
                    comboBoxSource1VMeasRange.Enabled = true;

                    wglib.SetWgfmuMeasRange(comboBoxSource1MeasMode, comboBoxSource1OperationMode, ref comboBoxSource1VMeasRange);

                }
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void comboBoxSource2MeasMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxSource2MeasMode.SelectedIndex == 1)
                {
                    comboBoxSource2VMeasRange.Enabled = false;

                }
                else
                {
                    comboBoxSource2VMeasRange.Enabled = true;

                    wglib.SetWgfmuMeasRange(comboBoxSource2MeasMode, comboBoxSource2OperationMode, ref comboBoxSource2VMeasRange);

                }
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void validPattern_Click(object sender, EventArgs e)
        {
            int[] ChannelList = new int[2];
            int i;
            for (i = 0; i < ChannelList.Length; i++)
            {
                ChannelList[i] = 0;
            }


            try
            {
                this.SetParameters();

                ChannelList[0] = Source1ChId;
                ChannelList[1] = Source2ChId;

                wglib.SaveSettings(this, ApplicationName, LatestConfigFileName);

                wglib.UpdateConfiginfo();

                wglib.WgfmuOnline = 0;          //set wgfmu library to the offline mode.

                wglib.Init();        // Initialize wgfmu session. 

                this.CreatePattern();           // Create pattern data and set sequence

                wglib.ValidatePattern(ref ChannelList);

            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            string FileName = "";

            try
            {
                wglib.GetLoadFileName(out FileName);
                if (FileName != "")
                {
                    wglib.RestoreSettings(this, ApplicationName, FileName);
                }
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void buttonSaveSetup_Click(object sender, EventArgs e)
        {
            string FileName = "";
            try
            {
                wglib.GetSaveFileName("CONFIG", out FileName);
                if (FileName != "")
                {
                    wglib.SaveSettings(this, ApplicationName, FileName);

                }
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            WGFMU_CONFIG wcfg = new WGFMU_CONFIG();
            try
            {
                wcfg.ShowDialog();
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonExecMeas_Click(object sender, EventArgs e)
        {
            string SaveFileName;

            int[] ChannelList = new int[2];
            int i;
            for (i = 0; i < ChannelList.Length; i++)
            {
                ChannelList[i] = 0;
            }


            try
            {
                wglib.GetSaveFileName("DATA", out SaveFileName);

                if (SaveFileName != "")
                {
                    wglib.SaveSettings(this, ApplicationName, SaveFileName);


                    this.SetParameters();

                    ChannelList[0] = Source1ChId;
                    ChannelList[1] = Source2ChId;

                    wglib.SaveSettings(this, ApplicationName, LatestConfigFileName);

                    wglib.UpdateConfiginfo();

                    wglib.WgfmuOnline = 1;          //set wgfmu library to the offline mode.

                    wglib.Init();        // Initialize wgfmu session. 

                    wglib.SetupChannel(
                        Source1ChId,
                        Source1OperationMode,
                        Source1ForceRange,
                        Source1MeasMode,
                        Source1IMeasRange,
                        Source1VMeasRange,
                        Source1MeasEnable,
                        Source1HwSkew);

                    wglib.SetupChannel(
                        Source2ChId,
                        Source2OperationMode,
                        Source2ForceRange,
                        Source2MeasMode,
                        Source2IMeasRange,
                        Source2VMeasRange,
                        Source2MeasEnable,
                        Source2HwSkew);

                    this.CreatePattern();
                    // Create pattern data and set sequence


                    if (BiasSource1Type != 0)
                    {
                        wglib.ForceBias(BiasSource1Type, BiasSource1Ch, BiasSource1BiasV, BiasSource1Compliance);
                    }

                    if (BiasSource2Type != 0)
                    {
                        wglib.ForceBias(BiasSource2Type, BiasSource2Ch, BiasSource2BiasV, BiasSource2Compliance);
                    }

                    wglib.ConnectChannesl(Source1ChId, 1);
                    wglib.ConnectChannesl(Source2ChId, 1);

                    wglib.UpdateChannel(Source1ChId);
                    wglib.UpdateChannel(Source2ChId);

                    wglib.ExecuteMeasurement();

                    wglib.RetrieveResultsByChannelRealTime(ref ChannelList, SaveFileName);

                    if (BiasSource1Type != 0)
                    {
                        wglib.StopBias(BiasSource1Type, BiasSource1Ch);
                    }

                    if (BiasSource2Type != 0)
                    {
                        wglib.StopBias(BiasSource2Type, BiasSource2Ch);
                    }


                    wglib.Close();
                }
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void comboBoxSource1MeasRange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxBiasSource1Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.SetBiasSourceCh(this.comboBoxBiasSource1Type, ref comboBoxBiasSource1Ch, ref textBoxBiasSource1Compliance);
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }

        }

        private void comboBoxBiasSource1Ch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.CheckBiasSourceChConflict(this.comboBoxBiasSource1Type,
                                                 ref this.comboBoxBiasSource1Ch,
                                                 this.comboBoxBiasSource2Type,
                                                 this.comboBoxBiasSource2Ch,
                                                 this.comboBoxSource1Ch,
                                                 this.comboBoxSource2Ch);

            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void comboBoxBiasSource2Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.SetBiasSourceCh(this.comboBoxBiasSource2Type, ref comboBoxBiasSource2Ch, ref textBoxBiasSource2Compliance);
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void comboBoxBiasSource2Ch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.CheckBiasSourceChConflict(this.comboBoxBiasSource2Type,
                                                 ref this.comboBoxBiasSource2Ch,
                                                 this.comboBoxBiasSource1Type,
                                                 this.comboBoxBiasSource1Ch,
                                                 this.comboBoxSource1Ch,
                                                 this.comboBoxSource2Ch);

            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void textBoxSource1HwSkew_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSource1HwSkew, WGFMULib.HWDELAY_MIN, WGFMULib.HWDELAY_MAX, e, errorProvider1);
        }

        private void textBoxSource2HwSkew_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSource2HwSkew, WGFMULib.HWDELAY_MIN, WGFMULib.HWDELAY_MAX, e, errorProvider1);
        }

        private void textBoxBiasSource1BiasV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBiasSource1BiasV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxBiasSource1Compliance_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBiasSource1Compliance, WGFMULib.SMU_ICOMP_MIN, WGFMULib.SMU_ICOMP_MAX, e, errorProvider1);
        }

        private void textBoxBiasSource2BiasV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBiasSource2BiasV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxBiasSource2Compliance_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBiasSource2Compliance, WGFMULib.SMU_ICOMP_MIN, WGFMULib.SMU_ICOMP_MAX, e, errorProvider1);
        }

        private void textBoxMeasurementDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxMeasurementDelay, WGFMULib.MEAS_DELAY_MIN, WGFMULib.MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxDuration_Validating(object sender, CancelEventArgs e)
        {
            int SamplingPoints;
     
            wglib.ValidateDouble(this.textBoxDuration, WGFMULib.DCSTRESS_DURAION_MIN, WGFMULib.DCSTRESS_DURAION_MAX, e, errorProvider1);

            if (e.Cancel != true)
            {

                Duration = double.Parse(this.textBoxDuration.Text);
               
                SamplingFreq = double.Parse(this.textBoxSamplingRate.Text);
                SamplingPoints = (int)Math.Round(Duration * SamplingFreq);
                this.labelSamplingPoints.Text = "Sampling Points: " + SamplingPoints.ToString("#,##0");
            }
        }

        private void textBoxAveragingTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxAveragingTime, WGFMULib.AVERAGING_TIME_MIN, WGFMULib.AVERAGING_TIME_MAX, e, errorProvider1);

        }

        private void textBoxSource1SourceV_Validating(object sender, CancelEventArgs e)
        {
            switch (this.comboBoxBiasSource1Type.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    wglib.ValidateDouble(this.textBoxSource1SourceV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
                    break;
                case 2:
                    wglib.ValidateDouble(this.textBoxSource1SourceV, WGFMULib.SMU_VFORCE_MIN, WGFMULib.SMU_VFORCE_MAX, e, errorProvider1);
                    break;
            }
        }

        private void textBoxSource2SourceV_Validating(object sender, CancelEventArgs e)
        {
            switch (this.comboBoxBiasSource2Type.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    wglib.ValidateDouble(this.textBoxSource2SourceV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
                    break;
                case 2:
                    wglib.ValidateDouble(this.textBoxSource2SourceV, WGFMULib.SMU_VFORCE_MIN, WGFMULib.SMU_VFORCE_MAX, e, errorProvider1);
                    break;
            }
        }

        private void textBoxStepEdge_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStepEdge, WGFMULib.DELTA_TIME_MIN, WGFMULib.DELTA_TIME_MAX, e, errorProvider1);
        }

        private void textBoxSamplingRate_Validating(object sender, CancelEventArgs e)
        {

            int SamplingPoints;
            

            wglib.ValidateDouble(this.textBoxSamplingRate, WGFMULib.SAMPLING_RATE_MIN, WGFMULib.SAMPLING_RATE_MAX, e, errorProvider1);

            if (e.Cancel != true)
            {
                SamplingFreq = double.Parse(this.textBoxSamplingRate.Text);

                if (SamplingFreq > 100E+6 && SamplingFreq != 200E+6)
                {
                    errorProvider1.SetError(this.textBoxSamplingRate, "Sampling rate must be 200E+6 or less than 100E+6");
                    e.Cancel = true;
                }

                Duration = double.Parse(this.textBoxDuration.Text);

                SamplingPoints = (int)Math.Round(Duration * SamplingFreq);

                this.labelSamplingPoints.Text = "Sampling Points: " + SamplingPoints.ToString("#,##0");

                AveragingTime = 1.0 / SamplingFreq;
                if (AveragingTime > WGFMULib.AVERAGING_TIME_MAX)
                {
                    AveragingTime = WGFMULib.AVERAGING_TIME_MAX;
                }
                if (AveragingTime < WGFMULib.AVERAGING_TIME_MIN)
                {
                    AveragingTime = WGFMULib.AVERAGING_TIME_MIN;
                }

                AveragingTime = 10E-9 * Math.Floor(AveragingTime / 10E-9);

                this.textBoxAveragingTime.Text = wglib.DoubleToTrimedString(AveragingTime);
            }
        }


    }
}