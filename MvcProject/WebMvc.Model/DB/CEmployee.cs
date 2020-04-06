using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMvc.Model.DB
{
    [Table("Employee")]
    public class CEmployee : CEntity
    {
        public CEmployee()
        {

        }
        
        #region Model

        private string _name;
        private string _age;

        private string _c = "123";

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



        public string c
        {
            set { _c = value; }
            get { return _c; }
        }

        #endregion Model
    }
}
