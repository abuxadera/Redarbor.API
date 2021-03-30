using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Redarbor.API.Models;
using Redarbor.Library.Interfaces;
using Redarbor.Library.BusinessObjects;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redarbor.API.Controllers
{
    [ApiController]
    [Route("api/redarbor")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeLibrary _employeeLibrary;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public EmployeeController(IEmployeeLibrary employeeLibrary, IMapper mapper, ILogger logger)
        {
            _employeeLibrary = employeeLibrary;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<Employee>> GetEmployee()
        {
            try
            {
                var employeeList = _mapper.Map<List<Employee>>(_employeeLibrary.GetEmployeeList());
                return employeeList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            try
            {
                var employee = _mapper.Map<Employee>(_employeeLibrary.GetEmployeeById(id));

                if (employee == null || employee.Id == 0)
                {
                    return NotFound();
                }

                return employee;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }

        
        [HttpPost]
        public ActionResult<Employee> InsertEmployee(Employee employee)
        {
            try 
            {
                employee = _mapper.Map<Employee>(_employeeLibrary.InsertEmployee(_mapper.Map<EmployeeBO>(employee)));
                if (employee.Id > 0)
                {
                    return employee;
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateEmployee(int id, Employee employee)
        {
            try 
            { 
                if (id != employee.Id)
                {
                    return BadRequest();
                }

                if(_mapper.Map<Employee>(_employeeLibrary.GetEmployeeById(id)) == null)
                {
                    return NotFound();
                }

                _employeeLibrary.UpdateEmployee(_mapper.Map<EmployeeBO>(employee));

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                return StatusCode(_employeeLibrary.DeleteEmployee(id));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
