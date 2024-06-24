using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using WebT.Common;

namespace WebT
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Obsolete]
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            TResult rst ;
            Transaction tx;
            TUser user;
            TLoginHis loginHis;
            MQHelper mQHelper = new MQHelper();

            string username = txtUsername.Text;
            string password = txtPassword.Text;
            user = new TUser(username, password);
            tx = new Transaction(CommonDef.TX_VALIDATE_USER,CommonDef.TX_SEND_TYPE);
            rst = mQHelper.SendAndReceiveMessage(JsonConvert.SerializeObject(tx), JsonConvert.SerializeObject(user));
            
            if (rst.Result.Equals("SUCCESS"))
            {
                // 登录成功，跳转到其他页面
                Session["Username"] = username;
                loginHis = new TLoginHis(username, GetIPAddress());
                tx = new Transaction(CommonDef.TX_INSERT_LOGINHIS, CommonDef.TX_SEND_TYPE);
                rst = mQHelper.SendAndReceiveMessage(JsonConvert.SerializeObject(tx),JsonConvert.SerializeObject(loginHis));
                if (rst.Result.Equals("SUCCESS"))
                {
                     Response.Redirect("Welcome.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('登录失败: " + rst.Message + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('登录失败: " + rst.Message + "');", true);
            }
        }


        private string GetIPAddress()
        {
            string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Current.Request.UserHostAddress;
            }
            return ipAddress;
        }
    }
}
