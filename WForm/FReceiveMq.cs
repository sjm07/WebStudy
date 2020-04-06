using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WForm
{
    public partial class FReceiveMq : Form
    {
        public FReceiveMq()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 接收队列内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceiveMq_Click(object sender, EventArgs e)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";

            string msg = string.Empty;
            this.txtReceiveMsg.Text = string.Empty;
            string queueName = this.txtQueueName.Text;

            #region QueueingBasicConsumer模式（）

            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        //在MQ上定义一个持久化队列，如果名称相同不会重复创建
            //        bool durable = true;
            //        channel.QueueDeclare(queueName, durable, false, false, null);
            //        //输入1，那如果接收一个消息，如果没有应答，则客户端不会收到下一个消息
            //        channel.BasicQos(0, 1, false);
            //        var consumer = new QueueingBasicConsumer(channel);
            //        channel.BasicConsume(queueName, false, consumer);
            //        while (true)
            //        {
            //            var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);
            //            msg = msg + " " + message;
            //            Console.WriteLine("Received {0}", message);
            //            Console.WriteLine("Done");
            //            //回复确认
            //            channel.BasicAck(ea.DeliveryTag, false);
            //            this.txtReceiveMsg.Text = msg;
            //        }
            //    }
            //}

            #endregion

            #region EventingBasicConsumer模式（生产者产生后，消费者自动触发）

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                    bool durable = true;
                    channel.QueueDeclare(queueName, durable, false, false, null);
                    //输入1，那如果接收一个消息，如果没有应答，则客户端不会收到下一个消息
                    channel.BasicQos(0, 1, false);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queueName, false, consumer);
                    //接收到消息时触发的事件
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        msg = msg + " " + message;
                        //手工处理，回复确认
                        channel.BasicAck(ea.DeliveryTag, false);
                        Console.WriteLine("Received {0}", message);
                        Console.WriteLine("Done");
                    };

                    Console.WriteLine("消费者准备就绪....");
                    //处理消息
                    channel.BasicConsume(queueName, false, consumer);
                }
            }

            #endregion
        }
    }
}
