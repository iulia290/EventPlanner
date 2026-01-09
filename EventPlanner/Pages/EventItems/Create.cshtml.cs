using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Authorization;


namespace EventPlanner.Pages.EventItems
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly EventPlanner.Data.ApplicationDbContext _context;

        public CreateModel(EventPlanner.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrganizerID"] = new SelectList(_context.Organizers, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public EventItem EventItem { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.EventItems.Add(EventItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
