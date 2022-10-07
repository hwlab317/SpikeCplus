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
using WGFMU_SAMPLE_Lib;

namespace WGFMU_Sample
{
    public partial class SAMPLING_Form : Form
    {
        WGFMULib wglib = new WGFMULib();
        private string ApplicationName = "SAMPLING";
        private string LatestConfigFileName = Application.StartupPath + @"\SAMPLING.cf";
        private string DefaultConfigFileName = Application.StartupPath + @"\SAMPLINGDefault";

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

        double BaseHold;
        double BaseToSourceEdge;
        double OutputSequenceDelay;
        double TerminationDelay;

        #endregion
        
        #region Primary

        int SamplingMode;
        double MeasurementDelay;
        double InitialInterval;
        int SamplingPoints;
        double AveragingTime;

        #endregion

        #region Secondary

        int SecondMeasEnable;
        int Source1SecondMeasRange;
        int Source2SecondMeasRange;
        double SecondMeasOrigin;
        int SecondSamplingMode;
        double SecondMeasDelay;
        double SecondInitiaInterval;
        int SecondSamplingPoints;
        double SecondAveragingTime;
        
        #endregion

        #region Bias
        double Source1BaseV;
        double Source1SourceV;
        double Source2BaseV;
        double Source2SourceV;

        #endregion

        #region Converted parameters
        
        double MeasOrigin;
        
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


        public SAMPLING_Form()
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

            BaseHold = double.Parse(this.textBoxBaseHold.Text);
            BaseToSourceEdge = double.Parse(this.textBoxBaseToSourceEdge.Text);
            OutputSequenceDelay = double.Parse(this.textBoxOutputSequenceDelay.Text);
            TerminationDelay = double.Parse(this.textBoxTerminationDelay.Text);

            #endregion

            #region Primary

            switch (this.comboBoxSamplingMode.SelectedIndex)
            {
                case 0:
                    SamplingMode = 0;
                    break;
                case 1:
                    SamplingMode = 1;
                    break;
                case 2:
                    SamplingMode = 2;
                    break;
            }

            MeasurementDelay = double.Parse(this.textBoxMeasDelay.Text);
            InitialInterval = double.Parse(this.textBoxInitialInterval.Text);
            SamplingPoints = int.Parse(this.textBoxSamplingPoints.Text);
            AveragingTime = double.Parse(this.textBoxAveragingTime.Text);

            #endregion

            #region Secondary

            if (this.checkBoxEnableSecondaryMeas.Checked == true)
            {
                SecondMeasEnable = 1;
            }
            else 
            {
                SecondMeasEnable = 0;
            }

            if (Source1MeasMode == WGFMU.MEASURE_MODE_CURRENT)
            {
                switch (this.comboBoxSource1SecondMeasRange.SelectedIndex)
                {
                    case 0:
                        Source1SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1UA;
                        break;
                    case 1:
                        Source1SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10UA;
                        break;
                    case 2:
                        Source1SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_100UA;
                        break;
                    case 3:
                        Source1SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1MA;
                        break;
                    case 4:
                        Source1SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10MA;
                        break;
                }
            }
            else
            {
                switch (this.comboBoxSource1SecondMeasRange.SelectedIndex)
                {
                    case 0:
                        Source1SecondMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_5V;
                        break;
                    case 1:
                        Source1SecondMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_10V;
                        break;
                }
            
            }


            if (Source2MeasMode == WGFMU.MEASURE_MODE_CURRENT)
            {
                switch (this.comboBoxSource2SecondMeasRange.SelectedIndex)
                {
                    case 0:
                        Source2SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1UA;
                        break;
                    case 1:
                        Source2SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10UA;
                        break;
                    case 2:
                        Source2SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_100UA;
                        break;
                    case 3:
                        Source2SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1MA;
                        break;
                    case 4:
                        Source2SecondMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10MA;
                        break;
                }
            }
            else
            {
                switch (this.comboBoxSource2SecondMeasRange.SelectedIndex)
                {
                    case 0:
                        Source2SecondMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_5V;
                        break;
                    case 1:
                        Source2SecondMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_10V;
                        break;
                }

            }
            SecondMeasOrigin = double.Parse(this.textBoxSecondMeasOrigin.Text);
            SecondMeasDelay = double.Parse(this.textBoxSecondMeasDelay.Text);

            switch (this.comboBoxSecondSamplingMode.SelectedIndex)
            {
                case 0:
                    SecondSamplingMode = 0;
                    break;
                case 1:
                    SecondSamplingMode = 1;
                    break;
                case 2:
                    SecondSamplingMode = 2;
                    break;
            }

            SecondInitiaInterval = double.Parse(this.textBoxSecondInitialInterval.Text);
            SecondSamplingPoints = int.Parse(this.textBoxSecondSamplingPoints.Text);
            SecondAveragingTime = double.Parse(this.textBoxSecondAveragingTime.Text);

            #endregion

            #region Bias
            Source1BaseV = double.Parse(this.textBoxSource1BaseV.Text);
            Source1SourceV = double.Parse(this.textBoxSource1SourceV.Text);
            Source2BaseV = double.Parse(this.textBoxSource2BaseV.Text);
            Source2SourceV = double.Parse(this.textBoxSource2SourceV.Text);

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
            string Source1PatternName = "";
            string Source2PatternName = "";
            string Source1MeasName = "";
            string Source2MeasName = "";
            int SourceType;
            double InitV;
            double InitHold;
            double InitToBaseEdge;
            double TermV;
            double SourceToTermEdge;
            double TermHold;
            int RawData;

            this.SetParameters();

            #region Create pattern

            #region Create source1

            Source1PatternName = "Src1";
            Source1MeasName = "Src1Meas";
            SourceType = 0;
            RawData = Source1RawData;

            InitV = 0.0;
            InitHold = 0.0;
            InitToBaseEdge = 100E-9;
            TermV = 0.0;
            SourceToTermEdge = 100E-9;
            TermHold = 0.0;
            MeasOrigin = 0.0;


            wglib.CreateSamplingMeasurement(
                SourceType,
                Source1MeasEnable,
                Source1MeasMode,
                Source1PatternName,
                Source1MeasName,
                InitV,
                InitHold,
                InitToBaseEdge,
                Source1BaseV,
                BaseHold,
                BaseToSourceEdge,
                Source1SourceV,
                TermV,
                SourceToTermEdge,
                TermHold,
                SamplingMode,
                MeasurementDelay,
                InitialInterval,
                SamplingPoints,
                AveragingTime,
                TerminationDelay,
                OutputSequenceDelay,
                SecondMeasEnable,
                Source1SecondMeasRange,
                SecondSamplingMode,
                SecondMeasOrigin,
                SecondMeasDelay,
                SecondInitiaInterval,
                SecondSamplingPoints,
                SecondAveragingTime,
                RawData,
                ref MeasOrigin);
            #endregion

            #region Create source2

            Source2PatternName = "Src2";
            Source2MeasName = "Src2Meas";
            SourceType = 1;
            RawData = Source2RawData;

            wglib.CreateSamplingMeasurement(
                SourceType,
                Source2MeasEnable,
                Source2MeasMode,
                Source2PatternName,
                Source2MeasName,
                InitV,
                InitHold,
                InitToBaseEdge,
                Source2BaseV,
                BaseHold,
                BaseToSourceEdge,
                Source2SourceV,
                TermV,
                SourceToTermEdge,
                TermHold,
                SamplingMode,
                MeasurementDelay,
                InitialInterval,
                SamplingPoints,
                AveragingTime,
                TerminationDelay,
                OutputSequenceDelay,
                SecondMeasEnable,
                Source2SecondMeasRange,
                SecondSamplingMode,
                SecondMeasOrigin,
                SecondMeasDelay,
                SecondInitiaInterval,
                SecondSamplingPoints,
                SecondAveragingTime,
                RawData,
                ref MeasOrigin);

            #endregion

            #endregion

            #region set sequence

            wglib.AddSequence(Source1ChId, Source1PatternName, 1);
            wglib.AddSequence(Source2ChId, Source2PatternName, 1);

            #endregion

        }
        #endregion

        #endregion

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
                    comboBoxSource1SecondMeasRange.Enabled = true;
                }
                else
                {
                    comboBoxSource1VMeasRange.Enabled = true;
                    comboBoxSource1SecondMeasRange.Enabled = false;

                    wglib.SetWgfmuMeasRange(comboBoxSource1MeasMode, comboBoxSource1OperationMode, ref comboBoxSource1VMeasRange);
                }

                wglib.SetWgfmuMeasRange(comboBoxSource1MeasMode, comboBoxSource1OperationMode, ref comboBoxSource1SecondMeasRange);

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
                    comboBoxSource2SecondMeasRange.Enabled = true;
                }
                else
                {
                    comboBoxSource2VMeasRange.Enabled = true;
                    comboBoxSource2SecondMeasRange.Enabled = false;

                    wglib.SetWgfmuMeasRange(comboBoxSource2MeasMode, comboBoxSource2OperationMode, ref comboBoxSource2VMeasRange);
                }

                wglib.SetWgfmuMeasRange(comboBoxSource2MeasMode, comboBoxSource2OperationMode, ref comboBoxSource2SecondMeasRange);
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabSeconMeasurement_Click(object sender, EventArgs e)
        {

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

                    //wglib.RetrieveResultsByChannelRealTime(ref ChannelList, SaveFileName);
                    if (OutputSequenceDelay >= 0)
                    {
                        MeasurementHold = BaseHold + 100E-9;
                    }
                    else
                    {
                        MeasurementHold = BaseHold + 100E-9 - OutputSequenceDelay;
                    
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
                we.Message.Remove(0);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
            }
        }

        private void SAMPLING_Form_Load(object sender, EventArgs e)
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

        private void comboBoxSource1ForceRange_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void comboBoxSource1VMeasRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxSource1SecondMeasRange.SelectedIndex = this.comboBoxSource1VMeasRange.SelectedIndex;
        }

        private void comboBoxSource2VMeasRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxSource2SecondMeasRange.SelectedIndex = this.comboBoxSource2VMeasRange.SelectedIndex;
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

        private void textBoxBaseHold_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBaseHold, WGFMULib.HOLD_MIN, WGFMULib.HOLD_MAX, e, errorProvider1);
        }

        private void textBoxBaseToSourceEdge_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBaseToSourceEdge, WGFMULib.EDGE_MIN, WGFMULib.EDGE_MAX, e, errorProvider1);
        }

        private void textBoxOutputSequenceDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxOutputSequenceDelay, WGFMULib.SEQUENCE_DELAY_MIN, WGFMULib.SEQUENCE_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxTerminationDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxTerminationDelay, WGFMULib.TERMINATION_DELAY_MIN, WGFMULib.TERMINATION_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxMeasDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxMeasDelay, WGFMULib.MEAS_DELAY_MIN, WGFMULib.MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxInitialInterval_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxInitialInterval, WGFMULib.SAMPLING_INTERVAL_MIN, WGFMULib.SAMPLING_INTERVAL_MAX, e, errorProvider1);
        }

        private void textBoxSamplingPoints_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateInt(this.textBoxSamplingPoints, WGFMULib.SAMPLING_POINTS_MIN, WGFMULib.SAMPLING_POINTS_MAX, e, errorProvider1);
        }

        private void textBoxAveragingTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxAveragingTime, WGFMULib.AVERAGING_TIME_MIN, WGFMULib.AVERAGING_TIME_MAX, e, errorProvider1);
        }

        private void textBoxSecondMeasOrigin_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSecondMeasOrigin, WGFMULib.DELTA_TIME_MIN, WGFMULib.DELTA_TIME_MAX, e, errorProvider1);
        }

        private void textBoxSecondMeasDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSecondMeasDelay, WGFMULib.MEAS_DELAY_MIN, WGFMULib.MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxSecondInitialInterval_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSecondInitialInterval, WGFMULib.SAMPLING_INTERVAL_MIN, WGFMULib.SAMPLING_INTERVAL_MAX, e, errorProvider1);
        }

        private void textBoxSecondSamplingPoints_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateInt(this.textBoxSecondSamplingPoints, WGFMULib.SAMPLING_POINTS_MIN, WGFMULib.SAMPLING_POINTS_MAX, e, errorProvider1);
        }

        private void textBoxSecondAveragingTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSecondAveragingTime, WGFMULib.AVERAGING_TIME_MIN, WGFMULib.AVERAGING_TIME_MAX, e, errorProvider1);
        }

        private void textBoxSource1BaseV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSource1BaseV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSource1SourceV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSource1SourceV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSource2BaseV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSource2BaseV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSource2SourceV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSource2SourceV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

    }
}