using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMvc.Model.Dto
{
    /// <summary>
    /// Employee 雇员实体类
    /// </summary>
    public class EmployeeDto
    {
        public EmployeeDto()
        { }

        #region Model

        private string _id;
        private string _name;
        private string _age;

        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string NAME
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AGE
        {
            set { _age = value; }
            get { return _age; }
        }
        
        #endregion Model
    }
}
