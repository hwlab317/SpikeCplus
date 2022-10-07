namespace WGFMU_SAMPLE_Lib
{
    partial class WGFMU_CONFIG
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
            this.GpibAddress = new System.Windows.Forms.TextBox();
            this.VisaId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.buttonInitialize = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.MessageList = new System.Windows.Forms.ListBox();
            this.checkBoxDebugEnable = new System.Windows.Forms.CheckBox();
            this.buttonSelfCal = new System.Windows.Forms.Button();
            this.buttonSelfTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GpibAddress
            // 
            this.GpibAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GpibAddress.Location = new System.Drawing.Point(12, 73);
            this.GpibAddress.Name = "GpibAddress";
            this.GpibAddress.Size = new System.Drawing.Size(100, 22);
            this.GpibAddress.TabIndex = 2;
            this.GpibAddress.Text = "17";
            // 
            // VisaId
            // 
            this.VisaId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisaId.Location = new System.Drawing.Point(12, 26);
            this.VisaId.Name = "VisaId";
            this.VisaId.Size = new System.Drawing.Size(100, 22);
            this.VisaId.TabIndex = 1;
            this.VisaId.Text = "GPIB0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "VISA ID";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(12, 54);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(105, 16);
            this.Label4.TabIndex = 10;
            this.Label4.Text = "GPIB Address";
            // 
            // buttonInitialize
            // 
            this.buttonInitialize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInitialize.Location = new System.Drawing.Point(123, 101);
            this.buttonInitialize.Name = "buttonInitialize";
            this.buttonInitialize.Size = new System.Drawing.Size(64, 34);
            this.buttonInitialize.TabIndex = 4;
            this.buttonInitialize.Text = "Initialize";
            this.buttonInitialize.UseVisualStyleBackColor = true;
            this.buttonInitialize.Click += new System.EventHandler(this.buttonInitialize_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(115, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Message";
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(351, 101);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(66, 34);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // MessageList
            // 
            this.MessageList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageList.FormattingEnabled = true;
            this.MessageList.HorizontalScrollbar = true;
            this.MessageList.Location = new System.Drawing.Point(118, 26);
            this.MessageList.Name = "MessageList";
            this.MessageList.Size = new System.Drawing.Size(299, 69);
            this.MessageList.TabIndex = 8;
            // 
            // checkBoxDebugEnable
            // 
            this.checkBoxDebugEnable.AutoSize = true;
            this.checkBoxDebugEnable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDebugEnable.Location = new System.Drawing.Point(12, 111);
            this.checkBoxDebugEnable.Name = "checkBoxDebugEnable";
            this.checkBoxDebugEnable.Size = new System.Drawing.Size(73, 20);
            this.checkBoxDebugEnable.TabIndex = 3;
            this.checkBoxDebugEnable.Text = "Debug";
            this.checkBoxDebugEnable.UseVisualStyleBackColor = true;
            // 
            // buttonSelfCal
            // 
            this.buttonSelfCal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelfCal.Location = new System.Drawing.Point(193, 101);
            this.buttonSelfCal.Name = "buttonSelfCal";
            this.buttonSelfCal.Size = new System.Drawing.Size(64, 34);
            this.buttonSelfCal.TabIndex = 5;
            this.buttonSelfCal.Text = "Self Cal";
            this.buttonSelfCal.UseVisualStyleBackColor = true;
            this.buttonSelfCal.Click += new System.EventHandler(this.buttonSelfCal_Click);
            // 
            // buttonSelfTest
            // 
            this.buttonSelfTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelfTest.Location = new System.Drawing.Point(263, 101);
            this.buttonSelfTest.Name = "buttonSelfTest";
            this.buttonSelfTest.Size = new System.Drawing.Size(82, 34);
            this.buttonSelfTest.TabIndex = 6;
            this.buttonSelfTest.Text = "Self Test";
            this.buttonSelfTest.UseVisualStyleBackColor = true;
            this.buttonSelfTest.Click += new System.EventHandler(this.buttonSelfTest_Click);
            // 
            // WGFMU_CONFIG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 142);
            this.Controls.Add(this.buttonSelfTest);
            this.Controls.Add(this.buttonSelfCal);
            this.Controls.Add(this.checkBoxDebugEnable);
            this.Controls.Add(this.MessageList);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonInitialize);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VisaId);
            this.Controls.Add(this.GpibAddress);
            this.MaximizeBox = false;
            this.Name = "WGFMU_CONFIG";
            this.Text = "WGFMU_Config";
            this.Load += new System.EventHandler(this.WGFMU_CONFIG_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WGFMU_CONFIG_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox GpibAddress;
        private System.Windows.Forms.TextBox VisaId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Button buttonInitialize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListBox MessageList;
        private System.Windows.Forms.CheckBox checkBoxDebugEnable;
        private System.Windows.Forms.Button buttonSelfCal;
        private System.Windows.Forms.Button buttonSelfTest;
    }
}