using EntityClass;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Electra_WebApi
{
    public partial class PrintInvoice : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int searchText = 0;

                if (Request.QueryString["searchText"] != null)
                {
                    searchText = Convert.ToInt32( Request.QueryString["searchText"]);
                }

                List<Invoice> InvMain = null;
                using (var _context = new CraModel())
                {
                    InvMain = _context.Invoices.Where(t => t.Invoice_ID == searchText).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportInvoice.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("Invoice", InvMain);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.DataBind();
                }
            }
        }
    }
}