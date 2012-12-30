using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DAL;
namespace BLL.Cheques_Folowup
{
    public class Cheques_Folowup
    {
        private DatabaseClass DB = new DatabaseClass();
        public DataTable Search(string ChequeDueDate1, string ChequeDueDate2, string ChequeType, string BankID, string ChequeNo, string Status)
        {
            bool isFirstArg = true;
            string sql = "SELECT * FROM vwChequeMgmtFullInfo ";


            if (ChequeDueDate1 != null)
            {
                if (isFirstArg)
                {
                    sql = string.Format("{0} WHERE ChequeDueDate>='{1}' ", sql, DatabaseClass.FormatDateString(ChequeDueDate1));
                    isFirstArg = false;
                }
                else
                    sql = string.Format("{0} AND ChequeDueDate>='{1}' ", sql, DatabaseClass.FormatDateString(ChequeDueDate1));
            }

            if (ChequeDueDate2 != null)
            {
                if (isFirstArg)
                {
                    sql = string.Format(" {0} WHERE ChequeDueDate<='{1}' ", sql, DatabaseClass.FormatDateString(ChequeDueDate2));
                    isFirstArg = false;
                }
                else
                    sql = string.Format(" {0} AND ChequeDueDate<='{1}' ", sql, DatabaseClass.FormatDateString(ChequeDueDate2));
            }

            if (ChequeType != "0")
            {
                if (isFirstArg)
                {
                    sql = string.Format("{0} WHERE {1}=1 ", sql, ChequeType == "1" ? "Incoming" : "Outgoing");
                    isFirstArg = false;
                }
                else
                    sql = string.Format("{0} AND {1}=1 ", sql, ChequeType == "1" ? "Incoming" : "Outgoing");
            }

            if (BankID != "0")
            {
                if (isFirstArg)
                {
                    sql = string.Format("{0} WHERE BankID={1} ", sql, BankID);
                    isFirstArg = false;
                }
                else
                    sql = string.Format("{0} AND BankID={1} ", sql, BankID);
            }

            if (ChequeNo != "")
            {
                if (isFirstArg)
                {
                    sql = string.Format("{0} WHERE ChequeNum LIKE '%{1}%' ", sql, ChequeNo);
                    isFirstArg = false;
                }
                else
                    sql = string.Format("{0} AND ChequeNum LIKE '%{1}%' ", sql, ChequeNo);
            }

            if (Status != "0")
            {
                if (isFirstArg)
                {
                    sql = string.Format(" {0} WHERE ChequeStatus={1} ", sql, Status);
                    isFirstArg = false;
                }
                else
                    sql = string.Format(" {0} AND ChequeStatus={1} ", sql, Status);
            }


            sql = string.Format("{0} ORDER BY ChequeDueDate DESC ", sql);
           /// DataTable dt = new DatabaseClass().ExecuteQuery(sql);
            return DB.ExecuteQuery(sql);

        }
        public DataTable CheckStatus(int RecordID)
        {
            string sql = string.Format("SELECT * FROM vwChequeMgmtFullInfo WHERE RecordID={0} ", RecordID);
            return DB.ExecuteQuery(sql);

        }

        public bool Update(string ChequeNo,string CheqeDate,int ChequeStatus,int BankId,decimal Amount,string AddressTo,int CurrencyId,string Description,int UserId,int RecordId)
        {
            string sql = string.Format("UPDATE ChequeMgmt SET ChequeNum='{0}', ChequeDueDate='{1}', ChequeStatus={2}, BankID={3}, ChequeAmount={4}, WrittenTo='{5}', ChequeCurrencyID={6}, Description='{7}', EditDate=GETDATE(), EditUserId={8} WHERE RecordID={9} ",
                ChequeNo, DatabaseClass.FormatDateString(CheqeDate),
                 ChequeStatus, BankId, Amount, AddressTo,
                 CurrencyId, Description, UserId, RecordId);

            int rows = new DatabaseClass().ExecuteNonQuery(sql);
            if (rows > 0)
                return true;
            else
                return false;


        }
        public bool Delete(int RecordID)
        {

            string sql = string.Format("DELETE FROM ChequeMgmt WHERE RecordID={0} ", RecordID);
            int rows = new DatabaseClass().ExecuteNonQuery(sql);
            if (rows > 0)
                return true;
            else
                return false;

        }
             
    }
}