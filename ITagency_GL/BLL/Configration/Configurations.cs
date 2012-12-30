using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for Configurations
/// </summary>
public class StockSales08Configurations
{
    //private DatabaseClass db;

    public int CashAccount { get; set; }
    public int SettlementAccountID { get; set; }
    public int BankAccountID  { get; set; }
    public int TaxVatAccount { get; set; }
    public int BankAccount { get; set; }
    public int DefaultCurrencyID { get; set; }

    public decimal VatRatio { get; set; }
    public int CurrentYear { get; set; }
    public int DebitorsUpperAccountID { get; set; }
    public int CreditorsUpperAccountID { get; set; }
    public int LoansUpperAccountID { get; set; }

    //public int PurchaseInvoiceJType { get; set; }
    //public int SaleInvoiceJType { get; set; }


    public StockSales08Configurations()
    {
        string sql = "SELECT TOP 1 ISNULL(CashAccountID, 0) AS CashAccountID, ISNULL(SettlementAccountID, 0) AS SettlementAccountID, ISNULL(BankAccountID, 0) AS BankAccountID, ISNULL(TaxVatAccount, 0) AS TaxVatAccount, ISNULL(DefaultCurrency, 0) AS DefaultCurrency, ISNULL(VatRatio, 0) AS VatRatio, ISNULL(DebitorsUpperAccountID, 0) AS DebitorsUpperAccountID, ISNULL(CreditorsUpperAccountID, 0) AS CreditorsUpperAccountID, ISNULL(LoansUpperAccountID, 0) AS LoansUpperAccountID FROM Configrations ";
        DataTable dt = new DatabaseClass().ExecuteQuery(sql);
        if (dt.Rows.Count == 1)
        {
            DataRow dr = dt.Rows[0];
            CashAccount = int.Parse(dr["CashAccountID"].ToString());
            SettlementAccountID = int.Parse(dr["SettlementAccountID"].ToString());
            BankAccountID = int.Parse(dr["BankAccountID"].ToString());
            TaxVatAccount = int.Parse(dr["TaxVatAccount"].ToString());

            DefaultCurrencyID = int.Parse(dr["DefaultCurrency"].ToString());

            VatRatio = decimal.Parse(dr["VatRatio"].ToString());

            DebitorsUpperAccountID = int.Parse(dr["DebitorsUpperAccountID"].ToString());
            CreditorsUpperAccountID = int.Parse(dr["CreditorsUpperAccountID"].ToString());
            LoansUpperAccountID = int.Parse(dr["LoansUpperAccountID"].ToString());
        }
        else
        {
            CashAccount = 0;
            SettlementAccountID = 0;
            BankAccountID = 0;
            TaxVatAccount = 0;

            DefaultCurrencyID = 0;
            CurrentYear = 0;

            VatRatio = 0;

            DebitorsUpperAccountID = 0;
            CreditorsUpperAccountID = 0;
            LoansUpperAccountID = 0;
        }
    }



}
