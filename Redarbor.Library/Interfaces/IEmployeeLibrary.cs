using Redarbor.Library.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redarbor.Library.Interfaces
{
    public interface IEmployeeLibrary
    {
        List<EmployeeBO> GetEmployeeList();
        EmployeeBO GetEmployeeById(int id);
        EmployeeBO InsertEmployee(EmployeeBO employee);
        void UpdateEmployee(EmployeeBO employee);
        int DeleteEmployee(int id);
    }
}
