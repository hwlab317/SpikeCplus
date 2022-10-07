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
using System.Text;

namespace FileReader
{
    public class CSVReader
    {
        private string _sFileName;

        //
        // Function:    CSVReader
        //
        // Description: <Constructor>
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       sFileName       CSV Filename
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public CSVReader(string sFileName)
        {
            _sFileName = sFileName;
        }

        //
        // Function:    read
        //
        // Description: <Constructor>
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              O       daTime          Sampling Time Array
        //              O       daCurrent       Sampling Current Array
        //              ----------------------------------------------------------
        //
        // Return:      Number of samples  (-1: Format Error)
        //
        // Note:        Maximum number of samples is 0x400000 (4 Mega).
        //              Data values indexed over 0x400000 are ignored.
        //
        public int read(ref double[] daTime, ref double[] daCurrent)
        {
            int iCount;
            int iOffset = 0;
            const int iSize = 0x100000;
            int iPos = 0;
            int iIndexCR;
            char[] caBuffer = new char[iSize]; 
            String s;
            int iDataCount = 0;
            int iIndex;

            //
            // Clear the arrays
            //
            Array.Resize<double>(ref daTime, 0x400000);
            Array.Resize<double>(ref daCurrent, 0x400000);

            try
            {
                TextReader tr = new StreamReader(_sFileName);
                FileInfo fi = new FileInfo(_sFileName);
                int iFileLength = (int)fi.Length;
                int iRemainder = iFileLength;

                while((iCount = tr.ReadBlock(caBuffer, iOffset, iSize - iOffset)) > 0)
                {
                    Array.Resize<char>(ref caBuffer, iOffset + iCount);
                    iRemainder -= iCount;

                    iPos = 0;
                    while((iIndexCR = Array.IndexOf(caBuffer, '\r', iPos)) > 0)
                    {
                        s = new string(caBuffer, iPos, iIndexCR - iPos);
                        if ((iIndex = s.IndexOf(',', 0)) > 0)
                        {
                            daTime[iDataCount] = Convert.ToDouble(s.Substring(0, iIndex));
                            daCurrent[iDataCount] = Convert.ToDouble(s.Substring(iIndex + 1, s.Length - iIndex - 1));
                            if (++iDataCount >= 0x400000)
                            {
                                break;
                            }
                        }

                        iPos = iIndexCR + 2;
                        if ((iRemainder > 0) && ((caBuffer.Length - iPos) < 1024))
                        {
                            break;
                        }
                    }

                    if (iDataCount >= 0x400000)
                    {
                        break;
                    }

                    iOffset = iCount + iOffset - iPos;
                    if (iOffset > 0)
                    {
                        Array.Copy(caBuffer, iPos, caBuffer, 0, iOffset);
                    }
                    Array.Resize<char>(ref caBuffer, iSize);
                }
                tr.Close();

                //
                // Re-dimension
                //
                Array.Resize<double>(ref daTime, iDataCount);
                Array.Resize<double>(ref daCurrent, iDataCount);
            }
            catch (Exception /* e */)
            {
                return -1;
            }

            return iDataCount;
        }
    }
}

