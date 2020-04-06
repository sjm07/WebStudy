using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMvc.Model
{
    /// <summary>
    /// 定义返回前端表格结果
    /// </summary>
    public class ResultDto
    {
        #region Property

        public object data { get; set; }
        
        /// <summary>
        /// 分页
        /// </summary>
        public DataTable dataTable { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; } = string.Empty;

        /// <summary>
        /// 返回标识位 
        /// E0000=成功
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 返回页数
        /// </summary>
        public int total { get; set; } = 0;

        #endregion

        private static ResultDto _instance = null;
        private static readonly object instanceLock = new object();

        public ResultDto()
        {
        }

        public static ResultDto Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ResultDto();
                        }

                    }
                }

                return _instance;
            }
        }
    }
}
