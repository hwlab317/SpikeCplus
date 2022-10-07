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

// WGFMU_CONFIG.cs Rev.A.01.00.2008-10-23

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WGFMU_SAMPLE_Lib
{
    public partial class WGFMU_CONFIG : Form
    {
        WGFMULib wglib = new WGFMULib();
        string ConfigFileName = Application.StartupPath + @"\wgfmuconfig.cf";

        public string VISANAME
        {
            get { return this.VisaId.Text; }
        }

        public int GPIBADDRESS
        {
            get { return int.Parse(this.GpibAddress.Text); }
        }

        public WGFMU_CONFIG()
        {
            InitializeComponent();
            if (System.IO.File.Exists(ConfigFileName))
            {
                wglib.RestoreSettings(this, "CONFIG", ConfigFileName);

            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            wglib.SaveSettings(this, "CONFIG", ConfigFileName);
            this.Close();
        }

        private void WGFMU_CONFIG_Load(object sender, EventArgs e)
        {
            this.MessageList.Items.Clear();

        }

        private void buttonInitialize_Click(object sender, EventArgs e)
        {
            int i;
            try
            {
                wglib.WgfmuVisaId = this.VisaId.Text;
                wglib.WgfmuGpibAddress = int.Parse(this.GpibAddress.Text);
                wglib.WgfmuOnline = 1;    
                wglib.Init();
                
                this.MessageList.Items.Clear();

                this.MessageList.Items.Add("WGFMU Available Channels");

                for (i = 0; i < wglib.WgfmuChannelList.Length; i++)
                {
                    this.MessageList.Items.Add(wglib.WgfmuChannelList[i].ToString());
                }

                this.MessageList.Items.Add("");
                this.MessageList.Items.Add("UNT? Response");

                for (i = 1; i <= wglib.WgfmuModuleList.Length; i++)
                { 
                    this.MessageList.Items.Add("Slot" + i.ToString() + " : " + wglib.WgfmuModuleList[i-1]);
                }
                wglib.Close();
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
                wglib.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
                wglib.Close();
            }
        }

        private void WGFMU_CONFIG_FormClosing(object sender, FormClosingEventArgs e)
        {
            wglib.SaveSettings(this, "CONFIG", ConfigFileName);
        }

        private void buttonSelfCal_Click(object sender, EventArgs e)
        {  
            int Result = 0;
            int Size = 256;
            string Message = "";

            try
            {
                wglib.WgfmuVisaId = this.VisaId.Text;
                wglib.WgfmuGpibAddress = int.Parse(this.GpibAddress.Text);
                wglib.WgfmuOnline = 1;
                wglib.Init();

                this.MessageList.Items.Clear();

                this.MessageList.Items.Add("Executing self calibration");

                Application.DoEvents();

                wglib.SelfCalibration(ref Result, out Message, ref Size);


                this.MessageList.Items.Add("");
                if (Result == 0)
                {
                    this.MessageList.Items.Add("PASSED");
                }
                else
                {
                    this.MessageList.Items.Add("Result: " + Result.ToString());
                    this.MessageList.Items.Add(Message);
                }
                wglib.Close();
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
                wglib.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
                wglib.Close();
            }
        }

        private void buttonSelfTest_Click(object sender, EventArgs e)
        {
            int Result = 0;
            int Size = 256;
            string Message = "";

            try
            {
                wglib.WgfmuVisaId = this.VisaId.Text;
                wglib.WgfmuGpibAddress = int.Parse(this.GpibAddress.Text);
                wglib.WgfmuOnline = 1;
                wglib.Init();

                this.MessageList.Items.Clear();

                this.MessageList.Items.Add("Executing self test");

                Application.DoEvents();

                wglib.SelfTest(ref Result, out Message, ref Size);


                this.MessageList.Items.Add("");
                if (Result == 0)
                {
                    this.MessageList.Items.Add("PASSED");
                }
                else
                {
                    this.MessageList.Items.Add("FAILED");
                    this.MessageList.Items.Add("Result: " + Result.ToString());
                    this.MessageList.Items.Add(Message);
                }
                wglib.Close();
            }
            catch (WGFMULibException we)
            {
                MessageBox.Show(we.StatusCode.ToString() + ":" + we.Message);
                we.Clear();
                wglib.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.GetType().FullName + ":" + ex.Message);
                ex.Message.Remove(0);
                wglib.Close();
            }
        }
    }
}