using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net.Http;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;


namespace MyBot.Models.Commands.AstrologyCommand
{
    public sealed class AstrologyCommand : Command
    {
        public override string Name { get; set; }
        private string Sign { get; set; }

        public AstrologyCommand(string _sign)
        {
            //if(!AstroSigns.ContainsKey(sign))
                Name = "/" + _sign;
                Sign = _sign;
        }

        public override async Task<bool> ExecuteAsync(Telegram.Bot.Types.Message message, TelegramBotClient client)
        {

            string url = "http://mygazeta.com/гороскоп/"+ Sign + "/гороскоп-" + Sign + "-" 
                                       + DateTime.Today.ToString("dd-MM-yyyy") + ".html";
            Console.WriteLine(url);
            var response = await new HttpClient().GetAsync(url);
            string source = null;
            string all = "";
            Parser parser = new Parser();
            int i = 0;
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            var domParser = new HtmlParser();
            var document = await domParser.ParseAsync(source);
            var result = parser.Parse(document);
            foreach (var elem in result)
            {
                i++;
                if ((i % 2 == 0) && (i != 22) && (i < 25))
                    all += elem;                
            }
            await client.SendTextMessageAsync(message.Chat.Id, all);
            return true;
        }

        public override bool Contains(string command)
        {
            Console.WriteLine(this.Name + "==" + command);
            return (command == this.Name);
        }
    }


}
