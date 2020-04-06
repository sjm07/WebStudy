using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebMvc.Model.DB;
using WebMvc.Model.Dto;
using System.Data.Entity.Infrastructure;

namespace WebMvc.IDAL.System
{
    public interface IEmployeeDAL : IBaseDAL<CEmployee>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool TempSave(CEmployee employee);

        CEmployee GetEmployeeByID(string id);

        DbRawSqlQuery<EmployeeDto> GetEmployeeBySQL();
    }
}
