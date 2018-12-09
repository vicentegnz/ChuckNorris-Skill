using ChuckNorrisAlexaSkill.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChuckNorrisAlexaSkill
{
    public class JsonQuotesLoader
    {
        #region "Const"
        
        private const string JSON_FILE = "JokesAboutChuckNorris.json";

        #endregion

        #region "Properties"

        public static JsonQuotesLoader Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        public List<QuoteModel> Quotes { get; set; }
        
        #endregion

        #region Constructors"
        private JsonQuotesLoader()
        {
            using (StreamReader r = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), JSON_FILE)))
            {
                string json = r.ReadToEnd();
                Quotes = JsonConvert.DeserializeObject<List<QuoteModel>>(json);
            }
        }

        #endregion

        #region "Singleton"
        /// <summary>
        /// Clase utilizada para el Singleton.
        /// Se implementa la solucion: fully lazy instantiation
        /// </summary>
        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
            static Nested()
            {
            }
            internal static readonly JsonQuotesLoader instance = new JsonQuotesLoader();
        }
        #endregion

    }
}