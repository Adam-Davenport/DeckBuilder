using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeckBuilder.Models;
using DeckBuilder.Utilities;

namespace DeckBuilder.Pages.Decks
{
    public class IndexModel : PageModel
    {
        private readonly DeckBuilder.Models.DeckBuilderContext _context;

        public IndexModel(DeckBuilder.Models.DeckBuilderContext context)
        {
            _context = context;
        }

        public PaginatedList<Deck> Decks { get;set; }

        public async Task OnGetAsync(int? pageIndex)
        {
			IQueryable<Deck> DeckIQ = from deck in _context.Decks select deck;
			int PageSize = 18;
			Decks = await PaginatedList<Deck>.CreateAsync(DeckIQ, pageIndex ?? 1, PageSize);
        }
    }
}
