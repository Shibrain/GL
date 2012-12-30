using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Shared;
using DAL;

namespace ITagency_GL.ASPX.GeneralPayments
{
    public partial class PaymentsReceipts : System.Web.UI.Page
    {
        BLL.Journal.Journal journal = new BLL.Journal.Journal();
        private DatabaseClass db = new DatabaseClass();
        private int selectedProfitCenter;
        private int transactionID; //= 0;
        private string finPeriodName;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //if (Session["UserPolicy"] == null || Session["UserPolicy"].ToString()[2] != '1')
                    //    Response.Redirect("~/ASPX/Default.aspx", false);
                   // else
                    {
                        ClearControls(sender);
                        ddlChequeStatus.Enabled = false;
                        ddlChequeStatus.Items.Clear();
                        ddlChequeStatus.Items.Add(new ListItem("Select", "0"));
                    }
                
                }

            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
            }
        }
        protected void radChequehDisburment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblFeedBack.Text = "";
                ddlChequeStatus.Enabled = true;
                ddlChequeStatus.Items.Clear();
                ddlChequeStatus.Items.Add(new ListItem("Select", "0"));
                ddlChequeStatus.Items.Add(new ListItem("تم التحصيل", "2"));
                ddlChequeStatus.Items.Add(new ListItem("راجع", "3"));
                ddlChequeStatus.Items.Add(new ListItem("آجل", "4"));
                pnlCheques.Visible = true;
               // ddlCashBankAccounts.SelectedValue = new BLL.Accounts.AccountingTree().GetBankAccount(int.Parse(ddlProfitCenters.SelectedValue)).ToString(); //.GetPaymentRecieptsAccount().ToString();
               
                ClearChequeData();

            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls(sender);
            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
            }
        }
        protected void radCashDisburment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlCheques.Visible = false;
               // ddlCashBankAccounts.SelectedValue = new  BLL.Accounts.AccountingTree().GetCashAccount(int.Parse(ddlProfitCenters.SelectedValue)).ToString();
                
                ddlChequeStatus.Enabled = false;
                ddlAccounts.Enabled = true;
                ddlCashBankAccounts.Enabled = true;
                CompareValidator1.Enabled = true;
            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
            }
        }
        protected void ddlProfitCenters_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlAccounts.Items.Clear();
                ddlAccounts.Items.Add(new ListItem("اختيار", "0"));
                ddlAccounts.DataBind();

                ddlCashBankAccounts.Items.Clear();
                ddlCashBankAccounts.Items.Add(new ListItem("اختيار", "0"));
                ddlCashBankAccounts.DataBind();

                selectedProfitCenter = int.Parse(ddlProfitCenters.SelectedValue);
                txtJournalNo.Text =journal.GetNewJournalID(int.Parse(selectedProfitCenter.ToString())).ToString();
              
                txtDocumentNo.Text = txtJournalNo.Text;

                if (radCashDisburment.Checked)
                    radCashDisburment_CheckedChanged(sender, new EventArgs());
                else if (radCashRecipt.Checked)
                    radCashRecipt_CheckedChanged(sender, new EventArgs());
                else if (radChequehDisburment.Checked)
                    radChequehDisburment_CheckedChanged(sender, new EventArgs());
                else if (radChequeRecipt.Checked)
                    radChequeRecipt_CheckedChanged(sender, new EventArgs());

            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
            }
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
                lblFeedBack.Text = ex.Message;
            }
        }

        protected void radChequeRecipt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblFeedBack.Text = "";
                ddlChequeStatus.Enabled = true;
                ddlChequeStatus.Items.Clear();
                ddlChequeStatus.Items.Add(new ListItem("Select", "0"));
                ddlChequeStatus.Items.Add(new ListItem("تحت التحصيل", "1"));
                ddlChequeStatus.Items.Add(new ListItem("تم التحصيل", "2"));

                pnlCheques.Visible = true;

              //  ddlCashBankAccounts.SelectedValue = new  BLL.Accounts.AccountingTree().GetBankAccount(int.Parse(ddlProfitCenters.SelectedValue)).ToString();
            


                ClearChequeData();

            }
            catch (Exception ex)
            {
                //   lblFeedBack.Text = ex.Message;
            }
        }

        protected void ddlCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int defaultCurrency = BLL.Currencies.Currencies.GetDefaultCurrency(int.Parse(Session["CompID"].ToString()));
                lblExchangeRate.Text = BLL.Currencies.Currencies.GetCurrencyConversionRatio(int.Parse(ddlCurrencies.SelectedValue), defaultCurrency,int.Parse(Session["CompID"].ToString())).ToString("N2");
                
            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
            }
        }
        protected void ddlCurrencies_DataBound(object sender, EventArgs e)
        {
            ddlCurrencies.SelectedValue = BLL.Currencies.Currencies.GetDefaultCurrency(int.Parse(Session["CompID"].ToString())).ToString(); //"0";
            ddlCurrencies_SelectedIndexChanged(sender, new EventArgs());
        }
        protected void radCashRecipt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlCheques.Visible = false;
             //   ddlCashBankAccounts.SelectedValue = new  BLL.Accounts.AccountingTree().GetCashAccount(int.Parse(ddlProfitCenters.SelectedValue)).ToString();
                ddlChequeStatus.Enabled = false;
                ddlAccounts.Enabled = true;
                ddlCashBankAccounts.Enabled = true;
                CompareValidator1.Enabled = true;
            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
            }
        }
        private void ClearChequeData()
        {
            txtChequeNo.Text = "";
            txtChequeDate.SelectedDate = null;
            ddlBanks.SelectedValue = "0";

        }
        private void ClearControls(object sender)
        {
            btnPrint.Enabled = false;
            btnSave.Enabled = true;

            //ddlFromTo.DataSource = null;
            //ddlFromTo.DataBind();

            ddlAccounts.SelectedValue = "0";
            //ddlCashBankAccounts.SelectedValue = "0";
            ddlBanks.SelectedValue = "0";
            ddlChequeStatus.SelectedValue = "0";
            ddlCurrencies.SelectedValue =BLL.Currencies.Currencies.GetDefaultCurrency(int.Parse(Session["CompId"].ToString())).ToString();
            ddlCurrencies_SelectedIndexChanged(sender, new EventArgs());

            txtAmount.Text = "0";

            txtDocumentDate.SelectedDate = DateTime.Today;
            txtChequeDate.SelectedDate = DateTime.Today;
            txtJournalDate.SelectedDate = DateTime.Today; //.ToString("dd/MM/yyyy");

            //ddlProfitCenters_SelectedIndexChanged(sender, new EventArgs());
            ddlProfitCenters.DataBind();

            string userDefaultProfitCenter = BLL.Profit_Center.Profit_Center.GetUserDefaultProfitCenter(int.Parse(Session["UserID"].ToString())).ToString();
            ddlProfitCenters.SelectedValue = userDefaultProfitCenter;


            txtJournalNo.Text = journal.GetNewJournalID(int.Parse(userDefaultProfitCenter)).ToString();
            txtDocumentNo.Text = txtJournalNo.Text;


            lblFeedBack.Text = "";
            txtChequeNo.Text = "";
            txtDescription.Text = "";
            txtAddressedTo.Text = "";

            txtCDNo.Text = "";
            txtCRNo.Text = "";
            lblExchangeRate.Text = "";

            transactionID = 0;

            radCashRecipt.Checked = true;
            radCashDisburment.Checked = false;
            radChequehDisburment.Checked = false;
            radChequeRecipt.Checked = false;
            radCashRecipt_CheckedChanged(sender, new EventArgs());


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

        protected void btnNew_Click1(object sender, EventArgs e)
        {
            try
            {
                ClearControls(sender);
            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
            }
        }
      
        protected void btnPrint_Click1(object sender, EventArgs e)
        {
            try
            {
                string sql = "";
                lblFeedBack.Text = "";

                db = new DatabaseClass();

                if (radCashRecipt.Checked)
                {
                    sql = string.Format("SELECT * FROM vwCashRecieptsReport WHERE CR_NO={0} ORDER BY JournalCode ", int.Parse(txtCRNo.Text));
                    DataTable dt = db.ExecuteQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        Session["vwCashRecieptsReport"] = dt;

                        string url = "ViewCashReceiptReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewCashReceiptReport", "window.open('" + url + "', '_new')", true);
                    }
                    else
                        lblFeedBack.Text = "No Data";

                }
                else if (radChequeRecipt.Checked)
                {
                    sql = string.Format("SELECT * FROM vwChequeRecieptsReport WHERE CR_NO={0} ORDER BY JournalCode ", int.Parse(txtCRNo.Text));
                    DataTable dt = db.ExecuteQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        Session["vwChequeRecieptsReport"] = dt;

                        string url = "ViewChequeRecieptReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewChequeRecieptReport", "window.open('" + url + "', '_new')", true);
                    }
                    else
                        lblFeedBack.Text = "No Data";


                    //sql = string.Format("SELECT * FROM vwChequeMgmtFullInfo WHERE RecordID={0} ", int.Parse(txtCRNo.Text));
                    //DataTable dt = db.ExecuteQuery(sql);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    Session["vwChequeMgmtFullInfo"] = dt;

                    //    string url = "ViewChequeRecieptReport.aspx";
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewChequeRecieptReport", "window.open('" + url + "', '_new')", true);
                    //}
                    //else
                    //    lblFeedback.Text = "No Data";

                }
                else if (radCashDisburment.Checked)
                {
                    sql = string.Format("SELECT * FROM vwCashDisbursementsReport WHERE CD_NO={0} ORDER BY JournalCode ", int.Parse(txtCDNo.Text));
                    DataTable dt = db.ExecuteQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        Session["vwCashDisbusrsesReport"] = dt;

                        string url = "ViewCashDisbursesReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewCashDisbursesReport", "window.open('" + url + "', '_new')", true);
                    }
                    else
                        lblFeedBack.Text = "No Data";

                }
                else if (radChequehDisburment.Checked)
                {
                    sql = string.Format("SELECT * FROM vwChequeDisbursementsReport WHERE CD_NO={0} ORDER BY JournalCode ", int.Parse(txtCDNo.Text));
                    DataTable dt = db.ExecuteQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        Session["vwChequeDisbursementsReport"] = dt;

                        string url = "ViewChequeDisbursesReport.aspx";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewChequeDisbursesReport", "window.open('" + url + "', '_new')", true);
                    }
                    else
                        lblFeedBack.Text = "No Data";

                    //sql = string.Format("SELECT * FROM vwChequeMgmtFullInfo WHERE RecordID={0} ", int.Parse(txtCDNo.Text));
                    //DataTable dt = db.ExecuteQuery(sql);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    Session["vwChequeMgmtFullInfo"] = dt;

                    //    string url = "ViewChequeDisbursesReport.aspx";
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RedirectViewChequeRecieptReport", "window.open('" + url + "', '_new')", true);
                    //}
                    //else
                    //    lblFeedback.Text = "No Data";
                }
                else
                    lblFeedBack.Text = "No Data";

                ClearControls(sender);
                ddlCurrencies_SelectedIndexChanged(sender, new EventArgs());

            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {


                int journalID = 0;
                int centerID = int.Parse(ddlProfitCenters.SelectedValue);
                int userID = int.Parse(Session["UserID"].ToString());

                int selectedAccount = int.Parse(ddlAccounts.SelectedValue);
                bool isValidAccount = new BLL.Accounts.AccountingTree().CheckAccountIsOpenNode(selectedAccount);

                bool userIsPermittedToAccount = Policy.CheckUserIsPermittedToAccountByCenterID(userID, selectedAccount);

                if (!isValidAccount) //&& (radCashDisburment.Checked || radCashRecipt.Checked))
                    lblFeedBack.Text = "أحد أو كلا الحسابين المختارين قد يكون مغلقاً أو حساباً إجمالياً! الرجاء اختيار حساب تفصيلي مفتوح!";

                else if (!userIsPermittedToAccount)
                    lblFeedBack.Text = "المستخدم الحالي غير مصرح له بالتعامل مع هذا الحساب";
                else
                {




                    journalID = journal.GetNewJournalID(int.Parse(Session["CompId"].ToString()));
                    txtJournalNo.Text = journalID.ToString();

                    decimal amount = 0;
                    decimal initialAmount = decimal.Parse(txtAmount.Text);
                    int defaultCurrency = BLL.Currencies.Currencies.GetDefaultCurrency(int.Parse(Session["CompId"].ToString()));

                    if (int.Parse(ddlCurrencies.SelectedValue) != defaultCurrency)
                    {
                        decimal ratio = BLL.Currencies.Currencies.GetCurrencyConversionRatio(defaultCurrency, int.Parse(ddlCurrencies.SelectedValue), int.Parse(Session["CompId"].ToString()));
                        amount = initialAmount * ratio;
                    }
                    else
                        amount = initialAmount;


                    bool journalCodeExists = false;
                    int finYearID = int.Parse(lblFinancialYearID.Text);
                    if (radCashDisburment.Checked)
                    {
                        if (journalID != 0)
                        {
                            journalCodeExists = journal.CheckSubjournalCodeExists(txtJournalNo.Text);
                            if (!journalCodeExists)
                                transactionID = journal.PostToGlMaster(finYearID, txtJournalNo.Text, txtJournalDate.SelectedDate.ToString(),
                                    userID, txtAddressedTo.Text, centerID, false, int.Parse(Session["CompId"].ToString()));
                            else
                                transactionID = journal.GetTransactionIDInGLMaster(txtJournalNo.Text);


                            if (transactionID > 0)
                            {
                                int CD_NO = journal.GenerateCDNoteNo();

                                btnSave.Enabled = false;
                                btnPrint.Enabled = true;
                                txtCDNo.Text = CD_NO.ToString();

                                int result = journal.PostToGlDetailsCashDisbursements(transactionID, txtJournalDate.SelectedDate.ToString(),
                                    selectedAccount, finYearID, amount, txtDescription.Text, userID, CD_NO, txtDocumentNo.Text,
                                    txtDocumentDate.SelectedDate.ToString(), int.Parse(ddlCashBankAccounts.SelectedValue));


                                if (result > 0)
                                {
                                    lblFeedBack.Text = Feedback.InsertSuccessfull();

                                }
                                else
                                    lblFeedBack.Text = Feedback.InsertException();
                            }
                            else
                                lblFeedBack.Text = Feedback.InsertException();
                        }
                        else
                            lblFeedBack.Text = "الرجاء اختيار قيد اليومية!";


                    }
                    else if (radChequehDisburment.Checked)
                    {
                        if (journalID != 0)
                        {
                            journalCodeExists = journal.CheckSubjournalCodeExists(txtJournalNo.Text);
                            if (!journalCodeExists)
                                //  txtJournalDate.Text
                                transactionID = journal.PostToGlMaster(finYearID, txtJournalNo.Text, txtChequeDate.SelectedDate.ToString(),
                                    userID, txtAddressedTo.Text, centerID, false, int.Parse(Session["CompId"].ToString()));
                            else
                                transactionID = journal.GetTransactionIDInGLMaster(txtJournalNo.Text);


                            if (transactionID > 0)
                            {
                                int CD_NO = journal.GenerateCDNoteNo();

                                btnSave.Enabled = false;
                                btnPrint.Enabled = true;
                                txtCDNo.Text = CD_NO.ToString();

                                // txtJournalDate.Text
                                int result = journal.PostToGlDetailsChequeDisbursements(transactionID, txtChequeDate.SelectedDate.ToString(),
                                    selectedAccount, finYearID, amount, txtDescription.Text, userID, CD_NO, int.Parse(ddlBanks.SelectedValue),
                                    txtChequeDate.SelectedDate.ToString(), txtChequeNo.Text, txtDocumentNo.Text, txtDocumentDate.SelectedDate.ToString(),
                                    int.Parse(ddlCashBankAccounts.SelectedValue));


                                if (result > 0)
                                {
                                    lblFeedBack.Text = Feedback.InsertSuccessfull();
                                    string type = "Outgoing";
                                    //string target = "SupplierID"; target, 

                                    string sql = string.Format("INSERT INTO ChequeMgmt(ChequeNum, ChequeDueDate, {0}, ChequeStatus, BankID, ChequeAmount, WrittenTo, ChequeCurrencyID, Description, EntryDate, EntryUserId, DocumentNo, DocumentDate, CenterID) " +
                                        "VALUES('{1}', '{2}', 1, {11}, {3}, {4}, '{5}', {6}, '{7}', GETDATE(), '{8}', '{9}', '{10}', {12}) ",
                                        type, txtChequeNo.Text.Replace("'", "''"), DatabaseClass.FormatDateString(txtChequeDate.SelectedDate.ToString()),
                                        ddlBanks.SelectedValue, decimal.Parse(txtAmount.Text), txtAddressedTo.Text.Replace("'", "''"),
                                        ddlCurrencies.SelectedValue, txtDescription.Text.Replace("'", "''"), userID, txtDocumentNo.Text.Replace("'", "''"),
                                        DatabaseClass.FormatDateString(txtDocumentDate.SelectedDate.ToString()), ddlChequeStatus.SelectedValue,
                                        ddlProfitCenters.SelectedValue);

                                    int chequeMgmtID = db.ExecuteInsertWithIDReturn(sql, "ChequeMgmt");
                                }
                                else
                                    lblFeedBack.Text = Feedback.InsertException();
                            }
                            else
                                lblFeedBack.Text = Feedback.InsertException();
                        }
                        else
                            lblFeedBack.Text = "الرجاء اختيار قيد اليومية!";

                    }
                    else if (radChequeRecipt.Checked)
                    {
                        if (journalID != 0)
                        {
                            journalCodeExists = journal.CheckSubjournalCodeExists(txtJournalNo.Text);
                            if (!journalCodeExists)
                                // txtJournalDate.Text
                                transactionID = journal.PostToGlMaster(finYearID, txtJournalNo.Text, txtChequeDate.SelectedDate.ToString(),
                                    userID, txtAddressedTo.Text, centerID, false, int.Parse(Session["CompId"].ToString()));
                            else
                                transactionID = journal.GetTransactionIDInGLMaster(txtJournalNo.Text);


                            if (transactionID > 0)
                            {
                                int CR_No = journal.GenerateCRNoteNo();

                                btnSave.Enabled = false;
                                btnPrint.Enabled = true;
                                txtCRNo.Text = CR_No.ToString();

                                //  txtJournalDate.Text
                                int result = journal.PostToGlDetailsChequeReciept(transactionID, txtChequeDate.SelectedDate.ToString(),
                                    selectedAccount, finYearID, amount, txtDescription.Text, userID, CR_No, int.Parse(ddlBanks.SelectedValue),
                                    txtChequeDate.SelectedDate.ToString(), txtChequeNo.Text, txtDocumentNo.Text, txtDocumentDate.SelectedDate.ToString(),
                                    int.Parse(ddlCashBankAccounts.SelectedValue));

                                if (result > 0)
                                {
                                    lblFeedBack.Text = Feedback.InsertSuccessfull();

                                    string type = "Incoming";

                                    string sql = string.Format("INSERT INTO ChequeMgmt(ChequeNum, ChequeDueDate, {0}, ChequeStatus, BankID, ChequeAmount, WrittenTo, ChequeCurrencyID, Description, EntryDate, EntryUserId, DocumentNo, DocumentDate, CenterID) " +
                                       "VALUES('{1}', '{2}', 1, {11}, {3}, {4}, '{5}', {6}, '{7}', GETDATE(), '{8}', '{9}', '{10}', {12}) ",
                                       type, txtChequeNo.Text.Replace("'", "''"), DatabaseClass.FormatDateString(txtChequeDate.SelectedDate.ToString()),
                                       ddlBanks.SelectedValue, decimal.Parse(txtAmount.Text), txtAddressedTo.Text.Replace("'", "''"),
                                       ddlCurrencies.SelectedValue, txtDescription.Text.Replace("'", "''"), userID, txtDocumentNo.Text.Replace("'", "''"),
                                       DatabaseClass.FormatDateString(txtDocumentDate.SelectedDate.ToString()), ddlChequeStatus.SelectedValue,
                                       ddlProfitCenters.SelectedValue);

                                    int chequeMgmtID = db.ExecuteInsertWithIDReturn(sql, "ChequeMgmt");
                                }
                                else
                                    lblFeedBack.Text = Feedback.InsertException();
                            }
                            else
                                lblFeedBack.Text = Feedback.InsertException();
                        }
                        else
                            lblFeedBack.Text = "الرجاء اختيار قيد اليومية!";

                    }
                    else if (radCashRecipt.Checked)
                    {
                        if (journalID != 0)
                        {
                            journalCodeExists = journal.CheckSubjournalCodeExists(txtJournalNo.Text);
                            if (!journalCodeExists)
                                transactionID = journal.PostToGlMaster(finYearID, txtJournalNo.Text, txtJournalDate.SelectedDate.ToString(),
                                    userID, txtAddressedTo.Text, centerID, false, int.Parse(Session["CompId"].ToString()));
                            else
                                transactionID = journal.GetTransactionIDInGLMaster(txtJournalNo.Text);


                            if (transactionID > 0)
                            {
                                int CR_No = journal.GenerateCRNoteNo();

                                btnSave.Enabled = false;
                                btnPrint.Enabled = true;
                                txtCRNo.Text = CR_No.ToString();

                                int result = journal.PostToGlDetailsCashReciept(transactionID, txtJournalDate.SelectedDate.ToString(),
                                    selectedAccount, finYearID, amount, txtDescription.Text, userID, CR_No, txtDocumentNo.Text,
                                    txtDocumentDate.SelectedDate.ToString(), int.Parse(ddlCashBankAccounts.SelectedValue));


                                if (result > 0)
                                {
                                    lblFeedBack.Text = Feedback.InsertSuccessfull();

                                    lblFeedBack.ForeColor = System.Drawing.Color.Green;
                                }
                                else
                                {
                                    lblFeedBack.Text = Feedback.InsertException();

                                    lblFeedBack.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                lblFeedBack.Text = Feedback.InsertException();

                                lblFeedBack.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                            lblFeedBack.Text = "الرجاء اختيار قيد اليومية!";
                    }
                }


            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
                lblFeedBack.ForeColor = System.Drawing.Color.Red;

            }
        }

    }
}