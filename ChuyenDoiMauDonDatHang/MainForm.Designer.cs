
namespace ChuyenDoiMauDonDatHang
{
    partial class MainForm
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
            this.btnSelectExcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtExcelPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtFolderOutput = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnSelectOutputPath = new DevExpress.XtraEditors.SimpleButton();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.btnRun = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtFindText = new DevExpress.XtraEditors.TextEdit();
            this.btnSelectPDFInput = new DevExpress.XtraEditors.SimpleButton();
            this.txtPDFInput = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtExcelPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFolderOutput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFindText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPDFInput.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectExcel
            // 
            this.btnSelectExcel.Location = new System.Drawing.Point(483, 11);
            this.btnSelectExcel.Name = "btnSelectExcel";
            this.btnSelectExcel.Size = new System.Drawing.Size(99, 23);
            this.btnSelectExcel.TabIndex = 0;
            this.btnSelectExcel.Text = "Chọn file Excel";
            this.btnSelectExcel.Click += new System.EventHandler(this.btnSelectExcel_Click);
            // 
            // txtExcelPath
            // 
            this.txtExcelPath.Location = new System.Drawing.Point(134, 12);
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.Properties.ReadOnly = true;
            this.txtExcelPath.Size = new System.Drawing.Size(343, 20);
            this.txtExcelPath.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(103, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Đường dẫn file excel:";
            // 
            // txtFolderOutput
            // 
            this.txtFolderOutput.Location = new System.Drawing.Point(134, 90);
            this.txtFolderOutput.Name = "txtFolderOutput";
            this.txtFolderOutput.Properties.ReadOnly = true;
            this.txtFolderOutput.Size = new System.Drawing.Size(343, 20);
            this.txtFolderOutput.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 94);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(86, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Folder lưu mẫu in:";
            // 
            // btnSelectOutputPath
            // 
            this.btnSelectOutputPath.Location = new System.Drawing.Point(483, 89);
            this.btnSelectOutputPath.Name = "btnSelectOutputPath";
            this.btnSelectOutputPath.Size = new System.Drawing.Size(99, 23);
            this.btnSelectOutputPath.TabIndex = 0;
            this.btnSelectOutputPath.Text = "Chọn folder";
            this.btnSelectOutputPath.Click += new System.EventHandler(this.btnSelectOutputPath_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 116);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(570, 177);
            this.richTextBoxLog.TabIndex = 3;
            this.richTextBoxLog.Text = "";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 299);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(570, 23);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Tạo và lưu mẫu in vào folder";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 68);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(116, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Khuyến mãi trong excel:";
            // 
            // txtFindText
            // 
            this.txtFindText.EditValue = "mẫu";
            this.txtFindText.Location = new System.Drawing.Point(134, 64);
            this.txtFindText.Name = "txtFindText";
            this.txtFindText.Size = new System.Drawing.Size(158, 20);
            this.txtFindText.TabIndex = 1;
            // 
            // btnSelectPDFInput
            // 
            this.btnSelectPDFInput.Location = new System.Drawing.Point(483, 37);
            this.btnSelectPDFInput.Name = "btnSelectPDFInput";
            this.btnSelectPDFInput.Size = new System.Drawing.Size(99, 23);
            this.btnSelectPDFInput.TabIndex = 0;
            this.btnSelectPDFInput.Text = "Chọn folder";
            this.btnSelectPDFInput.Click += new System.EventHandler(this.btnSelectPDFInput_Click);
            // 
            // txtPDFInput
            // 
            this.txtPDFInput.Location = new System.Drawing.Point(134, 38);
            this.txtPDFInput.Name = "txtPDFInput";
            this.txtPDFInput.Properties.ReadOnly = true;
            this.txtPDFInput.Size = new System.Drawing.Size(343, 20);
            this.txtPDFInput.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 42);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(53, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Folder pdf:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 334);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtFindText);
            this.Controls.Add(this.txtPDFInput);
            this.Controls.Add(this.txtFolderOutput);
            this.Controls.Add(this.txtExcelPath);
            this.Controls.Add(this.btnSelectPDFInput);
            this.Controls.Add(this.btnSelectOutputPath);
            this.Controls.Add(this.btnSelectExcel);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển đổi mẫu đơn đặt hàng";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtExcelPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFolderOutput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFindText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPDFInput.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSelectExcel;
        private DevExpress.XtraEditors.TextEdit txtExcelPath;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtFolderOutput;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSelectOutputPath;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private DevExpress.XtraEditors.SimpleButton btnRun;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtFindText;
        private DevExpress.XtraEditors.SimpleButton btnSelectPDFInput;
        private DevExpress.XtraEditors.TextEdit txtPDFInput;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}