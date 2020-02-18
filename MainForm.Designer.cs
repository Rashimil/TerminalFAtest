namespace TerminalFAtest
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.GetShiftInfo = new System.Windows.Forms.Button();
            this.OpenShiftBegin = new System.Windows.Forms.Button();
            this.SendCashierData = new System.Windows.Forms.Button();
            this.OpenShift = new System.Windows.Forms.Button();
            this.CancelFiscalDocument = new System.Windows.Forms.Button();
            this.CloseShiftBegin = new System.Windows.Forms.Button();
            this.CloseShift = new System.Windows.Forms.Button();
            this.OpenCheck = new System.Windows.Forms.Button();
            this.SendCheckPosition = new System.Windows.Forms.Button();
            this.SendPaymentData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(272, 12);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(516, 424);
            this.textBoxLog.TabIndex = 9999999;
            // 
            // GetShiftInfo
            // 
            this.GetShiftInfo.Location = new System.Drawing.Point(12, 12);
            this.GetShiftInfo.Name = "GetShiftInfo";
            this.GetShiftInfo.Size = new System.Drawing.Size(226, 28);
            this.GetShiftInfo.TabIndex = 1;
            this.GetShiftInfo.Text = "GetShiftInfo";
            this.GetShiftInfo.UseVisualStyleBackColor = true;
            this.GetShiftInfo.Click += new System.EventHandler(this.GetShiftInfo_Click);
            // 
            // OpenShiftBegin
            // 
            this.OpenShiftBegin.Location = new System.Drawing.Point(12, 65);
            this.OpenShiftBegin.Name = "OpenShiftBegin";
            this.OpenShiftBegin.Size = new System.Drawing.Size(226, 28);
            this.OpenShiftBegin.TabIndex = 10000000;
            this.OpenShiftBegin.Text = "OpenShiftBegin";
            this.OpenShiftBegin.UseVisualStyleBackColor = true;
            this.OpenShiftBegin.Click += new System.EventHandler(this.OpenShiftBegin_Click);
            // 
            // SendCashierData
            // 
            this.SendCashierData.Location = new System.Drawing.Point(12, 99);
            this.SendCashierData.Name = "SendCashierData";
            this.SendCashierData.Size = new System.Drawing.Size(226, 28);
            this.SendCashierData.TabIndex = 10000001;
            this.SendCashierData.Text = "SendCashierData";
            this.SendCashierData.UseVisualStyleBackColor = true;
            this.SendCashierData.Click += new System.EventHandler(this.SendCashierData_Click);
            // 
            // OpenShift
            // 
            this.OpenShift.Location = new System.Drawing.Point(12, 133);
            this.OpenShift.Name = "OpenShift";
            this.OpenShift.Size = new System.Drawing.Size(226, 28);
            this.OpenShift.TabIndex = 10000002;
            this.OpenShift.Text = "OpenShift";
            this.OpenShift.UseVisualStyleBackColor = true;
            this.OpenShift.Click += new System.EventHandler(this.OpenShift_Click);
            // 
            // CancelFiscalDocument
            // 
            this.CancelFiscalDocument.Location = new System.Drawing.Point(21, 408);
            this.CancelFiscalDocument.Name = "CancelFiscalDocument";
            this.CancelFiscalDocument.Size = new System.Drawing.Size(226, 28);
            this.CancelFiscalDocument.TabIndex = 10000003;
            this.CancelFiscalDocument.Text = "CancelFiscalDocument";
            this.CancelFiscalDocument.UseVisualStyleBackColor = true;
            this.CancelFiscalDocument.Click += new System.EventHandler(this.CancelFiscalDocument_Click);
            // 
            // CloseShiftBegin
            // 
            this.CloseShiftBegin.Location = new System.Drawing.Point(12, 182);
            this.CloseShiftBegin.Name = "CloseShiftBegin";
            this.CloseShiftBegin.Size = new System.Drawing.Size(226, 28);
            this.CloseShiftBegin.TabIndex = 10000004;
            this.CloseShiftBegin.Text = "CloseShiftBegin";
            this.CloseShiftBegin.UseVisualStyleBackColor = true;
            this.CloseShiftBegin.Click += new System.EventHandler(this.CloseShiftBegin_Click);
            // 
            // CloseShift
            // 
            this.CloseShift.Location = new System.Drawing.Point(12, 216);
            this.CloseShift.Name = "CloseShift";
            this.CloseShift.Size = new System.Drawing.Size(226, 28);
            this.CloseShift.TabIndex = 10000005;
            this.CloseShift.Text = "CloseShift";
            this.CloseShift.UseVisualStyleBackColor = true;
            this.CloseShift.Click += new System.EventHandler(this.CloseShift_Click);
            // 
            // OpenCheck
            // 
            this.OpenCheck.Location = new System.Drawing.Point(12, 266);
            this.OpenCheck.Name = "OpenCheck";
            this.OpenCheck.Size = new System.Drawing.Size(226, 28);
            this.OpenCheck.TabIndex = 10000006;
            this.OpenCheck.Text = "OpenCheck";
            this.OpenCheck.UseVisualStyleBackColor = true;
            this.OpenCheck.Click += new System.EventHandler(this.OpenCheck_Click);
            // 
            // SendCheckPosition
            // 
            this.SendCheckPosition.Location = new System.Drawing.Point(12, 300);
            this.SendCheckPosition.Name = "SendCheckPosition";
            this.SendCheckPosition.Size = new System.Drawing.Size(226, 28);
            this.SendCheckPosition.TabIndex = 10000007;
            this.SendCheckPosition.Text = "SendCheckPosition";
            this.SendCheckPosition.UseVisualStyleBackColor = true;
            this.SendCheckPosition.Click += new System.EventHandler(this.SendCheckPosition_Click);
            // 
            // SendPaymentData
            // 
            this.SendPaymentData.Location = new System.Drawing.Point(12, 334);
            this.SendPaymentData.Name = "SendPaymentData";
            this.SendPaymentData.Size = new System.Drawing.Size(226, 28);
            this.SendPaymentData.TabIndex = 10000008;
            this.SendPaymentData.Text = "SendPaymentData";
            this.SendPaymentData.UseVisualStyleBackColor = true;
            this.SendPaymentData.Click += new System.EventHandler(this.SendPaymentData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SendPaymentData);
            this.Controls.Add(this.SendCheckPosition);
            this.Controls.Add(this.OpenCheck);
            this.Controls.Add(this.CloseShift);
            this.Controls.Add(this.CloseShiftBegin);
            this.Controls.Add(this.CancelFiscalDocument);
            this.Controls.Add(this.OpenShift);
            this.Controls.Add(this.SendCashierData);
            this.Controls.Add(this.OpenShiftBegin);
            this.Controls.Add(this.GetShiftInfo);
            this.Controls.Add(this.textBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TErminalFA test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button GetShiftInfo;
        public System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button OpenShiftBegin;
        private System.Windows.Forms.Button SendCashierData;
        private System.Windows.Forms.Button OpenShift;
        private System.Windows.Forms.Button CancelFiscalDocument;
        private System.Windows.Forms.Button CloseShiftBegin;
        private System.Windows.Forms.Button CloseShift;
        private System.Windows.Forms.Button OpenCheck;
        private System.Windows.Forms.Button SendCheckPosition;
        private System.Windows.Forms.Button SendPaymentData;
    }
}

