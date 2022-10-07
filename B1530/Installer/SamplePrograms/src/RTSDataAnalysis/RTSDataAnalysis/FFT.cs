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
    public class FFT
    {
        public double[] daReal;
        public double[] daImage;

        //
        // Function:    FFT
        //
        // Description: <Constructor>
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       fr              Value Array
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public FFT(double[] fr)
        {
            daReal = new double[fr.Length];
            daImage = new double[fr.Length];
            fr.CopyTo(daReal, 0);
        }

        //
        // Function:    fft
        //
        // Description: Perform "Fast Fourier Transform".
        //
        // Parameters:  N/A
        //
        // Return:      (Always 0)
        //
        // Note:        The number of values will be rounded up to 2^n (n > 0).
        //
        public int fft()
        {
            int i, m, iw, j, l, lp, lp2, n2;
            double dArg, dDarg, dCos, dSin, dWreal, dWimage;
            double dXa, dYa;

            int iLength = daReal.Length;
            int iOldLength = iLength;
            int iFlag = 1;                 // -1 means iFFT
            int iCount;

            //
            // Number of data must be 2^n (n > 0)
            //
            if (iLength == 1)
            {
                iLength = 0;
                Array.Resize<double>(ref daReal, iLength);
                Array.Resize<double>(ref daImage, iLength);
            }
            else
            {
                iCount = 0;
                for ( int v = iLength ; v != 0 ; v = (v >> 1))
                {
                    iCount += (v & 0x1);
                }
                if (iCount > 1)
                {
                    int iMsb = 0;
                    for ( i = 0 ; i < 32 ; i++ )
                    {
                        if ((iLength & (0x1 << i)) != 0)
                        {
                            iMsb = i;
                        }
                    }
                    if (iMsb != 0)
                    {
                        iLength = 0x1 << ++iMsb;
                        Array.Resize<double>(ref daReal, iLength);
                        for ( i = iOldLength ; i < iLength ; i++ )
                        {
                            daReal[i] = daReal[0];
                        }
                        Array.Resize<double>(ref daImage, iLength);
                    }
                }
            }

            //
            // Create image values
            //
            Array.Clear(daImage, 0, daImage.Length);

            //
            // Bit Reversal
            //
            j = 0;
            for ( i = 0 ; i <= (iLength - 2) ; i++ )
            {
                if (i < j)
                {
                    dXa = daReal[i]; daReal[i] = daReal[j]; daReal[j] = dXa;
                    dYa = daImage[i]; daImage[i] = daImage[j]; daImage[j] = dYa;
                }

                n2 = iLength / 2;
                while (j >= n2)
                {
                    j = j - n2;
                    n2 = n2 / 2;
                }
                j = j + n2;
            }

            //
            // FFT
            //
            m = 0; n2 = iLength;
            while (n2 != 1)
            {
                m = m + 1;
                n2 = n2 / 2;
            }
            for ( l = 1 ; l <= m ; l++ )
            {
                lp = (int)System.Math.Pow(2, l);
                lp2 = lp / 2;
                dDarg = -iFlag * System.Math.PI / lp2;
                dArg = 0;
                for ( j = 0 ; j <= (lp2 - 1) ; j++ )
                {
                    dCos = System.Math.Cos(dArg);
                    dSin = System.Math.Sin(dArg);
                    dArg += dDarg;
                    for ( i = j ; i <= (iLength - 1) ; i += lp)
                    {
                        iw = i + lp2;
                        dWreal = daReal[iw] * dCos - daImage[iw] * dSin;
                        dWimage = daReal[iw] * dSin + daImage[iw] * dCos;
                        daReal[iw] = daReal[i] - dWreal;
                        daImage[iw] = daImage[i] - dWimage;
                        daReal[i] = daReal[i] + dWreal;
                        daImage[i] = daImage[i] + dWimage;
                    }
                }
            }

            //
            // Resize to the original
            //
            Array.Resize<double>(ref daReal, iOldLength);
            Array.Resize<double>(ref daImage, iOldLength);

            //
            // All elements must be divided by N, but it causes different
            // result from MATLAB fft() output.  So do nothing at here.
            //

            return 0;
        }
    }
}
