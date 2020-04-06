using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebMvc.Model.DB;
using WebMvc.Model.Dto;

namespace WebMvc.IBLL.System
{
    public interface IEmployeeBLL : IBaseDLL<CEmployee>
    {
        #region Interface

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        CEmployee Insert(EmployeeDto employeeDto);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        int Update(EmployeeDto employeeDto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        int Delete(EmployeeDto employeeDto);

        List<EmployeeDto> GetEmployeeInfo(EmployeeDto employeeDto);

        bool TempSave(EmployeeDto employeeDto);

        //List<CEmployee> GetEmployeeInfo(Expression<Func<CEmployee, bool>> exp = null);

        //List<EmployeeDto> GetEmployeeInfo1(Expression<Func<CEmployee, bool>> exp = null);

        


        CEmployee GetEmployeeByID(string id);

        List<EmployeeDto> GetEmployeeBySQL();

        #endregion
    }
}
