using System;
using System.Collections.Generic;
using System.Text;

namespace ChuckNorrisAlexaSkill.Core
{
    public interface IQuoteService
    {
        string GetQuoteFromId(int id);
    }
}
