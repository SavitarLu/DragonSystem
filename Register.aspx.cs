using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using WebT.Common;

namespace WebT
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [Obsolete]
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Transaction tx;
            TResult rst;
            MQHelper mQHelper = new MQHelper();
            var Data = new
            {
                UserID = txtUserID.Text,
                Password = txtPassword.Text,
                Email = txtEmail.Text,
                PhoneNo = txtPhoneNumber.Text,
                UserName = txtUserName.Text
            };
            
            tx = new Transaction(CommonDef.TX_REGISTER, CommonDef.TX_SEND_TYPE);
            rst = mQHelper.SendAndReceiveMessage(JsonConvert.SerializeObject(tx), JsonConvert.SerializeObject(Data));
            if (rst.Result.Equals("SUCCESS"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RegistrationSuccess", "alertAndRedirect('注册成功！', 'Login.aspx');", true);
                //Response.Redirect("Login.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RegistrationFailed", $"alert('注册失败：{rst.Message}');", true);
            }
        }
    }
}
