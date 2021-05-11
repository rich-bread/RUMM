using Discord.Commands;
using RUMM.Common;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration.Original
{
    public class downloadeach : ModuleBase<SocketCommandContext>
    {
        [Command("downloadeach")]

        public async Task DownloadEach(string x, string z)
        {
            //サーバーID等の変数の宣言
            string serverfolder = $@"R:\Project\RUMM.warehouse\{Context.Guild.Id}";
            string trimedfolder = $@"{serverfolder}\Trimed";
            string trimed_map = $@"{trimedfolder}\TrimedMap\{x},{z}.png";

            await Context.Channel.SendFileAsync(trimed_map);
            await Context.Channel.SendSuccessAsync("完了", $"指定された地図「{x},{z}」をアップロードしたよ！");
        }
    }
}