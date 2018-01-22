using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeckBuilder.Models;

namespace DeckBuilder.Pages.Sets
{
    public class IndexModel : PageModel
    {
        private readonly DeckBuilder.Models.DeckBuilderContext _context;

        public IndexModel(DeckBuilder.Models.DeckBuilderContext context)
        {
            _context = context;
        }

        public IList<Set> Set { get;set; }

        public async Task OnGetAsync()
        {
            Set = await _context.Set.ToListAsync();
        }
    }
}
