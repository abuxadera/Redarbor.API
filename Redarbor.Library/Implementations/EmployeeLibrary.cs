using AutoMapper;
using Redarbor.DAL.DataAccessObjects;
using Redarbor.DAL.Interfaces;
using Redarbor.Library.Interfaces;
using Redarbor.Library.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redarbor.Library.Implementations
{
    public class EmployeeLibrary : IEmployeeLibrary
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeLibrary(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public List<EmployeeBO> GetEmployeeList()
        {
            return _mapper.Map<List<EmployeeBO>>(_employeeRepository.GetEmployeeList());
        }

        public EmployeeBO GetEmployeeById(int id)
        {
            return _mapper.Map<EmployeeBO>(_employeeRepository.GetEmployeeById(id));
        }

        public EmployeeBO InsertEmployee(EmployeeBO employee)
        {
            return _mapper.Map<EmployeeBO>(_employeeRepository.InsertEmployee(_mapper.Map<EmployeeDAO>(employee)));
        }

        public void UpdateEmployee(EmployeeBO employee)
        {
            _employeeRepository.UpdateEmployee(_mapper.Map<EmployeeDAO>(employee));
        }

        public int DeleteEmployee(int id)
        {
            var employeeToDelete = _mapper.Map<EmployeeBO>(_employeeRepository.GetEmployeeById(id));
            if (employeeToDelete != null && employeeToDelete.Id == 0)
            {
                return 404;
            }

            if(_employeeRepository.DeleteEmployee(id) == 1)
            {
                return 201;
            }

            return 400;
            
        }
    }
}
