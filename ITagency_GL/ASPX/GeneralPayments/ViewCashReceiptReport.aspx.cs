﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace ITagency_GL.ASPX.GeneralPayments
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["vwCashRecieptsReport"] != null)
                {
                    DataTable dt = (DataTable)Session["vwCashRecieptsReport"];
                        dt.Rows[0]["AmountText"] = new NumberToWords.NumberToWords().GetAmountWord(decimal.Parse(dt.Rows[0]["Credit"].ToString()), NumberToWords.NumberToWords.ValueTypes.Money);

                    string rptFullPath = Server.MapPath("rptCashReciepts.rpt");
                    ReportDocument rpt = new ReportDocument();

                    rpt.Load(rptFullPath);
                    rpt.SetDataSource(dt);
                    rpt.Refresh();

                    MemoryStream memStream = (MemoryStream)rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(memStream.ToArray());
                    Response.End();

                    memStream.Dispose();
                }
                else
                    Response.Redirect("Payments.aspx", false);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                Session.Remove("vwCashRecieptsReport");
            }
        }
    }
}