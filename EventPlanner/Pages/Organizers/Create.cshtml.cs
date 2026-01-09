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


namespace EventPlanner.Pages.Organizers
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
            return Page();
        }

        [BindProperty]
        public Organizer Organizer { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Organizers.Add(Organizer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
