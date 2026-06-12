namespace CHEMLINK.Views
{
    partial class SupplierManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Panel pnlCrud;
        public System.Windows.Forms.TextBox txtNamaSup;
        public System.Windows.Forms.TextBox txtTelp;
        public System.Windows.Forms.TextBox txtAlamat;
        public System.Windows.Forms.Button btnAddSup;

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
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.pnlCrud = new System.Windows.Forms.Panel();
            this.txtNamaSup = new System.Windows.Forms.TextBox();
            this.txtTelp = new System.Windows.Forms.TextBox();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.btnAddSup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.pnlCrud.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMain
            // 
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Location = new System.Drawing.Point(20, 20);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1070, 520);
            this.dgvMain.TabIndex = 0;
            // 
            // pnlCrud
            // 
            this.pnlCrud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCrud.BackColor = System.Drawing.Color.White;
            this.pnlCrud.Controls.Add(this.txtNamaSup);
            this.pnlCrud.Controls.Add(this.txtTelp);
            this.pnlCrud.Controls.Add(this.txtAlamat);
            this.pnlCrud.Controls.Add(this.btnAddSup);
            this.pnlCrud.Location = new System.Drawing.Point(20, 560);
            this.pnlCrud.Name = "pnlCrud";
            this.pnlCrud.Size = new System.Drawing.Size(1070, 60);
            this.pnlCrud.TabIndex = 1;
            // 
            // txtNamaSup
            // 
            this.txtNamaSup.Location = new System.Drawing.Point(15, 17);
            this.txtNamaSup.Name = "txtNamaSup";
            this.txtNamaSup.PlaceholderText = "Nama Supplier";
            this.txtNamaSup.Size = new System.Drawing.Size(150, 25);
            this.txtNamaSup.TabIndex = 0;
            // 
            // txtTelp
            // 
            this.txtTelp.Location = new System.Drawing.Point(180, 17);
            this.txtTelp.Name = "txtTelp";
            this.txtTelp.PlaceholderText = "No Telepon";
            this.txtTelp.Size = new System.Drawing.Size(120, 25);
            this.txtTelp.TabIndex = 1;
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(315, 17);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.PlaceholderText = "Alamat";
            this.txtAlamat.Size = new System.Drawing.Size(150, 25);
            this.txtAlamat.TabIndex = 2;
            // 
            // btnAddSup
            // 
            this.btnAddSup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnAddSup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSup.ForeColor = System.Drawing.Color.White;
            this.btnAddSup.Location = new System.Drawing.Point(480, 12);
            this.btnAddSup.Name = "btnAddSup";
            this.btnAddSup.Size = new System.Drawing.Size(200, 35);
            this.btnAddSup.TabIndex = 3;
            this.btnAddSup.Text = "Tambahkan Supplier";
            this.btnAddSup.UseVisualStyleBackColor = false;
            // 
            // SupplierManagementControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlCrud);
            this.Controls.Add(this.dgvMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "SupplierManagementControl";
            this.Size = new System.Drawing.Size(1110, 670);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.pnlCrud.ResumeLayout(false);
            this.pnlCrud.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
