using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWithUnitTesting.DTO;
using ProjectWithUnitTesting.Data;
using ProjectWithUnitTesting.Models;

namespace ProjectWithUnitTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDTOController : ControllerBase
    {
        private readonly TodoContext _context;

        public EmployeeDTOController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            //if (_context.Employees == null)
            //{
            //    return NotFound();
            //}

            if (!_context.Employees.Any())
            {
                return NotFound();
            }
            return await _context.Employees.ToListAsync();
        }

        // GET: api/EmployeeDTO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeDTO(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employeeDTO = await _context.Employees.FindAsync(id);

            if (employeeDTO == null)
            {
                return NotFound();
            }

            return employeeDTO;
        }

        // PUT: api/EmployeeDTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeDTO(int id, EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployeeDTO(EmployeeDTO employeeDTO)
        {
          if (_context.Employees == null)
          {
              return Problem("Entity set 'TodoContext.Employees'  is null.");
          }
            _context.Employees.Add(employeeDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeDTO", new { id = employeeDTO.Id }, employeeDTO);
        }

        // DELETE: api/EmployeeDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeDTO(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employeeDTO = await _context.Employees.FindAsync(id);
            if (employeeDTO == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employeeDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeDTOExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
