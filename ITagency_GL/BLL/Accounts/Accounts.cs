using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DAL;
namespace BLL.Accounts
{
    public class Accounts
    {
        private DatabaseClass DB = new DatabaseClass();
        private DataTable dtAccountsTree;
        public DataTable View(int CompId)
        {
            string Sql = string.Format("Select AccountName +' - '+AccountCode as AccountName,AccountId,UpperAccountId from  AccountTree where CompId={0} ", CompId);
            return DB.ExecuteQuery(Sql);


        }
        public DataTable View_By_AccountId(int AccountId)
        {
            string Sql = string.Format("Select * from AccountTree where AccountId={0} ", AccountId);
            return DB.ExecuteQuery(Sql);


        }
        public DataTable View_By_UpperAccountId(int UpperAccountId)
        {
            string Sql = string.Format("Select * from AccountTree where UpperAccountId={0} ", UpperAccountId);
            return DB.ExecuteQuery(Sql);


        }
        public DataTable ViewBy_BANK_ID(int BANK_ID)
        {
            string Sql = string.Format("Select * from AccountTree where BANK_ID={0} and IS_VALID={1} ", BANK_ID, 1);
            return DB.ExecuteQuery(Sql);


        }


        public bool IsExist(string ACCOUNT_NO, int BANK_ID, string BANK_BRANCH_ID, int EmpId, int CompId)
        {
            string Sql = string.Format("Select * from AccountId where ACCOUNT_NO='{0}' and BANK_ID={1} and BANK_BRANCH_ID='{2}' and CompId={3} and EmpId={4} ", ACCOUNT_NO, BANK_ID, BANK_BRANCH_ID, CompId, EmpId);
            DataTable dt = DB.ExecuteQuery(Sql);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;

        }

        public bool Update(string ACCOUNT_NO, int BANK_ID, string BANK_BRANCH_ID, int AccountId_TYPE_ID, int UP_USERID, string UP_DATE, int EMPID, int CompId)
        {

            string Sql = string.Format("Update AccountId set IS_VALID={0}  where EMPID={1}  ", 0, EMPID);
            int ExSql = DB.ExecuteNonQuery(Sql);
            if (ExSql > 0)
            {
                Sql = string.Format("Insert into AccountId(ACCOUNT_NO, BANK_ID,  BANK_BRANCH_ID,  AccountId_TYPE_ID,CR_USERID, CR_DATE,EMPID,CompId)values('{0}',{1},'{2}',{3},{4},'{5}',{6},{7})", ACCOUNT_NO, BANK_ID, BANK_BRANCH_ID, AccountId_TYPE_ID, UP_USERID, DatabaseClass.FormatDateString(UP_DATE), EMPID, CompId);
                ExSql = DB.ExecuteNonQuery(Sql);
                if (ExSql > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;




        }




        public bool Insert_New_Account(string AccountName, string OpenDate, int IsOpen, int IsNode, int AccountLevelNo, int AccountTypeID, int EntryUserId, int CompId, string Entry_Date, string upperAccountId)
        {
            string Sql = "";
            if (upperAccountId == "" || upperAccountId == null)
            {
                Sql = string.Format("INSERT INTO AccountTree(AccountName, AccountCode,OpenDate, IsOpen, IsNode, AccountLevelNo,AccountTypeID, EntryUserId,CompId,Entry_Date) VALUES('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8},'{9}') ",
                       AccountName, this.GenerateNewAccountCode(0), DatabaseClass.FormatDateString(OpenDate),
                        IsOpen, DatabaseClass.Bool2Int(true), 1, AccountTypeID, EntryUserId, CompId, DatabaseClass.FormatDateString(Entry_Date));
                // DatabaseClass.Bool2Int(chkIsOpen.Checked)
            }
            else
            {

                int newAccountLevel = this.GetAccountLevel(int.Parse(upperAccountId)) + 1;
                string NewAccount = this.GenerateNewAccountCode(int.Parse(upperAccountId.ToString())).ToString();
                Sql = string.Format("INSERT INTO AccountTree(AccountName, AccountCode,OpenDate, IsOpen, IsNode, AccountLevelNo,AccountTypeID, EntryUserId,CompId,Entry_Date,upperAccountId) VALUES('{0}', '{1}', '{2}', {3}, {4}, '{5}', {6}, {7}, {8},'{9}','{10}') ",
                      AccountName, NewAccount, DatabaseClass.FormatDateString(OpenDate),
                       IsOpen, DatabaseClass.Bool2Int(true), newAccountLevel, AccountTypeID, EntryUserId, CompId, DatabaseClass.FormatDateString(Entry_Date), upperAccountId);
                string test = Sql;
            }
            int ExSql = DB.ExecuteNonQuery(Sql);
            if (ExSql > 0)
                return true;
            else
                return false;
            // newAccountID = DB.ExecuteInsertWithIDReturn(sql, "AccountTree");
        }

        public bool Delete(int AccountId)
        {
            string Sql = string.Format("Delete AccountTree where AccountId={0} and (UpperAccountId !={0} or UpperAccountId is null)  ", AccountId);
            int ExSql = DB.ExecuteNonQuery(Sql);
            if (ExSql > 0)
                return true;
            else
                return false;

        }
        public string GenerateNewAccountCode(int upperAccountId)
        {
            // first Get Upper Account Code
            if (upperAccountId != 0)
            {
                string latestCode = "";
                string upperAccountSql = string.Format("SELECT AccountCode FROM AccountTree WHERE AccountId={0} ", upperAccountId);
                string upperAccountCode = DB.ExecuteScalar(upperAccountSql).ToString();
                string maxCurrentLevelAccountCodeSql = string.Format("SELECT TOP 1 AccountCode FROM AccountTree WHERE UpperAccountId={0} order by accountID desc ", upperAccountId);
                DataTable dt = DB.ExecuteQuery(maxCurrentLevelAccountCodeSql);
                if (dt.Rows.Count == 0)
                    return string.Format("{0}-{1}", upperAccountCode, 1);
                else
                {
                    string lastAccountCode = DB.ExecuteScalar(maxCurrentLevelAccountCodeSql).ToString();
                    if (lastAccountCode.Length == 1)
                        latestCode = lastAccountCode;
                    else
                    {
                        string[] parts = lastAccountCode.Split('-');
                        latestCode = parts[parts.Length - 1];
                    }

                    int newAccountCode = int.Parse(latestCode) + 1;
                    return string.Format("{0}-{1}", upperAccountCode, newAccountCode);
                }

            }
            else
            {
                string maxCurrentLevelAccountCodeSql = "SELECT ISNULL(MAX(AccountId), '0') FROM AccountTree WHERE UpperAccountId IS NULL ";
                int newAccountCode = int.Parse(DB.ExecuteScalar(maxCurrentLevelAccountCodeSql).ToString());
                string Sql = "";
                int Sum = 0;
                if (newAccountCode > 0)
                {
                    Sql = string.Format("Select ISNULL(AccountCode,0) from AccountTree where AccountId={0}", newAccountCode);
                    newAccountCode = int.Parse(DB.ExecuteScalar(Sql).ToString());
                    Sum = newAccountCode + 1;
                }
                else
                    Sum = 1;


                return Sum.ToString();
            }
        }
        public int GetAccountLevel(int AccountID)
        {
            string sql = string.Format("SELECT ISNULL(AccountLevelNo, 0) FROM AccountTree WHERE AccountId={0} ", AccountID);
            return int.Parse(DB.ExecuteScalar(sql).ToString());
        }




        public DataTable GetAccountTree(int CompId)
        {
            string sql = string.Format("SELECT AccountId, IsNode, '|' +(REPLICATE('-', AccountLevelNo *2) + '(' + AccountCode + ')' + AccountName ) AS AccountFullName, (SELECT COUNT(*) FROM AccountTree WHERE UpperAccountId=sc.AccountId) AS ChildNodes FROM AccountTree sc WHERE AccountLevelNo=1 and CompId={0} ",CompId);
            DataTable dt = DB.ExecuteQuery(sql);

             dtAccountsTree = new DataTable();
            dtAccountsTree.Columns.Add(new DataColumn("AccountId", typeof(int)));
            dtAccountsTree.Columns.Add(new DataColumn("AccountFullName", typeof(string)));

            foreach (DataRow dr in dt.Rows)
            {
                if (bool.Parse(dr["IsNode"].ToString()))
                    dtAccountsTree.Rows.Add(dr["AccountId"].ToString(), dr["AccountFullName"].ToString());

                GetAccountSubTree(int.Parse(dr["AccountId"].ToString()));
            }

            return dtAccountsTree;
        }
        private void GetAccountSubTree(int accountNo)
        {
            string sql = string.Format("SELECT AccountId, IsNode, '|' + (REPLICATE('-', AccountLevelNo * 2) + ' (' + AccountCode + ') ' + AccountName) AS AccountFullName FROM AccountTree WHERE UpperAccountId={0} ", accountNo);
            DataTable dt = DB.ExecuteQuery(sql);
            foreach (DataRow dr in dt.Rows)
            {
                //if (bool.Parse(dr["IsNode"].ToString()))
                dtAccountsTree.Rows.Add(dr["AccountId"].ToString(), dr["AccountFullName"].ToString());

                GetAccountSubTree(int.Parse(dr["AccountId"].ToString()));
            }
        }
        public bool CheckAccountIsOpenNode(int accountID)
        {
            string sql = string.Format("SELECT IsOpen, IsNode FROM AccountTree WHERE AccountId={0} ", accountID);
            DataTable dt = DB.ExecuteQuery(sql);
            if (dt.Rows.Count >= 1)
            {
                DataRow dr = dt.Rows[0];
               
                if (!(bool.Parse(dr["IsOpen"].ToString())) || !(bool.Parse(dr["IsNode"].ToString())))
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public bool CheckAccountIsOpenUpper(int accountID)
        {
            string sql = string.Format("SELECT IsOpen, IsNode FROM AccountTree WHERE AccountId={0} and IsOpen=1 and IsNode=1 ", accountID);
            DataTable dt = DB.ExecuteQuery(sql);
            if (dt.Rows.Count >= 1)
            {
                DataRow dr = dt.Rows[0];
                
                //if ((!bool.Parse(dr["IsOpen"].ToString())) || (bool.Parse(dr["IsNode"].ToString())))
                //    return false;
                //else
                    return true;
            }
            else
                return false;
        }





    }
}

