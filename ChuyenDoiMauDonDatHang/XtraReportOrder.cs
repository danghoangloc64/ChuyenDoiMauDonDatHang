using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ChuyenDoiMauDonDatHang
{
    public partial class XtraReportOrder : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportOrder()
        {
            InitializeComponent();
        }

        public XtraReportOrder(DataOrderReportModel dataOrderReportModel)
        {
            InitializeComponent();
            objectDataSource1.DataSource = dataOrderReportModel;
        }
    }
}
