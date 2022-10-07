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
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Draw
{
    public enum GraphType { UNKNOWN, LINE, HISTOGRAM, PLOT };
    public enum ScaleType { UNKNOWN, LINEAR, LOG };

    public class DrawGraph
    {
        private Panel _pn;
        private PictureBox _pb;
        private ScaleType _stX, _stY;

        private GraphType[] _gta;
        private double[][] _daaX, _daaY;
        private Color[] _caColor;
        private string[] _saMark;
        private bool[] _baVisible;
        private bool[] _baLegend;
        private string[] _saLegend;

        private double _dXmin, _dXmax;
        private double _dYmin, _dYmax;
        private double _dExpX, _dExpY;
        private int _iScaleX, _iScaleY;

        private string _sTitle = "This is a graph title string.";
        private string _sLabelX = "This is a X axis label string.";
        private string _sLabelY = "This is a Y axis label string.";

        private bool _bUseRange;
        private double _dRangeStart, _dRangeEnd;

        private bool _bInverted;
        private int _iXs, _iXe;
        private bool _bInvertOddLine;

        struct CompressedItem
        {
            public int iNumber;
            public double dStart;
            public double dEnd;
            public double dMin;
            public double dMax;
        }

        //
        // Function:    DrawGraph
        //
        // Description: <Constructor>
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       pn              Panel Object
        //              I       pb              PictureBox Object
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        // Note:        PictureBox(pb) object must be a child of PanelObject(pn).
        //
        public DrawGraph(Panel pn, PictureBox pb)
        {
            _pn = pn;
            _pb = pb;
            _stX = ScaleType.LINEAR;
            _stY = ScaleType.LINEAR;
            _dXmin = _dXmax = 0.0;
            _dYmin = _dYmax = 0.0;

            _gta = new GraphType[0];
            _daaX = new double[0][];
            _daaY = new double[0][];
            _caColor = new Color[0];
            _saMark = new string[0];
            _baVisible = new bool[0];
            _baLegend = new bool[0];
            _saLegend = new string[0];

            _bUseRange = false;
            _bInverted = false;
        }

        //
        // Function:    CopyFrom
        //
        // Description: Copy all data except for Panel and PictureBox objects.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       dg              DrawGraph Object
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public void CopyFrom(DrawGraph dg)
        {
            this._stX = dg._stX;
            this._stY = dg._stY;

            this._gta = new GraphType[dg._gta.Length];
            dg._gta.CopyTo(this._gta, 0);

            this._daaX = new double[dg._daaX.Length][];
            for ( int i = 0 ; i < dg._daaX.Length ; i++ )
            {
                this._daaX[i] = new double[dg._daaX[i].Length];
                dg._daaX[i].CopyTo(this._daaX[i], 0);
            }
            this._daaY = new double[dg._daaY.Length][];
            for ( int i = 0 ; i < dg._daaY.Length ; i++ )
            {
                this._daaY[i] = new double[dg._daaY[i].Length];
                dg._daaY[i].CopyTo(this._daaY[i], 0);
            }

            this._caColor = new Color[dg._caColor.Length];
            dg._caColor.CopyTo(this._caColor, 0);

            this._saMark = new string[dg._saMark.Length];
            for ( int i = 0 ; i < dg._saMark.Length ; i++ )
            {
                this._saMark[i] = dg._saMark[i];
            }

            this._baVisible = new bool[dg._baVisible.Length];
            dg._baVisible.CopyTo(this._baVisible, 0);

            this._baLegend = new bool[dg._baLegend.Length];
            dg._baLegend.CopyTo(this._baLegend, 0);

            this._saLegend = new string[dg._saLegend.Length];
            for ( int i = 0 ; i < dg._saLegend.Length ; i++ )
            {
                this._saLegend[i] = dg._saLegend[i];
            }

            this._dXmin = dg._dXmin;
            this._dXmax = dg._dXmax;
            this._dYmin = dg._dYmin;
            this._dYmax = dg._dYmax;
            this._dExpX = dg._dExpX;
            this._dExpY = dg._dExpY;
            this._iScaleX = dg._iScaleX;
            this._iScaleY = dg._iScaleY;

            this._bUseRange = dg._bUseRange;
            this._dRangeStart = dg._dRangeStart;
            this._dRangeEnd = dg._dRangeEnd;

            this._bInverted = dg._bInverted;
            this._iXs = dg._iXe;
            this._iXe = dg._iXe;

            this._sTitle = dg._sTitle;
            this._sLabelX = dg._sLabelX;
            this._sLabelY = dg._sLabelY;
        }

        //
        // Function:    SetScaleType
        //
        // Description: Set the scale type for each axis.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       stX             X Scale Type(LINEAR or LOG)
        //              I       stY             Y Scale Type(LINEAR or LOG)
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public void SetScaleType(ScaleType stX, ScaleType stY)
        {
            _stX = stX;
            _stY = stY;
        }

        //
        // Function:    AddData
        //
        // Description: Add X/Y Data.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       gt              Graph Type(LINE, HISTOGRAM, or PLOT)
        //              I       daX             X Data Array
        //              I       daY             Y Data Array
        //              I       cColor          Color
        //              I       sMark           Mark (Only for PLOT)
        //              I       bVisible        All data are visible or not.
        //              I       bLegend         Show legend or not.
        //              I       sLegend         Legend string.
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public void AddData(GraphType gt, double[] daX, double[] daY, Color cColor, string sMark, bool bVisible, bool bLegend, string sLegend)
        {
            Array.Resize<GraphType>(ref _gta, _gta.Length + 1);
            Array.Resize<double []>(ref _daaX, _daaX.Length + 1);
            Array.Resize<double []>(ref _daaY, _daaY.Length + 1);
            Array.Resize<Color>(ref _caColor, _caColor.Length + 1);
            Array.Resize<string>(ref _saMark, _saMark.Length + 1);
            Array.Resize<bool>(ref _baVisible, _baVisible.Length + 1);
            Array.Resize<bool>(ref _baLegend, _baLegend.Length + 1);
            Array.Resize<string>(ref _saLegend, _saLegend.Length + 1);

            _gta[_gta.Length - 1] = gt;
            _daaX[_daaX.Length - 1] = daX;
            _daaY[_daaY.Length - 1] = daY;
            _caColor[_caColor.Length - 1] = cColor;
            _saMark[_saMark.Length - 1] = sMark;
            _baVisible[_baVisible.Length - 1] = bVisible;
            _baLegend[_baLegend.Length - 1] = bLegend;
            _saLegend[_saLegend.Length - 1] = sLegend;
        }

        //
        // Function:    ClearData
        //
        // Description: Clear X/Y Data.
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        public void ClearData()
        {
            Array.Resize<GraphType>(ref _gta, 0);
            Array.Resize<double []>(ref _daaX, 0);
            Array.Resize<double []>(ref _daaY, 0);
            Array.Resize<Color>(ref _caColor, 0);
            Array.Resize<string>(ref _saMark, 0);
            Array.Resize<bool>(ref _baVisible, 0);
            Array.Resize<bool>(ref _baLegend, 0);
            Array.Resize<string>(ref _saLegend, 0);
        }

        //
        // Function:    ResizeComponent
        //
        // Description: Resize PictureBox(Plot Area) according to its parent's size.
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        public void ResizeComponent()
        {
            int iGraphLeft = (int)((double)_pn.Size.Width * 0.05);
            int iGraphTop = (int)((double)_pn.Size.Height * 0.05);
            int iGraphWidth = (int)((double)_pn.Size.Width * 0.9);
            int iGraphHeight = (int)((double)_pn.Size.Height * 0.9);
            _pb.SetBounds(iGraphLeft, iGraphTop, iGraphWidth, iGraphHeight);

            _pb.Image = new Bitmap(System.Math.Max(iGraphWidth, 1), System.Math.Max(iGraphHeight, 1));

            RedrawGraph();
        }

        //
        // Function:    SetTitle
        //
        // Description: Set graph title string.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       s               Graph Title
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public void SetTitle(string s)
        {
            _sTitle = s;
        }

        //
        // Function:    RedrawTitle
        //
        // Description: Draw graph title to "Top-Middle" of the PictureBox.
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        private void RedrawTitle(Graphics g, Size s)
        {
            //
            // Calculate height
            //
            int iHeight = (int)((double)s.Height * 0.05 * 0.5);

            //
            // Create font and brush
            //
            Font fFont = new Font("Arial", System.Math.Max(iHeight, 1));
            SolidBrush sBrush = new SolidBrush(Color.Black);

            //
            // Calculate string length
            //
            SizeF sSizeString = g.MeasureString(_sTitle, fFont);

            //
            // Calculate string position
            //
            float fPosX = (float)s.Width * 0.5f - sSizeString.Width * 0.5f;
            float fPosY = (float)s.Height * 0.0125f;

            //
            // Draw the title
            //
            g.DrawString(_sTitle, fFont, sBrush, fPosX, fPosY);

            //
            // Clean up
            //
            sBrush.Dispose();
            fFont.Dispose();
        }

        //
        // Function:    SetAxisLabel
        //
        // Description: Set X/Y axis labels.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       sX              X-axis Label
        //              I       sY              Y-axis Label
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public void SetAxisLabel(string sX, string sY)
        {
            _sLabelX = sX;
            _sLabelY = sY;
        }

        //
        // Function:    RedrawAxisLabel
        //
        // Description: Draw axis labels to "Left-Middle"/"Bottom-Middle" of the PictureBox.
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        private void RedrawAxisLabel(Graphics g, Size s)
        {
            //
            // Calculate height (X/Y common)
            //
            int iHeight = (int)((double)s.Height * 0.05 * 0.5);

            //
            // Create font and brush (X/Y common)
            //
            Font fFont = new Font("Arial", System.Math.Max(iHeight, 1));
            SolidBrush sBrush = new SolidBrush(Color.Black);

            //
            // X Axis Label
            //

            //
            // Calculate string length
            //
            SizeF sSizeString = g.MeasureString(_sLabelX, fFont);

            //
            // Calculate string position
            //
            float fPosX = (float)s.Width * 0.5f - sSizeString.Width * 0.5f;
            float fPosY = (float)s.Height * (1.0f - 0.0375f);

            //
            // Draw the label
            //
            g.DrawString(_sLabelX, fFont, sBrush, fPosX, fPosY);

            //
            // Y Axis Label
            //
            
            //
            // Calculate string length
            //
            sSizeString = g.MeasureString(_sLabelY, fFont);

            //
            // Calculate string position
            //
            fPosX = (float)s.Width * 0.0125f;
            fPosY = (float)s.Height * 0.5f + sSizeString.Width * 0.5f;

            //
            // Rotate
            //
            g.TranslateTransform(fPosX, fPosY);
            g.RotateTransform(-90.0f);

            //
            // Draw the label
            //
            g.DrawString(_sLabelY, fFont, sBrush, 0, 0);

            //
            // Reset
            //
            g.ResetTransform();
            g.RotateTransform(0.0f);

            //
            // Clean up
            //
            sBrush.Dispose();
            fFont.Dispose();
        }

        //
        // Function:    RedrawFrame
        //
        // Description: Draw a frame of the plot area.
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        private void RedrawFrame(Graphics g, Rectangle r)
        {
            Pen pBlack = new Pen(Color.Black, 1);
            g.DrawRectangle(pBlack, r);
            pBlack.Dispose();
        }

        //
        // Function:    PrepareScaleX
        //
        // Description: Calculate scaling parameters for X-axis.
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        private void PrepareScaleX()
        {
            double dmin, dmax;
            double dExpMin, dExpMax;

            //
            // Initialize
            //
            _dXmin = _dXmax = 0.0;
            dExpMin =  dExpMax = _dExpX = 0.0;

            //
            // Find min/max value for X axis
            //
            dmin = Double.MaxValue;
            dmax = Double.MinValue;
            for ( int i = 0 ; i < _daaX.Length ; i++ )
            {
                if (_baVisible[i] == true)
                {
                    for ( int j = 0 ; j < _daaX[i].Length ; j++ )
                    {
                       if (_daaX[i][j] < dmin) dmin = _daaX[i][j];
                       if (_daaX[i][j] > dmax) dmax = _daaX[i][j];
                    }
                }
            }

            //
            // If zooming is enabled, set its range.
            //
            if (_bUseRange)
            {
                dmin = _dRangeStart;
                dmax = _dRangeEnd;
            }

            //
            // Find at least one histogram exists.
            //
            bool bHistogram = false;
            for ( int i = 0 ; i < _gta.Length ; i++ )
            {
                if (_gta[i] == GraphType.HISTOGRAM)
                {
                    bHistogram = true;
                    break;
                }
            }

            if (bHistogram)
            {
                //
                // Get exponent values
                //
                double dExpDiff = 0.0;
                double ddiff = dmax - dmin;
                if (ddiff > Double.Epsilon)
                {
                    dExpDiff = System.Math.Log10(ddiff);
                    dExpDiff = System.Math.Floor(dExpDiff);
                }
                _dXmin = System.Math.Floor(dmin * System.Math.Pow(10, -dExpDiff));
                _dXmin *= System.Math.Pow(10, dExpDiff);
                _dXmax = System.Math.Ceiling(dmax * System.Math.Pow(10, -dExpDiff));
                _dXmax *= System.Math.Pow(10, dExpDiff);

                if (System.Math.Abs(dmin) > Double.Epsilon)
                {
                    dExpMin = System.Math.Log10(System.Math.Abs(dmin));
                    dExpMin = System.Math.Floor(dExpMin);
                }

                if (System.Math.Abs(dmax) > Double.Epsilon)
                {
                    dExpMax = System.Math.Log10(System.Math.Abs(dmax));
                    dExpMax = System.Math.Floor(dExpMax);
                }

                _dExpX = (dExpMax > dExpMin ? dExpMax : dExpMin);
                _iScaleX = 5;
            }
            else if (_stX == ScaleType.LINEAR)
            {
                double dDigitMin, dDigitMax;
                double dExpDiff;
                const double dExpInfinite = -12.0;

                //
                // Get exponent values
                //
                dExpMin = dExpMax = dExpDiff = dExpInfinite - 1;
                if (System.Math.Abs(dmin) > Double.Epsilon)
                {
                    dExpMin = System.Math.Log10(System.Math.Abs(dmin));
                    dExpMin = System.Math.Floor(dExpMin);
                }
                if (System.Math.Abs(dmax) > Double.Epsilon)
                {
                    dExpMax = System.Math.Log10(System.Math.Abs(dmax));
                    dExpMax = System.Math.Floor(dExpMax);
                }
                if (System.Math.Abs(dmax - dmin) > Double.Epsilon)
                {
                    dExpDiff = System.Math.Log10(System.Math.Abs(dmax - dmin));
                    dExpDiff = System.Math.Floor(dExpDiff);
                }

                //
                // Calculate minimum/maximum values
                //
                if (dExpMin < dExpInfinite)
                {
                    _dXmin = 0.0;
                }
                else
                {
                    _dXmin = (int)(dmin / System.Math.Pow(10, dExpMin));
                    _dXmin *= System.Math.Pow(10, dExpMin);
                }
                if (dExpMax < dExpInfinite)
                {
                    _dXmax = 0.0;
                }
                else
                {
                    _dXmax = (int)(dmax / System.Math.Pow(10, dExpMax));
                    _dXmax *= System.Math.Pow(10, dExpMax);
                }
                int iRoundDigit = (int)System.Math.Abs(dExpDiff) + 1;
                _dXmin = System.Math.Round(_dXmin, iRoundDigit);
                _dXmax = System.Math.Round(_dXmax, iRoundDigit);

                //
                // Round minimum/maximum values by the exponent.
                //
                if (dExpDiff > dExpInfinite)
                {
                    bool bShift = false;
                    if ((dmin - _dXmin) > Double.Epsilon)
                    {
                        _dXmin = dmin + System.Math.Pow(10, dExpDiff - 1) * 0.5;
                        _dXmin /= System.Math.Pow(10, dExpDiff - 1);
                        _dXmin = System.Math.Round(_dXmin);
                        _dXmin *= System.Math.Pow(10, dExpDiff - 1);
                        bShift = true;
                    }
                    if ((_dXmin - dmin) > Double.Epsilon)
                    {
                        _dXmin = dmin - System.Math.Pow(10, dExpDiff - 1) * 0.5;
                        _dXmin /= System.Math.Pow(10, dExpDiff - 1);
                        _dXmin = System.Math.Round(_dXmin);
                        _dXmin *= System.Math.Pow(10, dExpDiff - 1);
                        bShift = true;
                    }
                    if ((_dXmax - dmax) > Double.Epsilon)
                    {
                        _dXmax = dmax - System.Math.Pow(10, dExpDiff - 1) * 0.5;
                        _dXmax /= System.Math.Pow(10, dExpDiff - 1);
                        _dXmax = System.Math.Round(_dXmax);
                        _dXmax *= System.Math.Pow(10, dExpDiff - 1);
                        bShift = true;
                    }
                    if ((dmax - _dXmax) > Double.Epsilon)
                    {
                        _dXmax = dmax + System.Math.Pow(10, dExpDiff - 1) * 0.5;
                        _dXmax /= System.Math.Pow(10, dExpDiff - 1);
                        _dXmax = System.Math.Round(_dXmax);
                        _dXmax *= System.Math.Pow(10, dExpDiff - 1);
                        bShift = true;
                    }
                    if (bShift)
                    {
                        dExpDiff -= 1.0;
                    }

                    //
                    // Calculate digits' range
                    //
                    dDigitMin = System.Math.Round(_dXmin / System.Math.Pow(10, dExpDiff), iRoundDigit);
                    dDigitMax = System.Math.Round(_dXmax / System.Math.Pow(10, dExpDiff), iRoundDigit);

                    //
                    // Calculate number of scales
                    //
                    _iScaleX = (int)System.Math.Abs(dDigitMax - dDigitMin);

                    //
                    // Trimming number of scales to 10 * n.
                    //
                    if (_iScaleX <= 0) _iScaleX = 1;
                    if (_iScaleX == 1) _iScaleX = 10;
                    if (_iScaleX == 2) _iScaleX = 20;
                    if (_iScaleX == 3) _iScaleX = 30;
                    if (_iScaleX == 4) _iScaleX = 20;
                }
                else
                {
                    _iScaleX = 1;
                }
                _dExpX = (dExpMax > dExpMin ? dExpMax : dExpMin);
            }
            else
            {
                //
                // Get exponent values
                //
                if (System.Math.Abs(dmin) > Double.Epsilon)
                {
                    dExpMin = System.Math.Log10(System.Math.Abs(dmin));
                    dExpMin = System.Math.Floor(dExpMin);
                }

                if (System.Math.Abs(dmax) > Double.Epsilon)
                {
                    dExpMax = System.Math.Log10(System.Math.Abs(dmax));
                    dExpMax = System.Math.Ceiling(dExpMax);
                }

                _dXmin = dExpMin;
                _dXmax = dExpMax;
                _iScaleX = (int)(dExpMax - dExpMin);
            }
        }

        //
        // Function:    PrepareScaleY
        //
        // Description: Calculate scaling parameters for Y-axis.
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        public void PrepareScaleY()
        {
            double dmin, dmax;
            double dExpMin, dExpMax;

            //
            // Initialize
            //
            _dYmin = _dYmax = 0.0;
            dExpMin =  dExpMax = 0.0;

            //
            // Find min/max value for Y axis
            //
            dmin = Double.MaxValue;
            dmax = Double.MinValue;
            for ( int i = 0 ; i < _daaY.Length ; i++ )
            {
                if (_baVisible[i] == true)
                {
                    for ( int j = 0 ; j < _daaY[i].Length ; j++ )
                    {
                        if (_daaY[i][j] < dmin) dmin = _daaY[i][j];
                        if (_daaY[i][j] > dmax) dmax = _daaY[i][j];
                    }
                }
            }

            if (_stY == ScaleType.LINEAR)
            {
                //
                // Get exponent values
                //
                double dExpDiff = 0.0;
                double ddiff = dmax - dmin;
                if (_gta[0] != GraphType.HISTOGRAM)
                {
                    dmax += ddiff * 0.05;
                    dmin -= ddiff * 0.15;
                }
                if (ddiff > Double.Epsilon)
                {
                    dExpDiff = System.Math.Log10(ddiff);
                    dExpDiff = System.Math.Floor(dExpDiff);
                }
                _dYmin = System.Math.Floor(dmin * System.Math.Pow(10, -dExpDiff));
                _dYmin *= System.Math.Pow(10, dExpDiff);
                _dYmax = System.Math.Ceiling(dmax * System.Math.Pow(10, -dExpDiff));
                _dYmax *= System.Math.Pow(10, dExpDiff);

                if (System.Math.Abs(dmin) > Double.Epsilon)
                {
                    dExpMin = System.Math.Log10(System.Math.Abs(dmin));
                    dExpMin = System.Math.Floor(dExpMin);
                }

                if (System.Math.Abs(dmax) > Double.Epsilon)
                {
                    dExpMax = System.Math.Log10(System.Math.Abs(dmax));
                    dExpMax = System.Math.Floor(dExpMax);
                }

                _dExpY = (dExpMax > dExpMin ? dExpMax : dExpMin);
                _iScaleY = 5;
            }
            else
            {
                //
                // Get exponent values
                //
                if (System.Math.Abs(dmin) > Double.Epsilon)
                {
                    dExpMin = System.Math.Log10(System.Math.Abs(dmin));
                    dExpMin = System.Math.Floor(dExpMin);
                }

                if (System.Math.Abs(dmax) > Double.Epsilon)
                {
                    dExpMax = System.Math.Log10(System.Math.Abs(dmax));
                    dExpMax = System.Math.Ceiling(dExpMax);
                }

                _dYmin = dExpMin;
                _dYmax = dExpMax;
                _iScaleY = System.Math.Max((int)(dExpMax - dExpMin), 1);
            }
        }

        //
        // Function:    RedrawScaleLineX
        //
        // Description: Draw scale lines for X-axis.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       g               Graphics Object
        //              I       r               Rectangle of "Plot Area"
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        private void RedrawScaleLineX(Graphics g, Rectangle r)
        {
            int iXs, iXe, iYs, iYe;
            Pen pBlackDash = new Pen(Color.Black, 1);
            int iOffset;

            //
            // Draw scale lines for X axis
            //
            if (_stX == ScaleType.LINEAR)
            {
                pBlackDash.DashStyle = DashStyle.Dash;
            }
            else
            {
            }

            for ( int i = 1 ; i < _iScaleX ; i++ )
            {
                if ((_iScaleX <= 15) ||
                    ((_iScaleX <= 50) && ((i % ((r.Width > 360) ? 5 : 5)) == 0)) ||
                    ((_iScaleX <= 100) && ((i % ((r.Width > 360) ? 5 : 10)) == 0)) ||
                    ((_iScaleX <= 1000) && ((i % ((r.Width > 360) ? 50 : 100)) == 0)) ||
                    ((_iScaleX <= 10000) && ((i % ((r.Width > 360) ? 500 : 1000)) == 0)))
                {
                    iXs = r.Left + r.Width * i / _iScaleX;
                    iXe = iXs;
                    iYs = r.Top;
                    iYe = r.Bottom;
                    g.DrawLine(pBlackDash, iXs, iYs, iXe, iYe);
                }
            }

            //
            // Draw sub-scale lines for X axis
            //
            if (((r.Width / _iScaleX) > 50) && (_stX == ScaleType.LOG))
            {
                pBlackDash.DashStyle = DashStyle.Dash;
                iYs = r.Top;
                iYe = r.Bottom;
                for ( int i = 1 ; i <= _iScaleX ; i++ )
                {
                    iOffset = r.Width * i / _iScaleX;
                    for ( int j = 2 ; j <= 9 ; j++ )
                    {
                        iXs = r.Left + iOffset + (int)((System.Math.Log10(0.1 * j) * r.Width / _iScaleX) - 0.5);
                        iXe = iXs;
                        g.DrawLine(pBlackDash, iXs, iYs, iXe, iYe);
                    }
                }
            }

            pBlackDash.Dispose();
        }

        //
        // Function:    RedrawScaleLineY
        //
        // Description: Draw scale lines for Y-axis.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       g               Graphics Object
        //              I       r               Rectangle of "Plot Area"
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        private void RedrawScaleLineY(Graphics g, Rectangle r)
        {
            int iXs, iXe, iYs, iYe;
            Pen pBlackDash = new Pen(Color.Black, 1);
            int iOffset;

            //
            // Draw scale lines for Y axis
            //
            if (_stY == ScaleType.LINEAR)
            {
                pBlackDash.DashStyle = DashStyle.Dash;
            }
            else
            {
            }

            for ( int i = 1 ; i < _iScaleY ; i++ )
            {
                iXs = r.Left;
                iXe = r.Right;
                iYs = r.Bottom - r.Height / _iScaleY * i;
                iYe = iYs;
                g.DrawLine(pBlackDash, iXs, iYs, iXe, iYe);
            }

            //
            // Draw sub-scale lines for Y axis
            //
            if ((r.Height / _iScaleY > 50) && (_stY == ScaleType.LOG))
            {
                pBlackDash.DashStyle = DashStyle.Dash;
                iXs = r.Left;
                iXe = r.Right;
                for ( int i = 1 ; i <= _iScaleY ; i++ )
                {
                    iOffset = r.Height / _iScaleY * i;
                    for ( int j = 2 ; j <= 9 ; j++ )
                    {
                        iYs = r.Bottom - iOffset - (int)((System.Math.Log10(0.1 * j) * r.Height / _iScaleY) - 0.5);
                        iYe = iYs;
                        g.DrawLine(pBlackDash, iXs, iYs, iXe, iYe);
                    }
                }
            }

            pBlackDash.Dispose();
        }

        //
        // Function:    RedrawScaleX
        //
        // Description: Draw scale values for X-axis.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       g               Graphics Object
        //              I       s               Size of PictureBox
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        private void RedrawScaleX(Graphics g, Size s)
        {
            double dmax, dmin;
            float fPosX, fPosY;
            string sScale;
            SizeF sSizeString;

            //
            // Calculate height (X/Y common)
            //
            int iHeight = (int)((double)s.Height * 0.05 * 0.5);
            int iHeightS = (int)((double)s.Height * 0.05 * 0.5 * 0.75);

            //
            // Create font and brush (X/Y common)
            //
            Font fFont = new Font("Arial", System.Math.Max(iHeight, 1));
            Font fFontS = new Font("Arial", System.Math.Max(iHeightS, 1));
            SolidBrush sBrush = new SolidBrush(Color.Black);

            //
            // Draw scales
            //
            if (_stX == ScaleType.LINEAR)
            {
                if (System.Math.Abs(_dExpX) > 3)
                {
                    dmax = _dXmax * System.Math.Pow(10, -_dExpX);
                    dmin = _dXmin * System.Math.Pow(10, -_dExpX);

                    sScale = "x10";
                    sSizeString = g.MeasureString(sScale, fFont);
                    fPosX = s.Width * 0.9f;
                    fPosY = s.Height * 0.9f - sSizeString.Height;
                    g.DrawString(sScale, fFont, sBrush, fPosX, fPosY);
                    fPosX += sSizeString.Width;
                    fPosY -= sSizeString.Height * 0.1f;
                    sScale = string.Format("{0,0:F0}", _dExpX);
                    g.DrawString(sScale, fFontS, sBrush, fPosX, fPosY);
                }
                else
                {
                    dmax = _dXmax;
                    dmin = _dXmin;
                }

                fPosY = s.Height * (0.05f + 0.85f + 0.0125f);
                double dAspect = (double)s.Width / (double)s.Height;
                for ( int i = 0 ; i <= _iScaleX ; i++ )
                {
                    if (((_iScaleX <= 15) && (i % ((dAspect > 1.2) ? 1 : 2) == 0)) ||
                        ((_iScaleX <= 50) && (i % ((dAspect > 1.2) ? 5 : 10) == 0)) ||
                        ((_iScaleX <= 100) && (i % ((dAspect > 1.2) ? 10 : 20) == 0)) ||
                        ((_iScaleX <= 1000) && (i % ((dAspect > 1.2) ? 100 : 200) == 0)) ||
                        ((_iScaleX <= 10000) && (i % ((dAspect > 1.2) ? 1000 : 2000) == 0)))
                    {
                        sScale = string.Format("{0,0:0.####}", dmin + (dmax - dmin) * i / _iScaleX);
                        sSizeString = g.MeasureString(sScale, fFont);
                        fPosX = (s.Width * 0.125f) + (s.Width * 0.775f * i / _iScaleX);
                        fPosX -= sSizeString.Width * 0.5f;
                        g.DrawString(sScale, fFont, sBrush, fPosX, fPosY);
                    }
                }
            }
            else
            {
                for ( int i = 0 ; i <= _iScaleX ; i++ )
                {
                    sScale = "10";
                    sSizeString = g.MeasureString(sScale, fFont);
                    fPosX = (s.Width * 0.125f) + (s.Width * 0.775f * i / _iScaleX);
                    fPosX -= sSizeString.Width * 0.5f;
                    fPosY = s.Height * (0.05f + 0.85f + 0.0125f);
                    g.DrawString(sScale, fFont, sBrush, fPosX, fPosY);
                    fPosX += sSizeString.Width;
                    fPosY -= sSizeString.Height * 0.1f;
                    sScale = string.Format("{0,0:F0}", _dXmin + i);
                    g.DrawString(sScale, fFontS, sBrush, fPosX, fPosY);
                }
            }

            //
            // Clean up
            //
            sBrush.Dispose();
            fFontS.Dispose();
            fFont.Dispose();
        }

        //
        // Function:    RedrawScaleY
        //
        // Description: Draw scale values for Y-axis.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       g               Graphics Object
        //              I       s               Size of PictureBox
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        private void RedrawScaleY(Graphics g, Size s)
        {
            double dmax, dmin;
            float fPosX, fPosY;
            string sScale;
            SizeF sSizeString;

            //
            // Calculate height (X/Y common)
            //
            int iHeight = (int)((double)s.Height * 0.05 * 0.5);
            int iHeightS = (int)((double)s.Height * 0.05 * 0.5 * 0.75);

            //
            // Create font and brush (X/Y common)
            //
            Font fFont = new Font("Arial", System.Math.Max(iHeight, 1));
            Font fFontS = new Font("Arial", System.Math.Max(iHeightS, 1));
            SolidBrush sBrush = new SolidBrush(Color.Black);

            //
            // Draw scales
            //
            if (_stY == ScaleType.LINEAR)
            {
                if (System.Math.Abs(_dExpY) > 4)
                {
                    dmax = _dYmax * System.Math.Pow(10, -_dExpY);
                    dmin = _dYmin * System.Math.Pow(10, -_dExpY);

                    sScale = "x10";
                    sSizeString = g.MeasureString(sScale, fFont);
                    fPosX = s.Width * 0.125f;
                    fPosY = s.Height * 0.05f - sSizeString.Height;
                    g.DrawString(sScale, fFont, sBrush, fPosX, fPosY);
                    fPosX += sSizeString.Width;
                    fPosY -= sSizeString.Height * 0.1f;
                    sScale = string.Format("{0,0:F0}", _dExpY);
                    g.DrawString(sScale, fFontS, sBrush, fPosX, fPosY);
                }
                else
                {
                    dmax = _dYmax;
                    dmin = _dYmin;
                }

                for ( int i = 0 ; i <= _iScaleY ; i++ )
                {
                    sScale = string.Format("{0,0:0.###}", dmin + (dmax - dmin) / _iScaleY * i);
                    sSizeString = g.MeasureString(sScale, fFont);
                    fPosX = s.Width * 0.125f - sSizeString.Width;
                    fPosY = s.Height * 0.90f - (float)(s.Height * 0.85f / _iScaleY * i) - (sSizeString.Height * 0.5f);
                    g.DrawString(sScale, fFont, sBrush, fPosX, fPosY);
                }
            }
            else
            {
                for ( int i = 0 ; i <= _iScaleY ; i++ )
                {
                    sScale = string.Format("{0,0:F0}", _dYmin + i);
                    sSizeString = g.MeasureString(sScale, fFontS);
                    fPosX = s.Width * 0.125f - sSizeString.Width;
                    fPosY = s.Height * 0.90f - (float)(s.Height * 0.85f / _iScaleY * i) - (sSizeString.Height * 0.5f);
                    g.DrawString(sScale, fFontS, sBrush, fPosX, fPosY - sSizeString.Height * 0.1f / 0.75f);
                    sScale = "10";
                    sSizeString = g.MeasureString(sScale, fFont);
                    fPosX -= sSizeString.Width;
                    g.DrawString(sScale, fFont, sBrush, fPosX, fPosY);
                }
            }

            //
            // Clean up
            //
            sBrush.Dispose();
            fFontS.Dispose();
            fFont.Dispose();
        }

        //
        // Function:    ClearGraph
        //
        // Description: Clear the graph
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        public void ClearGraph()
        {
            //
            // Get graphics handle
            //
            if (_pb.Image == null) return;
            Graphics g = Graphics.FromImage(_pb.Image);

            //
            // Clear all
            //
            g.Clear(System.Drawing.Color.White);

            //
            // Clean up
            //
            g.Dispose();    

            //
            // Update
            //
            _pb.Invalidate();
            _pb.Update();

            //
            // Update flags
            //
            _bInverted = false;
        }

        //
        // Function:    valueToPixel
        //
        // Description: Convert double value to integer pixel.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       st              Scale Type
        //              I       iFullScale      Number of pixels for full-scale
        //              I       dValue          Value to be converted
        //              I       dMin            Minimum value of full-scale
        //              I       dMax            Maximum value of full-scale
        //              ----------------------------------------------------------
        //
        // Return:      Integer Pixel
        //
        private int valueToPixel(ScaleType st, int iFullScale, double dValue, double dMin, double dMax)
        {
            int iX;

            if (System.Math.Abs(dMax - dMin) < Double.Epsilon)
            {
                return Int16.MaxValue;
            }

            //
            // Calculate X/Y coordinate in pixels.
            //
            if (st == ScaleType.LINEAR)
            {
                iX = (int)(((iFullScale * (dValue - dMin)) / (dMax - dMin)) + 0.5);
            }
            else
            {
                iX = (int)((iFullScale * System.Math.Log10(dValue / System.Math.Pow(10, dMin)) / (dMax - dMin)) + 0.5);
            }

            return iX;
        }

        //
        // Function:    drawCompressedLine
        //
        // Description: Draw a line for specified data index with compressing.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       g               Graphics Object
        //              I       r               Rectangle of "Plot Area"
        //              I       iIndex          Data Index
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        //
        private void drawCompressedLine(Graphics g, Rectangle r, int iIndex)
        {
            Pen pPen = new Pen(_caColor[iIndex], 1);
            int iXs, iXe, iYs, iYe;
            int iPrevious;

            //
            // Compress data
            //
            CompressedItem[] cia = new CompressedItem[r.Width + 1];
            for ( int i = 0 ; i < cia.Length ; i++ )
            {
                cia[i].iNumber = 0;
            }

            for ( int i = 0 ; i < _daaX[iIndex].Length ; i++ )
            {
                iXs = valueToPixel(_stX, r.Width, _daaX[iIndex][i], _dXmin, _dXmax);
                if ((iXs < 0) || (iXs > (cia.Length - 1)))
                {
                    continue;
                }
                if (cia[iXs].iNumber == 0)
                {
                    cia[iXs].dStart = cia[iXs].dEnd = cia[iXs].dMin = cia[iXs].dMax = _daaY[iIndex][i];
                }
                else
                {
                    if (_daaY[iIndex][i] < cia[iXs].dMin)
                    {
                        cia[iXs].dMin = _daaY[iIndex][i];
                    }
                    if (_daaY[iIndex][i] > cia[iXs].dMax)
                    {
                        cia[iXs].dMax = _daaY[iIndex][i];
                    }
                    cia[iXs].dEnd = _daaY[iIndex][i];
                }
                cia[iXs].iNumber++;
            }

            //
            // Draw lines
            //
            iPrevious = -1;
            for ( int i = 0 ; i < cia.Length ; i++ )
            {
                if (cia[i].iNumber == 0)
                {
                    continue;
                }

                //
                // Draw a vertical line
                //
                iXs = iXe = r.Left + i;
                iYs = r.Top + r.Height - valueToPixel(_stY, r.Height, cia[i].dMin, _dYmin, _dYmax);
                iYe = r.Top + r.Height - valueToPixel(_stY, r.Height, cia[i].dMax, _dYmin, _dYmax);
                if (iYs != iYe)
                {
                    g.DrawLine(pPen, iXs, iYs, iXe, iYe);
                }

                //
                // Draw a line from the previous
                //
                if (iPrevious >= 0)
                {
                    iXs = r.Left + iPrevious;
                    iYs = r.Top + r.Height - valueToPixel(_stY, r.Height, cia[iPrevious].dEnd, _dYmin, _dYmax);
                    iXe = r.Left + i;
                    iYe = r.Top + r.Height - valueToPixel(_stY, r.Height, cia[i].dStart, _dYmin, _dYmax);
                    g.DrawLine(pPen, iXs, iYs, iXe, iYe);
                }

                //
                // Keep current index as previous one.
                //
                iPrevious = i;
            }

            //
            // Clean up
            //
            pPen.Dispose();
        }

        //
        // Function:    drawHistogram
        //
        // Description: Draw a histogram for specified data index.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       g               Graphics Object
        //              I       r               Rectangle of "Plot Area"
        //              I       iIndex          Data Index
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        //
        private void drawHistogram(Graphics g, Rectangle r, int iIndex)
        {
            Pen pPen = new Pen(_caColor[iIndex], (r.Width / _daaX[iIndex].Length) + 1.0f);
            int iXs, iXe, iYs, iYe;

            for ( int i = 0 ; i < _daaX[iIndex].Length ; i++ )
            {
                //
                // Start Point
                //
                iXs = r.Left + valueToPixel(_stX, r.Width, _daaX[iIndex][i], _dXmin, _dXmax);
                iYs = r.Top + r.Height;

                //
                // End Point
                //
                iXe = iXs;
                iYe = r.Top + r.Height - valueToPixel(_stY, r.Height, _daaY[iIndex][i], _dYmin, _dYmax);

                //
                // Draw a line
                //
                g.DrawLine(pPen, iXs, iYs, iXe, iYe);
            }

            //
            // Clean up
            //
            pPen.Dispose();
        }

        //
        // Function:    plot
        //
        // Description: plot (draw marks) for specified data index.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       g               Graphics Object
        //              I       s               Size of PictureBox
        //              I       r               Rectangle of "Plot Area"
        //              I       iIndex          Data Index
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        //
        private void plot(Graphics g, Size s, Rectangle r, int iIndex)
        {
            int iX, iY;

            Font fFont = new Font("Arial", System.Math.Max((int)((double)s.Height * 0.05 * 0.5), 1));
            SolidBrush sBrush = new SolidBrush(_caColor[iIndex]);
            SizeF sSizeString = g.MeasureString(_saMark[iIndex], fFont);

            for ( int i = 0 ; i < _daaX[iIndex].Length ; i++ )
            {
                iX = valueToPixel(_stX, r.Width, _daaX[iIndex][i], _dXmin, _dXmax);
                iY = valueToPixel(_stY, r.Height, _daaY[iIndex][i], _dYmin, _dYmax);

                g.DrawString(_saMark[iIndex], fFont, sBrush,
                             r.Left + iX - sSizeString.Width / 2,
                             r.Top + r.Height - iY -  sSizeString.Height / 2);
            }

            //
            // Clean up
            //
            sBrush.Dispose();
            fFont.Dispose();
        }

        //
        // Function:    RedrawLegend
        //
        // Description: Draw legends
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       g               Graphics Object
        //              I       r               Rectangle of "Plot Area"
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        private void RedrawLegend(Graphics g, Rectangle r)
        {
            int iNumber = 0;
            int iHeight;
            float fMaxLength = -1;
            SizeF sSizeString;
            const int iMargin = 10;

            iHeight = (int)(r.Height * 0.05 * 0.8);
            Font fFont = new Font("Arial", System.Math.Max(iHeight, 1));

            for ( int i = 0 ; i < _baLegend.Length ; i++ )
            {
                if (_baLegend[i] == true)
                {
                    sSizeString = g.MeasureString(_saLegend[i], fFont);
                    if (sSizeString.Width > fMaxLength)
                    {
                        fMaxLength = sSizeString.Width;
                    }
                    iNumber++;
                }
            }

            if (iNumber > 0)
            {
                SolidBrush sBrushFrame = new SolidBrush(Color.White);

                int iWidth = (int)(fMaxLength * 1.2f * 2) + iMargin * 3;
                Rectangle rLegend = new Rectangle(r.Left + r.Width - iWidth - 10,
                                                  r.Top + 10,
                                                  iWidth,
                                                  (iHeight + 4) * iNumber);
                g.FillRectangle(sBrushFrame, rLegend);

                for ( int i = 0, j = 0 ; i < _baLegend.Length ; i++ )
                {
                    if (_baLegend[i] == true)
                    {
                        SolidBrush sBrush = new SolidBrush(_caColor[i]);

                        float fPosX;
                        float fPosY;

                        if (_gta[i] != GraphType.PLOT)
                        {
                            Pen pPen = new Pen(_caColor[i], 1);

                            int iXs = rLegend.Left + iMargin + (int)(fMaxLength * 0.3);
                            int iXe = iXs + (int)(fMaxLength * 0.6);
                            int iYs = rLegend.Top + 2 + iHeight / 2 + (iHeight + 4) * j;
                            int iYe = iYs;
                            g.DrawLine(pPen, iXs, iYs, iXe, iYe);

                            pPen.Dispose();
                        }
                        else
                        {
                            sSizeString = g.MeasureString(_saMark[i], fFont);
                            fPosX = rLegend.Left + iMargin + (int)(fMaxLength * 0.5);
                            fPosX -= sSizeString.Width * 0.5f;
                            fPosY = rLegend.Top + (iHeight + 4) * j;
                            g.DrawString(_saMark[i], fFont, sBrush, fPosX, fPosY);
                        }

                        fPosX = rLegend.Left + (int)fMaxLength + iMargin * 2;
                        fPosY = rLegend.Top + (iHeight + 4) * j;
                        g.DrawString(_saLegend[i], fFont, sBrush, fPosX, fPosY);

                        sBrush.Dispose();
                        j++;
                    }
                }

                sBrushFrame.Dispose();
            }

            fFont.Dispose();
        }

        //
        // Function:    RedrawGraph
        //
        // Description: Redraw the graph
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        public void RedrawGraph()
        {
            //
            // Get graphics handle
            //
            if (_pb.Image == null) return;
            Graphics g = Graphics.FromImage(_pb.Image);

            //
            // Clear all
            //
            g.Clear(System.Drawing.Color.White);

            //
            // Check data existence
            //
            if ((_daaX.Length == 0) || (_daaY.Length == 0))
            {
                g.Dispose();
                return;
            }

            //
            // Draw title and labels
            //
            RedrawTitle(g, _pb.Image.Size);
            RedrawAxisLabel(g, _pb.Image.Size);

            //
            // Draw a frame
            //
            Rectangle rRect = new Rectangle((int)(_pb.Image.Size.Width * 0.125f),
                                            (int)(_pb.Image.Size.Height * 0.05f),
                                            (int)(_pb.Image.Size.Width * 0.775f),
                                            (int)(_pb.Image.Size.Height * 0.85f));
            RedrawFrame(g, rRect);

            //
            // Prepare for drawing
            //
            PrepareScaleX();
            PrepareScaleY();
            if ((_stX == ScaleType.LOG) && (System.Math.Abs(_dXmin - _dXmax) < Double.Epsilon))
            {
                g.Clear(System.Drawing.Color.White);
                g.Dispose();
                return;
            }
            if ((_stY == ScaleType.LOG) && (System.Math.Abs(_dYmin - _dYmax) < Double.Epsilon))
            {
                g.Clear(System.Drawing.Color.White);
                g.Dispose();
                return;
            }

            //
            // Draw scales
            //
            RedrawScaleLineX(g, rRect);
            RedrawScaleLineY(g, rRect);
            RedrawScaleX(g, _pb.Image.Size);
            RedrawScaleY(g, _pb.Image.Size);

            //
            // Draw graphs
            //
            for ( int i = 0 ; i < _daaX.Length ; i++ )
            {
                switch(_gta[i])
                {
                case GraphType.LINE:
                    drawCompressedLine(g, rRect, i);
                    break;
                case GraphType.HISTOGRAM:
                    drawHistogram(g, rRect, i);
                    break;
                case GraphType.PLOT:
                    plot(g, _pb.Image.Size, rRect, i);
                    break;
                }
            }

            //
            // Draw legends
            //
            RedrawLegend(g, rRect);

            //
            // Clean up
            //
            g.Dispose();    

            //
            // Update
            //
            _pb.Invalidate();
            _pb.Update();

            //
            // Update flags
            //
            _bInverted = false;
        }

        //
        // Function:    GetMinY
        //
        // Description: Return Y-axis minimum value
        //
        // Parameters:  N/A
        //
        // Return:      Minimum value (Y-axis)
        //
        public double GetMinY()
        {
            return _dYmin;
        }

        //
        // Function:    GetStepY
        //
        // Description: Return Y-step (distance between Y-scale lines) value
        //
        // Parameters:  N/A
        //
        // Return:      Y step value
        //
        public double GetStepY()
        {
            return ((_iScaleY != 0) ? ((_dYmax - _dYmin) / _iScaleY) : 0.0);
        }

        //
        // Function:    GetNumberOfGraph
        //
        // Description: Return number of graphs (number of data groups)
        //
        // Parameters:  N/A
        //
        // Return:      Number of graphs
        //
        public int GetNumberOfGraph()
        {
            return _gta.Length;
        }

        //
        // Function:    GetGraphType
        //
        // Description: Get graph type by index
        //
        // Parameters:  N/A
        //
        // Return:      Graph type
        //
        public GraphType GetGraphType(int index)
        {
            if ((index >= 0) && (index < _gta.Length))
            {
                return _gta[index];
            }

            return GraphType.UNKNOWN;
        }

        //
        // Function:    ResetRange
        //
        // Description: Reset zooming range information. (Disable zooming)
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        public void ResetRange()
        {
            _bUseRange = false;
        }

        //
        // Function:    SetRange
        //
        // Description: Set zooming range information. (Enable zooming)
        //
        // Parameters:  N/A
        //
        // Return:      N/A
        //
        public void SetRange(double dRangeStart, double dRangeEnd)
        {
            if (_gta.Length > 0)
            {
                for ( int i = 0 ; i < _gta.Length ; i++ )
                {
                    if ((_gta[i] != GraphType.LINE) && (_gta[i] != GraphType.PLOT))
                    {
                        return;
                    }
                }

                _bUseRange = true;
                _dRangeStart = System.Math.Min(dRangeStart, dRangeEnd);
                _dRangeEnd = System.Math.Max(dRangeStart, dRangeEnd);
            }
        }

        //
        // Function:    PixelToValue
        //
        // Description: Convert integer pixel to double value.
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       iMin            Minimum pixel to be converted
        //              I       iMax            Maximum pixel to be converted
        //              O       dMin            Minimum value
        //              O       dMax            Maximum value
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public void PixelToValue(int iMin, int iMax, ref double dMin, ref double dMax)
        {
            float fWidth = _pb.Image.Size.Width * 0.775f;
            int iLeft = (int)(_pb.Image.Size.Width * 0.125f);
            int iRight = (int)(_pb.Image.Size.Width * 0.900f);

            if (iMin < iLeft)
            {
                iMin = iLeft;
            }
            else if (iMin > iRight)
            {
                iMin = iRight;
            }
            if (iMax < iLeft)
            {
                iMax = iLeft;
            }
            else if (iMax > iRight)
            {
                iMax = iRight;
            }

            //
            // Calculate X value from its coordinate.
            //
            if (_stX == ScaleType.LINEAR)
            {
                dMin = (iMin - iLeft) * (_dXmax - _dXmin) / fWidth + _dXmin;
                dMin = (dMin < _dXmin) ? _dXmin : dMin;
                dMin = (dMin > _dXmax) ? _dXmax : dMin;
                dMax = (iMax - iLeft) * (_dXmax - _dXmin) / fWidth + _dXmin;
                dMax = (dMax < _dXmin) ? _dXmin : dMax;
                dMax = (dMax > _dXmax) ? _dXmax : dMax;
            }
            else
            {
                dMin = System.Math.Pow(10, (iMin - iLeft) / fWidth * (_dXmax - _dXmin)) * System.Math.Pow(10, _dXmin);
                dMin = (dMin < System.Math.Pow(10, _dXmin)) ? System.Math.Pow(10, _dXmin) : dMin;
                dMax = System.Math.Pow(10, (iMax - iLeft) / fWidth * (_dXmax - _dXmin)) * System.Math.Pow(10, _dXmin);
                dMax = (dMax > System.Math.Pow(10, _dXmax)) ? System.Math.Pow(10, _dXmax) : dMax;
            }
        }

        //
        // Function:    invertRectangle
        //
        // Description: Invert specified rectangle
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       r               Rectangle
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        private void invertRectangle(Rectangle r)
        {
            Color color, color2;
            Bitmap bm = (Bitmap)_pb.Image;
            int iLeft = (int)(_pb.Image.Size.Width * 0.125f);
            int iRight = iLeft + (int)(_pb.Image.Size.Width * 0.775f);

            for ( int i = r.Left ; i < (r.Left + r.Width) ; i++ )
            {
                //
                // Do not invert frame lines
                //
                if ((i == iLeft) || (i == iRight))
                {
                    continue;
                }

                //
                // Skip odd(or even) vertical lines for speed-up.
                //
                if ((_bInvertOddLine && ((i & 0x1) == 0)) || (!_bInvertOddLine && ((i & 0x1) != 0)))
                {
                    continue;
                }

                //
                // Invert pixels except for white and black.
                //
                for ( int j = r.Top ; j < (r.Top + r.Height) ; j++ )
                {
                    //
                    // Do not invert odd pixels
                    //
                    if ((j & 0x1) != 0)
                    {
                        continue;
                    }

                    //
                    // Invert the pixel.
                    //
                    color = bm.GetPixel(i, j);
                    color2 = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
                    bm.SetPixel(i, j, color2);
                }
            }
        }

        //
        // Function:    Invert
        //
        // Description: Invert rectangle for specified range
        //
        // Parameters:  IN/OUT  Name            Description
        //              ----------------------------------------------------------
        //              I       iXs             Minimum value
        //              I       iXe             Maximum value
        //              ----------------------------------------------------------
        //
        // Return:      N/A
        //
        public void Invert(int iXs, int iXe)
        {
            int iLeft = (int)(_pb.Image.Size.Width * 0.125f);
            int iRight = (int)(_pb.Image.Size.Width * 0.900f);

            //
            // Trimming by the graph area
            //
            if (iXs < iLeft)
            {
                iXs = iLeft;
            }
            else if (iXs > iRight)
            {
                iXs = iRight;
            }
            if (iXe < iLeft)
            {
                iXe = iLeft;
            }
            else if (iXe > iRight)
            {
                iXe = iRight;
            }

            //
            // Trimming by the difference
            //
            if (iXs == iXe)
            {
                iXe += 1;
            }

            //
            // Update invert area
            //
            if (_bInverted)
            {
                //
                // Compare old/new rectangles
                //

                if ((iXe < _iXs) || (iXs > _iXe))
                {
                    //
                    // Restore old rectangle.
                    //
                    invertRectangle(new Rectangle(_iXs,
                                                  (int)(_pb.Image.Size.Height * 0.05f) + 1,
                                                  _iXe - _iXs,
                                                  (int)(_pb.Image.Size.Height * 0.85f) - 1));

                    //
                    // Invert new rectangle.
                    //
                    _bInvertOddLine = ((iXs & 0x1) != 0);
                    invertRectangle(new Rectangle(iXs,
                                                  (int)(_pb.Image.Size.Height * 0.05f) + 1,
                                                  iXe - iXs,
                                                  (int)(_pb.Image.Size.Height * 0.85f) - 1));
                }
                else
                {
                    if (iXs != _iXs)
                    {
                        //
                        // Invert left side
                        //
                        invertRectangle(new Rectangle(System.Math.Min(iXs, _iXs),
                                                      (int)(_pb.Image.Size.Height * 0.05f) + 1,
                                                      System.Math.Abs(iXs - _iXs),
                                                      (int)(_pb.Image.Size.Height * 0.85f) - 1));
                    }
                    if (iXe != _iXe)
                    {
                        //
                        // Invert right side
                        //
                        invertRectangle(new Rectangle(System.Math.Min(iXe, _iXe),
                                                      (int)(_pb.Image.Size.Height * 0.05f) + 1,
                                                      System.Math.Abs(iXe - _iXe),
                                                      (int)(_pb.Image.Size.Height * 0.85f) - 1));
                    }
                }
            }
            else
            {
                //
                // Invert new rectangle.
                //
                _bInvertOddLine = ((iXs & 0x1) != 0);
                invertRectangle(new Rectangle(iXs,
                                              (int)(_pb.Image.Size.Height * 0.05f) + 1,
                                              iXe - iXs,
                                              (int)(_pb.Image.Size.Height * 0.85f) - 1));
            }

            //
            // Keep current values
            //
            _iXs = iXs;
            _iXe = iXe;

            //
            // Update status
            //
            _bInverted = true;

            //
            // Update
            //
            _pb.Invalidate();
            _pb.Update();
        }
    }
}
