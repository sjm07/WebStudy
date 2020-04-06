using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebMvc.IBLL.System;
using WebMvc.Model;
using WebMvc.Model.DB;
using WebMvc.Model.Dto;

namespace WebMvc.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController(IEmployeeBLL employee)
        {
            this._employeeBLL = employee;
        }

        #region Property

        IEmployeeBLL _employeeBLL { get; set; }

        #endregion Property

        #region Page

        public ActionResult Index()
        {
            //EmployeeDto employeeDto = new EmployeeDto();
            //employeeDto.NAME = "张三";
            //employeeDto.AGE = "100";
            //_employeeBLL.TempSave(employeeDto);

            //CEmployee employeeModel = new CEmployee();
            //employeeModel.NAME = "张三";
            //employeeModel.AGE = "10";
            //_employeeBLL.Insert(employeeModel);
            //ViewBag.count = _employeeBLL.SaveChanges();
            return View();
        }

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Employee_Insert()
        {
            return View();
        }

        /// <summary>
        /// 获得编辑页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Employee_Edit(string id)
        {
            //List<CEmployee> lstEmployee = _employeeBLL.GetEmployeeInfo(u => u.ID.ToString() == id);
            //CEmployee employeeModel = new CEmployee();
            //if (lstEmployee != null && lstEmployee.Count > 0)
            //{
            //    ViewBag.id = lstEmployee[0].ID;
            //    ViewBag.name = lstEmployee[0].NAME;
            //    ViewBag.age = lstEmployee[0].AGE;
            //}

            CEmployee employeeModel = _employeeBLL.GetEmployeeByID(id);
            if (employeeModel != null)
            {
                ViewBag.id = employeeModel.ID;
                ViewBag.name = employeeModel.NAME;
                ViewBag.age = employeeModel.AGE;
            }

            return View(employeeModel);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult InsertEmployee(EmployeeDto model)
        {
            ResultDto resultDto = ResultDto.Instance;
            resultDto.code = "E000";//成功
            try
            {
                _employeeBLL.Insert(model);
            }
            catch (Exception ex)
            {
                resultDto.code = "E100"; //失败
                resultDto.message = ex.Message;
            }

            return Json(resultDto);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult UpdateEmployee(EmployeeDto model)
        {
            ResultDto resultDto = ResultDto.Instance;
            resultDto.code = "E000";
            try
            {
                _employeeBLL.Update(model);
            }
            catch (Exception ex)
            {
                resultDto.code = "E100";
                resultDto.message = ex.Message;
            }

            return Json(resultDto);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        public JsonResult Delete(EmployeeDto model)
        {
            ResultDto resultDto = ResultDto.Instance;
            resultDto.code = "E000";
            try
            {
                _employeeBLL.Delete(model);
            }
            catch (Exception ex)
            {
                resultDto.code = "E100";
                resultDto.message = ex.Message;
            }

            return Json(resultDto);
        }

        /// <summary>
        /// 获得雇员信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEmployeeData()
        {
            ResultDto resultDto = ResultDto.Instance;
            resultDto.code = "0";

            try
            {
                EmployeeDto model = new EmployeeDto();
                model.NAME = "三1";

                List<EmployeeDto> lstEmpDto = _employeeBLL.GetEmployeeInfo(model);
                resultDto.data = lstEmpDto;
            }
            catch (Exception ex)
            {
                resultDto.code = "E100";
                resultDto.message = ex.Message;
            }
            return Json(resultDto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeData1()
        {
            _employeeBLL.GetEmployeeBySQL();
            return null;
        }
        
        #endregion
    }
}