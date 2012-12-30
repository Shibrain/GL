using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DAL;

/// <summary>
/// Summary description for Policy
/// </summary>
public class Policy
{
    public Policy()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public const string EmptyPolicy = "0000000000000000";
    public const string FullPolicy = "7777777777777777";


    public enum Privilege
    {
        Lookups = 0,
        AccountTree,
        GeneralPayments,
        PostJournal,
        Journal,
        Reports,
        Security
    }

    public enum Operation
    {
        Insert = 0,
        Update,
        Delete
    }

    //public enum AccountTypes
    //{
    //    Assets=1
    //}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="privilege"></param>
    /// <param name="operation"></param>
    /// <param name="Policy"></param>
    /// <returns></returns>
    public static bool HasPrivilege(Privilege privilege, Operation operation, string Policy)
    {
        string bits = Convert.ToString(Convert.ToInt32(Policy[(int)privilege].ToString()), 2);
        bits += EmptyPolicy;
        return bits[(int)operation] == '1' ? true : false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="dtPolicy"></param>
    /// <returns></returns>
    public static string GeneratePolicy(DataTable dtPolicy)
    {
        int colsCount = dtPolicy.Columns.Count;
        int rowsCount = dtPolicy.Rows.Count;
        string bits = string.Empty;
        DataRow dr;
        string policy = string.Empty;

        for (int i = 0; i < rowsCount; i++)
        {
            bits = string.Empty;
            dr = dtPolicy.Rows[i];
            for (int j = 0; j < colsCount - 1; j++)
            {
                bits = (Convert.ToBoolean(dr[j]) ? "1" : "0") + bits;
            }

            policy += Convert.ToInt32(bits, 2).ToString();
        }

        return policy;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="policy"></param>
    /// <returns></returns>
    public static DataTable GeneratePolicyTable(string policy)
    {
        policy += EmptyPolicy;
        DataTable dtPolicy = PolicyTable();

        DataRow dr;


        foreach (string p in Enum.GetNames(typeof(Privilege)))
        {
            dr = dtPolicy.NewRow();
            dr["Privilege"] = p;
            Privilege privilege;
            privilege = (Privilege)Enum.Parse(typeof(Privilege), p);
            dr["Insert"] = HasPrivilege(privilege, Operation.Insert, policy);
            dr["Update"] = HasPrivilege(privilege, Operation.Update, policy);
            dr["Delete"] = HasPrivilege(privilege, Operation.Delete, policy);
            dtPolicy.Rows.Add(dr);
        }

        dtPolicy.AcceptChanges();
        return dtPolicy;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static DataTable PolicyTable()
    {
        DataTable dtPolicy = new DataTable();
        dtPolicy.Columns.Add(new DataColumn("Privilege", System.Type.GetType("System.String")));

        foreach (string o in Enum.GetNames(typeof(Operation)))
            dtPolicy.Columns.Add(new DataColumn(o, System.Type.GetType("System.Boolean")));

        return dtPolicy;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public static bool CheckIfAdmin(int userID)
    {
        string sql = string.Format("SELECT IsAdmin FROM SystemUsers WHERE UserID={0} ", userID);
        return bool.Parse(new DatabaseClass().ExecuteScalar(sql).ToString());
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="accountID"></param>
    /// <returns></returns>
    public static bool CheckUserIsPermittedToAccountByCenterID(int userID, int accountID)
    {
        if (CheckIfAdmin(userID))
            return true;
        else
        {
            string sql = string.Format("SELECT * FROM vwAccontsProfitCenters WHERE AccountID={0} AND CenterID IN (SELECT CenterID FROM SystemUsers WHERE UserID={1}) ",
                accountID, userID);

            return (new DatabaseClass().ExecuteQuery(sql).Rows.Count == 1);
        }
    }


}
