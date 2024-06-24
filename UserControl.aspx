<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserControl.aspx.cs" Inherits="WebT.UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>用户管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="text-center">用户管理</h2>
        <div class="row mt-3">
            <div class="col-md-6">
                <asp:TextBox ID="txtSearchName" runat="server" CssClass="form-control" placeholder="姓名"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary ml-2" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btnAddUser" runat="server" CssClass="btn btn-success ml-2" Text="新增用户" OnClick="btnAddUser_Click" />
            </div>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mt-3" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="账号" ReadOnly="True" />
                <asp:TemplateField HeaderText="姓名">
                    <ItemTemplate>
                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="邮箱">
                    <ItemTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("Email") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="手机">
                    <ItemTemplate>
                        <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Eval("PhoneNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPhoneNo" runat="server" Text='<%# Eval("PhoneNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户组">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGroupName" runat="server">
                            <asp:ListItem Text="普通员工" Value="普通员工"></asp:ListItem>
                            <asp:ListItem Text="管理员" Value="管理员"></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-warning btn-sm" Text="修改" CommandName="Edit" CommandArgument='<%# Eval("UserID") %>' />
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="删除" CommandName="Delete" CommandArgument='<%# Eval("UserID") %>' OnClientClick="return confirm('确定要删除吗?');" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success btn-sm" Text="更新" CommandName="Update" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary btn-sm" Text="取消" CommandName="Cancel" />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
