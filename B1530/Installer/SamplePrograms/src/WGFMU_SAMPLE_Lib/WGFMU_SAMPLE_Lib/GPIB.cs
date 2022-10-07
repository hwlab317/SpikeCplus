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
using System.Collections.Generic;
using System.Text;

namespace WGFMU_SAMPLE_Lib
{
    class GPIB
    {
        private int vi;
		private int viDefaultRM;
        private int errorStatus;
        private int ErrNum;
        private string ErrMsg;
		public GPIB()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

        public void Open(string GpibDescription)
        {
            //open GPIB connection
            errorStatus = visa32.viOpenDefaultRM(out viDefaultRM);
            errorStatus = visa32.viOpen(viDefaultRM, GpibDescription, 0, 0, out vi);        
        }

        public int Close()
        {
            int errorStatus = 0;

            //open GPIB connection

            errorStatus = visa32.viClose(vi);
            errorStatus = visa32.viClose(viDefaultRM);

            return 0;

        }

		#region ErrorCheck
		// Read out Error
		public int ErrorCheck()
		{

			string Cmmd;
			int errorStatus = 0;

			System.Text.StringBuilder msg = new System.Text.StringBuilder(2048);

			try
			{
				Cmmd = "ERRX?";
				errorStatus = visa32.viPrintf(vi, Cmmd + "\n");
				if (errorStatus < 0) throw ( new VisaException("VISA error"));

				errorStatus = visa32.viScanf(vi, "%d,%t", ref this.ErrNum,  msg);
				if (errorStatus < 0) throw ( new VisaException( "VISA error"));

				//SplitBuffer = Buffer.Split(',');
				//this.ErrNum = int.Parse(SplitBuffer[0]);
				//this.ErrMsg = SplitBuffer[1];
				this.ErrMsg = msg.ToString();
				if(ErrNum != 0) throw (new WGFMULibException(-1, ErrMsg));

				return errorStatus;
			}
			catch( VisaException)
			{
				this.ErrMsg = "VISA ERROR";
				this.ErrNum = -1;
				throw (new WGFMULibException(-1,"VISA Error in Error Check"));
			}
			catch( WGFMULibException tex)
			{
				throw (new WGFMULibException(-1, "Error:" + tex.Message));
			}
		}

		#endregion
	

		#region sendCmd
		//Send String Commands
		public int sendCmd(string Cmmd)
		{

			int errorStatus = 0;

			try
			{
				errorStatus = visa32.viPrintf(vi, Cmmd + "\n");
				if (errorStatus < 0) throw ( new VisaException("VISA error"));

				return 0;
			}
			catch( VisaException)
			{
					this.ErrNum = errorStatus;
				this.ErrMsg = "VISA ERROR";
				return -1;
			}
		}
		#endregion

		#region sendCmdE
		//Send String Commands
		public int sendCmdE(string Cmmd)
		{

			int errorStatus = 0;

			try
			{
				errorStatus = visa32.viPrintf(vi, Cmmd + "\n");
				if (errorStatus < 0) throw ( new VisaException("VISA error"));

				this.ErrorCheck();

				return 0;
			}
			catch( VisaException)
			{
				this.ErrNum = errorStatus;
				this.ErrMsg = "VISA ERROR";
				return -1;
			}
		}
		#endregion

		#region sendBynary
		//Send String Commands
		public int sendBynary(ref byte[] data, int count)
		{

			int errorStatus = 0;
			int retCount = 0;

			try
			{
				errorStatus = visa32.viWrite(vi, data, count, out retCount);
				if (errorStatus < 0) throw ( new VisaException("VISA error"));

				return 0;
			}
			catch( VisaException)
			{
				return -1;
			}
		}
		#endregion

		#region queryStr
		// Readout the ouptut buffer as a string

		public int queryStr(out string Response)
		{

			int errorStatus = 0;
			Response = "";

			System.Text.StringBuilder msg = new System.Text.StringBuilder(2048);

			try
			{
				errorStatus = visa32.viScanf(vi, "%t", msg);
				if (errorStatus < 0) throw ( new VisaException("VISA error"));
				Response = msg.ToString();
				
				return 0;
			}
			catch( VisaException)
			{
				return errorStatus;
			}
		}
		#endregion

		#region ViRead()
		public int	ViRead(int Count, out string Value, out int RetCount)
		{

			int errorStatus = 0;
			RetCount = 0;
			byte[] buf = new byte[Count];
			Value = "";
			try
			{
				System.Text.StringBuilder msg = new System.Text.StringBuilder(Count);
				errorStatus = visa32.viRead(vi, buf, Count, out RetCount);
				if (errorStatus < 0) throw ( new VisaException("VISA error"));

				Value = Encoding.ASCII.GetString(buf);

				return errorStatus;
			}
			catch( VisaException)
			{
				return errorStatus;
			}
		}
		#endregion

		#region readStb
		public int readStb(out short Status)
		{
			Status = 0;
			int rc;
			rc = visa32.viReadSTB(vi, ref Status);
			return rc;
		}

		
		#endregion

		#region waitOpc
		// Send "*OPC?" query and wait it's response

		public int waitOpc()
		{

			string Cmmd;
			int errorStatus = 0;

			System.Text.StringBuilder msg = new System.Text.StringBuilder(2048);

			try
			{
				Cmmd = "*OPC?";
				errorStatus = visa32.viPrintf(vi, Cmmd + "\n");
				errorStatus = visa32.viScanf(vi, "%2048t", msg);
			
				if (errorStatus < 0) throw ( new VisaException("VISA error"));

				return 0;
			}
			catch( VisaException)
			{
				return errorStatus;
			}
		}
		#endregion

       
    }

    
}
