namespace MyBot.Entities
{
    public class BotConfig
    {
        public string DbConnectionString { get; set; }

        public string BotToken { get; set; }

        public string Socks5Host { get; set; }

        public int Socks5Port { get; set; }

        public string Webhook { get; set; }
    }
}
