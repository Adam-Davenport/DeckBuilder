using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeckBuilder.Models;

namespace DeckBuilder.Pages.Decks
{
    public class DetailsModel : PageModel
    {
        private readonly DeckBuilder.Models.DeckBuilderContext _context;

        public DetailsModel(DeckBuilder.Models.DeckBuilderContext context)
        {
            _context = context;
        }

        public Deck Deck { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deck = await _context.Decks.SingleOrDefaultAsync(m => m.Id == id);
			RemoveOldData();
			Deck = await _context.Decks
				.Include(d => d.Decklists)
				.AsNoTracking()
				.SingleOrDefaultAsync(m => m.Id == id);

            if (Deck == null)
            {
                return NotFound();
            }
			foreach(Decklist dl in Deck.Decklists)
			{
				dl.Card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == dl.CardId);
			}
            return Page();
        }

		private void RemoveOldData()
		{
			List<Decklist> list = _context.DeckLists.Where(dl => dl.DeckId == Deck.Id).ToList();
		}
    }
}
