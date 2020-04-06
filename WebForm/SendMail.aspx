<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="WebForm.SendMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="js/jquery-1.11.0.min.js"></script>
    <script>
        function sendMail()
        {
            $.ajax({
                url: 'SendMail.aspx?Method=sendMail',
                dataType: "text",
                type: "POST",
                success: function (result) {
                    alert(result);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnSend" runat="server" Text="发送邮件" OnClick="btnSend_Click" />
        </div>
    </form>
    <input id="btnSumit" type="button" value="客户端发送邮件" onclick="sendMail()" />
</body>
</html>
