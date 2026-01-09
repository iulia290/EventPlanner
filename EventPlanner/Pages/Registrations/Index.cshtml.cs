using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Pages.Registrations
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly EventPlanner.Data.ApplicationDbContext _context;

        public IndexModel(EventPlanner.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Registration> Registration { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Registration = await _context.Registrations
                .Include(r => r.EventItem)
                .Include(r => r.Participant).ToListAsync();
        }
    }
}
