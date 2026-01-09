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
    public class IndexModel : PageModel
    {
        private readonly EventPlanner.Data.ApplicationDbContext _context;

        public IndexModel(EventPlanner.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Organizer> Organizer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Organizer = await _context.Organizers.ToListAsync();
        }
    }
}
