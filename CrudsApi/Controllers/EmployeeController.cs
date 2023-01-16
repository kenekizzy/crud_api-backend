using CrudsApi.Data;
using CrudsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly CrudDbContext _dbContext;

        public EmployeeController(CrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employess = await _dbContext.Employees.ToListAsync();

            return Ok(employess);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _dbContext.Employees.AddAsync(employeeRequest);
            await _dbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return BadRequest("Invalid User Id");
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return BadRequest("Invalid User");
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return BadRequest("Invalid User");
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
