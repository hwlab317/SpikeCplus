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
    delegate double EvalFunc(double[] sParam);

    public class Tau
    {
        public Histogram histH;
        public Histogram histL;

        private static double[] daXXH;
        private static double[] daYYH;
        private static double[] daXXL;
        private static double[] daYYL;

        public double[] daResultHigh;
        public double[] daResultLow;

        public double dResidualHigh;
        public double dResidualLow;

        public double dTauOn;
        public double dTauOff;

        //
        // Function:    Tau
        //
        // Description: <Constructor>
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        public Tau()
        {
        }

        //
        // Function:    Calculate
        //
        // Description: Calculate Tau Value.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       dFreq           Sampling Frequency
        //              I       diz             Digitized Data Object
        //              I       bNMOS           Junction Type (true: nMOS)
        //              ----------------------------------------------------------
        //
        // Return:      0: Success    Others: Failure
        //
        public int Calculate(double dFreq, Digitize diz, bool bNMOS)
        {
            int iNumRise = 0;
            int iNumFall = 0;
            int[] iaRiseEdge = new int[diz.iaHL.Length];
            int[] iaFallEdge = new int[diz.iaHL.Length];

            //
            // Initialize
            //
            double[] daInitial = { 10.0, 0.01 };
            daResultHigh = new double[daInitial.Length];
            daResultLow = new double[daInitial.Length];
            dResidualHigh = 0.0;
            dResidualLow = 0.0;
            dTauOn = 0.0;
            dTauOff = 0.0;

            //
            // Find rising/falling edges
            //
            for ( int i = 1 ; i < diz.iaHL.Length ; i++ )
            {
                if ((diz.iaHL[i - 1] == 0) && (diz.iaHL[i] == 1))
                {
                    iaRiseEdge[iNumRise++] = i;
                }
                else if ((diz.iaHL[i - 1] == 1) && (diz.iaHL[i] == 0))
                {
                    iaFallEdge[iNumFall++] = i;
                }
            }

            //
            // Trimming with the smallest size
            //
            int nLen = System.Math.Min(iNumRise, iNumFall);
            Array.Resize<int>(ref iaRiseEdge, nLen);
            Array.Resize<int>(ref iaFallEdge, nLen);
            if (nLen < 2)
            {
                return -1;
            }

            //
            // Check initial level
            //
            bool bStartLow = (diz.iaHL[0] == 0);
            double[] daHighWidth;
            double[] daLowWidth;
            if (bStartLow)
            {
                daHighWidth = new double[nLen];
                daLowWidth = new double[nLen - 1];
                for ( int i = 0 ; i < nLen ; i++ )
                {
                    daHighWidth[i] = iaFallEdge[i] - iaRiseEdge[i];
                }
                for ( int i = 0 ; i < (nLen - 1) ; i++ )
                {
                    daLowWidth[i] = iaRiseEdge[i + 1] - iaFallEdge[i];
                }
            }
            else
            {
                daHighWidth = new double[nLen - 1];
                daLowWidth = new double[nLen];
                for ( int i = 0 ; i < (nLen - 1) ; i++ )
                {
                    daHighWidth[i] = iaFallEdge[i + 1] - iaRiseEdge[i];
                }
                for ( int i = 0 ; i < nLen ; i++ )
                {
                    daLowWidth[i] = iaRiseEdge[i] - iaFallEdge[i];
                }
            }

            histH = new Histogram(daHighWidth);
            int nBin = 30;
            histH.Bin(System.Math.Min(1000, System.Math.Max(nLen / nBin, 10)));
            while ((histH.FindBin(1) < 7) && (nLen / nBin >= 10) && (nBin >= 2))
            {
                nBin /= 2;
                histH.Bin(System.Math.Max(nLen / nBin, 10));
            }

            histL = new Histogram(daLowWidth);
            nBin = 30;
            histL.Bin(System.Math.Min(1000, System.Math.Max(nLen / nBin, 10)));
            while ((histL.FindBin(1) < 7) && (nLen / nBin >= 10) && (nBin >= 2))
            {
                nBin /= 2;
                histL.Bin(System.Math.Max(nLen / nBin, 10));
            }

            //
            // Retrieve histogram classes their frequency is greater then 2.
            // (1st and 2nd class are ignored)
            //
            daXXH = new double[histH.daY.Length];
            daYYH = new double[histH.daY.Length];
            int nClass = 0;
            for ( int i = 0 ; i < histH.daY.Length ; i++ )
            {
                if ((i >= 2) && (histH.daY[i] > 2))
                {
                    daXXH[nClass] = histH.daX[i];
                    daYYH[nClass] = histH.daY[i];
                    nClass++;
                }
            }
            Array.Resize<double>(ref daXXH, nClass);
            Array.Resize<double>(ref daYYH, nClass);
            if (nClass < 2)
            {
                return -1;
            }

            //
            // Simplex Method
            //
            SimplexMethod ss = new SimplexMethod();
            EvalFunc ef = new EvalFunc(evalHigh);
            ss.search(daInitial, ref daResultHigh, ef);
            double dTauHigh = -1.0 / daResultHigh[1] / dFreq; 

            //
            // Residual Square Mean
            //
            for ( int i = 0 ; i < daXXH.Length ; i++ )
            {
                dResidualHigh += System.Math.Pow(daYYH[i] - daResultHigh[0] * System.Math.Exp(daResultHigh[1] * daXXH[i]), 2);
            }
            dResidualHigh /= daXXH.Length;

            //
            // Retrieve histogram classes their frequency is greater then 2.
            // (1st and 2nd class are ignored)
            //
            daXXL = new double[histL.daY.Length];
            daYYL = new double[histL.daY.Length];
            nClass = 0;
            for ( int i = 0 ; i < histL.daY.Length ; i++ )
            {
                if ((i >= 2) && (histL.daY[i] > 2))
                {
                    daXXL[nClass] = histL.daX[i];
                    daYYL[nClass] = histL.daY[i];
                    nClass++;
                }
            }
            Array.Resize<double>(ref daXXL, nClass);
            Array.Resize<double>(ref daYYL, nClass);
            if (nClass < 2)
            {
                return -1;
            }

            //
            // Simplex Method
            //
            ef = new EvalFunc(evalLow);
            ss.search(daInitial, ref daResultLow, ef);
            double dTauLow = -1.0 / daResultLow[1] / dFreq; 

            //
            // Residual Square Mean
            //
            for ( int i = 0 ; i < daXXL.Length ; i++ )
            {
                dResidualLow += System.Math.Pow(daYYL[i] - daResultLow[0] * System.Math.Exp(daResultLow[1] * daXXL[i]), 2);
            }
            dResidualLow /= daXXL.Length;

            //
            // Ton/Toff
            //
            if (bNMOS)
            {
                dTauOn = dTauHigh;
                dTauOff = dTauLow;
            }
            else
            {
                dTauOn = dTauLow;
                dTauOff = dTauHigh;
            }

            return 0;
        }

        //
        // Function:    eval
        //
        // Description: Simplex Search "Evaluation function"
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       sParam          Parameter Array
        //              I       daX             X-axis Data Array
        //              I       daY             Y-axis Data Array
        //              ----------------------------------------------------------
        //
        // Return:      Collection of errors
        //
        private static double eval(double[] sParam, double[] daX, double[] daY)
        {
            double value = 0.0;
            double a;
            double b;

            a = sParam[0];
            b = sParam[1];

            for ( int i = 0 ; i < daX.Length ; i++ )
            {
                value += System.Math.Pow(daY[i] - a * System.Math.Exp(b * daX[i]), 2);
            }

            return value;
        }

        //
        // Function:    evalHigh
        //
        // Description: "Simplex Search" Evaluation Function for "High" Side
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       sParam          Parameter Array
        //              ----------------------------------------------------------
        //
        // Return:      Collection of errors
        //
        public static double evalHigh(double[] sParam)
        {
            return eval(sParam, daXXH, daYYH);
        }

        //
        // Function:    evalLow
        //
        // Description: "Simplex Search" Evaluation Function for "Low" Side
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       sParam          Parameter Array
        //              ----------------------------------------------------------
        //
        // Return:      Collection of errors
        //
        public static double evalLow(double[] sParam)
        {
            return eval(sParam, daXXL, daYYL);
        }
    }
}
