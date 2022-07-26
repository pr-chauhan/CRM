﻿using EntityClass;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;


namespace Electra_WebApi.Reports
{
    public partial class ItemwiseSummary : System.Web.UI.Page
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
                    var from_dt = StaticVariables.From_Date;
                    var to_date = StaticVariables.To_Date;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    var InvMain = GetData("exec SP_Summary '"+ from_dt + "','"+ to_date + "'");
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ItemwiseSummary.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("SummaryDataSet", InvMain.Tables[0]);
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