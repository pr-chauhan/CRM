using EntityClass;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace Electra_WebApi.Reports
{
    public partial class DataWiseInvoiceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int searchText = 0;

                if (Request.QueryString["searchText"] != null)
                {
                    searchText = Convert.ToInt32(Request.QueryString["searchText"]);
                }

                //List<Invoice> InvMain = null;
                using (var _context = new CraModel())
                {
                    var fromdate = StaticVariables.From_Date ;
                    var todate =  StaticVariables.To_Date ;
                    var consignee_id = StaticVariables.Consignee_ID ;

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    var InvMain = GetData("exec SP_DatewiseInvoice "+ consignee_id + ",'"+ fromdate + "','"+ todate + "'");
                    //InvMain = _context.Invoices.Where(t => t.Invoice_ID == 1 && t.Financial_Yr =="2021-2022").ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DataWiseInvoiceList.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("InvoiceDataSet", InvMain.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
                    //ReportViewer1.LocalReport.Refresh();
                    //ReportViewer1.DataBind();
                    Open();
                }
            }
        }
        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            var Financial_yr = e.Parameters[0].Values[0];
            var Invoice_ID = Convert.ToInt32(e.Parameters[1].Values[0]);
            var InvMain = GetData("exec SP_DatewiseInvoiceSubReport "+ Invoice_ID + ",'" + Financial_yr + "'");
            if (e.ReportPath == "InvoiceDatewiseDetail")
            {
                //var eDetails = new ReportDataSource() { Name = "InvoiceDataSet", Value = InvMain };
                ReportDataSource rdc = new ReportDataSource("InvoiceDataSet", InvMain.Tables[0]);
                e.DataSources.Add(rdc);
            }
        }
        private DataSet GetData(string query)
        {
            string conString = ConfigurationManager.ConnectionStrings["CraModel"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;

                }
            }
        }
        protected void Open()
        {
            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            // Open generated PDF.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}
