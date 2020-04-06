<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxEncrypt.aspx.cs" Inherits="WebForm.AjaxEncrypt" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta charset="utf-8" />
    <script src="js/jquery-1.11.0.min.js"></script>
    <script src="js/JSEncry.js"></script>
    <script>
        $(function () {

            $('#btnOK').click(function () {
                console.log("开始发送数据请求");
                var data = {};
                data.username = $('#name').val();
                data.passwd = $('#password').val();

                console.log("加密前数据:", JSON.stringify(data));
                var result = $.encryptRequest({
                    data: data
                });
                console.log("加密后数据:", JSON.stringify(result));
                

                $.ajax({
                    url: 'AjaxEncrypt.aspx?Method=Decrypt',
                    dataType: 'jsonp',
                    type: 'post',
                    data: result,
                    success: function (res) {
                        console.log("服务端解密成功:", JSON.stringify(res));

                    }
                });
            });
        });
    </script>
</head>
<body>
    <input id="name"  />
    <input id="password" type="password" />
    <input id="btnOK" type="submit" value="加密传输"/>
</body>
</html>
