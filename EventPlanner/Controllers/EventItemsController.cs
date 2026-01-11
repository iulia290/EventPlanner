using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace EventPlanner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventItemsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public EventItemsController(ApplicationDbContext db) => _db = db;

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<EventItem>>> GetAll() => await _db.EventItems.AsNoTracking().ToListAsync();

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EventItem>> GetById(int id)
    {
        var item = await _db.EventItems.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
        return item == null ? NotFound() : item;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<EventItem>> Create(EventItem dto)
    {
        _db.EventItems.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.ID }, dto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, EventItem dto)
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
        var item = await _db.EventItems.FindAsync(id);
        if (item == null) return NotFound();

        _db.EventItems.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }

}
