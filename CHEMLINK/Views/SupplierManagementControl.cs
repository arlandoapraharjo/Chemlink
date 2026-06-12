using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class SupplierManagementControl : UserControl
    {
        public event EventHandler<SupplierEventArgs>? AddSupplierEvent;

        public SupplierManagementControl()
        {
            InitializeComponent();
            btnAddSup.Click += BtnAddSup_Click;
        }

        public void SetData(List<Supplier> suppliers)
        {
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = suppliers;
        }

        private void BtnAddSup_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaSup.Text)) return;
            AddSupplierEvent?.Invoke(this, new SupplierEventArgs { Name = txtNamaSup.Text, Phone = txtTelp.Text, Address = txtAlamat.Text });
            txtNamaSup.Clear(); txtTelp.Clear(); txtAlamat.Clear();
        }
    }
}
