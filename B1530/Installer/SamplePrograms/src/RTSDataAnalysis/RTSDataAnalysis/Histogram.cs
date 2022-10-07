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
using System.Collections;

namespace Calculate
{
    public class PeakData : System.IComparable
    {
        public double dHeight;
        public double dValue;

        //
        // Function:    PeakData
        //
        // Description: <Constructor>
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       h               Height Value
        //              I       v               Item Value
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public PeakData(double h, double v)
        {
            dHeight = h;
            dValue = v;
        }

        //
        // Function:    CompareTo
        //
        // Description: Comparator for ArrayList.Sort().
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       h               Height Value
        //              I       v               Item Value
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public int CompareTo(object obj)
        {
            return (int)(((PeakData)obj).dHeight - dHeight);
        }
    }

    public class Histogram
    {
        private double[] _daVal;
        private int _iNumber;

        public double dMin;
        public double dMax;
        public double dMedian;
        public double dMean;
        public double dStd;

        public double[] daX = new double[10];
        public double[] daY = new double[10];
        public double[] daYSmooth = new double[10];

        public ArrayList alPeak = new ArrayList();

        //
        // Function:    Histogram
        //
        // Description: <Constructor>
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       daVal           Value Array
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public Histogram(double[] daVal)
        {
            _iNumber = daVal.Length;
            _daVal = new double[_iNumber];
            daVal.CopyTo(_daVal, 0);
            Array.Sort(_daVal);

            //
            // Min./Max.
            //
            dMin = _daVal[0];
            dMax = _daVal[_iNumber - 1];

            //
            // Median
            //
            if ((_iNumber & 0x1) == 1)
            {
                dMedian = _daVal[_iNumber / 2];
            }
            else
            {
                dMedian = (_daVal[_iNumber / 2] + _daVal[_iNumber / 2 - 1]) / 2;
            }

            //
            // Mean./Standard Deviation
            //
            double dSum = 0;
            double dSum2 = 0;
            foreach (double v in _daVal)
            {
                dSum += v;
                dSum2 += System.Math.Pow(v, 2);
            }
            dMean = dSum / _iNumber;
            dStd = dSum2 - 2 * dMean * dSum + System.Math.Pow(dMean, 2) * _iNumber;
            dStd /= _iNumber;
            dStd = System.Math.Sqrt(dStd);
        }

        //
        // Function:    Bin
        //
        // Description: Sift values into the specified number of bins.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       iNumBin         Number of Bins
        //              ----------------------------------------------------------
        //
        // Return:      0:  Success    Others: Failure
        //
        public int Bin(int iNumBin)
        {
            int iIndex = 0;
            double dBinLow, dBinHigh;

            //
            // Number of bins must be positive
            //
            if (iNumBin <= 0) return -1;

            //
            // Prepare bins
            //
            Array.Resize<double>(ref daX, iNumBin);
            Array.Resize<double>(ref daY, iNumBin);

            //
            // Binning
            //
            for ( int i = 0 ; i < iNumBin ; i++ )
            {
                //
                // Calculate bin's range
                //
                dBinLow = dMin + ((dMax - dMin) / iNumBin) * i; 
                dBinHigh = dMin + ((dMax - dMin) / iNumBin) * (i + 1); 
                daX[i] = (dBinLow + dBinHigh) / 2;

                //
                // Count up
                //
                daY[i] = 0;
                for ( ; iIndex < _iNumber ; iIndex++ )
                {
                    if (_daVal[iIndex] > dBinHigh)
                    {
                        break;
                    }
                    daY[i] += 1.0;
                }
            }

            return 0;
        }

        //
        // Function:    FindPeak
        //
        // Description: Find frequency peaks.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       iMinHeight      Minimum Peak Height
        //              I       iMinPeakToPeak  Minimum Distance between Peaks
        //              ----------------------------------------------------------
        //
        // Return:      (Always 0)
        //
        public int FindPeak(int iMinHeight, int iMinPeakToPeak)
        {
            //
            // Parameters
            //
            if (iMinHeight < 2) iMinHeight = 2;
            if (iMinPeakToPeak < 1) iMinPeakToPeak = 1;

            //
            // Smoothing (1)
            //
            Array.Resize<double>(ref daYSmooth, daY.Length);
            daY.CopyTo(daYSmooth, 0);
            Filters.MovingAverageFilter(ref daYSmooth, 9);

            //
            // Trimming
            //
            double[] yTrim = new double[daYSmooth.Length];
            daYSmooth.CopyTo(yTrim, 0);
            for ( int i = 0 ; i < yTrim.Length ; i++ )
            {
               yTrim[i] = System.Math.Max(yTrim[i], iMinHeight);
            }

            //
            // Difference
            //
            double[] diff = new double[yTrim.Length - 1];
            for ( int i = 0 ; i < diff.Length ; i++ )
            {
                diff[i] = yTrim[i + 1] - yTrim[i];
            }

            //
            // Smoothing (2)
            //
            double[] daYSmooth2 = new double[diff.Length];
            diff.CopyTo(daYSmooth2, 0);
            Filters.MovingAverageFilter(ref daYSmooth2, 5);

            //
            // Find Peak
            //
            int[] iaPeakIndex = new int[daYSmooth2.Length];
            int iNumPeak = 0;
            for ( int i = 1 ; i < (daYSmooth2.Length - 1) ; i++ )
            {
                if ((daYSmooth2[i - 1] > 0) && (daYSmooth2[i] < 0))
                {
                    iaPeakIndex[iNumPeak++] = i;
                    i += iMinPeakToPeak - 1;
                }
            }
            Array.Resize<int>(ref iaPeakIndex, iNumPeak);

            //
            // Sort by height
            //
            alPeak.Clear();
            for ( int i = 0 ; i < iNumPeak ; i++ )
            {
                alPeak.Add(new PeakData(daYSmooth[iaPeakIndex[i]], daX[iaPeakIndex[i]]));
            }
            alPeak.Sort();

            return 0;
        }

        //
        // Function:    FindBin
        //
        // Description: Collect the bins those frequency exceeds specified one.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       dMinValue       Minimum Frequency
        //              ----------------------------------------------------------
        //
        // Return:      Number of Bins
        //
        public int FindBin(double dMinValue)
        {
            int iCount = 0;

            for ( int i = 0 ; i < daY.Length ; i++ )
            {
                if (daY[i] > dMinValue) iCount++;
            }

            return iCount;
        }
    }
}
