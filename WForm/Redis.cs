using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util.Redis;

namespace WForm
{
    public partial class Redis : Form
    {
        public Redis()
        {
            InitializeComponent();
            this.cbType.SelectedIndex = 0;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGet_Click(object sender, EventArgs e)
        {
            var redisClient = new RedisHelper(0);

            string redisKey = this.txtKey.Text;
            string key = this.txtFieldKey.Text;
            string expireTime = this.txtTime.Text;
            TimeSpan? expireTs = null;
            if (!string.IsNullOrEmpty(expireTime))
            {
                DateTime dtBegin = DateTime.Now;
                DateTime dtEnd = dtBegin.AddMinutes(double.Parse(expireTime));
                expireTs = dtEnd - dtBegin;
            }

            string type = this.cbType.SelectedItem.ToString();
            switch (type)
            {
                case "String":
                    this.richValue.Text = redisClient.StringGet(redisKey);
                    break;
                case "Hash":
                    this.richValue.Text = redisClient.HashGet(redisKey, key);
                    break;
                case "List":
                    IEnumerable<RedisValue> Ivalues = redisClient.ListRange(redisKey);
                    this.richValue.Text = string.Join(",", Ivalues.Select(x => x.ToString()));
                    break;
                case "ZSet":
                    IEnumerable<RedisValue> zsValues = redisClient.SortedSetRangeByRank(redisKey);
                    this.richValue.Text = string.Join(",", zsValues.Select(x => x.ToString()));
                    break;
                case "Set":
                    RedisValue[] sValues = redisClient.SetMembers(redisKey);
                    this.richValue.Text = string.Join(",", sValues.Select(x => x.ToString()));
                    break;
                default:
                    break;
            }

            MessageBox.Show("获取成功", "提示", MessageBoxButtons.OK);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            var redisClient = new RedisHelper(0);

            string redisKey = this.txtKey.Text;
            string key = this.txtFieldKey.Text;
            string value = this.txtValue.Text;
            string expireTime = this.txtTime.Text;
            TimeSpan? expireTs = null;
            if (!string.IsNullOrEmpty(expireTime))
            {
                DateTime dtBegin = DateTime.Now;
                DateTime dtEnd = dtBegin.AddMinutes(double.Parse(expireTime));
                expireTs = dtEnd - dtBegin;
            }

            string type = this.cbType.SelectedItem.ToString();
            switch (type)
            {
                case "String":
                    redisClient.StringSet(redisKey, value, expireTs);
                    break;
                case "Hash":
                    redisClient.HashSet(redisKey, key, value);
                    break;
                case "List":
                    redisClient.ListRightPush(redisKey, value);
                    break;
                case "ZSet":
                    redisClient.SortedSetAdd(redisKey, key, double.Parse(value));
                    break;
                case "Set":
                    redisClient.SetAdd(redisKey, value);
                    break;
                default:
                    break;
            }

            MessageBox.Show("设置成功", "提示", MessageBoxButtons.OK);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            var redisClient = new RedisHelper(0);
            redisClient.KeyDelete(this.txtKey.Text);
            MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK);
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.txtValue.Text = string.Empty;
            this.txtTime.Text = string.Empty;
            this.richValue.Text = string.Empty;
        }
    }
}
