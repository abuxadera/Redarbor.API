using AutoMapper;
using Redarbor.API.Models;
using Redarbor.DAL.DataAccessObjects;
using Redarbor.Library.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redarbor.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeBO>();
            CreateMap<EmployeeBO, Employee>();

            CreateMap<EmployeeBO, EmployeeDAO>();
            CreateMap<EmployeeDAO, EmployeeBO>();
        }
    }
}
