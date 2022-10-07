namespace WGFMU_SAMPLE_Lib
{
    partial class SAVEFILEMODE
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
            this.radioButtonExportPattern = new System.Windows.Forms.RadioButton();
            this.radioButtonExportWaveform = new System.Windows.Forms.RadioButton();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButtonExportPattern
            // 
            this.radioButtonExportPattern.AutoSize = true;
            this.radioButtonExportPattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonExportPattern.Location = new System.Drawing.Point(12, 21);
            this.radioButtonExportPattern.Name = "radioButtonExportPattern";
            this.radioButtonExportPattern.Size = new System.Drawing.Size(195, 20);
            this.radioButtonExportPattern.TabIndex = 0;
            this.radioButtonExportPattern.TabStop = true;
            this.radioButtonExportPattern.Text = "Export as a pattern data.";
            this.radioButtonExportPattern.UseVisualStyleBackColor = true;
            // 
            // radioButtonExportWaveform
            // 
            this.radioButtonExportWaveform.AutoSize = true;
            this.radioButtonExportWaveform.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonExportWaveform.Location = new System.Drawing.Point(12, 47);
            this.radioButtonExportWaveform.Name = "radioButtonExportWaveform";
            this.radioButtonExportWaveform.Size = new System.Drawing.Size(213, 20);
            this.radioButtonExportWaveform.TabIndex = 0;
            this.radioButtonExportWaveform.TabStop = true;
            this.radioButtonExportWaveform.Text = "Export as a waveform data.";
            this.radioButtonExportWaveform.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOk.Location = new System.Drawing.Point(247, 7);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(78, 34);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(247, 47);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(78, 34);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SAVEFILEMODE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 93);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.radioButtonExportWaveform);
            this.Controls.Add(this.radioButtonExportPattern);
            this.MinimizeBox = false;
            this.Name = "SAVEFILEMODE";
            this.Text = "SAVEFILEMODE";
            this.Load += new System.EventHandler(this.SAVEFILEMODE_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonExportPattern;
        private System.Windows.Forms.RadioButton radioButtonExportWaveform;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}