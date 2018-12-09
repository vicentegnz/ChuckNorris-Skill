using ChuckNorrisAlexaSkill.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChuckNorrisAlexaSkill.Models
{
    public class QuoteModel
    {
        public int Id { get; set; }

        public string Quote { get; set; }
    }
}
