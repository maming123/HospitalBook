<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistCodeManage.aspx.cs" Inherits="HospitalBookWebSite.Home.RegistCodeManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>注册码管理</title>
    <link href="css/admin.global.css" rel="stylesheet" type="text/css" />
    <link href="css/admin.content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.9.1.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    选择书籍：<asp:DropDownList ID="ddlBook" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBook_SelectedIndexChanged">
                    </asp:DropDownList>
        已有号码数量：<asp:Label ID="lblHaveCodeNum" runat="server" Font-Bold="True"></asp:Label>个，还要生成的号码数量:<asp:TextBox ID="txtCodeNum" runat="server"></asp:TextBox><asp:Button ID="btnGenerate" runat="server" Text="生成" OnClick="btnGenerate_Click" />
    </div>
    <asp:Button ID="btnQuery" runat="server" Text="查询未使用的100个序列号" OnClick="btnQuery_Click" />，按指定序列号：<asp:TextBox ID="txtCode" runat="server"></asp:TextBox><asp:Button ID="btnQureyCodeStatus" runat="server" Text="查询是否被使用" OnClick="btnQureyCodeStatus_Click" />
        <div class="block">
            <div class="h">
                <span class="icon-sprite icon-list"></span>
                <h3>
                    内容列表</h3>
                <div class="bar">
                </div>
            </div>
            <div class="tl corner">
            </div>
            <div class="tr corner">
            </div>
            <div class="bl corner">
            </div>
            <div class="br corner">
            </div>
            <div class="cnt-wp">
                <div class="cnt">
                    <asp:GridView ID="gvList" CssClass="data-table" GridLines="None" CellSpacing="0"
                        CellPadding="0" runat="server" AutoGenerateColumns="False" OnRowCommand="gvList_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RegistCode" HeaderText="标识" HtmlEncode="true">
                                <HeaderStyle Width="100px" HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="是否已用">
                                <ItemTemplate>
                                    <%# Eval("IsEnable").ToString()=="0"?"已用":"未用" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <HeaderStyle Width="120" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CommandName="ContentUpdate" CommandArgument='<%#Eval("Id") %>'>状态更新成已用</asp:LinkButton>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
