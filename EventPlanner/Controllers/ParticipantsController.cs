using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public ParticipantsController(ApplicationDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<Participant>>> GetAll() => await _db.Participants.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Participant>> GetById(int id)
    {
        var item = await _db.Participants.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Participant>> Create(Participant dto)
    {
        _db.Participants.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.ID }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Participant dto)
    {
        if (id != dto.ID) return BadRequest("Id mismatch");

        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Participants.FindAsync(id);
        if (item == null) return NotFound();

        _db.Participants.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
