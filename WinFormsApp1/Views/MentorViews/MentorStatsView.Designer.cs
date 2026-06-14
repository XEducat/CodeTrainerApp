namespace CodeTrainerApp.Views.MentorViews
{
    partial class MentorStatsView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView StatsDataGridView;
        private System.Windows.Forms.Label HeaderLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.StatsDataGridView = new System.Windows.Forms.DataGridView();
            this.HeaderLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StatsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // StatsDataGridView
            // 
            this.StatsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.StatsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StatsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatsDataGridView.Location = new System.Drawing.Point(20, 70);
            this.StatsDataGridView.Name = "StatsDataGridView";
            this.StatsDataGridView.ReadOnly = true;
            this.StatsDataGridView.RowTemplate.Height = 25;
            this.StatsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StatsDataGridView.Size = new System.Drawing.Size(760, 360);
            this.StatsDataGridView.TabIndex = 0;
            this.StatsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StatsDataGridView_CellDoubleClick);
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.HeaderLabel.Location = new System.Drawing.Point(20, 20);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(760, 50);
            this.HeaderLabel.TabIndex = 1;
            this.HeaderLabel.Text = "Статистика проходження тестів";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MentorStatsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.DoubleBuffered = true;
            this.Controls.Add(this.StatsDataGridView);
            this.Controls.Add(this.HeaderLabel);
            this.Name = "MentorStatsView";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "Статистика";
            this.Load += new System.EventHandler(this.MentorStatsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StatsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
