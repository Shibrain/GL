using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Data;

namespace BLL.Currencies
{ 
    public class Currencies
    {
        private DatabaseClass DB = new DatabaseClass();
        public DataTable View(int CompId)
        {
            string Sql = string.Format("Select (CurrencyName +' '+ CurrencyNameAr +' '+ CurrencyShortName ) as CurrencyNameAr ,CurrencyId  from  Currencies where CompId={0} ", CompId);
            return DB.ExecuteQuery(Sql);


        }
         public static int GetDefaultCurrency(int CompId)
         {
             string sql = string.Format("SELECT TOP 1 DefaultCurrency FROM Configrations where CompId={0} ",CompId);
             return int.Parse(new DatabaseClass().ExecuteScalar(sql).ToString());
         }

         public static decimal GetCurrencyConversionRatio(int from, int to, int CompId)
         {
             if (from == to)
                 return 1;
             else
             {
                 string conversionRatioSql = string.Format("SELECT Ratio FROM CurrencyConversion WHERE CurrencyFrom={0} AND CurrencyTo={1} and CompId={2} ", from, to, CompId);
                 return decimal.Parse(new DatabaseClass().ExecuteScalar(conversionRatioSql).ToString());
             }
         }
    }
}