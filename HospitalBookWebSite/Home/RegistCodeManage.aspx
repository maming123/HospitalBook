<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistCodeManage.aspx.cs" Inherits="HospitalBookWebSite.Home.RegistCodeManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>注册码管理</title>
    <link href="../css/admin.global.css" rel="stylesheet" type="text/css" />
    <link href="../css/admin.content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.9.1.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    选择书籍：<asp:DropDownList ID="ddlBook" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBook_SelectedIndexChanged">
                    </asp:DropDownList>
        已有号码数量：<asp:Label ID="lblHaveCodeNum" runat="server" Font-Bold="True"></asp:Label>个，还要生成的号码数量:<asp:TextBox ID="txtCodeNum" runat="server"></asp:TextBox><asp:Button ID="btnGenerate" runat="server" Text="生成" OnClick="btnGenerate_Click" />
    </div>
    查询未使用的100个序列号，按指定序列号查询是否被使用
    </form>
</body>
</html>
