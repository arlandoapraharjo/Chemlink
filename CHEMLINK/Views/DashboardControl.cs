using System;
using System.Data;
using System.Windows.Forms;

namespace CHEMLINK.Views
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
        }

        public void SetData(int totalProduk, int stokKritis, DataTable dtNotif)
        {
            lblValTotal.Text = $"{totalProduk} Item";
            lblValKritis.Text = $"{stokKritis} Item";
            dgvMain.DataSource = dtNotif;
        }
    }
}
