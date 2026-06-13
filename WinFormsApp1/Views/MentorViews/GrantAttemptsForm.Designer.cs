namespace CodeTrainerApp.Views.MentorViews
{
    partial class GrantAttemptsForm
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
            this._lblInfo = new System.Windows.Forms.Label();
            this._numCount = new System.Windows.Forms.NumericUpDown();
            this._btnOk = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._numCount)).BeginInit();
            this.SuspendLayout();
            // 
            // _lblInfo
            // 
            this._lblInfo.Location = new System.Drawing.Point(20, 20);
            this._lblInfo.Name = "_lblInfo";
            this._lblInfo.Size = new System.Drawing.Size(260, 80);
            this._lblInfo.TabIndex = 0;
            this._lblInfo.Text = "Інформація про користувача та тест";
            this._lblInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // _numCount
            // 
            this._numCount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._numCount.Location = new System.Drawing.Point(20, 110);
            this._numCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this._numCount.Name = "_numCount";
            this._numCount.Size = new System.Drawing.Size(260, 29);
            this._numCount.TabIndex = 1;
            this._numCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _btnOk
            // 
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Location = new System.Drawing.Point(20, 150);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(125, 35);
            this._btnOk.TabIndex = 2;
            this._btnOk.Text = "ОК";
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(155, 150);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(125, 35);
            this._btnCancel.TabIndex = 3;
            this._btnCancel.Text = "Скасувати";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // GrantAttemptsForm
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(304, 201);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnOk);
            this.Controls.Add(this._numCount);
            this.Controls.Add(this._lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GrantAttemptsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Керування спробами";
            ((System.ComponentModel.ISupportInitialize)(this._numCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _lblInfo;
        private System.Windows.Forms.NumericUpDown _numCount;
        private System.Windows.Forms.Button _btnOk;
        private System.Windows.Forms.Button _btnCancel;
    }
}