using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventPlanner.Pages.EventItems
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly EventPlanner.Data.ApplicationDbContext _context;

        public DeleteModel(EventPlanner.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EventItem EventItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventitem = await _context.EventItems.FirstOrDefaultAsync(m => m.ID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventitem = await _context.EventItems.FindAsync(id);
            if (eventitem != null)
            {
                EventItem = eventitem;
                _context.EventItems.Remove(EventItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
