using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebT.Common;

namespace WebT
{
    public partial class UserControl : Page
    {
        TResult rst;
        Transaction tx;
        MQHelper mQHelper = new MQHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    ((Literal)Master.FindControl("UserNameLiteral")).Text = Session["Username"].ToString();
                }

                //RefreshData();
            }
        }

        [Obsolete]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearchName.Text.Trim();
            tx = new Transaction(CommonDef.TX_REQ_USER, CommonDef.TX_SEND_TYPE);
            TReqUser reqUser = new TReqUser(name);
            rst = mQHelper.SendAndReceiveMessage(JsonConvert.SerializeObject(tx), JsonConvert.SerializeObject(reqUser));
            if (rst.Result.Equals("SUCCESS"))
            {
                string jsonData = JsonConvert.SerializeObject(rst.Data);
                List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonData);
                GridView1.DataSource = users;
                GridView1.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('查询失败: " + rst.Message + "');", true);
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string account = GridView1.Rows[e.RowIndex].Cells[0].Text;
            DeleteUser(account);
            RefreshData();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            Control parent = GridView1.Rows[e.NewEditIndex];
            DropDownList ddlGroupName = (DropDownList)parent.FindControl("ddlGroupName") as DropDownList;

            if (ddlGroupName != null)
            {
                ddlGroupName.SelectedIndex = 1; // Set selected index or value
            }

            RefreshData();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            RefreshData();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string account = row.Cells[0].Text;
            string name = ((TextBox)row.FindControl("txtUserName")).Text;
            string email = ((TextBox)row.FindControl("txtEmail")).Text;
            string phoneNo = ((TextBox)row.FindControl("txtPhoneNo")).Text;
            DropDownList ddlGroupName = (DropDownList)row.FindControl("ddlGroupName");
            string groupName = ddlGroupName.SelectedValue;

            UpdateUser(account, name, email, phoneNo, groupName);

            GridView1.EditIndex = -1;
            RefreshData();
        }

        [Obsolete]
        private void UpdateUser(string account, string name, string email, string phoneNo, string groupName)
        {
           tx = new Transaction(CommonDef.TX_UPDATEUSER, CommonDef.TX_SEND_TYPE);
           var Data = new
           {
               UserID = account,
               UserName = name,
               Email = email,
               PhoneNo = phoneNo,
               GroupName = groupName
           };
           rst = mQHelper.SendAndReceiveMessage(JsonConvert.SerializeObject(tx), JsonConvert.SerializeObject(Data));
           if (rst.Result.Equals("SUCCESS"))
           {
               ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('更新成功！');", true);
           }
           else
           {
               ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('更新失败: " + rst.Message + "');", true);
           }
        }

        [Obsolete]
        private void DeleteUser(string account)
        {
            tx = new Transaction(CommonDef.TX_DELETEUSER, CommonDef.TX_SEND_TYPE);
            var Data = new
            {
                UserID = account
            };
            rst = mQHelper.SendAndReceiveMessage(JsonConvert.SerializeObject(tx), JsonConvert.SerializeObject(Data));
            if (rst.Result.Equals("SUCCESS"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('删除成功！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('删除失败: " + rst.Message + "');", true);
            }
        }

        [Obsolete]
        private void RefreshData()
        {
            string name = string.Empty;
            tx = new Transaction(CommonDef.TX_REQ_USER, CommonDef.TX_SEND_TYPE);
            TReqUser reqUser = new TReqUser(name);
            rst = mQHelper.SendAndReceiveMessage(JsonConvert.SerializeObject(tx), JsonConvert.SerializeObject(reqUser));
            if (rst.Result.Equals("SUCCESS"))
            {
                string jsonData = JsonConvert.SerializeObject(rst.Data);
                List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonData);
                GridView1.DataSource = users;
                GridView1.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('查询失败: " + rst.Message + "');", true);
            }
        }
        private Control FindControlRecursive(Control parent, string controlId)
        {
            if (parent == null) return null;

            Control control = parent.FindControl(controlId);
            if (control == null)
            {
                foreach (Control child in parent.Controls)
                {
                    control = FindControlRecursive(child, controlId);
                    if (control != null) break;
                }
            }
            return control;
        }
    }
}
