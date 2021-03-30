using Redarbor.DAL.DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redarbor.DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        List<EmployeeDAO> GetEmployeeList();
        EmployeeDAO GetEmployeeById(int id);
        EmployeeDAO InsertEmployee(EmployeeDAO employee);
        void UpdateEmployee(EmployeeDAO employee);
        int DeleteEmployee(int id);
    }
}
