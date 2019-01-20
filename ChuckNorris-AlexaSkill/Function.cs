using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using ChuckNorrisAlexaSkill.Core;
using ChuckNorrisAlexaSkill.Infrastructure;
using Newtonsoft.Json;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ChuckNorrisAlexaSkill
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// 
        /// 
        private readonly IQuoteService quoteService = new QuoteService();

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            SkillResponse response = new SkillResponse(){ Response = new ResponseBody() };
            PlainTextOutputSpeech innerResponse = new PlainTextOutputSpeech();
            ILambdaLogger log = context.Logger;

            try { 
                if (input.GetRequestType() == typeof(LaunchRequest))
                {
                    log.LogLine($"Default LaunchRequest made");
                    innerResponse.Text = "Hola soy Chuck Norris, si quieres escuchar alguna frase o chiste solo tienes que decir, dime un chiste o cuentame una frase, de lo contrario te daré una patada voladora.";
                }
                else if (input.GetRequestType() == typeof(IntentRequest))
                {
                    var intentRequest = (IntentRequest)input.Request;

                    switch (intentRequest.Intent.Name)
                    {
                        case "AMAZON.CancelIntent":
                            log.LogLine($"AMAZON.CancelIntent: enviando mensaje de cancelación.");
                            innerResponse.Text = "Lo siento, me callo, si quieres puedes pedirme otro chiste.";
                            break;
                        case "AMAZON.StopIntent":
                            log.LogLine($"AMAZON.StopIntent: enviando mensaje de stop.");
                            innerResponse.Text = "Hasta luego, espero que hayas pasado un buen rato. Guiño guiño.";
                            response.Response.ShouldEndSession = true;
                            break;
                        case "AMAZON.HelpIntent":
                            log.LogLine($"AMAZON.HelpIntent: enviando mensaje de ayuda");
                            innerResponse.Text = "Para pedir un frase o chiste solo tienes que decir, dime una frase o cuentame un chiste.";
                            break;
                        case "QuoteIntent":
                            log.LogLine($"Decir chiste de chuck Norris");
                            innerResponse.Text = GetQuote() ?? "En estos momentos Chuck Norris tiene problemas, inténtalo mas tarde o tendras una patada voladora.";
                            break;
                        default:
                            log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                            innerResponse.Text = "No entiendo lo que me estas pidiendo, pero si sigues así te daré una patada voladora.";
                            break;
                    }
                }

            }
            catch (Exception)
            {
                innerResponse.Text = $"Lo siento, no te he entendido, para pedirme una frase, solo necesitas decir, dime una frase o dime un chiste. Por favor intentelo de nuevo.";
            }
            response.Response.OutputSpeech = innerResponse;
            response.Version = "1.0";
            log.LogLine($"Skill Response Object...");
            log.LogLine(JsonConvert.SerializeObject(response));

            return response;
        }

        private string GetQuote()
        {

            Random random = new Random();
            int randomNumber = random.Next(1, JsonQuotesLoader.Instance.Quotes.Count);

            return quoteService.GetQuoteFromId(randomNumber);
        }
    }
}
