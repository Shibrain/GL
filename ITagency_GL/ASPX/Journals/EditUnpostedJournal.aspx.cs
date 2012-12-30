using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared;
namespace ITagency_GL.ASPX.Journals
{
    public partial class EditUnpostedJournal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsData_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                lblFeedback.Text = e.Exception.Message;
                e.ExceptionHandled = true;
            }
            else
                lblFeedback.Text = Feedback.DeleteSuccessfull();
        }

        protected void odsData_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                lblFeedback.Text = e.Exception.Message;
                e.ExceptionHandled = true;
            }
            else
                lblFeedback.Text = Feedback.UpdateSuccessfull();
        }
    }
}