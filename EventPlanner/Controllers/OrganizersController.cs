using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizersController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public OrganizersController(ApplicationDbContext db) => _db = db;

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<Organizer>>> GetAll()
        => await _db.Organizers.AsNoTracking().ToListAsync();

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Organizer>> GetById(int id)
    {
        var item = await _db.Organizers.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
        return item == null ? NotFound() : item;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Organizer>> Create(Organizer dto)
    {
        _db.Organizers.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.ID }, dto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Organizer dto)
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
        var item = await _db.Organizers.FindAsync(id);
        if (item == null) return NotFound();

        _db.Organizers.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
