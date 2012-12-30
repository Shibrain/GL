using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Configuration;



namespace DAL
{
    public class DatabaseClass
    {

        #region Properties

        private static string dbName;
        private static string serverName;


        private DataSet ds;
        private SqlCommand cmd;
        private SqlDataAdapter da;


        public static string connectionString;
        public SqlConnection cn;


        public static string DBName
        {
            get { return DatabaseClass.dbName; }
            //set { DatabaseClass.dBName = value; }
        }
        
        public static string ServerName
        {
            get { return DatabaseClass.serverName; }
            //set { DatabaseClass.serverName = value; }
        }

        public string DatabaseClassConnectionString
        {
            get { return connectionString; }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Creates a connection to the database using the connection string saved in app.config
        /// </summary>
        public DatabaseClass()
        {

//  serverName = @".\sqlexpress   ";
      serverName = @"(Local)";
      // serverName = @"(Local)";
      dbName = "oma"; 
           
            connectionString = string.Format("server={0}; database={1}; uid=test; pwd=123; trusted_connection=false; Connect Timeout=600 ", ServerName, DBName);
        
            

            cn = new SqlConnection(connectionString);
        }


        /// <summary>
        /// Creates a connection to the database using the connection string saved in app.config
        /// </summary>
        public DatabaseClass(string connectionStringText)
        {
            connectionString = connectionStringText; //ConfigurationManager.ConnectionStrings[connectionStringName].ToString();
            cn = new SqlConnection(connectionString);
        }


        /// <summary>
        /// Creates a connection to the database using a customized connection string 
        /// </summary>
        /// <param name="sName">Server Name</param>
        /// <param name="dName">Database Name</param>
        public DatabaseClass(string sName, string dName)
        {
            dbName = dName;
            serverName = sName;
            //trusted_connection=yes
            //connectionString = string.Format("server={0}; database={1};trusted_connection=yes", ServerName, DBName);
            //connectionString = string.Format(@" Server=tcp:{0},1433; Network Library=DBMSSOCN; Database={1}; User ID=test_att; Password=123; ", ServerName, DBName);
            connectionString = string.Format(@" Server={0}; Database={1}; uid=test; pwd=123; trusted_connection=false; Connect Timeout=600 ", ServerName, DBName);
            //connectionString = string.Format("DSN=AcademyAttend;Uid=test_att;Pwd=123;");
            cn = new SqlConnection(connectionString);
        }

        #endregion



        /// <summary>
        /// Opens a connection to the database
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();
            }
            catch (Exception ex)
            {
                throw ex; //new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Closes the open connection
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            catch (Exception ex)
            {
                throw ex; // new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  To Execute SQL statement that returns result in rows
        /// </summary>
        /// <param name="sqlStmt">SQL Statement</param>
        /// <returns>result rows</returns>
        public DataTable ExecuteQuery(string sqlStmt)
        {
            cmd = new SqlCommand(sqlStmt, cn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();

            cmd.CommandTimeout = 600;

            try
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex; // new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return ds.Tables[0];
        }


        /// <summary>
        /// To Execute SQL command containing SQL Statement that returns result in rows
        /// </summary>
        /// <param name="cmd">QL command containing SQL Statement</param>
        /// <returns>result rows</returns>
        public DataTable ExecuteQuery(SqlCommand cmd)
        {
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();

            cmd.CommandTimeout = 600;

            try
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex; // new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return ds.Tables[0];
        }


        /// <summary>
        /// To execute insert/update/delete SQL statement
        /// </summary>
        /// <param name="sql">insert/update/delete SQL statement</param>
        /// <returns>No. of rows affected by the statement</returns>
        public int ExecuteNonQuery(string sql)
        {
            int recordsAffected;

            try
            {
                cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 600;

                if (cn.State != ConnectionState.Open)
                    cn.Open();

                recordsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex; // new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return recordsAffected;
        }


        /// <summary>
        /// To execute SQL command containing insert/update/delete SQL statement
        /// </summary>
        /// <param name="cmd">SQL command containing insert/update/delete SQL statement</param>
        /// <returns>No. of rows affected by the statement</returns>
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            int recordsAffected;
            cmd.CommandTimeout = 600;

            try
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                recordsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex; // new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return recordsAffected;
        }

        /// <summary>
        /// Executes a group of SQL statements as a transaction
        /// </summary>
        /// <param name="sqlQuery">The group of Insert, Update, and/or Delete statements to execute</param>
        /// <returns>The total number of rows affected by the transaction</returns>
        public int ExecuteTransaction(string[] transactionString)
        {
            // configure the transaction's command
            SqlTransaction trans = cn.BeginTransaction();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandTimeout = 600;
            cmd.Transaction = trans;
 
            int totalRecordsAffected = 0; 
           
            // execute the transaction
            for (int i = 0; i < transactionString.Length; i++)
            {
                // set or update the transaction's command to point to the current statement to execute 
                cmd.CommandText = transactionString[i];

                try
                {
                    if (cn.State != ConnectionState.Open)
                        cn.Open();

                    // execute the statement in the transaction
                    totalRecordsAffected += cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex; // new Exception(ex.Message);
                }
            }

            // close the connection, then commit the transaction
            CloseConnection();
            trans.Commit();

            // return the total number of rows affected
            return totalRecordsAffected;
        }


        /// <summary>
        /// To execute SQL command containing insert SQL statement returning the new identity column value
        /// </summary>
        /// <param name="sql">Insert SQL Statement</param>
        /// <param name="tableName">Table affected by the insert statement</param>
        /// <returns>New identity value</returns>
        public int ExecuteInsertWithIDReturn(string sql, string tableName)
        {
            int returnValue, recordsAffected;

            try
            {
                cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 600;

                if (cn.State != ConnectionState.Open)
                    cn.Open();

                recordsAffected = cmd.ExecuteNonQuery();
                if (recordsAffected == 1)
                {
                    sql = string.Format("select ident_current('{0}') ", tableName);
                    returnValue = Int32.Parse(ExecuteScalar(sql).ToString());
                    return returnValue;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex; // new Exception(ex.Message);
            }

            // return recordsAffected;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int ExecuteInsertWithIDReturn(SqlCommand cmd, string tableName)
        {
            int returnValue, recordsAffected;

            try
            {
                //cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 600;

                if (cn.State != ConnectionState.Open)
                    cn.Open();

                recordsAffected = cmd.ExecuteNonQuery();
                if (recordsAffected == 1)
                {
                    string sql = string.Format("select ident_current('{0}') ", tableName);
                    returnValue = Int32.Parse(ExecuteScalar(sql).ToString());
                    return returnValue;
                }
                else
                    return 0;

            }
            catch (Exception ex)
            {
                throw ex; //new Exception(ex.Message);
            }

            // return recordsAffected;
        }


        /// <summary>
        /// To execute SQL Statement that returns one value
        /// </summary>
        /// <param name="sql">SQL statement</param>
        /// <returns>Retrieved value</returns>
        public Object ExecuteScalar(string sql)
        {
            Object returnValue;

            try
            {
                cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 600;

                if (cn.State != ConnectionState.Open)
                    cn.Open();

                returnValue = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex; // new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            if (returnValue == null)
                return 0;
            else
                return returnValue;
        }


        /// <summary>
        /// To execute SQL Statement that returns one value
        /// </summary>
        /// <param name="cmd">SQL command containing SQL statement</param>
        /// <returns>Retrieved value</returns>
        public decimal ExecuteScalar(SqlCommand cmd)
        {
            decimal returnValue;

            try
            {
                cmd.CommandTimeout = 600;

                if (cn.State != ConnectionState.Open)
                    cn.Open();

                returnValue = (decimal)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex; //new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return returnValue;
        }




        /// <summary>
        /// To format date string to be in the format DMY
        /// </summary>
        /// <param name="dt">Date the must be formatted</param>
        /// <returns>date in string formatted in DMY</returns>
        public static string FormatDateDMY(DateTime dt)
        {
            // To format date string to be in the format DMY
            string date = string.Format("{0}/{1}/{2}", dt.Day, dt.Month, dt.Year);
            return date;
        }

        /// <summary>
        /// To format date string to be in the format MDY
        /// </summary>
        /// <param name="dt">Date the must be formatted</param>
        /// <returns>date in string formatted in MDY</returns>
        public static string FormatDateMDY(DateTime dt)
        {
            // To format date string to be in the format DMY
            string date = string.Format("{1}/{0}/{2}", dt.Day, dt.Month, dt.Year);
            return date;
        }


        /// <summary>
        ///  To format date string to be in the format MDY after parsing it
        /// </summary>
        /// <param name="strDate">String of date the must be formatted</param>
        /// <returns>date in string formatted in MDY</returns>
        //public static string FormatDateString(string strDate)
        //{
        //    DateTime passedDate = DateTime.Parse(strDate);
        //    string date = string.Format("{1}/{0}/{2}", passedDate.Day, passedDate.Month, passedDate.Year);
        //    return date;
        //}
        public static string FormatDateString(string strDate)
        {
            DateTime passedDate = DateTime.Parse(strDate);
            string date = string.Format("{1}/{0}/{2}", passedDate.Day, passedDate.Month, passedDate.Year);
            return date;
        }

        /// <summary>
        /// To format date string after parsing it to be in a determined format
        /// </summary>
        /// <param name="dateText">String of date the must be formatted</param>
        /// <param name="localeName">Locale short name (e.g: ar-eg, en-us, ar-sa, ...)</param>
        /// <param name="dateFormat">Date format, such as MM/dd/yyyy: MDY, dd/MM/yyyy</param>
        /// <returns>date in string formatted</returns>
        public static string FormatDateByLocale(string dateText, string localeName, string dateFormat)
        {
            DateTime parsedDateTime = Convert.ToDateTime(dateText, new CultureInfo(localeName));
            string parsedDateTimeString = parsedDateTime.ToString(dateFormat);
            return parsedDateTimeString;
        }


        /// <summary>
        /// To format date string after parsing according to Ar-Egypt locale it to be in MDY format
        /// </summary>
        /// <param name="dateText">String of date the must be formatted</param>
        /// <returns>ate in string formatted</returns>
        //public static string FormatDateString(string dateText)
        //{
        //    return FormatDateByLocale(dateText, "ar-eg", "MM/dd/yyyy");
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="localeName"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static string FormatDateTimeByLocale(string dateText, string localeName, string dateFormat)
        {
            DateTime parsedDateTime = DateTime.Parse(dateText, new CultureInfo(localeName));
            string parsedDateTimeString = parsedDateTime.ToString(dateFormat);
            return parsedDateTimeString;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="localeName"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static string FormatTimeByLocale(string dateText, string localeName, string dateFormat)
        {
            DateTime parsedDateTime = DateTime.Parse(dateText, new CultureInfo(localeName));
            //string parsedDateTimeString = parsedDateTime.ToShortTimeString(dateFormat);
            string parsedDateTimeString = parsedDateTime.ToShortTimeString();
            return parsedDateTimeString;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static string FormatTimeArEgMDY(string dateText)
        {
            return FormatTimeByLocale(dateText, "ar-eg", "hh:mm");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static string FormatDatTimeArEgMDY(string dateText)
        {
            return FormatDateTimeByLocale(dateText, "ar-eg", "MM/dd/yyyy hh:mm");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="returnDayShortName"></param>
        /// <returns></returns>
        public static string GetDayOfDate(string date, bool returnDayShortName)
        {
            DateTime dtAttendDate = DateTime.Parse(date, new CultureInfo("ar-eg"));

            if (returnDayShortName)
                return dtAttendDate.DayOfWeek.ToString().Substring(0, 3);
            else
                return dtAttendDate.DayOfWeek.ToString();
        }


        /// <summary>
        /// This function helps to make sure that there are no children rows for a parent row that we need to delete
        /// </summary>
        /// <param name="childTableName">Name of table that contains child rows</param>
        /// <param name="foreignKeyColName">Foreign key column name, which reference the parent row that we want to delete</param>
        /// <param name="foreignKeyColValue">The parent row value that we want to delete</param>
        /// <returns>number of children rows that refernce that parent row to be delete</returns>
        public int GetCountOfChildRecordsBeforeDeletingParent(string childTableName, string foreignKeyColName, string foreignKeyColValue)
        {
            string sql = string.Format("select count(*) from {0} where {1}={2} ", childTableName, foreignKeyColName, foreignKeyColValue);
            int records = int.Parse(ExecuteScalar(sql).ToString());
            return records;
        }


        /// <summary>
        /// This function helps to make sure that there are no children rows for a parent row that we need to delete
        /// </summary>
        /// <param name="childTableName">Name of table that contains child rows</param>
        /// <param name="foreignKeyColName">Foreign key column name, which reference the parent row that we want to delete</param>
        /// <param name="foreignKeyColValue">The parent row value that we want to delete</param>
        /// <returns>number of children rows that refernce that parent row to be delete</returns>
        public int GetCountOfChildRecordsBeforeDeletingParent(string childTableName, string foreignKeyColName, int foreignKeyColValue)
        {
            string sql = string.Format("select count(*) from {0} where {1}={2} ", childTableName, foreignKeyColName, foreignKeyColValue);
            int records = int.Parse(ExecuteScalar(sql).ToString());
            return records;
        }


        /// <summary>
        /// To convert boolean value to integer values
        /// </summary>
        /// <param name="value">Boolean value (True or False)</param>
        /// <returns>1 if true, 0 if false</returns>
        /// 
        public static int Bool2Int(bool value)
        {
            // if the passed value was true, return 1, otherwise return 0
            if (value)
                return 1;
            else
                return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Int2Bool(int value)
        {
            if (value == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Int2Bool(string value)
        {
            if (value == "0" || value.ToLower() == "false")
                return false;
            else
                return true;
        }


        /// <summary>
        /// To validate if the passed value is number or not
        /// </summary>
        /// <param name="s">Value to check if it is a number</param>
        /// <returns>Confirmation if it is or not</returns>
        public static bool IsNumeric(string s)
        {
            try
            {
                int.Parse(s);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// to get two characters string of the passed in number
        /// </summary>
        /// <param name="number">integer number</param>
        /// <returns>string of that number</returns>
        public static string GetNumberCode(int number)
        {
            if (number < 10)
                return string.Format("0{0}", number);
            else
                return number.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public static string GetNumber3DigitsString(int serial)
        {
            if (serial >= 1 && serial <= 9)
                return "00" + serial.ToString();
            else if (serial > 9 && serial <= 99)
                return "0" + serial.ToString();
            else if (serial > 99)
                return serial.ToString();
            else
                return "001";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool TimeIsValid(string time)
        {
            try
            {
                DateTime attTime = DateTime.Parse(time);
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw new Exception("Please enter time in correct format!");
                //lblOperation.Text = "Please enter time in correct format!";
            }
        }


        public enum OperationType
        {
            Save = 0,
            Update = 1
        }

        public static string GetCurrentDateString()
        {
            string dateTimePart = string.Format("{0}{1}{2}_{3}{4}", GetNumberCode(DateTime.Now.Day), GetNumberCode(DateTime.Now.Month),
                   GetNumberCode(DateTime.Now.Year), GetNumberCode(DateTime.Now.Hour), GetNumberCode(DateTime.Now.Minute));

            return dateTimePart;
        }

      

       
    }

}
