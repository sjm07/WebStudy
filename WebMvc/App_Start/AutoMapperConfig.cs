using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMvc.Model.AutoMapper;

namespace WebMvc.App_Start
{
    /// <summary>
    /// 对象自动映射配置类
    /// </summary>
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
                //cfg.AddProfile(new EmployeeProfile());
            });
        }
    }
}