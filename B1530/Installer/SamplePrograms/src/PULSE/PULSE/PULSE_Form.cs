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

namespace PULSE
{
    public partial class PULSE_Form : Form
    {
        WGFMULib wglib = new WGFMULib();
        private string ApplicationName = "PULSE";
        private string LatestConfigFileName = Application.StartupPath + @"\PULSE.cf";
        private string DefaultConfigFileName = Application.StartupPath + @"\PULSEDefault";
        private const int WGFMU_SAMPLE_LIB_ERROR = -42;


        #region Pattern Names

        string SweepSourceInitialPatternName;
        string SweepSourcePatternName;
        string SweepSourceFinalPatternName;
        string SweepSourcePrimMeasName;
        string SweepSourcePrimRangeChangeEventName;
        string SweepSourceSecondMeasName;
        string SweepSourceSecondRangeChangeEventName;

        string ConstSourcePatternName;
        string ConstSourceInitialPatternName;
        string ConstSourceFinalPatternName;
        string ConstSourcePrimMeasName;
        string ConstSourcePrimRangeChangeEventName;
        string ConstSourceSecondMeasName;
        string ConstSourceSecondRangeChangeEventName;

        #endregion

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

        int SweepSourceCh;
        int ConstSourceCh;
        double OutputSequenceDelay;

        #endregion

        #region Primary

        double StartV;
        double StopV;
        int StepNum;
        double Hold;

        int SamplingMode;
        double MeasurementDelay;
        double InitialInterval;
        int SamplingPoints;
        double AveragingTime;

        double ConstBiasV;

        #endregion

        #region secondary

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

        #region Converted parameter
        int SweepSourceMeasEnable;
        int SweepSourceRawdata;
        int SweepSourceMeasMode;
        int SweepSourceMeasRange;
        int SweepSourceSecondMeasRange;
        int ConstSourceMeasEnable;
        int ConstSourceRawData;
        int ConstSourceMeasMode;
        int ConstSourceMeasRange;
        int ConstSourceSecondMeasRange;


        #endregion

        #region Pulse parameters


        double SweepPulseBaseBefore;
        double SweepPulseBaseAfter;
        double SweepPulsePeriod;
        double SweepPulseWidth;
        double SweepPulseRiseTime;
        double SweepPulseFallTime;
        double SweepPulseDelay;

        int ConstPulseEnable;
        double ConstPulseBaseBefore;
        double ConstPulseBaseAfter;
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

        #region User defined functions

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


            if (this.comboBoxSweepSourceCh.SelectedIndex == 0)
            {
                SweepSourceCh = Source1ChId;
                ConstSourceCh = Source2ChId;
            }
            else
            {
                SweepSourceCh = Source2ChId;
                ConstSourceCh = Source1ChId;
            }

            OutputSequenceDelay = double.Parse(this.textBoxOutputSequenceDelay.Text);

            #endregion

            #region Sweep Parameter

            StartV = double.Parse(this.textBoxStartV.Text);
            StopV = double.Parse(this.textBoxStopV.Text);
            StepNum = int.Parse(this.textBoxStepNum.Text);
            Hold = double.Parse(this.textBoxHold.Text);
            ConstBiasV = double.Parse(this.textBoxConstantBiasV.Text);

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

            if (this.checkBoxEnableSecondMeas.Checked == true)
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

            #region Pulse parameters


            SweepPulseBaseBefore = double.Parse(this.textBoxSweepPulseBaseBefore.Text);
            SweepPulseBaseAfter = double.Parse(this.textBoxSweepPulseBaseAfter.Text);
            SweepPulsePeriod = double.Parse(this.textBoxSweepPulsePeriod.Text);
            SweepPulseWidth = double.Parse(this.textBoxSweepPulseWidth.Text);
            SweepPulseRiseTime = double.Parse(this.textBoxSweepPulseRiseTime.Text);
            SweepPulseFallTime = double.Parse(this.textBoxSweepPulseFallTime.Text);
            SweepPulseDelay = double.Parse(this.textBoxSweepPulseDelay.Text);

            if (checkBoxConstPulse.Checked == true)
            {
                ConstPulseEnable = 1;
            }
            else
            {
                ConstPulseEnable = 0;
            }

            ConstPulseBaseBefore = double.Parse(this.textBoxConstPulseBaseBefore.Text);
            ConstPulseBaseAfter = double.Parse(this.textBoxConstPulseBaseAfter.Text);
            ConstPulseWidth = double.Parse(this.textBoxConstPulseWidth.Text);
            ConstPulseRiseTime = double.Parse(this.textBoxConstPulseRiseTime.Text);
            ConstPulseFallTime = double.Parse(this.textBoxConstPulseFallTime.Text);
            ConstPulseDelay = double.Parse(this.textBoxConstPulseDelay.Text);
            #endregion

            #region Conversion
            if (this.comboBoxSweepSourceCh.SelectedIndex == 0)
            {
                SweepSourceCh = Source1ChId;
                ConstSourceCh = Source2ChId;
                SweepSourceRawdata = Source1RawData;
                SweepSourceMeasEnable = Source1MeasEnable;
                SweepSourceMeasMode = Source1MeasMode;
                SweepSourceMeasRange = Source1IMeasRange;
                SweepSourceSecondMeasRange = Source1SecondMeasRange;
                ConstSourceRawData = Source2RawData;
                ConstSourceMeasEnable = Source2MeasEnable;
                ConstSourceMeasMode = Source2MeasMode;
                ConstSourceMeasRange = Source2IMeasRange;
                ConstSourceSecondMeasRange = Source2SecondMeasRange;

            }
            else
            {
                SweepSourceCh = Source2ChId;
                ConstSourceCh = Source1ChId;
                SweepSourceRawdata = Source2RawData;
                SweepSourceMeasEnable = Source2MeasEnable;
                SweepSourceMeasMode = Source2MeasMode;
                SweepSourceMeasRange = Source2IMeasRange;
                SweepSourceSecondMeasRange = Source2SecondMeasRange;
                ConstSourceRawData = Source1RawData;
                ConstSourceMeasEnable = Source1MeasEnable;
                ConstSourceMeasMode = Source1MeasMode;
                ConstSourceMeasRange = Source1IMeasRange;
                ConstSourceSecondMeasRange = Source1SecondMeasRange;

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

        #region Createpattern

        private void CreatePattern()
        {


            #region temporaly paramters to create step pattern

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
            int RawData;

            double ActualMeasDelay;
            double RangeChangeTime;


            int i;
            double StepV;
            double PulseTop;
            string SweepSourceStepPatternName;
            string ConstSourceStepPatternName;
            string SweepSourceStepMeasName;
            string ConstSourceStepMeasName;

            #endregion


            this.SetParameters();

            #region Create pattern

            #region Create initial pattern

            // Create step pattern starting from 0 V to baseline voltage of pulse;

            SweepSourceInitialPatternName = "SweepSrcInitial";
            InitV = 0.0;
            InitToBaseEdge = 0.0;
            BaseV = 0.0;
            BaseHold = 0.0;
            BaseToSourceEdge = SweepPulseRiseTime;
            SourceV = SweepPulseBaseBefore;
            SourceToTermEdge = 0.0;
            TermV = SweepPulseBaseBefore;
            TermHold = 0.0;

            if (OutputSequenceDelay >= 0.0)
            {
                InitHold = 0.0;
                SourceDuration = Hold + OutputSequenceDelay;
            }
            else
            {
                InitHold = -1.0 * OutputSequenceDelay;
                SourceDuration = Hold;
            }

            wglib.CreateStepWaveform(
                SweepSourceInitialPatternName,
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

            // Create step pattern starting from 0 V to consant bias voltage;

            ConstSourceInitialPatternName = "ConstSrcInitial";

            if (ConstPulseEnable != 1)
            {
                SourceV = ConstBiasV;
                TermV = ConstBiasV;
            }
            else
            {
                SourceV = ConstPulseBaseBefore;
                TermV = ConstPulseBaseBefore;
            }

            if (OutputSequenceDelay >= 0.0)
            {
                InitHold = OutputSequenceDelay;
                SourceDuration = Hold;
            }
            else
            {
                InitHold = 0.0;
                SourceDuration = Hold -1.0 * OutputSequenceDelay;
            }

            wglib.CreateStepWaveform(
                ConstSourceInitialPatternName,
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

            #endregion

            #region Create final pattern

            // Create step pattern starting from baseline voltage to 0 V.

            SweepSourceFinalPatternName = "SweepSrcFinal";
            InitV = SweepPulseBaseAfter;
            InitToBaseEdge = 0.0;
            BaseV = SweepPulseBaseAfter;
            BaseHold = 0.0;
            BaseToSourceEdge = 0.0;
            SourceV = SweepPulseBaseAfter;
            SourceToTermEdge = SweepPulseFallTime;
            TermV = 0.0;
            TermHold = 0.0;

            if (OutputSequenceDelay >= 0.0)
            {
                InitHold = 0.0;
                SourceDuration = OutputSequenceDelay;
                TermHold = 0.0;
            }
            else
            {
                InitHold = 0.0;
                SourceDuration = 0.0;
                TermHold = -1.0 * OutputSequenceDelay;
            }

            wglib.CreateStepWaveform(
                SweepSourceFinalPatternName,
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

            // Create step pattern starting from consant bias voltage to 0 V.

            ConstSourceFinalPatternName = "ConstSrcFinal";

            if (ConstPulseEnable != 1)
            {
                InitV = ConstBiasV;
                BaseV = ConstBiasV;
                SourceV = ConstBiasV;
            }
            else
            {
                InitV = ConstPulseBaseAfter;
                BaseV = ConstPulseBaseAfter;
                SourceV = ConstPulseBaseAfter; 
            }
            InitToBaseEdge = 0.0;
            BaseHold = 0.0;
            BaseToSourceEdge = 0.0;
            SourceToTermEdge = SweepPulseFallTime;
            TermV = 0.0;
            TermHold = 0.0;

            if (OutputSequenceDelay >= 0.0)
            {
                InitHold = 0.0 ;
                SourceDuration = 0.0;
                TermHold = OutputSequenceDelay;
            }
            else
            {
                InitHold = -1.0 * OutputSequenceDelay; ;
                SourceDuration = 0.0;
                TermHold = 0.0;
            }

            wglib.CreateStepWaveform(
                ConstSourceFinalPatternName,
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

            #endregion

            #region Create patterns of measurement


            SweepSourcePatternName = "SweepSrc";
            SweepSourcePrimMeasName = "SweepSrcPrimMeas";
            SweepSourcePrimRangeChangeEventName = "SweepSrcPrimRangeChange";
            SweepSourceSecondMeasName = "SweepSrcSecondMeas";
            SweepSourceSecondRangeChangeEventName = "SweepSrcSecondRangeChange";
            RawData = Source1RawData;

            ConstSourcePatternName = "ConstSrc";
            ConstSourcePrimMeasName = "ConstSrcPimMeas";
            ConstSourcePrimRangeChangeEventName = "ConstSrcPrimRangeChange";
            ConstSourceSecondMeasName = "ConstSrcSecondMeas";
            ConstSourceSecondRangeChangeEventName = "ConstSrcSecondRangeChange";
            RawData = Source2RawData;



            if (StepNum != 1)
            {
                StepV = (StopV - StartV) / (StepNum - 1);
            }
            else
            {
                StepV = 0.0;
            }

            for (i = 0; i < StepNum; i++)
            {

                #region Create waveform
                // Create sweep source pattern of single period
                PulseTop = StartV + StepV * i;
                SweepSourceStepPatternName = SweepSourcePatternName + i.ToString();

                wglib.CreateSingleFlashWaveform(
                    SweepSourceStepPatternName,
                    SweepPulseBaseBefore,
                    SweepPulseBaseAfter,
                    PulseTop,
                    SweepPulsePeriod,
                    SweepPulseDelay,
                    SweepPulseWidth,
                    SweepPulseRiseTime,
                    SweepPulseFallTime);

                // Create constant bias source pattern of single period
                ConstSourceStepPatternName = ConstSourcePatternName + i.ToString();
                if (ConstPulseEnable != 1)
                {
                    wglib.CreateDcWaveForm(
                        ConstSourceStepPatternName,
                        ConstBiasV,
                        SweepPulsePeriod);
                }
                else
                {
                    wglib.CreateSingleFlashWaveform(
                        ConstSourceStepPatternName,
                        ConstPulseBaseBefore,
                        ConstPulseBaseAfter,
                        ConstBiasV,
                        SweepPulsePeriod,
                        ConstPulseDelay,
                        ConstPulseWidth,
                        ConstPulseRiseTime,
                        ConstPulseFallTime);
                }
                #endregion

                #region Set measurement event
                SweepSourceStepMeasName = SweepSourcePrimMeasName + i.ToString();
                ConstSourceStepMeasName = ConstSourcePrimMeasName + i.ToString();
                ActualMeasDelay = MeasurementDelay + SweepPulseDelay;

                if (SweepSourceMeasEnable == 1)
                {
                    // Add measurement event to the sweep source pattern
                    wglib.SetSamplingMeasEvent(
                        SweepSourceStepPatternName,
                        SweepSourceStepMeasName,
                        SamplingMode,
                        InitialInterval,
                        SamplingPoints,
                        AveragingTime,
                        ActualMeasDelay,
                        Source1RawData);
                }

                if (ConstSourceMeasEnable == 1)
                {
                    // Add measurement event to the constant bias source pattern
                    wglib.SetSamplingMeasEvent(
                        ConstSourceStepPatternName,
                        ConstSourceStepMeasName,
                        SamplingMode,
                        InitialInterval,
                        SamplingPoints,
                        AveragingTime,
                        ActualMeasDelay,
                        Source1RawData);
                }

                if (SecondMeasEnable == 1)
                {

                        if (SecondMeasOrigin < MeasurementDelay + InitialInterval * (SamplingPoints - 1) + AveragingTime)
                        {
                            wglib.Close();
                            throw (new WGFMULibException(WGFMU_SAMPLE_LIB_ERROR, "SecondMeasOrigin is too short"));
                        }

                        SweepSourceStepMeasName = SweepSourceSecondMeasName + i.ToString();
                        ConstSourceStepMeasName = ConstSourceSecondMeasName + i.ToString();

                        #region Add measurement event to the sweep source pattern

                        ActualMeasDelay = SecondMeasOrigin + SecondMeasDelay + SweepPulseDelay;

                        if (SweepSourceMeasEnable == 1)
                        {
                            // Add measurement event to the sweep source pattern
                            wglib.SetSamplingMeasEvent(
                                SweepSourceStepPatternName,
                                SweepSourceStepMeasName,
                                SecondSamplingMode,
                                SecondInitiaInterval,
                                SecondSamplingPoints,
                                SecondAveragingTime,
                                ActualMeasDelay,
                                Source1RawData);

                            if (SweepSourceMeasMode == WGFMU.MEASURE_MODE_CURRENT)
                            {
                                // Add range change event for primary measurement
                                if (SweepSourceMeasRange != SweepSourceSecondMeasRange)
                                {
                                    RangeChangeTime = 0.0;
                                    wglib.SetMeasRangeEvent(
                                        SweepSourceStepPatternName,
                                        SweepSourcePrimRangeChangeEventName,
                                        RangeChangeTime,
                                        SweepSourceMeasRange);

                                    // Add range change event for secondary measurement
                                    RangeChangeTime = SecondMeasOrigin + SweepPulseDelay;
                                    wglib.SetMeasRangeEvent(
                                        SweepSourceStepPatternName,
                                        SweepSourceSecondRangeChangeEventName,
                                        RangeChangeTime,
                                        SweepSourceSecondMeasRange);
                                }
                            }
                        }

                    #endregion

                    #region Add measurement event to the constant bias source pattern

                    if (ConstSourceMeasEnable == 1)
                    {
                        // Add measurement event to the constant bias source pattern
                        wglib.SetSamplingMeasEvent(
                            ConstSourceStepPatternName,
                            ConstSourceStepMeasName,
                            SecondSamplingMode,
                            SecondInitiaInterval,
                            SecondSamplingPoints,
                            SecondAveragingTime,
                            ActualMeasDelay,
                            Source1RawData);

                        if (ConstSourceMeasMode == WGFMU.MEASURE_MODE_CURRENT)
                        {
                            if (ConstSourceMeasRange != ConstSourceSecondMeasRange)
                            {
                                // Add range change event for primary measurement
                                RangeChangeTime = 0.0;
                                wglib.SetMeasRangeEvent(
                                    ConstSourceStepPatternName,
                                    ConstSourcePrimRangeChangeEventName,
                                    RangeChangeTime,
                                    ConstSourceMeasRange);

                                // Add range change event for secondary measurement
                                RangeChangeTime = SecondMeasOrigin + SweepPulseDelay;
                                wglib.SetMeasRangeEvent(
                                    ConstSourceStepPatternName,
                                    ConstSourceSecondRangeChangeEventName,
                                    RangeChangeTime,
                                    ConstSourceSecondMeasRange);
                            }
                        }
                    }
                    #endregion
                }

                #endregion

            }


            #endregion

            #endregion

            #region set sequence

            wglib.AddSequence(
                SweepSourceCh,
                SweepSourceInitialPatternName,
                1);


            wglib.AddSequence(
                ConstSourceCh,
                ConstSourceInitialPatternName,
                1);



            for (i = 0; i < StepNum; i++)
            {
                SweepSourceStepPatternName = SweepSourcePatternName + i.ToString();

                wglib.AddSequence(
                    SweepSourceCh,
                    SweepSourceStepPatternName,
                    1);

                ConstSourceStepPatternName = ConstSourcePatternName + i.ToString();

                wglib.AddSequence(
                    ConstSourceCh,
                    ConstSourceStepPatternName,
                    1);                        
            }


            wglib.AddSequence(
                SweepSourceCh,
                SweepSourceFinalPatternName,
                1);

            wglib.AddSequence(
                ConstSourceCh,
                ConstSourceFinalPatternName,
                1);


            #endregion

        }

        #endregion

        #endregion

        public PULSE_Form()
        {
            InitializeComponent();
        }

        private void groupVgSweep_Enter(object sender, EventArgs e)
        {

        }

        private void groupSweepVgPulse_Enter(object sender, EventArgs e)
        {

        }

        private void PULSED_Form_Load(object sender, EventArgs e)
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

                ChannelList[0] = SweepSourceCh;
                ChannelList[1] = ConstSourceCh;

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupSweepVdPulse_Enter(object sender, EventArgs e)
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxSource1Meas_CheckedChanged(object sender, EventArgs e)
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

        private void comboSweepSource_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void buttonExecMeas_Click(object sender, EventArgs e)
        {

            string SaveFileName;
            WGFMU_PROGRESS wgpg = new WGFMU_PROGRESS();
            
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
                    ChannelList[1] = ConstSourceCh;

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


                    wglib.ConnectChannesl(Source1ChId, 1);
                    wglib.ConnectChannesl(Source2ChId, 1);

                    wglib.UpdateChannel(Source1ChId);
                    wglib.UpdateChannel(Source2ChId);

                    if (BiasSource1Type != 0)
                    {
                        wglib.ForceBias(BiasSource1Type, BiasSource1Ch, BiasSource1BiasV, BiasSource1Compliance);
                    }

                    if (BiasSource2Type != 0)
                    {
                        wglib.ForceBias(BiasSource2Type, BiasSource2Ch, BiasSource2BiasV, BiasSource2Compliance);
                    }

                    wglib.ExecuteMeasurement();

                    if (OutputSequenceDelay >= 0)
                    {
                        MeasurementHold = Hold + SweepPulseDelay + SweepPulseRiseTime; // SweepPulseRiseTime is a initial transient
                    }
                    else
                    {
                        MeasurementHold = Hold + SweepPulseDelay - OutputSequenceDelay + SweepPulseRiseTime ;
                    }

                    wglib.RetrieveResultsByPatternRealTime(ref ChannelList, SaveFileName, MeasurementHold);

                    if (BiasSource2Type != 0)
                    {
                        wglib.StopBias(BiasSource2Type, BiasSource2Ch);
                    }

                    if (BiasSource1Type != 0)
                    {
                        wglib.StopBias(BiasSource1Type, BiasSource1Ch);
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

        private void textBoxSweepPulseDelay_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void textBoxOutputSequenceDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxOutputSequenceDelay, WGFMULib.SEQUENCE_DELAY_MIN, WGFMULib.SEQUENCE_DELAY_MAX, e, errorProvider1);
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


        private void textBoxHold_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxHold, WGFMULib.SWEEP_HOLD_MIN, WGFMULib.SWEEP_HOLD_MAX, e, errorProvider1);
        }

        private void textBoxAveragingTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxAveragingTime, WGFMULib.AVERAGING_TIME_MIN, WGFMULib.AVERAGING_TIME_MAX, e, errorProvider1);
        }

        private void textBoxConstantBiasV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstantBiasV, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSweepPulseBase_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulseBaseBefore, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
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

        private void textBoxSweepPulseFallTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulseFallTime, WGFMULib.PULSE_EDGE_MIN, WGFMULib.PULSE_EDGE_MAX, e, errorProvider1);
        }

        private void textBoxSweepPulseDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulseDelay, WGFMULib.PULSE_DELAY_MIN, WGFMULib.PULSE_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseBase_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseBaseBefore, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseWidth_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseWidth, WGFMULib.PULSE_WIDTH_MIN, WGFMULib.PULSE_WIDTH_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseRiseTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseRiseTime, WGFMULib.PULSE_EDGE_MIN, WGFMULib.PULSE_EDGE_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseFallTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseFallTime, WGFMULib.PULSE_EDGE_MIN, WGFMULib.PULSE_EDGE_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseDelay, WGFMULib.PULSE_DELAY_MIN, WGFMULib.PULSE_DELAY_MAX, e, errorProvider1);
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

        private void textBoxSweepPulseBaseAfter_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulseBaseAfter, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxConstPulseBaseAfter_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxConstPulseBaseAfter, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }


    }
}