using EntityClass;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

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
                    searchText = Convert.ToInt32(Request.QueryString["searchText"]);
                }
                using (var _context = new CraModel())
                {
                    var invoice_No = StaticVariables.Invoice_No;
                    var financial_yr = StaticVariables.Financial_Year;
                    var optionSel = StaticVariables.Option;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    var InvMain = GetData("exec SP_PrintInvoice "+ invoice_No + ", '"+ financial_yr + "'");
                    var totExcise = InvMain.Tables[0].AsEnumerable().Select(x => x.Field<double>("TOTAL_EXCISE")).FirstOrDefault();
                    var totamt = InvMain.Tables[0].AsEnumerable().Select(x => x.Field<double>("total_amount")).FirstOrDefault();
                    var tcsVal = InvMain.Tables[0].AsEnumerable().Select(x => x.Field<decimal>("tcsval")).FirstOrDefault();
                    totamt += Convert.ToDouble(tcsVal);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportInvoice.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("InvoiceDataSet", InvMain.Tables[0]);
                    ReportParameter[] parameters = new ReportParameter[4];
                    parameters[0] = new ReportParameter("headng", optionSel);
                    parameters[1] = new ReportParameter("h1", string.Empty);
                    parameters[2] = new ReportParameter("totamt", WebApiApplication.objCommon.words_money(totamt));
                    parameters[3] = new ReportParameter("excamt", WebApiApplication.objCommon.words_money(totExcise));
                    ReportViewer1.LocalReport.SetParameters(parameters);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    Open();
                }
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
            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);
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