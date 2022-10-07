namespace WGFMU_SAMPLE_Lib
{
    partial class WGFMU_DATASAVE_OPTIONS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonNBTITypical = new System.Windows.Forms.RadioButton();
            this.radioButtonAllMaes = new System.Windows.Forms.RadioButton();
            this.radioButtonTimeAndData = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButtonNBTITypical
            // 
            this.radioButtonNBTITypical.AutoSize = true;
            this.radioButtonNBTITypical.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonNBTITypical.Location = new System.Drawing.Point(12, 12);
            this.radioButtonNBTITypical.Name = "radioButtonNBTITypical";
            this.radioButtonNBTITypical.Size = new System.Drawing.Size(78, 20);
            this.radioButtonNBTITypical.TabIndex = 0;
            this.radioButtonNBTITypical.TabStop = true;
            this.radioButtonNBTITypical.Text = "Typical";
            this.radioButtonNBTITypical.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllMaes
            // 
            this.radioButtonAllMaes.AutoSize = true;
            this.radioButtonAllMaes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAllMaes.Location = new System.Drawing.Point(12, 38);
            this.radioButtonAllMaes.Name = "radioButtonAllMaes";
            this.radioButtonAllMaes.Size = new System.Drawing.Size(154, 20);
            this.radioButtonAllMaes.TabIndex = 0;
            this.radioButtonAllMaes.TabStop = true;
            this.radioButtonAllMaes.Text = "All Measured Data";
            this.radioButtonAllMaes.UseVisualStyleBackColor = true;
            // 
            // radioButtonTimeAndData
            // 
            this.radioButtonTimeAndData.AutoSize = true;
            this.radioButtonTimeAndData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonTimeAndData.Location = new System.Drawing.Point(12, 64);
            this.radioButtonTimeAndData.Name = "radioButtonTimeAndData";
            this.radioButtonTimeAndData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButtonTimeAndData.Size = new System.Drawing.Size(223, 20);
            this.radioButtonTimeAndData.TabIndex = 0;
            this.radioButtonTimeAndData.TabStop = true;
            this.radioButtonTimeAndData.Text = "All Time and Measured Data";
            this.radioButtonTimeAndData.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(287, 25);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 33);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // WGFMU_DATASAVE_OPTIONS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 95);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.radioButtonTimeAndData);
            this.Controls.Add(this.radioButtonAllMaes);
            this.Controls.Add(this.radioButtonNBTITypical);
            this.Name = "WGFMU_DATASAVE_OPTIONS";
            this.Text = "WGFMU_DATASAVE_OPTIONS";
            this.Load += new System.EventHandler(this.WGFMU_DATASAVE_OPTIONS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonNBTITypical;
        private System.Windows.Forms.RadioButton radioButtonAllMaes;
        private System.Windows.Forms.RadioButton radioButtonTimeAndData;
        private System.Windows.Forms.Button buttonOK;
    }
}