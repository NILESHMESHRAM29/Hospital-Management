using HospitalManagement.Data;
using HospitalManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoctorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _context.Doctors.ToListAsync());

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var d = await _context.Doctors.FindAsync(id);
            return d == null ? NotFound() : Ok(d);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Doctor d)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Doctors.Add(d);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = d.Id }, d);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Doctor d)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return NotFound();

            doctor.Name = d.Name;
            doctor.Specialization = d.Specialization;
            doctor.Phone = d.Phone;   // <-- FIXED

            await _context.SaveChangesAsync();
            return Ok(doctor);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var d = await _context.Doctors.FindAsync(id);
            if (d == null) return NotFound();

            _context.Doctors.Remove(d);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
