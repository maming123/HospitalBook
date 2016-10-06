<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HospitalBook.WebSite.Home.Index" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>后台 - 首页</title>
    <link href="/js/easyui-1.4.1/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="/js/easyui-1.4.1/themes/icon.css" rel="stylesheet" />
    <style>
            .tempul {
                list-style: none;
                margin: 0px;
            }

                .tempul li {
                    line-height: 32px;
                }

                    .tempul li a {
                        text-decoration: none;
                    }
        </style>
    <script src="/js/jquery-1.9.1.min.js"></script>
    <script src="/js/easyui-1.4.1/jquery.easyui.min.js"></script>
    <script src="/js/easyui-1.4.1/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        function addTab(subtitle, url) {
            if (!$('#tabs').tabs('exists', subtitle)) {
                $('#tabs').tabs('add', {
                    title: subtitle,
                    content: createFrame(url),
                    closable: true
                });
            } else {
                $('#tabs').tabs('select', subtitle);
                var tab = $('#tabs').tabs('getSelected');
                $('#tabs').tabs('update', { tab: tab, options: { content: createFrame(url) } });
            }
        }

        function createFrame(url) {
            var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
            return s;
        }
    </script>
</head>
<body class="easyui-layout">
    <div data-options="region:'north',border:false" style="background: #B3DFDA; padding: 10px">
        后台管理系统&nbsp;&nbsp;&nbsp;<%=LoginUserName %>
        
        <a href="/FetionActivity/LoginOut.aspx?U=/default.aspx">退出登录</a>
    </div>
    <div data-options="region:'west',split:false,title:'导航菜单'" style="width: 260px; padding: 10px;">

        <div id="accordion" class="easyui-accordion" style="width: 260px; height: auto;">

                <div title="配置管理" data-options="collapsed:false,collapsible:false" style="padding: 10px;">
                    <ul class="tempul">
                        <li><a href="javascript:void(0)" onclick="addTab('内容管理','Role/InfoManage.aspx')">内容</a></li>
                        <li><a href="javascript:void(0)" onclick="addTab('配置管理','Role/SettingManage.aspx')">配置</a></li>
                        <li><a href="javascript:void(0)" onclick="addTab('windows服务配置管理','Role/WSSettingManage.aspx?id=1')">windows服务配置管理</a></li>
                        <li><a href="javascript:void(0)" onclick="addTab('批量删除服务配置管理','Role/WSSettingManage.aspx?id=2')">批量删除服务配置管理</a></li>
                        <li><a href="javascript:void(0)" onclick="addTab('异步群发服务配置管理','Role/WSSettingManage.aspx?id=3')">异步群发服务配置管理</a></li>
                    </ul>
                </div>
                <div title="配置管理" data-options="collapsed:false,collapsible:false" style="padding: 10px;">
                    <ul class="tempul">
                        <li><a href="javascript:void(0)" onclick="addTab('内容管理','Role/InfoManage.aspx')">内容</a></li>
                        <li><a href="javascript:void(0)" onclick="addTab('配置管理','Role/SettingManage.aspx')">配置</a></li>
                        <li><a href="javascript:void(0)" onclick="addTab('windows服务配置管理','Role/WSSettingManage.aspx?id=1')">windows服务配置管理</a></li>
                        <li><a href="javascript:void(0)" onclick="addTab('批量删除服务配置管理','Role/WSSettingManage.aspx?id=2')">批量删除服务配置管理</a></li>
                        <li><a href="javascript:void(0)" onclick="addTab('异步群发服务配置管理','Role/WSSettingManage.aspx?id=3')">异步群发服务配置管理</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div data-options="region:'center',title:''">
            <div id="tabs" class="easyui-tabs" fit="true" border="false">
            </div>

        </div>
        

        
</body>
</html>
