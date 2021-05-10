using Discord.Commands;
using RUMM.Common;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration
{
    public class setup : ModuleBase<SocketCommandContext>
    {
        [Command ("setup")]

        public async Task Setup()
        {
            Method.Setup.CreateSetupFolder(Context.Guild.Id);

            await Context.Channel.SendSuccessAsync("完了", "セットアップしたよ！");
        }
    }
}