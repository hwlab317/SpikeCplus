using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

// NBTI_form.cs Rev. A.01.01.2009.01.07


using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WGFMU_SAMPLE_Lib;

namespace NBTI
{
    public partial class NBTI_Form : Form
    {
        #region define private variables

        WGFMULib wglib = new WGFMULib();
        private string ApplicationName = "NBTI";
        private string LatestConfigFileName = Application.StartupPath + @"\NBTI.cf";
        private string DefaultConfigFileName = Application.StartupPath + @"\NBTIDefault";

        #endregion

        #region Variables for measurements

        #region PatternName

        string GateInitialStressName = "GatePatternStressInit";
        string GateStressNameHeader = "GatePatternStress";
        string GateReferenceMeasName = "GatePatternRefMeas";
        string GateReferenceMeasEventName = "GateMeasRefMeas";
        string GatePostMeasName = "GatePatternPostMeas";
        string GatePostMeasEventName = "GateMeasPostMeas";
        string GatePostMeasInteravalName = "GatePatternPostInterval";
        string GateMeasInStressName = "GatePatternMeasDuringStress";
        string GateMeasLastStressName = "GatePatternMeasLastStress";
        string GateMeasInStressEventName = "GateMeasDuringStress";
        string GateMeasLastStressEventName = "GateMeasLastStress";
        string GateStressRangeChangeName = "GateRangeChangeStress";
        string GateMeasRangeChangeName = "GateRangeChangeMeas";


        string DrainInitialStressName = "DrainPatternStressInit";
        string DrainStressNameHeader = "DrainPatternStress";
        string DrainReferenceMeasName = "DrainPatternRefMeas";
        string DrainReferenceMeasEventName = "DrainMeasRefMeas";
        string DrainPostMeasName = "DrainPatternPostMeas";
        string DrainPostMeasEventName = "DrainMeasPostMeas";
        string DrainPostMeasInteravalName = "DrainPatternPostInterval";
        string DrainMeasInStressName = "DrainPatternMeasDuringStress";
        string DrainMeasLastStressName = "DrainPatternMeasLastStress";
        string DrainMeasInStressEventName = "DrainMeasDuringStress";
        string DrainMeasLastStressEventName = "DrainMeasLastStress";
        string DrainStressRangeChangeName = "DrainRangeChangeStress";
        string DrainMeasRangeChangeName = "DrainRangeChangeMeas";


        #endregion

        #region Gate channel setup

        int GateMeasEnable;
        int GateChId;
        int GateChOperationMode;
        int GateChMeasMode;
        int GateChForceRange;
        int GateChIMeasRange;
        int GateChVMeasRange;
        double GateChHwSkew;

        #endregion

        #region Drain channel setup

        int DrainMeasEnable;
        int DrainChId;
        int DrainChOperationMode;
        int DrainChMeasMode;
        int DrainChForceRange;
        int DrainChIMeasRange;
        int DrainChVMeasRange;
        double DrainChHwSkew;

        #endregion

        #region Stress

        int VgStressType;
        double StressVg;
        double StressVgBase;
        double StressVgPulseFreq;
        double StressVgPulsePeriod;
        double StressVgPulseDuty;
        double StressVgPulseWidth;
        double StressVgPulseRiseTime;
        double StressVgPulseFallTime;
        double StressVgPulseDelay;
        int StressVgCurrentRange;
        double StressVgRangeChangeHold;

        int VdStressType;
        double StressVd;
        double StressVdBase;
        double StressVdPulseDuty;
        double StressVdPulseWidth;
        double StressVdPulseRiseTime;
        double StressVdPulseFallTime;
        double StressVdPulseDelay;
        int StressVdCurrentRange;
        double StressVdRangeChangeHold;

        double[] AccStressTime;

        #endregion

        #region Sequence

        int EnableReferenceMeas;
        double StressDelayReferenceMeas;
        int EnableSamplingMeas;
        int EnableSweepMeas;
        int EnableOTFMeas;
        int EnablePostMeas;
        double PostMeasDelay;
        double MeasIterationInterval;
        int MeasIntervalMode;
        int MeasIterationNumber;
        double StressToMeasEdge;
        double VdSequenceDelay;

        #endregion

        #region Sampling Measurement

        double SamplingVg;
        double SamplngVd;
        int SamplingMode;
        double SamplingHold;
        double SamplingMeasDelay;
        double SamplingMeasInterval;
        int SamplingMeasPoints;
        double SamplingMeasAveragingTime;
        double SamplingExtraDelay;

        #endregion

        #region Sweep Measurement

        int SweepEnableDualSweep;
        int SweepType;
        double SweepVd;
        double SweepVgStart;
        double SweepVgStop;
        int SweepStepNum;
        double SweepStepRise;
        double SweepHold;
        double SweepMeasDelay;
        double SweepAveragingTime;
        double SweepStepDelay;
        double SweepDelayDualSweep;

        double SweepVgPulseBase;
        double SweepPulsePeriod;
        double SweepVgPulseWidth;
        double SweepVgPulseRiseTime;
        double SweepVgPulseFallTime;
        double SweepVgPulseDelay;
        int SweepEnableVdPulse;
        double SweepVdPulseBase;
        double SweepVdPulseWidth;
        double SweepVdPulseRiseTime;
        double SweepVdPulseFallTime;
        double SweepVdPulseDelay;

        #endregion

        #region OTF

        double OTFdVg;
        double OTFVd;
        double OTFEdge;
        double OTFMeasDelay;
        double OTFMeasAveragingTime;
        double OTFStepDelay;

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

        public NBTI_Form()
        {
            InitializeComponent();
        }

        #region User defined functions

        #region Set paramters
        private void SetParameters()
        {
            int i;

            #region Gate channel setup

            GateChId = int.Parse(this.comboBoxGateCh.Text);

            if (this.checkBoxGateMeas.Checked == true)
            {
                GateMeasEnable = 1;
            }
            else
            {
                GateMeasEnable = 0;
            }

            switch (this.comboBoxGateChOperationMode.SelectedIndex)
            {
                case 0:
                    GateChOperationMode =  WGFMU.OPERATION_MODE_FASTIV; // Set Fast IV mode
                    break;
                case 1:
                    GateChOperationMode = WGFMU.OPERATION_MODE_PG; // Set PG mode
                    break;
            }

            switch (this.comboBoxGateChMeasMode.SelectedIndex)
            {
                case 0:
                    GateChMeasMode = WGFMU.MEASURE_MODE_VOLTAGE; // Set voltage measurement mode
                    break;
                case 1:
                    GateChMeasMode = WGFMU.MEASURE_MODE_CURRENT; // Set current measurement mode
                    break;
            }

            switch (this.comboBoxGateChForceRange.SelectedIndex)
            {
                case 0:
                    GateChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_AUTO;
                    break;
                case 1:
                    GateChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_3V;
                    break;
                case 2:
                    GateChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_5V;
                    break;
                case 3:
                    GateChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_10V_NEGATIVE;
                    break;
                case 4:
                    GateChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_10V_POSITIVE;
                    break;
            }


            switch (this.comboBoxGateChIMeasRange.SelectedIndex)
            {
                case 0:
                    GateChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1UA;
                    break;
                case 1:
                    GateChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10UA;
                    break;
                case 2:
                    GateChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_100UA;
                    break;
                case 3:
                    GateChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1MA;
                    break;
                case 4:
                    GateChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10MA;
                    break;
            }

            switch (this.comboBoxGateChVMeasRange.SelectedIndex)
            {
                case 0:
                    GateChVMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_5V;
                    break;
                case 1:
                    GateChVMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_10V;
                    break;
            }

            GateChHwSkew = double.Parse(this.textBoxGateChHwSkew.Text);

            #endregion

            #region Drain channel setup

            DrainChId = int.Parse(this.comboBoxDrainCh.Text);


            if (this.checkBoxDrainMeas.Checked == true)
            {
                DrainMeasEnable = 1;
            }
            else
            {
                DrainMeasEnable = 0;
            }

            switch (this.comboBoxDrainChOperationMode.SelectedIndex)
            {
                case 0:
                    DrainChOperationMode = WGFMU.OPERATION_MODE_FASTIV; // Set Fast IV mode
                    break;
                case 1:
                    DrainChOperationMode = WGFMU.OPERATION_MODE_PG; // Set PG mode
                    break;

            }

            switch (this.comboBoxDrainChMeasMode.SelectedIndex)
            {
                case 0:
                    DrainChMeasMode = WGFMU.MEASURE_MODE_VOLTAGE; // Set voltage measurement mode
                    break;
                case 1:
                    DrainChMeasMode = WGFMU.MEASURE_MODE_CURRENT; // Set current measurement mode
                    break;
            }

            switch (this.comboBoxDrainChForceRange.SelectedIndex)
            {
                case 0:
                    DrainChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_AUTO;
                    break;
                case 1:
                    DrainChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_3V;
                    break;
                case 2:
                    DrainChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_5V;
                    break;
                case 3:
                    DrainChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_10V_NEGATIVE;
                    break;
                case 4:
                    DrainChForceRange = WGFMU.FORCE_VOLTAGE_RANGE_10V_POSITIVE;
                    break;
            }

            switch (this.comboBoxDrainChIMeasRange.SelectedIndex)
            {
                case 0:
                    DrainChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1UA;
                    break;
                case 1:
                    DrainChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10UA;
                    break;
                case 2:
                    DrainChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_100UA;
                    break;
                case 3:
                    DrainChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_1MA;
                    break;
                case 4:
                    DrainChIMeasRange = WGFMU.MEASURE_CURRENT_RANGE_10MA;
                    break;
            }

            switch (this.comboBoxDrainChVMeasRange.SelectedIndex)
            {
                case 0:
                    DrainChVMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_5V;
                    break;
                case 1:
                    DrainChVMeasRange = WGFMU.MEASURE_VOLTAGE_RANGE_10V;
                    break;
            }

            DrainChHwSkew = double.Parse(this.textBoxDrainChHwSkew.Text);

            #endregion

            #region Stress

            if (this.radioButtonDCStress.Checked == true)
            {
                VgStressType = 0;        // DC stress
            }
            else
            {
                VgStressType = 1;       // AC Stress
            }

            StressVg = double.Parse(this.textBoxStressVg.Text);
            StressVgBase = double.Parse(this.textBoxStressVgBase.Text);
            switch (this.comboBoxStressPulseFreq.SelectedIndex)
            {
                case 0:
                    StressVgPulseFreq = 100;
                    break;
                case 1:
                    StressVgPulseFreq = 1E+3;
                    break;
                case 2:
                    StressVgPulseFreq = 1E+4;
                    break;
                case 3:
                    StressVgPulseFreq = 1E+5;
                    break;
                case 4:
                    StressVgPulseFreq = 1E+6;
                    break;
                case 5:
                    StressVgPulseFreq = 2E+6;
                    break;
                case 6:
                    StressVgPulseFreq = 5E+6;
                    break;
            }
            StressVgPulsePeriod = 1.0 / StressVgPulseFreq;
            StressVgPulseDuty = double.Parse(this.textBoxStressVgPulseDuty.Text) / 100.0;
            StressVgPulseWidth = StressVgPulsePeriod * StressVgPulseDuty;
            StressVgPulseRiseTime = double.Parse(this.textBoxStressVgPulseLeadingEdge.Text);
            StressVgPulseFallTime = double.Parse(this.textBoxStressVgPulseLeadingEdge.Text);
            StressVgPulseDelay = double.Parse(this.textBoxStressVgPulseDelay.Text);

            switch (this.comboBoxVgStressCurrentRange.SelectedIndex)
            {
                case 0:
                    StressVgCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_1UA;
                    break;
                case 1:
                    StressVgCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_10UA;
                    break;
                case 2:
                    StressVgCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_100UA;
                    break;
                case 3:
                    StressVgCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_1MA;
                    break;
                case 4:
                    StressVgCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_10MA;
                    break;
            }

            StressVgRangeChangeHold = double.Parse(this.textBoxVgRangeChangeHold.Text);


            if (this.checkBoxVdPulseStress.Checked != true)
            {
                VdStressType = 0;   // DC stress
            }
            else
            {
                VdStressType = 1; // AC Stress
            }

            StressVd = double.Parse(textBoxStressVd.Text);
            StressVdBase = double.Parse(this.textBoxStressVdPulseBase.Text);
            StressVdPulseDuty = double.Parse(this.textBoxStressVdPulseDuty.Text) / 100;
            StressVdPulseWidth = StressVgPulsePeriod * StressVdPulseDuty;
            StressVdPulseRiseTime = double.Parse(this.textBoxStressVdPulseLeadingEdge.Text);
            StressVdPulseFallTime = double.Parse(this.textBoxStressVdPulseLeadingEdge.Text);
            StressVdPulseDelay = double.Parse(this.textBoxStressVdPulseDelay.Text);

            switch (this.comboBoxVdStressCurrentRange.SelectedIndex)
            {
                case 0:
                    StressVdCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_1UA;
                    break;
                case 1:
                    StressVdCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_10UA;
                    break;
                case 2:
                    StressVdCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_100UA;
                    break;
                case 3:
                    StressVdCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_1MA;
                    break;
                case 4:
                    StressVdCurrentRange = WGFMU.MEASURE_CURRENT_RANGE_10MA;
                    break;
            }

            StressVdRangeChangeHold = double.Parse(this.textBoxVdRangeChangeHold.Text);

            AccStressTime = new double[dataViewStressList.RowCount];

            for (i = 0; i < dataViewStressList.RowCount; i++)
            {
                if (dataViewStressList[1, i].Value != null)
                {
                    AccStressTime[i] = double.Parse(this.dataViewStressList[1, i].Value.ToString());
                }
                else
                {
                    AccStressTime[i] = 0.0;
                }
            }
            #endregion

            #region Sequence

            if (this.checkBoxReferenceMeas.Checked == true)
            {
                EnableReferenceMeas = 1;
            }
            else
            {
                EnableReferenceMeas = 0;
            }

            StressDelayReferenceMeas = double.Parse(this.textBoxStressDelayReferenceMeas.Text);
            if (this.radioButtonSampling.Checked == true)
            {
                EnableSamplingMeas = 1;
            }
            else
            {
                EnableSamplingMeas = 0;
            }

            if (this.radioButtonSweep.Checked == true)
            {
                EnableSweepMeas = 1;
            }
            else
            {
                EnableSweepMeas = 0;
            }

            if (this.radioButtonOTF.Checked == true)
            {
                EnableOTFMeas = 1;
            }
            else
            {
                EnableOTFMeas = 0;
            }

            if (this.checkBoxEnablePostMeas.Checked == true)
            {
                EnablePostMeas = 1;
            }
            else
            {
                EnablePostMeas = 0;
            }

            PostMeasDelay = double.Parse(this.textBoxPostMeasDelay.Text);
            MeasIterationInterval = double.Parse(this.textBoxIterativeMeasInterval.Text);
            MeasIntervalMode = this.comboBoxMeasIntervalMode.SelectedIndex;
            MeasIterationNumber = int.Parse(this.textBoxMeasIterationNumber.Text); ;
            StressToMeasEdge = double.Parse(this.textBoxStressToMeasEdge.Text);
            VdSequenceDelay = double.Parse(this.textBoxVdSequenceDelay.Text); ;

            #endregion

            #region Sampling Measurement

            SamplingVg = double.Parse(this.textBoxSamplingVg.Text);
            SamplngVd = double.Parse(this.textBoxSamplingVd.Text);
            SamplingMode = this.comboBoxSamplingMode.SelectedIndex;
            SamplingHold = double.Parse(this.textBoxSamplingHold.Text);
            SamplingMeasDelay = double.Parse(this.textBoxSamplingMeasDelay.Text);
            SamplingMeasInterval = double.Parse(this.textBoxSamplingMeasInterval.Text);
            SamplingMeasPoints = int.Parse(this.textBoxSampingMeasPoints.Text);
            SamplingMeasAveragingTime = double.Parse(this.textBoxSamplingMeasAveragingTime.Text);
            SamplingExtraDelay = double.Parse(this.textBoxSamplingExtraDelay.Text);

            #endregion

            #region Sweep Measurement

            if (this.checkBoxDualSweep.Checked == true)
            {
                SweepEnableDualSweep = 1;
            }
            else
            {
                SweepEnableDualSweep = 0;
            }

            SweepType = this.comboBoxSweepType.SelectedIndex;

            SweepVd = double.Parse(this.textBoxSweepVd.Text);
            SweepVgStart = double.Parse(this.textBoxSweepVgStart.Text);
            SweepVgStop = double.Parse(this.textBoxSweepVgStop.Text);
            SweepStepNum = int.Parse(this.textBoxSweepStepNum.Text);
            SweepStepRise = double.Parse(this.textBoxSweepStepRise.Text);
            SweepHold = double.Parse(this.textBoxSweepHold.Text);
            SweepMeasDelay = double.Parse(this.textBoxSweepDelay.Text);
            SweepAveragingTime = double.Parse(this.textBoxSweepAveragingTime.Text);
            SweepStepDelay = double.Parse(this.textBoxSweepStepDelay.Text);
            SweepDelayDualSweep = double.Parse(this.textBoxSweepDelayDualSweep.Text);

            SweepVgPulseBase = double.Parse(this.textBoxSweepVgPulseBase.Text);
            SweepPulsePeriod = double.Parse(this.textBoxSweepPulsePeriod.Text);
            SweepVgPulseWidth = double.Parse(this.textBoxSweepVgPulseWidth.Text);
            SweepVgPulseRiseTime = double.Parse(this.textBoxSweepVgPulseLeadingEdge.Text);
            SweepVgPulseFallTime = SweepVgPulseRiseTime;
            SweepVgPulseDelay = double.Parse(this.textBoxSweepVgPulseDelay.Text);

            if (this.checkBoxSweepVdPulse.Checked == true)
            {
                SweepEnableVdPulse = 1;
            }
            else
            {
                SweepEnableVdPulse = 0;
            }

            SweepVdPulseBase = double.Parse(this.textBoxSweepVdPulseBase.Text);
            SweepVdPulseWidth = double.Parse(this.textBoxSweepVdPulseWidth.Text);
            SweepVdPulseRiseTime = double.Parse(this.textBoxSweepVdPulseLeadingEdge.Text);
            SweepVdPulseFallTime = SweepVdPulseRiseTime;
            SweepVdPulseDelay = double.Parse(this.textBoxSweepVdPulseLeadingEdge.Text);

            #endregion

            #region OTF

            OTFdVg = double.Parse(this.textBoxOTFdVg.Text);
            OTFVd = double.Parse(this.textBoxOTFVd.Text);
            OTFEdge = double.Parse(this.textBoxOTFEdge.Text);
            OTFMeasDelay = double.Parse(this.textBoxOTFMeasDelay.Text);
            OTFMeasAveragingTime = double.Parse(this.textBoxOTFMeasAveragingTime.Text);
            OTFStepDelay = double.Parse(this.textBoxOTFStepDelay.Text);
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

        #region Creating measurement pattern

        #region CreateSamplingMeasurement

        private void CreateSamplingMeasurement(
            string GatePatternName,
            string GateMeasname,
            double GateInitV,
            double GateInitHold,
            double GateInitToMeasTransient,
            double GateTermV,
            double GateTermHold,
            double GateMeasToTermTransient,
            string DrainPatternName,
            string DrainMeasName,
            double DrainInitV,
            double DrainInitHold,
            double DrainInitToMeasTransient,
            double DrainTermV,
            double DrainTermHold,
            double DrainMeasToTermTransient)
        {

            int SourceType;
            double InitV;
            double InitHold;
            double InitToBaseEdge;
            double BaseV;
            double BaseHold;
            double BaseToSourceEdge;
            double SourceV;
            double TermV;
            double SourceToTermEdge;
            double TermHold;
            double MeasurementDelay;
            double InitialInterval;
            int SamplingPoints;
            double AveragingTime;
            double TerminationDelay;
            double OutputSequenceDelay;
            int SecondMeasEnable;
            int SecondMeasRange;
            int SecondSamplingMode;
            double SecondMeasOrigin;
            double SecondMeasDelay;
            double SecondInitialInterval;
            int  SecondSamplingPoints;
            double SecondAveragingTime;
            int RawData;
            double MeasOrigin = 0;

            #region Create pattern

            // Dummy values for the secondary measurement
            SecondMeasEnable        = 0;
            SecondMeasRange         = 0;
            SecondSamplingMode      = 0;
            SecondMeasOrigin        = 0;
            SecondMeasDelay         = 0;
            SecondInitialInterval = 10E-9;
            SecondSamplingPoints    = 1;
            SecondAveragingTime     = 10E-9;
            RawData = WGFMU.MEASURE_EVENT_DATA_AVERAGED;

            #region Create gate pattern

            SourceType = 0;
            InitV   = GateInitV;
            InitHold = GateInitHold;
            InitToBaseEdge  = 0;
            BaseV = GateInitV;
            BaseHold = SamplingHold;
            BaseToSourceEdge = GateInitToMeasTransient;
            SourceV = SamplingVg;
            TermV = GateTermV;
            SourceToTermEdge = GateMeasToTermTransient;
            TermHold = GateTermHold;
            MeasurementDelay = SamplingMeasDelay;
            InitialInterval = SamplingMeasInterval;
            SamplingPoints = SamplingMeasPoints;
            AveragingTime   = SamplingMeasAveragingTime;
            TerminationDelay = SamplingExtraDelay;
            OutputSequenceDelay = VdSequenceDelay;

            wglib.CreateSamplingMeasurement(
                SourceType,
                GateMeasEnable,
                GateChMeasMode,
                GatePatternName,
                GateMeasname,
                InitV,
                InitHold,
                InitToBaseEdge,
                BaseV,
                BaseHold,
                BaseToSourceEdge,
                SourceV,
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
                SecondMeasRange,
                SecondSamplingMode,
                SecondMeasOrigin,
                SecondMeasDelay,
                SecondInitialInterval,
                SecondSamplingPoints,
                SecondAveragingTime,
                RawData,
                ref MeasOrigin);
            #endregion

            #region Create drain pattern

            SourceType = 1;
            InitV   = DrainInitV;
            InitHold = DrainInitHold;
            InitToBaseEdge  = 0;
            BaseV = DrainInitV;
            BaseHold = SamplingHold;
            BaseToSourceEdge = DrainInitToMeasTransient;
            SourceV = SamplngVd;
            TermV = DrainTermV;
            SourceToTermEdge = DrainMeasToTermTransient;
            TermHold = DrainTermHold;

            wglib.CreateSamplingMeasurement(
                SourceType,
                DrainMeasEnable,
                DrainChMeasMode,
                DrainPatternName,
                DrainMeasName,
                InitV,
                InitHold,
                InitToBaseEdge,
                BaseV,
                BaseHold,
                BaseToSourceEdge,
                SourceV,
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
                SecondMeasRange,
                SecondSamplingMode,
                SecondMeasOrigin,
                SecondMeasDelay,
                InitialInterval,
                SecondSamplingPoints,
                SecondAveragingTime,
                RawData,
                ref MeasOrigin);  
            #endregion

            #endregion
        
        }

        #endregion

        #region CreateSatireCaseSweepMeasruement

        private void CreateSweepMeasurement(
            string GatePatternName,
            string GateMeasName,
            double GateInitV,
            double GateInitHold,
            double GateInitToMeasTransient,
            double GateTermV,
            double GateTermHold,
            double GateMeasToTermTransient,
            string DrainPatternName,
            string DrainMeasName,
            double DrainInitV,
            double DrainInitHold,
            double DrainInitToMeasTransient,
            double DrainTermV,
            double DrainTermHold,
            double DrainMeasToTermTransient)
        {
            #region Parameters for stairecase , ramp and common.

            double OutputSequenceDelay;
            double InitV;
            double InitHold;
            double InitToStartTransient;
            double Hold;
            double StartV;
            double StopV;
            int StepNum;
            double StepEdge;
            double MeasDelay;
            double AveragingTime;
            double StepDelay;
            double TerminationDelay;
            double TermV;
            double StopToTermTransient;
            double TermHold;
            int EnableDualSweep;
            double DualSweepDelay;
            int Rawdata;
            double MeasOrigin;
            int ConstPulseEnable;
            double BaseToSourceTransient;
            double InitToBaseTransient;
            double BaseToTermTransient;
            double BaseV;




            #endregion

            #region parameters for pulse sweep;


            #endregion

            #region set common parameter values
            OutputSequenceDelay = VdSequenceDelay;
            InitV = GateInitV;
            InitHold = GateInitHold;
            InitToStartTransient = GateInitToMeasTransient;
            Hold = SweepHold;
            StartV = SweepVgStart;
            StopV = SweepVgStop;
            StepNum = SweepStepNum;
            StepEdge = SweepStepRise;
            MeasDelay = SweepMeasDelay;
            AveragingTime = SweepAveragingTime; ;
            StepDelay = SweepStepDelay;
            TerminationDelay = 0.0;
            TermV = GateTermV;
            StopToTermTransient = GateMeasToTermTransient;
            TermHold = GateTermHold;
            EnableDualSweep = SweepEnableDualSweep;
            DualSweepDelay = SweepDelayDualSweep;
            Rawdata = WGFMU.MEASURE_EVENT_DATA_AVERAGED;
            MeasOrigin = 0;

            ConstPulseEnable = SweepEnableVdPulse;

            #endregion

            #region Create sweep source pattern

            MeasOrigin = 0.0;
            if (SweepType == 0)
            {
                wglib.CreateStaircaseSweepSource(
                    GateMeasEnable,
                    GatePatternName,
                    GateMeasName,
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
                    Rawdata,
                    ref MeasOrigin);
            }
            else if (SweepType == 1)
            {
                wglib.CreateRampSweepSource(
                    GateMeasEnable,
                    GatePatternName,
                    GateMeasName,
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
                    Rawdata,
                    ref MeasOrigin);

            }
            else 
            {
                InitToBaseTransient = GateInitToMeasTransient;
                BaseToTermTransient = GateMeasToTermTransient;

                wglib.CreatePulseSweepSource(
                    GateMeasEnable,
                    GatePatternName,
                    GateMeasName,
                    OutputSequenceDelay,
                    InitV,
                    InitHold,
                    InitToBaseTransient,
                    Hold,
                    SweepVgPulseBase,
                    StartV,
                    StopV,
                    StepNum,
                    SweepPulsePeriod,
                    SweepVgPulseWidth,
                    SweepVgPulseRiseTime,
                    SweepVgPulseFallTime,
                    SweepVgPulseDelay,
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
            InitV = DrainInitV;
            InitHold = DrainInitHold;
            InitToBaseTransient = DrainInitToMeasTransient;
            TermV = DrainTermV;
            StopToTermTransient = DrainMeasToTermTransient;
            TermHold = DrainTermHold;

            if (SweepType == 0 || SweepType == 1)
            {
                InitToBaseTransient = 0.0;
                BaseToSourceTransient = DrainInitToMeasTransient;

                BaseV = InitV;
                wglib.CreateStaircaseConstantSource(
                    DrainMeasEnable,
                    DrainPatternName,
                    DrainMeasName,
                    OutputSequenceDelay,
                    InitV,
                    InitHold,
                    InitToBaseTransient,
                    BaseV,
                    BaseToSourceTransient,
                    Hold,
                    SweepVd,
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
                    Rawdata);
            }
            else
            {
                InitToBaseTransient = DrainInitToMeasTransient;
                BaseToTermTransient = DrainMeasToTermTransient;

                wglib.CreatePulseConstantSource(
                    DrainMeasEnable,
                    DrainPatternName,
                    DrainMeasName,
                    ConstPulseEnable,
                    OutputSequenceDelay,
                    InitV,
                    InitHold,
                    InitToBaseTransient,
                    Hold,
                    SweepVdPulseBase,
                    SweepVd,
                    StepNum,
                    SweepPulsePeriod,
                    SweepVdPulseWidth,
                    SweepVdPulseRiseTime,
                    SweepVdPulseFallTime,
                    SweepVdPulseDelay,
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

        }


        #endregion

        #region CreateOTFMeasurement

        private void CreateOTFMeasurement(
            string GatePatternName,
            string GateMeasname,
            double GateInitV,
            double GateInitHold,
            double GateInitToMeasTransient,
            double GateTermV,
            double GateTermHold,
            double GateMeasToTermTransient,
            string DrainPatternName,
            string DrainMeasName,
            double DrainInitV,
            double DrainInitHold,
            double DrainInitToMeasTransient,
            double DrainTermV,
            double DrainTermHold,
            double DrainMeasToTermTransient)
        {
            int SourceType;

            double MeasOrigin = 0;


            SourceType = 0;

            wglib.CreateOTFMeasurement(
                GateMeasEnable,
                GatePatternName,
                GateMeasname,
                SourceType,
                VdSequenceDelay,
                GateInitV,
                GateInitHold,
                GateInitToMeasTransient,
                StressVg,
                OTFdVg,
                OTFEdge,
                OTFMeasDelay,
                OTFMeasAveragingTime,
                OTFStepDelay,
                GateTermV,
                GateMeasToTermTransient,
                GateTermHold,
                ref MeasOrigin);

            SourceType = 1;

            wglib.CreateOTFMeasurement(
                DrainMeasEnable,
                DrainPatternName,
                DrainMeasName,
                SourceType,
                VdSequenceDelay,
                DrainInitV,
                DrainInitHold,
                DrainInitToMeasTransient,
                StressVd,
                OTFVd,
                OTFEdge,
                OTFMeasDelay,
                OTFMeasAveragingTime,
                OTFStepDelay,
                DrainTermV,
                DrainMeasToTermTransient,
                DrainTermHold,
                ref MeasOrigin);
        
        }

        #endregion

        #region CreatePostMeasInterval

        private void CreatePostMeasInterval()
        {
            int i;
            double LogBase = 10.0;
            double CountResolution;
            double NumInDecade;
            double PowerFactor;
            double Duration;
            double MeasPatternDuration;
            string GatePatternName;
            string DrainPatternName;
            double MinimumInterval = 0.0;

            wglib.GetPatternDuration(GatePostMeasName, out MeasPatternDuration);

            for (i = 0; i < MeasIterationNumber; i++)
            {
                if (MeasIntervalMode == 0)
                {
                    MinimumInterval = MeasIterationInterval - 100E-9;

                    if (i == 0)
                    {
                        Duration = PostMeasDelay;
                    }
                    else
                    {
                        Duration = MeasIterationInterval - MeasPatternDuration - 50E-9 * 2;;
                    }
                }
                else
                {
                    if (MeasIntervalMode == 1)
                    {
                        NumInDecade = 10;
                        CountResolution = 1.0 / NumInDecade;
                    }
                    else
                    {
                        NumInDecade = 25;
                        CountResolution = 1.0 / NumInDecade;
                    }

                    PowerFactor = CountResolution * i;
                    MinimumInterval = MeasIterationInterval * (Math.Pow(LogBase, CountResolution) - 1);
                    if (i == 0)
                    {
                        Duration = PostMeasDelay;
                    }
                    else
                    {
                        Duration = MeasIterationInterval * (Math.Pow(LogBase, CountResolution * i) - Math.Pow(LogBase, CountResolution * (i - 1)));
                        Duration = Duration - MeasPatternDuration - 50E-9 * 2;
                    }
                     
                }

                if (Duration < 0)
                {
                    throw (new WGFMULibException(WGFMULib.WGFMU_SAMPLE_LIB_ERROR, "Minimum interval: " + wglib.DoubleToTrimedString(MinimumInterval) + " (s) Initial interval x (10^0.1 - 1) for LOG10 mode or Initial interval x (10^0.04 -1) for Log25 mode,  minus 100 ns of the post measurement must be longer than the duration of the measurement + 100 ns = " + wglib.DoubleToTrimedString(MeasPatternDuration) + " s"));

                }

                GatePatternName = GatePostMeasInteravalName + i.ToString();
                wglib.CreateDcWaveForm(GatePatternName, 0.0, Duration);

                DrainPatternName = DrainPostMeasInteravalName + i.ToString();
                wglib.CreateDcWaveForm(DrainPatternName, 0.0, Duration);

            }


        }

        #endregion

        #region  CreateStressPattern

        private void CreateStressPattern(ref double [] LoopCount)
        {
            int i;
            string GatePatternName;
            string DrainPatternName;

            double InitV;
            double InitHold;
            double InitToBaseTransient;
            double BaseV;
            double BaseHold;
            double BasetoSoruceTransient;
            double SourceV;
            double SourceDuration;
            double SouceToTermTransient;
            double TermV;
            double TermHold;

            double PulseBase;
            double PulseTop;
            double PulsePeriod;
            double PulseWidth;
            double PulseRiseTime;
            double PulseFallTime;
            double PulseDelay;


            #region Create Initial Stress

            #region Gate stress
            
            GatePatternName =GateInitialStressName;
            InitV = 0.0;
            InitHold = StressVgRangeChangeHold;
            InitToBaseTransient = 0.0;
            BaseV = 0.0;
            BaseHold = StressDelayReferenceMeas;
            SourceV = StressVg;
            SouceToTermTransient = 0.0;
            TermV = StressVg;
            TermHold = 0.0;

            if (VgStressType == 0)
            {
                BasetoSoruceTransient = StressToMeasEdge;
                SourceDuration = 0.0;

            }
            else
            {
                BasetoSoruceTransient = StressVgPulseRiseTime;
                SourceDuration = StressVgPulseWidth - StressVgPulseRiseTime - StressVgPulseDelay;
            }

            wglib.CreateStepWaveform(
                GatePatternName,
                InitV,
                InitHold,
                InitToBaseTransient,
                BaseV,
                BaseHold,
                BasetoSoruceTransient,
                SourceV,
                SourceDuration,
                SouceToTermTransient,
                TermV,
                TermHold);

            if (GateChIMeasRange != StressVgCurrentRange)
            {
                wglib.SetMeasRangeEvent(
                    GatePatternName,
                    GateStressRangeChangeName,
                    0.0,
                    StressVgCurrentRange);
            }

            #endregion

            #region Drain Stress
            // To make length of each pattern same, the length of drains tress is determined by the gate stress.
            // (Gate stress is master)
            // It makes the length of beginning of stress different between Vg and Vd if duty ration is different.

            DrainPatternName = DrainInitialStressName;
            InitV = 0.0;
            InitHold = StressVdRangeChangeHold;
            InitToBaseTransient = 0.0;
            BaseV = 0.0;
            BaseHold = StressDelayReferenceMeas;
            SourceV = StressVd;
            SouceToTermTransient = 0.0;
            TermV = StressVd;
            TermHold = 0.0;

            wglib.CreateStepWaveform(
                DrainPatternName,
                InitV,
                InitHold,
                InitToBaseTransient,
                BaseV,
                BaseHold,
                BasetoSoruceTransient,
                SourceV,
                SourceDuration,
                SouceToTermTransient,
                TermV,
                TermHold);
            if (DrainChIMeasRange != StressVdCurrentRange)
            {
                wglib.SetMeasRangeEvent(
                    DrainPatternName,
                    DrainStressRangeChangeName,
                    0.0,
                    StressVdCurrentRange);
            }
            #endregion

            #endregion

            #region Create stress pattern
            i = 0;
            while (AccStressTime[i] != 0)
            {
                GatePatternName = GateStressNameHeader + i.ToString();
                DrainPatternName = DrainStressNameHeader + i.ToString();

                if (i == 0)
                {
                    SourceDuration = AccStressTime[i];
                }
                else
                {
                    SourceDuration = AccStressTime[i] - AccStressTime[i-1];
                }

                if (VgStressType == 0)
                {
                    GatePatternName = GateStressNameHeader + i.ToString();
                    wglib.CreateDcWaveForm(GatePatternName, StressVg, SourceDuration);

                    DrainPatternName = DrainStressNameHeader + i.ToString();
                    wglib.CreateDcWaveForm(DrainPatternName, StressVd, SourceDuration);

                    #region Set current range for the stress phase
                    if (GateChIMeasRange != StressVgCurrentRange)
                    {
                        wglib.SetMeasRangeEvent(
                            GatePatternName,
                            GateStressRangeChangeName,
                            0.0,
                            StressVgCurrentRange);
                    }

                    if (DrainChIMeasRange != StressVdCurrentRange)
                    {
                        wglib.SetMeasRangeEvent(
                            DrainPatternName,
                            DrainStressRangeChangeName,
                            0.0,
                            StressVdCurrentRange);
                    }
                    #endregion
                    
                    LoopCount[i] = 1;

                }
                else
                {
                    if (i == 0)
                    {
                        GatePatternName = GateStressNameHeader + i.ToString();
                        PulseBase = StressVg;
                        PulseTop = StressVgBase;
                        PulsePeriod = StressVgPulsePeriod;
                        PulseRiseTime = StressVgPulseFallTime;
                        PulseFallTime = StressVgPulseRiseTime;
                        PulseWidth = StressVgPulsePeriod - StressVgPulseWidth;
                        PulseDelay = StressVgPulseDelay;

                        wglib.CreateSinglePulseWaveform(
                            GatePatternName,
                            PulseBase,
                            PulseTop,
                            PulsePeriod,
                            PulseDelay,
                            PulseWidth,
                            PulseRiseTime,
                            PulseFallTime);

                            DrainPatternName = DrainStressNameHeader + i.ToString();
                            PulseBase = StressVd;
                            PulseTop = StressVdBase;
                            PulsePeriod = StressVgPulsePeriod;
                            PulseRiseTime = StressVdPulseFallTime;
                            PulseFallTime = StressVdPulseRiseTime;
                            PulseWidth = PulsePeriod - StressVdPulseWidth;
                            PulseDelay = StressVdPulseDelay;

                            if (VdStressType == 0)
                            {
                                wglib.CreateDcWaveForm(DrainPatternName, StressVd, StressVgPulsePeriod);
                            }
                            else
                            {
                                wglib.CreateSinglePulseWaveform(
                                    DrainPatternName,
                                    PulseBase,
                                    PulseTop,
                                    PulsePeriod,
                                    PulseDelay,
                                    PulseWidth,
                                    PulseRiseTime,
                                    PulseFallTime);

                            }

                            #region Set current range for the stress phase
                            if (GateChIMeasRange != StressVgCurrentRange)
                            {
                                wglib.SetMeasRangeEvent(
                                    GatePatternName,
                                    GateStressRangeChangeName,
                                    0.0,
                                    StressVgCurrentRange);
                            }

                            if (DrainChIMeasRange != StressVdCurrentRange)
                            {
                                wglib.SetMeasRangeEvent(
                                    DrainPatternName,
                                    DrainStressRangeChangeName,
                                    0.0,
                                    StressVdCurrentRange);
                            }
                            #endregion

                    }

                    LoopCount[i] =  Math.Round(SourceDuration / StressVgPulsePeriod);

                }

                i = i + 1;
            }
            #endregion

        }

        #endregion

        #region CreatePattern

        private void CreatePattern()
        {
            int i = 0;
            string GatePatternName;
            string GateMeasName;
            string DrainPatternName;
            string DrainMeasName;
            int StressCount;
            int LoopCount;
            double [] StressLoopCount = new double[AccStressTime.Length];
            double[] PostMeasInterval = new double[MeasIterationNumber];

            double GateInitV;
            double GateInitHold;
            double GateInitToMeasTransient;

            double GateTermV;
            double GateTermHold;
            double GateMeastoTermTransient;

            double DrainInitV;
            double DrainInitHold;
            double DrainInitToMeasTransient;

            double DrainTermV;
            double DrainTermHold;
            double DrainMeastoTermTransient;

            StressCount = 0;
            while (AccStressTime[StressCount] != 0)
            {
                StressCount = StressCount + 1;
            }

            #region Create measurement patterns

            #region Create reference measurement pattern

            GatePatternName = GateReferenceMeasName;
            GateMeasName = GateReferenceMeasEventName;
            GateInitV = 0.0;
            GateInitHold = 0.0;
            GateInitToMeasTransient = StressToMeasEdge;
            GateTermV = 0.0;
            GateTermHold = 0.0;
            GateMeastoTermTransient = StressToMeasEdge;

            DrainPatternName = DrainReferenceMeasName;
            DrainMeasName = DrainReferenceMeasEventName;
            DrainInitV = 0.0;
            DrainInitHold = 0.0;
            DrainInitToMeasTransient = StressToMeasEdge;
            DrainTermV = 0.0;
            DrainTermHold = 0.0;
            DrainMeastoTermTransient = StressToMeasEdge;

            if (EnableReferenceMeas == 1)
            {
                if (EnableSamplingMeas == 1)
                {
                    this.CreateSamplingMeasurement(
                        GatePatternName,
                        GateMeasName,
                        GateInitV,
                        GateInitHold,
                        GateInitToMeasTransient,
                        GateTermV,
                        GateTermHold,
                        GateMeastoTermTransient,
                        DrainPatternName,
                        DrainMeasName,
                        DrainInitV,
                        DrainInitHold,
                        DrainInitToMeasTransient,
                        DrainTermV,
                        DrainTermHold,
                        DrainMeastoTermTransient);

                }
                else if (EnableSweepMeas == 1)
                {
                    this.CreateSweepMeasurement(
                            GatePatternName,
                            GateMeasName,
                            GateInitV,
                            GateInitHold,
                            GateInitToMeasTransient,
                            GateTermV,
                            GateTermHold,
                            GateMeastoTermTransient,
                            DrainPatternName,
                            DrainMeasName,
                            DrainInitV,
                            DrainInitHold,
                            DrainInitToMeasTransient,
                            DrainTermV,
                            DrainTermHold,
                            DrainMeastoTermTransient);
   

                }
                else if (EnableOTFMeas == 1)
                {
                    this.CreateOTFMeasurement(
                            GatePatternName,
                            GateMeasName,
                            GateInitV,
                            GateInitHold,
                            GateInitToMeasTransient,
                            GateTermV,
                            GateTermHold,
                            GateMeastoTermTransient,
                            DrainPatternName,
                            DrainMeasName,
                            DrainInitV,
                            DrainInitHold,
                            DrainInitToMeasTransient,
                            DrainTermV,
                            DrainTermHold,
                            DrainMeastoTermTransient);
                }
            }

            #endregion

            #region Create measurment during stress

            GatePatternName = GateMeasInStressName;
            GateMeasName = GateMeasInStressEventName;
            GateInitV = StressVg;
            GateInitHold = StressVgRangeChangeHold;
            GateInitToMeasTransient = StressToMeasEdge;
            GateTermV = StressVg;
            GateTermHold = 0.0;
            GateMeastoTermTransient = StressToMeasEdge;

            DrainPatternName = DrainMeasInStressName;
            DrainMeasName = DrainMeasInStressEventName;
            DrainInitV = StressVd;
            DrainInitHold = StressVdRangeChangeHold;
            DrainInitToMeasTransient = StressToMeasEdge;
            DrainTermV = StressVd;
            DrainTermHold = 0.0;
            DrainMeastoTermTransient = StressToMeasEdge;

            if (VgStressType == 1)
            {
                GateInitHold = StressVgPulseDelay + StressVgRangeChangeHold;
                DrainInitHold = GateInitHold;

                GateTermHold = StressVgPulseWidth - StressVgPulseRiseTime - StressVgPulseDelay;
                DrainTermHold = GateTermHold;
            }

            if (EnableSamplingMeas == 1)
            {
                this.CreateSamplingMeasurement(
                    GatePatternName,
                    GateMeasName,
                    GateInitV,
                    GateInitHold,
                    GateInitToMeasTransient,
                    GateTermV,
                    GateTermHold,
                    GateMeastoTermTransient,
                    DrainPatternName,
                    DrainMeasName,
                    DrainInitV,
                    DrainInitHold,
                    DrainInitToMeasTransient,
                    DrainTermV,
                    DrainTermHold,
                    DrainMeastoTermTransient);

            }
            else if (EnableSweepMeas == 1)
            {
                this.CreateSweepMeasurement(
                    GatePatternName,
                    GateMeasName,
                    GateInitV,
                    GateInitHold,
                    GateInitToMeasTransient,
                    GateTermV,
                    GateTermHold,
                    GateMeastoTermTransient,
                    DrainPatternName,
                    DrainMeasName,
                    DrainInitV,
                    DrainInitHold,
                    DrainInitToMeasTransient,
                    DrainTermV,
                    DrainTermHold,
                    DrainMeastoTermTransient);

            }
            else if (EnableOTFMeas == 1)
            {
                this.CreateOTFMeasurement(
                    GatePatternName,
                    GateMeasName,
                    GateInitV,
                    GateInitHold,
                    GateInitToMeasTransient,
                    GateTermV,
                    GateTermHold,
                    GateMeastoTermTransient,
                    DrainPatternName,
                    DrainMeasName,
                    DrainInitV,
                    DrainInitHold,
                    DrainInitToMeasTransient,
                    DrainTermV,
                    DrainTermHold,
                    DrainMeastoTermTransient);
            }

            if (GateChIMeasRange != StressVgCurrentRange)
            {
                wglib.SetMeasRangeEvent(
                    GatePatternName,
                    GateMeasRangeChangeName,
                    0.0,
                    GateChIMeasRange);
            }
            if (DrainChIMeasRange != StressVdCurrentRange)
            {
                wglib.SetMeasRangeEvent(
                    DrainPatternName,
                    DrainMeasRangeChangeName,
                    0.0,
                    DrainChIMeasRange);
            }

            #endregion

            #region Create measurement at last stress

            GatePatternName = GateMeasLastStressName;
            GateMeasName = GateMeasLastStressEventName;
            GateInitV = StressVg;
            GateInitHold = StressVgRangeChangeHold;
            GateInitToMeasTransient = StressToMeasEdge;
            GateTermV = 0.0;
            GateTermHold = 0.0;
            GateMeastoTermTransient = StressToMeasEdge;

            DrainPatternName = DrainMeasLastStressName;
            DrainMeasName = DrainMeasLastStressEventName;
            DrainInitV = StressVd;
            DrainInitHold = StressVdRangeChangeHold;
            DrainInitToMeasTransient = StressToMeasEdge;
            DrainTermV = 0.0;
            DrainTermHold = 0.0;
            DrainMeastoTermTransient = StressToMeasEdge;

            if (VgStressType == 1)
            {
                GateInitHold = StressVgPulseDelay;
                DrainInitHold = GateInitHold;
            }

            if (EnableSamplingMeas == 1)
            {
                this.CreateSamplingMeasurement(
                        GatePatternName,
                        GateMeasName,
                        GateInitV,
                        GateInitHold,
                        GateInitToMeasTransient,
                        GateTermV,
                        GateTermHold,
                        GateMeastoTermTransient,
                        DrainPatternName,
                        DrainMeasName,
                        DrainInitV,
                        DrainInitHold,
                        DrainInitToMeasTransient,
                        DrainTermV,
                        DrainTermHold,
                        DrainMeastoTermTransient);

            }
            else if (EnableSweepMeas == 1)
            {
                this.CreateSweepMeasurement(
                        GatePatternName,
                        GateMeasName,
                        GateInitV,
                        GateInitHold,
                        GateInitToMeasTransient,
                        GateTermV,
                        GateTermHold,
                        GateMeastoTermTransient,
                        DrainPatternName,
                        DrainMeasName,
                        DrainInitV,
                        DrainInitHold,
                        DrainInitToMeasTransient,
                        DrainTermV,
                        DrainTermHold,
                        DrainMeastoTermTransient);

            }
            else if (EnableOTFMeas == 1)
            {
                this.CreateOTFMeasurement(
                        GatePatternName,
                        GateMeasName,
                        GateInitV,
                        GateInitHold,
                        GateInitToMeasTransient,
                        GateTermV,
                        GateTermHold,
                        GateMeastoTermTransient,
                        DrainPatternName,
                        DrainMeasName,
                        DrainInitV,
                        DrainInitHold,
                        DrainInitToMeasTransient,
                        DrainTermV,
                        DrainTermHold,
                        DrainMeastoTermTransient);
            }

            if (GateChIMeasRange != StressVgCurrentRange)
            {
                wglib.SetMeasRangeEvent(
                    GatePatternName,
                    GateMeasRangeChangeName,
                    0.0,
                    GateChIMeasRange);
            }
            if (DrainChIMeasRange != StressVdCurrentRange)
            {
                wglib.SetMeasRangeEvent(
                    DrainPatternName,
                    DrainMeasRangeChangeName,
                    0.0,
                    DrainChIMeasRange);
            }

            
            #endregion

            #region Create post measurement pattern

            GatePatternName = GatePostMeasName;
            GateMeasName = GatePostMeasEventName;
            GateInitV = 0.0;
            GateInitHold = 0.0;
            GateInitToMeasTransient = StressToMeasEdge;
            GateTermV = 0.0;
            GateTermHold = 0.0;
            GateMeastoTermTransient = StressToMeasEdge;

            DrainPatternName = DrainPostMeasName;
            DrainMeasName = DrainPostMeasEventName;
            DrainInitV = 0.0;
            DrainInitHold = 0.0;
            DrainInitToMeasTransient = StressToMeasEdge;
            DrainTermV = 0.0;
            DrainTermHold = 0.0;
            DrainMeastoTermTransient = StressToMeasEdge;

            if (EnablePostMeas == 1)
            {
                if (EnableSamplingMeas == 1)
                {
                    this.CreateSamplingMeasurement(
                        GatePatternName,
                        GateMeasName,
                        GateInitV,
                        GateInitHold,
                        GateInitToMeasTransient,
                        GateTermV,
                        GateTermHold,
                        GateMeastoTermTransient,
                        DrainPatternName,
                        DrainMeasName,
                        DrainInitV,
                        DrainInitHold,
                        DrainInitToMeasTransient,
                        DrainTermV,
                        DrainTermHold,
                        DrainMeastoTermTransient);

                }
                else if (EnableSweepMeas == 1)
                {
                    this.CreateSweepMeasurement(
                        GatePatternName,
                        GateMeasName,
                        GateInitV,
                        GateInitHold,
                        GateInitToMeasTransient,
                        GateTermV,
                        GateTermHold,
                        GateMeastoTermTransient,
                        DrainPatternName,
                        DrainMeasName,
                        DrainInitV,
                        DrainInitHold,
                        DrainInitToMeasTransient,
                        DrainTermV,
                        DrainTermHold,
                        DrainMeastoTermTransient);

                }
                else if (EnableOTFMeas == 1)
                {
                    this.CreateOTFMeasurement(
                        GatePatternName,
                        GateMeasName,
                        GateInitV,
                        GateInitHold,
                        GateInitToMeasTransient,
                        GateTermV,
                        GateTermHold,
                        GateMeastoTermTransient,
                        DrainPatternName,
                        DrainMeasName,
                        DrainInitV,
                        DrainInitHold,
                        DrainInitToMeasTransient,
                        DrainTermV,
                        DrainTermHold,
                        DrainMeastoTermTransient);
                }

                if (GateChIMeasRange != StressVgCurrentRange)
                {
                    wglib.SetMeasRangeEvent(
                        GatePatternName,
                        GateMeasRangeChangeName,
                        0.0,
                        GateChIMeasRange);
                }
                if (DrainChIMeasRange != StressVdCurrentRange)
                {
                    wglib.SetMeasRangeEvent(
                        DrainPatternName,
                        DrainMeasRangeChangeName,
                        0.0,
                        DrainChIMeasRange);
                }


                #region Create post meas interval


                this.CreatePostMeasInterval();

                #endregion
           
            }

           
            #endregion


            #endregion

            #region Create stress pattern

            this.CreateStressPattern(ref StressLoopCount);

            #endregion

            #region Set sequence

            #region Set reference measurement

            if (EnableReferenceMeas == 1)
            {
                LoopCount = 1;

                GatePatternName = GateReferenceMeasName;
                wglib.AddSequence(GateChId, GatePatternName, LoopCount);

                DrainPatternName = DrainReferenceMeasName;
                wglib.AddSequence(DrainChId, DrainPatternName, LoopCount);
            }

            #endregion

            #region Set stress phase

            if (StressCount > 0)
            {
                LoopCount = 1;

                GatePatternName = GateInitialStressName;
                wglib.AddSequence(GateChId, GatePatternName, LoopCount);

                DrainPatternName = DrainInitialStressName;
                wglib.AddSequence(DrainChId, DrainPatternName, LoopCount);

                for (i = 0; i < StressCount; i++)
                {
                    if (VgStressType !=  1)
                    {
                        GatePatternName = GateStressNameHeader + i.ToString();
                        wglib.AddSequence(GateChId, GatePatternName, StressLoopCount[i]);

                        DrainPatternName = DrainStressNameHeader + i.ToString();
                        wglib.AddSequence(DrainChId, DrainPatternName, StressLoopCount[i]);
                    }
                    else
                    {
                        GatePatternName = GateStressNameHeader + "0";
                        wglib.AddSequence(GateChId, GatePatternName, StressLoopCount[i]);

                        DrainPatternName = DrainStressNameHeader + "0";
                        wglib.AddSequence(DrainChId, DrainPatternName, StressLoopCount[i]);
                    
                    }
                    LoopCount = 1;
                    if (i < StressCount - 1)
                    {
                        wglib.AddSequence(GateChId, GateMeasInStressName, LoopCount);
                        wglib.AddSequence(DrainChId, DrainMeasInStressName, LoopCount);
                    }
                    else
                    {
                        wglib.AddSequence(GateChId, GateMeasLastStressName, LoopCount);
                        wglib.AddSequence(DrainChId, DrainMeasLastStressName, LoopCount);
                    }
                }
            }
            #endregion

            #region Post measurement

            if (EnablePostMeas == 1)
            {
                for (i = 0; i < MeasIterationNumber; i++)
                {
                    LoopCount = 1;
                    GatePatternName = GatePostMeasInteravalName + i.ToString();
                    wglib.AddSequence(GateChId, GatePatternName, LoopCount);

                    DrainPatternName = GatePostMeasInteravalName + i.ToString();
                    wglib.AddSequence(DrainChId, DrainPatternName, LoopCount);

                    GatePatternName = GatePostMeasName;
                    wglib.AddSequence(GateChId, GatePatternName, LoopCount);

                    DrainPatternName = DrainPostMeasName;
                    wglib.AddSequence(DrainChId, DrainPatternName, LoopCount);
                }
            }


            #endregion

            #endregion
        }

        #endregion

        #endregion

        #endregion

        private void NBTI_Form_Load(object sender, EventArgs e)
        {
            int i;

            #region Create and numbering the list of the blank list to set an accumulated stress time.

            for (i = 0; i < 50; i++)
            {
                dataViewStressList.Rows.Add();                  // Add blank row.
                dataViewStressList[0, i].Value = i.ToString();  // Add number to the 1st column.
            }

            #region Restore the saved settings

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
            
            #endregion

            #endregion
        }

        private void groupVdACStress_Enter(object sender, EventArgs e)
        {

        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            WGFMU_CONFIG wgcfg = new WGFMU_CONFIG();
            try
            {
                wgcfg.ShowDialog();
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

        private void comboBoxGateChOperationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.SetWgfmuForceRange(this.comboBoxGateChOperationMode, ref this.comboBoxGateChForceRange);

                wglib.SetWgfmuMeasMode(this.comboBoxGateChOperationMode, ref this.comboBoxGateChMeasMode);
         
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

        private void comboGateChMeasMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxGateChMeasMode.SelectedIndex == 1)
                {
                    comboBoxGateChVMeasRange.Enabled = false;

                }
                else
                {
                    comboBoxGateChVMeasRange.Enabled = true;


                    wglib.SetWgfmuMeasRange(comboBoxGateChMeasMode, comboBoxGateChOperationMode, ref comboBoxGateChVMeasRange);
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

        private void comboBoxDrainOperationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.SetWgfmuForceRange(this.comboBoxDrainChOperationMode, ref this.comboBoxDrainChForceRange);

                wglib.SetWgfmuMeasMode(this.comboBoxDrainChOperationMode, ref this.comboBoxDrainChMeasMode);
         
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

        private void comboBoxDrainChMeasMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxDrainChMeasMode.SelectedIndex == 1)
                {
                    comboBoxDrainChVMeasRange.Enabled = false;

                }
                else
                {
                    comboBoxDrainChVMeasRange.Enabled = true;


                    wglib.SetWgfmuMeasRange(comboBoxDrainChMeasMode, comboBoxDrainChOperationMode, ref comboBoxDrainChVMeasRange);
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

        private void comboBoxGateCh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.CheckChannelConflict(this.comboBoxGateCh, ref this.comboBoxDrainCh);
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

        private void comboBoxDrainCh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.CheckChannelConflict(this.comboBoxDrainCh, ref this.comboBoxGateCh);
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

                ChannelList[0] = GateChId;
                ChannelList[1] = DrainChId;

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

        private void buttonExecMeas_Click(object sender, EventArgs e)
        {
            string SaveFileName;

            int[] ChannelList = new int[2];
            int i;
            for (i = 0; i < ChannelList.Length; i++)
            {
                ChannelList[i] = 0;
            }

            double MeasurementHold = 0;


            try
            {
                wglib.GetSaveFileName("DATA", out SaveFileName);

                if (SaveFileName != "")
                {
                    wglib.SaveSettings(this, ApplicationName, SaveFileName);


                    this.SetParameters();

                    ChannelList[0] = GateChId;
                    ChannelList[1] = DrainChId;

                    wglib.UpdateConfiginfo();

                    wglib.WgfmuOnline = 1;          //set wgfmu library to the offline mode.

                    wglib.Init();        // Initialize wgfmu session. 

                    wglib.SetupChannel(
                        GateChId,
                        GateChOperationMode,
                        GateChForceRange,
                        GateChMeasMode,
                        GateChIMeasRange,
                        GateChVMeasRange,
                        GateMeasEnable,
                        GateChHwSkew);

                    wglib.SetupChannel(
                        DrainChId,
                        DrainChOperationMode,
                        DrainChForceRange,
                        DrainChMeasMode,
                        DrainChIMeasRange,
                        DrainChVMeasRange,
                        DrainMeasEnable,
                        DrainChHwSkew);

                    this.CreatePattern();           // Create pattern data and set sequence

                    wglib.SaveSettings(this, ApplicationName, LatestConfigFileName);
                    if (BiasSource1Type != 0)
                    {
                        wglib.ForceBias(BiasSource1Type, BiasSource1Ch, BiasSource1BiasV, BiasSource1Compliance);
                    }

                    if (BiasSource2Type != 0)
                    {
                        wglib.ForceBias(BiasSource2Type, BiasSource2Ch, BiasSource2BiasV, BiasSource2Compliance);
                    }

                    wglib.ConnectChannesl(GateChId, 1);
                    wglib.ConnectChannesl(DrainChId, 1);

                    wglib.UpdateChannel(GateChId);
                    wglib.UpdateChannel(DrainChId);

                    wglib.ExecuteMeasurement();
                    
                    // System.Threading.Thread.Sleep(1000);

                    //wglib.NBTIRetrieveResultsByEventRealTtime(ref ChannelList, SaveFileName);
                    if (VdSequenceDelay >= 0)
                    {
                        if (EnableReferenceMeas == 1)
                        {
                            if (EnableSamplingMeas == 1)
                            {
                                MeasurementHold = SamplingHold;
                            }

                            if (EnableSweepMeas == 1)
                            {
                                MeasurementHold = SweepHold;
                            }

                            if (EnableOTFMeas == 1)
                            {
                                MeasurementHold = 0.0;
                            }
                        }
                        else if (AccStressTime[0] != 0)
                        {
                            if (EnableSamplingMeas == 1)
                            {
                                MeasurementHold = SamplingHold + StressVgRangeChangeHold;
                            }

                            if (EnableSweepMeas == 1)
                            {
                                MeasurementHold = SweepHold + StressVgRangeChangeHold;
                            }

                            if (EnableOTFMeas == 1)
                            {
                                MeasurementHold = 0.0 + StressVgRangeChangeHold;
                            }
                        }
                        else
                        {
                            if (EnableSamplingMeas == 1)
                            {
                                MeasurementHold = SamplingHold;
                            }

                            if (EnableSweepMeas == 1)
                            {
                                MeasurementHold = SweepHold;
                            }

                            if (EnableOTFMeas == 1)
                            {
                                MeasurementHold = 0.0;
                            }
                        }
                    }
                    else
                    {
                        if (EnableReferenceMeas == 1)
                        {
                            if (EnableSamplingMeas == 1)
                            {
                                MeasurementHold = SamplingHold - VdSequenceDelay;
                            }

                            if (EnableSweepMeas == 1)
                            {
                                MeasurementHold = SweepHold - VdSequenceDelay;
                            }

                            if (EnableOTFMeas == 1)
                            {
                                MeasurementHold = 0.0 - VdSequenceDelay;
                            }
                        }
                        else if (AccStressTime[0] != 0)
                        {
                            if (EnableSamplingMeas == 1)
                            {
                                MeasurementHold = SamplingHold + StressVgRangeChangeHold - VdSequenceDelay;
                            }

                            if (EnableSweepMeas == 1)
                            {
                                MeasurementHold = SweepHold + StressVgRangeChangeHold - VdSequenceDelay;
                            }

                            if (EnableOTFMeas == 1)
                            {
                                MeasurementHold = 0.0 + StressVgRangeChangeHold - VdSequenceDelay;
                            }
                        }
                        else
                        {
                            if (EnableSamplingMeas == 1)
                            {
                                MeasurementHold = SamplingHold - VdSequenceDelay;
                            }

                            if (EnableSweepMeas == 1)
                            {
                                MeasurementHold = SweepHold - VdSequenceDelay;
                            }

                            if (EnableOTFMeas == 1)
                            {
                                MeasurementHold = 0.0 - VdSequenceDelay;
                            }
                        }
                    
                    }
                    if (VgStressType == 1)
                    {
                        MeasurementHold = MeasurementHold + StressVgPulseDelay;
                    }
                        wglib.NBTIRetrieveResultsByPatternRealTime(ref ChannelList, SaveFileName, MeasurementHold);

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
                wglib.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
                wglib.Close();
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

        private void comboStressPulseFreq_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MeasIntervalMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonPulseStress_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonPulseStress.Checked == true)
            {
                this.checkBoxVdPulseStress.Enabled = true;
                this.groupVdACStress.Enabled = true;
            }
            else
            {
                this.checkBoxVdPulseStress.Enabled = false;
                this.groupVdACStress.Enabled = false;
            }
        }

        private void comboBoxSweepType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSweepType.SelectedIndex == 2)
            {
                groupSweepVgPulse.Enabled = true;
                groupSweepVdPulse.Enabled = true;
            }
            else
            {
                groupSweepVgPulse.Enabled = false;
                groupSweepVdPulse.Enabled = false;
            }
        }

        private void textBoxSweepVdPulseBase_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabDrainChSetup_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxDrainChVMeasRange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void comboBoxBiasSource1Ch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wglib.CheckBiasSourceChConflict(this.comboBoxBiasSource1Type,
                                                 ref this.comboBoxBiasSource1Ch,
                                                 this.comboBoxBiasSource2Type,
                                                 this.comboBoxBiasSource2Ch,
                                                 this.comboBoxGateCh,
                                                 this.comboBoxDrainCh);

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
                wglib.CheckBiasSourceChConflict( this.comboBoxBiasSource2Type,
                                                 ref this.comboBoxBiasSource2Ch,
                                                 this.comboBoxBiasSource1Type,
                                                 this.comboBoxBiasSource1Ch,
                                                 this.comboBoxGateCh,
                                                 this.comboBoxDrainCh);

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

        private void textBoxGateChHwSkew_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxGateChHwSkew, WGFMULib.HWDELAY_MIN, WGFMULib.MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxDrainChHwSkew_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxDrainChHwSkew, WGFMULib.HWDELAY_MIN, WGFMULib.MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxBiasSource1BiasV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBiasSource1BiasV, WGFMULib.SMU_VFORCE_MIN, WGFMULib.SMU_VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxBiasSource1Compliance_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBiasSource1Compliance, WGFMULib.SMU_ICOMP_MIN, WGFMULib.SMU_ICOMP_MAX, e, errorProvider1);
        }

        private void textBoxBiasSource2BiasV_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBiasSource2BiasV, WGFMULib.SMU_VFORCE_MIN, WGFMULib.SMU_VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxBiasSource2Compliance_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxBiasSource2Compliance, WGFMULib.SMU_ICOMP_MIN, WGFMULib.SMU_ICOMP_MAX, e, errorProvider1);
        }

        private void textBoxStressVg_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVg, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxStressVgBase_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVgBase, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxStressVgPulseDuty_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVgPulseDuty, WGFMULib.PULSE_DUTY_MIN, WGFMULib.PULSE_DUTY_MAX, e, errorProvider1);
        }

        private void textBoxStressVgPulseLeadingEdge_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVgPulseLeadingEdge, WGFMULib.PULSE_EDGE_MIN, WGFMULib.PULSE_EDGE_MAX, e, errorProvider1);
        }

        private void textBoxStressVgPulseDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVgPulseDelay, WGFMULib.PULSE_DELAY_MIN, WGFMULib.PULSE_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxStressVd_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVd, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxStressVdPulseBase_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVdPulseBase, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxStressVdPulseDuty_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVdPulseDuty, WGFMULib.PULSE_DUTY_MIN, WGFMULib.PULSE_DUTY_MAX, e, errorProvider1);
        }

        private void textBoxStressVdPulseLeadingEdge_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVdPulseLeadingEdge, WGFMULib.PULSE_EDGE_MIN, WGFMULib.PULSE_EDGE_MAX, e, errorProvider1);
        }

        private void textBoxStressVdPulseDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressVdPulseDelay, WGFMULib.PULSE_DELAY_MIN, WGFMULib.PULSE_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxStressToMeasEdge_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressToMeasEdge, WGFMULib.EDGE_MIN, WGFMULib.EDGE_MAX, e, errorProvider1);
        }

        private void textBoxVdSequenceDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxVdSequenceDelay, WGFMULib.SEQUENCE_DELAY_MIN, WGFMULib.SEQUENCE_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxStressDelayReferenceMeas_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxStressDelayReferenceMeas, WGFMULib.STRESS_MEAS_DELAY_MIN, WGFMULib.STRESS_MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxPostMeasDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxPostMeasDelay, WGFMULib.STRESS_MEAS_DELAY_MIN, WGFMULib.STRESS_MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxIterativeMeasInterval_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxIterativeMeasInterval, WGFMULib.MEAS_INTERVAL_MIN, WGFMULib.PATTERN_NAME_MAX, e, errorProvider1);
        }

        private void textBoxMeasIterationNumber_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxMeasIterationNumber, WGFMULib.MEAS_ITERATION_NUM_MIN, WGFMULib.MEAS_ITERATION_NUM_MAX, e, errorProvider1);
        }

        private void textBoxSamplingVg_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSamplingVg, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSamplingVd_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSamplingVd, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSamplingHold_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSamplingHold, WGFMULib.HOLD_MIN, WGFMULib.HOLD_MAX, e, errorProvider1);
        }

        private void textBoxSamplingMeasDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSamplingMeasDelay, WGFMULib.MEAS_DELAY_MIN, WGFMULib.MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxSamplingMeasInterval_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSamplingMeasInterval, WGFMULib.SAMPLING_INTERVAL_MIN, WGFMULib.SAMPLING_INTERVAL_MAX, e, errorProvider1);
        }

        private void textBoxSampingMeasPoints_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSampingMeasPoints, WGFMULib.SAMPLING_POINTS_MIN, WGFMULib.SAMPLING_POINTS_MAX, e, errorProvider1);
        }

        private void textBoxSamplingMeasAveragingTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSamplingMeasAveragingTime, WGFMULib.AVERAGING_TIME_MIN, WGFMULib.AVERAGING_TIME_MAX, e, errorProvider1);
        }

        private void textBoxSamplingExtraDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSamplingExtraDelay, WGFMULib.TERMINATION_DELAY_MIN, WGFMULib.TERMINATION_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxSweepVgStart_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVgStart, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSweepVgStop_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVgStop, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSweepStepNum_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateInt(this.textBoxSweepStepNum, WGFMULib.SWEEP_STEP_MIN, WGFMULib.SWEEP_STEP_MAX, e, errorProvider1);
        }

        private void textBoxSweepStepRise_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepStepRise, WGFMULib.EDGE_MIN, WGFMULib.EDGE_MAX, e, errorProvider1);
        }

        private void textBoxSweepHold_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepHold, WGFMULib.HOLD_MIN, WGFMULib.HOLD_MAX, e, errorProvider1);
        }

        private void textBoxSweepDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepDelay, WGFMULib.MEAS_DELAY_MIN, WGFMULib.MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxSweepAveragingTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepAveragingTime, WGFMULib.AVERAGING_TIME_MIN, WGFMULib.AVERAGING_TIME_MAX, e, errorProvider1);
        }

        private void textBoxSweepStepDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepStepDelay, WGFMULib.SWEEP_STEP_DELAY_MIN, WGFMULib.SWEEP_STEP_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxSweepDelayDualSweep_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepDelayDualSweep, WGFMULib.DUAL_SWEEP_DELAY_MIN, WGFMULib.DUAL_SWEEP_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxSweepVd_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVd, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSweepVgPulseBase_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVgPulseBase, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSweepPulsePeriod_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepPulsePeriod, WGFMULib.PULSE_PERIOD_MIN, WGFMULib.PULSE_PERIOD_MAX, e, errorProvider1);
        }

        private void textBoxSweepVgPulseLeadingEdge_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVgPulseLeadingEdge, WGFMULib.PULSE_EDGE_MIN, WGFMULib.PULSE_EDGE_MAX, e, errorProvider1);
        }

        private void textBoxSweepVgPulseWidth_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVgPulseWidth, WGFMULib.PULSE_WIDTH_MIN, WGFMULib.PULSE_WIDTH_MAX, e, errorProvider1);
        }

        private void textBoxSweepVgPulseDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVgPulseDelay, WGFMULib.PULSE_DELAY_MIN, WGFMULib.PULSE_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxSweepVdPulseBase_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVdPulseBase, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxSweepVdPulseWidth_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVdPulseWidth, WGFMULib.PULSE_WIDTH_MIN, WGFMULib.PULSE_WIDTH_MAX, e, errorProvider1);
        }

        private void textBoxSweepVdPulseLeadingEdge_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVdPulseLeadingEdge, WGFMULib.PULSE_EDGE_MIN, WGFMULib.PULSE_EDGE_MAX, e, errorProvider1);
        }

        private void textBoxSweepVdPulseDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxSweepVdPulseDelay, WGFMULib.PULSE_DELAY_MIN, WGFMULib.PULSE_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxOTFdVg_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxOTFdVg, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxOTFVd_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxOTFVd, WGFMULib.VFORCE_MIN, WGFMULib.VFORCE_MAX, e, errorProvider1);
        }

        private void textBoxOTFEdge_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxOTFEdge, WGFMULib.EDGE_MIN, WGFMULib.EDGE_MAX, e, errorProvider1);
        }

        private void textBoxOTFMeasDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxOTFMeasDelay, WGFMULib.MEAS_DELAY_MIN, WGFMULib.MEAS_DELAY_MAX, e, errorProvider1);
        }

        private void textBoxOTFMeasAveragingTime_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxOTFMeasAveragingTime, WGFMULib.AVERAGING_TIME_MIN, WGFMULib.AVERAGING_TIME_MAX, e, errorProvider1);
        }

        private void textBoxOTFStepDelay_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxOTFStepDelay, WGFMULib.SWEEP_STEP_DELAY_MIN, WGFMULib.SWEEP_STEP_DELAY_MAX, e, errorProvider1);
        }


        private void CheckStressTime(int Index)
        {
            double Value;
            double Value2;
            try
            {

                if (dataViewStressList[1, Index].Value == null) return;

                Value = double.Parse(dataViewStressList[1, Index].Value.ToString());
                this.errorProvider1.SetError(this.dataViewStressList, "");  

                if (Index == 0)
                {

                }
                else
                {
                    if (dataViewStressList[1, Index - 1].Value != null)
                    {
                        Value2 = double.Parse(dataViewStressList[1, Index - 1].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Previous cell must not be a blank!");
                        this.errorProvider1.SetError(this.dataViewStressList,"Previous cell must not be a blank!."); 
                        dataViewStressList[1, Index].Value = null;
                        return;

                    }

                    if (Value <= Value2)
                    {
                        MessageBox.Show("Duration of stress  must be larger than the previous stress ");
                        this.errorProvider1.SetError(this.dataViewStressList, "Duration of stress  must be larger than the previous stress.");  
                    }

                }
            }
            catch (System.FormatException)
            {

            }
        }


        private void dataViewStressList_CurrentCellDirtyStateChanged(
            object sender, EventArgs e)
        {
            if ((dataViewStressList.CurrentCellAddress.X == 1) &&
                dataViewStressList.IsCurrentCellDirty)
            {
                dataViewStressList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

/*
        private void dataViewStressList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            string StressTime = "";
            double Value = 0.0;

            for (i = 0; i < this.dataViewStressList.RowCount; i++)
            {
                if (this.dataViewStressList[1, i].Value != null)
                {
                    StressTime = this.dataViewStressList[1, i].Value.ToString();

                    try
                    {
                        Value = double.Parse(StressTime);
                        this.errorProvider1.SetError(this.dataViewStressList, "");

                        #region operation when the time value is changed
                        if (e.ColumnIndex == 1 && e.RowIndex >= 0)
                        {
                            CheckStressTime(e.RowIndex);
                        }
                        #endregion
                    }
                    catch (Exception)
                    {
                        this.errorProvider1.SetError(this.dataViewStressList, "Not an double value.");
                        MessageBox.Show("Input value is not an double value. Check the format.");
                    }
                }

            }
        }
*/
        private void dataViewStressList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int i = 0;
            string StressTime = "";
            double Value = 0.0;

            for (i = 0; i < this.dataViewStressList.RowCount; i++)
            {
                if (this.dataViewStressList[1, i].Value != null)
                {
                    StressTime = this.dataViewStressList[1, i].Value.ToString();

                    try
                    {
                        Value = double.Parse(StressTime);
                        this.errorProvider1.SetError(this.dataViewStressList, "");
                    }
                    catch (Exception)
                    {
                        this.errorProvider1.SetError(this.dataViewStressList, "Not an double value.");
                        MessageBox.Show("Input value is not an double value. Check the format.");

                    }

                    #region operation when the time value is changed
                    if (e.ColumnIndex == 1 && e.RowIndex >= 0)
                    {
                        CheckStressTime(e.RowIndex);
                    }
                    #endregion

                }

            }
        }

        private void textBoxVgRangeChangeHold_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxVgRangeChangeHold, WGFMULib.STRESS_RANGE_CHANGE_HOLD_MIN, WGFMULib.STRESS_RANGE_CHANGE_HOLD_MAX, e, errorProvider1);
        }

        private void textBoxVdRangeChangeHold_Validating(object sender, CancelEventArgs e)
        {
            wglib.ValidateDouble(this.textBoxVdRangeChangeHold, WGFMULib.STRESS_RANGE_CHANGE_HOLD_MIN, WGFMULib.STRESS_RANGE_CHANGE_HOLD_MAX, e, errorProvider1);
        }

        private void textBoxVdRangeChangeHold_TextChanged(object sender, EventArgs e)
        {
            this.textBoxVgRangeChangeHold.Text = this.textBoxVdRangeChangeHold.Text;
        }

        private void textBoxVgRangeChangeHold_TextChanged(object sender, EventArgs e)
        {
            this.textBoxVdRangeChangeHold.Text = this.textBoxVgRangeChangeHold.Text;
        }

    }
}