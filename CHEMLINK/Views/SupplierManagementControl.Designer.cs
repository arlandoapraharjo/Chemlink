namespace CHEMLINK.Views
{
    partial class SupplierManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Panel pnlCrud;
        public System.Windows.Forms.Button btnAddSup;
        public System.Windows.Forms.Button btnEditSup;
        public System.Windows.Forms.Button btnDeleteSup;
        public System.Windows.Forms.Button btnToggleEdit;

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
            dgvMain = new DataGridView();
            pnlCrud = new Panel();
            btnDelSup1 = new Button();
            btnEditSup1 = new Button();
            btnAddSup = new Button();
            btnEditSup = new Button();
            btnDeleteSup = new Button();
            btnToggleEdit = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMain).BeginInit();
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
            dgvMain.Location = new Point(20, 20);
            dgvMain.MultiSelect = false;
            dgvMain.Name = "dgvMain";
            dgvMain.ReadOnly = true;
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.Size = new Size(1070, 540);
            dgvMain.TabIndex = 0;
            // 
            // pnlCrud
            // 
            pnlCrud.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlCrud.BackColor = Color.White;
            pnlCrud.Controls.Add(btnAddSup);
            pnlCrud.Controls.Add(btnEditSup1);
            pnlCrud.Controls.Add(btnDelSup1);
            pnlCrud.Location = new Point(20, 580);
            pnlCrud.Name = "pnlCrud";
            pnlCrud.Size = new Size(1070, 60);
            pnlCrud.TabIndex = 1;
            // 
            // 
            // btnAddSup
            // 
            btnAddSup.BackColor = Color.FromArgb(72, 161, 17);
            btnAddSup.FlatStyle = FlatStyle.Flat;
            btnAddSup.ForeColor = Color.White;
            btnAddSup.Location = new Point(20, 12);
            btnAddSup.Name = "btnAddSup";
            btnAddSup.Size = new Size(142, 35);
            btnAddSup.TabIndex = 3;
            btnAddSup.Text = "+ Tambah Supplier";
            btnAddSup.UseVisualStyleBackColor = false;
            // 
            // btnEditSup1
            // 
            btnEditSup1.BackColor = Color.FromArgb(33, 150, 243);
            btnEditSup1.FlatStyle = FlatStyle.Flat;
            btnEditSup1.ForeColor = Color.White;
            btnEditSup1.Location = new Point(172, 12);
            btnEditSup1.Name = "btnEditSup1";
            btnEditSup1.Size = new Size(142, 35);
            btnEditSup1.TabIndex = 4;
            btnEditSup1.Text = "✏ Edit Supplier";
            btnEditSup1.UseVisualStyleBackColor = false;
            // 
            // btnDelSup1
            // 
            btnDelSup1.BackColor = Color.FromArgb(244, 67, 54);
            btnDelSup1.FlatStyle = FlatStyle.Flat;
            btnDelSup1.ForeColor = Color.White;
            btnDelSup1.Location = new Point(324, 12);
            btnDelSup1.Name = "btnDelSup1";
            btnDelSup1.Size = new Size(142, 35);
            btnDelSup1.TabIndex = 5;
            btnDelSup1.Text = "🗑 Hapus Supplier";
            btnDelSup1.UseVisualStyleBackColor = false;
            // 
            // btnEditSup
            // 
            btnEditSup.BackColor = Color.FromArgb(33, 150, 243);
            btnEditSup.FlatStyle = FlatStyle.Flat;
            btnEditSup.ForeColor = Color.White;
            btnEditSup.Location = new Point(690, 12);
            btnEditSup.Name = "btnEditSup";
            btnEditSup.Size = new Size(110, 35);
            btnEditSup.TabIndex = 4;
            btnEditSup.Text = "Edit Supplier";
            btnEditSup.UseVisualStyleBackColor = false;
            // 
            // btnDeleteSup
            // 
            btnDeleteSup.BackColor = Color.FromArgb(244, 67, 54);
            btnDeleteSup.FlatStyle = FlatStyle.Flat;
            btnDeleteSup.ForeColor = Color.White;
            btnDeleteSup.Location = new Point(810, 12);
            btnDeleteSup.Name = "btnDeleteSup";
            btnDeleteSup.Size = new Size(110, 35);
            btnDeleteSup.TabIndex = 5;
            btnDeleteSup.Text = "Hapus Supplier";
            btnDeleteSup.UseVisualStyleBackColor = false;
            // 
            // btnToggleEdit
            // 
            btnToggleEdit.BackColor = Color.Gray;
            btnToggleEdit.FlatStyle = FlatStyle.Flat;
            btnToggleEdit.ForeColor = Color.White;
            btnToggleEdit.Location = new Point(930, 12);
            btnToggleEdit.Name = "btnToggleEdit";
            btnToggleEdit.Size = new Size(120, 35);
            btnToggleEdit.TabIndex = 6;
            btnToggleEdit.Text = "Toggle Edit/Delete";
            btnToggleEdit.UseVisualStyleBackColor = false;
            // 
            // SupplierManagementControl
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(240, 243, 246);
            Controls.Add(pnlCrud);
            Controls.Add(dgvMain);
            Font = new Font("Segoe UI", 10F);
            Name = "SupplierManagementControl";
            Size = new Size(1110, 670);
            ((System.ComponentModel.ISupportInitialize)dgvMain).EndInit();
            pnlCrud.ResumeLayout(false);
            pnlCrud.PerformLayout();
            ResumeLayout(false);

        }

        public Button btnEditSup1;
        public Button btnDelSup1;
    }
}
