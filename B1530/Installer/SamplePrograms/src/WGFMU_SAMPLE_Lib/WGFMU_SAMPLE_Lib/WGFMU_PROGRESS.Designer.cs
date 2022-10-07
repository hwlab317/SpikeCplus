namespace WGFMU_SAMPLE_Lib
{
    partial class WGFMU_PROGRESS
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
            this.buttonAbort = new System.Windows.Forms.Button();
            this.ActualElapsedTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ExpectedTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonAbort
            // 
            this.buttonAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbort.Location = new System.Drawing.Point(230, 29);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(104, 40);
            this.buttonAbort.TabIndex = 8;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // ActualElapsedTime
            // 
            this.ActualElapsedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActualElapsedTime.Location = new System.Drawing.Point(16, 79);
            this.ActualElapsedTime.Name = "ActualElapsedTime";
            this.ActualElapsedTime.Size = new System.Drawing.Size(128, 22);
            this.ActualElapsedTime.TabIndex = 7;
            this.ActualElapsedTime.Text = "textBox1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Expected Execution Time (s)";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Elapsed Time (s)";
            // 
            // ExpectedTime
            // 
            this.ExpectedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpectedTime.Location = new System.Drawing.Point(14, 29);
            this.ExpectedTime.Name = "ExpectedTime";
            this.ExpectedTime.Size = new System.Drawing.Size(128, 22);
            this.ExpectedTime.TabIndex = 6;
            this.ExpectedTime.Text = "textBox1";
            // 
            // WGFMU_PROGRESS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 111);
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.ActualElapsedTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ExpectedTime);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WGFMU_PROGRESS";
            this.Text = "Measuring...";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PROGRESS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.TextBox ActualElapsedTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ExpectedTime;
    }
}