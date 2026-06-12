namespace CHEMLINK.Views
{
    partial class DashboardControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblValKritis;
        private System.Windows.Forms.Label lblValTotal;
        private System.Windows.Forms.Panel pnlStokKritis;
        private System.Windows.Forms.Panel pnlTotalProduk;
        public System.Windows.Forms.DataGridView dgvMain;

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
            this.pnlStokKritis = new System.Windows.Forms.Panel();
            this.lblValKritis = new System.Windows.Forms.Label();
            this.pnlTotalProduk = new System.Windows.Forms.Panel();
            this.lblValTotal = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.pnlStokKritis.SuspendLayout();
            this.pnlTotalProduk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlStokKritis
            // 
            this.pnlStokKritis.BackColor = System.Drawing.Color.White;
            this.pnlStokKritis.Controls.Add(this.lblValKritis);
            this.pnlStokKritis.Location = new System.Drawing.Point(320, 15);
            this.pnlStokKritis.Name = "pnlStokKritis";
            this.pnlStokKritis.Size = new System.Drawing.Size(280, 120);
            this.pnlStokKritis.TabIndex = 0;
            // 
            // lblValKritis
            // 
            this.lblValKritis.AutoSize = true;
            this.lblValKritis.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValKritis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblValKritis.Location = new System.Drawing.Point(15, 50);
            this.lblValKritis.Name = "lblValKritis";
            this.lblValKritis.Size = new System.Drawing.Size(95, 37);
            this.lblValKritis.TabIndex = 0;
            this.lblValKritis.Text = "0 Item";
            // 
            // pnlTotalProduk
            // 
            this.pnlTotalProduk.BackColor = System.Drawing.Color.White;
            this.pnlTotalProduk.Controls.Add(this.lblValTotal);
            this.pnlTotalProduk.Location = new System.Drawing.Point(20, 15);
            this.pnlTotalProduk.Name = "pnlTotalProduk";
            this.pnlTotalProduk.Size = new System.Drawing.Size(280, 120);
            this.pnlTotalProduk.TabIndex = 1;
            // 
            // lblValTotal
            // 
            this.lblValTotal.AutoSize = true;
            this.lblValTotal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblValTotal.Location = new System.Drawing.Point(15, 50);
            this.lblValTotal.Name = "lblValTotal";
            this.lblValTotal.Size = new System.Drawing.Size(95, 37);
            this.lblValTotal.TabIndex = 0;
            this.lblValTotal.Text = "0 Item";
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Location = new System.Drawing.Point(20, 150);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1070, 500);
            this.dgvMain.TabIndex = 2;
            // 
            // DashboardControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.pnlTotalProduk);
            this.Controls.Add(this.pnlStokKritis);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(1110, 670);
            this.pnlStokKritis.ResumeLayout(false);
            this.pnlStokKritis.PerformLayout();
            this.pnlTotalProduk.ResumeLayout(false);
            this.pnlTotalProduk.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
