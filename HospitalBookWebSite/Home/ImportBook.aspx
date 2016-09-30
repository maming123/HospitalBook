<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportBook.aspx.cs" Inherits="HospitalBookWebSite.Home.ImportBook" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtBook" runat="server" Height="402px" TextMode="MultiLine" Width="412px">第一部分 基础医学综合
     第一篇　生 物 化 学
          第一单元　蛋白质的结构与功能
              采分点1：蛋白质的基本结构单位是氨基酸。
              采分点n
          第二单元　蛋白质的结构与功能
             采分点1：蛋白质的基本结构单位是氨基酸。
             采分点n    
     第二篇　生 物 化 学
         第一单元　蛋白质的结构与功能
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n
         第二单元　蛋白质的结构与功能
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n       
     第三篇　生 物 化 学2
         第一单元　蛋白质的结构与功能
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n
         第二单元　蛋白质的结构与功能
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n       
第二部分 基础医学综合
     第一篇　生 物 化 学
          第一单元　蛋白质的结构与功能
              采分点1：蛋白质的基本结构单位是氨基酸。
              采分点n
          第二单元　蛋白质的结构与功能
             采分点1：蛋白质的基本结构单位是氨基酸。
             采分点n    
     第二篇　生 物 化 学
         第一单元　蛋白质的结构与功能
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n
         第二单元　蛋白质的结构与功能
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n       </asp:TextBox></td>
                <td>&nbsp;<asp:Button ID="btnImport0" runat="server" Height="145px" OnClick="btnImport0_Click" Text="转换1--&gt;" Width="76px" />
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="txtBookxml" runat="server" Height="402px" TextMode="MultiLine" Width="412px"></asp:TextBox></td>
            </tr>
            <tr><td></td><td></td><td><asp:Button ID="btnTransfor2" runat="server" Height="77px"  Text="转换2--&gt;" Width="353px" OnClick="btnTransfor2_Click" />
                    </td></tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtResult" runat="server" Height="402px" TextMode="MultiLine" Width="412px"></asp:TextBox></td>
                <td>



            <asp:Button ID="btnCheck" runat="server" OnClick="btnCheck_Click" Text="检查XML合法性" Height="48px" Width="95px" />
                    <br />
                    <asp:Label ID="Label1" runat="server" ForeColor="#FF3300"></asp:Label>
                    <br />
                    <asp:Button ID="btnImportDB" runat="server" Height="145px" OnClick="btnImportDB_Click" Text="&lt;-导入DB" Width="87px" Enabled="False" />
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="txtBookXML2" runat="server" Height="402px" TextMode="MultiLine" Width="412px"></asp:TextBox></td>
            </tr>
        </table>
        <div>

            <br />



        </div>
    </form>
</body>
</html>
