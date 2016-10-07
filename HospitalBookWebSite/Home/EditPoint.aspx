<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPoint.aspx.cs" Inherits="HospitalBookWebSite.Home.EditPoint"  ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" charset="utf-8" src="/js/kindeditor-4.1.10/kindeditor.js"></script>
    <script type="text/javascript" charset="utf-8" src="/js/kindeditor-4.1.10/lang/zh_CN.js"></script>
    <script type="text/javascript">
        var edit_id = "<%=this.txtContent.ClientID%>";
    </script>
    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('#' + edit_id, {
                uploadJson: '/js/kindeditor-4.1.8/asp.net/upload_json.ashx',
                fileManagerJson: '/js/kindeditor-4.1.8/asp.net/file_manager_json.ashx',
                allowFileManager: true,
                filterMode: false,/**不过滤任何标签***/
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblPointId" runat="server" Text=""></asp:Label><asp:Label ID="lblContent" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                <textarea id="txtContent" cols="100" rows="8" name="content" style="width: 800px; height: 372px;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="保存"
                    OnClick="btnSave_Click"  ></asp:Button>
                </td>
        </tr>
    </table>
        </form>

</body>
</html>
