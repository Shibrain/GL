using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using Shared;
namespace BLL.Journal
{
    public class Journal
    {

        public Journal()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private DatabaseClass db = new DatabaseClass();
        private BLL.Accounts.AccountingTree tree = new BLL.Accounts.AccountingTree();



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetJournalId(int CompId)
        {
            string sql = string.Format("SELECT ISNULL(MAX(JournalId), 0) as JournalId from GLMaster WHERE IsPosted=1 and CompId={0}", CompId);
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetNewJournalID(int CompId)
        {
            string sql = string.Format("SELECT ISNULL(MAX(JournalCode), 0)+1 FROM GLMaster where CompId={0} ", CompId);
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerID"></param>
        /// <returns></returns>
        public int GetNewJournalID(int centerID, int CompId)
        {
            string sql = string.Format("SELECT ISNULL(MAX(JournalCode), 0)+1 FROM GLMaster WHERE CenterID={0} and CompId={1} ", centerID, CompId);
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerID"></param>
        /// <returns></returns>
        public int GetOpeningJournalID(int centerID, int CompId)
        {
            string sql = string.Format("SELECT ISNULL(MIN(JournalCode), 1) FROM GLMaster  WHERE CenterID={0} AND IsOpening=1  and CompId={1}", centerID, CompId);
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerID"></param>
        /// <returns></returns>
        public DateTime GetOpeningBalancesJournalDate(int centerID, int CompId)
        {
            string sql = string.Format("SELECT ISNULL(TransactionDate, 0) AS TransactionDate FROM GLMaster  WHERE CenterID={0} and journalcode=1 AND IsOpening=1  and CompId={1}", centerID, CompId);
            DataTable dt = db.ExecuteQuery(sql);
            return ((dt.Rows.Count == 0) ? DateTime.Today : DateTime.Parse(dt.Rows[0]["TransactionDate"].ToString()));
            //return ((dt.Rows.Count == 0) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(dt.Rows[0]["TransactionDate"].ToString()).ToString("dd/MM/yyyy"));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerID"></param>
        /// <returns></returns>
        public int GetOpeningBalancesTransactionID(int centerID, int CompId)
        {
            string sql = string.Format("SELECT ISNULL(TransactionId, 0) AS TransactionId FROM GLMaster  WHERE CenterID={0} and journalcode=1  and CompId={1}", centerID, CompId);
            DataTable dt = db.ExecuteQuery(sql);
            return ((dt.Rows.Count == 0) ? 0 : int.Parse(dt.Rows[0]["TransactionId"].ToString()));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalTypeID"></param>
        /// <returns></returns>
        public int GetSubJournalId(int journalTypeID)
        {
            //string sql = string.Format("SELECT ISNULL(MAX(JournalCode), 0) as JournalId from GLMaster WHERE IsPosted=1 AND JournalTypeID={0} ", journalTypeID);
            //string sql = string.Format("SELECT ISNULL(MAX(SubJournalNo), 0) as JournalId from GLMaster WHERE IsPosted=1 AND JournalTypeID={0} ", journalTypeID);
            string sql = string.Format("SELECT ISNULL(MAX(SubJournalNo), 0) AS SubJournalNo from GLMaster WHERE JournalTypeID={0} ", journalTypeID);
            DataTable dt = db.ExecuteQuery(sql);
            return ((dt.Rows.Count == 0) ? 0 : int.Parse(dt.Rows[0][0].ToString()));

            //if (dt.Rows.Count == 0)
            //    return 0;
            //else
            //    return int.Parse(dt.Rows[0][0].ToString());
            //return int.Parse(db.ExecuteScalar(sql).ToString());
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GenerateTransactionID()
        {
            //string sql = "SELECT ISNULL(MAX(GLDetails.TransactionId), 0)+1 AS TransactionId FROM GLDetails, GLMaster where GLMaster.IsPosted=1 ";
            string sql = "SELECT ISNULL(MAX(GLDetails.TransactionId), 0)+1 AS TransactionId FROM GLDetails, GLMaster where GLMaster.IsPosted=1 ";
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetTransactionID()
        {
            //string sql = "SELECT ISNULL(MAX(TransactionId), 0) AS TransactionId FROM GLMaster Gl  WHERE  Gl.IsPosted=0 ";
            string sql = "SELECT ISNULL(MAX(TransactionId), 0) AS TransactionId FROM GLMaster Gl ";
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="JournalTypeID"></param>
        /// <returns></returns>
        public int GetTransactionID(int JournalTypeID)
        {
            string sql = string.Format("SELECT ISNULL(MAX(TransactionId), 0) AS TransactionId FROM GLMaster Gl  WHERE  JournalTypeID={0} ", JournalTypeID);
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetTransactionDate()
        {
            //string sql = "SELECT CONVERT(VARCHAR(20), ISNULL(MAX(TransactionDate), GETDATE()), 103) as TransactionDate FROM GLMaster WHERE IsPosted = 0 ";
            string sql = "SELECT CONVERT(VARCHAR(20), ISNULL(MAX(TransactionDate), GETDATE()), 103) as TransactionDate FROM GLMaster ";
            return db.ExecuteScalar(sql).ToString();
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="financialPeriodID"></param>
        /// <param name="journalCode"></param>
        /// <param name="date"></param>
        /// <param name="doneBy"></param>
        /// <param name="writtenTo"></param>
        /// <param name="centerID"></param>
        /// <param name="isOpening"></param>
        /// <returns></returns>
        public int PostToGlMaster(int financialPeriodID, string journalCode, string date, int doneBy, string writtenTo, int centerID,
            bool isOpening, int CompId)
        {
            int rows = 0;
            journalCode = journalCode.Replace("'", "''");
            string writtenToPart = (writtenTo == "") ? "NULL" : string.Format("'{0}'", writtenTo.Replace("'", "''"));

            string sql = string.Format("INSERT INTO GLMaster(TransactionDate, JournalCode, IsPosted, FinancialPeriodId, EntryDate, EntryUserId, WrittenTo, CenterID, IsOpening,CompId) VALUES('{0}', {1}, 0, {2}, '{8}', {3}, {4}, {5}, {6},{7}) ",
                DatabaseClass.FormatDateString(date), journalCode, financialPeriodID, doneBy, writtenToPart, centerID,
                DatabaseClass.Bool2Int(isOpening), CompId, DatabaseClass.FormatDateString(System.DateTime.Today.ToString()));
            
            rows = db.ExecuteInsertWithIDReturn(sql, "GLMaster");
            return rows;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="subJournalCode"></param>
        /// <returns></returns>
        public bool CheckSubjournalCodeExists(string subJournalCode)
        {
            string sql = string.Format("SELECT ISNULL(COUNT(*), 0) FROM GLMaster WHERE JournalCode={0} ", subJournalCode);

            DataTable dt = db.ExecuteQuery(sql);
            if (dt.Rows.Count == 0)
                return false;
            else
                return (int.Parse(dt.Rows[0][0].ToString()) != 0);

            //result = int.Parse(db.ExecuteScalar(sql).ToString());
            //return (result != 0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="subJournalCode"></param>
        /// <returns></returns>
        public int GetTransactionIDInGLMaster(string subJournalCode)
        {
            string sql = string.Format("SELECT ISNULL(TransactionId, 0) FROM GLMaster WHERE JournalCode='{0}' ", subJournalCode);
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="subJournalCode"></param>
        /// <returns></returns>
        public DataTable GetTransactionInGLMaster(string subJournalCode)
        {
            string sql = string.Format("SELECT * FROM vwGL_Details WHERE JournalCode={0} ", subJournalCode);
            return db.ExecuteQuery(sql);
        }


        #region "CashMgmt"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="transactionDate"></param>
        /// <param name="accountID"></param>
        /// <param name="subjournalID"></param>
        /// <param name="periodID"></param>
        /// <param name="credit"></param>
        /// <param name="description"></param>
        /// <param name="doneBy"></param>
        /// <param name="CR_No"></param>
        /// <param name="documentNo"></param>
        /// <param name="documentDate"></param>
        /// <param name="cashAccountID"></param>
        /// <param name="JournalTypeID"></param>
        /// <param name="JournalCode"></param>
        /// <returns></returns>
        public int PostToGlDetailsCashReciept(int transactionID, string transactionDate, int accountID, int periodID, decimal credit,
            string description, int doneBy, int CR_No, string documentNo, string documentDate, int cashAccountID)
        {
            int rows = 0;
            int rows2 = 0;
            //int cashAccountID = tree.GetCashAccount();
            if (cashAccountID == 0)
                return 0;

            string sql = "";
            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            documentNo = documentNo == null ? "NULL" : string.Format("'{0}'", documentNo.Replace("'", "''"));
            documentDate = documentDate == null ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, FinancialPeriodId, Debit, Credit, Description, IsCashReciept, EntryDate, EntryUserId, CR_No, DocumentNo, DocumentDate) " +
                "VALUES({0}, '{1}', {2}, {3}, {4}, {5}, '{6}', 1, GETDATE(), {7}, {8}, {9}, {10}) ",
                transactionID, DatabaseClass.FormatDateString(transactionDate), accountID, periodID, 0, credit, description, doneBy,
                CR_No, documentNo, documentDate);

            rows = db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, FinancialPeriodId, Debit, Credit, Description, IsCashReciept, EntryDate, EntryUserId, CR_No, DocumentNo, DocumentDate, RelatedEntryId) " +
              "VALUES({0}, '{1}', {2}, {3}, {4}, {5}, '{6}', 1, GETDATE(), {7}, NULL, {8}, {9}, {10}) ",
              transactionID, DatabaseClass.FormatDateString(transactionDate), cashAccountID, periodID, credit, 0, description, doneBy,
              documentNo, documentDate, rows);

            rows2 = db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            return rows;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="transactionDate"></param>
        /// <param name="accountID"></param>
        /// <param name="periodID"></param>
        /// <param name="debit"></param>
        /// <param name="description"></param>
        /// <param name="doneBy"></param>
        /// <param name="DR_No"></param>
        /// <param name="documentNo"></param>
        /// <param name="documentDate"></param>
        /// <param name="cashAccountID"></param>
        /// <returns></returns>
        public int PostToGlDetailsCashDisbursements(int transactionID, string transactionDate, int accountID, int periodID,
            decimal debit, string description, int doneBy, int DR_No, string documentNo, string documentDate, int cashAccountID)
        {
            int rows = 0;
            int rows2 = 0;
            //int cashAccountID = tree.GetCashAccount();
            if (cashAccountID == 0)
                return 0;

            string sql = "";
            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            documentNo = documentNo == null ? "NULL" : string.Format("'{0}'", documentNo.Replace("'", "''"));
            documentDate = documentDate == null ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));

            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, FinancialPeriodId, Debit, Credit, Description, isCashDisburment, EntryDate, EntryUserId, CD_NO, DocumentNo, DocumentDate) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, {5}, '{6}', 1, GETDATE(), {7}, {8}, {9}, {10}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), accountID, periodID, debit, 0, description, doneBy,
               DR_No, documentNo, documentDate);

            rows = db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, FinancialPeriodId, Debit, Credit, Description, isCashDisburment, EntryDate, EntryUserId, CD_NO, DocumentNo, DocumentDate, RelatedEntryId) " +
              "VALUES({0}, '{1}', {2}, {3}, {4}, {5}, '{6}', 1, GETDATE(), {7}, NULL, {8}, {9}, {10}) ",
              transactionID, DatabaseClass.FormatDateString(transactionDate), cashAccountID, periodID, 0, debit, description, doneBy,
              documentNo, documentDate, rows);

            rows2 = db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            return rows;
        }

        #endregion


        public int PostToGlDetailsFinancialTransaction(int transactionID, string transactionDate, int fromAccountID, int toAccountID,
            int periodID, decimal amount, string description, int doneBy, string documentNo, string documentDate)
        {
            int rows = 0;
            int rows2 = 0;

            //int cashAccountID = tree.GetCashAccount();
            if (fromAccountID == 0 || toAccountID == 0)
                return 0;

            string sql = "";
            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            documentNo = documentNo == null ? "NULL" : string.Format("'{0}'", documentNo.Replace("'", "''"));
            documentDate = documentDate == null ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, FinancialPeriodId, Debit, Credit, Description, IsCashReciept, EntryDate, EntryUserId, CR_No, DocumentNo, DocumentDate) " +
                "VALUES({0}, '{1}', {2}, {3}, {4}, {5}, '{6}', 1, GETDATE(), {7}, NULL, {8}, {9}) ",
                transactionID, DatabaseClass.FormatDateString(transactionDate), toAccountID, periodID, amount, 0, description, doneBy,
                documentNo, documentDate);

            rows = db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, FinancialPeriodId, Debit, Credit, Description, IsCashReciept, EntryDate, EntryUserId, CR_No, DocumentNo, DocumentDate, RelatedEntryId) " +
              "VALUES({0}, '{1}', {2}, {3}, {4}, {5}, '{6}', 1, GETDATE(), {7}, NULL, {8}, {9}, {10}) ",
              transactionID, DatabaseClass.FormatDateString(transactionDate), fromAccountID, periodID, 0, amount, description, doneBy,
              documentNo, documentDate, rows);

            rows2 = db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            return rows;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="debit"></param>
        /// <param name="credit"></param>
        /// <param name="description"></param>
        /// <param name="EntryId"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public bool UpdateNonPostedGlDetail(decimal debit, decimal credit, string description, int EntryId, int doneBy)
        {
            //transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            string sql = string.Format("UPDATE GLDetails SET Debit={0}, Credit={1}, Description='{2}', EditDate=GETDATE(), EditUserId={3} WHERE EntryId={4} ",
                 debit, credit, description, doneBy, EntryId);

            int rows = db.ExecuteNonQuery(sql);
            return (rows == 1);
        }






        #region "PostToGlDetailsChequeRecieptDisbursements"


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="transactionDate"></param>
        /// <param name="accountID"></param>
        /// <param name="periodID"></param>
        /// <param name="credit"></param>
        /// <param name="description"></param>
        /// <param name="doneBy"></param>
        /// <param name="CR_No"></param>
        /// <param name="bankID"></param>
        /// <param name="chequeDate"></param>
        /// <param name="chequeNo"></param>
        /// <param name="documentNo"></param>
        /// <param name="documentDate"></param>
        /// <param name="paymentRecieptsAccount"></param>
        /// <returns></returns>
        public int PostToGlDetailsChequeReciept(int transactionID, string transactionDate, int accountID, int periodID, decimal credit,
            string description, int doneBy, int CR_No, int bankID, string chequeDate, string chequeNo, string documentNo, string documentDate,
            int paymentRecieptsAccount)
        {
            int rows = 0;
            int rows2 = 0;
            //int paymentRecieptsAccount = tree.GetPaymentRecieptsAccount();
            if (paymentRecieptsAccount == 0)
                return 0;


            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            chequeDate = chequeDate.Replace("'", "''");
            chequeNo = chequeNo.Replace("'", "''");
            documentNo = documentNo == null ? "NULL" : string.Format("'{0}'", documentNo.Replace("'", "''"));
            documentDate = documentDate == null ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));


            string sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, IsCashReciept, Description, CR_No, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
                "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
                transactionID, DatabaseClass.FormatDateString(transactionDate), accountID, 0, credit, description, CR_No, bankID,
                DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows = db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, IsCashReciept, Description, CR_No, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), paymentRecieptsAccount, credit, 0, description, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate, rows);

            rows2 = db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            return rows;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="transactionDate"></param>
        /// <param name="accountID"></param>
        /// <param name="periodID"></param>
        /// <param name="debit"></param>
        /// <param name="description"></param>
        /// <param name="doneBy"></param>
        /// <param name="CD_No"></param>
        /// <param name="bankID"></param>
        /// <param name="chequeDate"></param>
        /// <param name="chequeNo"></param>
        /// <param name="documentNo"></param>
        /// <param name="documentDate"></param>
        /// <param name="paymentRecieptsAccount"></param>
        /// <returns></returns>
        public int PostToGlDetailsChequeDisbursements(int transactionID, string transactionDate, int accountID, int periodID, decimal debit,
            string description, int doneBy, int CD_No, int bankID, string chequeDate, string chequeNo, string documentNo, string documentDate,
            int paymentRecieptsAccount)
        {
            int rows = 0;
            int rows2 = 0;
            //int paymentRecieptsAccount = tree.GetPaymentRecieptsAccount();
            if (paymentRecieptsAccount == 0)
                return 0;


            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            chequeDate = chequeDate.Replace("'", "''");
            chequeNo = chequeNo.Replace("'", "''");
            documentNo = documentNo == null ? "NULL" : string.Format("'{0}'", documentNo.Replace("'", "''"));
            documentDate = documentDate == null ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));


            string sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
                "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
                transactionID, DatabaseClass.FormatDateString(transactionDate), accountID, debit, 0, description, CD_No, bankID,
                DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows = db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Credit, Debit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), paymentRecieptsAccount, debit, 0, description, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate, rows);

            //transactionID, DatabaseClass.FormatDateString(transactionDate), paymentRecieptsAccount, debit, 0, description, 0,
            //bankID, DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows2 = db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            return rows;
        }

        #endregion



        #region "ChequeMgmt"

        public int PostToGlDetailsIncomingCheque(int transactionID, string transactionDate, int accountID, int periodID, decimal debit,
            string description, int doneBy, int bankID, string chequeDate, string chequeNo, string documentNo, string documentDate)
        {
            int rows = 0;
            int rows2 = 0;
            //int paymentRecieptsAccount = tree.GetPaymentRecieptsAccount();
            StockSales08Configurations conf = new StockSales08Configurations();
            int cashAccount = conf.CashAccount;
            int bankAccount = conf.BankAccount;


            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            chequeDate = chequeDate.Replace("'", "''");
            chequeNo = chequeNo.Replace("'", "''");
            documentNo = documentNo == null ? "NULL" : string.Format("'{0}'", documentNo.Replace("'", "''"));
            documentDate = documentDate == null ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));


            string sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
                "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
                transactionID, DatabaseClass.FormatDateString(transactionDate), bankAccount, debit, 0, description, "", bankID,
                DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows += db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Credit, Debit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), accountID, debit, 0, description, 0, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows2 += db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
              "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
              transactionID, DatabaseClass.FormatDateString(transactionDate), cashAccount, debit, 0, description, "", bankID,
              DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows += db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Credit, Debit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), bankAccount, debit, 0, description, 0, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows2 += db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            return rows;
        }


        public int PostToGlDetailsOutgoingCheque(int transactionID, string transactionDate, int accountID, int periodID, decimal debit,
            string description, int doneBy, int bankID, string chequeDate, string chequeNo, string documentNo, string documentDate)
        {
            int rows = 0;
            int rows2 = 0;
            StockSales08Configurations conf = new StockSales08Configurations();
            int cashAccount = conf.CashAccount;
            int bankAccount = conf.BankAccount;


            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            chequeDate = chequeDate.Replace("'", "''");
            chequeNo = chequeNo.Replace("'", "''");
            documentNo = documentNo == null ? "NULL" : string.Format("'{0}'", documentNo.Replace("'", "''"));
            documentDate = documentDate == null ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));


            string sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
                "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
                transactionID, DatabaseClass.FormatDateString(transactionDate), bankAccount, debit, 0, description, "", bankID,
                DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows += db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Credit, Debit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), cashAccount, debit, 0, description, 0, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows2 += db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
            "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
            transactionID, DatabaseClass.FormatDateString(transactionDate), accountID, debit, 0, description, "", bankID,
            DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows += db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Credit, Debit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), bankAccount, debit, 0, description, 0, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows2 += db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            return rows;
        }



        public int PostToGlDetailsIncomingRDCheque(int transactionID, string transactionDate, int accountID, int periodID, decimal debit,
           string description, int doneBy, int bankID, string chequeDate, string chequeNo, string documentNo, string documentDate)
        {
            int rows = 0;
            int rows2 = 0;
            //int paymentRecieptsAccount = tree.GetPaymentRecieptsAccount();
            StockSales08Configurations conf = new StockSales08Configurations();
            int cashAccount = conf.CashAccount;
            int bankAccount = conf.BankAccount;


            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            chequeDate = chequeDate.Replace("'", "''");
            chequeNo = chequeNo.Replace("'", "''");
            documentNo = documentNo == null ? "NULL" : string.Format("'{0}'", documentNo.Replace("'", "''"));
            documentDate = documentDate == null ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));


            string sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
                "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
                transactionID, DatabaseClass.FormatDateString(transactionDate), bankAccount, debit, 0, description, "", bankID,
                DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows += db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Credit, Debit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), accountID, debit, 0, description, 0, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows2 += db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
              "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
              transactionID, DatabaseClass.FormatDateString(transactionDate), accountID, debit, 0, description, "", bankID,
              DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows += db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Credit, Debit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), bankAccount, debit, 0, description, 0, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows2 += db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            return rows;
        }


        public int PostToGlDetailsOutgoingRDCheque(int transactionID, string transactionDate, int accountID, int periodID, decimal debit,
            string description, int doneBy, int bankID, string chequeDate, string chequeNo, string documentNo, string documentDate)
        {
            int rows = 0;
            int rows2 = 0;
            StockSales08Configurations conf = new StockSales08Configurations();
            int cashAccount = conf.CashAccount;
            int bankAccount = conf.BankAccount;


            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            chequeDate = chequeDate.Replace("'", "''");
            chequeNo = chequeNo.Replace("'", "''");
            documentNo = documentNo == null ? "NULL" : string.Format("'{0}'", documentNo.Replace("'", "''"));
            documentDate = documentDate == null ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));


            string sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
                "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
                transactionID, DatabaseClass.FormatDateString(transactionDate), bankAccount, debit, 0, description, "", bankID,
                DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows += db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Credit, Debit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), cashAccount, debit, 0, description, 0, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows2 += db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Debit, Credit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate) " +
            "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', {6}, {7}, '{8}', '{9}', {10}, GETDATE(), {11}, {12}, {13}) ",
            transactionID, DatabaseClass.FormatDateString(transactionDate), cashAccount, debit, 0, description, "", bankID,
            DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows += db.ExecuteInsertWithIDReturn(sql, "GLDetails");


            sql = string.Format("INSERT INTO GLDetails(TransactionId, TransactionDate, AccountId, Credit, Debit, IsCheque, isCashDisburment, Description, CD_NO, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, DocumentNo, DocumentDate, RelatedEntryId) " +
               "VALUES({0}, '{1}', {2}, {3}, {4}, 1, 1, '{5}', NULL, {6}, '{7}', '{8}', {9}, GETDATE(), {10}, {11}, {12}, {13}) ",
               transactionID, DatabaseClass.FormatDateString(transactionDate), bankAccount, debit, 0, description, 0, bankID,
               DatabaseClass.FormatDateString(chequeDate), chequeNo, periodID, doneBy, documentNo, documentDate);

            rows2 += db.ExecuteInsertWithIDReturn(sql, "GLDetails");
            if (rows2 != 0)
            {
                string sqlRelated = string.Format("UPDATE GLDetails SET RelatedEntryId={0} WHERE EntryId={1} ", rows2, rows);
                db.ExecuteNonQuery(sqlRelated);
            }
            else
                throw new Exception(Feedback.InsertException());

            return rows;
        }


        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GenerateCRNoteNo()
        {
            string sql = "SELECT ISNULL(MAX(CR_No), 0)+1 AS CR_No from GLDetails ";
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GenerateCDNoteNo()
        {
            string sql = "SELECT ISNULL(MAX(CD_No), 0)+1 AS CD_No from GLDetails ";
            return int.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public decimal SumDebit(int TransactionId)
        {
            // JournalId
            string sql = string.Format("SELECT ISNULL(SUM(Debit), 0)  FROM GLDetails WHERE TransactionId={0} ", TransactionId);
            return decimal.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalCode"></param>
        /// <returns></returns>
        public decimal SumDebit(string journalCode)
        {
            string sql = string.Format("SELECT ISNULL(SUM(Debit), 0)  FROM vwGL_Details WHERE JournalCode={0} ", journalCode);
            return decimal.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public decimal SumCredit(int TransactionId)
        {
            string sql = string.Format("SELECT ISNULL(SUM(Credit), 0)  FROM GLDetails WHERE TransactionId={0} ", TransactionId);
            return decimal.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalCode"></param>
        /// <returns></returns>
        public decimal SumCredit(string journalCode)
        {
            string sql = string.Format("SELECT ISNULL(SUM(Credit), 0)  FROM vwGL_Details WHERE JournalCode={0} ", journalCode);
            return decimal.Parse(db.ExecuteScalar(sql).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalCode"></param>
        /// <param name="centerID"></param>
        /// <returns></returns>
        public decimal SumCredit(string journalCode, int centerID)
        {
            string sql = string.Format("SELECT ISNULL(SUM(Credit), 0)  FROM vwGL_Details WHERE JournalCode={0} AND CenterID={1} ", journalCode, centerID);
            return decimal.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalCode"></param>
        /// <returns></returns>
        public decimal SumDebit(string journalCode, int centerID)
        {
            string sql = string.Format("SELECT ISNULL(SUM(Debit), 0)  FROM vwGL_Details WHERE JournalCode={0} AND CenterID={1} ", journalCode, centerID);
            return decimal.Parse(db.ExecuteScalar(sql).ToString());
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public bool PostJournal(int TransactionId, int doneBy)
        {
            int rows = 0;
            string sql = string.Format("UPDATE GLMaster SET IsPosted=1, EditDate=GETDATE(), EditUserId={1} WHERE TransactionId={0} ",
                TransactionId, doneBy);

            rows = db.ExecuteNonQuery(sql);
            return (rows > 0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public bool UnPostJournal(int transactionId, int donyBy)
        {
            int rows = 0;
            string sql = "UnPostJournal";
            SqlCommand cmd = new SqlCommand(sql, db.cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@transactionId", 4)).Value = transactionId;
            cmd.Parameters.Add(new SqlParameter("@dontBy", 4)).Value = donyBy;
            rows = db.ExecuteNonQuery(cmd);

            //string rejectJournal = string.Format("INSERT INTO GLMasterRejected SELECT TransactionId, TransactionDate, FinancialPeriodId, CenterID, Adjustment, JournalCode, WrittenTo, IsPosted, EntryDate, EntryUserId, EditDate, EditUserId FROM GLMaster WHERE TransactionId={0} ", transactionId);
            //db.ExecuteNonQuery(rejectJournal);

            //rejectJournal = string.Format("INSERT INTO GLDetailsRejected SELECT EntryId, TransactionId, TransactionDate, AccountId, Debit, Credit, RelatedEntryId, DocumentNo, DocumentDate, IsCashReciept, isCashDisburment, IsTransfer, IsCheque, IsPosted, Description, CR_No, CD_No, BankId, ChequeDate, ChequeNo, FinancialPeriodId, EntryDate, EntryUserId, EditDate, EditUserId WHERE TransactionId={0} ", transactionId);

            //string sql = string.Format("DELETE FROM GLMaster WHERE TransactionId={0} ", transactionId);

            //rows = db.ExecuteNonQuery(sql);
            return (rows > 0);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public DataTable SearchPostedGLDetails(int TransactionId)
        {
            string sql = string.Format("SELECT EntryId, Debit, Credit, Description, AccountName, AccountCode, JournalCode, TransactionDate FROM vwGLMasterDetailed WHERE IsPosted=1 AND TransactionId={0} ORDER BY JournalCode ",
                TransactionId);

            return db.ExecuteQuery(sql);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalCode"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public DataTable SearchGLDetails(int journalCode, int doneBy)
        {
            // JournalId, 
            string sql = string.Format("SELECT EntryId, JournalCode, TransactionId, TransactionDate, AccountName, Debit, Credit, Description, CR_No, CD_No FROM vwGL_Details WHERE JournalCode={0} AND EntryUserId={1} ",
                journalCode, doneBy);

            return db.ExecuteQuery(sql);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetOpeningBalances() //int journalCode, int doneBy) JournalId, 
        {
            string sql = "SELECT EntryId, JournalCode, TransactionId, TransactionDate, AccountName, Debit, Credit, Description, CR_No, CD_No FROM vwGL_Details WHERE JournalCode=1 AND IsPosted=1 ";
            //string.Format(journalCode, doneBy);

            return db.ExecuteQuery(sql);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerID"></param>
        /// <returns></returns>
        public DataTable GetOpeningBalances(int centerID) //int journalCode, int doneBy) JournalId, 
        {
            string sql = string.Format("SELECT EntryId, JournalCode, TransactionId, TransactionDate, AccountName, Debit, Credit, Description, CR_No, CD_No FROM vwGL_Details WHERE JournalCode=1 AND IsPosted=1 AND CenterID={0} ", centerID);
            return db.ExecuteQuery(sql);
        }

        public DataTable View_By_TrasactionId(int transactionId)
        {
            string Sql = string.Format(" SELECT * FROM [vwGL_Details] WHERE TransactionId={0}", transactionId);
            return db.ExecuteQuery(Sql);
        }




        #region TempGL

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="transactionDate"></param>
        /// <param name="accountID"></param>
        /// <param name="periodID"></param>
        /// <param name="debit"></param>
        /// <param name="credit"></param>
        /// <param name="description"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public int PostTempGlDetails(int transactionID, string transactionDate, int accountID, int periodID, decimal debit, decimal credit,
            string description, int doneBy, string documentNo, string documentDate)
        {
            // int journalID JournalId,  journalID, 
            transactionDate = transactionDate.Replace("'", "''");
            description = description.Replace("'", "''");
            documentNo = documentNo.Replace("'", "''");
            documentDate = documentDate.Replace("'", "''");
            string documentDatepart = (documentDate == "") ? "NULL" : string.Format("'{0}'", DatabaseClass.FormatDateString(documentDate));

            string sql = string.Format("INSERT INTO TempGLDetails(TransactionId, TransactionDate, FinancialPeriodId, AccountId, Debit, Credit, Description, EntryDate, EntryUserId, DocumentNo, DocumentDate) VALUES({0}, '{1}', {2}, {3}, {4}, {5}, '{6}', GETDATE(), {7}, '{8}', {9}) ",
                transactionID, DatabaseClass.FormatDateString(transactionDate), periodID, accountID, debit, credit, description, doneBy,
                documentNo, documentDatepart);

            int rows = db.ExecuteInsertWithIDReturn(sql, "TempGLDetails");
            return rows;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <returns></returns>
        public int PostGLDetailsFromTemp(int transactionID)
        {
            string sql = string.Format("INSERT INTO GLDetails(TransactionId, AccountId, TransactionDate, Debit, Credit, Description, FinancialPeriodId, EntryDate, EntryUserId) " +
                " SELECT TransactionId, AccountId, TransactionDate, Debit, Credit, Description, FinancialPeriodId, EntryDate, EntryUserId FROM TempGLDetails WHERE TransactionId={0} ",
                transactionID);

            int rows = db.ExecuteNonQuery(sql);
            return rows;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public int ClearTempGLDetails(int transactionID, int doneBy)
        {
            string sql = string.Format("DELETE FROM TempGLDetails WHERE TransactionId={0} AND EntryUserId={1} ", transactionID, doneBy);
            int rows = db.ExecuteNonQuery(sql);
            return rows;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryID"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public bool DeleteTempGLDetails(int entryID, int doneBy)
        {
            string sql = string.Format("DELETE FROM TempGLDetails WHERE EntryId={0} AND EntryUserId={1} ", entryID, doneBy);
            int rows = db.ExecuteNonQuery(sql);
            return (rows >= 0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryID"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public bool DeleteGLDetails(int entryID)
        {
            string sql = string.Format("DELETE FROM GLDetails WHERE EntryId={0} ", entryID);
            int rows = db.ExecuteNonQuery(sql);
            return (rows >= 0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public DataTable SearchTempGLDetails(int transactionID, int doneBy)
        {
            string sql = string.Format("SELECT EntryId, JournalId, TransactionDate, AccountName, Debit, Credit, Description, CR_No, CD_No FROM vwTempGlDetailed WHERE TransactionId={0} AND EntryUserId={1} ",
                transactionID, doneBy);

            return db.ExecuteQuery(sql);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public Decimal SumTempDebit(int transactionID, int doneBy)
        {
            string sql = string.Format("SELECT ISNULL(SUM(Debit), 0) FROM vwTempGlDetailed WHERE TransactionId={0} AND EntryUserId={1} ",
                transactionID, doneBy);

            return decimal.Parse(db.ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public Decimal SumTempCredit(int transactionID, int doneBy)
        {
            string sql = string.Format("SELECT ISNULL(SUM(Credit), 0) FROM vwTempGlDetailed WHERE TransactionId={0} AND EntryUserId={1} ",
                transactionID, doneBy);

            return decimal.Parse(db.ExecuteScalar(sql).ToString());
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="doneBy"></param>
        /// <returns></returns>
        public DataTable GetPostedTempGLDetails(int transactionID, int doneBy)
        {
            // ORDER BY JournalCode
            string sql = string.Format("SELECT EntryId, JournalCode, Debit, Credit, Description, AccountName, AccountCode, TransactionDate FROM vwGLMasterDetailed WHERE IsPosted=0 AND IsCashReciept=0 AND isCashDisburment=0 AND TransactionId={0} AND EntryUserId={1} ",
                transactionID, doneBy);

            return db.ExecuteQuery(sql);
        }

        #endregion


    }
}