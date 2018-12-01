using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net.Sockets;
using MyBot.BLL.Contracts;
namespace MyBot.BLL.Core.Commands
{
    public class WoLCommand : Command
    {
        public override string Name { get => "/wol";  set => throw new NotImplementedException(); }

        private readonly string ip = "93.179.80.156";
        private readonly string mac = "70:85:C2:77:9C:BA";
        public override async Task<bool> ExecuteAsync(Telegram.Bot.Types.Message message, IBotService bot)
        {
            var WoLclient = new UdpClient();
            var data = new byte[102];

            for (int i = 0; i <= 5; i++)
                data[i] = 0xff;
            const int start = 6;

            for (int i = 0; i < 16; i++)
                for (int x = 0; x < 6; x++)
                    data[start + i * 6 + x] = (byte)Convert.ToInt32(mac.Split(mac.Contains("-") ? '-' : ':'));
            WoLclient.Send(data, data.Length, ip, 7);
            await bot.Client.SendTextMessageAsync(message.Chat.Id, "hello, dude!");
            return true;

        }

        public override bool Contains(string command)
        {
            return (command == this.Name);
        }
    }
}
