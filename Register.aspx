<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebT.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
        }
        .container {
            width: 300px;
            margin: 100px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        input[type="text"], input[type="password"], input[type="email"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 20px;
            border: 1px solid #ccc;
            border-radius: 3px;
            box-sizing: border-box;
        }
        input[type="submit"] {
            width: 100%;
            padding: 10px;
            background-color: #4CAF50;
            color: #fff;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            transition: background-color 0.3s;
        }
        input[type="submit"]:hover {
            background-color: #45a049;
        }
        .error {
            color: #f00;
            margin-bottom: 20px;
        }
    </style>
        <script type="text/javascript">
            function alertAndRedirect(message, url) {
                alert(message);
                window.location.href = url;
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="container">
            <h2>Register</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="error" Visible="false"></asp:Label>
            <asp:TextBox ID="txtUserID" runat="server" placeholder="账号" required></asp:TextBox>
            <asp:TextBox ID="txtUserName" runat="server" placeholder="姓名" required></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="密码" required></asp:TextBox>
            <asp:TextBox ID="txtEmail" runat="server" placeholder="邮箱" type="email" required ></asp:TextBox>
            <asp:TextBox ID="txtPhoneNumber" runat="server" placeholder="手机号码" required></asp:TextBox>
            <asp:Button ID="btnRegister" runat="server" Text="OK" OnClick="btnRegister_Click" />
        </div>
    </form>

</body>
</html>
