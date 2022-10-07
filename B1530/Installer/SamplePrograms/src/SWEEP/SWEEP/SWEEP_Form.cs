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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WGFMU_SAMPLE_Lib;

namespace SWEEP
{
    
    public partial class SWEEP_Form : Form
    {
        WGFMULib wglib = new WGFMULib();

        private string ApplicationName = "SWEEP";
        private string LatestConfigFileName = Application.StartupPath + @"\SWEEP.cf";
        private string DefaultConfigFileName = Application.StartupPath + @"\SWEEPDefault";

        #region MeasurementParameters
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

        #region Sequence

        int SweepType;
        int SweepSourceCh;
        int ConstantSouceCh;
        double TerminationDelay;
        int EnableDualSweep;
        double DualSweepDelay;
        double OutputSequenceDelay;

        #endregion

        #region Primary

        double StartV;
        double StopV;
        int StepNum;
        double StepEdge;
        double Hold;
        double MeasDelay;
        double AveragingTime;
        double StepDelay;

        double ConstBiasV;

        #endregion

        #region Converted parameter
        int SweepSourceMeasEnable;
        int SweepSourceRawdata;
        int SweepSourceMeasRange;
        int ConstSourceMeasEnable;
        int ConstSourceRawData;
        int ConstSourceMeasRange;


        double MeasOrigin;        
        
        #endregion

        #region Pulse parameters


        double SweepPulseBase;
        double SweepPulsePeriod;
        double SweepPulseWidth;
        double SweepPulseRiseTime;
        double SweepPulseFallTime;
        double SweepPulseDelay;

        int ConstPulseEnable;
        double ConstPulseBase;
        double ConstPulseWidth;
        double ConstPulseRiseTime;
        double ConstPulseFallTime;
        double ConstPulseDelay;

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

        #endregion

        public SWEEP_Form()
        {
            InitializeComponent();
        }

        #region Userdefined functions

        #region SetParameters
        private void SetParameters()
        {
            #region Source1 setup

            Source1ChId = int.Parse(this.comboBoxSource1Ch.Text);

            if (checkBoxSource1Meas.Checked == true)
            {
                Source1MeasEnable = 1;
            }
            else
            {
                Source1MeasEnable = 0;
            }

            switch (this.comboBoxSource1OperationMode.SelectedIndex)
            {
                case 0:
                    Source1OperationMode = WGFMU.OPERATION_MODE_FASTIV; // Set Fast IV mode
                    break;
                case 1:
                    Source1OperationMode = WGFMU.OPERATION_MODE_PG; // Set PG mode
                    break;

            }

            switch (this.comboBoxSource1MeasMode.SelectedIndex)
            {
                case 0:
                    Source1MeasMode = WGFMU.MEASURE_MODE_VOLTAGE; // Set voltage measurement mode
                    break;
                case 1:
                    Source1MeasMode = WGFMU.MEASURE_MODE_CURRENT; // Set current measurement mode
                    break;
            }

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


            switch (this.comboBoxSource1VMeasRange.SelectedIndex)
            {
                case 0:
                    Source1VMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_5V;
                    break;
                case 1:
                    Source1VMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_10V;
                    break;
            }


            Source1HwSkew = double.Parse(this.textBoxSource1HwSkew.Text);

            if (this.checkBoxSource1RawData.Checked == true)
            {
                Source1RawData = WGFMU.MEASURE_EVENT_DATA_RAW;
            }
            else
            {
                Source1RawData = WGFMU.MEASURE_EVENT_DATA_AVERAGED;
            }

            #endregion

            #region Source2 setup

            Source2ChId = int.Parse(this.comboBoxSource2Ch.Text);

            if (checkBoxSource2Meas.Checked == true)
            {
                Source2MeasEnable = 1;
            }
            else
            {
                Source2MeasEnable = 0;
            }

            switch (this.comboBoxSource2OperationMode.SelectedIndex)
            {
                case 0:
                    Source2OperationMode = WGFMU.OPERATION_MODE_FASTIV; // Set Fast IV mode
                    break;
                case 1:
                    Source2OperationMode = WGFMU.OPERATION_MODE_PG; // Set PG mode
                    break;

            }

            switch (this.comboBoxSource2MeasMode.SelectedIndex)
            {
                case 0:
                    Source2MeasMode = WGFMU.MEASURE_MODE_VOLTAGE; // Set voltage measurement mode
                    break;
                case 1:
                    Source2MeasMode = WGFMU.MEASURE_MODE_CURRENT; // Set current measurement mode
                    break;
            }

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


            switch (this.comboBoxSource2VMeasRange.SelectedIndex)
            {
                case 0:
                    Source2VMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_5V;
                    break;
                case 1:
                    Source2VMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_10V;
                    break;
            }

            Source2HwSkew = double.Parse(this.textBoxSource2HwSkew.Text);

            if (this.checkBoxSource2RawData.Checked == true)
            {
                Source2RawData = WGFMU.MEASURE_EVENT_DATA_RAW;
            }
            else
            {
                Source2RawData = WGFMU.MEASURE_EVENT_DATA_AVERAGED;
            }

            #endregion

            #region Sequence

            SweepType = this.comboBoxSweepType.SelectedIndex;

            if (this.comboSweepSourceCh.SelectedIndex == 0)
            {
                SweepSourceCh = Source1ChId;
                ConstantSouceCh = Source2ChId;
            }
            else
            {
                SweepSourceCh = Source2ChId;
                ConstantSouceCh = Source1ChId;
            }

            OutputSequenceDelay = double.Parse(this.textBoxOutputSequenceDelay.Text);

            if (this.checkBoxDualSweep.Checked == true)
            {
                EnableDualSweep = 1;
            }
            else
            {
                EnableDualSweep = 0;            
            }

            DualSweepDelay = double.Parse(this.textBoxDualSweepDelay.Text);


            TerminationDelay = double.Parse(this.textBoxTerminationDelay.Text);

            #endregion

            #region Primary

            StartV = double.Parse(this.textBoxStartV.Text);
            StopV = double.Parse(this.textBoxStopV.Text);
            StepNum = int.Parse(this.textBoxStepNum.Text);
            StepEdge = double.Parse(this.textBoxStepEdge.Text);
            Hold = double.Parse(this.textBoxHold.Text);
            MeasDelay = double.Parse(this.textBoxMeasurementDelay.Text);
            AveragingTime = double.Parse(this.textBoxAveragingTime.Text);
            StepDelay = double.Parse(this.textBoxStepDelay.Text);
            ConstBiasV = double.Parse(this.textBoxConstantBiasV.Text);

            #endregion

            #region Pulse parameters


            SweepPulseBase = double.Parse(this.textBoxSweepPulseBase.Text);
            SweepPulsePeriod = double.Parse(this.textBoxSweepPulsePeriod.Text);
            SweepPulseWidth = double.Parse(this.textBoxSweepPulseWidth.Text);
            SweepPulseRiseTime = double.Parse(this.textBoxSweepPulseRiseTime.Text);
            SweepPulseFallTime = double.Parse(this.textBoxSweepPulseRiseTime.Text);
            SweepPulseDelay = double.Parse(this.textBoxSweepPulseDelay.Text);

            if (checkBoxConstPulse.Checked == true)
            {
                ConstPulseEnable = 1;
            }
            else
            {
                ConstPulseEnable = 0;
            }

            ConstPulseBase = double.Parse(this.textBoxConstPulseBase.Text);
            ConstPulseWidth = double.Parse(this.textBoxConstPulseWidth.Text);
            ConstPulseRiseTime = double.Parse(this.textBoxConstPulseRiseTime.Text);
            ConstPulseFallTime = double.Parse(this.textBoxConstPulseRiseTime.Text);
            ConstPulseDelay = double.Parse(this.textBoxConstPulseDelay.Text);
            #endregion

            #region Conversion
            if (this.comboSweepSourceCh.SelectedIndex == 0)
            {
                SweepSourceCh = Source1ChId;
                ConstantSouceCh = Source2ChId;
                SweepSourceRawdata = Source1RawData;
                SweepSourceMeasEnable = Source1MeasEnable;
                SweepSourceMeasRange = Source1IMeasRange;
                ConstSourceRawData = Source2RawData;
                ConstSourceMeasEnable = Source2MeasEnable;
                ConstSourceMeasRange = Source2IMeasRange;
            }
            else
            {
                SweepSourceCh = Source2ChId;
                ConstantSouceCh = Source1ChId;
                SweepSourceRawdata = Source2RawData;
                SweepSourceMeasEnable = Source2MeasEnable;
                SweepSourceMeasRange = Source2IMeasRange;
                ConstSourceRawData = Source1RawData;
                ConstSourceMeasEnable = Source1MeasEnable;
                ConstSourceMeasRange = Source1IMeasRange;
            }
            #endregion

            #region BiasSource
            BiasSource1Type = this.comboBoxBiasSource1Type.SelectedIndex;
            if (BiasSource1Type != 0)
            {
                BiasSource1Ch = int.Parse(this.comboBoxBiasSource1Ch.Text);
            }

            BiasSource1BiasV = double.Parse(this.textBoxBiasSource1BiasV.Text);
            BiasSource1Compliance = double.Parse(this.textBoxBiasSource1Compliance.Text);

            BiasSource2Type = this.comboBoxBiasSource2Type.SelectedIndex;

            if (BiasSource2Type != 0)
            {
                BiasSource2Ch = int.Parse(this.comboBoxBiasSource2Ch.Text);
            } BiasSource2BiasV = double.Parse(this.textBoxBiasSource2BiasV.Text);
            BiasSource2Compliance = double.Parse(this.textBoxBiasSource2Compliance.Text);
            #endregion
        }
        #endregion

        #region CreatePattern
        private void CreatePattern()
        {
            string SweepSourcePatternName;
            string ConstSourcePatternName;
            string MeasEventName;
            double InitV = 0.0;
            double InitHold = 0.0;
            double InitToStartTransient = StepEdge;
            double InitToBaseTransient = 0.0;
            double TermV = 0.0;
            double StopToTermTransient = StepEdge;
            double TermHold = 0.0;
            double BaseV = 0.0;
            double BaseToTermTransient = 0.0;

            BaseToTermTransient = SweepPulseFallTime;

            #region Create sweep source pattern

            SweepSourcePatternName = "SweepSrc";
            MeasEventName = "SweepSrcMeas";
            MeasOrigin = 0.0;
            if (SweepType == 0)
            {
                wglib.CreateStaircaseSweepSource(
                    SweepSourceMeasEnable,
                    SweepSourcePatternName,
                    MeasEventName,
                    OutputSequenceDelay,
                    InitV,
                    InitHold,
                    InitToStartTransient,
                    Hold,
                    StartV,
                    StopV,
                    StepNum,
                    StepEdge,
                    MeasDelay,
                    AveragingTime,
                    StepDelay,
                    TerminationDelay,
                    TermV,
                    StopToTermTransient,
                    TermHold,
                    EnableDualSweep,
                    DualSweepDelay,
                    SweepSourceRawdata,
                    ref MeasOrigin);
            }
            else if (SweepType == 1)
            {
                wglib.CreateRampSweepSource(
                    SweepSourceMeasEnable,
                    SweepSourcePatternName,
                    MeasEventName,
                    OutputSequenceDelay,
                    InitV,
                    InitHold,
                    InitToStartTransient,
                    Hold,
                    StartV,
                    StopV,
                    StepNum,
                    StepEdge,
                    MeasDelay,
                    AveragingTime,
                    StepDelay,
                    TerminationDelay,
                    TermV,
                    StopToTermTransient,
                    TermHold,
                    EnableDualSweep,
                    DualSweepDelay,
                    SweepSourceRawdata,
                    ref MeasOrigin);

            }
            else 
            {
                InitToBaseTransient = SweepPulseRiseTime;
                BaseToTermTransient = SweepPulseRiseTime;

                wglib.CreatePulseSweepSource(
                    SweepSourceMeasEnable,
                    SweepSourcePatternName,
                    MeasEventName,
                    OutputSequenceDelay,
                    InitV,
                    InitHold,
                    InitToBaseTransient,
                    Hold,
                    SweepPulseBase,
                    StartV,
                    StopV,
                    StepNum,
                    SweepPulsePeriod,
                    SweepPulseWidth,
                    SweepPulseRiseTime,
                    SweepPulseFallTime,
                    SweepPulseDelay,
                    MeasDelay,
                    AveragingTime,
                    TerminationDelay,
                    TermV,
                    BaseToTermTransient,
                    TermHold,
                    EnableDualSweep,
                    DualSweepDelay,
                    ref MeasOrigin);
            }

            #endregion

            #region Create constant bias source pattern
            ConstSourcePatternName = "ConstSrc";
            MeasEventName = "ConstSrcMeas";

            if (SweepType == 0 || SweepType == 1)
            {
                wglib.CreateStaircaseConstantSource(
                    ConstSourceMeasEnable,
                    ConstSourcePatternName,
                    MeasEventName,
                    OutputSequenceDelay,
                    InitV,
                    InitHold,
                    InitToBaseTransient,
                    BaseV,
                    StepEdge,
                    Hold,
                    ConstBiasV,
                    StepNum,
                    StepEdge,
                    MeasDelay,
                    AveragingTime,
                    StepDelay,
                    TerminationDelay,
                    StopToTermTransient,
                    TermV,
                    TermHold,
                    EnableDualSweep,
                    DualSweepDelay,
                     ConstSourceRawData);
            }
            else
            {
                InitToBaseTransient = SweepPulseRiseTime;
                BaseToTermTransient = SweepPulseRiseTime;              
                wglib.CreatePulseConstantSource(
                    ConstSourceMeasEnable,
                    ConstSourcePatternName,
                    MeasEventName,
                    ConstPulseEnable,
                    OutputSequenceDelay,
                    InitV,
                    InitHold,
                    InitToBaseTransient,
                    Hold,
                    ConstPulseBase,
                    ConstBiasV,
                    StepNum,
                    SweepPulsePeriod,
                    ConstPulseWidth,
                    ConstPulseRiseTime,
                    ConstPulseFallTime,
                    ConstPulseDelay,
                    MeasDelay,
                    AveragingTime,
                    TerminationDelay,
                    TermV,
                    BaseToTermTransient,
                    TermHold,
                    EnableDualSweep,
                    DualSweepDelay);

                   


            
            }


            #endregion

            #region set sequence

            wglib.AddSequence(SweepSourceCh, SweepSourcePatternName, 1);
            wglib.AddSequence(ConstantSouceCh, ConstSourcePatternName, 1);

            #endregion

        }


        #endregion

        #endregion

        private void tab2ndSegment_Click(object sender, EventArgs e)
        {

        }

        private void SWEEP_Form_Load(object sender, EventArgs e)
        {
            try
            {
                wglib.RestoreSettings(this, ApplicationName, LatestConfigFileName);
            }
            catch
            {
                try
                {
                    wglib.RestoreSettings(this, ApplicationName, DefaultConfigFileName);
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

                ChannelList[0] = SweepSourceCh;
                ChannelList[1] = ConstantSouceCh;

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

            double MeasurementHold;

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

                    ChannelList[0] = SweepSourceCh;
                    ChannelList[1] = ConstantSouceCh;

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

                    this.CreatePattern();           // Create pattern data and set sequence

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

                    if (OutputSequenceDelay >= 0.0)
                    {
                        MeasurementHold = Hold + StepEdge;
                    }
                    else
                    {
                        MeasurementHold = Hold + StepEdge - OutputSequenceDelay;
                    }

                    wglib.RetrieveResultsByPatternRealTime(ref ChannelList, SaveFileName, MeasurementHold);

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

        private void comboBoxSource1VMeasRange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSource2VMeasRange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSource2ForceRange_SelectedIndexChanged(object sender, EventArgs e)
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

        private void textBoxOutputSequenceDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxOutputSequenceDelay, WGFMULib.SEQUENCE_DELAY_MIN, WGFMULib.SEQUENCE_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxTerminationDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxTerminationDelay, WGFMULib.TERMINATION_DELAY_MIN, WGFMULib.TERMINATION_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxDualSweepDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxDualSweepDelay, WGFMULib.DUAL_SWEEP_DELAY_MIN, WGFMULib.DUAL_SWEEP_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxStartV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStartV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxStopV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStopV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxStepNum_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateInt(this.textBoxStepNum, WGFMULib.SWEEP_STEP_MIN, WGFMULib.SWEEP_STEP_MAX, e, errorProvider1);
        }

        private void textBoxStepEdge_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStepEdge, WGFMULib.EDGE_MIN, WGFMULib.EDGE_MAX, e, errorProvider1);
        }

        private void textBoxHold_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxHold, WGFMULib.SWEEP_HOLD_MIN, WGFMULib.SWEEP_HOLD_MAX, e, errorProvider1);
        }

        private void textBoxMeasurementDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxMeasurementDelay, WGFMULib.MEAS_DELAY_MIN, WGFMULib.MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxAveragingTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxAveragingTime, WGFMULib.AVERAGING_TIME_MIN, WGFMULib.AVERAGING_TIME_MAX, e, errorProvider1);
        }

        private void textBoxStepDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStepDelay, WGFMULib.SWEEP_STEP_DELAY_MIN, WGFMULib.SWEEP_STEP_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxConstantBiasV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstantBiasV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSweepPulseBase_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulseBase, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSweepPulsePeriod_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulsePeriod, WGFMULib.PULSE_PERIOD_MIN, WGFMULib.PULSE_PERIOD_MAX, e, errorProvider1);
        }

        private void textBoxSweepPulseWidth_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulseWidth, WGFMULib.PULSE_WIDTH_MIN, WGFMULib.PULSE_WIDTH_MAX, e, errorProvider1);
        }

        private void textBoxSweepPulseRiseTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulseRiseTime, WGFMULib.PULSE_EDGE_MIN, WGFMULib.PULSE_EDGE_MAX, e, errorProvider1);
        }

        private void textBoxSweepPulseDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulseDelay, WGFMULib.PULSE_DELAY_MIN, WGFMULib.PULSE_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseBase_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseBase, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseWidth_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseWidth, WGFMULib.PULSE_WIDTH_MIN, WGFMULib.PULSE_WIDTH_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseRiseTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseRiseTime, WGFMULib.PULSE_EDGE_MIN, WGFMULib.PULSE_EDGE_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseDelay, WGFMULib.PULSE_DELAY_MIN, WGFMULib.PULSE_DELAY_MAX, e, errorProvider1);
        }




    }
}