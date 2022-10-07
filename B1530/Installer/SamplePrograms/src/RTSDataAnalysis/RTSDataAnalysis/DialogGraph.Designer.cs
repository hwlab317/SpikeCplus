namespace RTSDataAnalysis
{
    partial class DialogGraph
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBase = new System.Windows.Forms.Panel();
            this.pnGraph = new System.Windows.Forms.Panel();
            this.pbGraph = new System.Windows.Forms.PictureBox();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.pnHeaderCombo = new System.Windows.Forms.Panel();
            this.cmbRange = new System.Windows.Forms.ComboBox();
            this.pnHeaderButton = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.pnlBase.SuspendLayout();
            this.pnGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
            this.pnHeader.SuspendLayout();
            this.pnHeaderCombo.SuspendLayout();
            this.pnHeaderButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBase
            // 
            this.pnlBase.Controls.Add(this.pnGraph);
            this.pnlBase.Controls.Add(this.pnHeader);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBase.Location = new System.Drawing.Point(0, 0);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(632, 446);
            this.pnlBase.TabIndex = 0;
            // 
            // pnGraph
            // 
            this.pnGraph.Controls.Add(this.pbGraph);
            this.pnGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnGraph.Location = new System.Drawing.Point(0, 26);
            this.pnGraph.Name = "pnGraph";
            this.pnGraph.Size = new System.Drawing.Size(632, 420);
            this.pnGraph.TabIndex = 2;
            // 
            // pbGraph
            // 
            this.pbGraph.Location = new System.Drawing.Point(52, 36);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(100, 50);
            this.pbGraph.TabIndex = 0;
            this.pbGraph.TabStop = false;
            this.pbGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbGraph_MouseDown);
            this.pbGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbGraph_MouseMove);
            this.pbGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbGraph_MouseUp);
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.pnHeaderCombo);
            this.pnHeader.Controls.Add(this.pnHeaderButton);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(632, 26);
            this.pnHeader.TabIndex = 1;
            // 
            // pnHeaderCombo
            // 
            this.pnHeaderCombo.Controls.Add(this.cmbRange);
            this.pnHeaderCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnHeaderCombo.Location = new System.Drawing.Point(0, 0);
            this.pnHeaderCombo.Name = "pnHeaderCombo";
            this.pnHeaderCombo.Size = new System.Drawing.Size(479, 26);
            this.pnHeaderCombo.TabIndex = 2;
            // 
            // cmbRange
            // 
            this.cmbRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRange.FormattingEnabled = true;
            this.cmbRange.Location = new System.Drawing.Point(0, 0);
            this.cmbRange.Name = "cmbRange";
            this.cmbRange.Size = new System.Drawing.Size(479, 26);
            this.cmbRange.TabIndex = 0;
            this.cmbRange.TextChanged += new System.EventHandler(this.cmbRange_TextChanged);
            this.cmbRange.TextUpdate += new System.EventHandler(this.cmbRange_TextUpdate);
            // 
            // pnHeaderButton
            // 
            this.pnHeaderButton.Controls.Add(this.btnReset);
            this.pnHeaderButton.Controls.Add(this.btnZoom);
            this.pnHeaderButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnHeaderButton.Location = new System.Drawing.Point(479, 0);
            this.pnHeaderButton.Name = "pnHeaderButton";
            this.pnHeaderButton.Size = new System.Drawing.Size(153, 26);
            this.pnHeaderButton.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReset.Location = new System.Drawing.Point(78, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 26);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnZoom.Location = new System.Drawing.Point(0, 0);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(75, 26);
            this.btnZoom.TabIndex = 1;
            this.btnZoom.Text = "Zoom";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // DialogGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.pnlBase);
            this.Name = "DialogGraph";
            this.Text = "DialogGraph";
            this.Resize += new System.EventHandler(this.DialogGraph_Resize);
            this.ResizeEnd += new System.EventHandler(this.DialogGraph_ResizeEnd);
            this.Load += new System.EventHandler(this.DialogGraph_Load);
            this.pnlBase.ResumeLayout(false);
            this.pnGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
            this.pnHeader.ResumeLayout(false);
            this.pnHeaderCombo.ResumeLayout(false);
            this.pnHeaderButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.PictureBox pbGraph;
        private System.Windows.Forms.Panel pnGraph;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.ComboBox cmbRange;
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.Panel pnHeaderButton;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnHeaderCombo;
    }
}