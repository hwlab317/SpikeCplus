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
    public class Filters
    {
        //
        // Function:    MovingAverageFilter
        //
        // Description: MovingAverageFilter
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I/O     daIn            Value Array
        //              I       iSpan           Averaging Span    
        //              ----------------------------------------------------------
        //
        // Return:      0: Success     Others: Failure
        //
        // Note:        Averaging span must be positive.
        //              Averaging span must be odd.  If even value is specified,
        //              it is decremented.
        //
        public static int MovingAverageFilter(ref double[] daIn, int iSpan)
        {
            int iLength = daIn.Length;
            int iLeft, iRight, iRealSpan;
            int iHalfSpan;
            double dSum;
            double[] daTmp = new double[iLength];

            //
            // Span value must be odd.
            //
            if ((iSpan & 0x1) == 0) iSpan -= 1;

            //
            // Span value must be positive
            //
            if (iSpan <= 0) return -1;

            iHalfSpan = (iSpan - 1) / 2;
            for ( int i = 0 ; i < iLength ; i++ )
            {
                iLeft = (i - iHalfSpan) > 0 ? iHalfSpan : i;
                iRight = (i + iHalfSpan) > (iLength - 1) ? (iLength - 1 - i) : iHalfSpan;
                iRealSpan = System.Math.Min(iLeft, iRight);

                dSum = 0;
                for ( int j = i - iRealSpan ; j <= i + iRealSpan ; j++ )
                {
                    dSum += daIn[j];
                }
                daTmp[i] = dSum / ((iRealSpan * 2) + 1);
            }

            daTmp.CopyTo(daIn, 0);

            return 0;
        }
    }
}
