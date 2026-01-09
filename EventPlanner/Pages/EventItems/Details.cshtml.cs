using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Data;
using EventPlanner.Models;

namespace EventPlanner.Pages.EventItems
{
    public class DetailsModel : PageModel
    {
        private readonly EventPlanner.Data.ApplicationDbContext _context;

        public DetailsModel(EventPlanner.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public EventItem EventItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventitem = await _context.EventItems
                .Include(e => e.Organizer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventitem == null)
            {
                return NotFound();
            }
            else
            {
                EventItem = eventitem;
            }
            return Page();
        }
    }
}
