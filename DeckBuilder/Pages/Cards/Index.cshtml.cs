using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeckBuilder.Models;
using DeckBuilder.Utilities;

namespace DeckBuilder.Pages.Cards
{
    public class IndexModel : PageModel
    {
		public string CurrentFilter { get; set; }
		public string CurrentSort { get; set; }

		private readonly DeckBuilder.Models.DeckBuilderContext _context;

        public IndexModel(DeckBuilder.Models.DeckBuilderContext context)
        {
            _context = context;
        }

        public PaginatedList<Card> Card { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex)
        {
			CurrentFilter = searchString;
			IQueryable<Card> CardIQ = from c in _context.Card select c;

			if(!String.IsNullOrEmpty(searchString))
			{
				CardIQ = CardIQ.Where(c => c.Name.Contains(searchString));
			}
			int pageSize = 10;
			//Card = await CardIQ.AsNoTracking().ToListAsync();
			Card = await PaginatedList<Card>.CreateAsync(CardIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
