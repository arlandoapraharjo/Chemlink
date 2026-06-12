using System;
using System.Data;
using System.Windows.Forms;

namespace CHEMLINK.Views
{
    public partial class FinancialReportControl : UserControl
    {
        public FinancialReportControl()
        {
            InitializeComponent();
        }

        public void SetData(DataTable report)
        {
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = report;
        }
    }
}
