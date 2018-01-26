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
    public class IndexModel : PageModel
    {
        private readonly DeckBuilder.Models.DeckBuilderContext _context;

        public IndexModel(DeckBuilder.Models.DeckBuilderContext context)
        {
            _context = context;
        }

        public IList<Card> Card { get;set; }

        public async Task OnGetAsync(string SortOrder, string SearchString)
        {
			IQueryable<Card> CardIQ = from c in _context.Card select c;

			if(!String.IsNullOrEmpty(SearchString))
			{
				CardIQ = CardIQ.Where(c => c.Name.Contains(SearchString));
			}
			Card = await CardIQ.AsNoTracking().ToListAsync();
        }
    }
}
