using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Data;
namespace BLL.Profit_Center
{
    public class Profit_Center
    {
        private DatabaseClass db = new DatabaseClass();


        /// <summary>
        ///  
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        /// 
        public DataTable GetUserPermittedProfitCenters(int userID,int CompId)
        {
            // if the user is admin, return all profit centers, otherwise return just his own
            string sql = string.Format("SELECT IsAdmin, ISNULL(CenterID, 0) AS CenterID FROM SystemUsers WHERE UserID={0} and CompId={1}  ", userID, CompId);
            DataTable dt = db.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (bool.Parse(dr["IsAdmin"].ToString()))
                    return GetAll();
                else
                    return SearchByID(int.Parse(dr["CenterID"].ToString()));
            }
            else
                return new DataTable();
        }
        public DataTable view(int CenterID, int CompId)
        {
            string sql = string.Format("SELECT TOP 1 ISNULL(CashAccountID, 0) AS CashAccountID, ISNULL(SettlementAccountID, 0) AS SettlementAccountID, ISNULL(BankAccountID, 0) AS BankAccountID, ISNULL(TaxVatAccount, 0) AS TaxVatAccount, ISNULL(VatRatio, 0) AS VatRatio, ISNULL(DefaultCurrency, 0) AS DefaultCurrency, ISNULL(TaxVatAccount, 0) AS TaxVatAccount, ISNULL(DebitorsUpperAccountID, 0) AS DebitorsUpperAccountID, ISNULL(CreditorsUpperAccountID, 0) AS CreditorsUpperAccountID, ISNULL(LoansUpperAccountID, 0) AS LoansUpperAccountID FROM Configrations WHERE CenterID={0} and CompId ={1} ",
                  CenterID, CompId);

            DataTable dt = db.ExecuteQuery(sql);
            return dt;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetListOfUserPermittedProfitCenters(int userID, int CompId)
        {
            string centers = "";
            DataTable dtUserPermittedProfitCenters = GetUserPermittedProfitCenters(userID, CompId);
            for (int i = 0; i < dtUserPermittedProfitCenters.Rows.Count; i++)
            {
                if (i == 0)
                    centers = dtUserPermittedProfitCenters.Rows[0]["CenterID"].ToString();
                else
                    centers = string.Format("{0}, {1}", centers, dtUserPermittedProfitCenters.Rows[i]["CenterID"].ToString());
            }

            return centers;
        }
        public bool Insert(int CashAccounts, int SettlementsAccount, int Currency, int BanksAccount, int TaxAccount, string VatValue, int DebitorsPAccount, int CreditorsPAccount, int LoansPAccount, int ProfitCenters, int CompId)
        {

            string sql = string.Format("UPDATE Configrations SET CashAccountID={0}, SettlementAccountID={1}, DefaultCurrency={2}, BankAccountID={3}, TaxVatAccount={4}, VatRatio={5}, DebitorsUpperAccountID={6}, CreditorsUpperAccountID={7}, LoansUpperAccountID={8} WHERE CenterID={9} and CompId={10} ",
                   CashAccounts, SettlementsAccount, Currency, BanksAccount,
                   TaxAccount, VatValue, DebitorsPAccount, CreditorsPAccount,
                   LoansPAccount, ProfitCenters,CompId);

            int row = db.ExecuteNonQuery(sql);
            if (row > 0)
                return true;
            else
            {
                sql = string.Format("INSERT INTO Configrations(CashAccountID, SettlementAccountID, DefaultCurrency, BankAccountID, VatRatio, TaxVatAccount, DebitorsUpperAccountID, CreditorsUpperAccountID, LoansUpperAccountID, CenterID,CompId) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9},{10}) ",
                        CashAccounts, SettlementsAccount, Currency, BanksAccount,
                        VatValue, TaxAccount, DebitorsPAccount, CreditorsPAccount,
                        LoansPAccount, ProfitCenters,CompId);

                row = db.ExecuteNonQuery(sql);
                if (row > 0)
                    return true;
                else
                    return false;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int GetUserDefaultProfitCenter(int userID)
        {
            string sql = string.Format("SELECT ISNULL(CenterID, 0) AS CenterID FROM SystemUsers WHERE UserID={0} ", userID);
            return int.Parse(new DatabaseClass().ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll()
        {
            string sql = "SELECT CenterID, CenterName FROM ProfitCenters ORDER BY CenterName ";
            return db.ExecuteQuery(sql);
        }

        public DataTable GetAll_By_CompId(int CompId)
        {
            string sql = string.Format("SELECT * FROM ProfitCenters where CompId={0} ", CompId);
            return db.ExecuteQuery(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable SearchByID(int id)
        {
            string sql = string.Format("SELECT CenterID, CenterName FROM ProfitCenters WHERE CenterID={0} ORDER BY CenterName ", id);
            return db.ExecuteQuery(sql);
        }
    }
}