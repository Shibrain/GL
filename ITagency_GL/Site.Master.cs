using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         // if (Session["UserId"] == null)
             // Response.Redirect("~/Security/Login.aspx");
        //  lblUserName.Text = Session["UserName"].ToString();
            lblloginTime.Text = System.DateTime.Now.ToString();
            Session["CompId"] = 1;
            Session["UserId"] = 1;
        }



        protected void lblLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/security/logout.aspx");
        }
    }
}
