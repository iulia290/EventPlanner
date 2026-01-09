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


namespace EventPlanner.Pages.Organizers
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly EventPlanner.Data.ApplicationDbContext _context;

        public DetailsModel(EventPlanner.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Organizer Organizer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers.FirstOrDefaultAsync(m => m.ID == id);
            if (organizer == null)
            {
                return NotFound();
            }
            else
            {
                Organizer = organizer;
            }
            return Page();
        }
    }
}
