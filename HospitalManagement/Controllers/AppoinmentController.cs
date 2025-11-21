
using HospitalManagement.Data;
using HospitalManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _context.Appointments.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Appointment a)
        {
            _context.Appointments.Add(a);
            await _context.SaveChangesAsync();
            return Ok(a);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var a = await _context.Appointments.FindAsync(id);
            if (a == null) return NotFound();

            _context.Appointments.Remove(a);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
