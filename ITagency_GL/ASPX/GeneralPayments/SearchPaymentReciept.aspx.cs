using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;

namespace ITagency_GL.ASPX.GeneralPayments
{
    public partial class SearchPaymentReciept : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                
                string str = ddlProfitCenters.SelectedValue.ToString();
                if (str == "0")
                    throw new Exception("الرجاء اختيار مركز الإيرادات");

                DataTable dt = Search();
                if (dt.Rows.Count > 0)
                {
                    gvResults.DataSource = dt;
                    gvResults.DataBind();
                }
                else
                {
                    gvResults.DataSource = null;
                    gvResults.DataBind();

                    lblFeedback.Text = "No Data";
                }

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
             
                if (radCashDisburment.Checked)
                {
                    DataTable dt = Search();
                    if (dt.Rows.Count > 0)
                    {
                        Session["vwCashDisbusrsesReport"] = dt;

                        string url = "ViewCashDisbursesReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewCashDisbursesReport", "window.open('" + url + "', '_new')", true);
                    }
                    else
                        lblFeedback.Text = "No Data";
                }
                else if (radCashRecipt.Checked)
                {
                    DataTable dt = Search();
                    if (dt.Rows.Count > 0)
                    {
                        Session["vwCashRecieptsReport"] = dt;

                        string url = "ViewCashReceiptReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewCashReceiptReport", "window.open('" + url + "', '_new')", true);
                    }
                    else
                        lblFeedback.Text = "No Data";
                }
                else if (radChequeDisburment.Checked)
                {
                    DataTable dt = Search();
                    if (dt.Rows.Count > 0)
                    {
                        Session["vwChequeDisbursementsReport"] = dt;

                        string url = "ViewChequeDisbursesReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewChequeDisbursesReport", "window.open('" + url + "', '_new')", true);
                    }
                    else
                        lblFeedback.Text = "No Data";
                }
                else if (radChequeRecipt.Checked)
                {
                    DataTable dt = Search();
                    if (dt.Rows.Count > 0)
                    {
                        Session["vwChequeRecieptsReport"] = dt;

                        string url = "ViewChequeRecieptReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewChequeRecieptReport", "window.open('" + url + "', '_new')", true);
                    }
                    else
                        lblFeedback.Text = "No Data";
                }
                else
                    lblFeedback.Text = "No Data";

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
                    
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                lblFeedback.Text = "";
                ddlProfitCenters.DataBind();

                txtFrom2.SelectedDate = null;
                txtTo2.SelectedDate = null;

                radCashRecipt.Checked = true;
                radCashDisburment.Checked = false;
                radChequeDisburment.Checked = false;
                radChequeRecipt.Checked = false;

                gvResults.DataSource = null;
                gvResults.DataBind();

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }
        private DatabaseClass db;

        protected Label lblFeedback
        {
            get { return (Label)Master.FindControl("lblFeedback"); }
        }

        public string ServerRootPath
        {
            get
            {
                string completePath = this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\");
                string applicationPath = this.Request.ApplicationPath.Replace("/", "\\");
                string rootPath = completePath.Replace(applicationPath, string.Empty);

                return rootPath;
            }
        }


        private DataTable Search()
        {
            db = new DatabaseClass();

            string sql = "";
            string orderByPart = "ORDER BY JournalCode ";

            if (radCashRecipt.Checked)
            {
                #region radCashRecipt

                sql = "SELECT * FROM vwCashRecieptsReport WHERE CR_No IS NOT NULL ";
                if (ddlProfitCenters.SelectedValue != "0")
                    sql = string.Format("{0} AND CenterID={1} ", sql, ddlProfitCenters.SelectedValue);

                if (txtFrom2.SelectedDate != null)
                    sql = string.Format("{0} AND TransactionDate>='{1}' ", sql, DatabaseClass.FormatDateString(txtFrom2.SelectedDate.ToString()));

                if (txtTo2.SelectedDate != null)
                    sql = string.Format(" {0} AND TransactionDate<='{1}' ", sql, DatabaseClass.FormatDateString(txtTo2.SelectedDate.ToString()));


                sql = string.Format("{0} {1} ", sql, orderByPart);
                return db.ExecuteQuery(sql);

                #endregion
            }
            else if (radChequeRecipt.Checked)
            {
                #region radChequeRecipt

                sql = "SELECT * FROM vwChequeRecieptsReport  WHERE CR_No IS NOT NULL ";
                if (ddlProfitCenters.SelectedValue != "0")
                    sql = string.Format("{0} AND CenterID={1} ", sql, ddlProfitCenters.SelectedValue);

                if (txtFrom2.SelectedDate != null)
                    sql = string.Format("{0} AND TransactionDate>='{1}' ", sql, DatabaseClass.FormatDateString(txtFrom2.SelectedDate.ToString()));

                if (txtTo2.SelectedDate != null)
                    sql = string.Format(" {0} AND TransactionDate<='{1}' ", sql, DatabaseClass.FormatDateString(txtTo2.SelectedDate.ToString()));


                sql = string.Format("{0} {1} ", sql, orderByPart);
                return db.ExecuteQuery(sql);

                #endregion
            }
            else if (radCashDisburment.Checked)
            {
                #region radCashDisburment

                sql = "SELECT * FROM vwCashDisbursementsReport  WHERE CD_No IS NOT NULL ";
                if (ddlProfitCenters.SelectedValue != "0")
                    sql = string.Format("{0} AND CenterID={1} ", sql, ddlProfitCenters.SelectedValue);

                if (txtFrom2.SelectedDate != null)
                    sql = string.Format("{0} AND TransactionDate>='{1}' ", sql, DatabaseClass.FormatDateString(txtFrom2.SelectedDate.ToString()));

                if (txtTo2.SelectedDate != null)
                    sql = string.Format(" {0} AND TransactionDate<='{1}' ", sql, DatabaseClass.FormatDateString(txtTo2.SelectedDate.ToString()));

                sql = string.Format("{0} {1} ", sql, orderByPart);
                return db.ExecuteQuery(sql);

                #endregion
            }
            else if (radChequeDisburment.Checked)
            {
                #region radChequeDisburment

                sql = "SELECT * FROM vwChequeDisbursementsReport  WHERE CD_No IS NOT NULL ";
                if (ddlProfitCenters.SelectedValue != "0")
                    sql = string.Format("{0} AND CenterID={1} ", sql, ddlProfitCenters.SelectedValue);

                if (txtFrom2.SelectedDate != null)
                    sql = string.Format("{0} AND TransactionDate>='{1}' ", sql, DatabaseClass.FormatDateString(txtFrom2.SelectedDate.ToString()));

                if (txtTo2.SelectedDate != null)
                    sql = string.Format(" {0} AND TransactionDate<='{1}' ", sql, DatabaseClass.FormatDateString(txtTo2.SelectedDate.ToString()));

                sql = string.Format("{0} {1} ", sql, orderByPart);
                return db.ExecuteQuery(sql);

                #endregion
            }
            else
                return new DataTable();
        }

        private static DataView GetData()
        {
            DataView view = (DataView)HttpContext.Current.Cache["Suggestions"];
            if (view == null)
            {
                string sql = "SELECT * FROM Emails ORDER BY EmailAddress ";
                view = new DatabaseClass().ExecuteQuery(sql).DefaultView;
                HttpContext.Current.Cache["Suggestions"] = view;
            }

            return view;
        }

        private static DataView FilterData(DataView view, string prefix)
        {
            view.RowFilter = string.Format("EmailAddress LIKE '%{0}%'", prefix);
            return view;
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            int i = 0;
            DataView data = GetData();
            data = FilterData(data, prefixText);
            string[] suggestions = new string[data.Count];

            foreach (DataRowView row in data)
                suggestions[i++] = row["EmailAddress"].ToString();

            return suggestions;
        }



        protected void ddlProfitCenters_DataBound(object sender, EventArgs e)
        {
            try
            {
                if (ddlProfitCenters.Items.Count > 1)
                {
                    ddlProfitCenters.SelectedIndex = -1;
                    ddlProfitCenters.SelectedValue = BLL.Profit_Center.Profit_Center.GetUserDefaultProfitCenter(int.Parse(Session["UserID"].ToString())).ToString();
                 
                }

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
                
            }
        }

        protected void ddlProfitCenters_DataBound1(object sender, EventArgs e)
        {
            try
            {
                if (ddlProfitCenters.Items.Count > 1)
                {
                    ddlProfitCenters.SelectedIndex = -1;
                    ddlProfitCenters.SelectedValue = BLL.Profit_Center.Profit_Center.GetUserDefaultProfitCenter(int.Parse(Session["UserID"].ToString())).ToString();
                }

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void radCashRecipt_CheckedChanged(object sender, EventArgs e)
        {
            gvResults.DataSource = null;
            gvResults.DataBind();
        }

        protected void radCashDisburment_CheckedChanged(object sender, EventArgs e)
        {
            gvResults.DataSource = null;
            gvResults.DataBind();
        }

        protected void radChequeRecipt_CheckedChanged(object sender, EventArgs e)
        {
            gvResults.DataSource = null;
            gvResults.DataBind();
        }

        protected void radChequehDisburment_CheckedChanged(object sender, EventArgs e)
        {
            gvResults.DataSource = null;
            gvResults.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvResults.SelectedValue != null)
                {
                    lblFeedback.Text = "";
                    popupPanel.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            

        }

        protected void gvResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SendByEmail")
                {

                }
                else if (e.CommandName == "Print")
                {
                    db = new DatabaseClass();

                    DataTable dtResult;
                    int transID = int.Parse(e.CommandArgument.ToString()); //int.Parse(gvResults.SelectedValue.ToString());

                    string sql = string.Format("SELECT TOP 1 IsCheque, IsCashReciept, isCashDisburment  FROM vwGLMasterDetailed WHERE transactionid={0} ", transID);
                    DataTable dt = db.ExecuteQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        if (bool.Parse(dr["IsCheque"].ToString()))
                        {
                            if (bool.Parse(dr["IsCashReciept"].ToString()))
                            {
                                sql = string.Format("SELECT * FROM vwChequeRecieptsReport WHERE TransactionId={0} AND CR_NO IS NOT NULL  ", transID);
                                dtResult = db.ExecuteQuery(sql);
                                if (dtResult.Rows.Count > 0)
                                {
                                    Session["vwChequeRecieptsReport"] = dtResult;

                                    string url = "ViewChequeRecieptReport.aspx";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewChequeRecieptReport", "window.open('" + url + "', '_new')", true);
                                }
                                else
                                    lblFeedback.Text = "No Data";

                            }
                            else if (bool.Parse(dr["isCashDisburment"].ToString()))
                            {
                                sql = string.Format("SELECT * FROM vwChequeDisbursementsReport WHERE TransactionId={0} AND CD_NO IS NOT NULL ", transID);
                                dtResult = db.ExecuteQuery(sql);
                                if (dtResult.Rows.Count > 0)
                                {
                                    Session["vwChequeDisbursementsReport"] = dtResult;

                                    string url = "ViewChequeDisbursesReport.aspx";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewChequeDisbursesReport.aspx", "window.open('" + url + "', '_new')", true);
                                }
                                else
                                    lblFeedback.Text = "No Data";
                            }
                            else
                                lblFeedback.Text = "No Data";
                        }
                        else
                        {
                            if (bool.Parse(dr["IsCashReciept"].ToString()))
                            {
                                sql = string.Format("SELECT * FROM vwCashRecieptsReport WHERE TransactionId={0} AND CR_NO IS NOT NULL  ", transID);
                                dtResult = db.ExecuteQuery(sql);
                                if (dtResult.Rows.Count > 0)
                                {
                                    Session["vwCashRecieptsReport"] = dtResult;

                                    string url = "ViewCashReceiptReport.aspx";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewCashReceiptReport", "window.open('" + url + "', '_new')", true);
                                }
                                else
                                    lblFeedback.Text = "No Data";

                            }
                            else if (bool.Parse(dr["isCashDisburment"].ToString()))
                            {
                                sql = string.Format("SELECT * FROM vwCashDisbursementsReport WHERE TransactionId={0} AND CD_NO IS NOT NULL  ", transID);
                                dtResult = db.ExecuteQuery(sql);
                                if (dtResult.Rows.Count > 0)
                                {
                                    Session["vwCashDisbusrsesReport"] = dtResult;

                                    string url = "ViewCashDisbursesReport.aspx";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewCashDisbursesReport", "window.open('" + url + "', '_new')", true);
                                }
                                else
                                    lblFeedback.Text = "No Data";
                            }
                            else
                                lblFeedback.Text = "No Data";
                        }
                    }
                    else
                        lblFeedback.Text = "No Data";
                }

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

       
    }
}