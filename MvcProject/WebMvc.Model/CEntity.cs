using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMvc.Model
{
    /// <summary>
    /// 定义其它实体类的基类，存储共性属性
    /// </summary>
    public class CEntity
    {
        public CEntity()
        {
            
        }

        #region Model

        private Guid _id = Guid.NewGuid();
        private DateTime? _createtime = DateTime.Now;

        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

        #endregion
    }
}
