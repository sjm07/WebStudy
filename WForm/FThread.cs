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
    public partial class FThread : Form
    {
        public FThread()
        {
            InitializeComponent();

            Thread td_1 = new Thread(new ThreadStart(ThreadM_1));
            td_1.Start();

            //Thread td_2 = new Thread(new ParameterizedThreadStart(ThreadM_2));
            //td_2.Start("有参线程启动");
        }

        #region Method

        private void ThreadM_1()
        {
            this.txtNoPara.Text = string.Format(@"无参线程启动，时间：{0}", DateTime.Now);
            MessageBox.Show("无参线程启动:" + DateTime.Now);
            this.txtNoPara.Text = "1";
        }

        private void ThreadM_2(object msg)
        {
            //MessageBox.Show(msg.ToString() + ":" + DateTime.Now);
            this.txtHavePara.Text = string.Format(@"{0}，时间：{1}", msg.ToString(), DateTime.Now);
        }

        #endregion
    }
}
