using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using DAL;

namespace Shared
{
    /// <summary>
    /// Summary description for Feedback
    /// </summary>
    public class Feedback
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string InsertSuccessfull()
        {
            return "One Record Added Successfully!";
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string InsertException()
        {
            return "Error: Record Has not Been Added!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string InsertExceptionUnique()
        {
            return "Error: Record Has not Been Added, Because There Are Similar Data Already Exists!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string UpdateSuccessfull()
        {
            return "One Record Updated Successfully!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string UpdateException()
        {
            return "Error: Record Has not Been Updated!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string UpdateExceptionUnique()
        {
            return "Error: Record Has not Been Updated, Similar Data Already Exists!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string DeleteSuccessfull()
        {
            return "One Record Deleted Successfully!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string DeleteException()
        {
            return "Error: Record Has not Been Deleted!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="relatedData"></param>
        /// <returns></returns>
        public static string DeleteException(string relatedData)
        {
            return "Error: Record Has not Been Deleted, Some Related Data Should be Deleted First(" + relatedData + ")!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string StatusUpdateSuccessfull()
        {
            return "Your Status Updated Sucessfully!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string NoData()
        {
            return "There's no data to show!";
        }

        public static string OperationFailed()
        {
            return "Error: Operation failed!";
        }

        public static string OperationSuccessfully()
        {
            return "Operation done successfully";
        }

        public static string NoImageForStudent()
        {
            return "Cannot print without student photo";
        }

        public static string StudentHasCardNotPrinted()
        {
            return "This student has card not printed yet!";
        }

        public static string TimeTableSelectDate()
        {
            return "Please select date & time!";
        }

        public static string TimeTableError()
        {
            return "Batch/Group/Subgroup already have a lecture at this time!";
        }
        

    }
}