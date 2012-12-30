using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Data;

namespace BLL.Financial_Period
{
    public class Financial_Period
    {
        private DatabaseClass db = new DatabaseClass();
        public DataTable view( int CompId)
        {
            string sql = string.Format("SELECT * FROM FinancialPeriods WHERE  CompId ={0} ",
                  CompId);

            DataTable dt = db.ExecuteQuery(sql);
            return dt;
           

        }
        public static DataTable GetFinancialPeriod(int compId)
        {
            
            string sql = string.Format("SELECT TOP 1 PeriodId, PeriodName, StartDate, EndDate FROM FinancialPeriods WHERE Status=1 and CompId={0} ORDER BY StartDate DESC ",compId);
            return new DatabaseClass().ExecuteQuery(sql);
        }

    }
}