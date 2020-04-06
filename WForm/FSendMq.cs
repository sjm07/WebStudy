using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WForm
{
    public partial class FSendMq : Form
    {
        public FSendMq()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";//RabbitMQ服务在本地运行
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queueName = this.txtQueueName.Text;//队列名称
                    string message = this.txtMsg.Text; //传递的消息内容
                    /*队列持久化*/
                    bool durable = true;
                    channel.QueueDeclare(queueName, durable, false, false, null); //在MQ上定义一个持久化队列，如果名称相同不会重复创建，消息直接添加到现有队列中
                    for (int i = 1; i <= 10; i++)
                    {
                        /*消息持久化*/
                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        /*消息序列化二进制数组*/
                        var body = Encoding.UTF8.GetBytes(message + " " + i);
                        channel.BasicPublish("", queueName, properties, body); //开始传递
                    }
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FReceiveMq receiveMq = new FReceiveMq();
            receiveMq.Show();
        }
    }
}
