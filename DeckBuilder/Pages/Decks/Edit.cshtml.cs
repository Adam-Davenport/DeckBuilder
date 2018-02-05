using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeckBuilder.Models;
using DeckBuilder.Data;

namespace DeckBuilder.Pages.Decks
{
    public class EditModel : PageModel
    {
        private readonly DeckBuilder.Models.DeckBuilderContext _context;

        public EditModel(DeckBuilder.Models.DeckBuilderContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deck Deck { get; set; }

		[BindProperty]
		public string DeckString { get; set; }

		private void GetDeckString()
		{
			foreach(Decklist dl in Deck.Decklists)
			{
				DeckString += dl.Count + " " + dl.Card.Name + "\r\n";
			}
		}

		public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deck = await _context.Decks
				.Include(decklist => decklist.Decklists)
					.ThenInclude(deck => deck.Card)
				.SingleOrDefaultAsync(m => m.Id == id);

			GetDeckString();

            if (Deck == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
			RemoveOldList();
			List<Decklist> parsedList = DeckParser.ParseDeckList(DeckString, Deck.Id, _context);
			if(parsedList != null)
			{
				_context.AddRange(parsedList);
			}


            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Deck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeckExists(Deck.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

		private void RemoveOldList()
		{
			List<Decklist> oldList = _context.DeckLists
				.Where(dl => dl.DeckId == Deck.Id)
				.ToList();
			_context.RemoveRange(oldList);
			_context.SaveChanges();
		}

        private bool DeckExists(int id)
        {
            return _context.Decks.Any(e => e.Id == id);
        }
    }
}
