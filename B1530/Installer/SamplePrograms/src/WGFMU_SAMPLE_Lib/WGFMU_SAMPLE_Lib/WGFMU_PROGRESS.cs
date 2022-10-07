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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WGFMU_SAMPLE_Lib
{
    public partial class WGFMU_PROGRESS : Form
    {

        private int AbortStat;

        public WGFMU_PROGRESS()
        {
            InitializeComponent();
        }

        #region Property

        public double ExpectedExcecutionTime
        {
            set { this.ExpectedTime.Text = value.ToString();  }
        }

        public double ElapsedTime
        {
            set { this.ActualElapsedTime.Text = value.ToString(); }
        }

        public int AbortStatus
        {
            get { return AbortStat; }
        }
        
        
        #endregion

        private void PROGRESS_Load(object sender, EventArgs e)
        {
            ActualElapsedTime.Text = "0.0";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            AbortStat = 1;
        }
    }
}