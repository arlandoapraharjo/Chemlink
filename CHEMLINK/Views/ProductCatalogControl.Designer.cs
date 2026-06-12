namespace CHEMLINK.Views
{
    partial class ProductCatalogControl
    {
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Panel pnlCrud;
        public System.Windows.Forms.TextBox txtNama;
        public System.Windows.Forms.TextBox txtKategori;
        public System.Windows.Forms.TextBox txtStok;
        public System.Windows.Forms.TextBox txtHarga;
        public System.Windows.Forms.Button btnTambah;
        public System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Label lblKelola;

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
            this.lblKelola = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtKategori = new System.Windows.Forms.TextBox();
            this.txtStok = new System.Windows.Forms.TextBox();
            this.txtHarga = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
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
            this.pnlCrud.Controls.Add(this.lblKelola);
            this.pnlCrud.Controls.Add(this.txtNama);
            this.pnlCrud.Controls.Add(this.txtKategori);
            this.pnlCrud.Controls.Add(this.txtStok);
            this.pnlCrud.Controls.Add(this.txtHarga);
            this.pnlCrud.Controls.Add(this.btnTambah);
            this.pnlCrud.Controls.Add(this.btnHapus);
            this.pnlCrud.Location = new System.Drawing.Point(20, 560);
            this.pnlCrud.Name = "pnlCrud";
            this.pnlCrud.Size = new System.Drawing.Size(1070, 60);
            this.pnlCrud.TabIndex = 1;
            // 
            // lblKelola
            // 
            this.lblKelola.AutoSize = true;
            this.lblKelola.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblKelola.Location = new System.Drawing.Point(15, 20);
            this.lblKelola.Name = "lblKelola";
            this.lblKelola.Size = new System.Drawing.Size(91, 19);
            this.lblKelola.TabIndex = 0;
            this.lblKelola.Text = "Kelola Obat:";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(115, 17);
            this.txtNama.Name = "txtNama";
            this.txtNama.PlaceholderText = "Nama Obat";
            this.txtNama.Size = new System.Drawing.Size(150, 25);
            this.txtNama.TabIndex = 1;
            // 
            // txtKategori
            // 
            this.txtKategori.Location = new System.Drawing.Point(275, 17);
            this.txtKategori.Name = "txtKategori";
            this.txtKategori.PlaceholderText = "Kategori (Custom)";
            this.txtKategori.Size = new System.Drawing.Size(150, 25);
            this.txtKategori.TabIndex = 2;
            // 
            // txtStok
            // 
            this.txtStok.Location = new System.Drawing.Point(435, 17);
            this.txtStok.Name = "txtStok";
            this.txtStok.PlaceholderText = "Stok";
            this.txtStok.Size = new System.Drawing.Size(80, 25);
            this.txtStok.TabIndex = 3;
            // 
            // txtHarga
            // 
            this.txtHarga.Location = new System.Drawing.Point(525, 17);
            this.txtHarga.Name = "txtHarga";
            this.txtHarga.PlaceholderText = "Harga";
            this.txtHarga.Size = new System.Drawing.Size(100, 25);
            this.txtHarga.TabIndex = 4;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(635, 14);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(110, 30);
            this.btnTambah.TabIndex = 5;
            this.btnTambah.Text = "Tambah Obat";
            this.btnTambah.UseVisualStyleBackColor = false;
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.ForeColor = System.Drawing.Color.White;
            this.btnHapus.Location = new System.Drawing.Point(755, 14);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(110, 30);
            this.btnHapus.TabIndex = 6;
            this.btnHapus.Text = "Hapus Obat";
            this.btnHapus.UseVisualStyleBackColor = false;
            // 
            // ProductCatalogControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlCrud);
            this.Controls.Add(this.dgvMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "ProductCatalogControl";
            this.Size = new System.Drawing.Size(1110, 670);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.pnlCrud.ResumeLayout(false);
            this.pnlCrud.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
