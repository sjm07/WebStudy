using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebMvc.IBLL.System;
using WebMvc.IDAL.System;
using WebMvc.Model.AutoMapper;
using WebMvc.Model.DB;
using WebMvc.Model.Dto;

namespace WebMvc.BLL.System
{
    public class EmployeeBLL : BaseBLL<CEmployee>, IEmployeeBLL
    {
        public EmployeeBLL(IEmployeeDAL dalSer)
        {
            base.dal = dalSer;
            this.dalSer = dalSer;
        }

        #region Property

        IEmployeeDAL dalSer;

        #endregion

        #region Method

        /// <summary>
        /// 个性化新增
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        public CEmployee Insert(EmployeeDto employeeDto)
        {
            CEmployee employee = new CEmployee();
            employee.NAME = employeeDto.NAME;
            employee.AGE = employeeDto.AGE;
            return dalSer.Insert(employee);
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        public int Update(EmployeeDto employeeDto)
        {
            int result = 0;
            CEmployee employee = GetEmployeeByID(employeeDto.ID);
            if (employee != null)
            {
                employee.NAME = employeeDto.NAME;
                employee.AGE = employeeDto.AGE;
                result = dalSer.Update(employee);
            }

            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        public int Delete(EmployeeDto employeeDto)
        {
            int result = 0;
            CEmployee employee =  GetEmployeeByID(employeeDto.ID);
            if (employee != null)
            {
                dalSer.Delete(employee);
            }

            return result;
        }

        /// <summary>
        /// 通过ID获得雇员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CEmployee GetEmployeeByID(string id)
        {
            CEmployee employee = dalSer.GetEmployeeByID(id);
            return employee;
        }

        /// <summary>
        /// 获得雇员信息
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        public List<EmployeeDto> GetEmployeeInfo(EmployeeDto employeeDto)
        {
            IQueryable<CEmployee> qEmployees = dalSer.Find(HandleExpress(employeeDto));
            List<EmployeeDto> lstEmployee = AutoMapperHelper.MapToList<EmployeeDto>(qEmployees);
            return lstEmployee;
        }

        /// <summary>
        /// 通过SQL方式获得雇员信息
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDto> GetEmployeeBySQL()
        {
            List<EmployeeDto> lstEmployeeDto = dalSer.GetEmployeeBySQL().ToList();
            return lstEmployeeDto;
        }

        public bool TempSave(EmployeeDto employeeDto)
        {
            //CEmployee employee = new CEmployee();
            //employee.NAME = employeeDto.NAME;
            //employee.AGE = employeeDto.AGE;
            //return dalSer.TempSave(employee);



            //CEmployee employee = AutoMapperHelper.MapTo<CEmployee>(employeeDto);

            //return dalSer.TempSave(employee);

            CEmployee c = new CEmployee();
            c.NAME = "1";
            c.AGE = "2";
            EmployeeDto sDto = AutoMapperHelper.MapTo<EmployeeDto>(c);


            CEmployee employee = AutoMapperHelper.MapTo<CEmployee>(employeeDto);

            return dalSer.TempSave(employee);

        }

        //public List<CEmployee> GetEmployeeInfo(Expression<Func<CEmployee, bool>> exp = null)
        //{
        //    IQueryable<CEmployee> S = dalSer.Find(exp);
        //    List<CEmployee> emp = S.ToList();
        //    return emp;
        //}

        //public List<EmployeeDto> GetEmployeeInfo1(Expression<Func<CEmployee, bool>> exp = null)
        //{
        //    IQueryable<CEmployee> qEmployees = dalSer.Find(exp);
        //    List<EmployeeDto> lstEmployee = AutoMapperHelper.MapToList<EmployeeDto>(qEmployees);
        //    return lstEmployee;
        //}

        /// <summary>
        /// Linq动态拼接条件
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        public  ExpressionStarter<CEmployee> HandleExpress(EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return null;

            // 定义追加的过滤变量
            var searchWhere = PredicateBuilder.New<CEmployee>();
            if (!string.IsNullOrEmpty(employeeDto.NAME))
                searchWhere = searchWhere.And(t => t.NAME == employeeDto.NAME);
            if (!string.IsNullOrWhiteSpace(employeeDto.AGE))
                searchWhere = searchWhere.And(t => t.AGE.Contains(employeeDto.AGE));

            return searchWhere;
        }

        #endregion
    }
}
