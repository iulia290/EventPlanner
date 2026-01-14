using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using EventPlanner.Models;

namespace EventPlanner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public RegistrationsController(ApplicationDbContext db) => _db = db;

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<List<Registration>>> GetAll()
     => await _db.Registrations.AsNoTracking().ToListAsync();

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Registration>> GetById(int id)
    {
        var item = await _db.Registrations.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
        return item == null ? NotFound() : item;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<Registration>> Create(Registration dto)
    {
        var registration = new Registration
        {
            ParticipantId = dto.ParticipantId,
            EventItemId = dto.EventItemId
        };

        _db.Registrations.Add(registration);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),
            new { id = registration.ID },
            registration);
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Registration dto)
    {
        if (id != dto.ID) return BadRequest("Id mismatch");

        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Registrations.FindAsync(id);
        if (item == null) return NotFound();

        _db.Registrations.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }


}
