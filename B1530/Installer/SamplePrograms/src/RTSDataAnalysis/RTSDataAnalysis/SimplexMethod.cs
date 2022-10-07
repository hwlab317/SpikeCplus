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

namespace Calculate
{
    class SimplexMethod
    {
        private double[][] daaS;
        private double[] daSums;
        private double[] daSS;

        private const double dAlpha = 2;
        private const double dBeta = 0.5;
        private const double dGamma = 2;
        private const double dEpsilon = 0.0000001;
        private const int iMaxRepeat = 2000;

        //
        // Function:    SimplexMethod
        //
        // Description: <Constructor>
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        public SimplexMethod()
        {
        }

        //
        // Function:    search
        //
        // Description: Perform "Simplex Search".
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       initial         Initial Value
        //              O       rs              Result Value
        //              I       eval            Evaluation Function
        //              ----------------------------------------------------------
        //
        // Return:      0: Converged       -1:  Not Converged
        //
        public int search(double[] initial, ref double[] rs, EvalFunc eval)
        {
            System.Random r = new System.Random();
            double dFactor;
            double dMinValue, dMaxValue, dDiff;
            int iMinIndex, iMaxIndex;
            int iTemp;
            bool bFound = false;
            int iParameter;
            int il0, ilr, ile;

            iParameter = initial.Length;

            dMinValue = dMaxValue = 0.0;
            iMinIndex = iMaxIndex = 0;

            il0 = iParameter + 1;
            ilr = iParameter + 2;
            ile = iParameter + 3;

            daaS = new double[ile][];
            for ( int i = 0 ; i < daaS.Length ; i++ )
            {
                daaS[i] = new double[iParameter];
            }
            daSums = new double[iParameter];
            daSS = new double[ile];

            for ( int i = 0 ; i < il0 ; i++ )
            {
                for ( int j = 0 ; j < iParameter ; j++ )
                {
                    daaS[i][j] = initial[j];
                }
            }

            for ( int i = 0 ; i < iParameter ; i++ )
            {
                dFactor = r.NextDouble() * 0.1 + 0.2;
                daaS[i][i] = daaS[i][i] + daaS[i][i] * dFactor;
            }

            for ( int i = 0 ; i < iMaxRepeat ; i++ )
            {
                for ( int j = 0 ; j < iParameter ; j++ )
                {
                    daSums[j] = 0.0;
                }

                for ( int j = 0 ; j < il0 ; j++ )
                {
                    daSS[j] = eval(daaS[j]);
                    if (j == 0)
                    {
                        iMaxIndex = j;
                        iMinIndex = j;
                        dMaxValue = daSS[j];
                        dMinValue = dMaxValue;
                    }
                    else
                    {
                        if (daSS[j] >= dMaxValue)
                        {
                            iMaxIndex = j;
                            dMaxValue = daSS[j];
                        }
                        if (daSS[j] <= dMinValue)
                        {
                            iMinIndex = j;
                            dMinValue = daSS[j];
                        }
                    }
                    for ( int k = 0 ; k < iParameter ; k++ )
                    {
                        daSums[k] = daSums[k] + daaS[j][k];
                    }
                }

                for ( int j = 0 ; j < iParameter ; j++ )
                {
                    daaS[ilr - 1][j] = (1 + dAlpha) * (daSums[j] - daaS[iMaxIndex][j]) / iParameter - dAlpha * daaS[iMaxIndex][j];
                }

                daSS[ilr - 1] = eval(daaS[ilr - 1]);

                iTemp = ilr;
                if ((daSS[ilr - 1]) < dMinValue)
                {
                    for ( int j = 0 ; j < iParameter ; j++ )
                    {
                        daaS[ile - 1][j] = dGamma * daaS[ilr - 1][j] + (1 - dGamma) * (daSums[j] - daaS[iMaxIndex][j]) / iParameter;
                    }
                    daSS[ile - 1]= eval(daaS[ile - 1]);
                    if (daSS[ile - 1] < daSS[ilr - 1])
                    {
                        iTemp = ile;
                    }
                }
                else if (daSS[ilr - 1] > dMaxValue)
                {
                    for ( int j = 0 ; j < iParameter ; j++ )
                    {
                        daaS[ilr - 1][j] = dBeta * daaS[iMaxIndex][j] + (1 - dBeta) * (daSums[j] - daaS[iMaxIndex][j]) / iParameter;
                    }
                    daSS[ilr - 1] = eval(daaS[ilr - 1]);
                }

                dDiff = 0;
                for ( int j = 0 ; j < il0 ; j++ )
                {
                    for ( int k = 0 ; k < iParameter ; k++ )
                    {
                        if (dDiff < System.Math.Abs(daaS[j][k] - daaS[iTemp - 1][k]))
                        {
                            dDiff = System.Math.Abs(daaS[j][k] - daaS[iTemp - 1][k]);
                        }
                    }
                }

                if (dDiff < dEpsilon)
                {
                    bFound = true;
                    break;
                }
                for ( int j = 0 ; j < iParameter ; j++ )
                {
                    daaS[iMaxIndex][j] = daaS[iTemp - 1][j];
                }
            }

            for ( int i = 0 ; i < iParameter ; i++ )
            {
                rs[i] = daaS[0][i];
            }

            return (bFound == true ? 0 : -1);
        }
    }
}
