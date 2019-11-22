<%--
  Created by IntelliJ IDEA.
  User: zhangjiakai
  Date: 2017/7/11
  Time: 21:28
  To change this template use File | Settings | File Templates.
--%>
<%@ page language="java" import="java.util.*" contentType="text/html; charset=GB2312"%>
<%
    String path = request.getContextPath();
    String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>
<html>
<head>
    <base href="<%=basePath%>">

    <title>My JSP 'index.jsp' starting page</title>
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    <meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
    <meta http-equiv="description" content="This is my page">
    <!--
        <link rel="stylesheet" type="text/css" href="styles.css">
        -->
</head>

<body>

<!-- 传递给Land.do做处理，传递方式是get -->
<form action="Land.do" method="get">
    <input name="UserName" type="text" value="" />
    <input name="PassWord" type="password" value="" />
    <!-- 这个是在网页浏览测试用的 -->
    <input name="Finish" type="submit" value="Finish" />
</form>
<!-- 传递给Register.do做处理，传递方式是get -->
<form action="Register.do" method="get">
    <input name="UserName" type="text" value="" />
    <input name="PassWord" type="password" value="" />
    <!-- 这个是在网页浏览测试用的 -->
    <input name="Finish" type="submit" value="Finish" />
</form>
<!-- 传递给SaveData.do做处理，传递方式是get -->
<form action="SaveData.do" method="get">
    <input name="UserName" type="text" value="" />
    <input name="Score" type="text" value="" />
    <input name="Money" type="text" value="" />
    <input name="Level" type="text" value="" />
    <input name="Castle" type="text" value="" />
    <input name="Defence" type="text" value="" />
    <input name="Propone" type="text" value="" />
    <input name="Proptwo" type="text" value="" />
    <input name="Propthree" type="text" value="" />
    <!-- 这个是在网页浏览测试用的 -->
    <input name="Finish" type="submit" value="Finish" />
</form>
<!-- 传递给SaveData.do做处理，传递方式是get -->
<form action="GetRank.do" method="get">
    <!-- 这个是在网页浏览测试用的 -->
    <input name="Finish" type="submit" value="Finish" />
</form>

<!-- 传递给SaveData.do做处理，传递方式是get -->
<form action="GetData.do" method="get">
    <input name="UserName" type="text" value="" />
    <!-- 这个是在网页浏览测试用的 -->
    <input name="Finish" type="submit" value="Finish" />
</form>

</body>
</html>
