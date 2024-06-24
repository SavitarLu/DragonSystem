<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebT.Login" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Login</title>
    <!-- 引入Bootstrap CSS -->
    <link href="~/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background-image: url('images/background.jpg');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
        }
        .background-container {
            width: 100%;
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: rgba(233, 236, 239, 0.8); /* 半透明背景色 */
        }
        .content-container {
            width: 300px;
            padding: 20px;
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
    </style>
</head>
<body>
    <div class="background-container">
        <div class="content-container">
            <form id="form1" runat="server">
                <h2 class="text-center">欢迎</h2>
                <asp:Label ID="lblMessage" runat="server" CssClass="error" Visible="false"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="输入账号" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="输入密码" required></asp:TextBox>
                </div>
                <div class="form-group d-flex justify-content-between">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="登录" OnClick="btnLogin_Click" />
                    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-danger" Text="注册" OnClientClick="redirectToRegisterPage();" />
                </div>
            </form>
        </div>
    </div>

    <!-- 引入Bootstrap JS和依赖 -->
    <script src="~/js/jquery.min.js" defer></script>
    <script src="~/js/popper.min.js" defer></script>
    <script src="~/js/bootstrap.min.js" defer></script>
    <script>
        function redirectToRegisterPage() {
            window.location.href = 'Register.aspx'; // 替换为注册页面的实际路径
        }
    </script>
</body>
</html>
