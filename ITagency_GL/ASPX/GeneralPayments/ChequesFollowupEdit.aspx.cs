using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ITagency_GL.ASPX.GeneralPayments
{
    public partial class ChequesFollowupEdit : System.Web.UI.Page
    {
        BLL.Cheques_Folowup.Cheques_Folowup ChequesProcess = new BLL.Cheques_Folowup.Cheques_Folowup();
        BLL.Currencies.Currencies CurrenciesProcess = new BLL.Currencies.Currencies();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["id"] == null)
                Response.Redirect("ChequesFollowup.aspx", false);
            else
            {
                // load cheque info
                if (!IsPostBack)
                {
                    DataTable dt = ChequesProcess.CheckStatus(int.Parse(Request.QueryString["id"]));
                    if (dt.Rows.Count == 1)
                    {
                        DataRow dr = dt.Rows[0];
                        lblType.Text = bool.Parse(dr["Incoming"].ToString()) ? "وارد" : "صادر";
                        lblTypeID.Text = bool.Parse(dr["Incoming"].ToString()) ? "2" : "1";

                        txtChequeNo.Text = dr["ChequeNum"].ToString();
                        txtAddressedTo.Text = dr["WrittenTo"].ToString();
                        ddlBanks.SelectedValue = dr["BankID"].ToString();

                        txtDescription.Text = dr["Description"].ToString();
                        ddlStatus.SelectedValue = dr["ChequeStatus"].ToString();
                        lblStatusID.Text = dr["ChequeStatus"].ToString();

                        txtDueDate.SelectedDate = DateTime.Parse(dr["ChequeDueDate"].ToString());
                        txtAmount.Text = decimal.Parse(dr["ChequeAmount"].ToString()).ToString("N2");

                        ddlCurrencies.SelectedValue = dr["ChequeCurrencyID"].ToString();
                        ddlCurrencies_SelectedIndexChanged(sender, new EventArgs());


                    }
                    else
                        Response.Redirect("ChequesFollowup.aspx", false);

                }
            }
        }
        protected void ddlCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int defaultCurrency = BLL.Currencies.Currencies.GetDefaultCurrency(int.Parse(Session["CompId"].ToString()));
                lblExchangeRate.Text = BLL.Currencies.Currencies.GetCurrencyConversionRatio(int.Parse(ddlCurrencies.SelectedValue), defaultCurrency, int.Parse(Session["CompId"].ToString())).ToString("N2");
            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
                lblFeedBack.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ChequeStatus)int.Parse(lblStatusID.Text) == ChequeStatus.Processed || (ChequeStatus)int.Parse(lblStatusID.Text) == ChequeStatus.RD)
                    throw new Exception("لايمكن تعديل بيانات هذا الشيك!");
                else if ((ChequeStatus)int.Parse(lblStatusID.Text) == ChequeStatus.UnderProcessing)
                {
                    if (ChequesProcess.Update(txtChequeNo.Text.Replace("'", "''"), txtDueDate.SelectedDate.ToString(),
                        int.Parse(ddlStatus.SelectedValue), int.Parse(ddlBanks.SelectedValue), decimal.Parse(txtAmount.Text), txtAddressedTo.Text.Replace("'", "''"),
                        int.Parse(ddlCurrencies.SelectedValue), txtDescription.Text.Replace("'", "''"), int.Parse(Session["UserId"].ToString()), int.Parse(Request.QueryString["id"])))
                    {
                        lblFeedBack.Text = Shared.Feedback.UpdateSuccessfull();
                        lblFeedBack.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblFeedBack.Text = Shared.Feedback.UpdateExceptionUnique();
                        lblFeedBack.ForeColor = System.Drawing.Color.Red;


                    }

                }
            }
            catch (Exception ex)
            {
                lblFeedBack.Text = ex.Message;
                lblFeedBack.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ChequeStatus)int.Parse(lblStatusID.Text) == ChequeStatus.Processed || (ChequeStatus)int.Parse(lblStatusID.Text) == ChequeStatus.RD)
                    throw new Exception("لايمكن حذف هذا الشيك!");
                else if ((ChequeStatus)int.Parse(lblStatusID.Text) == ChequeStatus.UnderProcessing)
                {
                    if (ChequesProcess.Delete(int.Parse(Request.QueryString["id"])))
                    {
                        lblFeedBack.Text = Shared.Feedback.DeleteSuccessfull();
                        lblFeedBack.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        lblFeedBack.Text = Shared.Feedback.DeleteException();
                        lblFeedBack.ForeColor = System.Drawing.Color.Red;
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