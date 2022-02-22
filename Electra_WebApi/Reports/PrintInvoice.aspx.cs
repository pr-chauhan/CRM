﻿using EntityClass;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
                    searchText = Convert.ToInt32(Request.QueryString["searchText"]);
                }

                //List<Invoice> InvMain = null;
                using (var _context = new CraModel())
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    var InvMain = GetData("exec SP_PrintInvoice 822, '2017-2018'");
                    //InvMain = _context.Invoices.Where(t => t.Invoice_ID == 1 && t.Financial_Yr =="2021-2022").ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportInvoice.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("InvoiceDataSet", InvMain.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    //ReportViewer1.LocalReport.Refresh();
                    //ReportViewer1.DataBind();
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