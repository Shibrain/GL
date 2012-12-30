using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Shared;

namespace ITagency_GL.ASPX.Journals
{ 
    public partial class Journals : System.Web.UI.Page
    {
        BLL.Journal.Journal Journal = new BLL.Journal.Journal();
        BLL.Accounts.AccountingTree AccountingTree = new BLL.Accounts.AccountingTree();
       // private Journal Journal;
        private int transactionID;
        private string finPeriodName;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearControls(sender);
                    ddlCurrencies_SelectedIndexChanged(sender, new EventArgs());
                }


            }
            catch (Exception ex)
            {

                lblFeedback.Text = ex.Message;
                lblFeedback.ForeColor = System.Drawing.Color.Red;
            }
        }
        private void ClearRow()
        {
            ddlAccounts.SelectedValue = "0";

            txtCredit.Text = "0";
            txtDebit.Text = "0";

            //lblSumCredit.Text = "";
            //lblSumDebit.Text = "";
            txtDescription.Text = "";

            //txtDocumentDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtDocumentNo.Text = "";
            txtDocumentDate.SelectedDate = DateTime.Today;
            ddlCurrencies.SelectedValue = BLL.Currencies.Currencies.GetDefaultCurrency(int.Parse(Session["CompId"].ToString())).ToString();
            
        }


        private void ClearControls(object sender)
        {
            transactionID = 0;

            ClearRow();

            //txtJournalDate.Text = "";
            //txtJournalNo.Text = "";
            //lblTransaction.Text = "0";

            //txtDocumentDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //txtDocumentNo.Text = "";

            ddlProfitCenters_SelectedIndexChanged(sender, new EventArgs());
           
            string userDefaultProfitCenter = BLL.Profit_Center.Profit_Center.GetUserDefaultProfitCenter(int.Parse(Session["UserID"].ToString())).ToString();
            
            ddlProfitCenters.SelectedValue = userDefaultProfitCenter;

            txtJournalNo.Text =  Journal.GetNewJournalID(int.Parse(userDefaultProfitCenter)).ToString();
           
          
            txtDocumentNo.Text = txtJournalNo.Text;

            txtJournalDate.SelectedDate = DateTime.Today; //.ToString("dd/MM/yyyy");

            //txtJournalNo.Text = new Journal().GetNewJournalID().ToString();
            //txtDocumentNo.Text = txtJournalNo.Text;
            txtDocumentDate.SelectedDate = DateTime.Today;
            ddlCurrencies.SelectedValue = BLL.Profit_Center.Profit_Center.GetUserDefaultProfitCenter(int.Parse(Session["CompId"].ToString())).ToString();


            Session.Remove("dtJournal");
            gvJournal.DataSource = null;
            gvJournal.DataBind();

            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            btnPrint.Enabled = false;

            lblBalance.Text = "";
            lblSumCredit.Text = "";
            lblSumDebit.Text = "";

            //DataTable dtYear = new Journal().GetFinancialPeriod();

            DataTable dtYear = BLL.Financial_Period.Financial_Period.GetFinancialPeriod(int.Parse(Session["CompId"].ToString()));
            if (dtYear.Rows.Count >= 1)
            {
                DataRow dr = dtYear.Rows[0];

                lblFinancialYearID.Text = dr["PeriodId"].ToString();
                //finPeriodStartDate = dr["StartDate"].ToString();
                //finPeriodEndDate = dr["EndDate"].ToString();
                finPeriodName = dr["PeriodName"].ToString();

                lblFPeriod.Text = finPeriodName;
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                int newJournalID = Journal.GetNewJournalID(int.Parse(Session["CompId"].ToString())); //GetJournalId() + 1;
                int currentUserID = int.Parse(Session["UserID"].ToString());
                transactionID = int.Parse(lblTransaction.Text);

                decimal balance = decimal.Parse(lblBalance.Text);
                if (balance != 0)
                    throw new Exception("لايمكن الحفظ لعدم توازن رصيد قيد اليومية!");

                if (newJournalID > 0 && transactionID > 0)
                {
                    // int result = , int clearingResult = 
                    Journal.PostGLDetailsFromTemp(transactionID);
                    Journal.ClearTempGLDetails(transactionID, currentUserID);
                    ClearControls(sender);

                    btnSave.Enabled = false;
                    btnAdd.Enabled = false;
                    btnPrint.Enabled = true;
                }
                else
                    lblFeedback.Text = "الرجاء اختيار قيد اليومية!";

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
                transactionID = int.Parse(lblTransaction.Text);

                if (transactionID == 0)
                    lblFeedback.Text = "الرجاء الاعتماد/الترحيل قبل طباعة التقرير!";
                else
                {
                    DataTable dtTempPostedJournal =  Journal.GetPostedTempGLDetails(transactionID, int.Parse(Session["UserID"].ToString()));
                    if (dtTempPostedJournal.Rows.Count > 0)
                    {
                        Session["vwGLMasterDetailed"] = dtTempPostedJournal;

                        string url = "ViewPostedGLReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewPostedGLReport", "window.open('" + url + "', '_new')", true);

                        btnAdd.Enabled = true;
                        btnSave.Enabled = false;
                        btnClear.Enabled = false;
                        btnPrint.Enabled = false;

                        lblTransaction.Text = "0";
                    }
                    else
                        lblFeedback.Text ="No Data";
                }

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                
                int newJournalID = Journal.GetNewJournalID(int.Parse(Session["CompId"].ToString())); //GetSubJournalId(int.Parse(ddlJType.SelectedValue)) + 1;
                int currentUserID = int.Parse(Session["UserID"].ToString());

                Journal.ClearTempGLDetails(transactionID, currentUserID);

                ClearControls(sender);
                ddlCurrencies_SelectedIndexChanged(sender, new EventArgs());

                lblTransaction.Text = "0";
            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }


        protected void gvJournal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvJournal.PageIndex = e.NewPageIndex;
                gvJournal.DataSource = (DataTable)Session["dtJournal"];
                gvJournal.DataBind();
            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void gvJournal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                decimal balance = 0;
              

                int currentUserID = int.Parse(Session["UserID"].ToString());
                decimal sumCredit = Journal.SumTempCredit(transactionID, currentUserID);
                decimal sumDebit = Journal.SumTempDebit(transactionID, currentUserID);


                if (sumCredit > sumDebit)
                    balance = sumCredit - sumDebit;
                else if (sumDebit > sumCredit)
                    balance = sumDebit - sumCredit;
                else
                    balance = 0;


                lblSumCredit.Text = sumCredit.ToString("N2");
                lblSumDebit.Text = sumDebit.ToString("N2");
                lblBalance.Text = balance.ToString("N2");

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void odsJournal_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                int currentUserID = int.Parse(Session["UserID"].ToString());
                int transactionID = int.Parse(lblTransaction.Text);

                lblSumCredit.Text = Journal.SumTempCredit(transactionID, currentUserID).ToString();
                lblSumDebit.Text = Journal.SumTempDebit(transactionID, currentUserID).ToString();
            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void ddlCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int defaultCurrency = BLL.Profit_Center.Profit_Center.GetUserDefaultProfitCenter(int.Parse(Session["CompId"].ToString()));
                lblExchangeRate.Text = BLL.Currencies.Currencies.GetCurrencyConversionRatio(int.Parse(ddlCurrencies.SelectedValue), defaultCurrency, int.Parse(Session["CompId"].ToString())).ToString("N2");
              
            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void ddlCurrencies_DataBound(object sender, EventArgs e)
        {
            //ddlCurrencies.SelectedValue = "0";
            ddlCurrencies_SelectedIndexChanged(sender, new EventArgs());
        }

        protected void ddlProfitCenters_DataBound(object sender, EventArgs e)
        {
            try
            {
                if (ddlProfitCenters.Items.Count > 1)
                {
                    ddlProfitCenters.SelectedIndex = -1;
                    ddlProfitCenters.SelectedValue = BLL.Profit_Center.Profit_Center.GetUserDefaultProfitCenter(int.Parse(Session["UserID"].ToString())).ToString();
                    ddlProfitCenters_SelectedIndexChanged(sender, new EventArgs());
                }

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void ddlProfitCenters_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlAccounts.Items.Clear();
                ddlAccounts.Items.Add(new ListItem("اختيار", "0"));
                ddlAccounts.DataBind();

                //lblTransaction.Text = Journal.GetOpeningBalancesTransactionID(int.Parse(ddlProfitCenters.SelectedValue)).ToString();
                txtJournalNo.Text =  Journal.GetNewJournalID(int.Parse(ddlProfitCenters.SelectedValue)).ToString();
                txtDocumentNo.Text = txtJournalNo.Text;
            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void btnAdd_Click1(object sender, EventArgs e)
        {
            try
            {
                lblFeedback.Text = "";
                int currentUserID = int.Parse(Session["UserID"].ToString());
                int selectedAccount = int.Parse(ddlAccounts.SelectedValue);
                bool isValidAccount = AccountingTree.CheckAccountIsOpenNode(selectedAccount);
                bool userIsPermittedToAccount = Policy.CheckUserIsPermittedToAccountByCenterID(currentUserID, selectedAccount);


                if (txtCredit.Text == string.Empty && txtDebit.Text == string.Empty)
                    throw new Exception("الرجاء إدخال أياً من مبلغ المدين أو الدائن!");
                else if (txtCredit.Text == string.Empty)
                    txtCredit.Text = "0";
                else if (txtDebit.Text == string.Empty)
                    txtDebit.Text = "0";
                else if (!isValidAccount)
                    throw new Exception("أحد أو كلا الحسابين المختارين قد يكون مغلقاً أو حساباً إجمالياً! الرجاء اختيار حساب تفصيلي مفتوح!");
                else if (!userIsPermittedToAccount)
                    lblFeedback.Text = "المستخدم الحالي غير مصرح له بالتعامل مع هذا الحساب";
                else
                {

                    bool JournalCodeExists = false;

                    int centerID = int.Parse(ddlProfitCenters.SelectedValue);
                    int transactionID = int.Parse(lblTransaction.Text);
                    int finYearID = int.Parse(lblFinancialYearID.Text);

                    if (transactionID == 0)
                    {
                        JournalCodeExists = Journal.CheckSubjournalCodeExists(txtJournalNo.Text);
                        if (!JournalCodeExists)
                            transactionID = Journal.PostToGlMaster(finYearID, txtJournalNo.Text, txtJournalDate.SelectedDate.ToString(),
                                currentUserID, "", centerID, false, int.Parse(Session["CompId"].ToString()));

                        //transactionID = Journal.PostToGlMaster(int.Parse(lblFinancialYearID.Text), int.Parse(txtJournalNo.Text.Split('-')[1]),
                        //    txtJournalNo.Text, txtJournalDate.Text, currentUserID, int.Parse(ddlJType.SelectedValue));
                        else
                            transactionID = Journal.GetTransactionIDInGLMaster(txtJournalNo.Text);
                    }
                    else
                        transactionID = Journal.GetTransactionIDInGLMaster(txtJournalNo.Text);


                    lblTransaction.Text = transactionID.ToString();

                    decimal debitAmount = 0;
                    decimal creditAmount = 0;
                    decimal initialDebitAmount = decimal.Parse(txtDebit.Text);
                    decimal initialCreditAmount = decimal.Parse(txtCredit.Text);

                    int defaultCurrency = BLL.Profit_Center.Profit_Center.GetUserDefaultProfitCenter(int.Parse(Session["CompId"].ToString())); //int.Parse(db.ExecuteScalar(defaultCurrencySql).ToString());
                    if (int.Parse(ddlCurrencies.SelectedValue) != defaultCurrency)
                    {
                        decimal ratio = BLL.Currencies.Currencies.GetCurrencyConversionRatio(defaultCurrency, int.Parse(ddlAccounts.SelectedValue), int.Parse(Session["CompId"].ToString()));
                        debitAmount = initialDebitAmount * ratio;
                        creditAmount = initialCreditAmount * ratio;
                    }
                    else
                    {
                        debitAmount = initialDebitAmount;
                        creditAmount = initialCreditAmount;
                    }



                    // int.Parse(txtJournalNo.Text), 
                    int rows = Journal.PostTempGlDetails(transactionID, txtJournalDate.SelectedDate.ToString(), int.Parse(ddlAccounts.SelectedValue),
                        finYearID, debitAmount, creditAmount, txtDescription.Text, currentUserID, txtDocumentNo.Text, txtDocumentDate.SelectedDate.ToString());


                    if (rows > 0)
                    {
                        decimal balance = 0;
                        decimal sumCredit = Journal.SumTempCredit(transactionID, currentUserID);
                        decimal sumDebit = Journal.SumTempDebit(transactionID, currentUserID);


                        if (sumCredit > sumDebit)
                            balance = sumCredit - sumDebit;
                        else if (sumDebit > sumCredit)
                            balance = sumDebit - sumCredit;
                        else
                            balance = 0;


                        lblSumCredit.Text = sumCredit.ToString("N2");
                        lblSumDebit.Text = sumDebit.ToString("N2");
                        lblBalance.Text = balance.ToString("N2");

                        //odsJournal.Select();
                        gvJournal.DataBind();

                        btnSave.Enabled = true;
                        btnClear.Enabled = true;


                        ClearRow();
                        ddlCurrencies_SelectedIndexChanged(sender, new EventArgs());
                    }
                    else
                        lblFeedback.Text = Feedback.InsertException();
                }

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            try
            {
                
                int newJournalID = Journal.GetNewJournalID(int.Parse(Session["CompId"].ToString())); //GetJournalId() + 1;
                int currentUserID = int.Parse(Session["UserID"].ToString());
                transactionID = int.Parse(lblTransaction.Text);

                decimal balance = decimal.Parse(lblBalance.Text);
                if (balance != 0)
                    throw new Exception("لايمكن الحفظ لعدم توازن رصيد قيد اليومية!");

                if (newJournalID > 0 && transactionID > 0)
                {
                    // int result = , int clearingResult = 
                    Journal.PostGLDetailsFromTemp(transactionID);
                    Journal.ClearTempGLDetails(transactionID, currentUserID);
                    ClearControls(sender);

                    btnSave.Enabled = false;
                    btnAdd.Enabled = false;
                    btnPrint.Enabled = true;
                }
                else
                    lblFeedback.Text = "الرجاء اختيار قيد اليومية!";

            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void btnClear_Click1(object sender, EventArgs e)
        {
            try
            {
          
                int newJournalID = Journal.GetNewJournalID(int.Parse(Session["Compid"].ToString())); //GetSubJournalId(int.Parse(ddlJType.SelectedValue)) + 1;
                int currentUserID = int.Parse(Session["UserID"].ToString());

                Journal.ClearTempGLDetails(transactionID, currentUserID);

                ClearControls(sender);
                ddlCurrencies_SelectedIndexChanged(sender, new EventArgs());

                lblTransaction.Text = "0";
            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        protected void btnPrint_Click1(object sender, EventArgs e)
        {
            try
            {
                transactionID = int.Parse(lblTransaction.Text);

                if (transactionID == 0)
                    lblFeedback.Text = "الرجاء الاعتماد/الترحيل قبل طباعة التقرير!";
                else
                {
                    DataTable dtTempPostedJournal = Journal.GetPostedTempGLDetails(transactionID, int.Parse(Session["UserID"].ToString()));
                    if (dtTempPostedJournal.Rows.Count > 0)
                    {
                        Session["vwGLMasterDetailed"] = dtTempPostedJournal;

                        string url = "ViewPostedGLReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewPostedGLReport", "window.open('" + url + "', '_new')", true);

                        btnAdd.Enabled = true;
                        btnSave.Enabled = false;
                        btnClear.Enabled = false;
                        btnPrint.Enabled = false;

                        lblTransaction.Text = "0";
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