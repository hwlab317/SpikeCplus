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


using System;

namespace WGFMU_SAMPLE_Lib
{
    public class WGFMULibException : System.ApplicationException
    {

        private int status;
        private string errmsg;

        #region property

        public int StatusCode { get { return status; } }
        public override string Message { get { return errmsg; } }

        #endregion

        #region Constructor
        public WGFMULibException(int Status, string ErrorMessage)
        {
            this.status = Status;
            this.errmsg = ErrorMessage;
        }
        #endregion

        #region Method
        public void Clear()
        {
            status = 0;
            errmsg = "";
        }
        #endregion
    }
}
