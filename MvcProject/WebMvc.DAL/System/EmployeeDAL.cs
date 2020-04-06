using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebMvc.IDAL;
using WebMvc.IDAL.System;
using WebMvc.Model;
using WebMvc.Model.DB;
using WebMvc.Model.Dto;
using WebMvcDAL.System;

namespace WebMvcDAL.System
{
    public class EmployeeDAL : BaseDAL<CEmployee>, IEmployeeDAL
    {
        public bool TempSave(CEmployee employee)
        {
            Insert(employee);
            SaveChanges();
            return true;
        }

        /// <summary>
        /// 通过ID获得雇员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CEmployee GetEmployeeByID(string id)
        {
            return dbContext.Employees.Where(x => x.ID == new Guid(id)).FirstOrDefault();
        }

        #region 按SQL语句方式

        /// <summary>
        /// 获得雇员信息
        /// </summary>
        /// <returns></returns>
        public DbRawSqlQuery<EmployeeDto> GetEmployeeBySQL()
        {
            return dbContext.Database.SqlQuery<EmployeeDto>("select convert(varchar(100),id) as Id,Name,Age,CreateTime from employee");
        }

        #endregion
    }
}
