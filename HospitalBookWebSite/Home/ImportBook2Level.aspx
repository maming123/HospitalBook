<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportBook2Level.aspx.cs" Inherits="HospitalBookWebSite.Home.ImportBook2Level" ValidateRequest="false" %>

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
                    第一级：<asp:TextBox ID="txtLevelOne" runat="server" Width="50px">部分</asp:TextBox>
                    第二级：<asp:TextBox ID="txtLevelTwo" runat="server" Width="50px">篇</asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtBook" runat="server" Height="271px" TextMode="MultiLine" Width="412px">第一部分 基础医学综合
     第一篇　生 物 化 学
          
              采分点1：蛋白质的基本结构单位是氨基酸。
              采分点n
             
     第二篇　生 物 化 学
         
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n
        
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n       
     第三篇　生 物 化 学2
         
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n
         
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n       
第二部分 基础医学综合
     第一篇　生 物 化 学
          
              采分点1：蛋白质的基本结构单位是氨基酸。
              采分点n
          
             采分点1：蛋白质的基本结构单位是氨基酸。
             采分点n    
     第二篇　生 物 化 学
         
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n
         
         采分点1：蛋白质的基本结构单位是氨基酸。
         采分点n       </asp:TextBox></td>
                <td>&nbsp;<asp:Button ID="btnImport0" runat="server" Height="145px" OnClick="btnImport0_Click" Text="1、转换--&gt;" Width="86px" />
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="txtBookxml" runat="server" Height="265px" TextMode="MultiLine" Width="412px"></asp:TextBox></td>
            </tr>
            <tr><td></td><td></td><td><asp:Button ID="btnTransfor2" runat="server" Height="44px"  Text="2、转换--&gt;" Width="353px" OnClick="btnTransfor2_Click" />
                    </td></tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtResult" runat="server" Height="334px" TextMode="MultiLine" Width="412px" style="margin-bottom: 0px"></asp:TextBox></td>
                <td>



                    选择书籍：<asp:DropDownList ID="ddlBook" runat="server">
                    </asp:DropDownList>



            <asp:Button ID="btnCheck" runat="server" OnClick="btnCheck_Click" Text="3、检查XML" Height="48px" Width="95px" />
                    <br />
                    <asp:Label ID="Label1" runat="server" ForeColor="#FF3300"></asp:Label>
                    <br />
                    <asp:Button ID="btnImportDB" runat="server" Height="119px" OnClick="btnImportDB_Click" Text="&lt;-4、导入DB" Width="98px" Enabled="False" />
                    &nbsp;<asp:Button ID="btnExportXMLFromDB" runat="server" Height="88px" OnClick="btnExportXMLFromDB_Click" Text="5、导出书籍-&gt;" Width="101px"/>
                    </td>
                <td>
                    <asp:TextBox ID="txtBookXML2" runat="server" Height="314px" TextMode="MultiLine" Width="412px"></asp:TextBox></td>
            </tr>
        </table>
        <div>

            <br />



        </div>
    </form>
</body>
</html>
