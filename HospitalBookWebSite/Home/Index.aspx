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
        .nav {
	padding-left:0;
	margin-bottom:0;
	list-style:none;
}
.nav:before, .nav:after {
	display:table;
	content:" ";
}
.nav:after {
	clear:both;
}
.nav:before, .nav:after {
	display:table;
	content:" ";
}
.nav:after {
	clear:both;
}
.nav>li {
	position:relative;
	display:block;
}
.nav>li>a {
	position:relative;
	display:block;
	padding:10px 15px;
}
.nav>li>a:hover, .nav>li>a:focus {
	text-decoration:none;
	background-color:#eeeeee;
}
.nav>li.disabled>a {
	color:#999999;
}
.nav>li.disabled>a:hover, .nav>li.disabled>a:focus {
	color:#999999;
	text-decoration:none;
	cursor:not-allowed;
	background-color:transparent;
}
.nav .open>a, .nav .open>a:hover, .nav .open>a:focus {
	background-color:#eeeeee;
	border-color:#428bca;
}
.nav .open>a .caret, .nav .open>a:hover .caret, .nav .open>a:focus .caret {
	border-top-color:#2a6496;
	border-bottom-color:#2a6496;
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
            var s = '<iframe scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
            return s;
        }
    </script>
</head>
<body class="easyui-layout">
    <div data-options="region:'north',border:false" style="background: #005273; height: 105px;">
        <%=LoginUserName %>
        <image src="login/images/logo.png"></image>
        <a style="color: #FFFFFF; float:right;margin:85px 0px 0px 0px;" href="/home/login/LoginOut.aspx?U=/home/login/index.aspx">退出登录</a>
    </div>
    <div data-options="region:'west',split:false,title:'导航菜单'" style="width: 260px; padding: 10px;">

        <div id="accordion" class="easyui-accordion" style="width: 260px; height: auto;">

            <div title="功能管理" data-options="collapsed:false,collapsible:false" style="padding: 10px;">
                <ul class="tempul nav">

                    <li><a href="javascript:void(0)" onclick="addTab('书籍管理','SystemModule/Sys/Modules/modulelist.aspx')"><span style="color:#977171">>&nbsp;&nbsp;书籍管理</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('导入导出管理[一级目录]','ImportBook1Level.aspx')"><span style="color:#977171">>&nbsp;&nbsp;导入导出管理[一级目录]</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('导入导出管理[二级目录]','ImportBook2Level.aspx')"><span style="color:#977171">>&nbsp;&nbsp;导入导出管理[二级目录]</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('导入导出管理[三级目录]','ImportBook3Level.aspx')"><span style="color:#977171">>&nbsp;&nbsp;导入导出管理[三级目录]</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('导入导出管理[四级目录]','ImportBook4Level.aspx')"><span style="color:#977171">>&nbsp;&nbsp;导入导出管理[四级目录]</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('导入导出管理[五级目录]','ImportBook5Level.aspx')"><span style="color:#977171">>&nbsp;&nbsp;导入导出管理[五级目录]</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('注册用户管理','UserManage.aspx')"><span style="color:#977171">>&nbsp;&nbsp;注册用户管理</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('注册用户日志管理','UserRegistLogManage.aspx')"><span style="color:#977171">>&nbsp;&nbsp;注册用户日志管理</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('书籍注册码管理','RegistCodeManage.aspx')"><span style="color:#977171">>&nbsp;&nbsp;书籍注册码管理</span></a></li>

                </ul>
            </div>
            <div title="系统管理" data-options="collapsed:false,collapsible:false" style="padding: 10px;">
                <ul class="tempul">
                    <li><a href="javascript:void(0)" onclick="addTab('密码修改','Login/EditPassword.aspx')"><span style="color:#977171">>&nbsp;&nbsp;密码修改</span></a></li>
                </ul>
            </div>
        </div>
    </div>
    <div data-options="region:'center',title:''">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
        </div>

    </div>
    <div data-options="region:'south',title:''" style="background-color: #424242">

        <div style="color: #FFFFFF; text-align: center; vertical-align: bottom">地址：北京市东城区东单三条9号 网址：<a style="color: #FFFFFF" href="http://www.pumcp.com" target="_blank">http://www.pumcp.com</a>  互联网出版许可证 新出网证 （京）字282号 京ICP备 05029104号-4<br>Copyright2013 中国协和医科大学出版社 版权所有！</div>

    </div>


</body>
</html>
