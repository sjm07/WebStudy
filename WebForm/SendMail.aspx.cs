using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class SendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Method"] != null)
                {
                    try
                    {
                        string method = Request.QueryString["Method"];
                        if (!string.IsNullOrEmpty(method))
                        {
                            MethodInfo methodInfo = this.GetType().GetMethod(method);
                            methodInfo.Invoke(this, null);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 客户端方式发送邮件
        /// </summary>
        public void sendMail()
        {
            string activecode = RandomCode(6);
            StringBuilder sb = new StringBuilder();
            sb.Append("请单击以下链接完成激活");
            sb.Append(string.Format(@"<a href='http://www.pddla.com/mobile/register.aspx?activecode=" +
                activecode + "&id=" + activecode + "'>激活</a>"));
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("sjm07@163.com");//发送方邮箱
            mailMessage.To.Add("37250515@qq.com");//接收方邮箱
            mailMessage.Subject = "注册验证码";
            mailMessage.Body = sb.ToString();
            mailMessage.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.163.com";
            client.Port = 25;
            NetworkCredential credetial = new NetworkCredential();
            credetial.UserName = ""; //发送方用户名
            credetial.Password = "";//发送方密码
            client.Credentials = credetial;
            client.Send(mailMessage);

            Response.Write("成功");
            Response.End();
        }

        /// <summary>
        /// 服务端控件发送邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string activecode = RandomCode(6);
            StringBuilder sb = new StringBuilder();
            sb.Append("请单击以下链接完成激活");
            sb.Append(string.Format(@"<a href='http://www.pddla.com/mobile/register.aspx?activecode=" +
                activecode + "&id=" + activecode + "'>激活</a>"));
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("sjm07@163.com");//发送方邮箱
            mailMessage.To.Add("37250515@qq.com");//接收方邮箱
            mailMessage.Subject = "注册验证码";
            mailMessage.Body = sb.ToString();
            mailMessage.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.163.com";
            client.Port = 25;
            NetworkCredential credetial = new NetworkCredential();
            credetial.UserName = ""; //发送方用户名
            credetial.Password = "";//发送方密码
            client.Credentials = credetial;
            client.Send(mailMessage);
        }

        /// <summary>
        /// 生成n位随机验证码
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public  string RandomCode(int n)
        {
            int number;
            char code;
            string StrCode = String.Empty;
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                number = random.Next();
                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));
                StrCode += code.ToString();
            }
            return StrCode;
        }
    } 
}