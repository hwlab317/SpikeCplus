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
using System.IO;
using System.Collections;

namespace Calculate
{
    public class Digitize
    {
        private double[] _val;
        private double _dThreshold;
        private double _dHysteresis;
        private double _dHighLevel;
        private double _dLowLevel;
        private double _dHighLimit;
        private double _dLowLimit;
        private int _iNumber;

        private const int iInitialCheckRange1 = 50;
        private const int iInitialCheckRange2 = 300;
        private const int iInitialStep = 50;

        public int[] iaHL = new int[0];

        //
        // Function:    Digitize
        //
        // Description: <Constructor>
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       val             Value Array
        //              I       dThreshold      Threshold Value
        //              I       dHysteresis     Initial Hysteresis Value
        //              I       dHighLevel      "High" Level
        //              I       dLowLevel       "Low" Level
        //              I       dHighLimit      Limit Value for "High" Side
        //              I       dLowLimit       Limit Value for "Low" Side
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public Digitize(double[] val, double dThreshold, double dHysteresis, double dHighLevel, double dLowLevel, double dHighLimit, double dLowLimit)
        {
            _iNumber = val.Length;
            _val = new double[_iNumber];
            val.CopyTo(_val, 0);

            _dThreshold = dThreshold;
            _dHysteresis = dHysteresis;
            _dHighLevel = dHighLevel;
            _dLowLevel = dLowLevel;
            _dHighLimit = dHighLimit;
            _dLowLimit = dLowLimit;
        }

        //
        // Function:    countAbove
        //
        // Description: Collect "High" values.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       iRangeMin       Start Index
        //              I       iRangeMax       Stop Index
        //              I       dThreshold      Threshold Value
        //              I       dRelHighLimit   Limit Value for "High" Side
        //              O       iA              Current Array
        //              ----------------------------------------------------------
        //
        // Return:      Number of "High" values.
        //
        private int countAbove(int iRangeMin, int iRangeMax, double dThreshold, double dRelHighLimit, ref int[] iA)
        {
            int iCount = 0;

            for ( int i = iRangeMin ; i <= iRangeMax ; i++ )
            {
                if ((_val[i] > (dThreshold + _dHysteresis)) && (_val[i] < (_dHighLevel + dRelHighLimit)))
                {
                    iA[iCount++] = i - iRangeMin;
                }
            }

            Array.Resize<int>(ref iA, iCount);

            return iCount;
        }

        //
        // Function:    countBelow
        //
        // Description: Collect "Low" values.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       iRangeMin       Start Index
        //              I       iRangeMax       Stop Index
        //              I       dThreshold      Threshold Value
        //              I       dRelLowLimit    Limit Value for "Low" Side
        //              O       iA              Current Array
        //              ----------------------------------------------------------
        //
        // Return:      Number of "Low" values.
        //
        private int countBelow(int iRangeMin, int iRangeMax, double dThreshold, double dRelLowLimit, ref int[] iA)
        {
            int iCount = 0;

            for ( int i = iRangeMin ; i <= iRangeMax ; i++ )
            {
                if ((_val[i] < (dThreshold - _dHysteresis)) && (_val[i] > (_dLowLevel - dRelLowLimit)))
                {
                    iA[iCount++] = i - iRangeMin;
                }
            }

            Array.Resize<int>(ref iA, iCount);

            return iCount;
        }

        //
        // Function:    digitize
        //
        // Description: Digitize all values in the value array.
        //
        // Parameters:  N/A
        //
        // Return:      (Always 0)
        //
        public int digitize()
        {
            const int iCheckRange2Min = 40;
            const int iCheckRange2Max = 5000;
            const double dNgRatioMin = 0.05;
            const double dNgRatioMax = 0.2;

            int iCheckRange1;
            int iCheckRange2 = iInitialCheckRange2;
            int iStep = iInitialStep;
            int iLevel = 0;
            double dTh = _dThreshold;
            double dRelHighLimit = _dHighLimit - _dHighLevel;
            double dRelLowLimit = _dLowLevel - _dLowLimit;
            double dSwing = _dHighLevel - _dLowLevel;

            //
            // Prepare workspace for performance-up
            //
            double[] daTh2 = new double[_iNumber];
            double[] daL2 = new double[_iNumber];
            double[] daSwing2 = new double[_iNumber];
            double[] daRange2 = new double[_iNumber];
            double[] daTH0 = new double[_iNumber];
            iaHL = new int[_iNumber];
            for ( int i = 0 ; i < _iNumber ; i++ )
            {
                daTh2[i] = dTh;
                daL2[i] = dTh;
                daTH0[i] = dTh;
                daSwing2[i] = 0.0;
                daRange2[i] = 0.0;
                iaHL[i] = 0;
            }

            //
            // Main Routine
            //
            int iPti = 0;
            int iPt = 0;
            int iRangeMin, iRangeMax, iRangeLen;
            int[] iaAboveTh, iaBelowTh;
            int iNg;
            while (iPt < _iNumber - iCheckRange2Min)
            {
                if (iPt > iInitialCheckRange1)
                {
                    iCheckRange1 = iInitialCheckRange1;
                }
                else
                {
                    iCheckRange1 = iPt;
                }

                if (iPt > (_iNumber - iCheckRange2 - 1))
                {
                    iCheckRange2 = _iNumber - iPt - 1;
                }

                //
                // 1st step
                //
                iRangeMin = iPt - iCheckRange1;
                iRangeMax = iPt + iCheckRange2;
                iRangeLen = iRangeMax - iRangeMin + 1;
                iaAboveTh = new int[iRangeLen];
                iaBelowTh = new int[iRangeLen];
                countAbove(iRangeMin, iRangeMax, dTh, dRelHighLimit, ref iaAboveTh);
                countBelow(iRangeMin, iRangeMax, dTh, dRelLowLimit, ref iaBelowTh);
                iNg = iRangeLen - iaAboveTh.Length - iaBelowTh.Length;

                //
                // 2nd step
                //
                while ((((double)iNg / iRangeLen) < dNgRatioMin) && (iCheckRange2 < iCheckRange2Max) && ((iPt + iCheckRange2 + 10) < _iNumber))
                {
                    iCheckRange2 += 10;
                    iRangeMin = iPt - iCheckRange1;
                    iRangeMax = iPt + iCheckRange2;
                    iRangeLen = iRangeMax - iRangeMin + 1;
                    Array.Resize<int>(ref iaAboveTh, iRangeLen);
                    Array.Resize<int>(ref iaBelowTh, iRangeLen);
                    countAbove(iRangeMin, iRangeMax, dTh, dRelHighLimit, ref iaAboveTh);
                    countBelow(iRangeMin, iRangeMax, dTh, dRelLowLimit, ref iaBelowTh);
                    iNg = iRangeLen - iaAboveTh.Length - iaBelowTh.Length;
                }

                //
                // 3rd step
                //
                while ((((double)iNg / iRangeLen) > dNgRatioMax) && (iCheckRange2 > iCheckRange2Min))
                {
                    iCheckRange2 -= 10;
                    iRangeMin = iPt - iCheckRange1;
                    iRangeMax = iPt + iCheckRange2;
                    iRangeLen = iRangeMax - iRangeMin + 1;
                    Array.Resize<int>(ref iaAboveTh, iRangeLen);
                    Array.Resize<int>(ref iaBelowTh, iRangeLen);
                    countAbove(iRangeMin, iRangeMax, dTh, dRelHighLimit, ref iaAboveTh);
                    countBelow(iRangeMin, iRangeMax, dTh, dRelLowLimit, ref iaBelowTh);
                    iNg = iRangeLen - iaAboveTh.Length - iaBelowTh.Length;
                }

                iStep = iCheckRange2 - 20;

                //
                // Transition Points (High Side)
                //
                int[] iaAboveTh0 = new int[iaAboveTh.Length];
                if (iaAboveTh.Length > 0)
                {
                    int[] iaTrans = new int[iaAboveTh.Length - 1];
                    int iNumTrans = 0;

                    for ( int i = 0 ; i < iaAboveTh.Length - 1 ; i++ )
                    {
                        if ((iaAboveTh[i + 1] - iaAboveTh[i]) > 1)
                        {
                            iaTrans[iNumTrans++] = i;
                        }
                    }
                    Array.Resize<int>(ref iaTrans, iNumTrans);

                    int iTransIndex = 0;
                    iNumTrans = 0;
                    for ( int i = 0 ; i < iaAboveTh.Length ; i++ )
                    {
                        //
                        // Skip transition point and its next.
                        //
                        if (iTransIndex < iaTrans.Length)
                        {
                            if (i == iaTrans[iTransIndex]) continue;
                            if (i == iaTrans[iTransIndex] + 1)
                            {
                                iTransIndex++;
                                continue;
                            }
                        }

                        //
                        // Copy normal point
                        //
                        iaAboveTh0[iNumTrans++] = iaAboveTh[i];
                    }
                    Array.Resize<int>(ref iaAboveTh0, iNumTrans);
                }

                //
                // Transition Points (Low Side)
                //
                int[] iaBelowTh0 = new int[iaBelowTh.Length];
                if (iaBelowTh.Length > 0)
                {
                    int[] iaTrans = new int[iaBelowTh.Length - 1];
                    int iNumTrans = 0;

                    for ( int i = 0 ; i < iaBelowTh.Length - 1 ; i++ )
                    {
                        if ((iaBelowTh[i + 1] - iaBelowTh[i]) > 1)
                        {
                            iaTrans[iNumTrans++] = i;
                        }
                    }
                    Array.Resize<int>(ref iaTrans, iNumTrans);

                    int iTransIndex = 0;
                    iNumTrans = 0;
                    for ( int i = 0 ; i < iaBelowTh.Length ; i++ )
                    {
                        //
                        // Skip transition point and its next.
                        //
                        if (iTransIndex < iaTrans.Length)
                        {
                            if (i == iaTrans[iTransIndex]) continue;
                            if (i == iaTrans[iTransIndex] + 1)
                            {
                                iTransIndex++;
                                continue;
                            }
                        }

                        //
                        // Copy normal point
                        //
                        iaBelowTh0[iNumTrans++] = iaBelowTh[i];
                    }
                    Array.Resize<int>(ref iaBelowTh0, iNumTrans);
                }

                //
                // Compound
                //
                double[] daCurrentPart = new double[iRangeLen];
                for ( int i = 0 ; i < iaAboveTh0.Length ; i++ )
                {
                    daCurrentPart[iaAboveTh0[i]] = _val[iaAboveTh0[i] + iRangeMin];
                }
                for ( int i = 0 ; i < iaBelowTh0.Length ; i++ )
                {
                    daCurrentPart[iaBelowTh0[i]] = _val[iaBelowTh0[i] + iRangeMin] + dSwing;
                }

                int[] iaX = new int[iaAboveTh0.Length + iaBelowTh0.Length];
                iaAboveTh0.CopyTo(iaX, 0);
                iaBelowTh0.CopyTo(iaX, iaAboveTh0.Length);
                Array.Sort(iaX);

                //
                // Interpolate
                //
                if (iaX.Length > 0)
                {
                    for ( int i = 0 ; i < iaX[0] ; i++ )
                    {
                        daCurrentPart[i] = daCurrentPart[iaX[0]];
                    }
                    for ( int i = 0 ; i < iaX.Length - 1 ; i++ )
                    {
                        int iStart = iaX[i];
                        int iEnd = iaX[i + 1];

                        if (iEnd - iStart > 1)
                        {
                            double dStep = (daCurrentPart[iEnd] - daCurrentPart[iStart]) / (iEnd - iStart);

                            for ( int j = iStart ; j < iEnd ; j++ )
                            {
                                daCurrentPart[j] = daCurrentPart[iStart] + (j - iStart) * dStep;
                            }
                        }
                    }
                    for ( int i = iaX[iaX.Length - 1] + 1 ; i < iRangeLen ; i++ )
                    {
                        daCurrentPart[i] = daCurrentPart[iaX[iaX.Length - 1]];
                    }
                }
                else
                {
                    for ( int i = 0 ; i < iRangeLen ; i++ )
                    {
                        daCurrentPart[i] = _val[iRangeMin + i];
                    }
                }

                //
                // Curve Fitting (NOTE : Least Squares Method would be better.)
                //
                Filters.MovingAverageFilter(ref daCurrentPart, 9);

                //
                // New threshold level
                //
                for ( int i = 0 ; i < daCurrentPart.Length ; i++ )
                {
                    if ((iPt - iCheckRange1 + i + 1) < daTH0.Length)
                    {
                        daTH0[iPt - iCheckRange1 + i + 1] = daCurrentPart[i] - dSwing / 2;
                    }
                }

                //
                // Update threshold
                //
                double dSum = 0.0;
                for ( int i = iPt ; i < (iPt + iStep + 10 + 1) ; i++ )
                {
                    dSum += daTH0[i];
                }
                dTh = dSum / (iStep + 10 + 1);

                //
                // Keep variables
                //
                for ( int i = iPt ; i < (iPt + iStep) ; i++ )
                {
                    daTh2[i] = dTh;
                    daSwing2[i] = dSwing;
                }
                _dHysteresis = dSwing * 0.2;

                //
                // Judge High/Low
                //
                for ( int i = iPt ; i < (iPt + iStep) ; i++ )
                {
                    if (_val[i] >= (daTH0[i] + _dHysteresis))
                    {
                        iLevel = 1;
                    }
                    else if (_val[i] < (daTH0[i] - _dHysteresis))
                    {
                        iLevel = 0;
                    }
                    iaHL[i] = iLevel;
                }

                //
                // Prepare for next loop
                //
                iPti += 1;
                iPt += iStep;
            }

            return 0;
        }
    }
}
