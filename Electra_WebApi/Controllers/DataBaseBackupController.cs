﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class DataBaseBackupController : Controller
    {
        // GET: DataBaseBackup
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            try
            {
                string backlocation = Server.MapPath("~/BackupFolder/");
                String query = "backup database Inventory to disk='" + backlocation + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'";
                String mycon = "Data Source=lap-150; Initial Catalog=Inventory; Integrated Security=true";
                SqlConnection con = new SqlConnection(mycon);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch 
            {

            }
            return View();
        }

    }
}