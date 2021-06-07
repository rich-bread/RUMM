using Discord.Commands;
using RUMM.Common;
using System.IO;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration
{
    public class recenter : ModuleBase<SocketCommandContext>
    {
        [Command("recenter")]

        public async Task Recenter(string x, string z)
        {
            //サーバーID等の変数の宣言
            string serverfolder = $@"R:\Project\RUMM.warehouse\{Context.Guild.Id}";

            string datafolder = $@"{serverfolder}\Data";
            string datafolder_recenter = $@"{datafolder}\Recenter";
            string recenter_txt = $@"{datafolder_recenter}\recenter.txt";

            string centercoord = $"{x}\r\n{z}";

            if (File.Exists(recenter_txt))
            {
                File.WriteAllText(recenter_txt, centercoord);

                await Context.Channel.SendSuccessAsync("完了", "中心座標が変更されたよ！\r\n(※基本的に1回変えたらそれ以上変更しないようにしようね！)");
            }
            else
            {
                await Context.Channel.SendErrorAsync("エラー", "まだセットアップが完了していないね..、`r.setup`で設定してみよ！");
            }
        }
    }
}