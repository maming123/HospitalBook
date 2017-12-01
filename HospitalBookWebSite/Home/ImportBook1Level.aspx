<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportBook1Level.aspx.cs" Inherits="HospitalBookWebSite.Home.ImportBook1Level" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/sysmodule.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td>第一级：<asp:TextBox ID="txtLevelOne" CssClass="BigInput" runat="server" Width="50px">部分</asp:TextBox>
                    <br />
                    <br />
                    <div>1、请将原始数据粘贴至下方：</div><br />
                    <asp:TextBox ID="txtBook" CssClass="BigTextArea" runat="server" Height="250px" TextMode="MultiLine" Width="100%">第一部分 基础医学综合
     
         
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n
         
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n       
第二部分 基础医学综合
    
         
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n
         
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n       </asp:TextBox></td>
            </tr>
            <tr>
                <td align="center">
                    
                    <asp:Button ID="btnImport0" CssClass="BigButton" runat="server" Height="26px" OnClick="btnImport0_Click" Text="处理第一步" Width="351px" />
                    
                </td>

            </tr>
            <tr>
                <td>
                     <div>2、解析格式如下：</div><br />
                    <br />
                    <asp:TextBox ID="txtBookxml" runat="server" Height="250px" TextMode="MultiLine" Width="100%" CssClass="BigTextArea"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnTransfor2" runat="server" Height="26px" Text="处理第二步" Width="351px"  CssClass="BigButton" OnClick="btnTransfor2_Click" />
                </td>
                
            </tr>
            <tr>
                <td>
                    <div>
                        3、转换为XML格式如下： </div>
                    <br />
                    <asp:TextBox ID="txtBookXML2" runat="server" Height="250px" TextMode="MultiLine" Width="100%" CssClass="BigTextArea"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td align="center">请选择需导入的书籍：<asp:DropDownList ID="ddlBook" runat="server">
                </asp:DropDownList>



                    <asp:Label ID="Label1" runat="server" ForeColor="#FF3300"></asp:Label>



                    <asp:Button ID="btnCheck" runat="server" OnClick="btnCheck_Click" Height="26px" Text="检测" Width="162px"  CssClass="BigButton" />
                </td>
                
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnImportDB" runat="server"  OnClick="btnImportDB_Click"  Height="26px" Text="导入数据库" Width="351px"  CssClass="BigButton"  Enabled="False" />
                </td>
                
            </tr>
            <tr>
                <td>
                    4、导入结果显示<br />
                    <asp:TextBox ID="txtResult" runat="server" Height="77px" TextMode="MultiLine" Width="100%" Style="margin-bottom: 0px"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td align="center">
                    请选择需导出的书籍：<asp:DropDownList ID="ddlBookExport" runat="server">
                </asp:DropDownList>



                    <asp:Button ID="btnExportXMLFromDB" runat="server"  OnClick="btnExportXMLFromDB_Click"  Height="26px" Text="导出书籍" Width="351px"  CssClass="BigButton" />
                </td>
               
            </tr>
        </table>
       
    </form>
</body>
</html>
