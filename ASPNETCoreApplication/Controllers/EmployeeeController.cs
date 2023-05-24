using ASPNETCoreApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeeController : ControllerBase
    {
        private readonly EmployeeeContext _employeeeContext;
        public EmployeeeController(EmployeeeContext employeeeContext)
        {
            _employeeeContext=employeeeContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employeee>>> GetEmployeees()
        {
            if (_employeeeContext.Employees == null)
            {
                return NotFound();
            }
              
            return await _employeeeContext.Employees.ToListAsync();
        }
    }
   

}

