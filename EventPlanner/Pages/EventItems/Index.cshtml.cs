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
    public class IndexModel : PageModel
    {
        private readonly EventPlanner.Data.ApplicationDbContext _context;

        public IndexModel(EventPlanner.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EventItem> EventItem { get;set; } = new List<EventItem>();

        public async Task OnGetAsync()
        {
            EventItem = await _context.EventItems
                .Include(e => e.Organizer).ToListAsync();
        }
    }
}
