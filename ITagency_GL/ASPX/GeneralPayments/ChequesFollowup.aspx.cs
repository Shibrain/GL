using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITagency_GL.ASPX.GeneralPayments
{
    public partial class ChequesFollowp1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlChequeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChequeType.SelectedValue == "1")
            {

                ddlStatus.Items.Clear();
                ddlStatus.Items.Add(new ListItem("Select", "0"));
                ddlStatus.Items.Add(new ListItem("تحت التحصيل", "1"));
                ddlStatus.Items.Add(new ListItem("تم التحصيل", "2"));

            }
            if (ddlChequeType.SelectedValue == "2")
            {

                ddlStatus.Items.Clear();
                ddlStatus.Items.Add(new ListItem("Select", "0"));
                ddlStatus.Items.Add(new ListItem("تم التحصيل", "2"));
                ddlStatus.Items.Add(new ListItem("راجع", "3"));
                ddlStatus.Items.Add(new ListItem("آجل", "4"));


            }
        }

        protected void txtCRNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}