namespace CHEMLINK.Views
{
    partial class POSControl
    {
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.DataGridView dgvMain;
        public System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Panel pnlCrud;
        public System.Windows.Forms.ComboBox cbCategoryFilter;
        public System.Windows.Forms.TextBox txtSearch;
        public System.Windows.Forms.TextBox txtQty;
        public System.Windows.Forms.Button btnAddCart;
        public System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Label lblKategori;
        private System.Windows.Forms.Label lblCari;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dgvMain = new DataGridView();
            dgvCart = new DataGridView();
            pnlCrud = new Panel();
            lblKategori = new Label();
            cbCategoryFilter = new ComboBox();
            lblCari = new Label();
            txtSearch = new TextBox();
            txtQty = new TextBox();
            btnAddCart = new Button();
            btnCheckout = new Button();
            btnDelCart = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            pnlCrud.SuspendLayout();
            SuspendLayout();
            // 
            // dgvMain
            // 
            dgvMain.AllowUserToAddRows = false;
            dgvMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMain.BackgroundColor = Color.White;
            dgvMain.BorderStyle = BorderStyle.None;
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(72, 161, 17);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvMain.DefaultCellStyle = dataGridViewCellStyle1;
            dgvMain.Location = new Point(20, 280);
            dgvMain.MultiSelect = false;
            dgvMain.Name = "dgvMain";
            dgvMain.ReadOnly = true;
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.Size = new Size(1070, 370);
            dgvMain.TabIndex = 0;
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.BackgroundColor = Color.WhiteSmoke;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(72, 161, 17);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvCart.DefaultCellStyle = dataGridViewCellStyle2;
            dgvCart.Location = new Point(15, 60);
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(1040, 160);
            dgvCart.TabIndex = 7;
            // 
            // pnlCrud
            // 
            pnlCrud.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlCrud.BackColor = Color.White;
            pnlCrud.Controls.Add(btnDelCart);
            pnlCrud.Controls.Add(lblKategori);
            pnlCrud.Controls.Add(cbCategoryFilter);
            pnlCrud.Controls.Add(lblCari);
            pnlCrud.Controls.Add(txtSearch);
            pnlCrud.Controls.Add(txtQty);
            pnlCrud.Controls.Add(btnAddCart);
            pnlCrud.Controls.Add(btnCheckout);
            pnlCrud.Controls.Add(dgvCart);
            pnlCrud.Location = new Point(20, 20);
            pnlCrud.Name = "pnlCrud";
            pnlCrud.Size = new Size(1070, 240);
            pnlCrud.TabIndex = 1;
            // 
            // lblKategori
            // 
            lblKategori.AutoSize = true;
            lblKategori.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKategori.Location = new Point(15, 20);
            lblKategori.Name = "lblKategori";
            lblKategori.Size = new Size(71, 19);
            lblKategori.TabIndex = 0;
            lblKategori.Text = "Kategori:";
            // 
            // cbCategoryFilter
            // 
            cbCategoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategoryFilter.FormattingEnabled = true;
            cbCategoryFilter.Items.AddRange(new object[] { "Semua Kategori", "Herbisida", "Fungisida", "Insektisida", "Pupuk" });
            cbCategoryFilter.Location = new Point(90, 17);
            cbCategoryFilter.Name = "cbCategoryFilter";
            cbCategoryFilter.Size = new Size(150, 25);
            cbCategoryFilter.TabIndex = 1;
            // 
            // lblCari
            // 
            lblCari.AutoSize = true;
            lblCari.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCari.Location = new Point(260, 20);
            lblCari.Name = "lblCari";
            lblCari.Size = new Size(40, 19);
            lblCari.TabIndex = 2;
            lblCari.Text = "Cari:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(305, 17);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Cari Produk (Enter)";
            txtSearch.Size = new Size(180, 25);
            txtSearch.TabIndex = 3;
            // 
            // txtQty
            // 
            txtQty.Location = new Point(505, 17);
            txtQty.Name = "txtQty";
            txtQty.PlaceholderText = "Jumlah";
            txtQty.Size = new Size(80, 25);
            txtQty.TabIndex = 4;
            // 
            // btnAddCart
            // 
            btnAddCart.BackColor = Color.FromArgb(72, 161, 17);
            btnAddCart.FlatStyle = FlatStyle.Flat;
            btnAddCart.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddCart.ForeColor = Color.White;
            btnAddCart.Location = new Point(605, 14);
            btnAddCart.Name = "btnAddCart";
            btnAddCart.Size = new Size(150, 33);
            btnAddCart.TabIndex = 5;
            btnAddCart.Text = "Tambahkan";
            btnAddCart.UseVisualStyleBackColor = false;
            // 
            // btnCheckout
            // 
            btnCheckout.BackColor = Color.FromArgb(255, 152, 0);
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(922, 14);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(120, 33);
            btnCheckout.TabIndex = 6;
            btnCheckout.Text = "Bayar && Cetak";
            btnCheckout.UseVisualStyleBackColor = false;
            // 
            // btnDelCart
            // 
            btnDelCart.BackColor = Color.FromArgb(220, 38, 38);
            btnDelCart.FlatStyle = FlatStyle.Flat;
            btnDelCart.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelCart.ForeColor = Color.White;
            btnDelCart.Location = new Point(778, 14);
            btnDelCart.Name = "btnDelCart";
            btnDelCart.Size = new Size(120, 33);
            btnDelCart.TabIndex = 8;
            btnDelCart.Text = "Hapus";
            btnDelCart.UseVisualStyleBackColor = false;
            // 
            // POSControl
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(240, 243, 246);
            Controls.Add(pnlCrud);
            Controls.Add(dgvMain);
            Font = new Font("Segoe UI", 10F);
            Name = "POSControl";
            Size = new Size(1110, 670);
            ((System.ComponentModel.ISupportInitialize)dgvMain).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            pnlCrud.ResumeLayout(false);
            pnlCrud.PerformLayout();
            ResumeLayout(false);

        }

        public Button button1;
        public Button btnDelCart;
    }
}
