using ChuckNorrisAlexaSkill.Core;
using System.Linq;

namespace ChuckNorrisAlexaSkill.Infrastructure
{
    class QuoteService : IQuoteService
    {   
        public string GetQuoteFromId(int id)
        {
            return JsonQuotesLoader.Instance.Quotes.Where(x => x.Id == id).FirstOrDefault().Quote;
        }
    }

}
