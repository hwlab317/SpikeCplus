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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Draw;

namespace RTSDataAnalysis
{
    public partial class DialogGraph : Form
    {
        public DrawGraph dg;
        private FormWindowState _fws;
        private bool _bAllLine;
        private bool _bLeftDown;
        private int _iSelectStart;
        private int _iSelectEnd;

        public DialogGraph()
        {
            InitializeComponent();
            dg = new DrawGraph(pnGraph, pbGraph);
            _fws = WindowState;
            _bAllLine = false;
            _bLeftDown = false;
        }

        private void DialogGraph_Load(object sender, EventArgs e)
        {
            dg.ResizeComponent();

            _bAllLine = ((dg.GetNumberOfGraph() > 0) ? true : false);
            for ( int i = 0 ; i < dg.GetNumberOfGraph() ; i++ )
            {
                if (dg.GetGraphType(i) != GraphType.LINE)
                {
                    _bAllLine = false;
                }
            }
            cmbRange.Enabled = _bAllLine;
            btnZoom.Enabled = (cmbRange.Text != "");
            btnReset.Enabled = _bAllLine;
        }

        private void DialogGraph_ResizeEnd(object sender, EventArgs e)
        {
            dg.ResizeComponent();
        }

        private void DialogGraph_Resize(object sender, EventArgs e)
        {
            if ((WindowState != _fws) && (WindowState != FormWindowState.Minimized))
            {
                dg.ResizeComponent();
            }
            _fws = WindowState;
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            string sTrimmed = cmbRange.Text.Trim();

            //
            // Magnify with specified range.
            //

            //
            // Syntax Check
            //
            double dRangeStart, dRangeEnd;
            int iPos;
            iPos = sTrimmed.IndexOf(",");
            if (iPos < 0)
            {
                MessageBox.Show("Syntax error");
            }
            try
            {
                string sStart = sTrimmed.Substring(0, iPos);
                string sEnd = sTrimmed.Substring(iPos + 1, sTrimmed.Length - iPos - 1);
                dRangeStart = Convert.ToDouble(sStart);
                dRangeEnd = Convert.ToDouble(sEnd);

                //
                // Add it as item string
                //
                if (cmbRange.Items.Contains(sTrimmed) == false)
                {
                    cmbRange.Items.Insert(0, sTrimmed);
                }

                //
                // Update graphs
                //
                dg.SetRange(dRangeStart, dRangeEnd);
                dg.RedrawGraph();
            }
            catch(Exception /* e */)
            {
                MessageBox.Show("Syntax error");
            }
        }

        private void cmbRange_TextChanged(object sender, EventArgs e)
        {
            btnZoom.Enabled = (cmbRange.Text != "");
        }

        private void cmbRange_TextUpdate(object sender, EventArgs e)
        {
            btnZoom.Enabled = (cmbRange.Text != "");
        }

        private void pbGraph_MouseDown(object sender, MouseEventArgs e)
        {
            if (_bAllLine)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if ((Control.ModifierKeys & Keys.Shift) != 0)
                    {
                        _iSelectEnd = e.X;
                    }
                    else
                    {
                        _iSelectStart = e.X;
                        _iSelectEnd = e.X;
                    }
                    _bLeftDown = true;

                    double dMin = 0;
                    double dMax = 0;
                    int _iSelectMin = System.Math.Min(_iSelectStart, _iSelectEnd);
                    int _iSelectMax = System.Math.Max(_iSelectStart, _iSelectEnd);
                    dg.PixelToValue(_iSelectMin, _iSelectMax, ref dMin, ref dMax);
                    cmbRange.Text = string.Format("{0,0:E7}", dMin) + ", " + string.Format("{0,0:E7}", dMax);
                    dg.Invert(_iSelectMin, _iSelectMax);
                }
            }
        }

        private void pbGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bAllLine)
            {
                if (_bLeftDown == true)
                {
                    _iSelectEnd = e.X;

                    double dMin = 0;
                    double dMax = 0;
                    int _iSelectMin = System.Math.Min(_iSelectStart, _iSelectEnd);
                    int _iSelectMax = System.Math.Max(_iSelectStart, _iSelectEnd);
                    dg.PixelToValue(_iSelectMin, _iSelectMax, ref dMin, ref dMax);
                    cmbRange.Text = string.Format("{0,0:E7}", dMin) + ", " + string.Format("{0,0:E7}", dMax);
                    dg.Invert(_iSelectMin, _iSelectMax);
                }
            }
        }

        private void pbGraph_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _bLeftDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                //
                // Cancel selection and redraw.
                //
                dg.ResizeComponent();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //
            // Show in full scale.
            //
            dg.ResetRange();
            dg.RedrawGraph();
        }
    }
}
