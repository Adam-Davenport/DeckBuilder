using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeckBuilder.Models;

namespace DeckBuilder.Pages.Cards
{
    public class DetailsModel : PageModel
    {
        private readonly DeckBuilder.Models.DeckBuilderContext _context;

        public DetailsModel(DeckBuilder.Models.DeckBuilderContext context)
        {
            _context = context;
        }

        public Card Card { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Card = await _context.Cards
                .Include(c => c.Set).SingleOrDefaultAsync(m => m.Id == id);

            if (Card == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
