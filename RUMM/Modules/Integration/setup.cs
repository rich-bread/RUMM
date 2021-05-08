using Discord.Commands;
using Discord.WebSocket;
using RUMM.Common;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration.Original
{
    public class setup : ModuleBase<SocketCommandContext>
    {
        [Command ("setup")]

        public async Task Setup()
        {
            string serverid = ((Context.Guild as SocketGuild).Id).ToString();

            Method.Setup.CreateSetupFolder(serverid);

            await Context.Channel.SendSuccessAsync("完了", "セットアップしたよ！");
        }
    }
}